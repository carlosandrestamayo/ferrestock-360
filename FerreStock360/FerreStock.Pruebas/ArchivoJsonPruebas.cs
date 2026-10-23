using FerreStock.Datos;
using FerreStock.Modelos;

namespace FerreStock.Pruebas
{
    /// <summary>
    /// Cada prueba trabaja en una carpeta temporal propia para no tocar
    /// los datos reales del programa. La carpeta se borra al final (Dispose).
    /// </summary>
    public class ArchivoJsonPruebas : IDisposable
    {
        private readonly string carpeta;

        public ArchivoJsonPruebas()
        {
            carpeta = Path.Combine(Path.GetTempPath(), "FerreStockPruebas_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(carpeta);
        }

        public void Dispose()
        {
            Directory.Delete(carpeta, true);
        }

        [Fact]
        public async Task GuardarYCargar_DevuelveLosMismosDatos()
        {
            string ruta = Path.Combine(carpeta, "productos.json");
            List<Producto> originales = new List<Producto>();
            originales.Add(new Producto { Codigo = "CON-001", Nombre = "Cemento gris 50 kg", Stock = 80, PrecioVenta = 33500m, PesoKg = 50 });
            originales.Add(new Producto { Codigo = "PLO-001", Nombre = "Tubo PVC 1/2\"", Stock = 60, VolumenUnitarioM3 = 0.0027 });

            await ArchivoJson.GuardarAsync(ruta, originales);
            List<Producto> cargados = ArchivoJson.Cargar(ruta, new List<Producto>());

            Assert.Equal(2, cargados.Count);
            Assert.Equal("CON-001", cargados[0].Codigo);
            Assert.Equal("Cemento gris 50 kg", cargados[0].Nombre);
            Assert.Equal(80, cargados[0].Stock);
            Assert.Equal(33500m, cargados[0].PrecioVenta);
            Assert.Equal("Tubo PVC 1/2\"", cargados[1].Nombre);
            Assert.Equal(0.0027, cargados[1].VolumenUnitarioM3);
        }

        [Fact]
        public async Task GuardarYCargar_ConservaEnumeradosYListasInternas()
        {
            string ruta = Path.Combine(carpeta, "pedidos.json");
            Pedido pedido = new Pedido { Id = 7, Cliente = "Obra", Prioridad = PrioridadPedido.Urgente, Estado = EstadoPedido.Alistado };
            pedido.Items.Add(new ItemPedido { CodigoProducto = "CON-001", Cantidad = 5 });

            await ArchivoJson.GuardarAsync(ruta, new List<Pedido> { pedido });
            List<Pedido> cargados = ArchivoJson.Cargar(ruta, new List<Pedido>());

            Assert.Equal(PrioridadPedido.Urgente, cargados[0].Prioridad);
            Assert.Equal(EstadoPedido.Alistado, cargados[0].Estado);
            Assert.Single(cargados[0].Items);
            Assert.Equal(5, cargados[0].Items[0].Cantidad);
            // Los enumerados se guardan como texto para que el archivo sea legible.
            Assert.Contains("\"Urgente\"", File.ReadAllText(ruta));
        }

        [Fact]
        public void Cargar_SiNoExisteElArchivo_LoCreaConLosDatosIniciales()
        {
            string ruta = Path.Combine(carpeta, "sub", "categorias.json");
            List<Categoria> iniciales = new List<Categoria> { new Categoria { Id = 1, Nombre = "Construcción" } };

            List<Categoria> cargadas = ArchivoJson.Cargar(ruta, iniciales);

            Assert.True(File.Exists(ruta));
            Assert.Single(cargadas);
            // Al volver a cargar se lee lo que quedó escrito en el archivo.
            List<Categoria> otraVez = ArchivoJson.Cargar(ruta, new List<Categoria>());
            Assert.Equal("Construcción", otraVez[0].Nombre);
        }

        [Fact]
        public async Task GuardarDosVeces_QuedaLoUltimoYNoQuedanTemporales()
        {
            string ruta = Path.Combine(carpeta, "categorias.json");

            await ArchivoJson.GuardarAsync(ruta, new List<Categoria> { new Categoria { Id = 1, Nombre = "Primera" } });
            await ArchivoJson.GuardarAsync(ruta, new List<Categoria> { new Categoria { Id = 2, Nombre = "Segunda" } });

            List<Categoria> cargadas = ArchivoJson.Cargar(ruta, new List<Categoria>());
            Assert.Single(cargadas);
            Assert.Equal("Segunda", cargadas[0].Nombre);
            Assert.False(File.Exists(ruta + ".tmp"));
        }

        [Fact]
        public void Cargar_ArchivoDaniado_LanzaErrorConMensajeClaro()
        {
            string ruta = Path.Combine(carpeta, "productos.json");
            File.WriteAllText(ruta, "{ esto no es json");

            InvalidDataException error = Assert.Throws<InvalidDataException>(
                () => ArchivoJson.Cargar(ruta, new List<Producto>()));
            Assert.Contains("productos.json", error.Message);
        }
    }
}
