using FerreStock.Matematica;
using FerreStock.Modelos;
using FerreStock.Servicios;

namespace FerreStock.Formularios
{
    /// <summary>
    /// Calculadora del modelo matemático, en cuatro pestañas: volumen de montículos,
    /// área útil y ocupación, costo de ubicación (con gradiente y sugerencias) y Lagrange.
    /// Los cálculos están en las clases de Matematica/ y en BodegaServicio.
    /// </summary>
    public partial class FrmCalculadora : Form
    {
        private readonly InventarioServicio inventario;
        private readonly BodegaServicio bodega;
        private readonly PedidoServicio pedidos;

        public FrmCalculadora(InventarioServicio inventario, BodegaServicio bodega, PedidoServicio pedidos)
        {
            InitializeComponent();
            this.inventario = inventario;
            this.bodega = bodega;
            this.pedidos = pedidos;
        }

        private void FrmCalculadora_Load(object sender, EventArgs e)
        {
            // Montículos: solo tienen sentido los productos que se venden por m³ (arena, gravilla).
            cmbAgregado.DisplayMember = "Descripcion";
            cmbProductoCosto.DisplayMember = "Descripcion";
            foreach (Producto p in inventario.ObtenerProductos())
            {
                if (p.Unidad == "m³")
                {
                    cmbAgregado.Items.Add(p);
                }
                cmbProductoCosto.Items.Add(p);
            }
            if (cmbAgregado.Items.Count > 0)
            {
                cmbAgregado.SelectedIndex = 0;
            }
            if (cmbProductoCosto.Items.Count > 0)
            {
                cmbProductoCosto.SelectedIndex = 0;
            }

            nudA.Value = (decimal)CostoUbicacion.CoeficienteDistancia;
            nudB.Value = (decimal)CostoUbicacion.CoeficienteAltura;
            CargarEjemploLagrange();
        }

        // ---------------------------------------------------------------
        // Pestaña 1: montículos
        // ---------------------------------------------------------------

