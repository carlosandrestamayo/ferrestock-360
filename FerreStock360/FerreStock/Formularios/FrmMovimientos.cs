using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Formularios
{
    /// <summary>
    /// Registro de entradas, salidas y ajustes, con botón Deshacer.
    /// La lógica (validar stock, la pila de deshacer, guardar) está en MovimientoServicio.
    /// </summary>
    public partial class FrmMovimientos : Form
    {
        private readonly InventarioServicio inventario;
        private readonly MovimientoServicio movimientos;

        public FrmMovimientos(InventarioServicio inventario, MovimientoServicio movimientos)
        {
            InitializeComponent();
            this.inventario = inventario;
            this.movimientos = movimientos;
        }

        private void FrmMovimientos_Load(object sender, EventArgs e)
        {
            // Se agregan los objetos Producto; DisplayMember muestra "código - nombre".
            cmbProducto.DisplayMember = "Descripcion";
            foreach (Producto p in inventario.ObtenerProductos())
            {
                cmbProducto.Items.Add(p);
            }

            cmbTipo.Items.Add(TipoMovimiento.Entrada);
            cmbTipo.Items.Add(TipoMovimiento.Salida);
            cmbTipo.Items.Add(TipoMovimiento.Ajuste);
            cmbTipo.SelectedIndex = 0;

            MostrarMovimientos();
            ActualizarDeshacer();
        }

        /// <summary>Llena la tabla con el historial, el más reciente arriba.</summary>
        private void MostrarMovimientos()
        {
            dgvMovimientos.Rows.Clear();
            foreach (Movimiento m in movimientos.ObtenerRecientesPrimero())
            {
                Producto? producto = inventario.BuscarPorCodigo(m.CodigoProducto);
                string nombre = "(producto eliminado)";
                if (producto != null)
                {
                    nombre = producto.Nombre;
                }
                dgvMovimientos.Rows.Add(m.Id, m.Fecha.ToString("yyyy-MM-dd HH:mm"), m.CodigoProducto, nombre,
                                        m.Tipo.ToString(), m.Cantidad, m.Observacion);
            }
            dgvMovimientos.ClearSelection();
        }

        /// <summary>El botón Deshacer solo se activa si la pila tiene algo, y se dice qué se va a revertir.</summary>
        private void ActualizarDeshacer()
        {
            Movimiento? siguiente = movimientos.VerSiguienteParaDeshacer();
            btnDeshacer.Enabled = siguiente != null;
            lblPila.Text = "Movimientos para deshacer: " + movimientos.CantidadParaDeshacer();
            if (siguiente == null)
            {
                lblSiguiente.Text = "";
            }
            else
            {
                lblSiguiente.Text = "Se deshará: " + siguiente.Tipo + " de " + siguiente.Cantidad + " (" + siguiente.CodigoProducto + ")";
            }
        }

        private void MostrarStockActual()
        {
            Producto? producto = cmbProducto.SelectedItem as Producto;
            if (producto == null)
            {
                lblStockActual.Text = "Stock actual: -";
            }
            else
            {
                lblStockActual.Text = "Stock actual: " + producto.Stock + " " + producto.Unidad;
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarStockActual();
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // En un ajuste no se escribe cuánto entra o sale, sino cuánto se contó en la bodega.
            if (cmbTipo.SelectedItem is TipoMovimiento tipo && tipo == TipoMovimiento.Ajuste)
            {
                lblCantidad.Text = "Conteo físico";
            }
            else
            {
                lblCantidad.Text = "Cantidad";
            }
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            Producto? producto = cmbProducto.SelectedItem as Producto;
            if (producto == null)
            {
                MessageBox.Show("Debe elegir un producto.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TipoMovimiento tipo = (TipoMovimiento)cmbTipo.SelectedItem!;
            int cantidad = (int)nudCantidad.Value;

            btnRegistrar.Enabled = false;
            try
            {
                if (tipo == TipoMovimiento.Entrada)
                {
                    await movimientos.RegistrarEntradaAsync(producto.Codigo, cantidad, txtObservacion.Text);
                }
                else if (tipo == TipoMovimiento.Salida)
                {
                    await movimientos.RegistrarSalidaAsync(producto.Codigo, cantidad, txtObservacion.Text);
                }
                else
                {
                    await movimientos.RegistrarAjusteAsync(producto.Codigo, cantidad, txtObservacion.Text);
                }

                nudCantidad.Value = 0;
                txtObservacion.Text = "";
                MostrarMovimientos();
                MostrarStockActual();
                ActualizarDeshacer();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar el movimiento.\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRegistrar.Enabled = true;
            }
        }

        private async void btnDeshacer_Click(object sender, EventArgs e)
        {
            btnDeshacer.Enabled = false;
            try
            {
                Movimiento deshecho = await movimientos.DeshacerAsync();
                MessageBox.Show("Se deshizo: " + deshecho.Tipo + " de " + deshecho.Cantidad + " (" + deshecho.CodigoProducto + ").",
                    "Deshacer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "No se puede deshacer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo deshacer el movimiento.\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Se actualiza en cualquier caso: aunque falle, la pila pudo cambiar.
                MostrarMovimientos();
                MostrarStockActual();
                ActualizarDeshacer();
            }
        }

        // TODO estudiante: agregar un filtro para ver solo los movimientos de un producto o de un tipo.
    }
}
