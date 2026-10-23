namespace FerreStock.Modelos
{
    /// <summary>
    /// Categoría de productos. Forma un árbol de dos niveles:
    /// las categorías principales tienen PadreId = null y las
    /// subcategorías guardan el Id de su categoría principal.
    /// Ejemplo: Construcción (principal) → Cementos (subcategoría).
    /// </summary>
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public int? PadreId { get; set; }
    }
}
