using FerreStock.Estructuras;
using FerreStock.Modelos;

namespace FerreStock.Pruebas
{
    public class ColaPrioridadPruebas
    {
        private static readonly DateTime Base = new DateTime(2026, 10, 20, 8, 0, 0);

        private static Pedido CrearPedido(int id, PrioridadPedido prioridad, int minutos)
        {
            return new Pedido { Id = id, Prioridad = prioridad, Fecha = Base.AddMinutes(minutos), Cliente = "Cliente " + id };
        }

        [Fact]
        public void ColaNueva_EstaVacia()
        {
            ColaPrioridad cola = new ColaPrioridad();

            Assert.True(cola.EstaVacia());
            Assert.Throws<InvalidOperationException>(() => cola.ExtraerMinimo());
            Assert.Throws<InvalidOperationException>(() => cola.VerMinimo());
        }

        [Fact]
        public void Extraer_AtiendePrimeroElDeMayorPrioridad()
        {
            ColaPrioridad cola = new ColaPrioridad();
            cola.Insertar(CrearPedido(1, PrioridadPedido.Mostrador, 0));
            cola.Insertar(CrearPedido(2, PrioridadPedido.Mayorista, 10));
            cola.Insertar(CrearPedido(3, PrioridadPedido.Urgente, 20));

            Assert.Equal(3, cola.ExtraerMinimo().Id); // Urgente
            Assert.Equal(2, cola.ExtraerMinimo().Id); // Mayorista
            Assert.Equal(1, cola.ExtraerMinimo().Id); // Mostrador
            Assert.True(cola.EstaVacia());
        }

        [Fact]
        public void AIgualPrioridad_AtiendePrimeroElMasAntiguo()
        {
            ColaPrioridad cola = new ColaPrioridad();
            cola.Insertar(CrearPedido(1, PrioridadPedido.Mayorista, 30));
            cola.Insertar(CrearPedido(2, PrioridadPedido.Mayorista, 5));
            cola.Insertar(CrearPedido(3, PrioridadPedido.Mayorista, 15));

            Assert.Equal(2, cola.ExtraerMinimo().Id);
            Assert.Equal(3, cola.ExtraerMinimo().Id);
            Assert.Equal(1, cola.ExtraerMinimo().Id);
        }

        [Fact]
        public void MuchosPedidos_SalenSiempreEnOrden()
        {
            // 200 pedidos al azar: obliga a que el arreglo crezca varias veces.
            ColaPrioridad cola = new ColaPrioridad();
            Random azar = new Random(123);
            for (int i = 1; i <= 200; i++)
            {
                PrioridadPedido prioridad = (PrioridadPedido)azar.Next(1, 4);
                cola.Insertar(CrearPedido(i, prioridad, azar.Next(0, 500)));
            }

            Pedido anterior = cola.ExtraerMinimo();
            while (!cola.EstaVacia())
            {
                Pedido actual = cola.ExtraerMinimo();
                // El que salió antes nunca puede ir DESPUÉS del actual.
                Assert.False(ColaPrioridad.VaAntes(actual, anterior));
                anterior = actual;
            }
        }

        [Fact]
        public void ObtenerEnOrdenDeAtencion_NoModificaLaCola()
        {
            ColaPrioridad cola = new ColaPrioridad();
            cola.Insertar(CrearPedido(1, PrioridadPedido.Mostrador, 0));
            cola.Insertar(CrearPedido(2, PrioridadPedido.Urgente, 10));

            List<Pedido> orden = cola.ObtenerEnOrdenDeAtencion();

            Assert.Equal(2, orden[0].Id);
            Assert.Equal(1, orden[1].Id);
            Assert.Equal(2, cola.Cantidad);
            Assert.Equal(2, cola.VerMinimo().Id);
        }
    }
}
