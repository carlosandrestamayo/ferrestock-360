using FerreStock.Datos;
using FerreStock.Estructuras;
using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Pruebas
{
    public class GrafoPruebas
    {
        /// <summary>
        /// Grafo de ejemplo que también se puede resolver a mano en el tablero:
        ///
        ///   0 --4,5-- 1 --1-- 3
        ///   |         |       |
        ///   1         2       5
        ///   |         |       |
        ///   2 ---1--- 4 ------+
        ///
        /// De 0 a 1 conviene dar la vuelta (0 → 2 → 4 → 1 = 4) en vez de ir directo (4,5).
        /// </summary>
        private static Grafo CrearGrafoEjemplo()
        {
            Grafo grafo = new Grafo(5);
            grafo.AgregarArista(0, 1, 4.5);
            grafo.AgregarArista(0, 2, 1);
            grafo.AgregarArista(1, 3, 1);
            grafo.AgregarArista(1, 4, 2);
            grafo.AgregarArista(2, 4, 1);
            grafo.AgregarArista(3, 4, 5);
            return grafo;
        }

        [Fact]
        public void Dijkstra_CalculaLasDistanciasMasCortas()
        {
            ResultadoDijkstra resultado = CrearGrafoEjemplo().Dijkstra(0);

            Assert.Equal(0, resultado.Distancias[0]);
            Assert.Equal(4, resultado.Distancias[1]);   // 0-2-4-1 = 4, mejor que 0-1 directo = 4,5
            Assert.Equal(1, resultado.Distancias[2]);
            Assert.Equal(5, resultado.Distancias[3]);
            Assert.Equal(2, resultado.Distancias[4]);
        }

        [Fact]
        public void CaminoHasta_DevuelveLosNodosEnOrden()
        {
            ResultadoDijkstra resultado = CrearGrafoEjemplo().Dijkstra(0);

            Assert.Equal(new List<int> { 0, 2, 4, 1, 3 }, resultado.CaminoHasta(3));
            Assert.Equal(new List<int> { 0 }, resultado.CaminoHasta(0));
        }

        [Fact]
        public void NodoSinConexion_QuedaEnInfinitoYSinCamino()
        {
            Grafo grafo = new Grafo(3);
            grafo.AgregarArista(0, 1, 2);

            ResultadoDijkstra resultado = grafo.Dijkstra(0);

            Assert.True(double.IsInfinity(resultado.Distancias[2]));
            Assert.Empty(resultado.CaminoHasta(2));
        }

        [Fact]
        public void AgregarArista_EsNoDirigidaYNoSeRepite()
        {
            Grafo grafo = new Grafo(2);
            grafo.AgregarArista(0, 1, 3);
            grafo.AgregarArista(1, 0, 3);

            Assert.Single(grafo.ObtenerVecinos(0));
            Assert.Single(grafo.ObtenerVecinos(1));
            Assert.Throws<ArgumentException>(() => grafo.AgregarArista(0, 5, 1));
            Assert.Throws<ArgumentException>(() => grafo.AgregarArista(0, 1, -1));
        }
    }

    [Collection("RutasDatos")]
    public class BodegaServicioPruebas : CarpetaDatosTemporal
    {
        private readonly BodegaServicio bodega;

        public BodegaServicioPruebas()
        {
            bodega = new BodegaServicio(new InventarioServicio());
        }

        [Fact]
        public void Semilla_TodosLosNodosSonAlcanzablesDesdeElMostrador()
        {
            ResultadoDijkstra resultado = bodega.ObtenerGrafo().Dijkstra(BodegaServicio.NodoMostrador);

            Assert.Equal(13, bodega.ObtenerNodos().Count);
            foreach (double distancia in resultado.Distancias)
            {
                Assert.False(double.IsInfinity(distancia));
            }
        }

        [Fact]
        public void Ruta_PedidoUrgenteDeLaSemilla()
        {
            // Pedido 3: CON-001 (D-01-1, nodo 4) y CON-004 (D-03-1, nodo 8).
            Pedido pedido = new Pedido { Id = 3 };
            pedido.Items.Add(new ItemPedido { CodigoProducto = "CON-001", Cantidad = 10 });
            pedido.Items.Add(new ItemPedido { CodigoProducto = "CON-004", Cantidad = 1 });

            RutaAlistamiento ruta = bodega.CalcularRuta(pedido);

            // Mostrador → nodo 4: √(4² + 2²) + 6 + 6 + 6 = 22,47 m; nodo 4 → 8: 6 m; nodo 8 → mostrador: 28,47 m.
            double esperado = 2 * Math.Sqrt(20) + 18 + 6 + 6 + 18;
            Assert.Equal(esperado, ruta.DistanciaM, 6);
            Assert.Equal(2, ruta.Paradas.Count);
            Assert.Equal(4, ruta.Paradas[0].Nodo); // el más cercano primero
            Assert.Equal(8, ruta.Paradas[1].Nodo);
            Assert.Equal(0, ruta.Camino[0]);
            Assert.Equal(0, ruta.Camino[ruta.Camino.Count - 1]);
            Assert.Empty(ruta.SinUbicacion);
        }

        [Fact]
        public void Ruta_ProductosEnElMismoNodo_SeAgrupanEnUnaParada()
        {
            // PLO-004 y PLO-005 están en C-02-2 (nodo 7).
            Pedido pedido = new Pedido { Id = 99 };
            pedido.Items.Add(new ItemPedido { CodigoProducto = "PLO-004", Cantidad = 1 });
            pedido.Items.Add(new ItemPedido { CodigoProducto = "PLO-005", Cantidad = 1 });

            RutaAlistamiento ruta = bodega.CalcularRuta(pedido);

            Assert.Single(ruta.Paradas);
            Assert.Contains("PLO-004", ruta.Paradas[0].Descripcion);
            Assert.Contains("PLO-005", ruta.Paradas[0].Descripcion);
        }

        [Fact]
        public void Ruta_ConUbicacionesSimuladas_UsaElDiccionario()
        {
            Pedido pedido = new Pedido { Id = 1 };
            pedido.Items.Add(new ItemPedido { CodigoProducto = "CON-001", Cantidad = 1 });
            Dictionary<string, string> simuladas = new Dictionary<string, string> { { "CON-001", "A-01-1" } };

            RutaAlistamiento ruta = bodega.CalcularRuta(pedido, simuladas);

            // Ida y vuelta al nodo 1: 2 × √20.
            Assert.Equal(2 * Math.Sqrt(20), ruta.DistanciaM, 6);
        }

        [Fact]
        public void Ruta_ProductoSinUbicacion_SeReporta()
        {
            Pedido pedido = new Pedido { Id = 1 };
            pedido.Items.Add(new ItemPedido { CodigoProducto = "CON-001", Cantidad = 1 });

            RutaAlistamiento ruta = bodega.CalcularRuta(pedido, new Dictionary<string, string>());

            Assert.Equal(new List<string> { "CON-001" }, ruta.SinUbicacion);
            Assert.Equal(0, ruta.DistanciaM);
        }
    }
}
