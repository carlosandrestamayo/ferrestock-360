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
        private IndicadorServicio? indicadores;

        public FrmPrincipal()
        {
            InitializeComponent();
            CargarImagenes();
        }

        /// <summary>
        /// Pone la imagen de portada en el encabezado y el ícono de la ventana.
        /// Las imágenes están en la carpeta Imagenes junto al .exe; si alguna falta,
        /// la ventana se muestra igual con el fondo de color, por eso el error no se le muestra al usuario.
        /// </summary>
        private void CargarImagenes()
        {
            string carpeta = Path.Combine(AppContext.BaseDirectory, "Imagenes");
            try
            {
                string rutaPortada = Path.Combine(carpeta, "portada.png");
                if (File.Exists(rutaPortada))
                {
                    pnlEncabezado.BackgroundImage = Image.FromFile(rutaPortada);
                }

                string rutaIcono = Path.Combine(carpeta, "icono.ico");
                if (File.Exists(rutaIcono))
                {
                    Icon = new Icon(rutaIcono);
                }
            }
            catch (Exception)
            {
                // Imagen dañada o con formato no válido: se deja el diseño sin imagen.
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                inventario = new InventarioServicio();
                movimientos = new MovimientoServicio(inventario);
                pedidos = new PedidoServicio(inventario, movimientos);
                bodega = new BodegaServicio(inventario);
                indicadores = new IndicadorServicio(inventario, movimientos, pedidos, bodega);
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

        /// <summary>Muestra abajo cuántos productos hay, cuántos están bajo el mínimo y cuántos pedidos esperan.</summary>
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

            if (pedidos != null)
            {
                lblEstadoPedidos.Text = "Pedidos pendientes: " + pedidos.ObtenerColaEnOrden().Count;
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
            ActualizarBarraEstado();
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

        private void mnuIndicadores_Click(object sender, EventArgs e)
        {
            if (indicadores == null)
            {
                return;
            }
            using (FrmIndicadores frm = new FrmIndicadores(indicadores))
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

        private void mnuAcercaDe_Click(object sender, EventArgs e)
        {
            // TODO estudiante: escribir aquí los nombres de los integrantes del equipo.
            MessageBox.Show("FerreStock 360\n" +
                "Proyecto integrador SIGLO 360 - Equipo 2: Ferretería\n\n" +
                "Inventario con ubicación espacial, priorización de pedidos y optimización de la bodega.\n\n" +
                "Datos guardados en:\n" + Datos.RutasDatos.Carpeta,
                "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // TODO estudiante: cambiar los textos, colores o el ícono de esta ventana
        // para que coincidan con la ferretería visitada. Para cambiar la portada basta con
        // reemplazar Imagenes/portada.png por otra imagen de 1200 x 300 píxeles
        // (dejar la parte izquierda oscura y despejada, porque ahí va el título).
    }
}
