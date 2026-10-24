namespace FerreStock.Formularios
{
    partial class FrmPrincipal
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
            menuPrincipal = new MenuStrip();
            mnuInventario = new ToolStripMenuItem();
            mnuProductos = new ToolStripMenuItem();
            mnuSeparador = new ToolStripSeparator();
            mnuSalir = new ToolStripMenuItem();
            mnuOperaciones = new ToolStripMenuItem();
            mnuMovimientos = new ToolStripMenuItem();
            mnuPedidos = new ToolStripMenuItem();
            mnuBodega = new ToolStripMenuItem();
            mnuMapaBodega = new ToolStripMenuItem();
            mnuCalculadora = new ToolStripMenuItem();
            mnuReportes = new ToolStripMenuItem();
            mnuIndicadores = new ToolStripMenuItem();
            mnuComparador = new ToolStripMenuItem();
            barraEstado = new StatusStrip();
            lblEstadoProductos = new ToolStripStatusLabel();
            lblEstadoBajoMinimo = new ToolStripStatusLabel();
            pnlEncabezado = new Panel();
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            lblLema = new Label();
            tablaModulos = new TableLayoutPanel();
            btnProductos = new Button();
            btnMovimientos = new Button();
            btnPedidos = new Button();
            btnMapa = new Button();
            btnCalculadora = new Button();
            btnIndicadores = new Button();
            btnComparador = new Button();
            btnAcercaDe = new Button();
            lblEstadoPedidos = new ToolStripStatusLabel();
            mnuAyuda = new ToolStripMenuItem();
            mnuAcercaDe = new ToolStripMenuItem();
            menuPrincipal.SuspendLayout();
            barraEstado.SuspendLayout();
            pnlEncabezado.SuspendLayout();
            tablaModulos.SuspendLayout();
            SuspendLayout();
            //
            // menuPrincipal
            //
            menuPrincipal.Items.AddRange(new ToolStripItem[] { mnuInventario, mnuOperaciones, mnuBodega, mnuReportes, mnuAyuda });
            menuPrincipal.Location = new Point(0, 0);
            menuPrincipal.Name = "menuPrincipal";
            menuPrincipal.Size = new Size(960, 24);
            menuPrincipal.TabIndex = 0;
            //
            // mnuInventario
            //
            mnuInventario.DropDownItems.AddRange(new ToolStripItem[] { mnuProductos, mnuSeparador, mnuSalir });
            mnuInventario.Name = "mnuInventario";
            mnuInventario.Size = new Size(72, 20);
            mnuInventario.Text = "&Inventario";
            //
            // mnuProductos
            //
            mnuProductos.Name = "mnuProductos";
            mnuProductos.ShortcutKeys = Keys.Control | Keys.P;
            mnuProductos.Size = new Size(180, 22);
            mnuProductos.Text = "&Productos";
            mnuProductos.Click += mnuProductos_Click;
            //
            // mnuSeparador
            //
            mnuSeparador.Name = "mnuSeparador";
            mnuSeparador.Size = new Size(177, 6);
            //
            // mnuSalir
            //
            mnuSalir.Name = "mnuSalir";
            mnuSalir.Size = new Size(180, 22);
            mnuSalir.Text = "&Salir";
            mnuSalir.Click += mnuSalir_Click;
            //
            // mnuOperaciones
            //
            mnuOperaciones.DropDownItems.AddRange(new ToolStripItem[] { mnuMovimientos, mnuPedidos });
            mnuOperaciones.Name = "mnuOperaciones";
            mnuOperaciones.Size = new Size(85, 20);
            mnuOperaciones.Text = "&Operaciones";
            //
            // mnuMovimientos
            //
            mnuMovimientos.Name = "mnuMovimientos";
            mnuMovimientos.ShortcutKeys = Keys.Control | Keys.M;
            mnuMovimientos.Size = new Size(180, 22);
            mnuMovimientos.Text = "&Movimientos";
            mnuMovimientos.Click += mnuMovimientos_Click;
            //
            // mnuPedidos
            //
            mnuPedidos.Name = "mnuPedidos";
            mnuPedidos.ShortcutKeys = Keys.Control | Keys.D;
            mnuPedidos.Size = new Size(180, 22);
            mnuPedidos.Text = "Pe&didos";
            mnuPedidos.Click += mnuPedidos_Click;
            //
            // mnuBodega
            //
            mnuBodega.DropDownItems.AddRange(new ToolStripItem[] { mnuMapaBodega, mnuCalculadora });
            mnuBodega.Name = "mnuBodega";
            mnuBodega.Size = new Size(59, 20);
            mnuBodega.Text = "&Bodega";
            //
            // mnuMapaBodega
            //
            mnuMapaBodega.Name = "mnuMapaBodega";
            mnuMapaBodega.ShortcutKeys = Keys.Control | Keys.B;
            mnuMapaBodega.Size = new Size(220, 22);
            mnuMapaBodega.Text = "&Mapa de bodega";
            mnuMapaBodega.Click += mnuMapaBodega_Click;
            //
            // mnuCalculadora
            //
            mnuCalculadora.Name = "mnuCalculadora";
            mnuCalculadora.Size = new Size(220, 22);
            mnuCalculadora.Text = "&Calculadora matemática";
            mnuCalculadora.Click += mnuCalculadora_Click;
            //
            // mnuReportes
            //
            mnuReportes.DropDownItems.AddRange(new ToolStripItem[] { mnuIndicadores, mnuComparador });
            mnuReportes.Name = "mnuReportes";
            mnuReportes.Size = new Size(65, 20);
            mnuReportes.Text = "&Reportes";
            //
            // mnuIndicadores
            //
            mnuIndicadores.Name = "mnuIndicadores";
            mnuIndicadores.ShortcutKeys = Keys.Control | Keys.I;
            mnuIndicadores.Size = new Size(260, 22);
            mnuIndicadores.Text = "&Indicadores";
            mnuIndicadores.Click += mnuIndicadores_Click;
            //
            // mnuComparador
            //
            mnuComparador.Name = "mnuComparador";
            mnuComparador.Size = new Size(260, 22);
            mnuComparador.Text = "&Comparador de algoritmos";
            mnuComparador.Click += mnuComparador_Click;
            //
            // mnuAyuda
            //
            mnuAyuda.DropDownItems.AddRange(new ToolStripItem[] { mnuAcercaDe });
            mnuAyuda.Name = "mnuAyuda";
            mnuAyuda.Size = new Size(53, 20);
            mnuAyuda.Text = "A&yuda";
            //
            // mnuAcercaDe
            //
            mnuAcercaDe.Name = "mnuAcercaDe";
            mnuAcercaDe.Size = new Size(180, 22);
            mnuAcercaDe.Text = "&Acerca de FerreStock 360";
            mnuAcercaDe.Click += mnuAcercaDe_Click;
            //
            // barraEstado
            // 
            barraEstado.Items.AddRange(new ToolStripItem[] { lblEstadoProductos, lblEstadoBajoMinimo, lblEstadoPedidos });
            barraEstado.BackColor = Color.White;
            barraEstado.Location = new Point(0, 538);
            barraEstado.Name = "barraEstado";
            barraEstado.Size = new Size(960, 22);
            barraEstado.TabIndex = 1;
            //
            // lblEstadoProductos
            //
            lblEstadoProductos.Name = "lblEstadoProductos";
            lblEstadoProductos.Size = new Size(80, 17);
            lblEstadoProductos.Text = "Productos: 0";
            //
            // lblEstadoBajoMinimo
            //
            lblEstadoBajoMinimo.Margin = new Padding(20, 3, 0, 2);
            lblEstadoBajoMinimo.Name = "lblEstadoBajoMinimo";
            lblEstadoBajoMinimo.Size = new Size(125, 17);
            lblEstadoBajoMinimo.Text = "Bajo stock mínimo: 0";
            //
            // lblEstadoPedidos
            //
            lblEstadoPedidos.Margin = new Padding(20, 3, 0, 2);
            lblEstadoPedidos.Name = "lblEstadoPedidos";
            lblEstadoPedidos.Size = new Size(125, 17);
            lblEstadoPedidos.Text = "Pedidos pendientes: 0";
            //
            // pnlEncabezado
            //
            pnlEncabezado.BackColor = Color.FromArgb(30, 44, 60);
            pnlEncabezado.BackgroundImageLayout = ImageLayout.Zoom;
            pnlEncabezado.Controls.Add(lblLema);
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 24);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(960, 240);
            pnlEncabezado.TabIndex = 2;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(36, 55);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "FerreStock 360";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.BackColor = Color.Transparent;
            lblSubtitulo.Font = new Font("Segoe UI", 13F);
            lblSubtitulo.ForeColor = Color.FromArgb(243, 156, 18);
            lblSubtitulo.Location = new Point(42, 118);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Inventario, pedidos y bodega de la ferretería";
            //
            // lblLema
            //
            lblLema.AutoSize = true;
            lblLema.BackColor = Color.Transparent;
            lblLema.Font = new Font("Segoe UI", 9.5F);
            lblLema.ForeColor = Color.FromArgb(189, 195, 199);
            lblLema.Location = new Point(43, 150);
            lblLema.Name = "lblLema";
            lblLema.TabIndex = 2;
            lblLema.Text = "Proyecto integrador SIGLO 360  ·  Equipo 2: Ferretería";
            //
            // tablaModulos
            //
            tablaModulos.BackColor = Color.FromArgb(244, 246, 248);
            tablaModulos.ColumnCount = 4;
            tablaModulos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaModulos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaModulos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaModulos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaModulos.Controls.Add(btnProductos, 0, 0);
            tablaModulos.Controls.Add(btnMovimientos, 1, 0);
            tablaModulos.Controls.Add(btnPedidos, 2, 0);
            tablaModulos.Controls.Add(btnMapa, 3, 0);
            tablaModulos.Controls.Add(btnCalculadora, 0, 1);
            tablaModulos.Controls.Add(btnIndicadores, 1, 1);
            tablaModulos.Controls.Add(btnComparador, 2, 1);
            tablaModulos.Controls.Add(btnAcercaDe, 3, 1);
            tablaModulos.Dock = DockStyle.Fill;
            tablaModulos.Location = new Point(0, 264);
            tablaModulos.Name = "tablaModulos";
            tablaModulos.Padding = new Padding(20, 16, 20, 16);
            tablaModulos.RowCount = 2;
            tablaModulos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tablaModulos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tablaModulos.Size = new Size(960, 274);
            tablaModulos.TabIndex = 3;
            //
            // btnProductos
            //
            btnProductos.BackColor = Color.FromArgb(41, 128, 185);
            btnProductos.Cursor = Cursors.Hand;
            btnProductos.Dock = DockStyle.Fill;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.FlatAppearance.MouseOverBackColor = Color.FromArgb(31, 97, 141);
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.Font = new Font("Segoe UI", 11F);
            btnProductos.ForeColor = Color.White;
            btnProductos.Margin = new Padding(8);
            btnProductos.Name = "btnProductos";
            btnProductos.TabIndex = 0;
            btnProductos.Text = "PRODUCTOS\r\nCrear, editar y buscar";
            btnProductos.UseVisualStyleBackColor = false;
            btnProductos.Click += btnProductos_Click;
            //
            // btnMovimientos
            //
            btnMovimientos.BackColor = Color.FromArgb(22, 160, 133);
            btnMovimientos.Cursor = Cursors.Hand;
            btnMovimientos.Dock = DockStyle.Fill;
            btnMovimientos.FlatAppearance.BorderSize = 0;
            btnMovimientos.FlatAppearance.MouseOverBackColor = Color.FromArgb(17, 122, 101);
            btnMovimientos.FlatStyle = FlatStyle.Flat;
            btnMovimientos.Font = new Font("Segoe UI", 11F);
            btnMovimientos.ForeColor = Color.White;
            btnMovimientos.Margin = new Padding(8);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.TabIndex = 1;
            btnMovimientos.Text = "MOVIMIENTOS\r\nEntradas y salidas";
            btnMovimientos.UseVisualStyleBackColor = false;
            btnMovimientos.Click += btnMovimientos_Click;
            //
            // btnPedidos
            //
            btnPedidos.BackColor = Color.FromArgb(230, 126, 34);
            btnPedidos.Cursor = Cursors.Hand;
            btnPedidos.Dock = DockStyle.Fill;
            btnPedidos.FlatAppearance.BorderSize = 0;
            btnPedidos.FlatAppearance.MouseOverBackColor = Color.FromArgb(186, 99, 22);
            btnPedidos.FlatStyle = FlatStyle.Flat;
            btnPedidos.Font = new Font("Segoe UI", 11F);
            btnPedidos.ForeColor = Color.White;
            btnPedidos.Margin = new Padding(8);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.TabIndex = 2;
            btnPedidos.Text = "PEDIDOS\r\nAtender por prioridad";
            btnPedidos.UseVisualStyleBackColor = false;
            btnPedidos.Click += btnPedidos_Click;
            //
            // btnMapa
            //
            btnMapa.BackColor = Color.FromArgb(142, 68, 173);
            btnMapa.Cursor = Cursors.Hand;
            btnMapa.Dock = DockStyle.Fill;
            btnMapa.FlatAppearance.BorderSize = 0;
            btnMapa.FlatAppearance.MouseOverBackColor = Color.FromArgb(108, 52, 131);
            btnMapa.FlatStyle = FlatStyle.Flat;
            btnMapa.Font = new Font("Segoe UI", 11F);
            btnMapa.ForeColor = Color.White;
            btnMapa.Margin = new Padding(8);
            btnMapa.Name = "btnMapa";
            btnMapa.TabIndex = 3;
            btnMapa.Text = "MAPA DE BODEGA\r\nRuta de alistamiento";
            btnMapa.UseVisualStyleBackColor = false;
            btnMapa.Click += mnuMapaBodega_Click;
            //
            // btnCalculadora
            //
            btnCalculadora.BackColor = Color.FromArgb(52, 73, 94);
            btnCalculadora.Cursor = Cursors.Hand;
            btnCalculadora.Dock = DockStyle.Fill;
            btnCalculadora.FlatAppearance.BorderSize = 0;
            btnCalculadora.FlatAppearance.MouseOverBackColor = Color.FromArgb(36, 52, 71);
            btnCalculadora.FlatStyle = FlatStyle.Flat;
            btnCalculadora.Font = new Font("Segoe UI", 11F);
            btnCalculadora.ForeColor = Color.White;
            btnCalculadora.Margin = new Padding(8);
            btnCalculadora.Name = "btnCalculadora";
            btnCalculadora.TabIndex = 4;
            btnCalculadora.Text = "CALCULADORA\r\nVolúmenes y Lagrange";
            btnCalculadora.UseVisualStyleBackColor = false;
            btnCalculadora.Click += mnuCalculadora_Click;
            //
            // btnIndicadores
            //
            btnIndicadores.BackColor = Color.FromArgb(39, 174, 96);
            btnIndicadores.Cursor = Cursors.Hand;
            btnIndicadores.Dock = DockStyle.Fill;
            btnIndicadores.FlatAppearance.BorderSize = 0;
            btnIndicadores.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 132, 73);
            btnIndicadores.FlatStyle = FlatStyle.Flat;
            btnIndicadores.Font = new Font("Segoe UI", 11F);
            btnIndicadores.ForeColor = Color.White;
            btnIndicadores.Margin = new Padding(8);
            btnIndicadores.Name = "btnIndicadores";
            btnIndicadores.TabIndex = 5;
            btnIndicadores.Text = "INDICADORES\r\nRotación, ocupación y ABC";
            btnIndicadores.UseVisualStyleBackColor = false;
            btnIndicadores.Click += mnuIndicadores_Click;
            //
            // btnComparador
            //
            btnComparador.BackColor = Color.FromArgb(192, 57, 43);
            btnComparador.Cursor = Cursors.Hand;
            btnComparador.Dock = DockStyle.Fill;
            btnComparador.FlatAppearance.BorderSize = 0;
            btnComparador.FlatAppearance.MouseOverBackColor = Color.FromArgb(146, 43, 33);
            btnComparador.FlatStyle = FlatStyle.Flat;
            btnComparador.Font = new Font("Segoe UI", 11F);
            btnComparador.ForeColor = Color.White;
            btnComparador.Margin = new Padding(8);
            btnComparador.Name = "btnComparador";
            btnComparador.TabIndex = 6;
            btnComparador.Text = "ALGORITMOS\r\nComparar ordenamientos";
            btnComparador.UseVisualStyleBackColor = false;
            btnComparador.Click += mnuComparador_Click;
            //
            // btnAcercaDe
            //
            btnAcercaDe.BackColor = Color.FromArgb(127, 140, 141);
            btnAcercaDe.Cursor = Cursors.Hand;
            btnAcercaDe.Dock = DockStyle.Fill;
            btnAcercaDe.FlatAppearance.BorderSize = 0;
            btnAcercaDe.FlatAppearance.MouseOverBackColor = Color.FromArgb(95, 106, 106);
            btnAcercaDe.FlatStyle = FlatStyle.Flat;
            btnAcercaDe.Font = new Font("Segoe UI", 11F);
            btnAcercaDe.ForeColor = Color.White;
            btnAcercaDe.Margin = new Padding(8);
            btnAcercaDe.Name = "btnAcercaDe";
            btnAcercaDe.TabIndex = 7;
            btnAcercaDe.Text = "ACERCA DE\r\nEquipo y datos";
            btnAcercaDe.UseVisualStyleBackColor = false;
            btnAcercaDe.Click += mnuAcercaDe_Click;
            //
            // FrmPrincipal
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 248);
            ClientSize = new Size(960, 560);
            Controls.Add(tablaModulos);
            Controls.Add(pnlEncabezado);
            Controls.Add(barraEstado);
            Controls.Add(menuPrincipal);
            MainMenuStrip = menuPrincipal;
            MinimumSize = new Size(820, 520);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FerreStock 360";
            Load += FrmPrincipal_Load;
            menuPrincipal.ResumeLayout(false);
            menuPrincipal.PerformLayout();
            barraEstado.ResumeLayout(false);
            barraEstado.PerformLayout();
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            tablaModulos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuPrincipal;
        private ToolStripMenuItem mnuInventario;
        private ToolStripMenuItem mnuProductos;
        private ToolStripSeparator mnuSeparador;
        private ToolStripMenuItem mnuSalir;
        private ToolStripMenuItem mnuOperaciones;
        private ToolStripMenuItem mnuMovimientos;
        private ToolStripMenuItem mnuPedidos;
        private ToolStripMenuItem mnuBodega;
        private ToolStripMenuItem mnuMapaBodega;
        private ToolStripMenuItem mnuCalculadora;
        private ToolStripMenuItem mnuReportes;
        private ToolStripMenuItem mnuIndicadores;
        private ToolStripMenuItem mnuComparador;
        private StatusStrip barraEstado;
        private ToolStripStatusLabel lblEstadoProductos;
        private ToolStripStatusLabel lblEstadoBajoMinimo;
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblLema;
        private TableLayoutPanel tablaModulos;
        private Button btnProductos;
        private Button btnMovimientos;
        private Button btnPedidos;
        private Button btnMapa;
        private Button btnCalculadora;
        private Button btnIndicadores;
        private Button btnComparador;
        private Button btnAcercaDe;
        private ToolStripStatusLabel lblEstadoPedidos;
        private ToolStripMenuItem mnuAyuda;
        private ToolStripMenuItem mnuAcercaDe;
    }
}
