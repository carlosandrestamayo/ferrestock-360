namespace FerreStock.Matematica
{
    /// <summary>Vector gradiente ∇C = (∂C/∂x, ∂C/∂y, ∂C/∂z).</summary>
    public class Gradiente
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    /// <summary>
    /// Costo de tener un producto en el punto (x, y, z) de la bodega (función de tres variables):
    ///
    ///   C(x, y, z) = r · ( a·√(x² + y²) + b·p·z )
    ///
    /// r = ventas del mes (cuántas veces hay que ir a buscarlo), p = peso unitario (kg),
    /// √(x² + y²) = distancia en planta al mostrador, z = altura del nivel del estante.
    /// a y b son pesos que dicen cuánto importa caminar y cuánto importa subir peso.
    /// </summary>
    public static class CostoUbicacion
    {
        // TODO estudiante: ajustar a y b con lo observado en la ferretería y justificarlos.
        // a = 1: cada metro caminado cuenta 1. b = 0,1: subir 1 kg a 1 m de altura "equivale" a caminar 0,1 m.
        public const double CoeficienteDistancia = 1.0;
        public const double CoeficienteAltura = 0.1;

        public static double Costo(double ventasMes, double pesoKg, double x, double y, double z, double a, double b)
        {
            double distanciaPlanta = Math.Sqrt(x * x + y * y);
            return ventasMes * (a * distanciaPlanta + b * pesoKg * z);
        }

        /// <summary>
        /// Derivadas parciales de C:
        ///   ∂C/∂x = r·a·x / √(x² + y²)
        ///   ∂C/∂y = r·a·y / √(x² + y²)
        ///   ∂C/∂z = r·b·p
        /// Todas son positivas cuando x, y, z ≥ 0: el costo SUBE al alejarse del mostrador o al subir de nivel,
        /// así que para bajarlo hay que moverse en la dirección de −∇C (hacia el origen y hacia abajo).
        /// En el origen la raíz vale 0 y la derivada no existe; ahí se toma 0 en x e y.
        /// </summary>
        public static Gradiente CalcularGradiente(double ventasMes, double pesoKg, double x, double y, double z, double a, double b)
        {
            Gradiente g = new Gradiente();
            double distanciaPlanta = Math.Sqrt(x * x + y * y);
            if (distanciaPlanta > 0)
            {
                g.X = ventasMes * a * x / distanciaPlanta;
                g.Y = ventasMes * a * y / distanciaPlanta;
            }
            g.Z = ventasMes * b * pesoKg;
            return g;
        }
    }
}
