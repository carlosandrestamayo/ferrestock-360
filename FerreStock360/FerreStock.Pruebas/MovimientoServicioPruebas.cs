using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Pruebas
{
    [Collection("RutasDatos")]
    public class MovimientoServicioPruebas : CarpetaDatosTemporal
    {
        private readonly InventarioServicio inventario;
        private readonly MovimientoServicio servicio;

        public MovimientoServicioPruebas()
        {
            inventario = new InventarioServicio();
            servicio = new MovimientoServicio(inventario);
        }

        private int StockDe(string codigo)
        {
            return inventario.BuscarPorCodigo(codigo)!.Stock;
        }

        [Fact]
        public async Task Entrada_SumaAlStock()
        {
            int antes = StockDe("CON-001");

            await servicio.RegistrarEntradaAsync("CON-001", 20, "Compra");

            Assert.Equal(antes + 20, StockDe("CON-001"));
            Assert.Single(servicio.ObtenerMovimientos());
        }

        [Fact]
        public async Task Salida_RestaAlStock()
        {
            int antes = StockDe("CON-001");

            await servicio.RegistrarSalidaAsync("CON-001", 5, "Venta");

            Assert.Equal(antes - 5, StockDe("CON-001"));
        }

        [Fact]
        public async Task Salida_SinStockSuficiente_LanzaErrorYNoCambiaNada()
        {
            int antes = StockDe("CON-001");

            await Assert.ThrowsAsync<ArgumentException>(() => servicio.RegistrarSalidaAsync("CON-001", antes + 1, ""));

            Assert.Equal(antes, StockDe("CON-001"));
            Assert.Empty(servicio.ObtenerMovimientos());
        }

        [Fact]
        public async Task CantidadCeroONegativa_LanzaError()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => servicio.RegistrarEntradaAsync("CON-001", 0, ""));
            await Assert.ThrowsAsync<ArgumentException>(() => servicio.RegistrarSalidaAsync("CON-001", -3, ""));
        }

        [Fact]
        public async Task Ajuste_IgualaElStockAlConteoYGuardaLaDiferencia()
        {
            int antes = StockDe("PLO-001"); // 60 en la semilla

            Movimiento ajuste = await servicio.RegistrarAjusteAsync("PLO-001", antes - 2, "Faltaban dos");

            Assert.Equal(antes - 2, StockDe("PLO-001"));
            Assert.Equal(-2, ajuste.Cantidad);
            Assert.Equal(TipoMovimiento.Ajuste, ajuste.Tipo);
        }

        [Fact]
        public async Task Deshacer_RevierteEnOrdenInverso()
        {
            int antes = StockDe("CON-001");
            await servicio.RegistrarEntradaAsync("CON-001", 10, "");
            await servicio.RegistrarSalidaAsync("CON-001", 3, "");

            Movimiento primero = await servicio.DeshacerAsync();
            Assert.Equal(TipoMovimiento.Salida, primero.Tipo);
            Assert.Equal(antes + 10, StockDe("CON-001"));

            Movimiento segundo = await servicio.DeshacerAsync();
            Assert.Equal(TipoMovimiento.Entrada, segundo.Tipo);
            Assert.Equal(antes, StockDe("CON-001"));
            Assert.Empty(servicio.ObtenerMovimientos());
            Assert.Equal(0, servicio.CantidadParaDeshacer());
        }

        [Fact]
        public async Task Deshacer_SiElStockQuedariaNegativo_LanzaErrorYConservaLaPila()
        {
            await servicio.RegistrarEntradaAsync("ELE-001", 5, "");
            // Se vende todo el stock directamente en el producto (por ejemplo, editado en el CRUD).
            inventario.BuscarPorCodigo("ELE-001")!.Stock = 0;

            await Assert.ThrowsAsync<ArgumentException>(() => servicio.DeshacerAsync());

            Assert.Equal(1, servicio.CantidadParaDeshacer());
        }

        [Fact]
        public async Task Deshacer_SinMovimientos_LanzaError()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => servicio.DeshacerAsync());
        }

        [Fact]
        public async Task Movimientos_SeGuardanEnElArchivo()
        {
            await servicio.RegistrarEntradaAsync("CON-001", 10, "Compra");

            // Servicios nuevos leen de nuevo los archivos.
            InventarioServicio otroInventario = new InventarioServicio();
            MovimientoServicio otro = new MovimientoServicio(otroInventario);

            Assert.Single(otro.ObtenerMovimientos());
            Assert.Equal(StockDe("CON-001"), otroInventario.BuscarPorCodigo("CON-001")!.Stock);
            // La pila no se guarda: al reabrir el programa no hay nada para deshacer.
            Assert.Equal(0, otro.CantidadParaDeshacer());
        }
    }
}
