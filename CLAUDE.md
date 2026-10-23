# FerreStock 360 — Plan del proyecto

Este archivo es el plan maestro del proyecto. Claude debe leerlo antes de escribir código y respetar
todas las decisiones que aquí aparecen. Si algo no está definido, se elige la opción **más simple**.

## 1. Contexto

- Proyecto integrador de Ingeniería de Sistemas, propuesta **SIGLO 360**, variante **Equipo 2: Ferretería**.
- Asignatura base: Programación de Computadores III (30%). Además aporta evidencias para Estructura de Datos,
  Cálculo Multivariable, Metodología de la Investigación y Fundamentos de Administración (10% cada una).
- Lo entregan estudiantes de **cuarto semestre** que no son programadores expertos. **Todo el código debe
  poder explicarse en una sustentación oral.**
- Duración: **4 semanas**.

**Problema:** en una ferretería con bodega, los productos pesados o de alta venta (cemento, varilla, PVC)
quedan mal ubicados, el alistamiento de pedidos es lento y el inventario no cuadra con el físico.

**Pregunta de investigación:** ¿En qué medida un sistema que integra la ubicación espacial de los productos,
la priorización de pedidos y la optimización del espacio reduce el tiempo de alistamiento y los errores de
inventario en una ferretería?

## 2. Decisiones técnicas (no cambiar sin autorización del docente)

- **C# con .NET 8 y Windows Forms** (`net8.0-windows`, `UseWindowsForms`).
- **Persistencia en archivos JSON planos** con `System.Text.Json`. Nada de bases de datos ni Entity Framework.
- **Un solo proyecto de aplicación** organizado por carpetas, más un proyecto de pruebas **xUnit**.
- Git con commits pequeños, uno por funcionalidad.

### Estilo de código (obligatorio)

- Nombres de clases, métodos, variables y comentarios **en español**.
- Comentarios que expliquen el **por qué** y cada paso de los algoritmos (pensando en que el estudiante los lea).
- Código simple: `for`/`foreach`, `if`, clases con propiedades. **Evitar**: LINQ encadenado largo, lambdas
  complejas, genéricos rebuscados, patrones avanzados (inyección de dependencias, MVVM, eventos personalizados).
  Un `.Where(...)` o `.OrderBy(...)` simple está permitido solo donde no sea parte de lo que se evalúa.
- Las estructuras y algoritmos que se evalúan (pila, cola de prioridad, ordenamientos, búsqueda binaria,
  grafo) se implementan **a mano**, nunca con las clases de .NET equivalentes.
- Manejo de errores con `try/catch` y mensajes claros al usuario (`MessageBox`).
- Validar entradas del usuario (campos vacíos, números negativos, códigos repetidos, stock insuficiente).
- Cada formulario delega la lógica a una clase de `Servicios/`; el formulario solo muestra y captura datos.

## 3. Estructura de la solución

```
FerreStock360.sln
├─ FerreStock/                       (Windows Forms)
│  ├─ Modelos/        Producto, Categoria, Ubicacion, Movimiento, Pedido, ItemPedido, TipoMovimiento, PrioridadPedido
│  ├─ Datos/          ArchivoJson (Cargar / GuardarAsync), RutasDatos, DatosIniciales (semilla)
│  ├─ Estructuras/    Nodo<T>, Pila<T> (enlazada), ColaPrioridad (heap binario), Grafo (lista de adyacencia)
│  ├─ Algoritmos/     Ordenamientos (Burbuja, Insercion, Comparador con Stopwatch), Busquedas (Lineal, Binaria)
│  ├─ Matematica/     Volumenes, CostoUbicacion, OptimizacionLagrange
│  ├─ Servicios/      InventarioServicio, MovimientoServicio, PedidoServicio, BodegaServicio, IndicadorServicio
│  ├─ Formularios/    FrmPrincipal, FrmProductos, FrmMovimientos, FrmPedidos, FrmMapaBodega,
│  │                  FrmCalculadora, FrmIndicadores, FrmComparadorAlgoritmos
│  └─ data/           productos.json, categorias.json, ubicaciones.json, movimientos.json, pedidos.json, bodega.json
├─ FerreStock.Pruebas/               (xUnit, net8.0-windows, referencia a FerreStock)
└─ docs/                             guia-sustentacion.md, modelo-matematico.md, justificacion-estructuras.md
```

