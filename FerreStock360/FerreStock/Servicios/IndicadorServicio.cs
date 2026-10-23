using FerreStock.Matematica;
using FerreStock.Modelos;

namespace FerreStock.Servicios
{
    /// <summary>Un producto dentro de la clasificación ABC.</summary>
    public class ItemABC
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public decimal ValorVendido { get; set; }
        public double Porcentaje { get; set; }
        public double PorcentajeAcumulado { get; set; }
        public string Clase { get; set; } = "";
    }

    /// <summary>Volumen ocupado y capacidad de una zona de la bodega.</summary>
    public class OcupacionZona
    {
        public string Zona { get; set; } = "";
        public double OcupadoM3 { get; set; }
        public double CapacidadM3 { get; set; }
        public double Porcentaje { get; set; }
    }

    /// <summary>Todos los indicadores calculados en un momento dado.</summary>
    public class ReporteIndicadores
    {
        public DateTime Fecha { get; set; }
        public decimal ValorInventarioCosto { get; set; }
        public decimal VentasMesCosto { get; set; }
        public double RotacionMensual { get; set; }
        public double DiasInventario { get; set; }
        public double OcupacionPorcentaje { get; set; }
        public List<OcupacionZona> OcupacionPorZona { get; set; } = new List<OcupacionZona>();
        public List<ItemABC> ClasificacionABC { get; set; } = new List<ItemABC>();
        public List<Producto> BajoStockMinimo { get; set; } = new List<Producto>();
        public int ConteosRealizados { get; set; }
        public int ConteosExactos { get; set; }
        public double ExactitudPorcentaje { get; set; }
        public ResultadoSimulacion Simulacion { get; set; } = new ResultadoSimulacion();
    }

    /// <summary>
    /// Indicadores administrativos del inventario (ver CLAUDE.md, sección 8).
    /// Calcular() solo LEE los datos, por eso se puede ejecutar en otro hilo con Task.Run.
    /// </summary>
    public class IndicadorServicio
    {
        private readonly InventarioServicio inventario;
        private readonly MovimientoServicio movimientos;
        private readonly PedidoServicio pedidos;
        private readonly BodegaServicio bodega;

        public IndicadorServicio(InventarioServicio inventario, MovimientoServicio movimientos,
                                 PedidoServicio pedidos, BodegaServicio bodega)
        {
            this.inventario = inventario;
            this.movimientos = movimientos;
            this.pedidos = pedidos;
            this.bodega = bodega;
        }

        public ReporteIndicadores Calcular()
        {
            ReporteIndicadores reporte = new ReporteIndicadores();
            reporte.Fecha = DateTime.Now;
            List<Producto> productos = inventario.ObtenerProductos();

            // 1. Rotación = ventas del periodo (a costo) / inventario promedio (a costo).
            //    Como el sistema aún no guarda cierres mensuales, el inventario promedio se aproxima
            //    con el inventario actual.
            //    TODO estudiante: guardar el valor del inventario al inicio de cada mes y usar (inicio + fin) / 2.
            foreach (Producto p in productos)
            {
                reporte.ValorInventarioCosto += p.Stock * p.PrecioCompra;
                reporte.VentasMesCosto += p.VentasMes * p.PrecioCompra;
            }
            if (reporte.ValorInventarioCosto > 0)
            {
                reporte.RotacionMensual = (double)(reporte.VentasMesCosto / reporte.ValorInventarioCosto);
            }

            // 2. Días de inventario = 30 / rotación mensual: cuántos días alcanza lo que hay en bodega.
            if (reporte.RotacionMensual > 0)
            {
                reporte.DiasInventario = 30 / reporte.RotacionMensual;
            }

            // 3. Ocupación: total y por zona.
            CalcularOcupacion(reporte);

            // 4. Clasificación ABC.
            reporte.ClasificacionABC = ClasificarABC(productos);

            // 5. Productos bajo el stock mínimo.
            reporte.BajoStockMinimo = inventario.ObtenerBajoStockMinimo();

            // 6. Exactitud de inventario = conteos físicos que coincidieron con el sistema / conteos realizados.
            //    Cada conteo queda registrado como un ajuste; si la diferencia fue 0, el sistema estaba exacto.
            foreach (Movimiento m in movimientos.ObtenerMovimientos())
            {
                if (m.Tipo == TipoMovimiento.Ajuste)
                {
                    reporte.ConteosRealizados++;
                    if (m.Cantidad == 0)
                    {
                        reporte.ConteosExactos++;
                    }
                }
            }
            if (reporte.ConteosRealizados > 0)
            {
                reporte.ExactitudPorcentaje = 100.0 * reporte.ConteosExactos / reporte.ConteosRealizados;
            }

            // 7. Simulación de tiempos de alistamiento antes / después de reubicar.
            reporte.Simulacion = bodega.SimularAntesDespues(pedidos.ObtenerPedidos(),
                                                            CostoUbicacion.CoeficienteDistancia, CostoUbicacion.CoeficienteAltura);

            // TODO estudiante: agregar un indicador nuevo, por ejemplo el margen de utilidad por categoría
            // = Σ (precio venta − precio compra) × ventas / Σ precio venta × ventas.
            return reporte;
        }

        private void CalcularOcupacion(ReporteIndicadores reporte)
        {
            // Se suma la capacidad de cada zona y el volumen de los productos ubicados en ella.
            Dictionary<string, OcupacionZona> zonas = new Dictionary<string, OcupacionZona>();
            Dictionary<string, string> zonaDeUbicacion = new Dictionary<string, string>();
            foreach (Ubicacion u in inventario.ObtenerUbicaciones())
            {
                if (!zonas.ContainsKey(u.Zona))
                {
                    zonas[u.Zona] = new OcupacionZona { Zona = u.Zona };
                }
                zonas[u.Zona].CapacidadM3 += u.CapacidadM3;
                zonaDeUbicacion[u.Id] = u.Zona;
            }

            foreach (Producto p in inventario.ObtenerProductos())
            {
                if (zonaDeUbicacion.TryGetValue(p.UbicacionId, out string? zona))
                {
                    zonas[zona].OcupadoM3 += p.Stock * p.VolumenUnitarioM3;
                }
            }

            double totalOcupado = 0;
            double totalCapacidad = 0;
            foreach (OcupacionZona z in zonas.Values)
            {
                if (z.CapacidadM3 > 0)
                {
                    z.Porcentaje = Volumenes.PorcentajeOcupacion(z.OcupadoM3, z.CapacidadM3);
                }
                totalOcupado += z.OcupadoM3;
                totalCapacidad += z.CapacidadM3;
                reporte.OcupacionPorZona.Add(z);
            }
            if (totalCapacidad > 0)
            {
                reporte.OcupacionPorcentaje = Volumenes.PorcentajeOcupacion(totalOcupado, totalCapacidad);
            }
        }

        /// <summary>
        /// Clasificación ABC por valor vendido (ventas del mes × precio de venta):
        /// se ordena de mayor a menor y se va acumulando el porcentaje del total.
        /// Un producto es A si al llegar a él el acumulado todavía no pasaba del 80 %,
        /// B si no pasaba del 95 % (el siguiente 15 %) y C en otro caso.
        /// Se mira el acumulado ANTES de sumar el producto para que el más vendido siempre sea A.
        /// </summary>
        public static List<ItemABC> ClasificarABC(List<Producto> productos)
        {
            // Paso 1: calcular el valor vendido de cada producto y el total.
            List<ItemABC> items = new List<ItemABC>();
            decimal total = 0;
            foreach (Producto p in productos)
            {
                ItemABC item = new ItemABC();
                item.Codigo = p.Codigo;
                item.Nombre = p.Nombre;
                item.ValorVendido = p.VentasMes * p.PrecioVenta;
                total += item.ValorVendido;
                items.Add(item);
            }

            // Paso 2: ordenar de mayor a menor valor (aquí no se evalúa el ordenamiento, se usa List.Sort).
            items.Sort(CompararPorValorDescendente);

            // Paso 3: recorrer acumulando porcentajes y asignar la clase.
            double acumulado = 0;
            foreach (ItemABC item in items)
            {
                if (total > 0)
                {
                    item.Porcentaje = (double)(item.ValorVendido / total) * 100;
                }

                if (total > 0 && acumulado < 80)
                {
                    item.Clase = "A";
                }
                else if (total > 0 && acumulado < 95)
                {
                    item.Clase = "B";
                }
                else
                {
                    item.Clase = "C";
                }

                acumulado += item.Porcentaje;
                item.PorcentajeAcumulado = acumulado;
            }
            return items;
        }

        private static int CompararPorValorDescendente(ItemABC a, ItemABC b)
        {
            // b antes que a para que quede de MAYOR a menor.
            return b.ValorVendido.CompareTo(a.ValorVendido);
        }
    }
}
