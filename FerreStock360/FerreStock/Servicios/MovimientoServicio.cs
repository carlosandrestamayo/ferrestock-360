using FerreStock.Datos;
using FerreStock.Estructuras;
using FerreStock.Modelos;

namespace FerreStock.Servicios
{
    /// <summary>
    /// Registra entradas, salidas y ajustes de inventario, y actualiza el stock del producto.
    /// Cada movimiento que hace el usuario se guarda también en una Pila propia
    /// para poder deshacerlo (el último registrado es el primero que se revierte).
    /// </summary>
    public class MovimientoServicio
    {
        private readonly InventarioServicio inventario;
        private readonly List<Movimiento> movimientos;

        // La pila solo vive mientras el programa está abierto: al cerrarlo,
        // los movimientos quedan guardados y ya no se pueden deshacer.
        private readonly Pila<Movimiento> pilaDeshacer = new Pila<Movimiento>();

        public MovimientoServicio(InventarioServicio inventario)
        {
            this.inventario = inventario;
            movimientos = ArchivoJson.Cargar(RutasDatos.Movimientos, DatosIniciales.CrearMovimientos());
        }

        // ---------------------------------------------------------------
        // Consultas
        // ---------------------------------------------------------------

        public List<Movimiento> ObtenerMovimientos()
        {
            return movimientos;
        }

        /// <summary>Copia de la lista con el movimiento más reciente primero (para mostrar en la tabla).</summary>
        public List<Movimiento> ObtenerRecientesPrimero()
        {
            List<Movimiento> resultado = new List<Movimiento>();
            // Se recorre la lista de atrás hacia adelante porque los nuevos se agregan al final.
            for (int i = movimientos.Count - 1; i >= 0; i--)
            {
                resultado.Add(movimientos[i]);
            }
            return resultado;
        }

        public int CantidadParaDeshacer()
        {
            return pilaDeshacer.Cantidad;
        }

        /// <summary>El movimiento que se revertiría al pulsar Deshacer, o null si no hay.</summary>
        public Movimiento? VerSiguienteParaDeshacer()
        {
            if (pilaDeshacer.EstaVacia())
            {
                return null;
            }
            return pilaDeshacer.VerTope();
        }

        // ---------------------------------------------------------------
        // Registrar movimientos
        // ---------------------------------------------------------------

        /// <summary>Llega mercancía: el stock aumenta en la cantidad indicada.</summary>
        public async Task<Movimiento> RegistrarEntradaAsync(string codigo, int cantidad, string observacion)
        {
            Producto producto = ObtenerProducto(codigo);
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad de la entrada debe ser mayor que cero.");
            }
            return await RegistrarAsync(producto, TipoMovimiento.Entrada, cantidad, observacion);
        }

