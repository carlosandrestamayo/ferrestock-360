using FerreStock.Estructuras;

namespace FerreStock.Pruebas
{
    public class PilaPruebas
    {
        [Fact]
        public void PilaNueva_EstaVacia()
        {
            Pila<int> pila = new Pila<int>();

            Assert.True(pila.EstaVacia());
            Assert.Equal(0, pila.Cantidad);
        }

        [Fact]
        public void Desapilar_DevuelveEnOrdenInverso()
        {
            Pila<string> pila = new Pila<string>();
            pila.Apilar("primero");
            pila.Apilar("segundo");
            pila.Apilar("tercero");

            Assert.Equal(3, pila.Cantidad);
            Assert.Equal("tercero", pila.Desapilar());
            Assert.Equal("segundo", pila.Desapilar());
            Assert.Equal("primero", pila.Desapilar());
            Assert.True(pila.EstaVacia());
        }

        [Fact]
        public void VerTope_NoQuitaElElemento()
        {
            Pila<int> pila = new Pila<int>();
            pila.Apilar(10);
            pila.Apilar(20);

            Assert.Equal(20, pila.VerTope());
            Assert.Equal(2, pila.Cantidad);
        }

        [Fact]
        public void PilaVacia_DesapilarYVerTope_LanzanError()
        {
            Pila<int> pila = new Pila<int>();

            Assert.Throws<InvalidOperationException>(() => pila.Desapilar());
            Assert.Throws<InvalidOperationException>(() => pila.VerTope());
        }

        [Fact]
        public void SePuedeReutilizarDespuesDeVaciarla()
        {
            Pila<int> pila = new Pila<int>();
            pila.Apilar(1);
            pila.Desapilar();
            pila.Apilar(2);

            Assert.Equal(2, pila.VerTope());
            Assert.Equal(1, pila.Cantidad);
        }

        // TODO estudiante: agregar una prueba que apile 1.000 elementos y compruebe que salen
        // en el orden inverso y que Cantidad vuelve a 0.
    }
}
