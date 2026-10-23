using FerreStock.Matematica;

namespace FerreStock.Pruebas
{
    public class VolumenesPruebas
    {
        [Fact]
        public void MonticuloParabolico_EsPiRCuadradoHMedios()
        {
            // R = 2 m, h = 1,5 m → π·4·1,5/2 = 3π ≈ 9,4248 m³
            Assert.Equal(3 * Math.PI, Volumenes.MonticuloParabolico(2, 1.5), 10);
        }

        [Fact]
        public void MonticuloConico_EsDosTerciosDelParabolico()
        {
            double parabolico = Volumenes.MonticuloParabolico(2, 1.5);
            double conico = Volumenes.MonticuloConico(2, 1.5);

            Assert.Equal(2 * Math.PI, conico, 10);
            Assert.Equal(parabolico * 2 / 3, conico, 10);
        }

        [Fact]
        public void SumaDeRiemann_SeAcercaALaFormula()
        {
            double exacto = Volumenes.MonticuloParabolico(2, 1.5);
            double aproximado = Volumenes.MonticuloParabolicoNumerico(2, 1.5, 200);

            // Error relativo menor a 0,01 %.
            Assert.True(Math.Abs(aproximado - exacto) / exacto < 0.0001);
        }

        [Fact]
        public void AreaUtilYOcupacion()
        {
            double pasillos = Volumenes.AreaPasillos(3, 1.5, 26);   // 117 m²
            double util = Volumenes.AreaUtil(26, 16, pasillos);     // 416 - 117 = 299 m²
            double volumenUtil = Volumenes.VolumenUtil(util, 2);    // 598 m³

            Assert.Equal(117, pasillos, 10);
            Assert.Equal(299, util, 10);
            Assert.Equal(50, Volumenes.PorcentajeOcupacion(299, volumenUtil), 10);
        }

        [Fact]
        public void MedidasInvalidas_LanzanError()
        {
            Assert.Throws<ArgumentException>(() => Volumenes.MonticuloParabolico(0, 1));
            Assert.Throws<ArgumentException>(() => Volumenes.MonticuloConico(1, -1));
            Assert.Throws<ArgumentException>(() => Volumenes.AreaUtil(10, 10, 100));
        }
    }

    public class CostoUbicacionPruebas
    {
        [Fact]
        public void Costo_SeCalculaConLaFormula()
        {
            // r = 10, p = 50, punto (3, 4, 1): 10·(1·5 + 0,1·50·1) = 10·(5 + 5) = 100
            Assert.Equal(100, CostoUbicacion.Costo(10, 50, 3, 4, 1, 1, 0.1), 10);
        }

        [Fact]
        public void Costo_EsMenorCercaDelMostradorYAbajo()
        {
            double lejosYArriba = CostoUbicacion.Costo(120, 50, 22, 4, 1.8, 1, 0.1);
            double cercaYAbajo = CostoUbicacion.Costo(120, 50, 4, 4, 0.2, 1, 0.1);

            Assert.True(cercaYAbajo < lejosYArriba);
        }

        [Fact]
        public void Gradiente_ApuntaHaciaAfueraYHaciaArriba()
        {
            Gradiente g = CostoUbicacion.CalcularGradiente(10, 50, 3, 4, 1, 1, 0.1);

            Assert.Equal(10 * 3.0 / 5, g.X, 10); // r·a·x/d = 6
            Assert.Equal(10 * 4.0 / 5, g.Y, 10); // 8
            Assert.Equal(10 * 0.1 * 50, g.Z, 10); // 50
        }

        [Fact]
        public void Gradiente_EnElOrigen_NoDivideEntreCero()
        {
            Gradiente g = CostoUbicacion.CalcularGradiente(10, 50, 0, 0, 0, 1, 0.1);

            Assert.Equal(0, g.X);
            Assert.Equal(0, g.Y);
        }
    }

    public class LagrangePruebas
    {
        private static List<ProductoLagrange> Productos()
        {
            return new List<ProductoLagrange>
            {
                new ProductoLagrange { Nombre = "Cemento", Demanda = 1440, CostoPedido = 50000, CostoMantener = 7000, Volumen = 0.036 },
                new ProductoLagrange { Nombre = "Varilla", Demanda = 1800, CostoPedido = 50000, CostoMantener = 5500, Volumen = 0.0008 },
                new ProductoLagrange { Nombre = "Tubo PVC", Demanda = 840, CostoPedido = 30000, CostoMantener = 2375, Volumen = 0.0027 }
            };
        }

        [Fact]
        public void SiCabe_EsElLoteEconomicoClasico()
        {
            ResultadoLagrange r = OptimizacionLagrange.Resolver(Productos(), 100);

            Assert.False(r.RestriccionActiva);
            Assert.Equal(0, r.Lambda);
            // Q = √(2·1440·50000/7000) ≈ 143,4
            Assert.Equal(Math.Sqrt(2.0 * 1440 * 50000 / 7000), r.Cantidades[0], 6);
        }

        [Fact]
        public void SiNoCabe_LlenaExactamenteElEspacio()
        {
            List<ProductoLagrange> productos = Productos();
            double sinRestriccion = OptimizacionLagrange.VolumenUsado(productos, 0); // ≈ 5,7 m³

            ResultadoLagrange r = OptimizacionLagrange.Resolver(productos, 4);

            Assert.True(sinRestriccion > 4);
            Assert.True(r.RestriccionActiva);
            Assert.True(r.Lambda > 0);
            Assert.True(r.VolumenUsado <= 4);
            Assert.Equal(4, r.VolumenUsado, 4);
            // Con menos espacio se pide menos de cada producto.
            Assert.True(r.Cantidades[0] < OptimizacionLagrange.Cantidad(productos[0], 0));
        }

        [Fact]
        public void ConRestriccion_ElCostoEsMayorQueSinRestriccion()
        {
            double costoLibre = OptimizacionLagrange.Resolver(Productos(), 100).CostoTotal;
            double costoLimitado = OptimizacionLagrange.Resolver(Productos(), 4).CostoTotal;

            Assert.True(costoLimitado > costoLibre);
        }

        [Fact]
        public void DatosInvalidos_LanzanError()
        {
            List<ProductoLagrange> productos = Productos();
            productos[1].Volumen = 0;

            Assert.Throws<ArgumentException>(() => OptimizacionLagrange.Resolver(productos, 4));
            Assert.Throws<ArgumentException>(() => OptimizacionLagrange.Resolver(Productos(), 0));
            Assert.Throws<ArgumentException>(() => OptimizacionLagrange.Resolver(new List<ProductoLagrange>(), 4));
        }
    }
}
