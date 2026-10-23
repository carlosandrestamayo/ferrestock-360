using FerreStock.Modelos;

namespace FerreStock.Algoritmos
{
    /// <summary>
    /// Ordenamientos implementados a mano para compararlos con List.Sort() de .NET.
    /// Todos ordenan los productos por código, de menor a mayor, y modifican la lista recibida.
    /// </summary>
    public static class Ordenamientos
    {
        /// <summary>
        /// Regla de orden usada por todos los algoritmos (y por List.Sort en el comparador):
        /// negativo si a va antes, cero si son iguales, positivo si a va después.
        /// CompareOrdinal compara letra por letra según su código numérico, que es lo más rápido
        /// y da el mismo resultado en cualquier computador.
        /// </summary>
        public static int CompararPorCodigo(Producto a, Producto b)
        {
            return string.CompareOrdinal(a.Codigo, b.Codigo);
        }

        /// <summary>
        /// Burbuja: recorre la lista intercambiando vecinos desordenados. En cada pasada
        /// el mayor que queda "sube" hasta el final, como una burbuja.
        /// Peor caso y caso promedio O(n²); mejor caso O(n) si la lista ya estaba ordenada.
        /// </summary>
        public static void Burbuja(List<Producto> lista)
        {
            int n = lista.Count;
            for (int pasada = 0; pasada < n - 1; pasada++)
            {
                bool huboIntercambio = false;

                // Los últimos "pasada" elementos ya quedaron en su lugar: no se revisan.
                for (int j = 0; j < n - 1 - pasada; j++)
                {
                    if (CompararPorCodigo(lista[j], lista[j + 1]) > 0)
                    {
                        Producto temporal = lista[j];
                        lista[j] = lista[j + 1];
                        lista[j + 1] = temporal;
                        huboIntercambio = true;
                    }
                }

                // Si en una pasada completa no se intercambió nada, la lista ya está ordenada.
                if (!huboIntercambio)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Inserción: como ordenar cartas en la mano. Se toma cada elemento y se corre hacia
        /// la izquierda hasta encontrar su lugar dentro de la parte que ya está ordenada.
        /// Peor caso O(n²), pero hace menos movimientos que Burbuja y es O(n) si ya estaba ordenada.
        /// </summary>
        public static void Insercion(List<Producto> lista)
        {
            for (int i = 1; i < lista.Count; i++)
            {
                // Paso 1: sacar el elemento que se va a ubicar.
                Producto actual = lista[i];
                int j = i - 1;

                // Paso 2: correr una posición a la derecha los mayores que él.
                while (j >= 0 && CompararPorCodigo(lista[j], actual) > 0)
                {
                    lista[j + 1] = lista[j];
                    j--;
                }

                // Paso 3: ponerlo en el hueco que quedó.
                lista[j + 1] = actual;
            }
        }

        /// <summary>Revisa que la lista esté ordenada por código (se usa en el comparador y en las pruebas).</summary>
        public static bool EstaOrdenada(List<Producto> lista)
        {
            for (int i = 0; i < lista.Count - 1; i++)
            {
                if (CompararPorCodigo(lista[i], lista[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
