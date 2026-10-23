using FerreStock.Algoritmos;
using FerreStock.Datos;
using FerreStock.Modelos;

namespace FerreStock.Pruebas
{
    public class OrdenamientosPruebas
    {
        private static List<Producto> CrearLista(params string[] codigos)
        {
            List<Producto> lista = new List<Producto>();
            foreach (string codigo in codigos)
            {
                lista.Add(new Producto { Codigo = codigo });
            }
            return lista;
        }

        private static List<string> Codigos(List<Producto> lista)
        {
            List<string> codigos = new List<string>();
            foreach (Producto p in lista)
            {
                codigos.Add(p.Codigo);
            }
            return codigos;
        }

        [Fact]
        public void Burbuja_OrdenaPorCodigo()
        {
            List<Producto> lista = CrearLista("PLO-002", "CON-001", "TOR-005", "ELE-003", "CON-002");

            Ordenamientos.Burbuja(lista);

            Assert.Equal(new List<string> { "CON-001", "CON-002", "ELE-003", "PLO-002", "TOR-005" }, Codigos(lista));
        }

        [Fact]
        public void Insercion_OrdenaPorCodigo()
        {
            List<Producto> lista = CrearLista("PLO-002", "CON-001", "TOR-005", "ELE-003", "CON-002");

            Ordenamientos.Insercion(lista);

            Assert.Equal(new List<string> { "CON-001", "CON-002", "ELE-003", "PLO-002", "TOR-005" }, Codigos(lista));
        }

        [Fact]
        public void ListasVaciasDeUnoYYaOrdenadas_NoFallan()
        {
            List<Producto> vacia = CrearLista();
            List<Producto> uno = CrearLista("A");
            List<Producto> ordenada = CrearLista("A", "B", "C");

            Ordenamientos.Burbuja(vacia);
            Ordenamientos.Insercion(uno);
            Ordenamientos.Burbuja(ordenada);

            Assert.Empty(vacia);
            Assert.Single(uno);
            Assert.True(Ordenamientos.EstaOrdenada(ordenada));
        }

        [Fact]
        public void ConDatosAlAzar_AmbosDanElMismoResultadoQueListSort()
        {
            Random azar = new Random(7);
            List<Producto> original = new List<Producto>();
            for (int i = 0; i < 500; i++)
            {
                original.Add(new Producto { Codigo = "P-" + azar.Next(0, 100000).ToString("D5") });
            }

            List<Producto> burbuja = new List<Producto>(original);
            List<Producto> insercion = new List<Producto>(original);
            List<Producto> referencia = new List<Producto>(original);
            Ordenamientos.Burbuja(burbuja);
            Ordenamientos.Insercion(insercion);
            referencia.Sort(Ordenamientos.CompararPorCodigo);

            Assert.Equal(Codigos(referencia), Codigos(burbuja));
            Assert.Equal(Codigos(referencia), Codigos(insercion));
        }
    }

    public class BusquedasPruebas
    {
        private static List<Producto> ProductosOrdenados()
        {
            List<Producto> lista = DatosIniciales.CrearProductos();
            Ordenamientos.Insercion(lista);
            return lista;
        }

        [Fact]
        public void Binaria_EncuentraCadaProducto()
        {
            List<Producto> lista = ProductosOrdenados();

            for (int i = 0; i < lista.Count; i++)
            {
                ResultadoBusqueda resultado = Busquedas.Binaria(lista, lista[i].Codigo);
                Assert.Equal(i, resultado.Indice);
            }
        }

        [Fact]
        public void Binaria_CodigoInexistente_DevuelveMenosUno()
        {
            List<Producto> lista = ProductosOrdenados();

            Assert.Equal(-1, Busquedas.Binaria(lista, "AAA-000").Indice);   // menor que todos
            Assert.Equal(-1, Busquedas.Binaria(lista, "ZZZ-999").Indice);   // mayor que todos
            Assert.Equal(-1, Busquedas.Binaria(lista, "CON-0015").Indice);  // en medio
            Assert.Equal(-1, Busquedas.Binaria(new List<Producto>(), "CON-001").Indice);
        }

        [Fact]
        public void Binaria_HaceMuchasMenosComparacionesQueLineal()
        {
            List<Producto> lista = ProductosOrdenados();
            string ultimo = lista[lista.Count - 1].Codigo;

            ResultadoBusqueda lineal = Busquedas.Lineal(lista, ultimo);
            ResultadoBusqueda binaria = Busquedas.Binaria(lista, ultimo);

            Assert.Equal(lista.Count, lineal.Comparaciones);
            // log₂(44) ≈ 5.5, así que nunca deberían ser más de 6.
            Assert.True(binaria.Comparaciones <= 6);
        }

        [Fact]
        public void Lineal_FuncionaConListaDesordenada()
        {
            List<Producto> lista = DatosIniciales.CrearProductos();

            Assert.Equal(0, Busquedas.Lineal(lista, "CON-001").Indice);
            Assert.Equal(-1, Busquedas.Lineal(lista, "NO-EXISTE").Indice);
        }
    }

    public class ComparadorPruebas
    {
        [Fact]
        public void MedirOrdenamientos_DevuelveTiemposValidos()
        {
            ResultadoOrdenamiento resultado = Comparador.MedirOrdenamientos(200, new Random(1));

            Assert.Equal(200, resultado.N);
            Assert.True(resultado.MsBurbuja >= 0);
            Assert.True(resultado.MsInsercion >= 0);
            Assert.True(resultado.MsListSort >= 0);
        }

        [Fact]
        public void MedirBusquedas_BinariaHaceMenosComparaciones()
        {
            ResultadoBusquedas resultado = Comparador.MedirBusquedas(1000, new Random(1), 200);

            // Lineal ≈ n/2 = 500 en promedio; binaria ≈ log₂(1000) ≈ 10.
            Assert.True(resultado.ComparacionesLineal > 100);
            Assert.True(resultado.ComparacionesBinaria <= 11);
        }
    }
}
