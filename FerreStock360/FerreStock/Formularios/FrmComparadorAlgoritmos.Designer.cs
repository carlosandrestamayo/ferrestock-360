namespace FerreStock.Formularios
{
    partial class FrmComparadorAlgoritmos
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
            btnEjecutar = new Button();
            grpOrdenamientos = new GroupBox();
            dgvOrdenamientos = new DataGridView();
            colOrdN = new DataGridViewTextBoxColumn();
            colOrdBurbuja = new DataGridViewTextBoxColumn();
            colOrdInsercion = new DataGridViewTextBoxColumn();
            colOrdListSort = new DataGridViewTextBoxColumn();
            grpBusquedas = new GroupBox();
            dgvBusquedas = new DataGridView();
            colBusN = new DataGridViewTextBoxColumn();
            colBusCantidad = new DataGridViewTextBoxColumn();
            colBusCompLineal = new DataGridViewTextBoxColumn();
            colBusCompBinaria = new DataGridViewTextBoxColumn();
            colBusMsLineal = new DataGridViewTextBoxColumn();
            colBusMsBinaria = new DataGridViewTextBoxColumn();
            lblExplicacion = new Label();
            pnlSuperior.SuspendLayout();
            grpOrdenamientos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenamientos).BeginInit();
            grpBusquedas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBusquedas).BeginInit();
            SuspendLayout();
            //
            // pnlSuperior
            //
            pnlSuperior.Controls.Add(lblEstado);
            pnlSuperior.Controls.Add(barraProgreso);
            pnlSuperior.Controls.Add(btnEjecutar);
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Location = new Point(0, 0);
            pnlSuperior.Name = "pnlSuperior";
            pnlSuperior.Size = new Size(820, 56);
            pnlSuperior.TabIndex = 0;
            //
            // btnEjecutar
            //
            btnEjecutar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEjecutar.Location = new Point(12, 12);
            btnEjecutar.Name = "btnEjecutar";
            btnEjecutar.Size = new Size(160, 32);
            btnEjecutar.TabIndex = 0;
            btnEjecutar.Text = "Ejecutar comparación";
            btnEjecutar.UseVisualStyleBackColor = true;
            btnEjecutar.Click += btnEjecutar_Click;
            //
            // barraProgreso
            //
            barraProgreso.Location = new Point(185, 17);
            barraProgreso.Name = "barraProgreso";
            barraProgreso.Size = new Size(200, 23);
            barraProgreso.TabIndex = 1;
            //
            // lblEstado
            //
            lblEstado.AutoSize = true;
            lblEstado.ForeColor = Color.DimGray;
            lblEstado.Location = new Point(400, 21);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(200, 15);
            lblEstado.TabIndex = 2;
            lblEstado.Text = "Pulse \"Ejecutar comparación\" para empezar.";
            //
            // grpOrdenamientos
            //
            grpOrdenamientos.Controls.Add(dgvOrdenamientos);
            grpOrdenamientos.Dock = DockStyle.Top;
            grpOrdenamientos.Location = new Point(0, 56);
            grpOrdenamientos.Name = "grpOrdenamientos";
            grpOrdenamientos.Size = new Size(820, 160);
            grpOrdenamientos.TabIndex = 1;
            grpOrdenamientos.TabStop = false;
            grpOrdenamientos.Text = "Ordenamiento por código (tiempo en ms)";
            //
            // dgvOrdenamientos
            //
            dgvOrdenamientos.AllowUserToAddRows = false;
            dgvOrdenamientos.AllowUserToDeleteRows = false;
            dgvOrdenamientos.AllowUserToResizeRows = false;
            dgvOrdenamientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrdenamientos.BackgroundColor = SystemColors.Window;
            dgvOrdenamientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenamientos.Columns.AddRange(new DataGridViewColumn[] { colOrdN, colOrdBurbuja, colOrdInsercion, colOrdListSort });
            dgvOrdenamientos.Dock = DockStyle.Fill;
            dgvOrdenamientos.Location = new Point(3, 19);
            dgvOrdenamientos.Name = "dgvOrdenamientos";
            dgvOrdenamientos.ReadOnly = true;
            dgvOrdenamientos.RowHeadersVisible = false;
            dgvOrdenamientos.Size = new Size(814, 138);
            dgvOrdenamientos.TabIndex = 0;
            //
            // colOrdN
            //
            estiloNumero.Alignment = DataGridViewContentAlignment.MiddleRight;
            colOrdN.DefaultCellStyle = estiloNumero;
            colOrdN.HeaderText = "N (productos)";
            colOrdN.Name = "colOrdN";
            colOrdN.ReadOnly = true;
            //
            // colOrdBurbuja
            //
            colOrdBurbuja.DefaultCellStyle = estiloNumero;
            colOrdBurbuja.HeaderText = "Burbuja O(n²)";
            colOrdBurbuja.Name = "colOrdBurbuja";
            colOrdBurbuja.ReadOnly = true;
            //
            // colOrdInsercion
            //
            colOrdInsercion.DefaultCellStyle = estiloNumero;
            colOrdInsercion.HeaderText = "Inserción O(n²)";
            colOrdInsercion.Name = "colOrdInsercion";
            colOrdInsercion.ReadOnly = true;
            //
            // colOrdListSort
            //
            colOrdListSort.DefaultCellStyle = estiloNumero;
            colOrdListSort.HeaderText = "List.Sort() O(n log n)";
            colOrdListSort.Name = "colOrdListSort";
            colOrdListSort.ReadOnly = true;
            //
            // grpBusquedas
            //
            grpBusquedas.Controls.Add(dgvBusquedas);
            grpBusquedas.Dock = DockStyle.Fill;
            grpBusquedas.Location = new Point(0, 216);
            grpBusquedas.Name = "grpBusquedas";
            grpBusquedas.Size = new Size(820, 164);
            grpBusquedas.TabIndex = 2;
            grpBusquedas.TabStop = false;
            grpBusquedas.Text = "Búsqueda por código: lineal O(n) vs binaria O(log n)";
            //
            // dgvBusquedas
            //
            dgvBusquedas.AllowUserToAddRows = false;
            dgvBusquedas.AllowUserToDeleteRows = false;
            dgvBusquedas.AllowUserToResizeRows = false;
            dgvBusquedas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBusquedas.BackgroundColor = SystemColors.Window;
            dgvBusquedas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBusquedas.Columns.AddRange(new DataGridViewColumn[] { colBusN, colBusCantidad, colBusCompLineal, colBusCompBinaria, colBusMsLineal, colBusMsBinaria });
            dgvBusquedas.Dock = DockStyle.Fill;
            dgvBusquedas.Location = new Point(3, 19);
            dgvBusquedas.Name = "dgvBusquedas";
            dgvBusquedas.ReadOnly = true;
            dgvBusquedas.RowHeadersVisible = false;
            dgvBusquedas.Size = new Size(814, 142);
            dgvBusquedas.TabIndex = 0;
            //
            // colBusN
            //
            colBusN.DefaultCellStyle = estiloNumero;
            colBusN.HeaderText = "N (productos)";
            colBusN.Name = "colBusN";
            colBusN.ReadOnly = true;
            //
            // colBusCantidad
            //
            colBusCantidad.DefaultCellStyle = estiloNumero;
            colBusCantidad.HeaderText = "Búsquedas";
            colBusCantidad.Name = "colBusCantidad";
            colBusCantidad.ReadOnly = true;
            //
            // colBusCompLineal
            //
            colBusCompLineal.DefaultCellStyle = estiloNumero;
            colBusCompLineal.HeaderText = "Comparaciones promedio (lineal)";
            colBusCompLineal.Name = "colBusCompLineal";
            colBusCompLineal.ReadOnly = true;
            //
            // colBusCompBinaria
            //
            colBusCompBinaria.DefaultCellStyle = estiloNumero;
            colBusCompBinaria.HeaderText = "Comparaciones promedio (binaria)";
            colBusCompBinaria.Name = "colBusCompBinaria";
            colBusCompBinaria.ReadOnly = true;
            //
            // colBusMsLineal
            //
            colBusMsLineal.DefaultCellStyle = estiloNumero;
            colBusMsLineal.HeaderText = "Tiempo total lineal (ms)";
            colBusMsLineal.Name = "colBusMsLineal";
            colBusMsLineal.ReadOnly = true;
            //
            // colBusMsBinaria
            //
            colBusMsBinaria.DefaultCellStyle = estiloNumero;
            colBusMsBinaria.HeaderText = "Tiempo total binaria (ms)";
            colBusMsBinaria.Name = "colBusMsBinaria";
            colBusMsBinaria.ReadOnly = true;
            //
            // lblExplicacion
            //
            lblExplicacion.Dock = DockStyle.Bottom;
            lblExplicacion.ForeColor = Color.DimGray;
            lblExplicacion.Location = new Point(0, 380);
            lblExplicacion.Name = "lblExplicacion";
            lblExplicacion.Padding = new Padding(8, 4, 8, 4);
            lblExplicacion.Size = new Size(820, 60);
            lblExplicacion.TabIndex = 3;
            lblExplicacion.Text = "Los productos se generan al azar. Si N se multiplica por 10, Burbuja e Inserción tardan unas 100 veces más (n²), " +
                "mientras que List.Sort tarda poco más de 10 veces (n log n). La búsqueda binaria necesita la lista ordenada, " +
                "pero hace muchas menos comparaciones que la lineal.";
            //
            // FrmComparadorAlgoritmos
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 440);
            Controls.Add(grpBusquedas);
            Controls.Add(grpOrdenamientos);
            Controls.Add(pnlSuperior);
            Controls.Add(lblExplicacion);
            MinimumSize = new Size(760, 420);
            Name = "FrmComparadorAlgoritmos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Comparador de algoritmos";
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            grpOrdenamientos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrdenamientos).EndInit();
            grpBusquedas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBusquedas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSuperior;
        private Label lblEstado;
        private ProgressBar barraProgreso;
        private Button btnEjecutar;
        private GroupBox grpOrdenamientos;
        private DataGridView dgvOrdenamientos;
        private DataGridViewTextBoxColumn colOrdN;
        private DataGridViewTextBoxColumn colOrdBurbuja;
        private DataGridViewTextBoxColumn colOrdInsercion;
        private DataGridViewTextBoxColumn colOrdListSort;
        private GroupBox grpBusquedas;
        private DataGridView dgvBusquedas;
        private DataGridViewTextBoxColumn colBusN;
        private DataGridViewTextBoxColumn colBusCantidad;
        private DataGridViewTextBoxColumn colBusCompLineal;
        private DataGridViewTextBoxColumn colBusCompBinaria;
        private DataGridViewTextBoxColumn colBusMsLineal;
        private DataGridViewTextBoxColumn colBusMsBinaria;
        private Label lblExplicacion;
    }
}
