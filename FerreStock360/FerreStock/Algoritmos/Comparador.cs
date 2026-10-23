using System.Diagnostics;
using FerreStock.Modelos;

namespace FerreStock.Algoritmos
{
    /// <summary>Tiempos (en milisegundos) de ordenar N productos con cada algoritmo.</summary>
    public class ResultadoOrdenamiento
    {
        public int N { get; set; }
        public double MsBurbuja { get; set; }
        public double MsInsercion { get; set; }
        public double MsListSort { get; set; }
    }

    /// <summary>Comparaciones promedio y tiempo total de hacer muchas búsquedas en N productos.</summary>
    public class ResultadoBusquedas
    {
        public int N { get; set; }
        public int CantidadBusquedas { get; set; }
        public double ComparacionesLineal { get; set; }
        public double ComparacionesBinaria { get; set; }
        public double MsLineal { get; set; }
        public double MsBinaria { get; set; }
    }

    /// <summary>
    /// Mide con Stopwatch los algoritmos propios contra List.Sort() de .NET,
    /// usando listas de productos generadas al azar.
    /// </summary>
    public static class Comparador
    {
        // TODO estudiante: agregar N = 10.000 y explicar en la sustentación por qué Burbuja tarda tanto.
        public static readonly int[] Tamanios = { 100, 1000, 5000 };

        /// <summary>Crea n productos con códigos al azar del tipo "P-004821".</summary>
        public static List<Producto> GenerarProductos(int n, Random azar)
        {
            List<Producto> lista = new List<Producto>();
            for (int i = 0; i < n; i++)
            {
                Producto p = new Producto();
                p.Codigo = "P-" + azar.Next(0, 1000000).ToString("D6");
                p.Nombre = "Producto generado " + i;
                lista.Add(p);
            }
            return lista;
        }

        /// <summary>
        /// Ordena la MISMA lista desordenada con los tres métodos (cada uno sobre su propia copia)
        /// para que la comparación sea justa.
        /// </summary>
        public static ResultadoOrdenamiento MedirOrdenamientos(int n, Random azar)
        {
            List<Producto> original = GenerarProductos(n, azar);
            ResultadoOrdenamiento resultado = new ResultadoOrdenamiento();
            resultado.N = n;

            // new List<Producto>(original) crea una copia: así cada algoritmo empieza desordenado.
            List<Producto> copia = new List<Producto>(original);
            Stopwatch reloj = Stopwatch.StartNew();
            Ordenamientos.Burbuja(copia);
            reloj.Stop();
            resultado.MsBurbuja = reloj.Elapsed.TotalMilliseconds;
            VerificarOrden(copia, "Burbuja");

            copia = new List<Producto>(original);
            reloj = Stopwatch.StartNew();
            Ordenamientos.Insercion(copia);
            reloj.Stop();
            resultado.MsInsercion = reloj.Elapsed.TotalMilliseconds;
            VerificarOrden(copia, "Inserción");

            // List.Sort usa un algoritmo O(n log n) (introsort). Se le pasa la misma regla de orden.
            copia = new List<Producto>(original);
            reloj = Stopwatch.StartNew();
            copia.Sort(Ordenamientos.CompararPorCodigo);
            reloj.Stop();
            resultado.MsListSort = reloj.Elapsed.TotalMilliseconds;
            VerificarOrden(copia, "List.Sort");

            return resultado;
        }

        /// <summary>
        /// Busca muchos códigos al azar (que sí existen) con búsqueda lineal y binaria
        /// y promedia cuántas comparaciones necesitó cada una.
        /// </summary>
        public static ResultadoBusquedas MedirBusquedas(int n, Random azar, int cantidadBusquedas)
        {
            // La búsqueda binaria necesita la lista ordenada; se ordena una vez antes de medir.
            List<Producto> lista = GenerarProductos(n, azar);
            lista.Sort(Ordenamientos.CompararPorCodigo);

            // Se eligen primero los códigos a buscar, para que las dos búsquedas busquen lo mismo.
            string[] buscados = new string[cantidadBusquedas];
            for (int i = 0; i < cantidadBusquedas; i++)
            {
                buscados[i] = lista[azar.Next(0, n)].Codigo;
            }

            long totalLineal = 0;
            Stopwatch reloj = Stopwatch.StartNew();
            for (int i = 0; i < cantidadBusquedas; i++)
            {
                totalLineal += Busquedas.Lineal(lista, buscados[i]).Comparaciones;
            }
            reloj.Stop();
            double msLineal = reloj.Elapsed.TotalMilliseconds;

            long totalBinaria = 0;
            reloj = Stopwatch.StartNew();
            for (int i = 0; i < cantidadBusquedas; i++)
            {
                totalBinaria += Busquedas.Binaria(lista, buscados[i]).Comparaciones;
            }
            reloj.Stop();
            double msBinaria = reloj.Elapsed.TotalMilliseconds;

            ResultadoBusquedas resultado = new ResultadoBusquedas();
            resultado.N = n;
            resultado.CantidadBusquedas = cantidadBusquedas;
            resultado.ComparacionesLineal = (double)totalLineal / cantidadBusquedas;
            resultado.ComparacionesBinaria = (double)totalBinaria / cantidadBusquedas;
            resultado.MsLineal = msLineal;
            resultado.MsBinaria = msBinaria;
            return resultado;
        }

        // Si un algoritmo tuviera un error, los tiempos no servirían: se avisa en vez de mostrarlos.
        private static void VerificarOrden(List<Producto> lista, string algoritmo)
        {
            if (!Ordenamientos.EstaOrdenada(lista))
            {
                throw new InvalidOperationException("El algoritmo " + algoritmo + " no dejó la lista ordenada.");
            }
        }
    }
}
