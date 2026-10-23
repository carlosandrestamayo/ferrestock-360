using FerreStock.Datos;
using FerreStock.Modelos;

namespace FerreStock.Servicios
{
    /// <summary>
    /// Lógica del inventario: cargar, validar, crear, editar, eliminar y buscar productos.
    /// Los formularios no tocan las listas ni los archivos directamente: todo pasa por aquí.
    /// Cuando un dato no es válido se lanza ArgumentException con un mensaje para el usuario.
    /// </summary>
    public class InventarioServicio
    {
        // Unidades de venta permitidas. El formulario las muestra en una lista desplegable.
        public static readonly string[] Unidades =
        {
            "unidad", "bulto", "varilla", "tubo", "kg", "galón", "caja", "rollo", "metro", "m³"
        };

        // La lista guarda el orden en que se muestran y se guardan los productos.
        private readonly List<Producto> productos;

        // El diccionario permite encontrar un producto por su código en O(1) promedio,
        // sin recorrer toda la lista. Siempre debe tener los mismos productos que la lista.
        private readonly Dictionary<string, Producto> productosPorCodigo;

        private readonly List<Categoria> categorias;
        private readonly List<Ubicacion> ubicaciones;

        public InventarioServicio()
        {
            categorias = ArchivoJson.Cargar(RutasDatos.Categorias, DatosIniciales.CrearCategorias());
            ubicaciones = ArchivoJson.Cargar(RutasDatos.Ubicaciones, DatosIniciales.CrearUbicaciones());
            productos = ArchivoJson.Cargar(RutasDatos.Productos, DatosIniciales.CrearProductos());

            // Se llena el diccionario a partir de la lista cargada.
            // Si alguien editó el JSON a mano y repitió un código, se avisa en vez de seguir con datos dañados.
            productosPorCodigo = new Dictionary<string, Producto>();
            foreach (Producto p in productos)
            {
                string codigo = NormalizarCodigo(p.Codigo);
                if (productosPorCodigo.ContainsKey(codigo))
                {
                    throw new InvalidDataException("El archivo productos.json tiene el código repetido " + codigo + ".");
                }
                p.Codigo = codigo;
                productosPorCodigo.Add(codigo, p);
            }
        }

        // ---------------------------------------------------------------
        // Consultas
        // ---------------------------------------------------------------

        public List<Producto> ObtenerProductos()
        {
            return productos;
        }

        public List<Categoria> ObtenerCategorias()
        {
            return categorias;
        }

        public List<Ubicacion> ObtenerUbicaciones()
        {
            return ubicaciones;
        }

        /// <summary>Categorías del primer nivel del árbol (sin padre).</summary>
        public List<Categoria> ObtenerCategoriasPrincipales()
        {
            List<Categoria> resultado = new List<Categoria>();
            foreach (Categoria c in categorias)
            {
                if (c.PadreId == null)
                {
                    resultado.Add(c);
                }
            }
            return resultado;
        }

        /// <summary>Subcategorías (segundo nivel) que cuelgan de la categoría indicada.</summary>
        public List<Categoria> ObtenerSubcategorias(int padreId)
        {
            List<Categoria> resultado = new List<Categoria>();
            foreach (Categoria c in categorias)
            {
                if (c.PadreId == padreId)
                {
                    resultado.Add(c);
                }
            }
            return resultado;
        }

        /// <summary>Todas las subcategorías: son las únicas a las que se puede asignar un producto.</summary>
        public List<Categoria> ObtenerTodasLasSubcategorias()
        {
            List<Categoria> resultado = new List<Categoria>();
            foreach (Categoria c in categorias)
            {
                if (c.PadreId != null)
                {
                    resultado.Add(c);
                }
            }
            return resultado;
        }

