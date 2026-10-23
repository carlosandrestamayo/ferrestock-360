namespace FerreStock.Estructuras
{
    /// <summary>
    /// Pila (LIFO: el último en entrar es el primero en salir) implementada con nodos enlazados.
    /// Se usa para "Deshacer": el último movimiento registrado es el primero que se revierte.
    ///
    /// Por qué enlazada: apilar y desapilar solo cambian la referencia del tope,
    /// así que ambas operaciones son O(1) y nunca hay que copiar ni agrandar un arreglo.
    /// </summary>
    public class Pila<T>
    {
        // Nodo de arriba de la pila. Es null cuando la pila está vacía.
        private Nodo<T>? tope;

        public int Cantidad { get; private set; }

        public bool EstaVacia()
        {
            return tope == null;
        }

        /// <summary>Pone un valor encima de la pila. O(1).</summary>
        public void Apilar(T valor)
        {
            // Paso 1: crear el nodo nuevo.
            Nodo<T> nuevo = new Nodo<T>(valor);
            // Paso 2: el nuevo nodo apunta al que antes era el tope.
            nuevo.Siguiente = tope;
            // Paso 3: el nuevo nodo pasa a ser el tope.
            tope = nuevo;
            Cantidad++;
        }

        /// <summary>Quita y devuelve el valor de arriba. O(1).</summary>
        public T Desapilar()
        {
            if (tope == null)
            {
                throw new InvalidOperationException("La pila está vacía.");
            }
            // Paso 1: guardar el valor del tope para devolverlo.
            T valor = tope.Valor;
            // Paso 2: el tope baja al siguiente nodo; el anterior queda sin referencias
            // y el recolector de basura de .NET lo libera.
            tope = tope.Siguiente;
            Cantidad--;
            return valor;
        }

        /// <summary>Devuelve el valor de arriba sin quitarlo. O(1).</summary>
        public T VerTope()
        {
            if (tope == null)
            {
                throw new InvalidOperationException("La pila está vacía.");
            }
            return tope.Valor;
        }
    }
}
