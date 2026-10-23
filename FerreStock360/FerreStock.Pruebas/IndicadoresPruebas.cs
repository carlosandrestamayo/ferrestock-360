using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Pruebas
{
    public class ClasificacionABCPruebas
    {
        private static Producto P(string codigo, int ventas, decimal precio)
        {
            return new Producto { Codigo = codigo, Nombre = codigo, VentasMes = ventas, PrecioVenta = precio };
        }

        [Fact]
        public void ABC_ClasificaPorValorVendidoAcumulado()
        {
            // Valores: X = 700, Y = 150, Z = 100, W = 50 → total 1.000
            // Acumulado antes de cada uno: X 0 % (A), Y 70 % (A), Z 85 % (B), W 95 % (C)
            List<Producto> productos = new List<Producto> { P("W", 5, 10), P("Y", 15, 10), P("X", 70, 10), P("Z", 10, 10) };

            List<ItemABC> abc = IndicadorServicio.ClasificarABC(productos);

            Assert.Equal("X", abc[0].Codigo);
            Assert.Equal("A", abc[0].Clase);
            Assert.Equal("A", abc[1].Clase);
            Assert.Equal("B", abc[2].Clase);
            Assert.Equal("C", abc[3].Clase);
            Assert.Equal(100, abc[3].PorcentajeAcumulado, 6);
        }

        [Fact]
        public void ABC_ElMasVendidoSiempreEsA()
        {
            // Un solo producto concentra el 95 % de las ventas.
            List<Producto> productos = new List<Producto> { P("X", 95, 10), P("Y", 5, 10) };

            List<ItemABC> abc = IndicadorServicio.ClasificarABC(productos);

            Assert.Equal("A", abc[0].Clase);
            Assert.Equal("C", abc[1].Clase);
        }

        [Fact]
        public void ABC_SinVentas_TodosSonC()
        {
            List<Producto> productos = new List<Producto> { P("X", 0, 10), P("Y", 0, 10) };

            List<ItemABC> abc = IndicadorServicio.ClasificarABC(productos);

            Assert.Equal("C", abc[0].Clase);
            Assert.Equal("C", abc[1].Clase);
        }
    }

    [Collection("RutasDatos")]
    public class IndicadorServicioPruebas : CarpetaDatosTemporal
    {
        private readonly InventarioServicio inventario;
        private readonly MovimientoServicio movimientos;
        private readonly IndicadorServicio indicadores;

        public IndicadorServicioPruebas()
        {
            inventario = new InventarioServicio();
            movimientos = new MovimientoServicio(inventario);
            PedidoServicio pedidos = new PedidoServicio(inventario, movimientos);
            indicadores = new IndicadorServicio(inventario, movimientos, pedidos, new BodegaServicio(inventario));
        }

        [Fact]
        public void Reporte_RotacionYDiasDeInventarioSonCoherentes()
        {
            ReporteIndicadores r = indicadores.Calcular();

            Assert.True(r.RotacionMensual > 0);
            Assert.Equal(30 / r.RotacionMensual, r.DiasInventario, 6);
            Assert.Equal((double)(r.VentasMesCosto / r.ValorInventarioCosto), r.RotacionMensual, 6);
        }

        [Fact]
        public void Reporte_OcupacionTotalCoincideConLasZonas()
        {
            ReporteIndicadores r = indicadores.Calcular();

            double ocupado = 0;
            double capacidad = 0;
            foreach (OcupacionZona z in r.OcupacionPorZona)
            {
                ocupado += z.OcupadoM3;
                capacidad += z.CapacidadM3;
            }
            Assert.Equal(6, r.OcupacionPorZona.Count);
            Assert.Equal(ocupado / capacidad * 100, r.OcupacionPorcentaje, 6);
            Assert.Equal(inventario.VolumenOcupado(), ocupado, 6);
        }

        [Fact]
        public void Reporte_IncluyeABCYBajoMinimo()
        {
            ReporteIndicadores r = indicadores.Calcular();

            Assert.Equal(inventario.ObtenerProductos().Count, r.ClasificacionABC.Count);
            Assert.Equal(3, r.BajoStockMinimo.Count);
        }

        [Fact]
        public async Task Exactitud_CuentaLosConteosSinDiferencia()
        {
            int stockCemento = inventario.BuscarPorCodigo("CON-001")!.Stock;
            int stockTubo = inventario.BuscarPorCodigo("PLO-001")!.Stock;
            await movimientos.RegistrarAjusteAsync("CON-001", stockCemento, "Conteo exacto");
            await movimientos.RegistrarAjusteAsync("PLO-001", stockTubo - 3, "Faltaban tres");

            ReporteIndicadores r = indicadores.Calcular();

            Assert.Equal(2, r.ConteosRealizados);
            Assert.Equal(1, r.ConteosExactos);
            Assert.Equal(50, r.ExactitudPorcentaje, 6);
        }
    }
}
