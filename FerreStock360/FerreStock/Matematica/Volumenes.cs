namespace FerreStock.Matematica
{
    /// <summary>
    /// Volúmenes y áreas de la bodega. Las fórmulas salen de las integrales que se
    /// deducen a mano en docs/modelo-matematico.md; aquí solo se evalúa el resultado.
    /// </summary>
    public static class Volumenes
    {
        /// <summary>
        /// Montículo de arena con forma de paraboloide: la altura a una distancia r del centro es
        /// z = h(1 − r²/R²). En coordenadas polares:
        ///   V = ∫₀^{2π} ∫₀^R h(1 − r²/R²) r dr dθ = πR²h / 2
        /// </summary>
        public static double MonticuloParabolico(double radio, double altura)
        {
            ValidarPositivos(radio, altura);
            return Math.PI * radio * radio * altura / 2;
        }

        /// <summary>
        /// Montículo con forma de cono (los lados son rectas): V = πR²h / 3.
        /// El cono es más "puntudo" que el paraboloide, por eso tiene menos volumen con el mismo R y h.
        /// </summary>
        public static double MonticuloConico(double radio, double altura)
        {
            ValidarPositivos(radio, altura);
            return Math.PI * radio * radio * altura / 3;
        }

        /// <summary>
        /// Aproxima la misma integral doble del paraboloide con una suma de Riemann en polares:
        /// se parte el círculo en "anillos" (Δr) y "tajadas" (Δθ), y en cada pedacito se suma
        /// altura × área, donde el área de un pedacito en polares es r·Δr·Δθ.
        /// Sirve para comprobar que la fórmula πR²h/2 está bien: con más divisiones, más se acerca.
        /// </summary>
        public static double MonticuloParabolicoNumerico(double radio, double altura, int divisiones)
        {
            ValidarPositivos(radio, altura);
            if (divisiones < 1)
            {
                throw new ArgumentException("Debe haber al menos una división.");
            }

            double deltaR = radio / divisiones;
            double deltaTheta = 2 * Math.PI / divisiones;
            double suma = 0;

            for (int i = 0; i < divisiones; i++)
            {
                // Se usa el punto medio de cada anillo: da mejor aproximación que el borde.
                double r = (i + 0.5) * deltaR;
                double z = altura * (1 - (r * r) / (radio * radio));
                for (int j = 0; j < divisiones; j++)
                {
                    suma += z * r * deltaR * deltaTheta;
                }
            }
            return suma;
        }

        /// <summary>Área ocupada por pasillos rectangulares iguales.</summary>
        public static double AreaPasillos(int cantidadPasillos, double anchoPasillo, double largoPasillo)
        {
            if (cantidadPasillos < 0 || anchoPasillo < 0 || largoPasillo < 0)
            {
                throw new ArgumentException("Los datos de los pasillos no pueden ser negativos.");
            }
            return cantidadPasillos * anchoPasillo * largoPasillo;
        }

        /// <summary>
        /// Área útil = ∫∫_bodega dA − área de pasillos. Para una bodega rectangular
        /// la integral doble de 1 sobre [0, ancho] × [0, largo] es ancho × largo.
        /// </summary>
        public static double AreaUtil(double ancho, double largo, double areaPasillos)
        {
            ValidarPositivos(ancho, largo);
            double total = ancho * largo;
            if (areaPasillos < 0 || areaPasillos >= total)
            {
                throw new ArgumentException("El área de pasillos debe estar entre 0 y el área total de la bodega.");
            }
            return total - areaPasillos;
        }

        /// <summary>Volumen útil para almacenar: área útil × altura útil de los estantes.</summary>
        public static double VolumenUtil(double areaUtil, double alturaUtil)
        {
            ValidarPositivos(areaUtil, alturaUtil);
            return areaUtil * alturaUtil;
        }

        /// <summary>Ocupación (%) = volumen ocupado / volumen útil × 100.</summary>
        public static double PorcentajeOcupacion(double volumenOcupado, double volumenUtil)
        {
            if (volumenUtil <= 0)
            {
                throw new ArgumentException("El volumen útil debe ser mayor que cero.");
            }
            return volumenOcupado / volumenUtil * 100;
        }

        private static void ValidarPositivos(double a, double b)
        {
            if (a <= 0 || b <= 0)
            {
                throw new ArgumentException("Las medidas deben ser mayores que cero.");
            }
        }
    }
}
