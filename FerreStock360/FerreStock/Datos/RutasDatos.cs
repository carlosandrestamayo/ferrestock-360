namespace FerreStock.Datos
{
    /// <summary>
    /// Reúne en un solo lugar las rutas de los archivos JSON.
    /// Así, si la carpeta cambia, solo se modifica aquí.
    /// </summary>
    public static class RutasDatos
    {
        // La carpeta "data" queda junto al .exe porque el proyecto la copia
        // a la carpeta de salida al compilar (ver FerreStock.csproj).
        // Tiene "set" para que las pruebas puedan usar una carpeta temporal.
        public static string Carpeta { get; set; } = Path.Combine(AppContext.BaseDirectory, "data");

        public static string Productos => Path.Combine(Carpeta, "productos.json");
        public static string Categorias => Path.Combine(Carpeta, "categorias.json");
        public static string Ubicaciones => Path.Combine(Carpeta, "ubicaciones.json");
        public static string Movimientos => Path.Combine(Carpeta, "movimientos.json");
        public static string Pedidos => Path.Combine(Carpeta, "pedidos.json");
    }
}
