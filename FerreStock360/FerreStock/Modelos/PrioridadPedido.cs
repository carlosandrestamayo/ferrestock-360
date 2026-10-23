namespace FerreStock.Modelos
{
    /// <summary>
    /// Prioridad de atención de un pedido. Un número MENOR significa
    /// que se atiende ANTES; por eso la cola de prioridad es un heap mínimo.
    /// </summary>
    public enum PrioridadPedido
    {
        Urgente = 1,
        Mayorista = 2,
        Mostrador = 3
    }
}
