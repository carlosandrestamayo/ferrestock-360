using FerreStock.Servicios;

namespace FerreStock.Formularios
{
    /// <summary>
    /// Ventana principal: carga los datos al iniciar y abre los demás módulos desde el menú.
    /// Los servicios se crean una sola vez aquí y se pasan a cada formulario,
    /// para que todos trabajen sobre los mismos datos en memoria.
    /// </summary>
    public partial class FrmPrincipal : Form
    {
        private InventarioServicio? inventario;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                inventario = new InventarioServicio();
                ActualizarBarraEstado();
            }
            catch (Exception ex)
            {
                // Sin datos el programa no puede trabajar: se informa y se cierra.
                MessageBox.Show("No se pudieron cargar los datos.\n\n" + ex.Message,
                    "FerreStock 360", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        /// <summary>Muestra abajo cuántos productos hay y cuántos están bajo el stock mínimo.</summary>
        private void ActualizarBarraEstado()
        {
            if (inventario == null)
            {
                return;
            }
            lblEstadoProductos.Text = "Productos: " + inventario.ObtenerProductos().Count;

            int bajos = inventario.ObtenerBajoStockMinimo().Count;
            lblEstadoBajoMinimo.Text = "Bajo stock mínimo: " + bajos;
            // En rojo para llamar la atención cuando hay productos por pedir.
            if (bajos > 0)
            {
                lblEstadoBajoMinimo.ForeColor = Color.Firebrick;
            }
            else
            {
                lblEstadoBajoMinimo.ForeColor = SystemColors.ControlText;
            }
        }

        private void AbrirProductos()
        {
            if (inventario == null)
            {
                return;
            }
            MessageBox.Show("Módulo en construcción.", "FerreStock 360");
        }

        private void mnuProductos_Click(object sender, EventArgs e)
        {
            AbrirProductos();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirProductos();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        // TODO estudiante: cambiar los textos, colores o el ícono de esta ventana
        // para que coincidan con la ferretería visitada.
    }
}
