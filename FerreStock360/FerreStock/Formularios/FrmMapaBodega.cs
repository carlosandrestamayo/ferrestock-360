using System.Drawing.Drawing2D;
using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Formularios
{
    /// <summary>
    /// Dibuja el plano de la bodega con GDI+ (zonas, estantes, pasillos y nodos) y, al elegir
    /// un pedido, resalta la ruta de alistamiento calculada con Dijkstra desde el mostrador.
    /// Todo el dibujo se hace en el evento Paint del panel: Windows lo llama cada vez que
    /// hay que repintar, y nosotros pedimos repintar con Invalidate().
    /// </summary>
    public partial class FrmMapaBodega : Form
    {
        private readonly BodegaServicio bodega;
        private readonly PedidoServicio pedidos;
        private readonly Pedido? pedidoInicial;

        // Ruta del pedido elegido; null si no hay pedido seleccionado.
        private RutaAlistamiento? ruta;

        // Factor para pasar de metros a píxeles y margen alrededor del dibujo.
        private float escala = 1;
        private const float Margen = 30;

        // Desplazamiento para que el plano quede centrado en el panel.
        private float desplazamientoX = 0;
        private float desplazamientoY = 0;

        // Medidas del dibujo de un estante, en metros, a la derecha de su punto de acceso.
        private const float SeparacionEstante = 0.5f;
        private const float AnchoEstante = 1.8f;
        private const float AltoEstante = 1.8f;

        // TODO estudiante: cambiar los colores de las zonas.
        private static readonly Color[] ColoresZona =
        {
            Color.FromArgb(255, 243, 205), Color.FromArgb(220, 237, 255), Color.FromArgb(222, 247, 222),
            Color.FromArgb(255, 222, 222), Color.FromArgb(238, 228, 255), Color.FromArgb(225, 245, 245)
        };

        public FrmMapaBodega(BodegaServicio bodega, PedidoServicio pedidos, Pedido? pedidoInicial)
        {
            InitializeComponent();
            this.bodega = bodega;
            this.pedidos = pedidos;
            this.pedidoInicial = pedidoInicial;
        }

        private void FrmMapaBodega_Load(object sender, EventArgs e)
        {
            // Se muestran los pedidos sin despachar: primero la cola en orden de atención, luego los alistados.
            cmbPedido.DisplayMember = "Descripcion";
            foreach (Pedido p in pedidos.ObtenerColaEnOrden())
            {
                cmbPedido.Items.Add(p);
            }
            foreach (Pedido p in pedidos.ObtenerPorEstado(EstadoPedido.Alistado))
            {
                cmbPedido.Items.Add(p);
            }

            if (pedidoInicial != null)
            {
                cmbPedido.SelectedItem = pedidoInicial;
            }
            MostrarDatosRuta();
        }

        private void cmbPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pedido? pedido = cmbPedido.SelectedItem as Pedido;
            ruta = null;
            if (pedido != null)
            {
                try
                {
                    ruta = bodega.CalcularRuta(pedido);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo calcular la ruta.\n\n" + ex.Message, "Mapa de bodega",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            MostrarDatosRuta();
            pnlMapa.Invalidate();
        }

        private void chkNumeros_CheckedChanged(object sender, EventArgs e)
        {
            pnlMapa.Invalidate();
        }

        private void MostrarDatosRuta()
        {
            lstParadas.Items.Clear();
            if (ruta == null)
            {
                lblDistancia.Text = "Distancia: -";
                lblTiempo.Text = "Tiempo estimado: -";
                lblSinUbicacion.Text = "";
                return;
            }

            lblDistancia.Text = "Distancia: " + ruta.DistanciaM.ToString("N1") + " m";
            lblTiempo.Text = "Tiempo estimado: " + ruta.MinutosEstimados.ToString("N1") + " min";
            foreach (ParadaRuta parada in ruta.Paradas)
            {
                lstParadas.Items.Add(parada.Orden + ". Nodo " + parada.Nodo + " → " + parada.Descripcion);
            }
            lstParadas.Items.Add("Regreso al mostrador");

            if (ruta.SinUbicacion.Count > 0)
            {
                lblSinUbicacion.Text = "Sin ubicación (no están en la ruta): " + string.Join(", ", ruta.SinUbicacion);
            }
            else
            {
                lblSinUbicacion.Text = "";
            }
        }

        // ---------------------------------------------------------------
        // Dibujo
        // ---------------------------------------------------------------

        /// <summary>
        /// Convierte coordenadas de la bodega (metros, Y hacia el fondo) a píxeles del panel.
        /// En la pantalla la Y crece hacia ABAJO, por eso se resta: así el mostrador queda abajo a la izquierda.
        /// </summary>
        private PointF APantalla(double x, double y)
        {
            float px = Margen + desplazamientoX + (float)x * escala;
            float py = pnlMapa.ClientSize.Height - Margen - desplazamientoY - (float)y * escala;
            return new PointF(px, py);
        }

        /// <summary>Rectángulo en pantalla de un estante, a la derecha de su punto de acceso (x, y).</summary>
        private RectangleF RectanguloEstante(Ubicacion u)
        {
            PointF esquina = APantalla(u.X + SeparacionEstante, u.Y + AltoEstante / 2);
            return new RectangleF(esquina.X, esquina.Y, AnchoEstante * escala, AltoEstante * escala);
        }

        /// <summary>Elige la escala para que toda la bodega quepa en el panel sin deformarse.</summary>
        private void CalcularEscala()
        {
            double maxX = 1;
            double maxY = 1;
            foreach (NodoBodega n in bodega.ObtenerNodos())
            {
                maxX = Math.Max(maxX, n.X);
                maxY = Math.Max(maxY, n.Y);
            }
            foreach (Ubicacion u in bodega.ObtenerUbicaciones())
            {
                maxX = Math.Max(maxX, u.X + SeparacionEstante + AnchoEstante);
                maxY = Math.Max(maxY, u.Y + AltoEstante / 2);
            }

            float anchoDisponible = pnlMapa.ClientSize.Width - 2 * Margen;
            float altoDisponible = pnlMapa.ClientSize.Height - 2 * Margen;
            // Se usa la menor de las dos escalas para que quepa a lo ancho y a lo alto.
            escala = Math.Max(1, Math.Min(anchoDisponible / (float)maxX, altoDisponible / (float)maxY));

            // El espacio que sobra se reparte a ambos lados para centrar el dibujo.
            desplazamientoX = Math.Max(0, (anchoDisponible - (float)maxX * escala) / 2);
            desplazamientoY = Math.Max(0, (altoDisponible - (float)maxY * escala) / 2);
        }

        private void pnlMapa_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            CalcularEscala();

            DibujarZonasYEstantes(g);
            DibujarPasillos(g);
            if (ruta != null)
            {
                DibujarRuta(g);
            }
            DibujarNodos(g);
        }

        private void DibujarZonasYEstantes(Graphics g)
        {
            // Paso 1: agrupar las ubicaciones por estante ("A-01-1", "A-01-2"... son el estante "A-01").
            // Basta con dibujar un rectángulo por estante.
            Dictionary<string, Ubicacion> estantes = new Dictionary<string, Ubicacion>();
            List<string> zonas = new List<string>();
            foreach (Ubicacion u in bodega.ObtenerUbicaciones())
            {
                string estante = NombreEstante(u.Id);
                if (!estantes.ContainsKey(estante))
                {
                    estantes[estante] = u;
                }
                if (!zonas.Contains(u.Zona))
                {
                    zonas.Add(u.Zona);
                }
            }

            // Paso 2: los estantes que están en la ruta se pintan de naranja.
            List<string> estantesEnRuta = new List<string>();
            if (ruta != null)
            {
                foreach (string ubicacionId in ruta.UbicacionesARecoger)
                {
                    estantesEnRuta.Add(NombreEstante(ubicacionId));
                }
            }

            using (Font fuente = new Font("Segoe UI", 8F))
            using (Font fuenteZona = new Font("Segoe UI", 10F, FontStyle.Bold))
            {
                // Paso 3: fondo de cada zona = rectángulo que encierra todos sus estantes.
                for (int i = 0; i < zonas.Count; i++)
                {
                    RectangleF? area = null;
                    foreach (Ubicacion u in estantes.Values)
                    {
                        if (u.Zona == zonas[i])
                        {
                            RectangleF r = RectanguloEstante(u);
                            area = area == null ? r : RectangleF.Union(area.Value, r);
                        }
                    }
                    if (area == null)
                    {
                        continue;
                    }
                    RectangleF fondo = area.Value;
                    fondo.Inflate(6, 6);
                    using (SolidBrush pincel = new SolidBrush(ColoresZona[i % ColoresZona.Length]))
                    {
                        g.FillRectangle(pincel, fondo);
                    }
                    g.DrawString("Zona " + zonas[i], fuenteZona, Brushes.DimGray, fondo.Right - 52, fondo.Top - 18);
                }

                // Paso 4: cada estante con su nombre.
                foreach (KeyValuePair<string, Ubicacion> par in estantes)
                {
                    RectangleF r = RectanguloEstante(par.Value);
                    Brush relleno = estantesEnRuta.Contains(par.Key) ? Brushes.Orange : Brushes.Gainsboro;
                    g.FillRectangle(relleno, r);
                    g.DrawRectangle(Pens.Gray, r.X, r.Y, r.Width, r.Height);
                    g.DrawString(par.Key, fuente, Brushes.Black, r.X + 2, r.Y + 2);
                }
            }
        }

        // "A-01-2" → "A-01": se quita el nivel (lo que va después del último guion).
        private static string NombreEstante(string ubicacionId)
        {
            int ultimoGuion = ubicacionId.LastIndexOf('-');
            if (ultimoGuion <= 0)
            {
                return ubicacionId;
            }
            return ubicacionId.Substring(0, ultimoGuion);
        }

        private void DibujarPasillos(Graphics g)
        {
            List<NodoBodega> nodos = bodega.ObtenerNodos();
            using (Pen lapiz = new Pen(Color.Silver, 4))
            {
                foreach (NodoBodega nodo in nodos)
                {
                    foreach (int vecino in nodo.Vecinos)
                    {
                        // Cada pasillo se dibuja una sola vez (de menor a mayor Id).
                        if (vecino > nodo.Id)
                        {
                            g.DrawLine(lapiz, APantalla(nodo.X, nodo.Y), APantalla(nodos[vecino].X, nodos[vecino].Y));
                        }
                    }
                }
            }
        }

        private void DibujarRuta(Graphics g)
        {
            List<NodoBodega> nodos = bodega.ObtenerNodos();
            using (Pen ida = new Pen(Color.RoyalBlue, 3))
            using (Pen regreso = new Pen(Color.SteelBlue, 2))
            {
                // La flecha al final de cada tramo muestra el sentido del recorrido.
                ida.CustomEndCap = new AdjustableArrowCap(4, 5);
                regreso.CustomEndCap = new AdjustableArrowCap(4, 5);
                // El regreso va punteado para distinguirlo cuando usa el mismo pasillo que la ida.
                regreso.DashStyle = DashStyle.Dash;

                for (int i = 0; i < ruta!.Camino.Count - 1; i++)
                {
                    NodoBodega a = nodos[ruta.Camino[i]];
                    NodoBodega b = nodos[ruta.Camino[i + 1]];
                    PointF desde = APantalla(a.X, a.Y);
                    PointF hasta = APantalla(b.X, b.Y);
                    if (i >= ruta.InicioRegreso)
                    {
                        // El regreso se corre 5 píxeles para que no tape la ida si va por el mismo pasillo.
                        g.DrawLine(regreso, desde.X + 5, desde.Y + 5, hasta.X + 5, hasta.Y + 5);
                    }
                    else
                    {
                        g.DrawLine(ida, desde, hasta);
                    }
                }
            }
        }

        private void DibujarNodos(Graphics g)
        {
            using (Font fuente = new Font("Segoe UI", 7.5F))
            using (Font fuenteParada = new Font("Segoe UI", 8F, FontStyle.Bold))
            {
                foreach (NodoBodega nodo in bodega.ObtenerNodos())
                {
                    PointF p = APantalla(nodo.X, nodo.Y);
                    if (nodo.Id == BodegaServicio.NodoMostrador)
                    {
                        g.FillEllipse(Brushes.Firebrick, p.X - 8, p.Y - 8, 16, 16);
                        g.DrawString("Mostrador", fuenteParada, Brushes.Firebrick, p.X + 10, p.Y - 6);
                        continue;
                    }
                    g.FillEllipse(Brushes.DimGray, p.X - 4, p.Y - 4, 8, 8);
                    if (chkNumeros.Checked)
                    {
                        g.DrawString(nodo.Id.ToString(), fuente, Brushes.DimGray, p.X + 4, p.Y + 2);
                    }
                }

                // Número de orden de cada parada en un círculo azul.
                if (ruta != null)
                {
                    foreach (ParadaRuta parada in ruta.Paradas)
                    {
                        NodoBodega nodo = bodega.ObtenerNodos()[parada.Nodo];
                        PointF p = APantalla(nodo.X, nodo.Y);
                        g.FillEllipse(Brushes.RoyalBlue, p.X - 10, p.Y - 10, 20, 20);
                        string texto = parada.Orden.ToString();
                        SizeF tamano = g.MeasureString(texto, fuenteParada);
                        g.DrawString(texto, fuenteParada, Brushes.White, p.X - tamano.Width / 2, p.Y - tamano.Height / 2);
                    }
                }
            }
        }
    }
}
