namespace FerreStock.Formularios
{
    partial class FrmIndicadores
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
            pnlSuperior = new Panel();
            lblEstado = new Label();
            barraProgreso = new ProgressBar();
            btnCalcular = new Button();
            grpResumen = new GroupBox();
            lblSimulacion = new Label();
            lblResumen = new Label();
            tabDetalle = new TabControl();
            tabABC = new TabPage();
            dgvABC = new DataGridView();
            colAbcCodigo = new DataGridViewTextBoxColumn();
            colAbcNombre = new DataGridViewTextBoxColumn();
            colAbcValor = new DataGridViewTextBoxColumn();
            colAbcPorcentaje = new DataGridViewTextBoxColumn();
            colAbcAcumulado = new DataGridViewTextBoxColumn();
            colAbcClase = new DataGridViewTextBoxColumn();
            tabBajoMinimo = new TabPage();
            dgvBajoMinimo = new DataGridView();
            colBajoCodigo = new DataGridViewTextBoxColumn();
            colBajoNombre = new DataGridViewTextBoxColumn();
            colBajoStock = new DataGridViewTextBoxColumn();
            colBajoMinimo = new DataGridViewTextBoxColumn();
            colBajoFaltan = new DataGridViewTextBoxColumn();
            colBajoUbicacion = new DataGridViewTextBoxColumn();
            tabZonas = new TabPage();
            dgvZonas = new DataGridView();
            colZona = new DataGridViewTextBoxColumn();
            colZonaOcupado = new DataGridViewTextBoxColumn();
            colZonaCapacidad = new DataGridViewTextBoxColumn();
            colZonaPorcentaje = new DataGridViewTextBoxColumn();
            pnlSuperior.SuspendLayout();
            grpResumen.SuspendLayout();
            tabDetalle.SuspendLayout();
            tabABC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvABC).BeginInit();
            tabBajoMinimo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBajoMinimo).BeginInit();
            tabZonas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvZonas).BeginInit();
            SuspendLayout();
            //
            // pnlSuperior
            //
            pnlSuperior.Controls.Add(lblEstado);
            pnlSuperior.Controls.Add(barraProgreso);
            pnlSuperior.Controls.Add(btnCalcular);
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Location = new Point(0, 0);
            pnlSuperior.Name = "pnlSuperior";
            pnlSuperior.Size = new Size(1000, 52);
            pnlSuperior.TabIndex = 0;
            //
            // btnCalcular
            //
            btnCalcular.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCalcular.Location = new Point(12, 10);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(170, 32);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Actualizar indicadores";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            //
            // barraProgreso
            //
            barraProgreso.Location = new Point(195, 15);
            barraProgreso.MarqueeAnimationSpeed = 30;
            barraProgreso.Name = "barraProgreso";
            barraProgreso.Size = new Size(160, 23);
            barraProgreso.Style = ProgressBarStyle.Marquee;
            barraProgreso.TabIndex = 1;
            barraProgreso.Visible = false;
            //
            // lblEstado
            //
            lblEstado.AutoSize = true;
            lblEstado.ForeColor = Color.DimGray;
            lblEstado.Location = new Point(370, 19);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(0, 15);
            lblEstado.TabIndex = 2;
            //
            // grpResumen
            //
            grpResumen.Controls.Add(lblSimulacion);
            grpResumen.Controls.Add(lblResumen);
            grpResumen.Dock = DockStyle.Top;
            grpResumen.Location = new Point(0, 52);
            grpResumen.Name = "grpResumen";
            grpResumen.Padding = new Padding(10, 6, 10, 6);
            grpResumen.Size = new Size(1000, 200);
            grpResumen.TabIndex = 1;
            grpResumen.TabStop = false;
            grpResumen.Text = "Resumen";
            //
            // lblResumen
            //
            lblResumen.Dock = DockStyle.Left;
            lblResumen.Font = new Font("Segoe UI", 10F);
            lblResumen.Location = new Point(10, 22);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(520, 172);
            lblResumen.TabIndex = 0;
            //
            // lblSimulacion
            //
            lblSimulacion.Dock = DockStyle.Fill;
            lblSimulacion.Font = new Font("Segoe UI", 10F);
            lblSimulacion.ForeColor = Color.DarkGreen;
            lblSimulacion.Location = new Point(530, 22);
            lblSimulacion.Name = "lblSimulacion";
            lblSimulacion.Size = new Size(460, 172);
            lblSimulacion.TabIndex = 1;
            //
            // tabDetalle
            //
            tabDetalle.Controls.Add(tabABC);
            tabDetalle.Controls.Add(tabBajoMinimo);
            tabDetalle.Controls.Add(tabZonas);
            tabDetalle.Dock = DockStyle.Fill;
            tabDetalle.Location = new Point(0, 252);
            tabDetalle.Name = "tabDetalle";
            tabDetalle.SelectedIndex = 0;
            tabDetalle.Size = new Size(1000, 348);
            tabDetalle.TabIndex = 2;
            //
            // tabABC
            //
            tabABC.Controls.Add(dgvABC);
            tabABC.Location = new Point(4, 24);
            tabABC.Name = "tabABC";
            tabABC.Size = new Size(992, 320);
            tabABC.TabIndex = 0;
            tabABC.Text = "Clasificación ABC";
            tabABC.UseVisualStyleBackColor = true;
            //
            // dgvABC
            //
            dgvABC.AllowUserToAddRows = false;
            dgvABC.AllowUserToDeleteRows = false;
            dgvABC.AllowUserToResizeRows = false;
            dgvABC.BackgroundColor = SystemColors.Window;
            dgvABC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvABC.Columns.AddRange(new DataGridViewColumn[] { colAbcCodigo, colAbcNombre, colAbcValor, colAbcPorcentaje, colAbcAcumulado, colAbcClase });
            dgvABC.Dock = DockStyle.Fill;
            dgvABC.Location = new Point(0, 0);
            dgvABC.Name = "dgvABC";
            dgvABC.ReadOnly = true;
            dgvABC.RowHeadersVisible = false;
            dgvABC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvABC.Size = new Size(992, 320);
            dgvABC.TabIndex = 0;
            //
            // colAbcCodigo
            //
            colAbcCodigo.HeaderText = "Código";
            colAbcCodigo.Name = "colAbcCodigo";
            colAbcCodigo.ReadOnly = true;
            colAbcCodigo.Width = 80;
            //
            // colAbcNombre
            //
            colAbcNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAbcNombre.HeaderText = "Producto";
            colAbcNombre.Name = "colAbcNombre";
            colAbcNombre.ReadOnly = true;
            //
            // colAbcValor
            //
            estiloNumero.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAbcValor.DefaultCellStyle = estiloNumero;
            colAbcValor.HeaderText = "Valor vendido ($/mes)";
            colAbcValor.Name = "colAbcValor";
            colAbcValor.ReadOnly = true;
            colAbcValor.Width = 130;
            //
            // colAbcPorcentaje
            //
            colAbcPorcentaje.DefaultCellStyle = estiloNumero;
            colAbcPorcentaje.HeaderText = "% del total";
            colAbcPorcentaje.Name = "colAbcPorcentaje";
            colAbcPorcentaje.ReadOnly = true;
            colAbcPorcentaje.Width = 85;
            //
            // colAbcAcumulado
            //
            colAbcAcumulado.DefaultCellStyle = estiloNumero;
            colAbcAcumulado.HeaderText = "% acumulado";
            colAbcAcumulado.Name = "colAbcAcumulado";
            colAbcAcumulado.ReadOnly = true;
            colAbcAcumulado.Width = 95;
            //
            // colAbcClase
            //
            colAbcClase.HeaderText = "Clase";
            colAbcClase.Name = "colAbcClase";
            colAbcClase.ReadOnly = true;
            colAbcClase.Width = 55;
            //
            // tabBajoMinimo
            //
            tabBajoMinimo.Controls.Add(dgvBajoMinimo);
            tabBajoMinimo.Location = new Point(4, 24);
            tabBajoMinimo.Name = "tabBajoMinimo";
            tabBajoMinimo.Size = new Size(992, 320);
            tabBajoMinimo.TabIndex = 1;
            tabBajoMinimo.Text = "Bajo stock mínimo";
            tabBajoMinimo.UseVisualStyleBackColor = true;
            //
            // dgvBajoMinimo
            //
            dgvBajoMinimo.AllowUserToAddRows = false;
            dgvBajoMinimo.AllowUserToDeleteRows = false;
            dgvBajoMinimo.AllowUserToResizeRows = false;
            dgvBajoMinimo.BackgroundColor = SystemColors.Window;
            dgvBajoMinimo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBajoMinimo.Columns.AddRange(new DataGridViewColumn[] { colBajoCodigo, colBajoNombre, colBajoStock, colBajoMinimo, colBajoFaltan, colBajoUbicacion });
            dgvBajoMinimo.Dock = DockStyle.Fill;
            dgvBajoMinimo.Location = new Point(0, 0);
            dgvBajoMinimo.Name = "dgvBajoMinimo";
            dgvBajoMinimo.ReadOnly = true;
            dgvBajoMinimo.RowHeadersVisible = false;
            dgvBajoMinimo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBajoMinimo.Size = new Size(992, 320);
            dgvBajoMinimo.TabIndex = 0;
            //
            // colBajoCodigo
            //
            colBajoCodigo.HeaderText = "Código";
            colBajoCodigo.Name = "colBajoCodigo";
            colBajoCodigo.ReadOnly = true;
            colBajoCodigo.Width = 80;
            //
            // colBajoNombre
            //
            colBajoNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBajoNombre.HeaderText = "Producto";
            colBajoNombre.Name = "colBajoNombre";
            colBajoNombre.ReadOnly = true;
            //
            // colBajoStock
            //
            colBajoStock.DefaultCellStyle = estiloNumero;
            colBajoStock.HeaderText = "Stock";
            colBajoStock.Name = "colBajoStock";
            colBajoStock.ReadOnly = true;
            colBajoStock.Width = 70;
            //
            // colBajoMinimo
            //
            colBajoMinimo.DefaultCellStyle = estiloNumero;
            colBajoMinimo.HeaderText = "Mínimo";
            colBajoMinimo.Name = "colBajoMinimo";
            colBajoMinimo.ReadOnly = true;
            colBajoMinimo.Width = 70;
            //
            // colBajoFaltan
            //
            colBajoFaltan.DefaultCellStyle = estiloNumero;
            colBajoFaltan.HeaderText = "Faltan";
            colBajoFaltan.Name = "colBajoFaltan";
            colBajoFaltan.ReadOnly = true;
            colBajoFaltan.Width = 70;
            //
            // colBajoUbicacion
            //
            colBajoUbicacion.HeaderText = "Ubicación";
            colBajoUbicacion.Name = "colBajoUbicacion";
            colBajoUbicacion.ReadOnly = true;
            colBajoUbicacion.Width = 85;
            //
            // tabZonas
            //
            tabZonas.Controls.Add(dgvZonas);
            tabZonas.Location = new Point(4, 24);
            tabZonas.Name = "tabZonas";
            tabZonas.Size = new Size(992, 320);
            tabZonas.TabIndex = 2;
            tabZonas.Text = "Ocupación por zona";
            tabZonas.UseVisualStyleBackColor = true;
            //
            // dgvZonas
            //
            dgvZonas.AllowUserToAddRows = false;
            dgvZonas.AllowUserToDeleteRows = false;
            dgvZonas.AllowUserToResizeRows = false;
            dgvZonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvZonas.BackgroundColor = SystemColors.Window;
            dgvZonas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvZonas.Columns.AddRange(new DataGridViewColumn[] { colZona, colZonaOcupado, colZonaCapacidad, colZonaPorcentaje });
            dgvZonas.Dock = DockStyle.Fill;
            dgvZonas.Location = new Point(0, 0);
            dgvZonas.Name = "dgvZonas";
            dgvZonas.ReadOnly = true;
            dgvZonas.RowHeadersVisible = false;
            dgvZonas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvZonas.Size = new Size(992, 320);
            dgvZonas.TabIndex = 0;
            //
            // colZona
            //
            colZona.HeaderText = "Zona";
            colZona.Name = "colZona";
            colZona.ReadOnly = true;
            //
            // colZonaOcupado
            //
            colZonaOcupado.DefaultCellStyle = estiloNumero;
            colZonaOcupado.HeaderText = "Ocupado (m³)";
            colZonaOcupado.Name = "colZonaOcupado";
            colZonaOcupado.ReadOnly = true;
            //
            // colZonaCapacidad
            //
            colZonaCapacidad.DefaultCellStyle = estiloNumero;
            colZonaCapacidad.HeaderText = "Capacidad (m³)";
            colZonaCapacidad.Name = "colZonaCapacidad";
            colZonaCapacidad.ReadOnly = true;
            //
            // colZonaPorcentaje
            //
            colZonaPorcentaje.DefaultCellStyle = estiloNumero;
            colZonaPorcentaje.HeaderText = "Ocupación (%)";
            colZonaPorcentaje.Name = "colZonaPorcentaje";
            colZonaPorcentaje.ReadOnly = true;
            //
            // FrmIndicadores
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(tabDetalle);
            Controls.Add(grpResumen);
            Controls.Add(pnlSuperior);
            MinimumSize = new Size(900, 560);
            Name = "FrmIndicadores";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Indicadores";
            Load += FrmIndicadores_Load;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            grpResumen.ResumeLayout(false);
            tabDetalle.ResumeLayout(false);
            tabABC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvABC).EndInit();
            tabBajoMinimo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBajoMinimo).EndInit();
            tabZonas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvZonas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSuperior;
        private Label lblEstado;
        private ProgressBar barraProgreso;
        private Button btnCalcular;
        private GroupBox grpResumen;
        private Label lblSimulacion;
        private Label lblResumen;
        private TabControl tabDetalle;
        private TabPage tabABC;
        private DataGridView dgvABC;
        private DataGridViewTextBoxColumn colAbcCodigo;
        private DataGridViewTextBoxColumn colAbcNombre;
        private DataGridViewTextBoxColumn colAbcValor;
        private DataGridViewTextBoxColumn colAbcPorcentaje;
        private DataGridViewTextBoxColumn colAbcAcumulado;
        private DataGridViewTextBoxColumn colAbcClase;
        private TabPage tabBajoMinimo;
        private DataGridView dgvBajoMinimo;
        private DataGridViewTextBoxColumn colBajoCodigo;
        private DataGridViewTextBoxColumn colBajoNombre;
        private DataGridViewTextBoxColumn colBajoStock;
        private DataGridViewTextBoxColumn colBajoMinimo;
        private DataGridViewTextBoxColumn colBajoFaltan;
        private DataGridViewTextBoxColumn colBajoUbicacion;
        private TabPage tabZonas;
        private DataGridView dgvZonas;
        private DataGridViewTextBoxColumn colZona;
        private DataGridViewTextBoxColumn colZonaOcupado;
        private DataGridViewTextBoxColumn colZonaCapacidad;
        private DataGridViewTextBoxColumn colZonaPorcentaje;
    }
}
