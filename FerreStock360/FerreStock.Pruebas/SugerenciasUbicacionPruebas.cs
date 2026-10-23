using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Pruebas
{
    [Collection("RutasDatos")]
    public class SugerenciasUbicacionPruebas : CarpetaDatosTemporal
    {
        private readonly InventarioServicio inventario;
        private readonly BodegaServicio bodega;

        public SugerenciasUbicacionPruebas()
        {
            inventario = new InventarioServicio();
            bodega = new BodegaServicio(inventario);
        }

        [Fact]
        public void Sugerencias_RespetanLaCapacidadDeCadaUbicacion()
        {
            List<SugerenciaUbicacion> sugerencias = bodega.SugerirUbicaciones(1, 0.1);

            Assert.Equal(inventario.ObtenerProductos().Count, sugerencias.Count);

            Dictionary<string, double> ocupado = new Dictionary<string, double>();
            foreach (SugerenciaUbicacion s in sugerencias)
            {
                Producto p = inventario.BuscarPorCodigo(s.Codigo)!;
                if (!ocupado.ContainsKey(s.UbicacionSugerida))
                {
                    ocupado[s.UbicacionSugerida] = 0;
                }
                ocupado[s.UbicacionSugerida] += p.Stock * p.VolumenUnitarioM3;
            }
            foreach (KeyValuePair<string, double> par in ocupado)
            {
                Assert.True(par.Value <= bodega.BuscarUbicacion(par.Key)!.CapacidadM3 + 1e-9, "Sobrecupo en " + par.Key);
            }
        }

        [Fact]
        public void Sugerencias_ElCementoQuedaMasCercaDelMostrador()
        {
            SugerenciaUbicacion? cemento = null;
            foreach (SugerenciaUbicacion s in bodega.SugerirUbicaciones(1, 0.1))
            {
                if (s.Codigo == "CON-001")
                {
                    cemento = s;
                }
            }

            Assert.NotNull(cemento);
            Assert.Equal("D-01-1", cemento!.UbicacionActual);
            Assert.NotEqual("D-01-1", cemento.UbicacionSugerida);
            Assert.True(cemento.CostoSugerido < cemento.CostoActual);
        }

        [Fact]
        public void Simulacion_ConUbicacionesSugeridas_BajaElTiempoDeAlistamiento()
        {
            PedidoServicio pedidos = new PedidoServicio(inventario, new MovimientoServicio(inventario));

            ResultadoSimulacion r = bodega.SimularAntesDespues(pedidos.ObtenerPedidos(), 1, 0.1);

            Assert.Equal(5, r.CantidadPedidos);
            Assert.True(r.DistanciaPromedioDespues < r.DistanciaPromedioAntes);
            Assert.True(r.ReduccionTiempoPorcentaje > 0);
        }
    }
}
