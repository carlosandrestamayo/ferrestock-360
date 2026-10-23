using FerreStock.Modelos;

namespace FerreStock.Datos
{
    /// <summary>
    /// Datos de ejemplo ("semilla") que se usan cuando un archivo JSON todavía no existe.
    /// Representan una ferretería con bodega de 26 m x 16 m y el mostrador en el origen (0, 0).
    ///
    /// Plano simplificado (vista desde arriba, Y crece hacia el fondo). Los pasillos son las
    /// líneas entre nodos (N0...N12). Cada estante está al lado de un pasillo vertical y su
    /// X, Y es el punto del pasillo desde donde se alcanza (por eso coincide con la línea).
    ///
    ///   y=14   N9 ------ N10 ------ N11 ------ N12
    ///   y=12   |[E-01]   |[E-02]    |[F-01]    |[F-02]
    ///   y=8    N5 ------ N6 ------- N7 ------- N8     [D-03 patio]
    ///   y=6    |[A-02]   |[B-02]    |[C-02]    |[D-02]
    ///   y=4    |[A-01]   |[B-01]    |[C-01]    |[D-01]
    ///   y=2    N1 ------ N2 ------- N3 ------- N4
    ///         /
    ///   y=0  N0 (mostrador)
    ///          x=4       x=10       x=16       x=22
    ///
    /// A propósito, los productos pesados y de alta venta (zona D) quedan lejos
    /// del mostrador: así es la situación "antes" que el proyecto quiere mejorar.
    /// </summary>
    public static class DatosIniciales
    {
        // TODO estudiante: reemplazar estos datos por los de la ferretería visitada.

        public static List<Categoria> CrearCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            // Categorías principales (PadreId = null).
            lista.Add(new Categoria { Id = 1, Nombre = "Construcción", PadreId = null });
            lista.Add(new Categoria { Id = 2, Nombre = "Plomería", PadreId = null });
            lista.Add(new Categoria { Id = 3, Nombre = "Eléctricos", PadreId = null });
            lista.Add(new Categoria { Id = 4, Nombre = "Tornillería", PadreId = null });
            lista.Add(new Categoria { Id = 5, Nombre = "Pinturas", PadreId = null });
            lista.Add(new Categoria { Id = 6, Nombre = "Herramientas", PadreId = null });

            // Subcategorías: el Id empieza con el número de su categoría principal
            // (11, 12, 13 son de Construcción) solo para que sea fácil de leer.
            lista.Add(new Categoria { Id = 11, Nombre = "Cementos y pegantes", PadreId = 1 });
            lista.Add(new Categoria { Id = 12, Nombre = "Agregados", PadreId = 1 });
            lista.Add(new Categoria { Id = 13, Nombre = "Hierro y acero", PadreId = 1 });
            lista.Add(new Categoria { Id = 21, Nombre = "Tubería PVC", PadreId = 2 });
            lista.Add(new Categoria { Id = 22, Nombre = "Accesorios PVC", PadreId = 2 });
            lista.Add(new Categoria { Id = 31, Nombre = "Cables", PadreId = 3 });
            lista.Add(new Categoria { Id = 32, Nombre = "Interruptores y tomas", PadreId = 3 });
            lista.Add(new Categoria { Id = 33, Nombre = "Iluminación", PadreId = 3 });
            lista.Add(new Categoria { Id = 41, Nombre = "Tornillos y chazos", PadreId = 4 });
            lista.Add(new Categoria { Id = 42, Nombre = "Puntillas", PadreId = 4 });
            lista.Add(new Categoria { Id = 51, Nombre = "Vinilos", PadreId = 5 });
            lista.Add(new Categoria { Id = 52, Nombre = "Esmaltes y solventes", PadreId = 5 });
            lista.Add(new Categoria { Id = 53, Nombre = "Accesorios de pintura", PadreId = 5 });
            lista.Add(new Categoria { Id = 61, Nombre = "Herramienta manual", PadreId = 6 });
            lista.Add(new Categoria { Id = 62, Nombre = "Abrasivos y corte", PadreId = 6 });

            return lista;
        }

