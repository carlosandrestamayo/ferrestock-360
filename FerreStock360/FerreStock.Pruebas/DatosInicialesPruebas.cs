using FerreStock.Datos;
using FerreStock.Modelos;

namespace FerreStock.Pruebas
{
    /// <summary>
    /// Revisa que la semilla sea coherente. Si un estudiante cambia los datos
    /// y se equivoca (código repetido, ubicación que no existe...), estas pruebas fallan.
    /// </summary>
    public class DatosInicialesPruebas
    {
        [Fact]
        public void Productos_TienenCodigosUnicos()
        {
            List<Producto> productos = DatosIniciales.CrearProductos();
            HashSet<string> vistos = new HashSet<string>();
            foreach (Producto p in productos)
            {
                Assert.True(vistos.Add(p.Codigo), "Código repetido: " + p.Codigo);
            }
            Assert.True(productos.Count >= 40);
        }

        [Fact]
        public void Productos_UsanSubcategoriasYUbicacionesQueExisten()
        {
            List<Categoria> categorias = DatosIniciales.CrearCategorias();
            List<Ubicacion> ubicaciones = DatosIniciales.CrearUbicaciones();

            foreach (Producto p in DatosIniciales.CrearProductos())
            {
                Categoria? categoria = categorias.Find(c => c.Id == p.CategoriaId);
                Assert.True(categoria != null, p.Codigo + " tiene una categoría que no existe");
                Assert.True(categoria!.PadreId != null, p.Codigo + " debe estar en una subcategoría");
                Assert.True(ubicaciones.Exists(u => u.Id == p.UbicacionId), p.Codigo + " tiene una ubicación que no existe");
            }
        }

        [Fact]
        public void Ubicaciones_NoSuperanSuCapacidad()
        {
            List<Producto> productos = DatosIniciales.CrearProductos();
            foreach (Ubicacion u in DatosIniciales.CrearUbicaciones())
            {
                double ocupado = 0;
                foreach (Producto p in productos)
                {
                    if (p.UbicacionId == u.Id)
                    {
                        ocupado += p.Stock * p.VolumenUnitarioM3;
                    }
                }
                Assert.True(ocupado <= u.CapacidadM3, "La ubicación " + u.Id + " está sobrecargada");
            }
        }
    }
}
