using FerreStock.Modelos;

namespace FerreStock.Algoritmos
{
    /// <summary>
    /// Resultado de una búsqueda: dónde quedó el producto (-1 si no está)
    /// y cuántas comparaciones se hicieron para encontrarlo.
    /// Contar comparaciones permite ver la diferencia entre O(n) y O(log n)
    /// aunque la búsqueda sea tan rápida que el tiempo casi no se pueda medir.
    /// </summary>
    public class ResultadoBusqueda
    {
        public int Indice { get; set; }
        public int Comparaciones { get; set; }
    }

    public static class Busquedas
    {
        /// <summary>
        /// Búsqueda lineal: revisa los productos uno por uno desde el principio.
        /// Funciona con cualquier lista (ordenada o no). O(n).
        /// </summary>
        public static ResultadoBusqueda Lineal(List<Producto> lista, string codigo)
        {
            ResultadoBusqueda resultado = new ResultadoBusqueda { Indice = -1, Comparaciones = 0 };
            for (int i = 0; i < lista.Count; i++)
            {
                resultado.Comparaciones++;
                if (lista[i].Codigo == codigo)
                {
                    resultado.Indice = i;
                    return resultado;
                }
            }
            return resultado;
        }

        /// <summary>
        /// Búsqueda binaria: la lista DEBE estar ordenada por código.
        /// Se mira el elemento del medio; si el buscado es menor se descarta la mitad derecha,
        /// si es mayor la izquierda. Cada paso parte el problema a la mitad: O(log n).
        /// Con 5.000 productos bastan unas 13 comparaciones, contra 5.000 de la lineal en el peor caso.
        /// </summary>
        public static ResultadoBusqueda Binaria(List<Producto> listaOrdenada, string codigo)
        {
            ResultadoBusqueda resultado = new ResultadoBusqueda { Indice = -1, Comparaciones = 0 };
            int izquierda = 0;
            int derecha = listaOrdenada.Count - 1;

            while (izquierda <= derecha)
            {
                // Se calcula así y no como (izquierda + derecha) / 2 para evitar desbordes con listas enormes.
                int medio = izquierda + (derecha - izquierda) / 2;
                int comparacion = string.CompareOrdinal(listaOrdenada[medio].Codigo, codigo);
                resultado.Comparaciones++;

                if (comparacion == 0)
                {
                    resultado.Indice = medio;
                    return resultado;
                }
                if (comparacion < 0)
                {
                    // El del medio es menor que el buscado: el buscado está a la derecha.
                    izquierda = medio + 1;
                }
                else
                {
                    // El del medio es mayor: el buscado está a la izquierda.
                    derecha = medio - 1;
                }
            }
            return resultado;
        }
    }
}
