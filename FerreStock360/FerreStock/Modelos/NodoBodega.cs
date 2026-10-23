namespace FerreStock.Modelos
{
    /// <summary>
    /// Un punto de los pasillos de la bodega (un cruce o la entrada a un estante).
    /// Los nodos y sus vecinos forman el grafo sobre el que se calcula la ruta de alistamiento.
    /// El nodo 0 es siempre el mostrador (origen de coordenadas).
    /// </summary>
    public class NodoBodega
    {
        // Debe ser 0, 1, 2... sin saltos, porque el grafo lo usa como posición en un arreglo.
        public int Id { get; set; }
        public string Nombre { get; set; } = "";

        // Posición en metros desde el mostrador.
        public double X { get; set; }
        public double Y { get; set; }

        // Ids de los nodos a los que se puede caminar en línea recta por un pasillo.
        // La distancia no se guarda: se calcula con las coordenadas (teorema de Pitágoras).
        public List<int> Vecinos { get; set; } = new List<int>();
    }
}