        public static List<Ubicacion> CrearUbicaciones()
        {
            List<Ubicacion> lista = new List<Ubicacion>();

            // TODO estudiante: ajustar las coordenadas al plano real de la bodega.
            // Cada estante tiene 3 niveles. Parámetros: zona, estante, x, y, nodo, capacidad por nivel.
            AgregarEstante(lista, "A", 1, 4, 4, 1, 1.5);
            AgregarEstante(lista, "A", 2, 4, 6, 5, 1.5);
            AgregarEstante(lista, "B", 1, 10, 4, 2, 1.5);
            AgregarEstante(lista, "B", 2, 10, 6, 6, 1.5);
            AgregarEstante(lista, "C", 1, 16, 4, 3, 1.5);
            AgregarEstante(lista, "C", 2, 16, 6, 7, 1.5);
            AgregarEstante(lista, "D", 1, 22, 4, 4, 4.0);
            AgregarEstante(lista, "D", 2, 22, 6, 8, 4.0);
            AgregarEstante(lista, "E", 1, 4, 12, 9, 1.5);
            AgregarEstante(lista, "E", 2, 10, 12, 10, 1.5);
            AgregarEstante(lista, "F", 1, 16, 12, 11, 1.5);
            AgregarEstante(lista, "F", 2, 22, 12, 12, 1.5);

            // El patio de arena y gravilla no es un estante: es una sola ubicación en el piso.
            lista.Add(new Ubicacion { Id = "D-03-1", Zona = "D", X = 24, Y = 10, Z = 0, CapacidadM3 = 12, NodoGrafo = 8 });

            return lista;
        }

        // Crea los 3 niveles de un estante. Todos comparten X, Y y nodo; solo cambia la altura Z.
        private static void AgregarEstante(List<Ubicacion> lista, string zona, int estante,
                                           double x, double y, int nodo, double capacidad)
        {
            double[] alturas = { 0.2, 1.0, 1.8 }; // metros del piso a cada nivel
            for (int nivel = 1; nivel <= 3; nivel++)
            {
                Ubicacion u = new Ubicacion();
                // "D2" escribe el número con dos dígitos: 1 -> "01".
                u.Id = zona + "-" + estante.ToString("D2") + "-" + nivel;
                u.Zona = zona;
                u.X = x;
                u.Y = y;
                u.Z = alturas[nivel - 1];
                u.CapacidadM3 = capacidad;
                u.NodoGrafo = nodo;
                lista.Add(u);
            }
        }

        public static List<NodoBodega> CrearNodosBodega()
        {
            List<NodoBodega> lista = new List<NodoBodega>();

            // TODO estudiante: dibujar los pasillos reales de la bodega y ajustar nodos y vecinos.
            // Parámetros: id, nombre, x, y, vecinos.
            lista.Add(N(0, "Mostrador", 0, 0, 1));
            lista.Add(N(1, "Pasillo 1 - zona A", 4, 2, 0, 2, 5));
            lista.Add(N(2, "Pasillo 1 - zona B", 10, 2, 1, 3, 6));
            lista.Add(N(3, "Pasillo 1 - zona C", 16, 2, 2, 4, 7));
            lista.Add(N(4, "Pasillo 1 - zona D", 22, 2, 3, 8));
            lista.Add(N(5, "Pasillo 2 - zona A", 4, 8, 1, 6, 9));
            lista.Add(N(6, "Pasillo 2 - zona B", 10, 8, 2, 5, 7, 10));
            lista.Add(N(7, "Pasillo 2 - zona C", 16, 8, 3, 6, 8, 11));
            lista.Add(N(8, "Pasillo 2 - zona D", 22, 8, 4, 7, 12));
            lista.Add(N(9, "Pasillo 3 - zona E", 4, 14, 5, 10));
            lista.Add(N(10, "Pasillo 3 - zona E", 10, 14, 6, 9, 11));
            lista.Add(N(11, "Pasillo 3 - zona F", 16, 14, 7, 10, 12));
            lista.Add(N(12, "Pasillo 3 - zona F", 22, 14, 8, 11));

            return lista;
        }

