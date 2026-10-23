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
        private MovimientoServicio? movimientos;
        private PedidoServicio? pedidos;
        private BodegaServicio? bodega;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                inventario = new InventarioServicio();
                movimientos = new MovimientoServicio(inventario);
                pedidos = new PedidoServicio(inventario, movimientos);
                bodega = new BodegaServicio(inventario);
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
            if (inventario == null || pedidos == null)
            {
                return;
            }
            // ShowDialog abre la ventana y espera a que se cierre;
            // después se actualiza la barra de estado con los cambios hechos.
            using (FrmProductos frm = new FrmProductos(inventario, pedidos))
            {
                frm.ShowDialog(this);
            }
            ActualizarBarraEstado();
        }

        private void AbrirMovimientos()
        {
            if (inventario == null || movimientos == null)
            {
                return;
            }
            using (FrmMovimientos frm = new FrmMovimientos(inventario, movimientos))
            {
                frm.ShowDialog(this);
            }
            ActualizarBarraEstado();
        }

        private void AbrirPedidos()
        {
            if (inventario == null || pedidos == null || bodega == null)
            {
                return;
            }
            using (FrmPedidos frm = new FrmPedidos(inventario, pedidos, bodega))
            {
                frm.ShowDialog(this);
            }
            ActualizarBarraEstado();
        }

        private void mnuProductos_Click(object sender, EventArgs e)
        {
            AbrirProductos();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirProductos();
        }

        private void mnuMovimientos_Click(object sender, EventArgs e)
        {
            AbrirMovimientos();
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            AbrirMovimientos();
        }

        private void mnuPedidos_Click(object sender, EventArgs e)
        {
            AbrirPedidos();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            AbrirPedidos();
        }

        private void mnuMapaBodega_Click(object sender, EventArgs e)
        {
            if (bodega == null || pedidos == null)
            {
                return;
            }
            using (FrmMapaBodega frm = new FrmMapaBodega(bodega, pedidos, null))
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuCalculadora_Click(object sender, EventArgs e)
        {
            if (inventario == null || bodega == null || pedidos == null)
            {
                return;
            }
            using (FrmCalculadora frm = new FrmCalculadora(inventario, bodega, pedidos))
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuComparador_Click(object sender, EventArgs e)
        {
            // El comparador usa datos generados, no necesita los servicios.
            using (FrmComparadorAlgoritmos frm = new FrmComparadorAlgoritmos())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        // TODO estudiante: cambiar los textos, colores o el ícono de esta ventana
        // para que coincidan con la ferretería visitada.
    }
}
