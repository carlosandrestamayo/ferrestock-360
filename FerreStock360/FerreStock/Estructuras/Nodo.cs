namespace FerreStock.Estructuras
{
    /// <summary>
    /// Eslabón de una lista enlazada: guarda un valor y la referencia al siguiente nodo.
    /// La T es el tipo del valor (por ejemplo, Movimiento), así el mismo nodo sirve para cualquier dato.
    /// </summary>
    public class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T>? Siguiente { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }
}
