namespace FerreStock.Modelos
{
    /// <summary>
    /// Una línea de un pedido: qué producto y cuántas unidades.
    /// </summary>
    public class ItemPedido
    {
        public string CodigoProducto { get; set; } = "";
        public int Cantidad { get; set; }
    }
}
