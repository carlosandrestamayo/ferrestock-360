namespace FerreStock.Formularios
{
    partial class FrmPedidos
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
            grpNuevo = new GroupBox();
            btnCrearPedido = new Button();
            btnQuitarItem = new Button();
            dgvItems = new DataGridView();
            colItemCodigo = new DataGridViewTextBoxColumn();
            colItemProducto = new DataGridViewTextBoxColumn();
            colItemCantidad = new DataGridViewTextBoxColumn();
            btnAgregarItem = new Button();
            nudCantidad = new NumericUpDown();
            lblCantidad = new Label();
            cmbProducto = new ComboBox();
            lblProducto = new Label();
            cmbPrioridad = new ComboBox();
            lblPrioridad = new Label();
            txtCliente = new TextBox();
            lblCliente = new Label();
            grpCola = new GroupBox();
            dgvCola = new DataGridView();
            colTurno = new DataGridViewTextBoxColumn();
            colColaId = new DataGridViewTextBoxColumn();
            colColaCliente = new DataGridViewTextBoxColumn();
            colColaPrioridad = new DataGridViewTextBoxColumn();
            colColaFecha = new DataGridViewTextBoxColumn();
            colColaItems = new DataGridViewTextBoxColumn();
            pnlCola = new Panel();
            lblSiguiente = new Label();
            btnAtenderSiguiente = new Button();
            grpAlistados = new GroupBox();
            dgvAlistados = new DataGridView();
            colAlistadoId = new DataGridViewTextBoxColumn();
            colAlistadoCliente = new DataGridViewTextBoxColumn();
            colAlistadoPrioridad = new DataGridViewTextBoxColumn();
            colAlistadoFecha = new DataGridViewTextBoxColumn();
            colAlistadoItems = new DataGridViewTextBoxColumn();
            pnlAlistados = new Panel();
            lblDespachados = new Label();
            btnDespachar = new Button();
            pnlDerecha = new Panel();
            grpNuevo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            grpCola.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCola).BeginInit();
            pnlCola.SuspendLayout();
            grpAlistados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlistados).BeginInit();
            pnlAlistados.SuspendLayout();
            pnlDerecha.SuspendLayout();
            SuspendLayout();
            //
            // grpNuevo
            //
            grpNuevo.Controls.Add(btnCrearPedido);
            grpNuevo.Controls.Add(btnQuitarItem);
            grpNuevo.Controls.Add(dgvItems);
            grpNuevo.Controls.Add(btnAgregarItem);
            grpNuevo.Controls.Add(nudCantidad);
            grpNuevo.Controls.Add(lblCantidad);
            grpNuevo.Controls.Add(cmbProducto);
            grpNuevo.Controls.Add(lblProducto);
            grpNuevo.Controls.Add(cmbPrioridad);
            grpNuevo.Controls.Add(lblPrioridad);
            grpNuevo.Controls.Add(txtCliente);
            grpNuevo.Controls.Add(lblCliente);
            grpNuevo.Dock = DockStyle.Left;
            grpNuevo.Location = new Point(0, 0);
            grpNuevo.Name = "grpNuevo";
            grpNuevo.Size = new Size(380, 620);
            grpNuevo.TabIndex = 0;
            grpNuevo.TabStop = false;
            grpNuevo.Text = "Nuevo pedido";
            //
            // lblCliente
            //
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(15, 33);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(44, 15);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Cliente";
            //
            // txtCliente
            //
            txtCliente.Location = new Point(100, 30);
            txtCliente.MaxLength = 80;
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(260, 23);
            txtCliente.TabIndex = 1;
            //
            // lblPrioridad
            //
            lblPrioridad.AutoSize = true;
            lblPrioridad.Location = new Point(15, 65);
            lblPrioridad.Name = "lblPrioridad";
            lblPrioridad.Size = new Size(55, 15);
            lblPrioridad.TabIndex = 2;
            lblPrioridad.Text = "Prioridad";
            //
            // cmbPrioridad
            //
            cmbPrioridad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrioridad.Location = new Point(100, 62);
            cmbPrioridad.Name = "cmbPrioridad";
            cmbPrioridad.Size = new Size(260, 23);
            cmbPrioridad.TabIndex = 3;
            //
            // lblProducto
            //
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(15, 110);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(56, 15);
            lblProducto.TabIndex = 4;
            lblProducto.Text = "Producto";
            //
            // cmbProducto
            //
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.DropDownWidth = 360;
            cmbProducto.Location = new Point(100, 107);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(260, 23);
            cmbProducto.TabIndex = 5;
            //
            // lblCantidad
            //
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(15, 142);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(55, 15);
            lblCantidad.TabIndex = 6;
            lblCantidad.Text = "Cantidad";
            //
            // nudCantidad
            //
            nudCantidad.Location = new Point(100, 139);
            nudCantidad.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(120, 23);
            nudCantidad.TabIndex = 7;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnAgregarItem
            //
            btnAgregarItem.Location = new Point(230, 137);
            btnAgregarItem.Name = "btnAgregarItem";
            btnAgregarItem.Size = new Size(130, 27);
            btnAgregarItem.TabIndex = 8;
            btnAgregarItem.Text = "Agregar ítem";
            btnAgregarItem.UseVisualStyleBackColor = true;
            btnAgregarItem.Click += btnAgregarItem_Click;
            //
            // dgvItems
            //
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.BackgroundColor = SystemColors.Window;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { colItemCodigo, colItemProducto, colItemCantidad });
            dgvItems.Location = new Point(15, 178);
            dgvItems.MultiSelect = false;
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.Size = new Size(345, 260);
            dgvItems.TabIndex = 9;
            //
            // colItemCodigo
            //
            colItemCodigo.HeaderText = "Código";
            colItemCodigo.Name = "colItemCodigo";
            colItemCodigo.ReadOnly = true;
            colItemCodigo.Width = 75;
            //
            // colItemProducto
            //
            colItemProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colItemProducto.HeaderText = "Producto";
            colItemProducto.Name = "colItemProducto";
            colItemProducto.ReadOnly = true;
            //
            // colItemCantidad
            //
            estiloNumero.Alignment = DataGridViewContentAlignment.MiddleRight;
            colItemCantidad.DefaultCellStyle = estiloNumero;
            colItemCantidad.HeaderText = "Cantidad";
            colItemCantidad.Name = "colItemCantidad";
            colItemCantidad.ReadOnly = true;
            colItemCantidad.Width = 70;
            //
            // btnQuitarItem
            //
            btnQuitarItem.Location = new Point(15, 448);
            btnQuitarItem.Name = "btnQuitarItem";
            btnQuitarItem.Size = new Size(120, 32);
            btnQuitarItem.TabIndex = 10;
            btnQuitarItem.Text = "Quitar ítem";
            btnQuitarItem.UseVisualStyleBackColor = true;
            btnQuitarItem.Click += btnQuitarItem_Click;
            //
            // btnCrearPedido
            //
            btnCrearPedido.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCrearPedido.Location = new Point(200, 448);
            btnCrearPedido.Name = "btnCrearPedido";
            btnCrearPedido.Size = new Size(160, 32);
            btnCrearPedido.TabIndex = 11;
            btnCrearPedido.Text = "Crear pedido";
            btnCrearPedido.UseVisualStyleBackColor = true;
            btnCrearPedido.Click += btnCrearPedido_Click;
            //
            // pnlDerecha
            //
            pnlDerecha.Controls.Add(grpAlistados);
            pnlDerecha.Controls.Add(grpCola);
            pnlDerecha.Dock = DockStyle.Fill;
            pnlDerecha.Location = new Point(380, 0);
            pnlDerecha.Name = "pnlDerecha";
            pnlDerecha.Size = new Size(800, 620);
            pnlDerecha.TabIndex = 1;
            //
            // grpCola
            //
            grpCola.Controls.Add(dgvCola);
            grpCola.Controls.Add(pnlCola);
            grpCola.Dock = DockStyle.Top;
            grpCola.Location = new Point(0, 0);
            grpCola.Name = "grpCola";
            grpCola.Size = new Size(800, 320);
            grpCola.TabIndex = 0;
            grpCola.TabStop = false;
            grpCola.Text = "Cola de atención (pendientes, en orden de prioridad)";
            //
            // dgvCola
            //
            dgvCola.AllowUserToAddRows = false;
            dgvCola.AllowUserToDeleteRows = false;
            dgvCola.AllowUserToResizeRows = false;
            dgvCola.BackgroundColor = SystemColors.Window;
            dgvCola.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCola.Columns.AddRange(new DataGridViewColumn[] { colTurno, colColaId, colColaCliente, colColaPrioridad, colColaFecha, colColaItems });
            dgvCola.Dock = DockStyle.Fill;
            dgvCola.Location = new Point(3, 19);
            dgvCola.MultiSelect = false;
            dgvCola.Name = "dgvCola";
            dgvCola.ReadOnly = true;
            dgvCola.RowHeadersVisible = false;
            dgvCola.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCola.Size = new Size(794, 253);
            dgvCola.TabIndex = 0;
            //
            // colTurno
            //
            colTurno.HeaderText = "Turno";
            colTurno.Name = "colTurno";
            colTurno.ReadOnly = true;
            colTurno.Width = 50;
            //
            // colColaId
            //
            colColaId.HeaderText = "Pedido";
            colColaId.Name = "colColaId";
            colColaId.ReadOnly = true;
            colColaId.Width = 62;
            //
            // colColaCliente
            //
            colColaCliente.HeaderText = "Cliente";
            colColaCliente.Name = "colColaCliente";
            colColaCliente.ReadOnly = true;
            colColaCliente.Width = 160;
            //
            // colColaPrioridad
            //
            colColaPrioridad.HeaderText = "Prioridad";
            colColaPrioridad.Name = "colColaPrioridad";
            colColaPrioridad.ReadOnly = true;
            colColaPrioridad.Width = 80;
            //
            // colColaFecha
            //
            colColaFecha.HeaderText = "Fecha";
            colColaFecha.Name = "colColaFecha";
            colColaFecha.ReadOnly = true;
            colColaFecha.Width = 125;
            //
            // colColaItems
            //
            colColaItems.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colColaItems.HeaderText = "Ítems";
            colColaItems.MinimumWidth = 120;
            colColaItems.Name = "colColaItems";
            colColaItems.ReadOnly = true;
            //
            // pnlCola
            //
            pnlCola.Controls.Add(lblSiguiente);
            pnlCola.Controls.Add(btnAtenderSiguiente);
            pnlCola.Dock = DockStyle.Bottom;
            pnlCola.Location = new Point(3, 272);
            pnlCola.Name = "pnlCola";
            pnlCola.Size = new Size(794, 45);
            pnlCola.TabIndex = 1;
            //
            // btnAtenderSiguiente
            //
            btnAtenderSiguiente.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAtenderSiguiente.Location = new Point(5, 7);
            btnAtenderSiguiente.Name = "btnAtenderSiguiente";
            btnAtenderSiguiente.Size = new Size(170, 32);
            btnAtenderSiguiente.TabIndex = 0;
            btnAtenderSiguiente.Text = "Atender siguiente";
            btnAtenderSiguiente.UseVisualStyleBackColor = true;
            btnAtenderSiguiente.Click += btnAtenderSiguiente_Click;
            //
            // lblSiguiente
            //
            lblSiguiente.AutoSize = true;
            lblSiguiente.ForeColor = Color.DimGray;
            lblSiguiente.Location = new Point(190, 16);
            lblSiguiente.Name = "lblSiguiente";
            lblSiguiente.Size = new Size(60, 15);
            lblSiguiente.TabIndex = 1;
            lblSiguiente.Text = "Siguiente:";
            //
            // grpAlistados
            //
            grpAlistados.Controls.Add(dgvAlistados);
            grpAlistados.Controls.Add(pnlAlistados);
            grpAlistados.Dock = DockStyle.Fill;
            grpAlistados.Location = new Point(0, 320);
            grpAlistados.Name = "grpAlistados";
            grpAlistados.Size = new Size(800, 300);
            grpAlistados.TabIndex = 1;
            grpAlistados.TabStop = false;
            grpAlistados.Text = "Pedidos alistados (listos para despachar)";
            //
            // dgvAlistados
            //
            dgvAlistados.AllowUserToAddRows = false;
            dgvAlistados.AllowUserToDeleteRows = false;
            dgvAlistados.AllowUserToResizeRows = false;
            dgvAlistados.BackgroundColor = SystemColors.Window;
            dgvAlistados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlistados.Columns.AddRange(new DataGridViewColumn[] { colAlistadoId, colAlistadoCliente, colAlistadoPrioridad, colAlistadoFecha, colAlistadoItems });
            dgvAlistados.Dock = DockStyle.Fill;
            dgvAlistados.Location = new Point(3, 19);
            dgvAlistados.MultiSelect = false;
            dgvAlistados.Name = "dgvAlistados";
            dgvAlistados.ReadOnly = true;
            dgvAlistados.RowHeadersVisible = false;
            dgvAlistados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlistados.Size = new Size(794, 233);
            dgvAlistados.TabIndex = 0;
            //
            // colAlistadoId
            //
            colAlistadoId.HeaderText = "Pedido";
            colAlistadoId.Name = "colAlistadoId";
            colAlistadoId.ReadOnly = true;
            colAlistadoId.Width = 62;
            //
            // colAlistadoCliente
            //
            colAlistadoCliente.HeaderText = "Cliente";
            colAlistadoCliente.Name = "colAlistadoCliente";
            colAlistadoCliente.ReadOnly = true;
            colAlistadoCliente.Width = 160;
            //
            // colAlistadoPrioridad
            //
            colAlistadoPrioridad.HeaderText = "Prioridad";
            colAlistadoPrioridad.Name = "colAlistadoPrioridad";
            colAlistadoPrioridad.ReadOnly = true;
            colAlistadoPrioridad.Width = 80;
            //
            // colAlistadoFecha
            //
            colAlistadoFecha.HeaderText = "Fecha";
            colAlistadoFecha.Name = "colAlistadoFecha";
            colAlistadoFecha.ReadOnly = true;
            colAlistadoFecha.Width = 125;
            //
            // colAlistadoItems
            //
            colAlistadoItems.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAlistadoItems.HeaderText = "Ítems";
            colAlistadoItems.MinimumWidth = 120;
            colAlistadoItems.Name = "colAlistadoItems";
            colAlistadoItems.ReadOnly = true;
            //
            // pnlAlistados
            //
            pnlAlistados.Controls.Add(lblDespachados);
            pnlAlistados.Controls.Add(btnDespachar);
            pnlAlistados.Dock = DockStyle.Bottom;
            pnlAlistados.Location = new Point(3, 252);
            pnlAlistados.Name = "pnlAlistados";
            pnlAlistados.Size = new Size(794, 45);
            pnlAlistados.TabIndex = 1;
            //
            // btnDespachar
            //
            btnDespachar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDespachar.Location = new Point(5, 7);
            btnDespachar.Name = "btnDespachar";
            btnDespachar.Size = new Size(170, 32);
            btnDespachar.TabIndex = 0;
            btnDespachar.Text = "Despachar seleccionado";
            btnDespachar.UseVisualStyleBackColor = true;
            btnDespachar.Click += btnDespachar_Click;
            //
            // lblDespachados
            //
            lblDespachados.AutoSize = true;
            lblDespachados.ForeColor = Color.DimGray;
            lblDespachados.Location = new Point(190, 16);
            lblDespachados.Name = "lblDespachados";
            lblDespachados.Size = new Size(85, 15);
            lblDespachados.TabIndex = 1;
            lblDespachados.Text = "Despachados: 0";
            //
            // FrmPedidos
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 620);
            Controls.Add(pnlDerecha);
            Controls.Add(grpNuevo);
            MinimumSize = new Size(1000, 560);
            Name = "FrmPedidos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pedidos";
            Load += FrmPedidos_Load;
            grpNuevo.ResumeLayout(false);
            grpNuevo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            grpCola.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCola).EndInit();
            pnlCola.ResumeLayout(false);
            pnlCola.PerformLayout();
            grpAlistados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAlistados).EndInit();
            pnlAlistados.ResumeLayout(false);
            pnlAlistados.PerformLayout();
            pnlDerecha.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpNuevo;
        private Button btnCrearPedido;
        private Button btnQuitarItem;
        private DataGridView dgvItems;
        private DataGridViewTextBoxColumn colItemCodigo;
        private DataGridViewTextBoxColumn colItemProducto;
        private DataGridViewTextBoxColumn colItemCantidad;
        private Button btnAgregarItem;
        private NumericUpDown nudCantidad;
        private Label lblCantidad;
        private ComboBox cmbProducto;
        private Label lblProducto;
        private ComboBox cmbPrioridad;
        private Label lblPrioridad;
        private TextBox txtCliente;
        private Label lblCliente;
        private GroupBox grpCola;
        private DataGridView dgvCola;
        private DataGridViewTextBoxColumn colTurno;
        private DataGridViewTextBoxColumn colColaId;
        private DataGridViewTextBoxColumn colColaCliente;
        private DataGridViewTextBoxColumn colColaPrioridad;
        private DataGridViewTextBoxColumn colColaFecha;
        private DataGridViewTextBoxColumn colColaItems;
        private Panel pnlCola;
        private Label lblSiguiente;
        private Button btnAtenderSiguiente;
        private GroupBox grpAlistados;
        private DataGridView dgvAlistados;
        private DataGridViewTextBoxColumn colAlistadoId;
        private DataGridViewTextBoxColumn colAlistadoCliente;
        private DataGridViewTextBoxColumn colAlistadoPrioridad;
        private DataGridViewTextBoxColumn colAlistadoFecha;
        private DataGridViewTextBoxColumn colAlistadoItems;
        private Panel pnlAlistados;
        private Label lblDespachados;
        private Button btnDespachar;
        private Panel pnlDerecha;
    }
}