Los archivos de `data/` se copian a la carpeta de salida (`CopyToOutputDirectory: PreserveNewest`).
Si un archivo no existe, se crea con los datos de `DatosIniciales`.

## 4. Modelos

- **Producto**: Codigo (string, único), Nombre, CategoriaId, Unidad (bulto, varilla, tubo, unidad, kg),
  PrecioCompra, PrecioVenta, Stock, StockMinimo, PesoKg, VolumenUnitarioM3, UbicacionId, VentasMes.
- **Categoria**: Id, Nombre, PadreId (null si es categoría principal). Árbol de **dos niveles**,
  por ejemplo Construcción → Cementos; Plomería → Tubería PVC.
- **Ubicacion**: Id (ej. "A-01-2"), Zona, X, Y, Z (nivel de estante), CapacidadM3, NodoGrafo (nodo más cercano).
- **Movimiento**: Id, Fecha, CodigoProducto, Tipo (Entrada, Salida, Ajuste), Cantidad, Observacion.
- **Pedido**: Id, Fecha, Cliente, Prioridad (Urgente = 1, Mayorista = 2, Mostrador = 3), Items, Estado
  (Pendiente, Alistado, Despachado).
- **ItemPedido**: CodigoProducto, Cantidad.

Datos de ejemplo: unos 40 productos reales de ferretería (cemento gris 50 kg, varilla 1/2", tubo PVC 1/2",
tornillo drywall, pintura galón, arena por m³, etc.), 6 zonas y 12 a 15 nodos de bodega.

## 5. Módulos (alcance mínimo viable)

1. **Productos**: crear, editar, eliminar, buscar y listar (DataGridView), filtrar por categoría.
2. **Movimientos**: entradas y salidas que actualizan el stock. Botón **Deshacer** con la `Pila` propia.
3. **Pedidos**: registrar pedidos, atenderlos en orden de prioridad con la `ColaPrioridad` propia
   (a igual prioridad, el más antiguo primero). Al despachar se generan las salidas.
4. **Mapa de bodega**: dibujo en un `Panel` (GDI+) de zonas y estantes según X, Y. Al elegir un pedido se
   calcula y resalta la ruta de alistamiento sobre el `Grafo` (Dijkstra desde el mostrador).
5. **Calculadora matemática**: volumen de montículos de arena o gravilla, área útil, costo de ubicación
   y optimización de pedidos con Lagrange.
6. **Indicadores**: rotación de inventario, ocupación de la bodega, clasificación ABC, productos bajo el
   stock mínimo.
7. **Comparador de algoritmos**: ordena N productos con Burbuja, Inserción y `List.Sort()`, mide con
   `Stopwatch` para N = 100, 1.000, 5.000 (datos generados) y muestra la tabla. Muestra también búsqueda
   lineal vs binaria.

**Asincronía**: guardar en JSON con `GuardarAsync`, y el comparador de algoritmos y el reporte de
indicadores se ejecutan con `await Task.Run(...)` mostrando una barra de progreso para no congelar la ventana.

## 6. Estructuras de datos y su justificación

| Estructura | Implementación | Uso | Complejidad clave |
|---|---|---|---|
| Pila enlazada | Propia (`Nodo<T>`) | Deshacer movimientos | Push/Pop O(1) |
| Cola de prioridad (heap binario min) | Propia (sobre arreglo) | Atención de pedidos | Insertar/Extraer O(log n) |
| Grafo con lista de adyacencia + Dijkstra | Propia | Ruta de alistamiento | O((V+E) log V) o O(V²) simple |
| Burbuja / Inserción | Propias | Comparación de ordenamientos | O(n²) |
| Búsqueda binaria | Propia | Buscar por código en lista ordenada | O(log n) |
| `Dictionary<string, Producto>` | .NET | Acceso por código | O(1) promedio |
| `List<T>` | .NET | Almacenamiento general | — |
| Árbol de categorías (2 niveles) | Modelo con PadreId | Clasificación, `TreeView` | — |

Dijkstra puede hacerse en la versión O(V²) sin heap, que es más fácil de explicar.

