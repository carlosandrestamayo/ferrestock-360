using FerreStock.Algoritmos;

namespace FerreStock.Formularios
{
    /// <summary>
    /// Ejecuta el comparador de algoritmos y muestra los resultados en dos tablas.
    /// Las mediciones corren en otro hilo con Task.Run para que la ventana no se congele.
    /// </summary>
    public partial class FrmComparadorAlgoritmos : Form
    {
        // Cuántas búsquedas se hacen para sacar el promedio de comparaciones.
        private const int CantidadBusquedas = 1000;

        public FrmComparadorAlgoritmos()
        {
            InitializeComponent();
        }

        private async void btnEjecutar_Click(object sender, EventArgs e)
        {
            btnEjecutar.Enabled = false;
            dgvOrdenamientos.Rows.Clear();
            dgvBusquedas.Rows.Clear();

            // Cada tamaño N tiene dos pasos: ordenamientos y búsquedas.
            barraProgreso.Minimum = 0;
            barraProgreso.Maximum = Comparador.Tamanios.Length * 2;
            barraProgreso.Value = 0;

            // Semilla fija: cada vez que se ejecute se generan los mismos datos, así los resultados se pueden repetir.
            Random azar = new Random(2026);

            try
            {
                foreach (int n in Comparador.Tamanios)
                {
                    lblEstado.Text = "Ordenando " + n.ToString("N0") + " productos...";
                    // Task.Run manda el trabajo pesado a otro hilo. Mientras tanto la ventana sigue
                    // respondiendo; "await" espera el resultado y vuelve aquí, al hilo de la ventana,
                    // por eso después se pueden tocar las tablas sin problema.
                    ResultadoOrdenamiento orden = await Task.Run(() => Comparador.MedirOrdenamientos(n, azar));
                    dgvOrdenamientos.Rows.Add(orden.N.ToString("N0"), orden.MsBurbuja.ToString("N2"),
                                              orden.MsInsercion.ToString("N2"), orden.MsListSort.ToString("N2"));
                    barraProgreso.Value++;

                    lblEstado.Text = "Buscando en " + n.ToString("N0") + " productos...";
                    ResultadoBusquedas busqueda = await Task.Run(() => Comparador.MedirBusquedas(n, azar, CantidadBusquedas));
                    dgvBusquedas.Rows.Add(busqueda.N.ToString("N0"), busqueda.CantidadBusquedas.ToString("N0"),
                                          busqueda.ComparacionesLineal.ToString("N1"), busqueda.ComparacionesBinaria.ToString("N1"),
                                          busqueda.MsLineal.ToString("N3"), busqueda.MsBinaria.ToString("N3"));
                    barraProgreso.Value++;
                }
                dgvOrdenamientos.ClearSelection();
                dgvBusquedas.ClearSelection();
                lblEstado.Text = "Listo. Los tiempos varían un poco en cada ejecución.";
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Ocurrió un error.";
                MessageBox.Show("No se pudo completar la comparación.\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEjecutar.Enabled = true;
            }
        }
    }
}