        private void btnCalcularVolumen_Click(object sender, EventArgs e)
        {
            try
            {
                double radio = (double)nudRadio.Value;
                double altura = (double)nudAltura.Value;
                double parabolico = Volumenes.MonticuloParabolico(radio, altura);
                double conico = Volumenes.MonticuloConico(radio, altura);
                double numerico = Volumenes.MonticuloParabolicoNumerico(radio, altura, 100);

                string texto = "Paraboloide  V = πR²h/2 = " + parabolico.ToString("N3") + " m³\n" +
                               "Cono            V = πR²h/3 = " + conico.ToString("N3") + " m³\n" +
                               "Diferencia: el paraboloide tiene " + (parabolico - conico).ToString("N3") + " m³ más (50 % más que el cono).\n\n" +
                               "Comprobación con suma de Riemann (100 × 100 divisiones): " + numerico.ToString("N4") + " m³";

                Producto? agregado = cmbAgregado.SelectedItem as Producto;
                if (agregado != null)
                {
                    texto += "\n\nValor del montículo de " + agregado.Nombre + " a $" + agregado.PrecioVenta.ToString("N0") + "/m³:\n" +
                             "  paraboloide $" + ((decimal)parabolico * agregado.PrecioVenta).ToString("N0") +
                             "   cono $" + ((decimal)conico * agregado.PrecioVenta).ToString("N0");
                }
                lblResultadoMonticulo.Text = texto;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ---------------------------------------------------------------
        // Pestaña 2: área útil y ocupación
        // ---------------------------------------------------------------

        private void btnCalcularArea_Click(object sender, EventArgs e)
        {
            try
            {
                double ancho = (double)nudAncho.Value;
                double largo = (double)nudLargo.Value;
                double pasillos = Volumenes.AreaPasillos((int)nudCantidadPasillos.Value, (double)nudAnchoPasillo.Value, (double)nudLargoPasillo.Value);
                double areaUtil = Volumenes.AreaUtil(ancho, largo, pasillos);
                double volumenUtil = Volumenes.VolumenUtil(areaUtil, (double)nudAlturaUtil.Value);
                double ocupado = inventario.VolumenOcupado();

                // Capacidad real de los estantes registrados, para comparar con el cálculo teórico.
                double capacidadEstantes = 0;
                foreach (Ubicacion u in inventario.ObtenerUbicaciones())
                {
                    capacidadEstantes += u.CapacidadM3;
                }

                lblResultadoArea.Text =
                    "Área total (∫∫ dA) = " + (ancho * largo).ToString("N2") + " m²\n" +
                    "Área de pasillos = " + pasillos.ToString("N2") + " m²\n" +
                    "Área útil = " + areaUtil.ToString("N2") + " m²\n" +
                    "Volumen útil = área útil × altura = " + volumenUtil.ToString("N2") + " m³\n\n" +
                    "Volumen ocupado por el inventario (Σ stock × volumen) = " + ocupado.ToString("N2") + " m³\n" +
                    "Ocupación del volumen útil = " + Volumenes.PorcentajeOcupacion(ocupado, volumenUtil).ToString("N1") + " %\n" +
                    "Ocupación de los estantes registrados (" + capacidadEstantes.ToString("N1") + " m³) = " +
                    Volumenes.PorcentajeOcupacion(ocupado, capacidadEstantes).ToString("N1") + " %";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ---------------------------------------------------------------
        // Pestaña 3: costo de ubicación
        // ---------------------------------------------------------------

        private void cmbProductoCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Al elegir un producto se llenan x, y, z con su ubicación actual.
            Producto? producto = cmbProductoCosto.SelectedItem as Producto;
            if (producto == null)
            {
                return;
            }
            Ubicacion? ubicacion = bodega.BuscarUbicacion(producto.UbicacionId);
            if (ubicacion != null)
            {
                nudX.Value = (decimal)ubicacion.X;
                nudY.Value = (decimal)ubicacion.Y;
                nudZ.Value = (decimal)ubicacion.Z;
            }
            lblDatosProducto.Text = "Ventas/mes r = " + producto.VentasMes + "   Peso p = " + producto.PesoKg.ToString("N2") +
                                    " kg   Ubicación: " + (producto.UbicacionId == "" ? "(sin ubicación)" : producto.UbicacionId);
        }

        private void btnCalcularCosto_Click(object sender, EventArgs e)
        {
            Producto? producto = cmbProductoCosto.SelectedItem as Producto;
            if (producto == null)
            {
                MessageBox.Show("Elija un producto.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            double x = (double)nudX.Value;
            double y = (double)nudY.Value;
            double z = (double)nudZ.Value;
            double a = (double)nudA.Value;
            double b = (double)nudB.Value;

            double costo = CostoUbicacion.Costo(producto.VentasMes, producto.PesoKg, x, y, z, a, b);
            Gradiente g = CostoUbicacion.CalcularGradiente(producto.VentasMes, producto.PesoKg, x, y, z, a, b);

            lblResultadoCosto.Text =
                "C(x, y, z) = r·(a·√(x² + y²) + b·p·z) = " + costo.ToString("N1") + "\n\n" +
                "∇C = (" + g.X.ToString("N2") + ", " + g.Y.ToString("N2") + ", " + g.Z.ToString("N2") + ")\n" +
                "Las tres componentes son positivas: el costo baja al acercar el producto\n" +
                "al mostrador (x, y menores) y al bajarlo de nivel (z menor).";
        }

        private void btnSugerir_Click(object sender, EventArgs e)
        {
            double a = (double)nudA.Value;
            double b = (double)nudB.Value;
            try
            {
                List<SugerenciaUbicacion> sugerencias = bodega.SugerirUbicaciones(a, b);
                dgvSugerencias.Rows.Clear();
                foreach (SugerenciaUbicacion s in sugerencias)
                {
                    int fila = dgvSugerencias.Rows.Add(s.Codigo, s.Nombre, s.UbicacionActual, s.CostoActual.ToString("N0"),
                                                       s.UbicacionSugerida, s.CostoSugerido.ToString("N0"), s.Ahorro.ToString("N0"));
                    // Se resaltan los productos que conviene mover.
                    if (s.UbicacionSugerida != s.UbicacionActual)
                    {
                        dgvSugerencias.Rows[fila].DefaultCellStyle.BackColor = Color.Honeydew;
                    }
                }
                dgvSugerencias.ClearSelection();

                ResultadoSimulacion sim = bodega.SimularAntesDespues(pedidos.ObtenerPedidos(), a, b);
                lblSimulacion.Text = "Simulación con los " + sim.CantidadPedidos + " pedidos registrados: recorrido promedio " +
                    sim.DistanciaPromedioAntes.ToString("N1") + " m → " + sim.DistanciaPromedioDespues.ToString("N1") + " m; tiempo promedio " +
                    sim.MinutosPromedioAntes.ToString("N2") + " → " + sim.MinutosPromedioDespues.ToString("N2") + " min (" +
                    sim.ReduccionTiempoPorcentaje.ToString("N1") + " % menos).";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron calcular las sugerencias.\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // TODO estudiante: agregar un botón "Aplicar sugerencias" que mueva los productos (pidiendo confirmación).
        }

        // ---------------------------------------------------------------
        // Pestaña 4: Lagrange
        // ---------------------------------------------------------------

        // Ejemplo con tres productos de alta venta. Demanda anual = ventas del mes × 12;
        // costo de mantener = 25 % del precio de compra al año.
        private void CargarEjemploLagrange()
        {
            dgvLagrange.Rows.Clear();
            string[] codigos = { "CON-001", "CON-006", "PLO-001" };
            foreach (string codigo in codigos)
            {
                Producto? p = inventario.BuscarPorCodigo(codigo);
                if (p == null)
                {
                    continue;
                }
                double demanda = p.VentasMes * 12;
                double mantener = (double)p.PrecioCompra * 0.25;
                dgvLagrange.Rows.Add(p.Nombre, demanda.ToString(), "50000", mantener.ToString(), p.VolumenUnitarioM3.ToString());
            }
        }

        private void btnEjemploLagrange_Click(object sender, EventArgs e)
        {
            CargarEjemploLagrange();
        }

        private void btnResolverLagrange_Click(object sender, EventArgs e)
        {
            try
            {
                List<ProductoLagrange> productos = LeerTablaLagrange();
                ResultadoLagrange r = OptimizacionLagrange.Resolver(productos, (double)nudVolumenDisponible.Value);

                dgvResultadoLagrange.Rows.Clear();
                for (int i = 0; i < productos.Count; i++)
                {
                    double libre = OptimizacionLagrange.Cantidad(productos[i], 0);
                    dgvResultadoLagrange.Rows.Add(productos[i].Nombre, libre.ToString("N1"), r.Cantidades[i].ToString("N1"),
                                                  (r.Cantidades[i] * productos[i].Volumen).ToString("N3"));
                }
                dgvResultadoLagrange.ClearSelection();

                string texto;
                if (r.RestriccionActiva)
                {
                    texto = "No cabía con el lote económico (λ = 0): " + OptimizacionLagrange.VolumenUsado(productos, 0).ToString("N2") +
                            " m³ > " + nudVolumenDisponible.Value.ToString("N2") + " m³.\n" +
                            "Bisección: λ = " + r.Lambda.ToString("N4") + " en " + r.Iteraciones + " iteraciones.";
                }
                else
                {
                    texto = "Las cantidades sin restricción caben en el espacio: λ = 0 (lote económico clásico).";
                }
                texto += "\nVolumen usado: " + r.VolumenUsado.ToString("N3") + " m³    Costo anual: $" + r.CostoTotal.ToString("N0");
                lblResultadoLagrange.Text = texto;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>Lee la tabla editable. Cada celda numérica debe ser un número válido.</summary>
        private List<ProductoLagrange> LeerTablaLagrange()
        {
            List<ProductoLagrange> productos = new List<ProductoLagrange>();
            foreach (DataGridViewRow fila in dgvLagrange.Rows)
            {
                if (fila.IsNewRow)
                {
                    continue;
                }
                ProductoLagrange p = new ProductoLagrange();
                p.Nombre = fila.Cells[colLagProducto.Index].Value?.ToString() ?? "";
                if (p.Nombre.Trim() == "")
                {
                    p.Nombre = "Fila " + (fila.Index + 1);
                }
                p.Demanda = LeerNumero(fila, colLagDemanda, p.Nombre);
                p.CostoPedido = LeerNumero(fila, colLagCostoPedido, p.Nombre);
                p.CostoMantener = LeerNumero(fila, colLagMantener, p.Nombre);
                p.Volumen = LeerNumero(fila, colLagVolumen, p.Nombre);
                productos.Add(p);
            }
            return productos;
        }

        private static double LeerNumero(DataGridViewRow fila, DataGridViewColumn columna, string nombre)
        {
            string texto = fila.Cells[columna.Index].Value?.ToString() ?? "";
            if (!double.TryParse(texto, out double valor))
            {
                throw new ArgumentException("El valor \"" + texto + "\" de la columna " + columna.HeaderText + " (" + nombre + ") no es un número.");
            }
            return valor;
        }
    }
}