        // "params int[] vecinos" permite escribir los vecinos separados por comas al final.
        private static NodoBodega N(int id, string nombre, double x, double y, params int[] vecinos)
        {
            NodoBodega nodo = new NodoBodega();
            nodo.Id = id;
            nodo.Nombre = nombre;
            nodo.X = x;
            nodo.Y = y;
            foreach (int v in vecinos)
            {
                nodo.Vecinos.Add(v);
            }
            return nodo;
        }

        public static List<Producto> CrearProductos()
        {
            List<Producto> lista = new List<Producto>();

            // Orden de los datos:
            // código, nombre, subcategoría, unidad, precio compra, precio venta, stock, stock mínimo,
            // peso kg, volumen m³, ubicación, ventas del mes
            lista.Add(P("CON-001", "Cemento gris 50 kg", 11, "bulto", 28000, 33500, 80, 30, 50, 0.036, "D-01-1", 120));
            lista.Add(P("CON-002", "Cemento blanco 25 kg", 11, "bulto", 32000, 39000, 15, 5, 25, 0.018, "D-01-2", 10));
            lista.Add(P("CON-003", "Pegante cerámico 25 kg", 11, "bulto", 18000, 23000, 8, 10, 25, 0.018, "D-01-3", 25));
            lista.Add(P("CON-004", "Arena de pega", 12, "m³", 70000, 90000, 6, 2, 1500, 1.0, "D-03-1", 8));
            lista.Add(P("CON-005", "Gravilla", 12, "m³", 85000, 110000, 4, 2, 1600, 1.0, "D-03-1", 5));
            lista.Add(P("CON-006", "Varilla corrugada 1/2\" x 6 m", 13, "varilla", 22000, 27000, 120, 40, 5.97, 0.0008, "D-02-1", 150));
            lista.Add(P("CON-007", "Varilla corrugada 3/8\" x 6 m", 13, "varilla", 13000, 16000, 150, 50, 3.36, 0.0005, "D-02-1", 180));
            lista.Add(P("CON-008", "Alambre negro calibre 18", 13, "kg", 7000, 9500, 40, 15, 1, 0.0002, "D-02-2", 35));

            lista.Add(P("PLO-001", "Tubo PVC presión 1/2\" x 6 m", 21, "tubo", 9500, 13000, 60, 20, 1.1, 0.0027, "C-01-2", 70));
            lista.Add(P("PLO-002", "Tubo PVC presión 3/4\" x 6 m", 21, "tubo", 14000, 18500, 40, 15, 1.6, 0.0042, "C-01-2", 40));
            lista.Add(P("PLO-003", "Tubo PVC sanitario 4\" x 6 m", 21, "tubo", 55000, 68000, 20, 8, 7.5, 0.065, "C-01-1", 18));
            lista.Add(P("PLO-004", "Codo PVC presión 1/2\"", 22, "unidad", 600, 1000, 300, 100, 0.01, 0.00002, "C-02-2", 250));
            lista.Add(P("PLO-005", "Tee PVC presión 1/2\"", 22, "unidad", 800, 1300, 200, 80, 0.015, 0.00003, "C-02-2", 150));
            lista.Add(P("PLO-006", "Soldadura PVC 1/4 de galón", 22, "unidad", 18000, 24000, 25, 10, 0.95, 0.0011, "C-02-3", 20));
            lista.Add(P("PLO-007", "Llave de paso 1/2\"", 22, "unidad", 12000, 17000, 30, 10, 0.25, 0.0003, "C-02-3", 15));
            lista.Add(P("PLO-008", "Cinta teflón 1/2\"", 22, "unidad", 900, 1500, 150, 50, 0.01, 0.00002, "C-02-1", 110));

            lista.Add(P("ELE-001", "Cable THHN 12 AWG rollo 100 m", 31, "rollo", 230000, 280000, 3, 4, 5.8, 0.012, "A-01-1", 6));
            lista.Add(P("ELE-002", "Cable THHN 14 AWG rollo 100 m", 31, "rollo", 160000, 199000, 12, 4, 3.9, 0.010, "A-01-1", 8));
            lista.Add(P("ELE-003", "Cable dúplex 2x16", 31, "metro", 1600, 2500, 500, 100, 0.03, 0.00003, "A-01-2", 300));
            lista.Add(P("ELE-004", "Interruptor sencillo", 32, "unidad", 4500, 7500, 60, 20, 0.08, 0.0002, "A-02-2", 40));
            lista.Add(P("ELE-005", "Toma doble con polo a tierra", 32, "unidad", 5500, 8900, 60, 20, 0.09, 0.0002, "A-02-2", 45));
            lista.Add(P("ELE-006", "Cinta aislante negra", 32, "unidad", 2200, 3500, 80, 25, 0.03, 0.00005, "A-02-1", 60));
            lista.Add(P("ELE-007", "Bombillo LED 9 W", 33, "unidad", 3500, 6000, 100, 30, 0.05, 0.0004, "A-02-3", 90));

            lista.Add(P("TOR-001", "Tornillo drywall 6 x 1\" caja x100", 41, "caja", 4500, 7000, 80, 25, 0.2, 0.0002, "B-01-2", 70));
            lista.Add(P("TOR-002", "Tornillo drywall 6 x 1-1/4\" caja x100", 41, "caja", 5000, 7800, 60, 20, 0.25, 0.0002, "B-01-2", 50));
            lista.Add(P("TOR-003", "Chazo plástico 1/4\" caja x100", 41, "caja", 3000, 5000, 50, 15, 0.1, 0.0002, "B-01-3", 35));
            lista.Add(P("TOR-004", "Tornillo hexagonal 3/8\" x 2\"", 41, "unidad", 600, 1000, 400, 100, 0.05, 0.00001, "B-01-1", 200));
            lista.Add(P("TOR-005", "Puntilla 2\" con cabeza", 42, "kg", 6500, 9000, 50, 15, 1, 0.00015, "B-02-1", 45));
            lista.Add(P("TOR-006", "Puntilla 3\" con cabeza", 42, "kg", 6500, 9000, 40, 15, 1, 0.00015, "B-02-1", 30));
            lista.Add(P("TOR-007", "Puntilla para concreto 2\"", 42, "kg", 12000, 16000, 15, 5, 1, 0.00015, "B-02-2", 10));

            lista.Add(P("PIN-001", "Vinilo blanco tipo 1 galón", 51, "galón", 38000, 52000, 30, 10, 5.5, 0.0045, "F-01-1", 25));
            lista.Add(P("PIN-002", "Vinilo blanco tipo 1 cuñete 5 galones", 51, "unidad", 165000, 215000, 8, 3, 27, 0.022, "F-01-1", 6));
            lista.Add(P("PIN-003", "Esmalte sintético blanco galón", 52, "galón", 62000, 82000, 4, 5, 4.8, 0.0045, "F-01-2", 10));
            lista.Add(P("PIN-004", "Anticorrosivo gris galón", 52, "galón", 55000, 72000, 12, 5, 5.0, 0.0045, "F-01-2", 8));
            lista.Add(P("PIN-005", "Thinner galón", 52, "galón", 22000, 30000, 20, 8, 3.2, 0.0045, "F-02-1", 18));
            lista.Add(P("PIN-006", "Brocha 3\"", 53, "unidad", 4500, 7500, 40, 15, 0.08, 0.0006, "F-02-2", 30));
            lista.Add(P("PIN-007", "Rodillo de felpa 9\"", 53, "unidad", 9000, 14000, 25, 10, 0.2, 0.0025, "F-02-3", 20));

            lista.Add(P("HER-001", "Martillo de uña 16 oz", 61, "unidad", 18000, 27000, 15, 5, 0.6, 0.0012, "E-01-2", 8));
            lista.Add(P("HER-002", "Flexómetro 5 m", 61, "unidad", 9000, 15000, 25, 8, 0.25, 0.0004, "E-01-2", 20));
            lista.Add(P("HER-003", "Pala cuadrada", 61, "unidad", 28000, 38000, 10, 4, 2.0, 0.012, "E-01-1", 6));
            lista.Add(P("HER-004", "Palustre 9\"", 61, "unidad", 9500, 14500, 15, 5, 0.35, 0.0008, "E-01-3", 10));
            lista.Add(P("HER-005", "Disco de corte metal 4-1/2\"", 62, "unidad", 3200, 5500, 80, 25, 0.06, 0.0001, "E-02-2", 60));
            lista.Add(P("HER-006", "Lija de agua #150", 62, "unidad", 1200, 2000, 150, 50, 0.01, 0.00005, "E-02-1", 100));
            lista.Add(P("HER-007", "Segueta 12\"", 62, "unidad", 2500, 4500, 40, 15, 0.03, 0.0001, "E-02-3", 25));

            return lista;
        }

