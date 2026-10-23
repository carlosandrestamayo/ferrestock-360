using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Formularios
{
    /// <summary>
    /// A la izquierda se arma un pedido nuevo. A la derecha arriba está la cola de atención
    /// (en el orden que da la ColaPrioridad) y abajo los pedidos alistados listos para despachar.
    /// </summary>
    public partial class FrmPedidos : Form
    {
        private readonly InventarioServicio inventario;
        private readonly PedidoServicio pedidos;
        private readonly BodegaServicio bodega;

        // Ítems del pedido que el usuario está armando (todavía no se ha creado).
        private readonly List<ItemPedido> itemsNuevos = new List<ItemPedido>();

        public FrmPedidos(InventarioServicio inventario, PedidoServicio pedidos, BodegaServicio bodega)
        {
            InitializeComponent();
            this.inventario = inventario;
            this.pedidos = pedidos;
            this.bodega = bodega;
        }

        private void FrmPedidos_Load(object sender, EventArgs e)
        {
            cmbPrioridad.Items.Add(PrioridadPedido.Urgente);
            cmbPrioridad.Items.Add(PrioridadPedido.Mayorista);
            cmbPrioridad.Items.Add(PrioridadPedido.Mostrador);
            cmbPrioridad.SelectedItem = PrioridadPedido.Mostrador;

            cmbProducto.DisplayMember = "Descripcion";
            foreach (Producto p in inventario.ObtenerProductos())
            {
                cmbProducto.Items.Add(p);
            }

            MostrarItemsNuevos();
            MostrarPedidos();
        }

        // ---------------------------------------------------------------
        // Mostrar datos
        // ---------------------------------------------------------------

        private void MostrarItemsNuevos()
        {
            dgvItems.Rows.Clear();
            foreach (ItemPedido item in itemsNuevos)
            {
                dgvItems.Rows.Add(item.CodigoProducto, NombreProducto(item.CodigoProducto), item.Cantidad);
            }
            dgvItems.ClearSelection();
        }

        private void MostrarPedidos()
        {
            // Cola: el número de "Turno" es la posición en que se atenderá cada pedido.
            dgvCola.Rows.Clear();
            int turno = 1;
            foreach (Pedido p in pedidos.ObtenerColaEnOrden())
            {
                dgvCola.Rows.Add(turno, p.Id, p.Cliente, p.Prioridad.ToString(), p.Fecha.ToString("yyyy-MM-dd HH:mm"), DescribirItems(p));
                turno++;
            }
            dgvCola.ClearSelection();

            dgvAlistados.Rows.Clear();
            foreach (Pedido p in pedidos.ObtenerPorEstado(EstadoPedido.Alistado))
            {
                dgvAlistados.Rows.Add(p.Id, p.Cliente, p.Prioridad.ToString(), p.Fecha.ToString("yyyy-MM-dd HH:mm"), DescribirItems(p));
            }
            dgvAlistados.ClearSelection();

            Pedido? siguiente = pedidos.VerSiguiente();
            btnAtenderSiguiente.Enabled = siguiente != null;
            if (siguiente == null)
            {
                lblSiguiente.Text = "No hay pedidos pendientes.";
            }
            else
            {
                lblSiguiente.Text = "Siguiente: pedido " + siguiente.Id + " de " + siguiente.Cliente + " (" + siguiente.Prioridad + ")";
            }
            lblDespachados.Text = "Despachados: " + pedidos.ObtenerPorEstado(EstadoPedido.Despachado).Count;
        }

        // Texto corto con los ítems, por ejemplo "CON-001 x20, CON-006 x30".
        private static string DescribirItems(Pedido pedido)
        {
            string texto = "";
            foreach (ItemPedido item in pedido.Items)
            {
                if (texto != "")
                {
                    texto += ", ";
                }
                texto += item.CodigoProducto + " x" + item.Cantidad;
            }
            return texto;
        }

        private string NombreProducto(string codigo)
        {
            Producto? producto = inventario.BuscarPorCodigo(codigo);
            if (producto == null)
            {
                return "(no existe)";
            }
            return producto.Nombre;
        }

        // ---------------------------------------------------------------
        // Armar el pedido nuevo
        // ---------------------------------------------------------------

        private void btnAgregarItem_Click(object sender, EventArgs e)
        {
            try
            {
                Producto? producto = cmbProducto.SelectedItem as Producto;
                string codigo = "";
                if (producto != null)
                {
                    codigo = producto.Codigo;
                }
                pedidos.AgregarItem(itemsNuevos, codigo, (int)nudCantidad.Value);
                nudCantidad.Value = 1;
                MostrarItemsNuevos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuitarItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione el ítem que quiere quitar.", "Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Las filas de la tabla están en el mismo orden que la lista itemsNuevos.
            itemsNuevos.RemoveAt(dgvItems.SelectedRows[0].Index);
            MostrarItemsNuevos();
        }

        private async void btnCrearPedido_Click(object sender, EventArgs e)
        {
            btnCrearPedido.Enabled = false;
            try
            {
                PrioridadPedido prioridad = (PrioridadPedido)cmbPrioridad.SelectedItem!;
                Pedido nuevo = await pedidos.CrearPedidoAsync(txtCliente.Text, prioridad, itemsNuevos);

                MessageBox.Show("Pedido " + nuevo.Id + " creado y agregado a la cola.", "Pedidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCliente.Text = "";
                cmbPrioridad.SelectedItem = PrioridadPedido.Mostrador;
                itemsNuevos.Clear();
                MostrarItemsNuevos();
                MostrarPedidos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear el pedido.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCrearPedido.Enabled = true;
            }
        }

        // ---------------------------------------------------------------
        // Atender y despachar
        // ---------------------------------------------------------------

        private async void btnAtenderSiguiente_Click(object sender, EventArgs e)
        {
            btnAtenderSiguiente.Enabled = false;
            try
            {
                Pedido atendido = await pedidos.AtenderSiguienteAsync();
                MostrarPedidos();
                DialogResult verRuta = MessageBox.Show("Alistar pedido " + atendido.Id + " de " + atendido.Cliente + ":\n" +
                    DescribirItems(atendido) + "\n\n¿Ver la ruta de alistamiento en el mapa?",
                    "Pedido alistado", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (verRuta == DialogResult.Yes)
                {
                    using (FrmMapaBodega mapa = new FrmMapaBodega(bodega, pedidos, atendido))
                    {
                        mapa.ShowDialog(this);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo atender el pedido.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarPedidos();
            }
        }

        private async void btnDespachar_Click(object sender, EventArgs e)
        {
            if (dgvAlistados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione el pedido alistado que quiere despachar.", "Pedidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int pedidoId = (int)dgvAlistados.SelectedRows[0].Cells[colAlistadoId.Index].Value!;

            btnDespachar.Enabled = false;
            try
            {
                await pedidos.DespacharAsync(pedidoId);
                MessageBox.Show("Pedido " + pedidoId + " despachado. Se registraron las salidas de inventario.",
                    "Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "No se puede despachar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo despachar el pedido.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDespachar.Enabled = true;
                MostrarPedidos();
            }
        }
    }
}
