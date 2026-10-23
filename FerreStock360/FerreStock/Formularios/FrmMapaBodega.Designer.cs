namespace FerreStock.Formularios
{
    partial class FrmMapaBodega
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
            pnlMapa = new PanelDobleBuffer();
            pnlLateral = new Panel();
            lblLeyenda = new Label();
            chkNumeros = new CheckBox();
            lblSinUbicacion = new Label();
            lstParadas = new ListBox();
            lblParadas = new Label();
            lblTiempo = new Label();
            lblDistancia = new Label();
            cmbPedido = new ComboBox();
            lblPedido = new Label();
            pnlLateral.SuspendLayout();
            SuspendLayout();
            //
            // pnlMapa
            //
            pnlMapa.BackColor = Color.White;
            pnlMapa.Dock = DockStyle.Fill;
            pnlMapa.Location = new Point(0, 0);
            pnlMapa.Name = "pnlMapa";
            pnlMapa.Size = new Size(820, 600);
            pnlMapa.TabIndex = 0;
            pnlMapa.Paint += pnlMapa_Paint;
            //
            // pnlLateral
            //
            pnlLateral.Controls.Add(lblLeyenda);
            pnlLateral.Controls.Add(chkNumeros);
            pnlLateral.Controls.Add(lblSinUbicacion);
            pnlLateral.Controls.Add(lstParadas);
            pnlLateral.Controls.Add(lblParadas);
            pnlLateral.Controls.Add(lblTiempo);
            pnlLateral.Controls.Add(lblDistancia);
            pnlLateral.Controls.Add(cmbPedido);
            pnlLateral.Controls.Add(lblPedido);
            pnlLateral.Dock = DockStyle.Right;
            pnlLateral.Location = new Point(820, 0);
            pnlLateral.Name = "pnlLateral";
            pnlLateral.Padding = new Padding(10);
            pnlLateral.Size = new Size(320, 600);
            pnlLateral.TabIndex = 1;
            //
            // lblPedido
            //
            lblPedido.AutoSize = true;
            lblPedido.Location = new Point(12, 15);
            lblPedido.Name = "lblPedido";
            lblPedido.Size = new Size(137, 15);
            lblPedido.TabIndex = 0;
            lblPedido.Text = "Pedido (sin despachar):";
            //
            // cmbPedido
            //
            cmbPedido.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPedido.Location = new Point(12, 35);
            cmbPedido.Name = "cmbPedido";
            cmbPedido.Size = new Size(295, 23);
            cmbPedido.TabIndex = 1;
            cmbPedido.SelectedIndexChanged += cmbPedido_SelectedIndexChanged;
            //
            // lblDistancia
            //
            lblDistancia.AutoSize = true;
            lblDistancia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDistancia.Location = new Point(12, 72);
            lblDistancia.Name = "lblDistancia";
            lblDistancia.Size = new Size(80, 19);
            lblDistancia.TabIndex = 2;
            lblDistancia.Text = "Distancia: -";
            //
            // lblTiempo
            //
            lblTiempo.AutoSize = true;
            lblTiempo.Location = new Point(12, 96);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(110, 15);
            lblTiempo.TabIndex = 3;
            lblTiempo.Text = "Tiempo estimado: -";
            //
            // lblParadas
            //
            lblParadas.AutoSize = true;
            lblParadas.Location = new Point(12, 126);
            lblParadas.Name = "lblParadas";
            lblParadas.Size = new Size(116, 15);
            lblParadas.TabIndex = 4;
            lblParadas.Text = "Paradas, en orden:";
            //
            // lstParadas
            //
            lstParadas.HorizontalScrollbar = true;
            lstParadas.IntegralHeight = false;
            lstParadas.Location = new Point(12, 146);
            lstParadas.Name = "lstParadas";
            lstParadas.Size = new Size(295, 200);
            lstParadas.TabIndex = 5;
            //
            // lblSinUbicacion
            //
            lblSinUbicacion.ForeColor = Color.Firebrick;
            lblSinUbicacion.Location = new Point(12, 352);
            lblSinUbicacion.Name = "lblSinUbicacion";
            lblSinUbicacion.Size = new Size(295, 36);
            lblSinUbicacion.TabIndex = 6;
            //
            // chkNumeros
            //
            chkNumeros.AutoSize = true;
            chkNumeros.Checked = true;
            chkNumeros.CheckState = CheckState.Checked;
            chkNumeros.Location = new Point(12, 395);
            chkNumeros.Name = "chkNumeros";
            chkNumeros.Size = new Size(170, 19);
            chkNumeros.TabIndex = 7;
            chkNumeros.Text = "Mostrar número de nodos";
            chkNumeros.UseVisualStyleBackColor = true;
            chkNumeros.CheckedChanged += chkNumeros_CheckedChanged;
            //
            // lblLeyenda
            //
            lblLeyenda.ForeColor = Color.DimGray;
            lblLeyenda.Location = new Point(12, 425);
            lblLeyenda.Name = "lblLeyenda";
            lblLeyenda.Size = new Size(295, 110);
            lblLeyenda.TabIndex = 8;
            lblLeyenda.Text = "Gris: pasillos.  Azul: ruta de alistamiento (Dijkstra desde el mostrador, yendo siempre a la parada más cercana); punteado: regreso.  " +
                "Naranja: estantes donde hay que recoger productos.  Los números en azul son el orden de las paradas.";
            //
            // FrmMapaBodega
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1140, 600);
            Controls.Add(pnlMapa);
            Controls.Add(pnlLateral);
            MinimumSize = new Size(900, 500);
            Name = "FrmMapaBodega";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Mapa de bodega";
            Load += FrmMapaBodega_Load;
            pnlLateral.ResumeLayout(false);
            pnlLateral.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PanelDobleBuffer pnlMapa;
        private Panel pnlLateral;
        private Label lblLeyenda;
        private CheckBox chkNumeros;
        private Label lblSinUbicacion;
        private ListBox lstParadas;
        private Label lblParadas;
        private Label lblTiempo;
        private Label lblDistancia;
        private ComboBox cmbPedido;
        private Label lblPedido;
    }
}
