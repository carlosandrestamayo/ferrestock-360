namespace FerreStock.Modelos
{
    /// <summary>
    /// Entrada: llega mercancía (suma al stock).
    /// Salida: se vende o despacha (resta al stock).
    /// Ajuste: corrige el stock después de un conteo físico.
    /// </summary>
    public enum TipoMovimiento
    {
        Entrada,
        Salida,
        Ajuste
    }
}
