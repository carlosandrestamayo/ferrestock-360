using FerreStock.Datos;
using FerreStock.Estructuras;
using FerreStock.Modelos;

namespace FerreStock.Servicios
{
    /// <summary>
    /// Registro y atención de pedidos.
    /// Flujo: Pendiente (espera en la cola) → Alistado (se sacó de la cola y se está preparando)
    /// → Despachado (salió la mercancía y se descontó el stock).
    /// La cola de prioridad propia decide qué pedido se atiende primero.
    /// </summary>
    public class PedidoServicio
    {
        private readonly InventarioServicio inventario;
        private readonly MovimientoServicio movimientos;
        private readonly List<Pedido> pedidos;

        // Solo contiene los pedidos en estado Pendiente.
        private readonly ColaPrioridad cola = new ColaPrioridad();

        public PedidoServicio(InventarioServicio inventario, MovimientoServicio movimientos)
        {
            this.inventario = inventario;
            this.movimientos = movimientos;
            pedidos = ArchivoJson.Cargar(RutasDatos.Pedidos, DatosIniciales.CrearPedidos());

            // La cola no se guarda en el archivo: se reconstruye con los pedidos pendientes.
            foreach (Pedido p in pedidos)
            {
                if (p.Estado == EstadoPedido.Pendiente)
                {
                    cola.Insertar(p);
                }
            }
        }

        // ---------------------------------------------------------------
        // Consultas
        // ---------------------------------------------------------------

        public List<Pedido> ObtenerPedidos()
        {
            return pedidos;
        }

        /// <summary>Pedidos pendientes en el orden en que se van a atender.</summary>
        public List<Pedido> ObtenerColaEnOrden()
        {
            return cola.ObtenerEnOrdenDeAtencion();
        }

        public List<Pedido> ObtenerPorEstado(EstadoPedido estado)
        {
            List<Pedido> resultado = new List<Pedido>();
            foreach (Pedido p in pedidos)
            {
                if (p.Estado == estado)
                {
                    resultado.Add(p);
                }
            }
            return resultado;
        }

        /// <summary>El próximo pedido que se atendería, o null si la cola está vacía.</summary>
        public Pedido? VerSiguiente()
        {
            if (cola.EstaVacia())
            {
                return null;
            }
            return cola.VerMinimo();
        }

        public Pedido? BuscarPorId(int id)
        {
            foreach (Pedido p in pedidos)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }
            return null;
        }

        // ---------------------------------------------------------------
        // Crear pedidos
        // ---------------------------------------------------------------

        /// <summary>
        /// Agrega un ítem a la lista de un pedido en construcción. Si el producto ya está
        /// en la lista, suma la cantidad en vez de repetir la línea.
        /// </summary>
        public void AgregarItem(List<ItemPedido> items, string codigo, int cantidad)
        {
            ValidarItem(codigo, cantidad);
            string codigoNormalizado = inventario.BuscarPorCodigo(codigo)!.Codigo;

            foreach (ItemPedido item in items)
            {
                if (item.CodigoProducto == codigoNormalizado)
                {
                    item.Cantidad = item.Cantidad + cantidad;
                    return;
                }
            }
            items.Add(new ItemPedido { CodigoProducto = codigoNormalizado, Cantidad = cantidad });
        }