        public Categoria? BuscarCategoria(int id)
        {
            foreach (Categoria c in categorias)
            {
                if (c.Id == id)
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>Devuelve el nombre de la categoría o "(sin categoría)" si el Id no existe.</summary>
        public string ObtenerNombreCategoria(int id)
        {
            Categoria? c = BuscarCategoria(id);
            if (c == null)
            {
                return "(sin categoría)";
            }
            return c.Nombre;
        }

        public bool ExisteUbicacion(string id)
        {
            foreach (Ubicacion u in ubicaciones)
            {
                if (u.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Busca un producto por código usando el diccionario. Devuelve null si no existe.</summary>
        public Producto? BuscarPorCodigo(string codigo)
        {
            string clave = NormalizarCodigo(codigo);
            if (productosPorCodigo.TryGetValue(clave, out Producto? producto))
            {
                return producto;
            }
            return null;
        }

        /// <summary>
        /// Filtra los productos cuyo código o nombre contiene el texto y que pertenecen a la categoría.
        /// Si texto está vacío no filtra por texto; si categoriaId es null no filtra por categoría.
        /// </summary>
        public List<Producto> Filtrar(string texto, int? categoriaId)
        {
            string buscado = texto.Trim();
            List<Producto> resultado = new List<Producto>();

            foreach (Producto p in productos)
            {
                // Paso 1: ¿coincide el texto? Se compara sin importar mayúsculas o minúsculas.
                bool coincideTexto = buscado == ""
                    || p.Codigo.Contains(buscado, StringComparison.OrdinalIgnoreCase)
                    || p.Nombre.Contains(buscado, StringComparison.OrdinalIgnoreCase);

                // Paso 2: ¿pertenece a la categoría elegida?
                bool coincideCategoria = categoriaId == null || PerteneceACategoria(p, categoriaId.Value);

                if (coincideTexto && coincideCategoria)
                {
                    resultado.Add(p);
                }
            }
            return resultado;
        }

        /// <summary>
        /// Un producto pertenece a una categoría si está en ella directamente (subcategoría)
        /// o si su subcategoría es hija de ella (categoría principal). Así, al elegir
        /// "Construcción" aparecen los cementos, los agregados y el hierro.
        /// </summary>
        public bool PerteneceACategoria(Producto producto, int categoriaId)
        {
            if (producto.CategoriaId == categoriaId)
            {
                return true;
            }
            Categoria? subcategoria = BuscarCategoria(producto.CategoriaId);
            return subcategoria != null && subcategoria.PadreId == categoriaId;
        }

        /// <summary>Productos cuyo stock está por debajo del mínimo (hay que volver a pedirlos).</summary>
        public List<Producto> ObtenerBajoStockMinimo()
        {
            List<Producto> resultado = new List<Producto>();
            foreach (Producto p in productos)
            {
                if (p.Stock < p.StockMinimo)
                {
                    resultado.Add(p);
                }
            }
            return resultado;
        }

        /// <summary>Volumen total que ocupa la mercancía en bodega: Σ stock × volumen unitario (m³).</summary>
        public double VolumenOcupado()
        {
            double total = 0;
            foreach (Producto p in productos)
            {
                total += p.Stock * p.VolumenUnitarioM3;
            }
            return total;
        }

        // ---------------------------------------------------------------
        // Crear, editar y eliminar
        // ---------------------------------------------------------------

        /// <summary>Agrega un producto nuevo y guarda el archivo.</summary>
        public async Task AgregarAsync(Producto nuevo)
        {
            nuevo.Codigo = NormalizarCodigo(nuevo.Codigo);
            nuevo.Nombre = nuevo.Nombre.Trim();
            Validar(nuevo);

            if (productosPorCodigo.ContainsKey(nuevo.Codigo))
            {
                throw new ArgumentException("Ya existe un producto con el código " + nuevo.Codigo + ".");
            }

            // Se agrega en las dos estructuras para que sigan sincronizadas.
            productos.Add(nuevo);
            productosPorCodigo.Add(nuevo.Codigo, nuevo);
            await GuardarAsync();
        }

        /// <summary>
        /// Reemplaza los datos del producto que tiene el mismo código.
        /// El código no se puede cambiar porque es la llave del producto.
        /// El STOCK tampoco se cambia aquí: solo cambia con entradas, salidas o ajustes
        /// (MovimientoServicio), para que cada cambio quede registrado en el historial.
        /// </summary>
        public async Task EditarAsync(Producto cambios)
        {
            cambios.Codigo = NormalizarCodigo(cambios.Codigo);
            cambios.Nombre = cambios.Nombre.Trim();

            Producto? existente = BuscarPorCodigo(cambios.Codigo);
            if (existente == null)
            {
                throw new ArgumentException("No existe un producto con el código " + cambios.Codigo + ".");
            }

            // Se valida ANTES de copiar, para no dejar el producto a medio cambiar si hay un error.
            // El stock que se valida es el actual, porque es el que se va a conservar.
            cambios.Stock = existente.Stock;
            Validar(cambios);

            existente.Nombre = cambios.Nombre;
            existente.CategoriaId = cambios.CategoriaId;
            existente.Unidad = cambios.Unidad;
            existente.PrecioCompra = cambios.PrecioCompra;
            existente.PrecioVenta = cambios.PrecioVenta;
            existente.StockMinimo = cambios.StockMinimo;
            existente.PesoKg = cambios.PesoKg;
            existente.VolumenUnitarioM3 = cambios.VolumenUnitarioM3;
            existente.UbicacionId = cambios.UbicacionId;
            existente.VentasMes = cambios.VentasMes;
            await GuardarAsync();
        }

        /// <summary>Elimina el producto con ese código y guarda el archivo.</summary>
        public async Task EliminarAsync(string codigo)
        {
            Producto? producto = BuscarPorCodigo(codigo);
            if (producto == null)
            {
                throw new ArgumentException("No existe un producto con el código " + codigo + ".");
            }

            // Ojo: antes de llamar este método, el formulario pide a PedidoServicio que verifique
            // que el producto no esté en un pedido sin despachar (ValidarEliminacionProducto).

            productos.Remove(producto);
            productosPorCodigo.Remove(producto.Codigo);
            await GuardarAsync();
        }

        /// <summary>
        /// Revisa que el producto tenga datos válidos. Si algo falla lanza ArgumentException
        /// con un mensaje que el formulario muestra tal cual al usuario.
        /// </summary>
        public void Validar(Producto p)
        {
            if (string.IsNullOrWhiteSpace(p.Codigo))
            {
                throw new ArgumentException("El código es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(p.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(p.Unidad))
            {
                throw new ArgumentException("La unidad es obligatoria.");
            }

            // Solo se permiten subcategorías, para que el árbol de categorías tenga sentido.
            Categoria? categoria = BuscarCategoria(p.CategoriaId);
            if (categoria == null || categoria.PadreId == null)
            {
                throw new ArgumentException("Debe elegir una subcategoría válida.");
            }

            if (p.PrecioCompra < 0 || p.PrecioVenta < 0)
            {
                throw new ArgumentException("Los precios no pueden ser negativos.");
            }
            // TODO estudiante: validar que el precio de venta no sea menor que el precio de compra.

            if (p.Stock < 0)
            {
                throw new ArgumentException("El stock no puede ser negativo.");
            }
            if (p.StockMinimo < 0)
            {
                throw new ArgumentException("El stock mínimo no puede ser negativo.");
            }
            if (p.VentasMes < 0)
            {
                throw new ArgumentException("Las ventas del mes no pueden ser negativas.");
            }
            if (p.PesoKg < 0)
            {
                throw new ArgumentException("El peso no puede ser negativo.");
            }
            // El volumen debe ser mayor que cero porque se usa para calcular la ocupación de la bodega.
            if (p.VolumenUnitarioM3 <= 0)
            {
                throw new ArgumentException("El volumen unitario debe ser mayor que cero.");
            }

            // La ubicación puede quedar vacía (producto aún sin ubicar), pero si se escribe debe existir.
            if (p.UbicacionId != "" && !ExisteUbicacion(p.UbicacionId))
            {
                throw new ArgumentException("La ubicación " + p.UbicacionId + " no existe en la bodega.");
            }
        }

        public async Task GuardarAsync()
        {
            await ArchivoJson.GuardarAsync(RutasDatos.Productos, productos);
        }

        // Los códigos se guardan sin espacios a los lados y en mayúsculas,
        // así "con-001 " y "CON-001" se consideran el mismo código.
        private static string NormalizarCodigo(string codigo)
        {
            return codigo.Trim().ToUpper();
        }
    }
}
