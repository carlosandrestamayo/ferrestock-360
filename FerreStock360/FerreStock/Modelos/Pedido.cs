using System.Text.Json.Serialization;

namespace FerreStock.Modelos
{
    /// <summary>
    /// Pedido de un cliente. Se atiende según su prioridad y,
    /// a igual prioridad, el más antiguo primero (por Fecha).
    /// </summary>
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = "";
        public PrioridadPedido Prioridad { get; set; } = PrioridadPedido.Mostrador;
        public List<ItemPedido> Items { get; set; } = new List<ItemPedido>();
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;

        // Texto para listas desplegables, por ejemplo "Pedido 3 - Obra Calle 10 (Urgente)".
        [JsonIgnore]
        public string Descripcion
        {
            get { return "Pedido " + Id + " - " + Cliente + " (" + Prioridad + ")"; }
        }
    }
}
