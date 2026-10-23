using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Formularios
{
    /// <summary>
    /// CRUD de productos. A la izquierda el árbol de categorías filtra la tabla,
    /// arriba se busca por código o nombre, y a la derecha se editan los datos.
    /// Este formulario solo muestra y captura datos: las reglas están en InventarioServicio.
    /// </summary>
    public partial class FrmProductos : Form
    {
        private readonly InventarioServicio inventario;
        private readonly PedidoServicio pedidos;

        // true cuando el panel de la derecha está creando un producto nuevo;
        // false cuando está editando uno que el usuario eligió en la tabla.
        private bool esNuevo = true;

        public FrmProductos(InventarioServicio inventario, PedidoServicio pedidos)
        {
            InitializeComponent();
            this.inventario = inventario;
            this.pedidos = pedidos;
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarArbolCategorias();
            CargarListasDesplegables();
            LimpiarFormulario();
            MostrarProductos();
        }

        // ---------------------------------------------------------------
        // Carga de controles
        // ---------------------------------------------------------------

        /// <summary>
        /// Arma el árbol de dos niveles: categorías principales y, dentro, sus subcategorías.
        /// En el Tag de cada nodo se guarda el Id de la categoría (null en la raíz = todas).
        /// </summary>
        private void CargarArbolCategorias()
        {
            tvCategorias.Nodes.Clear();
            TreeNode raiz = new TreeNode("Todas las categorías");
            raiz.Tag = null;

            foreach (Categoria principal in inventario.ObtenerCategoriasPrincipales())
            {
                TreeNode nodoPrincipal = new TreeNode(principal.Nombre);
                nodoPrincipal.Tag = principal.Id;

                foreach (Categoria sub in inventario.ObtenerSubcategorias(principal.Id))
                {
                    TreeNode nodoSub = new TreeNode(sub.Nombre);
                    nodoSub.Tag = sub.Id;
                    nodoPrincipal.Nodes.Add(nodoSub);
                }
                raiz.Nodes.Add(nodoPrincipal);
            }

            tvCategorias.Nodes.Add(raiz);
            raiz.Expand();
            tvCategorias.SelectedNode = raiz;
        }

        private void CargarListasDesplegables()
        {
            // Se agregan los objetos Categoria completos; DisplayMember indica qué propiedad se ve.
            cmbCategoria.Items.Clear();
            cmbCategoria.DisplayMember = "Nombre";
            foreach (Categoria c in inventario.ObtenerTodasLasSubcategorias())
            {
                cmbCategoria.Items.Add(c);
            }

            cmbUnidad.Items.Clear();
            cmbUnidad.Items.AddRange(InventarioServicio.Unidades);

            // La primera opción vacía significa "sin ubicación".
            cmbUbicacion.Items.Clear();
            cmbUbicacion.Items.Add("");
            foreach (Ubicacion u in inventario.ObtenerUbicaciones())
            {
                cmbUbicacion.Items.Add(u.Id);
            }
        }

        /// <summary>Llena la tabla con los productos que cumplen la búsqueda y la categoría elegida.</summary>
        private void MostrarProductos()
        {
            int? categoriaId = null;
            if (tvCategorias.SelectedNode != null && tvCategorias.SelectedNode.Tag != null)
            {
                categoriaId = (int)tvCategorias.SelectedNode.Tag;
            }

            List<Producto> lista = inventario.Filtrar(txtBuscar.Text, categoriaId);

            dgvProductos.Rows.Clear();
            foreach (Producto p in lista)
            {
                int fila = dgvProductos.Rows.Add(p.Codigo, p.Nombre, inventario.ObtenerNombreCategoria(p.CategoriaId),
                    p.Unidad, p.PrecioVenta, p.Stock, p.StockMinimo, p.UbicacionId);

                // Los productos por debajo del stock mínimo se resaltan para verlos de un vistazo.
                if (p.Stock < p.StockMinimo)
                {
                    dgvProductos.Rows[fila].DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }

            // Al llenar la tabla la primera fila queda seleccionada; se quita para no confundir.
            dgvProductos.ClearSelection();
            lblTotal.Text = lista.Count + " productos";
        }

        // ---------------------------------------------------------------
        // Pasar datos entre el formulario y el objeto Producto
        // ---------------------------------------------------------------

        private void CargarEnFormulario(Producto p)
        {
            txtCodigo.Text = p.Codigo;
            txtNombre.Text = p.Nombre;
            SeleccionarCategoria(p.CategoriaId);
            cmbUnidad.SelectedItem = p.Unidad;
            PonerValor(nudPrecioCompra, p.PrecioCompra);
            PonerValor(nudPrecioVenta, p.PrecioVenta);
            PonerValor(nudStock, p.Stock);
            PonerValor(nudStockMinimo, p.StockMinimo);
            PonerValor(nudPeso, (decimal)p.PesoKg);
            PonerValor(nudVolumen, (decimal)p.VolumenUnitarioM3);
            cmbUbicacion.SelectedItem = p.UbicacionId;
            PonerValor(nudVentasMes, p.VentasMes);

            // El código no se puede cambiar al editar porque identifica al producto.
            esNuevo = false;
            txtCodigo.ReadOnly = true;
            btnEliminar.Enabled = true;
            lblModo.Text = "Editando " + p.Codigo;
        }

        /// <summary>Crea un Producto con lo que el usuario escribió. No valida: eso lo hace el servicio.</summary>
        private Producto LeerFormulario()
        {
            Producto p = new Producto();
            p.Codigo = txtCodigo.Text;
            p.Nombre = txtNombre.Text;

            // Si no eligió categoría queda en 0 y el servicio muestra el error.
            Categoria? categoria = cmbCategoria.SelectedItem as Categoria;
            if (categoria != null)
            {
                p.CategoriaId = categoria.Id;
            }

            p.Unidad = cmbUnidad.Text;
            p.PrecioCompra = nudPrecioCompra.Value;
            p.PrecioVenta = nudPrecioVenta.Value;
            p.Stock = (int)nudStock.Value;
            p.StockMinimo = (int)nudStockMinimo.Value;
            p.PesoKg = (double)nudPeso.Value;
            p.VolumenUnitarioM3 = (double)nudVolumen.Value;
            p.UbicacionId = cmbUbicacion.Text;
            p.VentasMes = (int)nudVentasMes.Value;
            return p;
        }

        /// <summary>Deja el panel listo para crear un producto nuevo.</summary>
        private void LimpiarFormulario()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            cmbCategoria.SelectedIndex = -1;
            cmbUnidad.SelectedIndex = 0;
            nudPrecioCompra.Value = 0;
            nudPrecioVenta.Value = 0;
            nudStock.Value = 0;
            nudStockMinimo.Value = 0;
            nudPeso.Value = 0;
            nudVolumen.Value = 0;
            cmbUbicacion.SelectedIndex = 0;
            nudVentasMes.Value = 0;

            esNuevo = true;
            txtCodigo.ReadOnly = false;
            btnEliminar.Enabled = false;
            lblModo.Text = "Nuevo producto";
            dgvProductos.ClearSelection();
            txtCodigo.Focus();
        }

        private void SeleccionarCategoria(int categoriaId)
        {
            cmbCategoria.SelectedIndex = -1;
            for (int i = 0; i < cmbCategoria.Items.Count; i++)
            {
                Categoria c = (Categoria)cmbCategoria.Items[i]!;
                if (c.Id == categoriaId)
                {
                    cmbCategoria.SelectedIndex = i;
                    return;
                }
            }
        }

        // Un NumericUpDown lanza error si se le asigna un valor fuera de su rango
        // (por ejemplo, si alguien escribió un número negativo en el JSON). Aquí se ajusta al límite.
        private static void PonerValor(NumericUpDown control, decimal valor)
        {
            if (valor < control.Minimum)
            {
                valor = control.Minimum;
            }
            if (valor > control.Maximum)
            {
                valor = control.Maximum;
            }
            control.Value = valor;
        }

        // ---------------------------------------------------------------
        // Eventos
        // ---------------------------------------------------------------

        private void tvCategorias_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MostrarProductos();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            MostrarProductos();
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            // Volver a la raíz del árbol muestra todas las categorías.
            tvCategorias.SelectedNode = tvCategorias.Nodes[0];
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // RowIndex es -1 cuando se hace clic en el encabezado de una columna.
            if (e.RowIndex < 0)
            {
                return;
            }
            string codigo = dgvProductos.Rows[e.RowIndex].Cells[colCodigo.Index].Value?.ToString() ?? "";
            Producto? producto = inventario.BuscarPorCodigo(codigo);
            if (producto != null)
            {
                CargarEnFormulario(producto);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // Los manejadores de eventos con await se declaran "async void": es la única excepción
        // a la regla de no usar async void, porque Windows Forms no espera un Task.
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // Se desactiva el botón para evitar un doble clic mientras se guarda.
            btnGuardar.Enabled = false;
            try
            {
                Producto producto = LeerFormulario();
                if (esNuevo)
                {
                    await inventario.AgregarAsync(producto);
                    MessageBox.Show("Producto " + producto.Codigo + " agregado.", "Productos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await inventario.EditarAsync(producto);
                    MessageBox.Show("Cambios de " + producto.Codigo + " guardados.", "Productos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MostrarProductos();
                LimpiarFormulario();
            }
            catch (ArgumentException ex)
            {
                // Error de validación: el mensaje ya viene escrito para el usuario.
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Cualquier otro error (por ejemplo, no se pudo escribir el archivo).
                MessageBox.Show("No se pudo guardar el producto.\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            try
            {
                // Un producto que está en un pedido sin despachar no se puede eliminar.
                pedidos.ValidarEliminacionProducto(codigo);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "No se puede eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Eliminar el producto " + codigo + "?\nEsta acción no se puede deshacer.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                await inventario.EliminarAsync(codigo);
                MostrarProductos();
                LimpiarFormulario();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el producto.\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO estudiante: agregar un botón "Exportar" que guarde la tabla visible en un archivo CSV.
    }
}
