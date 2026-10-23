using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Pruebas
{
    /// <summary>
    /// La semilla trae 5 pedidos pendientes:
    /// 1 Mayorista 8:15, 2 Mostrador 9:00, 3 Urgente 9:30, 4 Mostrador 10:10, 5 Mayorista 10:45.
    /// </summary>
    [Collection("RutasDatos")]
    public class PedidoServicioPruebas : CarpetaDatosTemporal
    {
        private readonly InventarioServicio inventario;
        private readonly MovimientoServicio movimientos;
        private readonly PedidoServicio servicio;

        public PedidoServicioPruebas()
        {
            inventario = new InventarioServicio();
            movimientos = new MovimientoServicio(inventario);
            servicio = new PedidoServicio(inventario, movimientos);
        }

        [Fact]
        public void Cola_OrdenaPorPrioridadYLuegoPorFecha()
        {
            List<Pedido> orden = servicio.ObtenerColaEnOrden();

            Assert.Equal(5, orden.Count);
            Assert.Equal(3, orden[0].Id); // Urgente
            Assert.Equal(1, orden[1].Id); // Mayorista más antiguo
            Assert.Equal(5, orden[2].Id); // Mayorista
            Assert.Equal(2, orden[3].Id); // Mostrador más antiguo
            Assert.Equal(4, orden[4].Id); // Mostrador
        }

        [Fact]
        public async Task CrearPedido_UrgenteNuevo_PasaDeSegundoEnLaCola()
        {
            List<ItemPedido> items = new List<ItemPedido>();
            servicio.AgregarItem(items, "PIN-001", 1);

            Pedido nuevo = await servicio.CrearPedidoAsync("Obra nueva", PrioridadPedido.Urgente, items);

            List<Pedido> orden = servicio.ObtenerColaEnOrden();
            Assert.Equal(6, nuevo.Id);
            Assert.Equal(3, orden[0].Id);      // el urgente más antiguo sigue primero
            Assert.Equal(nuevo.Id, orden[1].Id);
        }

        [Fact]
        public void AgregarItem_ProductoRepetido_SumaLaCantidad()
        {
            List<ItemPedido> items = new List<ItemPedido>();
            servicio.AgregarItem(items, "CON-001", 2);
            servicio.AgregarItem(items, "con-001", 3);

            Assert.Single(items);
            Assert.Equal(5, items[0].Cantidad);
        }

        [Fact]
        public async Task CrearPedido_DatosInvalidos_LanzaError()
        {
            List<ItemPedido> vacio = new List<ItemPedido>();
            List<ItemPedido> conItem = new List<ItemPedido> { new ItemPedido { CodigoProducto = "CON-001", Cantidad = 1 } };
            List<ItemPedido> productoInexistente = new List<ItemPedido> { new ItemPedido { CodigoProducto = "NO-EXISTE", Cantidad = 1 } };

            await Assert.ThrowsAsync<ArgumentException>(() => servicio.CrearPedidoAsync("Cliente", PrioridadPedido.Mostrador, vacio));
            await Assert.ThrowsAsync<ArgumentException>(() => servicio.CrearPedidoAsync("  ", PrioridadPedido.Mostrador, conItem));
            await Assert.ThrowsAsync<ArgumentException>(() => servicio.CrearPedidoAsync("Cliente", PrioridadPedido.Mostrador, productoInexistente));
            Assert.Throws<ArgumentException>(() => servicio.AgregarItem(new List<ItemPedido>(), "CON-001", 0));
        }

        [Fact]
        public async Task AtenderSiguiente_SacaElUrgenteYLoMarcaAlistado()
        {
            Pedido atendido = await servicio.AtenderSiguienteAsync();

            Assert.Equal(3, atendido.Id);
            Assert.Equal(EstadoPedido.Alistado, atendido.Estado);
            Assert.Equal(4, servicio.ObtenerColaEnOrden().Count);
        }

        [Fact]
        public async Task Despachar_DescuentaElStockYRegistraSalidas()
        {
            int cementoAntes = inventario.BuscarPorCodigo("CON-001")!.Stock;
            int arenaAntes = inventario.BuscarPorCodigo("CON-004")!.Stock;
            Pedido atendido = await servicio.AtenderSiguienteAsync(); // pedido 3: CON-001 x10, CON-004 x1

            await servicio.DespacharAsync(atendido.Id);

            Assert.Equal(EstadoPedido.Despachado, atendido.Estado);
            Assert.Equal(cementoAntes - 10, inventario.BuscarPorCodigo("CON-001")!.Stock);
            Assert.Equal(arenaAntes - 1, inventario.BuscarPorCodigo("CON-004")!.Stock);
            Assert.Equal(2, movimientos.ObtenerMovimientos().Count);
            // Las salidas de un despacho no se pueden deshacer desde movimientos.
            Assert.Equal(0, movimientos.CantidadParaDeshacer());
        }

        [Fact]
        public async Task Despachar_SinStock_NoDescuentaNadaYSigueAlistado()
        {
            Pedido atendido = await servicio.AtenderSiguienteAsync(); // pedido 3
            int cementoAntes = inventario.BuscarPorCodigo("CON-001")!.Stock;
            inventario.BuscarPorCodigo("CON-004")!.Stock = 0;       // ya no hay arena

            await Assert.ThrowsAsync<ArgumentException>(() => servicio.DespacharAsync(atendido.Id));

            Assert.Equal(EstadoPedido.Alistado, atendido.Estado);
            Assert.Equal(cementoAntes, inventario.BuscarPorCodigo("CON-001")!.Stock);
            Assert.Empty(movimientos.ObtenerMovimientos());
        }

        [Fact]
        public async Task Despachar_PedidoPendiente_LanzaError()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => servicio.DespacharAsync(1));
        }

        [Fact]
        public async Task AlReabrir_LaColaSoloTieneLosPendientes()
        {
            await servicio.AtenderSiguienteAsync();

            InventarioServicio otroInventario = new InventarioServicio();
            PedidoServicio otro = new PedidoServicio(otroInventario, new MovimientoServicio(otroInventario));

            Assert.Equal(4, otro.ObtenerColaEnOrden().Count);
            Assert.Single(otro.ObtenerPorEstado(EstadoPedido.Alistado));
        }

        [Fact]
        public void ValidarEliminacionProducto_EnPedidoActivo_LanzaError()
        {
            Assert.Throws<ArgumentException>(() => servicio.ValidarEliminacionProducto("CON-001"));
            // HER-001 no está en ningún pedido: no lanza error.
            servicio.ValidarEliminacionProducto("HER-001");
        }
    }
}
