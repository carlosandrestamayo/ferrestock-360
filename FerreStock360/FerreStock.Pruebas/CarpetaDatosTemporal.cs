using FerreStock.Datos;

namespace FerreStock.Pruebas
{
    /// <summary>
    /// Clase base para las pruebas de servicios: cambia RutasDatos.Carpeta a una carpeta
    /// temporal nueva (así cada prueba empieza con los datos iniciales) y al terminar
    /// la borra y deja la ruta como estaba.
    ///
    /// RutasDatos.Carpeta es compartida (static). Por eso todas las clases que heredan de esta
    /// deben llevar [Collection("RutasDatos")]: xUnit corre en paralelo las colecciones distintas,
    /// pero las pruebas de una misma colección una por una.
    /// </summary>
    [Collection("RutasDatos")]
    public abstract class CarpetaDatosTemporal : IDisposable
    {
        private readonly string carpetaOriginal;
        private readonly string carpetaTemporal;

        protected CarpetaDatosTemporal()
        {
            carpetaOriginal = RutasDatos.Carpeta;
            carpetaTemporal = Path.Combine(Path.GetTempPath(), "FerreStockPruebas_" + Guid.NewGuid().ToString("N"));
            RutasDatos.Carpeta = carpetaTemporal;
        }

        public void Dispose()
        {
            RutasDatos.Carpeta = carpetaOriginal;
            if (Directory.Exists(carpetaTemporal))
            {
                Directory.Delete(carpetaTemporal, true);
            }
        }
    }
}
