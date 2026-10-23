namespace FerreStock.Modelos
{
    /// <summary>
    /// Un artículo que la ferretería compra, guarda en bodega y vende.
    /// El código es la "llave": no puede repetirse entre productos.
    /// </summary>
    public class Producto
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";

        // Id de la subcategoría a la que pertenece (ver Categoria).
        public int CategoriaId { get; set; }

        // Cómo se vende: bulto, varilla, tubo, unidad, kg, etc.
        public string Unidad { get; set; } = "unidad";

        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }

        // Cantidad disponible en la bodega, en la unidad del producto.
        public int Stock { get; set; }

        // Si el stock baja de este número hay que volver a pedir.
        public int StockMinimo { get; set; }

        // Peso y volumen de UNA unidad. Se usan en el costo de ubicación
        // (los productos pesados deben ir abajo) y en la ocupación de la bodega.
        public double PesoKg { get; set; }
        public double VolumenUnitarioM3 { get; set; }

        // Id de la ubicación en la bodega (ej. "A-01-2"). Vacío si aún no tiene.
        public string UbicacionId { get; set; } = "";

        // Unidades vendidas en el último mes. Sirve para la rotación,
        // la clasificación ABC y el costo de ubicación.
        public int VentasMes { get; set; }
    }
}
