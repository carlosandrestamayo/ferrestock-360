using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Formularios
{
    /// <summary>
    /// Reporte de indicadores administrativos. El cálculo se hace con await Task.Run para que la
    /// ventana no se congele mientras tanto (con datos reales y muchos pedidos puede tardar).
    /// </summary>
    public partial class FrmIndicadores : Form
    {
        private readonly IndicadorServicio indicadores;

        public FrmIndicadores(IndicadorServicio indicadores)
        {
            InitializeComponent();
            this.indicadores = indicadores;
        }

        private async void FrmIndicadores_Load(object sender, EventArgs e)
        {
            await CalcularAsync();
        }

        private async void btnCalcular_Click(object sender, EventArgs e)
        {
            await CalcularAsync();
        }

        private async Task CalcularAsync()
        {
            btnCalcular.Enabled = false;
            // Estilo "marquesina": la barra se mueve sin indicar porcentaje, porque no sabemos cuánto falta.
            barraProgreso.Visible = true;
            lblEstado.Text = "Calculando indicadores...";
            try
            {
                ReporteIndicadores reporte = await Task.Run(() => indicadores.Calcular());
                MostrarReporte(reporte);
                lblEstado.Text = "Actualizado: " + reporte.Fecha.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Ocurrió un error.";
                MessageBox.Show("No se pudieron calcular los indicadores.\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                barraProgreso.Visible = false;
                btnCalcular.Enabled = true;
            }
        }

        private void MostrarReporte(ReporteIndicadores r)
        {
            string exactitud = "sin conteos (registre ajustes en Movimientos)";
            if (r.ConteosRealizados > 0)
            {
                exactitud = r.ExactitudPorcentaje.ToString("N1") + " % (" + r.ConteosExactos + " de " + r.ConteosRealizados + " conteos exactos)";
            }

            lblResumen.Text =
                "Valor del inventario (a costo): $" + r.ValorInventarioCosto.ToString("N0") + "\n" +
                "Ventas del mes (a costo): $" + r.VentasMesCosto.ToString("N0") + "\n" +
                "Rotación mensual: " + r.RotacionMensual.ToString("N2") + " veces\n" +
                "Días de inventario: " + r.DiasInventario.ToString("N1") + " días\n" +
                "Ocupación de la bodega: " + r.OcupacionPorcentaje.ToString("N1") + " %\n" +
                "Productos bajo el stock mínimo: " + r.BajoStockMinimo.Count + "\n" +
                "Exactitud de inventario: " + exactitud;

            ResultadoSimulacion s = r.Simulacion;
            lblSimulacion.Text =
                "Simulación de alistamiento (" + s.CantidadPedidos + " pedidos)\n\n" +
                "Con las ubicaciones actuales:\n   " + s.DistanciaPromedioAntes.ToString("N1") + " m y " +
                s.MinutosPromedioAntes.ToString("N2") + " min por pedido\n" +
                "Con las ubicaciones sugeridas (menor costo):\n   " + s.DistanciaPromedioDespues.ToString("N1") + " m y " +
                s.MinutosPromedioDespues.ToString("N2") + " min por pedido\n\n" +
                "Reducción del tiempo de alistamiento: " + s.ReduccionTiempoPorcentaje.ToString("N1") + " %";

            dgvABC.Rows.Clear();
            foreach (ItemABC item in r.ClasificacionABC)
            {
                int fila = dgvABC.Rows.Add(item.Codigo, item.Nombre, item.ValorVendido.ToString("N0"),
                                           item.Porcentaje.ToString("N1"), item.PorcentajeAcumulado.ToString("N1"), item.Clase);
                dgvABC.Rows[fila].DefaultCellStyle.BackColor = ColorClase(item.Clase);
            }

            dgvBajoMinimo.Rows.Clear();
            foreach (Producto p in r.BajoStockMinimo)
            {
                dgvBajoMinimo.Rows.Add(p.Codigo, p.Nombre, p.Stock, p.StockMinimo, p.StockMinimo - p.Stock, p.UbicacionId);
            }

            dgvZonas.Rows.Clear();
            foreach (OcupacionZona z in r.OcupacionPorZona)
            {
                dgvZonas.Rows.Add(z.Zona, z.OcupadoM3.ToString("N2"), z.CapacidadM3.ToString("N2"), z.Porcentaje.ToString("N1"));
            }

            dgvABC.ClearSelection();
            dgvBajoMinimo.ClearSelection();
            dgvZonas.ClearSelection();
            tabABC.Text = "Clasificación ABC";
            tabBajoMinimo.Text = "Bajo stock mínimo (" + r.BajoStockMinimo.Count + ")";
        }

        private static Color ColorClase(string clase)
        {
            if (clase == "A")
            {
                return Color.FromArgb(222, 247, 222);
            }
            if (clase == "B")
            {
                return Color.FromArgb(255, 243, 205);
            }
            return Color.White;
        }
    }
}
