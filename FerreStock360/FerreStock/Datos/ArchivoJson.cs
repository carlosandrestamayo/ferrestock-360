using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FerreStock.Datos
{
    /// <summary>
    /// Lee y escribe listas de objetos en archivos JSON.
    /// Es la única clase del programa que toca los archivos de datos.
    /// </summary>
    public static class ArchivoJson
    {
        // Opciones compartidas por todas las lecturas y escrituras:
        // - WriteIndented: el archivo queda con saltos de línea y se puede leer a mano.
        // - UnsafeRelaxedJsonEscaping: guarda tildes y eñes tal cual (sin ñ).
        // - JsonStringEnumConverter: guarda los enumerados como texto ("Entrada")
        //   en vez de números (0), para que el archivo se entienda.
        private static readonly JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = { new JsonStringEnumConverter() }
        };

        /// <summary>
        /// Carga la lista guardada en <paramref name="ruta"/>.
        /// Si el archivo no existe, lo crea con <paramref name="datosIniciales"/>
        /// y devuelve esos mismos datos.
        /// </summary>
        public static List<T> Cargar<T>(string ruta, List<T> datosIniciales)
        {
            // Paso 1: si el archivo no existe es la primera vez que se usa el programa,
            // entonces se guarda la semilla para que la próxima vez ya esté.
            if (!File.Exists(ruta))
            {
                CrearCarpetaSiNoExiste(ruta);
                File.WriteAllText(ruta, JsonSerializer.Serialize(datosIniciales, opciones));
                return datosIniciales;
            }

            // Paso 2: leer el texto y convertirlo a objetos.
            string texto = File.ReadAllText(ruta);
            try
            {
                List<T>? datos = JsonSerializer.Deserialize<List<T>>(texto, opciones);

                // Un archivo con el texto "null" devuelve null: se trata como lista vacía.
                if (datos == null)
                {
                    return new List<T>();
                }
                return datos;
            }
            catch (JsonException ex)
            {
                // Se vuelve a lanzar con un mensaje que el usuario entienda,
                // indicando qué archivo está mal escrito.
                throw new InvalidDataException(
                    "El archivo " + Path.GetFileName(ruta) + " tiene un formato inválido. " +
                    "Revíselo o bórrelo para que se cree de nuevo. Detalle: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Guarda la lista completa en <paramref name="ruta"/>, reemplazando el contenido anterior.
        /// Es asíncrono para que la ventana no se congele mientras se escribe el archivo.
        ///
        /// Guardado seguro: si se escribiera directo sobre el archivo y el programa se cerrara a la mitad
        /// (o se fuera la luz), el JSON quedaría cortado y no se podría volver a abrir. Por eso:
        ///   1. se escribe todo en un archivo temporal ("productos.json.tmp"),
        ///   2. y solo cuando terminó bien, se reemplaza el archivo original de una sola vez (File.Move).
        /// Así el archivo original siempre tiene o los datos viejos completos o los nuevos completos.
        /// </summary>
        public static async Task GuardarAsync<T>(string ruta, List<T> datos)
        {
            CrearCarpetaSiNoExiste(ruta);
            string texto = JsonSerializer.Serialize(datos, opciones);
            string temporal = ruta + ".tmp";
            await File.WriteAllTextAsync(temporal, texto);
            // El último parámetro (true) permite reemplazar el archivo si ya existe.
            File.Move(temporal, ruta, true);
        }

        private static void CrearCarpetaSiNoExiste(string ruta)
        {
            string? carpeta = Path.GetDirectoryName(ruta);
            if (!string.IsNullOrEmpty(carpeta))
            {
                // CreateDirectory no hace nada si la carpeta ya existe.
                Directory.CreateDirectory(carpeta);
            }
        }
    }
}
