namespace FerreStock.Modelos
{
    /// <summary>
    /// Un espacio de un estante de la bodega.
    /// El Id sigue el formato Zona-Estante-Nivel, por ejemplo "A-01-2"
    /// es la zona A, estante 01, nivel 2.
    /// Las coordenadas están en metros y el origen (0, 0, 0) es el mostrador.
    /// </summary>
    public class Ubicacion
    {
        public string Id { get; set; } = "";
        public string Zona { get; set; } = "";

        // Posición en el plano de la bodega (metros desde el mostrador).
        public double X { get; set; }
        public double Y { get; set; }

        // Altura del nivel del estante (metros desde el piso).
        public double Z { get; set; }

        // Volumen máximo que cabe en este espacio.
        public double CapacidadM3 { get; set; }

        // Número del nodo del grafo de pasillos más cercano a esta ubicación.
        // Se usa para calcular la ruta de alistamiento.
        public int NodoGrafo { get; set; }
    }
}