        /// <summary>Sale mercancía: el stock disminuye. No se permite dejar el stock negativo.</summary>
        public async Task<Movimiento> RegistrarSalidaAsync(string codigo, int cantidad, string observacion)
        {
            Producto producto = ObtenerProducto(codigo);
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad de la salida debe ser mayor que cero.");
            }
            if (cantidad > producto.Stock)
            {
                throw new ArgumentException("Stock insuficiente de " + producto.Codigo + ": hay " + producto.Stock +
                                            " y se quieren sacar " + cantidad + ".");
            }
            return await RegistrarAsync(producto, TipoMovimiento.Salida, cantidad, observacion);
        }

        /// <summary>
        /// Después de contar físicamente un producto, el stock del sistema se iguala al conteo.
        /// En el movimiento se guarda la DIFERENCIA (conteo - sistema): positiva si sobraba,
        /// negativa si faltaba y cero si el inventario estaba exacto.
        /// </summary>
        public async Task<Movimiento> RegistrarAjusteAsync(string codigo, int conteoFisico, string observacion)
        {
            Producto producto = ObtenerProducto(codigo);
            if (conteoFisico < 0)
            {
                throw new ArgumentException("El conteo físico no puede ser negativo.");
            }
            int diferencia = conteoFisico - producto.Stock;
            string detalle = "Sistema: " + producto.Stock + ", conteo: " + conteoFisico + ". " + observacion.Trim();
            return await RegistrarAsync(producto, TipoMovimiento.Ajuste, diferencia, detalle);
        }

        /// <summary>
        /// Genera una salida por cada ítem del pedido. Primero revisa TODOS los ítems y,
        /// solo si hay stock para todos, descuenta; así nunca se despacha un pedido a medias.
        /// Estas salidas no van a la pila de deshacer, porque deshacerlas dejaría el pedido
        /// marcado como despachado sin que la mercancía haya salido.
        /// </summary>
        public async Task RegistrarSalidasDePedidoAsync(Pedido pedido)
        {
            // Paso 1: validar todo antes de tocar el stock.
            foreach (ItemPedido item in pedido.Items)
            {
                Producto producto = ObtenerProducto(item.CodigoProducto);
                if (item.Cantidad > producto.Stock)
                {
                    throw new ArgumentException("Stock insuficiente de " + producto.Codigo + " para el pedido " + pedido.Id +
                                                ": hay " + producto.Stock + " y se necesitan " + item.Cantidad + ".");
                }
            }

            // Paso 2: descontar y registrar cada salida.
            foreach (ItemPedido item in pedido.Items)
            {
                Producto producto = ObtenerProducto(item.CodigoProducto);
                Movimiento movimiento = CrearMovimiento(producto.Codigo, TipoMovimiento.Salida, item.Cantidad,
                                                        "Despacho del pedido " + pedido.Id + " (" + pedido.Cliente + ")");
                producto.Stock = producto.Stock - item.Cantidad;
                movimientos.Add(movimiento);
            }

            // Paso 3: guardar una sola vez al final.
            await GuardarAsync();
        }

        /// <summary>
        /// Revierte el último movimiento registrado por el usuario (el tope de la pila)
        /// y lo borra del historial. Devuelve el movimiento deshecho.
        /// </summary>
        public async Task<Movimiento> DeshacerAsync()
        {
            if (pilaDeshacer.EstaVacia())
            {
                throw new ArgumentException("No hay movimientos para deshacer.");
            }

            // Paso 1: mirar el tope SIN quitarlo, por si no se puede deshacer.
            Movimiento ultimo = pilaDeshacer.VerTope();
            Producto? producto = inventario.BuscarPorCodigo(ultimo.CodigoProducto);
            if (producto == null)
            {
                // Si el producto se eliminó no hay stock que revertir; se descarta para no bloquear la pila.
                pilaDeshacer.Desapilar();
                throw new ArgumentException("El producto " + ultimo.CodigoProducto +
                                            " ya no existe. El movimiento se descartó de la lista de deshacer.");
            }

            // Paso 2: calcular el stock que quedaría al revertir.
            // Entrada sumó → se resta. Salida restó → se suma. Ajuste sumó la diferencia → se resta.
            int stockRevertido;
            if (ultimo.Tipo == TipoMovimiento.Salida)
            {
                stockRevertido = producto.Stock + ultimo.Cantidad;
            }
            else
            {
                stockRevertido = producto.Stock - ultimo.Cantidad;
            }

            // Esto pasa, por ejemplo, si se deshace una entrada cuando ya se vendió parte de esa mercancía.
            if (stockRevertido < 0)
            {
                throw new ArgumentException("No se puede deshacer: el stock de " + producto.Codigo + " quedaría negativo.");
            }

            // Paso 3: ahora sí, quitar de la pila, aplicar y borrar del historial.
            pilaDeshacer.Desapilar();
            producto.Stock = stockRevertido;
            movimientos.Remove(ultimo);
            await GuardarAsync();
            return ultimo;
        }

        // ---------------------------------------------------------------
        // Métodos de apoyo
        // ---------------------------------------------------------------

        // Pasos comunes a entrada, salida y ajuste: crear el movimiento, aplicar el stock,
        // agregarlo al historial, apilarlo para deshacer y guardar.
        private async Task<Movimiento> RegistrarAsync(Producto producto, TipoMovimiento tipo, int cantidad, string observacion)
        {
            Movimiento movimiento = CrearMovimiento(producto.Codigo, tipo, cantidad, observacion);

            if (tipo == TipoMovimiento.Salida)
            {
                producto.Stock = producto.Stock - cantidad;
            }
            else
            {
                // Entrada suma la cantidad; Ajuste suma la diferencia (que puede ser negativa).
                producto.Stock = producto.Stock + cantidad;
            }

            movimientos.Add(movimiento);
            pilaDeshacer.Apilar(movimiento);
            await GuardarAsync();
            return movimiento;
        }

        private Movimiento CrearMovimiento(string codigo, TipoMovimiento tipo, int cantidad, string observacion)
        {
            Movimiento movimiento = new Movimiento();
            movimiento.Id = SiguienteId();
            movimiento.Fecha = DateTime.Now;
            movimiento.CodigoProducto = codigo;
            movimiento.Tipo = tipo;
            movimiento.Cantidad = cantidad;
            movimiento.Observacion = observacion.Trim();
            return movimiento;
        }

        // El nuevo Id es el mayor que exista más uno. Así no se repite aunque se hayan borrado movimientos.
        private int SiguienteId()
        {
            int mayor = 0;
            foreach (Movimiento m in movimientos)
            {
                if (m.Id > mayor)
                {
                    mayor = m.Id;
                }
            }
            return mayor + 1;
        }

        private Producto ObtenerProducto(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new ArgumentException("Debe elegir un producto.");
            }
            Producto? producto = inventario.BuscarPorCodigo(codigo);
            if (producto == null)
            {
                throw new ArgumentException("No existe un producto con el código " + codigo + ".");
            }
            return producto;
        }

        // Se guardan los dos archivos porque un movimiento cambia el stock del producto.
        private async Task GuardarAsync()
        {
            await inventario.GuardarAsync();
            await ArchivoJson.GuardarAsync(RutasDatos.Movimientos, movimientos);
        }
    }
}
