namespace FerreStock.Formularios
{
    partial class FrmMovimientos
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
            DataGridViewCellStyle estiloNumero = new DataGridViewCellStyle();
            dgvMovimientos = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            colCodigo = new DataGridViewTextBoxColumn();
            colProducto = new DataGridViewTextBoxColumn();
            colTipo = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colObservacion = new DataGridViewTextBoxColumn();
            grpRegistro = new GroupBox();
            lblSiguiente = new Label();
            lblPila = new Label();
            btnDeshacer = new Button();
            btnRegistrar = new Button();
            txtObservacion = new TextBox();
            lblObservacion = new Label();
            nudCantidad = new NumericUpDown();
            lblCantidad = new Label();
            cmbTipo = new ComboBox();
            lblTipo = new Label();
            lblStockActual = new Label();
            cmbProducto = new ComboBox();
            lblProducto = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).BeginInit();
            grpRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            SuspendLayout();
            //
            // dgvMovimientos
            //
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AllowUserToDeleteRows = false;
            dgvMovimientos.AllowUserToResizeRows = false;
            dgvMovimientos.BackgroundColor = SystemColors.Window;
            dgvMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMovimientos.Columns.AddRange(new DataGridViewColumn[] { colId, colFecha, colCodigo, colProducto, colTipo, colCantidad, colObservacion });
            dgvMovimientos.Dock = DockStyle.Fill;
            dgvMovimientos.Location = new Point(340, 0);
            dgvMovimientos.MultiSelect = false;
            dgvMovimientos.Name = "dgvMovimientos";
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.RowHeadersVisible = false;
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.Size = new Size(760, 520);
            dgvMovimientos.TabIndex = 1;
            //
            // colId
            //
            colId.HeaderText = "N.º";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 45;
            //
            // colFecha
            //
            colFecha.HeaderText = "Fecha";
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            colFecha.Width = 130;
            //
            // colCodigo
            //
            colCodigo.HeaderText = "Código";
            colCodigo.Name = "colCodigo";
            colCodigo.ReadOnly = true;
            colCodigo.Width = 80;
            //
            // colProducto
            //
            colProducto.HeaderText = "Producto";
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            colProducto.Width = 180;
            //
            // colTipo
            //
            colTipo.HeaderText = "Tipo";
            colTipo.Name = "colTipo";
            colTipo.ReadOnly = true;
            colTipo.Width = 65;
            //
            // colCantidad
            //
            estiloNumero.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCantidad.DefaultCellStyle = estiloNumero;
            colCantidad.HeaderText = "Cantidad";
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            colCantidad.Width = 75;
            //
            // colObservacion
            //
            colObservacion.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colObservacion.HeaderText = "Observación";
            colObservacion.MinimumWidth = 120;
            colObservacion.Name = "colObservacion";
            colObservacion.ReadOnly = true;
            //
            // grpRegistro
            //
            grpRegistro.Controls.Add(lblSiguiente);
            grpRegistro.Controls.Add(lblPila);
            grpRegistro.Controls.Add(btnDeshacer);
            grpRegistro.Controls.Add(btnRegistrar);
            grpRegistro.Controls.Add(txtObservacion);
            grpRegistro.Controls.Add(lblObservacion);
            grpRegistro.Controls.Add(nudCantidad);
            grpRegistro.Controls.Add(lblCantidad);
            grpRegistro.Controls.Add(cmbTipo);
            grpRegistro.Controls.Add(lblTipo);
            grpRegistro.Controls.Add(lblStockActual);
            grpRegistro.Controls.Add(cmbProducto);
            grpRegistro.Controls.Add(lblProducto);
            grpRegistro.Dock = DockStyle.Left;
            grpRegistro.Location = new Point(0, 0);
            grpRegistro.Name = "grpRegistro";
            grpRegistro.Size = new Size(340, 520);
            grpRegistro.TabIndex = 0;
            grpRegistro.TabStop = false;
            grpRegistro.Text = "Registrar movimiento";
            //
            // lblProducto
            //
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(15, 33);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(56, 15);
            lblProducto.TabIndex = 0;
            lblProducto.Text = "Producto";
            //
            // cmbProducto
            //
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.DropDownWidth = 360;
            cmbProducto.Location = new Point(110, 30);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(210, 23);
            cmbProducto.TabIndex = 1;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            //
            // lblStockActual
            //
            lblStockActual.AutoSize = true;
            lblStockActual.ForeColor = Color.DimGray;
            lblStockActual.Location = new Point(110, 58);
            lblStockActual.Name = "lblStockActual";
            lblStockActual.Size = new Size(85, 15);
            lblStockActual.TabIndex = 2;
            lblStockActual.Text = "Stock actual: -";
            //
            // lblTipo
            //
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(15, 89);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(31, 15);
            lblTipo.TabIndex = 3;
            lblTipo.Text = "Tipo";
            //
            // cmbTipo
            //
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Location = new Point(110, 86);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(210, 23);
            cmbTipo.TabIndex = 4;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
            //
            // lblCantidad
            //
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(15, 121);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(55, 15);
            lblCantidad.TabIndex = 5;
            lblCantidad.Text = "Cantidad";
            //
            // nudCantidad
            //
            nudCantidad.Location = new Point(110, 118);
            nudCantidad.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(210, 23);
            nudCantidad.TabIndex = 6;
            //
            // lblObservacion
            //
            lblObservacion.AutoSize = true;
            lblObservacion.Location = new Point(15, 153);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(73, 15);
            lblObservacion.TabIndex = 7;
            lblObservacion.Text = "Observación";
            //
            // txtObservacion
            //
            txtObservacion.Location = new Point(110, 150);
            txtObservacion.MaxLength = 200;
            txtObservacion.Multiline = true;
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(210, 60);
            txtObservacion.TabIndex = 8;
            //
            // btnRegistrar
            //
            btnRegistrar.Location = new Point(110, 222);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(210, 32);
            btnRegistrar.TabIndex = 9;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            //
            // btnDeshacer
            //
            btnDeshacer.Location = new Point(15, 300);
            btnDeshacer.Name = "btnDeshacer";
            btnDeshacer.Size = new Size(305, 36);
            btnDeshacer.TabIndex = 10;
            btnDeshacer.Text = "Deshacer último movimiento";
            btnDeshacer.UseVisualStyleBackColor = true;
            btnDeshacer.Click += btnDeshacer_Click;
            //
            // lblPila
            //
            lblPila.AutoSize = true;
            lblPila.Location = new Point(15, 345);
            lblPila.Name = "lblPila";
            lblPila.Size = new Size(160, 15);
            lblPila.TabIndex = 11;
            lblPila.Text = "Movimientos para deshacer: 0";
            //
            // lblSiguiente
            //
            lblSiguiente.ForeColor = Color.DimGray;
            lblSiguiente.Location = new Point(15, 368);
            lblSiguiente.Name = "lblSiguiente";
            lblSiguiente.Size = new Size(305, 40);
            lblSiguiente.TabIndex = 12;
            //
            // FrmMovimientos
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 520);
            Controls.Add(dgvMovimientos);
            Controls.Add(grpRegistro);
            MinimumSize = new Size(900, 450);
            Name = "FrmMovimientos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Movimientos de inventario";
            Load += FrmMovimientos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).EndInit();
            grpRegistro.ResumeLayout(false);
            grpRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvMovimientos;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colCodigo;
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colObservacion;
        private GroupBox grpRegistro;
        private Label lblSiguiente;
        private Label lblPila;
        private Button btnDeshacer;
        private Button btnRegistrar;
        private TextBox txtObservacion;
        private Label lblObservacion;
        private NumericUpDown nudCantidad;
        private Label lblCantidad;
        private ComboBox cmbTipo;
        private Label lblTipo;
        private Label lblStockActual;
        private ComboBox cmbProducto;
        private Label lblProducto;
    }
}
