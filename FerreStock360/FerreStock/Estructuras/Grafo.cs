namespace FerreStock.Estructuras
{
    /// <summary>Conexión desde un nodo hacia otro, con su distancia en metros.</summary>
    public class Arista
    {
        public int Destino { get; set; }
        public double Peso { get; set; }
    }

    /// <summary>
    /// Resultado de Dijkstra desde un nodo de origen:
    /// - Distancias[v]: metros del camino más corto desde el origen hasta v (infinito si no hay camino).
    /// - Anteriores[v]: nodo por el que se llega a v en ese camino (-1 si es el origen o no hay camino).
    /// </summary>
    public class ResultadoDijkstra
    {
        public int Origen { get; set; }
        public double[] Distancias { get; set; } = new double[0];
        public int[] Anteriores { get; set; } = new int[0];

        /// <summary>
        /// Reconstruye el camino del origen al destino siguiendo los "anteriores" hacia atrás.
        /// Como se recorre desde el destino, se usa la Pila propia para invertir el orden.
        /// Devuelve una lista vacía si el destino no se puede alcanzar.
        /// </summary>
        public List<int> CaminoHasta(int destino)
        {
            List<int> camino = new List<int>();
            if (double.IsInfinity(Distancias[destino]))
            {
                return camino;
            }

            Pila<int> pila = new Pila<int>();
            int actual = destino;
            while (actual != -1)
            {
                pila.Apilar(actual);
                actual = Anteriores[actual];
            }
            while (!pila.EstaVacia())
            {
                camino.Add(pila.Desapilar());
            }
            return camino;
        }
    }

    /// <summary>
    /// Grafo no dirigido y con pesos, guardado como lista de adyacencia:
    /// para cada nodo se guarda la lista de aristas que salen de él.
    /// Se eligió lista de adyacencia (y no matriz) porque en una bodega cada cruce
    /// tiene pocos vecinos (2 a 4): la lista ocupa O(V + E) en vez de O(V²).
    /// </summary>
    public class Grafo
    {
        private readonly List<Arista>[] adyacencia;

        public int CantidadNodos
        {
            get { return adyacencia.Length; }
        }

        public Grafo(int cantidadNodos)
        {
            adyacencia = new List<Arista>[cantidadNodos];
            for (int i = 0; i < cantidadNodos; i++)
            {
                adyacencia[i] = new List<Arista>();
            }
        }

        /// <summary>
        /// Agrega un pasillo entre a y b. Como se puede caminar en los dos sentidos,
        /// se guarda la arista en la lista de a y en la de b.
        /// Si ya existía no se repite (así no importa si el JSON lo lista en un nodo o en los dos).
        /// </summary>
        public void AgregarArista(int a, int b, double peso)
        {
            ValidarNodo(a);
            ValidarNodo(b);
            if (peso < 0)
            {
                throw new ArgumentException("Dijkstra no admite distancias negativas.");
            }
            if (a == b || ExisteArista(a, b))
            {
                return;
            }
            adyacencia[a].Add(new Arista { Destino = b, Peso = peso });
            adyacencia[b].Add(new Arista { Destino = a, Peso = peso });
        }

        public bool ExisteArista(int a, int b)
        {
            foreach (Arista arista in adyacencia[a])
            {
                if (arista.Destino == b)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Arista> ObtenerVecinos(int nodo)
        {
            ValidarNodo(nodo);
            return adyacencia[nodo];
        }

        /// <summary>
        /// Dijkstra en su versión O(V²), sin cola de prioridad, que es la más fácil de seguir a mano.
        /// Idea: se van "fijando" nodos de uno en uno, siempre el no visitado más cercano al origen;
        /// cuando se fija un nodo, su distancia ya es la definitiva porque no hay pesos negativos.
        /// </summary>
        public ResultadoDijkstra Dijkstra(int origen)
        {
            ValidarNodo(origen);
            int n = adyacencia.Length;
            double[] distancias = new double[n];
            int[] anteriores = new int[n];
            bool[] visitado = new bool[n];

            // Paso 1: al comienzo todos los nodos están "infinitamente" lejos, menos el origen.
            for (int i = 0; i < n; i++)
            {
                distancias[i] = double.PositiveInfinity;
                anteriores[i] = -1;
                visitado[i] = false;
            }
            distancias[origen] = 0;

            // Paso 2: repetir hasta fijar todos los nodos alcanzables.
            for (int vuelta = 0; vuelta < n; vuelta++)
            {
                // 2a. Buscar el nodo NO visitado con menor distancia (recorrido lineal: O(V)).
                int u = -1;
                for (int i = 0; i < n; i++)
                {
                    if (!visitado[i] && (u == -1 || distancias[i] < distancias[u]))
                    {
                        u = i;
                    }
                }

                // Si el más cercano está en infinito, los que quedan no se pueden alcanzar.
                if (u == -1 || double.IsInfinity(distancias[u]))
                {
                    break;
                }
                visitado[u] = true;

                // 2b. "Relajar" las aristas de u: ¿llegar a cada vecino pasando por u es más corto?
                foreach (Arista arista in adyacencia[u])
                {
                    int v = arista.Destino;
                    double nuevaDistancia = distancias[u] + arista.Peso;
                    if (!visitado[v] && nuevaDistancia < distancias[v])
                    {
                        distancias[v] = nuevaDistancia;
                        anteriores[v] = u;
                    }
                }
            }

            ResultadoDijkstra resultado = new ResultadoDijkstra();
            resultado.Origen = origen;
            resultado.Distancias = distancias;
            resultado.Anteriores = anteriores;
            return resultado;
        }

        private void ValidarNodo(int nodo)
        {
            if (nodo < 0 || nodo >= adyacencia.Length)
            {
                throw new ArgumentException("El nodo " + nodo + " no existe en el grafo.");
            }
        }
    }
}
