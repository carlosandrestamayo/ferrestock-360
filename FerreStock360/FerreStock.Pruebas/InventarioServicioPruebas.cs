using FerreStock.Datos;
using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Pruebas
{
    /// <summary>
    /// Cada prueba empieza con los datos iniciales en una carpeta temporal (ver CarpetaDatosTemporal).
    /// </summary>
    [Collection("RutasDatos")]
    public class InventarioServicioPruebas : CarpetaDatosTemporal
    {
        private static Producto CrearProductoValido(string codigo)
        {
            return new Producto
            {
                Codigo = codigo,
                Nombre = "Producto de prueba",
                CategoriaId = 11,
                Unidad = "bulto",
                PrecioCompra = 1000,
                PrecioVenta = 1500,
                Stock = 10,
                StockMinimo = 2,
                PesoKg = 1,
                VolumenUnitarioM3 = 0.01,
                UbicacionId = "A-01-1"
            };
        }

        [Fact]
        public void SinArchivos_CargaLosDatosIniciales()
        {
            InventarioServicio servicio = new InventarioServicio();

            Assert.Equal(DatosIniciales.CrearProductos().Count, servicio.ObtenerProductos().Count);
            Assert.NotNull(servicio.BuscarPorCodigo("CON-001"));
            Assert.True(File.Exists(RutasDatos.Productos));
        }

        [Fact]
        public async Task Agregar_GuardaElProductoEnElArchivo()
        {
            InventarioServicio servicio = new InventarioServicio();
            await servicio.AgregarAsync(CrearProductoValido(" nue-001 "));

            // Un servicio nuevo vuelve a leer el archivo: el producto debe seguir ahí.
            InventarioServicio otro = new InventarioServicio();
            Producto? encontrado = otro.BuscarPorCodigo("NUE-001");
            Assert.NotNull(encontrado);
            Assert.Equal("NUE-001", encontrado!.Codigo);
        }

        [Fact]
        public async Task Agregar_CodigoRepetido_LanzaError()
        {
            InventarioServicio servicio = new InventarioServicio();
            await Assert.ThrowsAsync<ArgumentException>(() => servicio.AgregarAsync(CrearProductoValido("con-001")));
        }

        [Fact]
        public async Task Agregar_StockNegativo_LanzaError()
        {
            InventarioServicio servicio = new InventarioServicio();
            Producto p = CrearProductoValido("NUE-002");
            p.Stock = -5;
            await Assert.ThrowsAsync<ArgumentException>(() => servicio.AgregarAsync(p));
            Assert.Null(servicio.BuscarPorCodigo("NUE-002"));
        }

        [Fact]
        public void Validar_DetectaDatosInvalidos()
        {
            InventarioServicio servicio = new InventarioServicio();

            Producto sinNombre = CrearProductoValido("X-1");
            sinNombre.Nombre = "  ";
            Assert.Throws<ArgumentException>(() => servicio.Validar(sinNombre));

            Producto categoriaPrincipal = CrearProductoValido("X-2");
            categoriaPrincipal.CategoriaId = 1; // "Construcción" no es subcategoría
            Assert.Throws<ArgumentException>(() => servicio.Validar(categoriaPrincipal));

            Producto ubicacionInexistente = CrearProductoValido("X-3");
            ubicacionInexistente.UbicacionId = "Z-99-9";
            Assert.Throws<ArgumentException>(() => servicio.Validar(ubicacionInexistente));

            Producto precioNegativo = CrearProductoValido("X-4");
            precioNegativo.PrecioVenta = -1;
            Assert.Throws<ArgumentException>(() => servicio.Validar(precioNegativo));
        }

        [Fact]
        public async Task Editar_CambiaLosDatosDelProducto()
        {
            InventarioServicio servicio = new InventarioServicio();
            Producto cambios = CrearProductoValido("CON-001");
            cambios.Nombre = "Cemento gris 50 kg (editado)";
            cambios.Stock = 99;

            await servicio.EditarAsync(cambios);

            Producto? editado = new InventarioServicio().BuscarPorCodigo("CON-001");
            Assert.Equal("Cemento gris 50 kg (editado)", editado!.Nombre);
            Assert.Equal(99, editado.Stock);
        }

        [Fact]
        public async Task Eliminar_QuitaElProducto()
        {
            InventarioServicio servicio = new InventarioServicio();
            int antes = servicio.ObtenerProductos().Count;

            await servicio.EliminarAsync("PLO-001");

            Assert.Null(servicio.BuscarPorCodigo("PLO-001"));
            Assert.Equal(antes - 1, servicio.ObtenerProductos().Count);
        }

        [Fact]
        public void Filtrar_PorCategoriaPrincipal_IncluyeSusSubcategorias()
        {
            InventarioServicio servicio = new InventarioServicio();

            // Plomería (2) tiene Tubería PVC (21) y Accesorios PVC (22).
            List<Producto> plomeria = servicio.Filtrar("", 2);

            Assert.Equal(8, plomeria.Count);
            foreach (Producto p in plomeria)
            {
                Assert.StartsWith("PLO-", p.Codigo);
            }
        }

        [Fact]
        public void Filtrar_PorTexto_NoDistingueMayusculas()
        {
            InventarioServicio servicio = new InventarioServicio();

            List<Producto> resultado = servicio.Filtrar("CEMENTO", null);

            Assert.Equal(2, resultado.Count);
        }

        [Fact]
        public void BajoStockMinimo_DevuelveLosQueHayQuePedir()
        {
            InventarioServicio servicio = new InventarioServicio();

            List<Producto> bajos = servicio.ObtenerBajoStockMinimo();

            Assert.Equal(3, bajos.Count);
        }
    }
}