        /// <summary>Crea un pedido Pendiente y lo mete en la cola de prioridad.</summary>
        public async Task<Pedido> CrearPedidoAsync(string cliente, PrioridadPedido prioridad, List<ItemPedido> items)
        {
            if (string.IsNullOrWhiteSpace(cliente))
            {
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            }
            if (items.Count == 0)
            {
                throw new ArgumentException("El pedido debe tener al menos un producto.");
            }

            // Se revisa cada ítem y que ningún producto esté repetido.
            List<string> codigosVistos = new List<string>();
            foreach (ItemPedido item in items)
            {
                ValidarItem(item.CodigoProducto, item.Cantidad);
                if (codigosVistos.Contains(item.CodigoProducto))
                {
                    throw new ArgumentException("El producto " + item.CodigoProducto + " está repetido en el pedido.");
                }
                codigosVistos.Add(item.CodigoProducto);
            }

            Pedido pedido = new Pedido();
            pedido.Id = SiguienteId();
            pedido.Fecha = DateTime.Now;
            pedido.Cliente = cliente.Trim();
            pedido.Prioridad = prioridad;
            pedido.Estado = EstadoPedido.Pendiente;
            // Se copian los ítems para que el formulario pueda limpiar su lista sin afectar el pedido.
            foreach (ItemPedido item in items)
            {
                pedido.Items.Add(new ItemPedido { CodigoProducto = item.CodigoProducto, Cantidad = item.Cantidad });
            }

            pedidos.Add(pedido);
            cola.Insertar(pedido);
            await GuardarAsync();
            return pedido;
        }

        // ---------------------------------------------------------------
        // Atender y despachar
        // ---------------------------------------------------------------

        /// <summary>
        /// Saca de la cola el pedido más prioritario y lo marca como Alistado.
        /// Aquí todavía no se toca el stock: eso pasa al despachar.
        /// </summary>
        public async Task<Pedido> AtenderSiguienteAsync()
        {
            if (cola.EstaVacia())
            {
                throw new ArgumentException("No hay pedidos pendientes.");
            }
            Pedido pedido = cola.ExtraerMinimo();
            pedido.Estado = EstadoPedido.Alistado;
            await GuardarAsync();
            return pedido;
        }

        /// <summary>
        /// Despacha un pedido alistado: genera una salida de inventario por cada ítem.
        /// Si algún producto no tiene stock suficiente no se despacha nada y el pedido sigue Alistado.
        /// </summary>
        public async Task DespacharAsync(int pedidoId)
        {
            Pedido? pedido = BuscarPorId(pedidoId);
            if (pedido == null)
            {
                throw new ArgumentException("No existe el pedido " + pedidoId + ".");
            }
            if (pedido.Estado != EstadoPedido.Alistado)
            {
                throw new ArgumentException("Solo se pueden despachar pedidos alistados. El pedido " + pedidoId +
                                            " está " + pedido.Estado + ".");
            }

            // Si falta stock, este método lanza el error ANTES de cambiar algo.
            await movimientos.RegistrarSalidasDePedidoAsync(pedido);

            pedido.Estado = EstadoPedido.Despachado;
            await GuardarAsync();
        }

        /// <summary>
        /// Impide eliminar un producto que esté en un pedido sin despachar,
        /// porque luego ese pedido no se podría despachar.
        /// </summary>
        public void ValidarEliminacionProducto(string codigo)
        {
            string buscado = codigo.Trim().ToUpper();
            foreach (Pedido p in pedidos)
            {
                if (p.Estado == EstadoPedido.Despachado)
                {
                    continue;
                }
                foreach (ItemPedido item in p.Items)
                {
                    if (item.CodigoProducto == buscado)
                    {
                        throw new ArgumentException("No se puede eliminar " + buscado + ": está en el pedido " + p.Id +
                                                    " (" + p.Estado + ").");
                    }
                }
            }
        }

        // ---------------------------------------------------------------
        // Métodos de apoyo
        // ---------------------------------------------------------------

        private void ValidarItem(string codigo, int cantidad)
        {
            if (string.IsNullOrWhiteSpace(codigo) || inventario.BuscarPorCodigo(codigo) == null)
            {
                throw new ArgumentException("Debe elegir un producto que exista.");
            }
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que cero.");
            }
        }

        private int SiguienteId()
        {
            int mayor = 0;
            foreach (Pedido p in pedidos)
            {
                if (p.Id > mayor)
                {
                    mayor = p.Id;
                }
            }
            return mayor + 1;
        }

        private async Task GuardarAsync()
        {
            await ArchivoJson.GuardarAsync(RutasDatos.Pedidos, pedidos);
        }
    }
}
