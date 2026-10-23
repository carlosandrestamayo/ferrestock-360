namespace FerreStock.Formularios
{
    partial class FrmCalculadora
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
            tabCalculadora = new TabControl();
            tabMonticulo = new TabPage();
            lblResultadoMonticulo = new Label();
            lblFormulaMonticulo = new Label();
            btnCalcularVolumen = new Button();
            cmbAgregado = new ComboBox();
            lblAgregado = new Label();
            nudAltura = new NumericUpDown();
            lblAltura = new Label();
            nudRadio = new NumericUpDown();
            lblRadio = new Label();
            tabArea = new TabPage();
            lblResultadoArea = new Label();
            btnCalcularArea = new Button();
            nudAlturaUtil = new NumericUpDown();
            lblAlturaUtil = new Label();
            nudLargoPasillo = new NumericUpDown();
            lblLargoPasillo = new Label();
            nudAnchoPasillo = new NumericUpDown();
            lblAnchoPasillo = new Label();
            nudCantidadPasillos = new NumericUpDown();
            lblCantidadPasillos = new Label();
            nudLargo = new NumericUpDown();
            lblLargo = new Label();
            nudAncho = new NumericUpDown();
            lblAncho = new Label();
            tabCosto = new TabPage();
            dgvSugerencias = new DataGridView();
            colSugCodigo = new DataGridViewTextBoxColumn();
            colSugNombre = new DataGridViewTextBoxColumn();
            colSugActual = new DataGridViewTextBoxColumn();
            colSugCostoActual = new DataGridViewTextBoxColumn();
            colSugSugerida = new DataGridViewTextBoxColumn();
            colSugCostoSugerido = new DataGridViewTextBoxColumn();
            colSugAhorro = new DataGridViewTextBoxColumn();
            lblSimulacion = new Label();
            btnSugerir = new Button();
            lblResultadoCosto = new Label();
            btnCalcularCosto = new Button();
            nudB = new NumericUpDown();
            lblB = new Label();
            nudA = new NumericUpDown();
            lblA = new Label();
            nudZ = new NumericUpDown();
            lblZ = new Label();
            nudY = new NumericUpDown();
            lblY = new Label();
            nudX = new NumericUpDown();
            lblX = new Label();
            lblDatosProducto = new Label();
            cmbProductoCosto = new ComboBox();
            lblProductoCosto = new Label();
            tabLagrange = new TabPage();
            lblResultadoLagrange = new Label();
            dgvResultadoLagrange = new DataGridView();
            colResProducto = new DataGridViewTextBoxColumn();
            colResLibre = new DataGridViewTextBoxColumn();
            colResOptimo = new DataGridViewTextBoxColumn();
            colResVolumen = new DataGridViewTextBoxColumn();
            btnEjemploLagrange = new Button();
            btnResolverLagrange = new Button();
            nudVolumenDisponible = new NumericUpDown();
            lblVolumenDisponible = new Label();
            lblLagExplicacion = new Label();
            dgvLagrange = new DataGridView();
            colLagProducto = new DataGridViewTextBoxColumn();
            colLagDemanda = new DataGridViewTextBoxColumn();
            colLagCostoPedido = new DataGridViewTextBoxColumn();
            colLagMantener = new DataGridViewTextBoxColumn();
            colLagVolumen = new DataGridViewTextBoxColumn();
            tabCalculadora.SuspendLayout();
            tabMonticulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAltura).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRadio).BeginInit();
            tabArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAlturaUtil).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLargoPasillo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAnchoPasillo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidadPasillos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLargo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAncho).BeginInit();
            tabCosto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSugerencias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            tabLagrange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResultadoLagrange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVolumenDisponible).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLagrange).BeginInit();
            SuspendLayout();
            //
            // tabCalculadora
            //
            tabCalculadora.Controls.Add(tabMonticulo);
            tabCalculadora.Controls.Add(tabArea);
            tabCalculadora.Controls.Add(tabCosto);
            tabCalculadora.Controls.Add(tabLagrange);
            tabCalculadora.Dock = DockStyle.Fill;
            tabCalculadora.Location = new Point(0, 0);
            tabCalculadora.Name = "tabCalculadora";
            tabCalculadora.SelectedIndex = 0;
            tabCalculadora.Size = new Size(940, 600);
            tabCalculadora.TabIndex = 0;
            //
            // tabMonticulo
            //
            tabMonticulo.Controls.Add(lblResultadoMonticulo);
            tabMonticulo.Controls.Add(lblFormulaMonticulo);
            tabMonticulo.Controls.Add(btnCalcularVolumen);
            tabMonticulo.Controls.Add(cmbAgregado);
            tabMonticulo.Controls.Add(lblAgregado);
            tabMonticulo.Controls.Add(nudAltura);
            tabMonticulo.Controls.Add(lblAltura);
            tabMonticulo.Controls.Add(nudRadio);
            tabMonticulo.Controls.Add(lblRadio);
            tabMonticulo.Location = new Point(4, 24);
            tabMonticulo.Name = "tabMonticulo";
            tabMonticulo.Padding = new Padding(3);
            tabMonticulo.Size = new Size(932, 572);
            tabMonticulo.TabIndex = 0;
            tabMonticulo.Text = "Montículo de arena";
            tabMonticulo.UseVisualStyleBackColor = true;
            //
            // lblRadio
            //
            lblRadio.AutoSize = true;
            lblRadio.Location = new Point(20, 25);
            lblRadio.Name = "lblRadio";
            lblRadio.Size = new Size(72, 15);
            lblRadio.TabIndex = 0;
            lblRadio.Text = "Radio R (m)";
            //
            // nudRadio
            //
            nudRadio.DecimalPlaces = 2;
            nudRadio.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudRadio.Location = new Point(160, 22);
            nudRadio.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudRadio.Name = "nudRadio";
            nudRadio.Size = new Size(120, 23);
            nudRadio.TabIndex = 1;
            nudRadio.Value = new decimal(new int[] { 2, 0, 0, 0 });
            //
            // lblAltura
            //
            lblAltura.AutoSize = true;
            lblAltura.Location = new Point(20, 57);
            lblAltura.Name = "lblAltura";
            lblAltura.Size = new Size(76, 15);
            lblAltura.TabIndex = 2;
            lblAltura.Text = "Altura h (m)";
            //
            // nudAltura
            //
            nudAltura.DecimalPlaces = 2;
            nudAltura.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudAltura.Location = new Point(160, 54);
            nudAltura.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudAltura.Name = "nudAltura";
            nudAltura.Size = new Size(120, 23);
            nudAltura.TabIndex = 3;
            nudAltura.Value = new decimal(new int[] { 15, 0, 0, 65536 });
            //
            // lblAgregado
            //
            lblAgregado.AutoSize = true;
            lblAgregado.Location = new Point(20, 89);
            lblAgregado.Name = "lblAgregado";
            lblAgregado.Size = new Size(58, 15);
            lblAgregado.TabIndex = 4;
            lblAgregado.Text = "Agregado";
            //
            // cmbAgregado
            //
            cmbAgregado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAgregado.Location = new Point(160, 86);
            cmbAgregado.Name = "cmbAgregado";
            cmbAgregado.Size = new Size(260, 23);
            cmbAgregado.TabIndex = 5;
            //
            // btnCalcularVolumen
            //
            btnCalcularVolumen.Location = new Point(160, 120);
            btnCalcularVolumen.Name = "btnCalcularVolumen";
            btnCalcularVolumen.Size = new Size(150, 30);
            btnCalcularVolumen.TabIndex = 6;
            btnCalcularVolumen.Text = "Calcular volumen";
            btnCalcularVolumen.UseVisualStyleBackColor = true;
            btnCalcularVolumen.Click += btnCalcularVolumen_Click;
            //
            // lblFormulaMonticulo
            //
            lblFormulaMonticulo.ForeColor = Color.DimGray;
            lblFormulaMonticulo.Location = new Point(460, 22);
            lblFormulaMonticulo.Name = "lblFormulaMonticulo";
            lblFormulaMonticulo.Size = new Size(440, 110);
            lblFormulaMonticulo.TabIndex = 7;
            lblFormulaMonticulo.Text = "Altura del montículo a una distancia r del centro: z = h(1 − r²/R²).\r\n\r\n" +
                "V = ∫₀^2π ∫₀^R h(1 − r²/R²) r dr dθ = πR²h / 2\r\n\r\n" +
                "Si el montículo fuera un cono: V = πR²h / 3.";
            //
            // lblResultadoMonticulo
            //
            lblResultadoMonticulo.Font = new Font("Segoe UI", 10F);
            lblResultadoMonticulo.Location = new Point(20, 170);
            lblResultadoMonticulo.Name = "lblResultadoMonticulo";
            lblResultadoMonticulo.Size = new Size(880, 220);
            lblResultadoMonticulo.TabIndex = 8;
            //
            // tabArea
            //
            tabArea.Controls.Add(lblResultadoArea);
            tabArea.Controls.Add(btnCalcularArea);
            tabArea.Controls.Add(nudAlturaUtil);
            tabArea.Controls.Add(lblAlturaUtil);
            tabArea.Controls.Add(nudLargoPasillo);
            tabArea.Controls.Add(lblLargoPasillo);
            tabArea.Controls.Add(nudAnchoPasillo);
            tabArea.Controls.Add(lblAnchoPasillo);
            tabArea.Controls.Add(nudCantidadPasillos);
            tabArea.Controls.Add(lblCantidadPasillos);
            tabArea.Controls.Add(nudLargo);
            tabArea.Controls.Add(lblLargo);
            tabArea.Controls.Add(nudAncho);
            tabArea.Controls.Add(lblAncho);
            tabArea.Location = new Point(4, 24);
            tabArea.Name = "tabArea";
            tabArea.Padding = new Padding(3);
            tabArea.Size = new Size(932, 572);
            tabArea.TabIndex = 1;
            tabArea.Text = "Área útil y ocupación";
            tabArea.UseVisualStyleBackColor = true;
            //
            // lblAncho
            //
            lblAncho.AutoSize = true;
            lblAncho.Location = new Point(20, 25);
            lblAncho.Name = "lblAncho";
            lblAncho.Size = new Size(122, 15);
            lblAncho.TabIndex = 0;
            lblAncho.Text = "Ancho de bodega (m)";
            //
            // nudAncho
            //
            nudAncho.DecimalPlaces = 2;
            nudAncho.Location = new Point(200, 22);
            nudAncho.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudAncho.Name = "nudAncho";
            nudAncho.Size = new Size(120, 23);
            nudAncho.TabIndex = 1;
            nudAncho.Value = new decimal(new int[] { 26, 0, 0, 0 });
            //
            // lblLargo
            //
            lblLargo.AutoSize = true;
            lblLargo.Location = new Point(20, 57);
            lblLargo.Name = "lblLargo";
            lblLargo.Size = new Size(118, 15);
            lblLargo.TabIndex = 2;
            lblLargo.Text = "Largo de bodega (m)";
            //
            // nudLargo
            //
            nudLargo.DecimalPlaces = 2;
            nudLargo.Location = new Point(200, 54);
            nudLargo.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudLargo.Name = "nudLargo";
            nudLargo.Size = new Size(120, 23);
            nudLargo.TabIndex = 3;
            nudLargo.Value = new decimal(new int[] { 16, 0, 0, 0 });
            //
            // lblCantidadPasillos
            //
            lblCantidadPasillos.AutoSize = true;
            lblCantidadPasillos.Location = new Point(20, 89);
            lblCantidadPasillos.Name = "lblCantidadPasillos";
            lblCantidadPasillos.Size = new Size(119, 15);
            lblCantidadPasillos.TabIndex = 4;
            lblCantidadPasillos.Text = "Cantidad de pasillos";
            //
            // nudCantidadPasillos
            //
            nudCantidadPasillos.Location = new Point(200, 86);
            nudCantidadPasillos.Name = "nudCantidadPasillos";
            nudCantidadPasillos.Size = new Size(120, 23);
            nudCantidadPasillos.TabIndex = 5;
            nudCantidadPasillos.Value = new decimal(new int[] { 3, 0, 0, 0 });
            //
            // lblAnchoPasillo
            //
            lblAnchoPasillo.AutoSize = true;
            lblAnchoPasillo.Location = new Point(20, 121);
            lblAnchoPasillo.Name = "lblAnchoPasillo";
            lblAnchoPasillo.Size = new Size(122, 15);
            lblAnchoPasillo.TabIndex = 6;
            lblAnchoPasillo.Text = "Ancho de pasillo (m)";
            //
            // nudAnchoPasillo
            //
            nudAnchoPasillo.DecimalPlaces = 2;
            nudAnchoPasillo.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudAnchoPasillo.Location = new Point(200, 118);
            nudAnchoPasillo.Name = "nudAnchoPasillo";
            nudAnchoPasillo.Size = new Size(120, 23);
            nudAnchoPasillo.TabIndex = 7;
            nudAnchoPasillo.Value = new decimal(new int[] { 15, 0, 0, 65536 });
            //
            // lblLargoPasillo
            //
            lblLargoPasillo.AutoSize = true;
            lblLargoPasillo.Location = new Point(20, 153);
            lblLargoPasillo.Name = "lblLargoPasillo";
            lblLargoPasillo.Size = new Size(118, 15);
            lblLargoPasillo.TabIndex = 8;
            lblLargoPasillo.Text = "Largo de pasillo (m)";
            //
            // nudLargoPasillo
            //
            nudLargoPasillo.DecimalPlaces = 2;
            nudLargoPasillo.Location = new Point(200, 150);
            nudLargoPasillo.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudLargoPasillo.Name = "nudLargoPasillo";
            nudLargoPasillo.Size = new Size(120, 23);
            nudLargoPasillo.TabIndex = 9;
            nudLargoPasillo.Value = new decimal(new int[] { 26, 0, 0, 0 });
            //
            // lblAlturaUtil
            //
            lblAlturaUtil.AutoSize = true;
            lblAlturaUtil.Location = new Point(20, 185);
            lblAlturaUtil.Name = "lblAlturaUtil";
            lblAlturaUtil.Size = new Size(159, 15);
            lblAlturaUtil.TabIndex = 10;
            lblAlturaUtil.Text = "Altura útil de estantes (m)";
            //
            // nudAlturaUtil
            //
            nudAlturaUtil.DecimalPlaces = 2;
            nudAlturaUtil.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudAlturaUtil.Location = new Point(200, 182);
            nudAlturaUtil.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudAlturaUtil.Name = "nudAlturaUtil";
            nudAlturaUtil.Size = new Size(120, 23);
            nudAlturaUtil.TabIndex = 11;
            nudAlturaUtil.Value = new decimal(new int[] { 2, 0, 0, 0 });
            //
            // btnCalcularArea
            //
            btnCalcularArea.Location = new Point(200, 216);
            btnCalcularArea.Name = "btnCalcularArea";
            btnCalcularArea.Size = new Size(150, 30);
            btnCalcularArea.TabIndex = 12;
            btnCalcularArea.Text = "Calcular";
            btnCalcularArea.UseVisualStyleBackColor = true;
            btnCalcularArea.Click += btnCalcularArea_Click;
            //
            // lblResultadoArea
            //
            lblResultadoArea.Font = new Font("Segoe UI", 10F);
            lblResultadoArea.Location = new Point(400, 22);
            lblResultadoArea.Name = "lblResultadoArea";
            lblResultadoArea.Size = new Size(510, 260);
            lblResultadoArea.TabIndex = 13;
            //
            // tabCosto
            //
            tabCosto.Controls.Add(dgvSugerencias);
            tabCosto.Controls.Add(lblSimulacion);
            tabCosto.Controls.Add(btnSugerir);
            tabCosto.Controls.Add(lblResultadoCosto);
            tabCosto.Controls.Add(btnCalcularCosto);
            tabCosto.Controls.Add(nudB);
            tabCosto.Controls.Add(lblB);
            tabCosto.Controls.Add(nudA);
            tabCosto.Controls.Add(lblA);
            tabCosto.Controls.Add(nudZ);
            tabCosto.Controls.Add(lblZ);
            tabCosto.Controls.Add(nudY);
            tabCosto.Controls.Add(lblY);
            tabCosto.Controls.Add(nudX);
            tabCosto.Controls.Add(lblX);
            tabCosto.Controls.Add(lblDatosProducto);
            tabCosto.Controls.Add(cmbProductoCosto);
            tabCosto.Controls.Add(lblProductoCosto);
            tabCosto.Location = new Point(4, 24);
            tabCosto.Name = "tabCosto";
            tabCosto.Padding = new Padding(3);
            tabCosto.Size = new Size(932, 572);
            tabCosto.TabIndex = 2;
            tabCosto.Text = "Costo de ubicación";
            tabCosto.UseVisualStyleBackColor = true;
            //
            // lblProductoCosto
            //
            lblProductoCosto.AutoSize = true;
            lblProductoCosto.Location = new Point(20, 22);
            lblProductoCosto.Name = "lblProductoCosto";
            lblProductoCosto.Size = new Size(56, 15);
            lblProductoCosto.TabIndex = 0;
            lblProductoCosto.Text = "Producto";
            //
            // cmbProductoCosto
            //
            cmbProductoCosto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductoCosto.DropDownWidth = 360;
            cmbProductoCosto.Location = new Point(160, 19);
            cmbProductoCosto.Name = "cmbProductoCosto";
            cmbProductoCosto.Size = new Size(300, 23);
            cmbProductoCosto.TabIndex = 1;
            cmbProductoCosto.SelectedIndexChanged += cmbProductoCosto_SelectedIndexChanged;
            //
            // lblDatosProducto
            //
            lblDatosProducto.AutoSize = true;
            lblDatosProducto.ForeColor = Color.DimGray;
            lblDatosProducto.Location = new Point(160, 47);
            lblDatosProducto.Name = "lblDatosProducto";
            lblDatosProducto.Size = new Size(0, 15);
            lblDatosProducto.TabIndex = 2;
            //
            // lblX
            //
            lblX.AutoSize = true;
            lblX.Location = new Point(20, 75);
            lblX.Name = "lblX";
            lblX.Size = new Size(36, 15);
            lblX.TabIndex = 3;
            lblX.Text = "x (m)";
            //
            // nudX
            //
            nudX.DecimalPlaces = 2;
            nudX.Location = new Point(160, 72);
            nudX.Name = "nudX";
            nudX.Size = new Size(120, 23);
            nudX.TabIndex = 4;
            //
            // lblY
            //
            lblY.AutoSize = true;
            lblY.Location = new Point(20, 107);
            lblY.Name = "lblY";
            lblY.Size = new Size(36, 15);
            lblY.TabIndex = 5;
            lblY.Text = "y (m)";
            //
            // nudY
            //
            nudY.DecimalPlaces = 2;
            nudY.Location = new Point(160, 104);
            nudY.Name = "nudY";
            nudY.Size = new Size(120, 23);
            nudY.TabIndex = 6;
            //
            // lblZ
            //
            lblZ.AutoSize = true;
            lblZ.Location = new Point(20, 139);
            lblZ.Name = "lblZ";
            lblZ.Size = new Size(108, 15);
            lblZ.TabIndex = 7;
            lblZ.Text = "z, altura nivel (m)";
            //
            // nudZ
            //
            nudZ.DecimalPlaces = 2;
            nudZ.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudZ.Location = new Point(160, 136);
            nudZ.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudZ.Name = "nudZ";
            nudZ.Size = new Size(120, 23);
            nudZ.TabIndex = 8;
            //
            // lblA
            //
            lblA.AutoSize = true;
            lblA.Location = new Point(20, 171);
            lblA.Name = "lblA";
            lblA.Size = new Size(110, 15);
            lblA.TabIndex = 9;
            lblA.Text = "a (peso distancia)";
            //
            // nudA
            //
            nudA.DecimalPlaces = 2;
            nudA.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudA.Location = new Point(160, 168);
            nudA.Name = "nudA";
            nudA.Size = new Size(120, 23);
            nudA.TabIndex = 10;
            //
            // lblB
            //
            lblB.AutoSize = true;
            lblB.Location = new Point(20, 203);
            lblB.Name = "lblB";
            lblB.Size = new Size(93, 15);
            lblB.TabIndex = 11;
            lblB.Text = "b (peso altura)";
            //
            // nudB
            //
            nudB.DecimalPlaces = 3;
            nudB.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudB.Location = new Point(160, 200);
            nudB.Name = "nudB";
            nudB.Size = new Size(120, 23);
            nudB.TabIndex = 12;
            //
            // btnCalcularCosto
            //
            btnCalcularCosto.Location = new Point(160, 234);
            btnCalcularCosto.Name = "btnCalcularCosto";
            btnCalcularCosto.Size = new Size(150, 30);
            btnCalcularCosto.TabIndex = 13;
            btnCalcularCosto.Text = "Calcular costo";
            btnCalcularCosto.UseVisualStyleBackColor = true;
            btnCalcularCosto.Click += btnCalcularCosto_Click;
            //
            // lblResultadoCosto
            //
            lblResultadoCosto.Font = new Font("Segoe UI", 10F);
            lblResultadoCosto.Location = new Point(480, 72);
            lblResultadoCosto.Name = "lblResultadoCosto";
            lblResultadoCosto.Size = new Size(430, 160);
            lblResultadoCosto.TabIndex = 14;
            //
            // btnSugerir
            //
            btnSugerir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSugerir.Location = new Point(20, 282);
            btnSugerir.Name = "btnSugerir";
            btnSugerir.Size = new Size(260, 32);
            btnSugerir.TabIndex = 15;
            btnSugerir.Text = "Sugerir ubicaciones de menor costo";
            btnSugerir.UseVisualStyleBackColor = true;
            btnSugerir.Click += btnSugerir_Click;
            //
            // lblSimulacion
            //
            lblSimulacion.ForeColor = Color.DarkGreen;
            lblSimulacion.Location = new Point(295, 278);
            lblSimulacion.Name = "lblSimulacion";
            lblSimulacion.Size = new Size(615, 40);
            lblSimulacion.TabIndex = 16;
            //
            // dgvSugerencias
            //
            dgvSugerencias.AllowUserToAddRows = false;
            dgvSugerencias.AllowUserToDeleteRows = false;
            dgvSugerencias.AllowUserToResizeRows = false;
            dgvSugerencias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSugerencias.BackgroundColor = SystemColors.Window;
            dgvSugerencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSugerencias.Columns.AddRange(new DataGridViewColumn[] { colSugCodigo, colSugNombre, colSugActual, colSugCostoActual, colSugSugerida, colSugCostoSugerido, colSugAhorro });
            dgvSugerencias.Location = new Point(20, 325);
            dgvSugerencias.Name = "dgvSugerencias";
            dgvSugerencias.ReadOnly = true;
            dgvSugerencias.RowHeadersVisible = false;
            dgvSugerencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSugerencias.Size = new Size(890, 235);
            dgvSugerencias.TabIndex = 17;
            //
            // colSugCodigo
            //
            colSugCodigo.HeaderText = "Código";
            colSugCodigo.Name = "colSugCodigo";
            colSugCodigo.ReadOnly = true;
            colSugCodigo.Width = 75;
            //
            // colSugNombre
            //
            colSugNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSugNombre.HeaderText = "Producto";
            colSugNombre.Name = "colSugNombre";
            colSugNombre.ReadOnly = true;
            //
            // colSugActual
            //
            colSugActual.HeaderText = "Ubicación actual";
            colSugActual.Name = "colSugActual";
            colSugActual.ReadOnly = true;
            colSugActual.Width = 90;
            //
            // colSugCostoActual
            //
            colSugCostoActual.HeaderText = "Costo actual";
            colSugCostoActual.Name = "colSugCostoActual";
            colSugCostoActual.ReadOnly = true;
            colSugCostoActual.Width = 85;
            //
            // colSugSugerida
            //
            colSugSugerida.HeaderText = "Ubicación sugerida";
            colSugSugerida.Name = "colSugSugerida";
            colSugSugerida.ReadOnly = true;
            colSugSugerida.Width = 90;
            //
            // colSugCostoSugerido
            //
            colSugCostoSugerido.HeaderText = "Costo sugerido";
            colSugCostoSugerido.Name = "colSugCostoSugerido";
            colSugCostoSugerido.ReadOnly = true;
            colSugCostoSugerido.Width = 85;
            //
            // colSugAhorro
            //
            colSugAhorro.HeaderText = "Ahorro";
            colSugAhorro.Name = "colSugAhorro";
            colSugAhorro.ReadOnly = true;
            colSugAhorro.Width = 75;
            //
            // tabLagrange
            //
            tabLagrange.Controls.Add(lblResultadoLagrange);
            tabLagrange.Controls.Add(dgvResultadoLagrange);
            tabLagrange.Controls.Add(btnEjemploLagrange);
            tabLagrange.Controls.Add(btnResolverLagrange);
            tabLagrange.Controls.Add(nudVolumenDisponible);
            tabLagrange.Controls.Add(lblVolumenDisponible);
            tabLagrange.Controls.Add(lblLagExplicacion);
            tabLagrange.Controls.Add(dgvLagrange);
            tabLagrange.Location = new Point(4, 24);
            tabLagrange.Name = "tabLagrange";
            tabLagrange.Padding = new Padding(3);
            tabLagrange.Size = new Size(932, 572);
            tabLagrange.TabIndex = 3;
            tabLagrange.Text = "Cantidad a pedir (Lagrange)";
            tabLagrange.UseVisualStyleBackColor = true;
            //
            // dgvLagrange
            //
            dgvLagrange.BackgroundColor = SystemColors.Window;
            dgvLagrange.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLagrange.Columns.AddRange(new DataGridViewColumn[] { colLagProducto, colLagDemanda, colLagCostoPedido, colLagMantener, colLagVolumen });
            dgvLagrange.Location = new Point(20, 20);
            dgvLagrange.Name = "dgvLagrange";
            dgvLagrange.Size = new Size(600, 140);
            dgvLagrange.TabIndex = 0;
            //
            // colLagProducto
            //
            colLagProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colLagProducto.HeaderText = "Producto";
            colLagProducto.Name = "colLagProducto";
            //
            // colLagDemanda
            //
            colLagDemanda.HeaderText = "D (unid./año)";
            colLagDemanda.Name = "colLagDemanda";
            colLagDemanda.Width = 85;
            //
            // colLagCostoPedido
            //
            colLagCostoPedido.HeaderText = "S ($/pedido)";
            colLagCostoPedido.Name = "colLagCostoPedido";
            colLagCostoPedido.Width = 85;
            //
            // colLagMantener
            //
            colLagMantener.HeaderText = "h ($/unid./año)";
            colLagMantener.Name = "colLagMantener";
            colLagMantener.Width = 90;
            //
            // colLagVolumen
            //
            colLagVolumen.HeaderText = "v (m³/unid.)";
            colLagVolumen.Name = "colLagVolumen";
            colLagVolumen.Width = 85;
            //
            // lblLagExplicacion
            //
            lblLagExplicacion.ForeColor = Color.DimGray;
            lblLagExplicacion.Location = new Point(640, 20);
            lblLagExplicacion.Name = "lblLagExplicacion";
            lblLagExplicacion.Size = new Size(270, 170);
            lblLagExplicacion.TabIndex = 1;
            lblLagExplicacion.Text = "Minimizar Σ (D·S/Q + h·Q/2)\r\nsujeto a Σ v·Q ≤ V\r\n\r\n" +
                "Q(λ) = √(2·D·S / (h + 2λ·v))\r\n\r\n" +
                "Si con λ = 0 cabe, esa es la respuesta. Si no, λ se busca por bisección. Puede editar la tabla (2 o 3 productos).";
            //
            // lblVolumenDisponible
            //
            lblVolumenDisponible.AutoSize = true;
            lblVolumenDisponible.Location = new Point(20, 177);
            lblVolumenDisponible.Name = "lblVolumenDisponible";
            lblVolumenDisponible.Size = new Size(152, 15);
            lblVolumenDisponible.TabIndex = 2;
            lblVolumenDisponible.Text = "Volumen disponible V (m³)";
            //
            // nudVolumenDisponible
            //
            nudVolumenDisponible.DecimalPlaces = 2;
            nudVolumenDisponible.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            nudVolumenDisponible.Location = new Point(190, 174);
            nudVolumenDisponible.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudVolumenDisponible.Name = "nudVolumenDisponible";
            nudVolumenDisponible.Size = new Size(100, 23);
            nudVolumenDisponible.TabIndex = 3;
            nudVolumenDisponible.Value = new decimal(new int[] { 4, 0, 0, 0 });
            //
            // btnResolverLagrange
            //
            btnResolverLagrange.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnResolverLagrange.Location = new Point(310, 170);
            btnResolverLagrange.Name = "btnResolverLagrange";
            btnResolverLagrange.Size = new Size(120, 30);
            btnResolverLagrange.TabIndex = 4;
            btnResolverLagrange.Text = "Resolver";
            btnResolverLagrange.UseVisualStyleBackColor = true;
            btnResolverLagrange.Click += btnResolverLagrange_Click;
            //
            // btnEjemploLagrange
            //
            btnEjemploLagrange.Location = new Point(440, 170);
            btnEjemploLagrange.Name = "btnEjemploLagrange";
            btnEjemploLagrange.Size = new Size(140, 30);
            btnEjemploLagrange.TabIndex = 5;
            btnEjemploLagrange.Text = "Cargar ejemplo";
            btnEjemploLagrange.UseVisualStyleBackColor = true;
            btnEjemploLagrange.Click += btnEjemploLagrange_Click;
            //
            // dgvResultadoLagrange
            //
            dgvResultadoLagrange.AllowUserToAddRows = false;
            dgvResultadoLagrange.AllowUserToDeleteRows = false;
            dgvResultadoLagrange.AllowUserToResizeRows = false;
            dgvResultadoLagrange.BackgroundColor = SystemColors.Window;
            dgvResultadoLagrange.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultadoLagrange.Columns.AddRange(new DataGridViewColumn[] { colResProducto, colResLibre, colResOptimo, colResVolumen });
            dgvResultadoLagrange.Location = new Point(20, 215);
            dgvResultadoLagrange.Name = "dgvResultadoLagrange";
            dgvResultadoLagrange.ReadOnly = true;
            dgvResultadoLagrange.RowHeadersVisible = false;
            dgvResultadoLagrange.Size = new Size(600, 130);
            dgvResultadoLagrange.TabIndex = 6;
            //
            // colResProducto
            //
            colResProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colResProducto.HeaderText = "Producto";
            colResProducto.Name = "colResProducto";
            colResProducto.ReadOnly = true;
            //
            // colResLibre
            //
            colResLibre.HeaderText = "Q sin restricción (λ = 0)";
            colResLibre.Name = "colResLibre";
            colResLibre.ReadOnly = true;
            colResLibre.Width = 110;
            //
            // colResOptimo
            //
            colResOptimo.HeaderText = "Q óptimo";
            colResOptimo.Name = "colResOptimo";
            colResOptimo.ReadOnly = true;
            colResOptimo.Width = 90;
            //
            // colResVolumen
            //
            colResVolumen.HeaderText = "Volumen (m³)";
            colResVolumen.Name = "colResVolumen";
            colResVolumen.ReadOnly = true;
            colResVolumen.Width = 90;
            //
            // lblResultadoLagrange
            //
            lblResultadoLagrange.Font = new Font("Segoe UI", 10F);
            lblResultadoLagrange.Location = new Point(20, 355);
            lblResultadoLagrange.Name = "lblResultadoLagrange";
            lblResultadoLagrange.Size = new Size(890, 90);
            lblResultadoLagrange.TabIndex = 7;
            //
            // FrmCalculadora
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(940, 600);
            Controls.Add(tabCalculadora);
            MinimumSize = new Size(956, 600);
            Name = "FrmCalculadora";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Calculadora matemática";
            Load += FrmCalculadora_Load;
            tabCalculadora.ResumeLayout(false);
            tabMonticulo.ResumeLayout(false);
            tabMonticulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAltura).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRadio).EndInit();
            tabArea.ResumeLayout(false);
            tabArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAlturaUtil).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLargoPasillo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAnchoPasillo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidadPasillos).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLargo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAncho).EndInit();
            tabCosto.ResumeLayout(false);
            tabCosto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSugerencias).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudB).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudA).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            tabLagrange.ResumeLayout(false);
            tabLagrange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResultadoLagrange).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVolumenDisponible).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLagrange).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabCalculadora;
        private TabPage tabMonticulo;
        private Label lblResultadoMonticulo;
        private Label lblFormulaMonticulo;
        private Button btnCalcularVolumen;
        private ComboBox cmbAgregado;
        private Label lblAgregado;
        private NumericUpDown nudAltura;
        private Label lblAltura;
        private NumericUpDown nudRadio;
        private Label lblRadio;
        private TabPage tabArea;
        private Label lblResultadoArea;
        private Button btnCalcularArea;
        private NumericUpDown nudAlturaUtil;
        private Label lblAlturaUtil;
        private NumericUpDown nudLargoPasillo;
        private Label lblLargoPasillo;
        private NumericUpDown nudAnchoPasillo;
        private Label lblAnchoPasillo;
        private NumericUpDown nudCantidadPasillos;
        private Label lblCantidadPasillos;
        private NumericUpDown nudLargo;
        private Label lblLargo;
        private NumericUpDown nudAncho;
        private Label lblAncho;
        private TabPage tabCosto;
        private DataGridView dgvSugerencias;
        private DataGridViewTextBoxColumn colSugCodigo;
        private DataGridViewTextBoxColumn colSugNombre;
        private DataGridViewTextBoxColumn colSugActual;
        private DataGridViewTextBoxColumn colSugCostoActual;
        private DataGridViewTextBoxColumn colSugSugerida;
        private DataGridViewTextBoxColumn colSugCostoSugerido;
        private DataGridViewTextBoxColumn colSugAhorro;
        private Label lblSimulacion;
        private Button btnSugerir;
        private Label lblResultadoCosto;
        private Button btnCalcularCosto;
        private NumericUpDown nudB;
        private Label lblB;
        private NumericUpDown nudA;
        private Label lblA;
        private NumericUpDown nudZ;
        private Label lblZ;
        private NumericUpDown nudY;
        private Label lblY;
        private NumericUpDown nudX;
        private Label lblX;
        private Label lblDatosProducto;
        private ComboBox cmbProductoCosto;
        private Label lblProductoCosto;
        private TabPage tabLagrange;
        private Label lblResultadoLagrange;
        private DataGridView dgvResultadoLagrange;
        private DataGridViewTextBoxColumn colResProducto;
        private DataGridViewTextBoxColumn colResLibre;
        private DataGridViewTextBoxColumn colResOptimo;
        private DataGridViewTextBoxColumn colResVolumen;
        private Button btnEjemploLagrange;
        private Button btnResolverLagrange;
        private NumericUpDown nudVolumenDisponible;
        private Label lblVolumenDisponible;
        private Label lblLagExplicacion;
        private DataGridView dgvLagrange;
        private DataGridViewTextBoxColumn colLagProducto;
        private DataGridViewTextBoxColumn colLagDemanda;
        private DataGridViewTextBoxColumn colLagCostoPedido;
        private DataGridViewTextBoxColumn colLagMantener;
        private DataGridViewTextBoxColumn colLagVolumen;
    }
}
