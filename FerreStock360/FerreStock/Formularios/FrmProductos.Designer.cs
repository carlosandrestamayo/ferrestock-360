namespace FerreStock.Formularios
{
    partial class FrmProductos
    {
        /// <summary>
        ///  Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Libera los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        ///  Método necesario para admitir el Diseñador. No se puede modificar
        ///  el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle estiloPrecio = new DataGridViewCellStyle();
            DataGridViewCellStyle estiloNumero = new DataGridViewCellStyle();
            tvCategorias = new TreeView();
            pnlCentro = new Panel();
            dgvProductos = new DataGridView();
            colCodigo = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colCategoria = new DataGridViewTextBoxColumn();
            colUnidad = new DataGridViewTextBoxColumn();
            colPrecioVenta = new DataGridViewTextBoxColumn();
            colStock = new DataGridViewTextBoxColumn();
            colStockMinimo = new DataGridViewTextBoxColumn();
            colUbicacion = new DataGridViewTextBoxColumn();
            pnlBusqueda = new Panel();
            lblTotal = new Label();
            btnLimpiarBusqueda = new Button();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            grpDatos = new GroupBox();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNuevo = new Button();
            lblModo = new Label();
            nudVentasMes = new NumericUpDown();
            lblVentasMes = new Label();
            cmbUbicacion = new ComboBox();
            lblUbicacion = new Label();
            nudVolumen = new NumericUpDown();
            lblVolumen = new Label();
            nudPeso = new NumericUpDown();
            lblPeso = new Label();
            nudStockMinimo = new NumericUpDown();
            lblStockMinimo = new Label();
            nudStock = new NumericUpDown();
            lblStock = new Label();
            nudPrecioVenta = new NumericUpDown();
            lblPrecioVenta = new Label();
            nudPrecioCompra = new NumericUpDown();
            lblPrecioCompra = new Label();
            cmbUnidad = new ComboBox();
            lblUnidad = new Label();
            cmbCategoria = new ComboBox();
            lblCategoria = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            txtCodigo = new TextBox();
            lblCodigo = new Label();
            pnlCentro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            pnlBusqueda.SuspendLayout();
            grpDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudVentasMes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVolumen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPeso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecioVenta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecioCompra).BeginInit();
            SuspendLayout();
            //
            // tvCategorias
            //
            tvCategorias.Dock = DockStyle.Left;
            tvCategorias.HideSelection = false;
            tvCategorias.Location = new Point(0, 0);
            tvCategorias.Name = "tvCategorias";
            tvCategorias.Size = new Size(220, 600);
            tvCategorias.TabIndex = 0;
            tvCategorias.AfterSelect += tvCategorias_AfterSelect;
            //
            // pnlCentro
            //
            pnlCentro.Controls.Add(dgvProductos);
            pnlCentro.Controls.Add(pnlBusqueda);
            pnlCentro.Dock = DockStyle.Fill;
            pnlCentro.Location = new Point(220, 0);
            pnlCentro.Name = "pnlCentro";
            pnlCentro.Size = new Size(620, 600);
            pnlCentro.TabIndex = 1;
            //
            // dgvProductos
            //
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.BackgroundColor = SystemColors.Window;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Columns.AddRange(new DataGridViewColumn[] { colCodigo, colNombre, colCategoria, colUnidad, colPrecioVenta, colStock, colStockMinimo, colUbicacion });
            dgvProductos.Dock = DockStyle.Fill;
            dgvProductos.Location = new Point(0, 44);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(620, 556);
            dgvProductos.TabIndex = 1;
            dgvProductos.CellClick += dgvProductos_CellClick;
            //
            // colCodigo
            //
            colCodigo.HeaderText = "Código";
            colCodigo.Name = "colCodigo";
            colCodigo.ReadOnly = true;
            colCodigo.Width = 75;
            //
            // colNombre
            //
            colNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 150;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            //
            // colCategoria
            //
            colCategoria.HeaderText = "Categoría";
            colCategoria.Name = "colCategoria";
            colCategoria.ReadOnly = true;
            colCategoria.Width = 130;
            //
            // colUnidad
            //
            colUnidad.HeaderText = "Unidad";
            colUnidad.Name = "colUnidad";
            colUnidad.ReadOnly = true;
            colUnidad.Width = 60;
            //
            // colPrecioVenta
            //
            estiloPrecio.Alignment = DataGridViewContentAlignment.MiddleRight;
            estiloPrecio.Format = "N0";
            colPrecioVenta.DefaultCellStyle = estiloPrecio;
            colPrecioVenta.HeaderText = "Precio venta";
            colPrecioVenta.Name = "colPrecioVenta";
            colPrecioVenta.ReadOnly = true;
            colPrecioVenta.Width = 85;
            //
            // colStock
            //
            estiloNumero.Alignment = DataGridViewContentAlignment.MiddleRight;
            colStock.DefaultCellStyle = estiloNumero;
            colStock.HeaderText = "Stock";
            colStock.Name = "colStock";
            colStock.ReadOnly = true;
            colStock.Width = 55;
            //
            // colStockMinimo
            //
            colStockMinimo.DefaultCellStyle = estiloNumero;
            colStockMinimo.HeaderText = "Mínimo";
            colStockMinimo.Name = "colStockMinimo";
            colStockMinimo.ReadOnly = true;
            colStockMinimo.Width = 60;
            //
            // colUbicacion
            //
            colUbicacion.HeaderText = "Ubicación";
            colUbicacion.Name = "colUbicacion";
            colUbicacion.ReadOnly = true;
            colUbicacion.Width = 75;
            //
            // pnlBusqueda
            //
            pnlBusqueda.Controls.Add(lblTotal);
            pnlBusqueda.Controls.Add(btnLimpiarBusqueda);
            pnlBusqueda.Controls.Add(txtBuscar);
            pnlBusqueda.Controls.Add(lblBuscar);
            pnlBusqueda.Dock = DockStyle.Top;
            pnlBusqueda.Location = new Point(0, 0);
            pnlBusqueda.Name = "pnlBusqueda";
            pnlBusqueda.Size = new Size(620, 44);
            pnlBusqueda.TabIndex = 0;
            //
            // lblTotal
            //
            lblTotal.AutoSize = true;
            lblTotal.ForeColor = Color.DimGray;
            lblTotal.Location = new Point(440, 14);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(70, 15);
            lblTotal.TabIndex = 3;
            lblTotal.Text = "0 productos";
            //
            // btnLimpiarBusqueda
            //
            btnLimpiarBusqueda.Location = new Point(340, 9);
            btnLimpiarBusqueda.Name = "btnLimpiarBusqueda";
            btnLimpiarBusqueda.Size = new Size(85, 25);
            btnLimpiarBusqueda.TabIndex = 2;
            btnLimpiarBusqueda.Text = "Ver todos";
            btnLimpiarBusqueda.UseVisualStyleBackColor = true;
            btnLimpiarBusqueda.Click += btnLimpiarBusqueda_Click;
            //
            // txtBuscar
            //
            txtBuscar.Location = new Point(70, 10);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Código o nombre";
            txtBuscar.Size = new Size(260, 23);
            txtBuscar.TabIndex = 1;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            //
            // lblBuscar
            //
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(12, 14);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(45, 15);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar:";
            //
            // grpDatos
            //
            grpDatos.Controls.Add(btnEliminar);
            grpDatos.Controls.Add(btnGuardar);
            grpDatos.Controls.Add(btnNuevo);
            grpDatos.Controls.Add(lblModo);
            grpDatos.Controls.Add(nudVentasMes);
            grpDatos.Controls.Add(lblVentasMes);
            grpDatos.Controls.Add(cmbUbicacion);
            grpDatos.Controls.Add(lblUbicacion);
            grpDatos.Controls.Add(nudVolumen);
            grpDatos.Controls.Add(lblVolumen);
            grpDatos.Controls.Add(nudPeso);
            grpDatos.Controls.Add(lblPeso);
            grpDatos.Controls.Add(nudStockMinimo);
            grpDatos.Controls.Add(lblStockMinimo);
            grpDatos.Controls.Add(nudStock);
            grpDatos.Controls.Add(lblStock);
            grpDatos.Controls.Add(nudPrecioVenta);
            grpDatos.Controls.Add(lblPrecioVenta);
            grpDatos.Controls.Add(nudPrecioCompra);
            grpDatos.Controls.Add(lblPrecioCompra);
            grpDatos.Controls.Add(cmbUnidad);
            grpDatos.Controls.Add(lblUnidad);
            grpDatos.Controls.Add(cmbCategoria);
            grpDatos.Controls.Add(lblCategoria);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtCodigo);
            grpDatos.Controls.Add(lblCodigo);
            grpDatos.Dock = DockStyle.Right;
            grpDatos.Location = new Point(840, 0);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(340, 600);
            grpDatos.TabIndex = 2;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos del producto";
            //
            // lblCodigo
            //
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(15, 33);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(46, 15);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código";
            //
            // txtCodigo
            //
            txtCodigo.CharacterCasing = CharacterCasing.Upper;
            txtCodigo.Location = new Point(130, 30);
            txtCodigo.MaxLength = 20;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(190, 23);
            txtCodigo.TabIndex = 1;
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(15, 65);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(130, 62);
            txtNombre.MaxLength = 80;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(190, 23);
            txtNombre.TabIndex = 3;
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(15, 97);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(58, 15);
            lblCategoria.TabIndex = 4;
            lblCategoria.Text = "Categoría";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(130, 94);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(190, 23);
            cmbCategoria.TabIndex = 5;
            //
            // lblUnidad
            //
            lblUnidad.AutoSize = true;
            lblUnidad.Location = new Point(15, 129);
            lblUnidad.Name = "lblUnidad";
            lblUnidad.Size = new Size(45, 15);
            lblUnidad.TabIndex = 6;
            lblUnidad.Text = "Unidad";
            //
            // cmbUnidad
            //
            cmbUnidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnidad.Location = new Point(130, 126);
            cmbUnidad.Name = "cmbUnidad";
            cmbUnidad.Size = new Size(190, 23);
            cmbUnidad.TabIndex = 7;
            //
            // lblPrecioCompra
            //
            lblPrecioCompra.AutoSize = true;
            lblPrecioCompra.Location = new Point(15, 161);
            lblPrecioCompra.Name = "lblPrecioCompra";
            lblPrecioCompra.Size = new Size(85, 15);
            lblPrecioCompra.TabIndex = 8;
            lblPrecioCompra.Text = "Precio compra";
            //
            // nudPrecioCompra
            //
            nudPrecioCompra.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudPrecioCompra.Location = new Point(130, 158);
            nudPrecioCompra.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudPrecioCompra.Name = "nudPrecioCompra";
            nudPrecioCompra.Size = new Size(190, 23);
            nudPrecioCompra.TabIndex = 9;
            nudPrecioCompra.ThousandsSeparator = true;
            //
            // lblPrecioVenta
            //
            lblPrecioVenta.AutoSize = true;
            lblPrecioVenta.Location = new Point(15, 193);
            lblPrecioVenta.Name = "lblPrecioVenta";
            lblPrecioVenta.Size = new Size(74, 15);
            lblPrecioVenta.TabIndex = 10;
            lblPrecioVenta.Text = "Precio venta";
            //
            // nudPrecioVenta
            //
            nudPrecioVenta.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudPrecioVenta.Location = new Point(130, 190);
            nudPrecioVenta.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudPrecioVenta.Name = "nudPrecioVenta";
            nudPrecioVenta.Size = new Size(190, 23);
            nudPrecioVenta.TabIndex = 11;
            nudPrecioVenta.ThousandsSeparator = true;
            //
            // lblStock
            //
            lblStock.AutoSize = true;
            lblStock.Location = new Point(15, 225);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(36, 15);
            lblStock.TabIndex = 12;
            lblStock.Text = "Stock inicial";
            //
            // nudStock
            //
            nudStock.Location = new Point(130, 222);
            nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(190, 23);
            nudStock.TabIndex = 13;
            //
            // lblStockMinimo
            //
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Location = new Point(15, 257);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(81, 15);
            lblStockMinimo.TabIndex = 14;
            lblStockMinimo.Text = "Stock mínimo";
            //
            // nudStockMinimo
            //
            nudStockMinimo.Location = new Point(130, 254);
            nudStockMinimo.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudStockMinimo.Name = "nudStockMinimo";
            nudStockMinimo.Size = new Size(190, 23);
            nudStockMinimo.TabIndex = 15;
            //
            // lblPeso
            //
            lblPeso.AutoSize = true;
            lblPeso.Location = new Point(15, 289);
            lblPeso.Name = "lblPeso";
            lblPeso.Size = new Size(55, 15);
            lblPeso.TabIndex = 16;
            lblPeso.Text = "Peso (kg)";
            //
            // nudPeso
            //
            nudPeso.DecimalPlaces = 2;
            nudPeso.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudPeso.Location = new Point(130, 286);
            nudPeso.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudPeso.Name = "nudPeso";
            nudPeso.Size = new Size(190, 23);
            nudPeso.TabIndex = 17;
            //
            // lblVolumen
            //
            lblVolumen.AutoSize = true;
            lblVolumen.Location = new Point(15, 321);
            lblVolumen.Name = "lblVolumen";
            lblVolumen.Size = new Size(84, 15);
            lblVolumen.TabIndex = 18;
            lblVolumen.Text = "Volumen (m³)";
            //
            // nudVolumen
            //
            nudVolumen.DecimalPlaces = 5;
            nudVolumen.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            nudVolumen.Location = new Point(130, 318);
            nudVolumen.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudVolumen.Name = "nudVolumen";
            nudVolumen.Size = new Size(190, 23);
            nudVolumen.TabIndex = 19;
            //
            // lblUbicacion
            //
            lblUbicacion.AutoSize = true;
            lblUbicacion.Location = new Point(15, 353);
            lblUbicacion.Name = "lblUbicacion";
            lblUbicacion.Size = new Size(60, 15);
            lblUbicacion.TabIndex = 20;
            lblUbicacion.Text = "Ubicación";
            //
            // cmbUbicacion
            //
            cmbUbicacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUbicacion.Location = new Point(130, 350);
            cmbUbicacion.Name = "cmbUbicacion";
            cmbUbicacion.Size = new Size(190, 23);
            cmbUbicacion.TabIndex = 21;
            //
            // lblVentasMes
            //
            lblVentasMes.AutoSize = true;
            lblVentasMes.Location = new Point(15, 385);
            lblVentasMes.Name = "lblVentasMes";
            lblVentasMes.Size = new Size(87, 15);
            lblVentasMes.TabIndex = 22;
            lblVentasMes.Text = "Ventas del mes";
            //
            // nudVentasMes
            //
            nudVentasMes.Location = new Point(130, 382);
            nudVentasMes.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudVentasMes.Name = "nudVentasMes";
            nudVentasMes.Size = new Size(190, 23);
            nudVentasMes.TabIndex = 23;
            //
            // lblModo
            //
            lblModo.AutoSize = true;
            lblModo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblModo.ForeColor = Color.FromArgb(192, 64, 0);
            lblModo.Location = new Point(15, 420);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(98, 15);
            lblModo.TabIndex = 24;
            lblModo.Text = "Nuevo producto";
            //
            // btnNuevo
            //
            btnNuevo.Location = new Point(15, 450);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(95, 32);
            btnNuevo.TabIndex = 25;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            //
            // btnGuardar
            //
            btnGuardar.Location = new Point(120, 450);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 32);
            btnGuardar.TabIndex = 26;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            //
            // btnEliminar
            //
            btnEliminar.Location = new Point(225, 450);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(95, 32);
            btnEliminar.TabIndex = 27;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            //
            // FrmProductos
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 600);
            Controls.Add(pnlCentro);
            Controls.Add(tvCategorias);
            Controls.Add(grpDatos);
            MinimumSize = new Size(1000, 560);
            Name = "FrmProductos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Productos";
            Load += FrmProductos_Load;
            pnlCentro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            pnlBusqueda.ResumeLayout(false);
            pnlBusqueda.PerformLayout();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudVentasMes).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVolumen).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPeso).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecioVenta).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecioCompra).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TreeView tvCategorias;
        private Panel pnlCentro;
        private DataGridView dgvProductos;
        private DataGridViewTextBoxColumn colCodigo;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colCategoria;
        private DataGridViewTextBoxColumn colUnidad;
        private DataGridViewTextBoxColumn colPrecioVenta;
        private DataGridViewTextBoxColumn colStock;
        private DataGridViewTextBoxColumn colStockMinimo;
        private DataGridViewTextBoxColumn colUbicacion;
        private Panel pnlBusqueda;
        private Label lblTotal;
        private Button btnLimpiarBusqueda;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private GroupBox grpDatos;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNuevo;
        private Label lblModo;
        private NumericUpDown nudVentasMes;
        private Label lblVentasMes;
        private ComboBox cmbUbicacion;
        private Label lblUbicacion;
        private NumericUpDown nudVolumen;
        private Label lblVolumen;
        private NumericUpDown nudPeso;
        private Label lblPeso;
        private NumericUpDown nudStockMinimo;
        private Label lblStockMinimo;
        private NumericUpDown nudStock;
        private Label lblStock;
        private NumericUpDown nudPrecioVenta;
        private Label lblPrecioVenta;
        private NumericUpDown nudPrecioCompra;
        private Label lblPrecioCompra;
        private ComboBox cmbUnidad;
        private Label lblUnidad;
        private ComboBox cmbCategoria;
        private Label lblCategoria;
        private TextBox txtNombre;
        private Label lblNombre;
        private TextBox txtCodigo;
        private Label lblCodigo;
    }
}
