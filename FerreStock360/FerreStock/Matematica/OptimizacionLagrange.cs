namespace FerreStock.Matematica
{
    /// <summary>Datos de un producto para el problema de cuánto pedir.</summary>
    public class ProductoLagrange
    {
        public string Nombre { get; set; } = "";
        // D: demanda al año (unidades).
        public double Demanda { get; set; }
        // S: costo de hacer un pedido (transporte, trámite), en pesos.
        public double CostoPedido { get; set; }
        // h: costo de mantener una unidad guardada durante un año, en pesos.
        public double CostoMantener { get; set; }
        // v: volumen de una unidad en m³.
        public double Volumen { get; set; }
    }

    public class ResultadoLagrange
    {
        // Multiplicador de Lagrange: cuánto baja el costo por cada m³ extra de bodega.
        public double Lambda { get; set; }
        // Cantidad óptima a pedir de cada producto (mismo orden que la lista de entrada).
        public double[] Cantidades { get; set; } = new double[0];
        public double VolumenUsado { get; set; }
        public double CostoTotal { get; set; }
        // true si el espacio no alcanzaba con las cantidades sin restricción y hubo que reducirlas.
        public bool RestriccionActiva { get; set; }
        public int Iteraciones { get; set; }
    }

    /// <summary>
    /// Cantidad a pedir de varios productos cuando la bodega tiene espacio limitado.
    ///
    ///   Minimizar   C(Q) = Σ ( Dᵢ·Sᵢ / Qᵢ + hᵢ·Qᵢ / 2 )
    ///   sujeto a    Σ vᵢ·Qᵢ ≤ V
    ///
    /// Con el lagrangiano L = C(Q) + λ(Σ vᵢQᵢ − V) y ∂L/∂Qᵢ = 0 se obtiene (ver docs/modelo-matematico.md):
    ///
    ///   Qᵢ(λ) = √( 2·Dᵢ·Sᵢ / (hᵢ + 2λ·vᵢ) )
    ///
    /// Si con λ = 0 (la fórmula clásica de lote económico) las cantidades caben, esa es la respuesta.
    /// Si no caben, se busca el λ > 0 que hace que el volumen sea exactamente V, por bisección:
    /// a mayor λ, menores Qᵢ y menor volumen, así que se puede ir partiendo el intervalo a la mitad.
    /// </summary>
    public static class OptimizacionLagrange
    {
        public static double Cantidad(ProductoLagrange p, double lambda)
        {
            return Math.Sqrt(2 * p.Demanda * p.CostoPedido / (p.CostoMantener + 2 * lambda * p.Volumen));
        }

        public static double VolumenUsado(List<ProductoLagrange> productos, double lambda)
        {
            double total = 0;
            foreach (ProductoLagrange p in productos)
            {
                total += p.Volumen * Cantidad(p, lambda);
            }
            return total;
        }

        public static ResultadoLagrange Resolver(List<ProductoLagrange> productos, double volumenDisponible)
        {
            Validar(productos, volumenDisponible);
            ResultadoLagrange resultado = new ResultadoLagrange();

            // Paso 1: probar sin restricción (λ = 0).
            double lambda = 0;
            if (VolumenUsado(productos, 0) <= volumenDisponible)
            {
                resultado.RestriccionActiva = false;
            }
            else
            {
                resultado.RestriccionActiva = true;

                // Paso 2: buscar un λ "alto" con el que sí quepa (se duplica hasta lograrlo).
                double bajo = 0;
                double alto = 1;
                while (VolumenUsado(productos, alto) > volumenDisponible)
                {
                    alto = alto * 2;
                }

                // Paso 3: bisección. La respuesta está entre bajo (no cabe) y alto (sí cabe).
                int iteraciones = 0;
                while (iteraciones < 200 && (alto - bajo) > 1e-9 * Math.Max(1, alto))
                {
                    double medio = (bajo + alto) / 2;
                    if (VolumenUsado(productos, medio) > volumenDisponible)
                    {
                        bajo = medio;
                    }
                    else
                    {
                        alto = medio;
                    }
                    iteraciones++;
                }
                resultado.Iteraciones = iteraciones;
                // Se toma "alto" porque con ese valor se garantiza que el pedido cabe.
                lambda = alto;
            }

            // Paso 4: calcular cantidades, volumen y costo con el λ encontrado.
            resultado.Lambda = lambda;
            resultado.Cantidades = new double[productos.Count];
            for (int i = 0; i < productos.Count; i++)
            {
                double q = Cantidad(productos[i], lambda);
                resultado.Cantidades[i] = q;
                resultado.CostoTotal += productos[i].Demanda * productos[i].CostoPedido / q + productos[i].CostoMantener * q / 2;
            }
            resultado.VolumenUsado = VolumenUsado(productos, lambda);
            return resultado;
        }

        private static void Validar(List<ProductoLagrange> productos, double volumenDisponible)
        {
            if (productos.Count == 0)
            {
                throw new ArgumentException("Debe haber al menos un producto.");
            }
            if (volumenDisponible <= 0)
            {
                throw new ArgumentException("El volumen disponible debe ser mayor que cero.");
            }
            foreach (ProductoLagrange p in productos)
            {
                if (p.Demanda <= 0 || p.CostoPedido <= 0 || p.CostoMantener <= 0 || p.Volumen <= 0)
                {
                    throw new ArgumentException("Todos los datos de " + p.Nombre + " deben ser mayores que cero.");
                }
            }
        }
    }
}
