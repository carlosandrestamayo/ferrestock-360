using FerreStock.Datos;
using FerreStock.Estructuras;
using FerreStock.Modelos;

namespace FerreStock.Servicios
{
    /// <summary>Una parada de la ruta: un nodo donde el bodeguero recoge uno o varios productos.</summary>
    public class ParadaRuta
    {
        public int Orden { get; set; }
        public int Nodo { get; set; }
        public string Descripcion { get; set; } = "";
    }

    /// <summary>Recorrido completo para alistar un pedido: sale del mostrador y vuelve a él.</summary>
    public class RutaAlistamiento
    {
        // Todos los nodos por los que se pasa, en orden (empieza y termina en 0).
        public List<int> Camino { get; set; } = new List<int>();

        // Posición en Camino desde la que empieza el regreso al mostrador (para dibujarlo distinto).
        public int InicioRegreso { get; set; }
        public List<ParadaRuta> Paradas { get; set; } = new List<ParadaRuta>();
        public double DistanciaM { get; set; }
        public double MinutosEstimados { get; set; }

        // Ubicaciones donde hay que recoger algo (para resaltarlas en el mapa).
        public List<string> UbicacionesARecoger { get; set; } = new List<string>();

        // Productos del pedido que no tienen ubicación y no se pueden incluir en la ruta.
        public List<string> SinUbicacion { get; set; } = new List<string>();
    }

    /// <summary>
    /// Representa la bodega como un grafo (nodos = cruces de pasillos, aristas = pasillos)
    /// y calcula la ruta de alistamiento de los pedidos.
    /// </summary>
    public class BodegaServicio
    {
        // El mostrador es el origen de coordenadas y el punto de salida y llegada de toda ruta.
        public const int NodoMostrador = 0;

        // TODO estudiante: reemplazar por los valores medidos en la visita a la ferretería
        // (velocidad caminando con el carrito y tiempo promedio para sacar un producto del estante).
        public const double VelocidadMetrosPorSegundo = 0.8;
        public const double SegundosPorItem = 30;

        private readonly InventarioServicio inventario;
        private readonly List<NodoBodega> nodos;
        private readonly Grafo grafo;

        public BodegaServicio(InventarioServicio inventario)
        {
            this.inventario = inventario;
            nodos = ArchivoJson.Cargar(RutasDatos.Bodega, DatosIniciales.CrearNodosBodega());
            grafo = ConstruirGrafo(nodos);
        }

        public List<NodoBodega> ObtenerNodos()
        {
            return nodos;
        }

        public Grafo ObtenerGrafo()
        {
            return grafo;
        }

        public List<Ubicacion> ObtenerUbicaciones()
        {
            return inventario.ObtenerUbicaciones();
        }

        public Ubicacion? BuscarUbicacion(string id)
        {
            foreach (Ubicacion u in inventario.ObtenerUbicaciones())
            {
                if (u.Id == id)
                {
                    return u;
                }
            }
            return null;
        }

