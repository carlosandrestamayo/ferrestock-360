namespace FerreStock.Modelos
{
    /// <summary>
    /// Registro de un cambio en el stock de un producto.
    /// Guardar cada movimiento permite saber por qué cambió el inventario.
    /// </summary>
    public class Movimiento
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoProducto { get; set; } = "";
        public TipoMovimiento Tipo { get; set; }
        public int Cantidad { get; set; }
        public string Observacion { get; set; } = "";
    }
}