        // Método corto para crear un producto en una sola línea y que la lista de arriba
        // se lea como una tabla.
        private static Producto P(string codigo, string nombre, int categoriaId, string unidad,
                                  decimal precioCompra, decimal precioVenta, int stock, int stockMinimo,
                                  double pesoKg, double volumenM3, string ubicacionId, int ventasMes)
        {
            Producto p = new Producto();
            p.Codigo = codigo;
            p.Nombre = nombre;
            p.CategoriaId = categoriaId;
            p.Unidad = unidad;
            p.PrecioCompra = precioCompra;
            p.PrecioVenta = precioVenta;
            p.Stock = stock;
            p.StockMinimo = stockMinimo;
            p.PesoKg = pesoKg;
            p.VolumenUnitarioM3 = volumenM3;
            p.UbicacionId = ubicacionId;
            p.VentasMes = ventasMes;
            return p;
        }

        // Al empezar no hay movimientos registrados.
        public static List<Movimiento> CrearMovimientos()
        {
            return new List<Movimiento>();
        }

        // Algunos pedidos pendientes para probar la cola de prioridad en la semana 2.
        public static List<Pedido> CrearPedidos()
        {
            List<Pedido> lista = new List<Pedido>();

            Pedido p1 = new Pedido { Id = 1, Fecha = new DateTime(2026, 10, 20, 8, 15, 0), Cliente = "Constructora Andina", Prioridad = PrioridadPedido.Mayorista };
            p1.Items.Add(new ItemPedido { CodigoProducto = "CON-001", Cantidad = 20 });
            p1.Items.Add(new ItemPedido { CodigoProducto = "CON-006", Cantidad = 30 });
            lista.Add(p1);

            Pedido p2 = new Pedido { Id = 2, Fecha = new DateTime(2026, 10, 20, 9, 0, 0), Cliente = "Juan Pérez", Prioridad = PrioridadPedido.Mostrador };
            p2.Items.Add(new ItemPedido { CodigoProducto = "PLO-001", Cantidad = 3 });
            p2.Items.Add(new ItemPedido { CodigoProducto = "PLO-004", Cantidad = 6 });
            p2.Items.Add(new ItemPedido { CodigoProducto = "PLO-006", Cantidad = 1 });
            lista.Add(p2);

            Pedido p3 = new Pedido { Id = 3, Fecha = new DateTime(2026, 10, 20, 9, 30, 0), Cliente = "Obra Calle 10", Prioridad = PrioridadPedido.Urgente };
            p3.Items.Add(new ItemPedido { CodigoProducto = "CON-001", Cantidad = 10 });
            p3.Items.Add(new ItemPedido { CodigoProducto = "CON-004", Cantidad = 1 });
            lista.Add(p3);

            Pedido p4 = new Pedido { Id = 4, Fecha = new DateTime(2026, 10, 20, 10, 10, 0), Cliente = "María Gómez", Prioridad = PrioridadPedido.Mostrador };
            p4.Items.Add(new ItemPedido { CodigoProducto = "PIN-001", Cantidad = 2 });
            p4.Items.Add(new ItemPedido { CodigoProducto = "PIN-006", Cantidad = 1 });
            lista.Add(p4);

            Pedido p5 = new Pedido { Id = 5, Fecha = new DateTime(2026, 10, 20, 10, 45, 0), Cliente = "Ferretería El Tornillo", Prioridad = PrioridadPedido.Mayorista };
            p5.Items.Add(new ItemPedido { CodigoProducto = "TOR-001", Cantidad = 10 });
            p5.Items.Add(new ItemPedido { CodigoProducto = "TOR-005", Cantidad = 5 });
            lista.Add(p5);

            return lista;
        }
    }
}
