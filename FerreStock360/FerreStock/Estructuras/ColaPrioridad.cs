using FerreStock.Modelos;

namespace FerreStock.Estructuras
{
    /// <summary>
    /// Cola de prioridad de pedidos implementada como un heap binario mínimo sobre un arreglo.
    /// Siempre deja "arriba" (posición 0) el pedido que se debe atender primero:
    /// el de menor número de prioridad (Urgente = 1) y, si empatan, el más antiguo.
    ///
    /// El arreglo representa un árbol binario completo:
    ///   - los hijos del elemento i están en 2i + 1 y 2i + 2
    ///   - el padre del elemento i está en (i - 1) / 2
    /// Como el árbol siempre está balanceado, su altura es log₂(n), y por eso
    /// insertar y extraer cuestan O(log n) en vez de O(n) de una lista ordenada.
    /// </summary>
    public class ColaPrioridad
    {
        private Pedido[] elementos;

        public int Cantidad { get; private set; }

        public ColaPrioridad()
        {
            elementos = new Pedido[8];
            Cantidad = 0;
        }

        public bool EstaVacia()
        {
            return Cantidad == 0;
        }

        /// <summary>
        /// Regla de prioridad: ¿el pedido a debe atenderse antes que b?
        /// TODO estudiante: si la ferretería usa otro criterio de desempate (por ejemplo,
        /// el pedido con menos ítems primero), este es el único método que hay que cambiar.
        /// </summary>
        public static bool VaAntes(Pedido a, Pedido b)
        {
            if (a.Prioridad != b.Prioridad)
            {
                return a.Prioridad < b.Prioridad;
            }
            if (a.Fecha != b.Fecha)
            {
                return a.Fecha < b.Fecha;
            }
            // Mismo momento: el de Id menor se creó primero.
            return a.Id < b.Id;
        }

        /// <summary>Agrega un pedido. O(log n).</summary>
        public void Insertar(Pedido pedido)
        {
            // Paso 1: si el arreglo está lleno se duplica su tamaño (pasa pocas veces).
            if (Cantidad == elementos.Length)
            {
                Pedido[] masGrande = new Pedido[elementos.Length * 2];
                for (int i = 0; i < Cantidad; i++)
                {
                    masGrande[i] = elementos[i];
                }
                elementos = masGrande;
            }

            // Paso 2: poner el pedido al final del arreglo (última hoja del árbol).
            elementos[Cantidad] = pedido;
            Cantidad++;

            // Paso 3: "subirlo" mientras vaya antes que su padre.
            Subir(Cantidad - 1);
        }

        /// <summary>Devuelve el pedido que se atiende primero, sin quitarlo. O(1).</summary>
        public Pedido VerMinimo()
        {
            if (Cantidad == 0)
            {
                throw new InvalidOperationException("La cola de pedidos está vacía.");
            }
            return elementos[0];
        }

        /// <summary>Quita y devuelve el pedido que se atiende primero. O(log n).</summary>
        public Pedido ExtraerMinimo()
        {
            if (Cantidad == 0)
            {
                throw new InvalidOperationException("La cola de pedidos está vacía.");
            }

            // Paso 1: el mínimo siempre está en la raíz.
            Pedido minimo = elementos[0];

            // Paso 2: el último elemento pasa a la raíz y se acorta el arreglo.
            Cantidad--;
            elementos[0] = elementos[Cantidad];
            elementos[Cantidad] = null!; // se suelta la referencia a la posición que quedó libre

            // Paso 3: "bajar" la nueva raíz hasta que quede en su lugar.
            if (Cantidad > 0)
            {
                Bajar(0);
            }
            return minimo;
        }

        /// <summary>
        /// Devuelve los pedidos en el orden en que se atenderían, sin modificar la cola.
        /// Se copian en otra cola y se extraen de ella uno por uno: O(n log n).
        /// </summary>
        public List<Pedido> ObtenerEnOrdenDeAtencion()
        {
            ColaPrioridad copia = new ColaPrioridad();
            for (int i = 0; i < Cantidad; i++)
            {
                copia.Insertar(elementos[i]);
            }

            List<Pedido> resultado = new List<Pedido>();
            while (!copia.EstaVacia())
            {
                resultado.Add(copia.ExtraerMinimo());
            }
            return resultado;
        }

        // Sube el elemento de la posición i intercambiándolo con su padre
        // mientras deba atenderse antes que él.
        private void Subir(int i)
        {
            while (i > 0)
            {
                int padre = (i - 1) / 2;
                if (VaAntes(elementos[i], elementos[padre]))
                {
                    Intercambiar(i, padre);
                    i = padre;
                }
                else
                {
                    // Ya está en su lugar: el padre va antes que él.
                    break;
                }
            }
        }

        // Baja el elemento de la posición i intercambiándolo con el MEJOR de sus dos hijos
        // mientras alguno de ellos deba atenderse antes.
        private void Bajar(int i)
        {
            while (true)
            {
                int izquierdo = 2 * i + 1;
                int derecho = 2 * i + 2;
                int mejor = i;

                if (izquierdo < Cantidad && VaAntes(elementos[izquierdo], elementos[mejor]))
                {
                    mejor = izquierdo;
                }
                if (derecho < Cantidad && VaAntes(elementos[derecho], elementos[mejor]))
                {
                    mejor = derecho;
                }

                if (mejor == i)
                {
                    // Ningún hijo va antes: el heap quedó ordenado.
                    break;
                }
                Intercambiar(i, mejor);
                i = mejor;
            }
        }

        private void Intercambiar(int a, int b)
        {
            Pedido temporal = elementos[a];
            elementos[a] = elementos[b];
            elementos[b] = temporal;
        }
    }
}