## 7. Modelo matemático (los estudiantes lo resuelven a mano; el software usa el resultado)

1. **Representación en ℝ³**: el mostrador es el origen; cada estante tiene coordenadas (x, y, z) en metros.
2. **Volumen de montículo de arena (integral triple / doble en polares)**:
   altura z = h(1 − r²/R²). V = ∫₀^{2π} ∫₀^R h(1 − r²/R²) r dr dθ = **πR²h/2**.
   Alternativa cónica: V = πR²h/3. La calculadora permite elegir y compara ambos.
3. **Área útil (integral doble)**: área de la bodega menos pasillos; ocupación = volumen usado / volumen útil.
4. **Costo de ubicación (función de varias variables)**:
   C(x, y, z) = r · (a·√(x² + y²) + b·p·z), con r = ventas del mes, p = peso.
   El gradiente indica que el costo baja acercando el producto al origen y al nivel bajo.
   El software sugiere, para cada producto, la ubicación libre de menor costo.
5. **Optimización con Lagrange (cantidad a pedir con espacio limitado)**:
   minimizar Σ (Dᵢ·Sᵢ / Qᵢ + hᵢ·Qᵢ / 2) sujeto a Σ vᵢ·Qᵢ ≤ V.
   Solución: Qᵢ = √(2·Dᵢ·Sᵢ / (hᵢ + 2λ·vᵢ)). Si con λ = 0 cabe en el espacio, esa es la respuesta;
   si no, λ se busca con bisección (un ciclo simple). Se trabaja con **2 o 3 productos**.

El detalle de cada cálculo va en `docs/modelo-matematico.md` con la deducción paso a paso.

## 8. Indicadores administrativos

- Rotación = ventas del periodo (costo) / inventario promedio (costo).
- Días de inventario = 30 / rotación mensual.
- Ocupación (%) = volumen ocupado / volumen útil.
- Clasificación ABC: ordenar por valor vendido; A = 80% acumulado, B = siguiente 15%, C = resto.
- Productos bajo stock mínimo.
- Exactitud de inventario = conteo físico vs sistema (se registra por ajuste).

## 9. Pruebas (xUnit)

Pruebas para: Pila, ColaPrioridad, Ordenamientos, Búsqueda binaria, Dijkstra, Volumenes, Lagrange,
ArchivoJson (guardar y volver a cargar en una carpeta temporal) e indicadores ABC.

## 10. Cronograma (4 semanas)

| Semana | Software | Otras asignaturas |
|---|---|---|
| 1 | Repo, estructura, modelos, JSON + semilla, FrmPrincipal, CRUD productos | Metodología: pregunta, objetivos, variables, instrumentos. Administración: diagnóstico y DOFA. Visita a la ferretería: observación cronometrada (medición "antes") |
| 2 | Movimientos + Pila (deshacer), Pedidos + ColaPrioridad, ordenamientos, búsquedas y comparador | Cálculo: modelo de la bodega, volúmenes e integrales a mano |
| 3 | Grafo + Dijkstra + mapa, calculadora matemática, indicadores, pruebas xUnit | Cálculo: Lagrange y gradiente. Metodología: datos "después" con simulación. Administración: indicadores y plan P-O-D-C |
| 4 | Integración, asincronía, manejo de errores, pulido, `TODO estudiante` | Informe integrador, guía de sustentación, ensayo de la sustentación |

## 11. Tareas para los estudiantes

Dejar en el código comentarios `// TODO estudiante:` con tareas pequeñas y reales para que cada integrante
haga sus propios commits. Ejemplos:

- Cargar los productos reales de la ferretería visitada en `productos.json`.
- Ajustar coordenadas de los estantes al plano real de la bodega.
- Agregar un indicador nuevo (ej. margen de utilidad por categoría).
- Agregar una validación faltante en un formulario.
- Cambiar textos, colores o íconos de la interfaz.
- Agregar una prueba unitaria nueva.

## 12. Convenciones de Git

- Mensajes en español: `feat: CRUD de productos`, `fix: validar stock negativo`, `docs: modelo matemático`,
  `test: pruebas de la cola de prioridad`.
- Un commit por funcionalidad terminada que compile.
- Rama principal `main`; los estudiantes trabajan en ramas propias y hacen pull request.
