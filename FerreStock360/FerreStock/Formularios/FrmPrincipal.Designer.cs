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
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            btnProductos = new Button();
            btnMovimientos = new Button();
            btnPedidos = new Button();
            menuPrincipal.SuspendLayout();
            barraEstado.SuspendLayout();
            SuspendLayout();
            //
            // menuPrincipal
            //
            menuPrincipal.Items.AddRange(new ToolStripItem[] { mnuInventario, mnuOperaciones, mnuBodega, mnuReportes });
            menuPrincipal.Location = new Point(0, 0);
            menuPrincipal.Name = "menuPrincipal";
            menuPrincipal.Size = new Size(800, 24);
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
            // barraEstado
            //
            barraEstado.Items.AddRange(new ToolStripItem[] { lblEstadoProductos, lblEstadoBajoMinimo });
            barraEstado.Location = new Point(0, 428);
            barraEstado.Name = "barraEstado";
            barraEstado.Size = new Size(800, 22);
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
            // lblTitulo
            //
            lblTitulo.Anchor = AnchorStyles.Top;
            lblTitulo.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(192, 64, 0);
            lblTitulo.Location = new Point(150, 90);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(500, 50);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "FerreStock 360";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblSubtitulo
            //
            lblSubtitulo.Anchor = AnchorStyles.Top;
            lblSubtitulo.Font = new Font("Segoe UI", 11F);
            lblSubtitulo.ForeColor = Color.DimGray;
            lblSubtitulo.Location = new Point(150, 145);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(500, 25);
            lblSubtitulo.TabIndex = 3;
            lblSubtitulo.Text = "Inventario, pedidos y bodega de la ferretería";
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnProductos
            //
            btnProductos.Anchor = AnchorStyles.Top;
            btnProductos.Font = new Font("Segoe UI", 12F);
            btnProductos.Location = new Point(110, 210);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(180, 50);
            btnProductos.TabIndex = 4;
            btnProductos.Text = "Productos";
            btnProductos.UseVisualStyleBackColor = true;
            btnProductos.Click += btnProductos_Click;
            // 
            // btnMovimientos
            // 
            btnMovimientos.Anchor = AnchorStyles.Top;
            btnMovimientos.Font = new Font("Segoe UI", 12F);
            btnMovimientos.Location = new Point(310, 210);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Size = new Size(180, 50);
            btnMovimientos.TabIndex = 5;
            btnMovimientos.Text = "Movimientos";
            btnMovimientos.UseVisualStyleBackColor = true;
            btnMovimientos.Click += btnMovimientos_Click;
            // 
            // btnPedidos
            // 
            btnPedidos.Anchor = AnchorStyles.Top;
            btnPedidos.Font = new Font("Segoe UI", 12F);
            btnPedidos.Location = new Point(510, 210);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.Size = new Size(180, 50);
            btnPedidos.TabIndex = 6;
            btnPedidos.Text = "Pedidos";
            btnPedidos.UseVisualStyleBackColor = true;
            btnPedidos.Click += btnPedidos_Click;
            //
            // FrmPrincipal
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPedidos);
            Controls.Add(btnMovimientos);
            Controls.Add(btnProductos);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTitulo);
            Controls.Add(barraEstado);
            Controls.Add(menuPrincipal);
            MainMenuStrip = menuPrincipal;
            MinimumSize = new Size(600, 400);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FerreStock 360";
            Load += FrmPrincipal_Load;
            menuPrincipal.ResumeLayout(false);
            menuPrincipal.PerformLayout();
            barraEstado.ResumeLayout(false);
            barraEstado.PerformLayout();
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
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Button btnProductos;
        private Button btnMovimientos;
        private Button btnPedidos;
    }
}