        /// <summary>Distancia en línea recta entre dos nodos (teorema de Pitágoras).</summary>
        public static double Distancia(NodoBodega a, NodoBodega b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>Código de producto → ubicación actual. Es la entrada normal para calcular rutas.</summary>
        public Dictionary<string, string> UbicacionesActuales()
        {
            Dictionary<string, string> resultado = new Dictionary<string, string>();
            foreach (Producto p in inventario.ObtenerProductos())
            {
                resultado[p.Codigo] = p.UbicacionId;
            }
            return resultado;
        }

        /// <summary>Ruta del pedido con las ubicaciones actuales de los productos.</summary>
        public RutaAlistamiento CalcularRuta(Pedido pedido)
        {
            return CalcularRuta(pedido, UbicacionesActuales());
        }

        /// <summary>
        /// Ruta del pedido suponiendo que cada producto está en la ubicación que dice el diccionario.
        /// Recibir el diccionario permite simular "¿qué pasaría si moviéramos los productos?".
        ///
        /// Visitar varias paradas por el camino más corto posible es un problema muy difícil
        /// (el del "agente viajero"). Se usa una regla simple y fácil de explicar, el VECINO MÁS CERCANO:
        /// desde donde está, el bodeguero va a la parada pendiente más cercana según Dijkstra.
        /// No siempre da la mejor ruta, pero da una buena y se calcula rápido.
        /// </summary>
        public RutaAlistamiento CalcularRuta(Pedido pedido, Dictionary<string, string> ubicacionPorCodigo)
        {
            RutaAlistamiento ruta = new RutaAlistamiento();

            // Paso 1: averiguar en qué nodo hay que parar por cada ítem.
            // Varios productos pueden estar junto al mismo nodo: se agrupan en una sola parada.
            List<int> pendientes = new List<int>();
            Dictionary<int, string> detallePorNodo = new Dictionary<int, string>();
            int cantidadItems = 0;

            foreach (ItemPedido item in pedido.Items)
            {
                string ubicacionId = "";
                ubicacionPorCodigo.TryGetValue(item.CodigoProducto, out string? encontrada);
                if (encontrada != null)
                {
                    ubicacionId = encontrada;
                }
                Ubicacion? ubicacion = BuscarUbicacion(ubicacionId);
                if (ubicacion == null)
                {
                    ruta.SinUbicacion.Add(item.CodigoProducto);
                    continue;
                }

                cantidadItems++;
                if (!ruta.UbicacionesARecoger.Contains(ubicacion.Id))
                {
                    ruta.UbicacionesARecoger.Add(ubicacion.Id);
                }
                string detalle = ubicacion.Id + ": " + item.CodigoProducto + " x" + item.Cantidad;
                if (detallePorNodo.ContainsKey(ubicacion.NodoGrafo))
                {
                    detallePorNodo[ubicacion.NodoGrafo] = detallePorNodo[ubicacion.NodoGrafo] + ", " + detalle;
                }
                else
                {
                    detallePorNodo[ubicacion.NodoGrafo] = detalle;
                    pendientes.Add(ubicacion.NodoGrafo);
                }
            }

            // Paso 2: salir del mostrador e ir siempre a la parada pendiente más cercana.
            int actual = NodoMostrador;
            ruta.Camino.Add(actual);
            int orden = 1;
            while (pendientes.Count > 0)
            {
                ResultadoDijkstra desdeActual = grafo.Dijkstra(actual);

                int elegido = pendientes[0];
                foreach (int nodo in pendientes)
                {
                    if (desdeActual.Distancias[nodo] < desdeActual.Distancias[elegido])
                    {
                        elegido = nodo;
                    }
                }
                if (double.IsInfinity(desdeActual.Distancias[elegido]))
                {
                    throw new InvalidOperationException("No hay un pasillo que llegue al nodo " + elegido + ". Revise bodega.json.");
                }

                AgregarTramo(ruta, desdeActual.CaminoHasta(elegido));
                ruta.DistanciaM += desdeActual.Distancias[elegido];
                ruta.Paradas.Add(new ParadaRuta { Orden = orden, Nodo = elegido, Descripcion = detallePorNodo[elegido] });

                orden++;
                pendientes.Remove(elegido);
                actual = elegido;
            }

            // Paso 3: volver al mostrador a entregar el pedido.
            ruta.InicioRegreso = ruta.Camino.Count - 1;
            if (actual != NodoMostrador)
            {
                ResultadoDijkstra regreso = grafo.Dijkstra(actual);
                AgregarTramo(ruta, regreso.CaminoHasta(NodoMostrador));
                ruta.DistanciaM += regreso.Distancias[NodoMostrador];
            }

            // Tiempo = caminar la distancia + sacar cada producto del estante.
            double segundos = ruta.DistanciaM / VelocidadMetrosPorSegundo + cantidadItems * SegundosPorItem;
            ruta.MinutosEstimados = segundos / 60;
            return ruta;
        }

        // Un tramo empieza en el nodo donde ya está la ruta: se omite el primero para no repetirlo.
        private static void AgregarTramo(RutaAlistamiento ruta, List<int> tramo)
        {
            for (int i = 1; i < tramo.Count; i++)
            {
                ruta.Camino.Add(tramo[i]);
            }
        }

        /// <summary>
        /// Arma el grafo a partir de los nodos del JSON. Revisa que los Id sean 0, 1, 2... y que
        /// los vecinos existan, porque un error ahí haría fallar todas las rutas.
        /// </summary>
        private static Grafo ConstruirGrafo(List<NodoBodega> nodos)
        {
            for (int i = 0; i < nodos.Count; i++)
            {
                if (nodos[i].Id != i)
                {
                    throw new InvalidDataException("En bodega.json los nodos deben tener Id 0, 1, 2... en orden. " +
                                                   "En la posición " + i + " aparece el Id " + nodos[i].Id + ".");
                }
            }

            Grafo grafo = new Grafo(nodos.Count);
            foreach (NodoBodega nodo in nodos)
            {
                foreach (int vecino in nodo.Vecinos)
                {
                    if (vecino < 0 || vecino >= nodos.Count)
                    {
                        throw new InvalidDataException("En bodega.json el nodo " + nodo.Id + " tiene el vecino " + vecino + ", que no existe.");
                    }
                    grafo.AgregarArista(nodo.Id, vecino, Distancia(nodo, nodos[vecino]));
                }
            }
            return grafo;
        }
    }
}
