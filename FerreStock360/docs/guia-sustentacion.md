# Guía de sustentación – FerreStock 360

Guion para presentar el proyecto, reparto de temas, preguntas que probablemente harán los jurados y
tareas pendientes del equipo. Duración sugerida: **20 minutos de presentación + 10 de preguntas**.

---

## 1. Antes de la sustentación (lista de chequeo)

- [ ] `dotnet test` en verde (todas las pruebas pasan). Llevar la captura por si piden evidencia.
- [ ] Datos reales de la ferretería cargados en `data/*.json` (productos, ubicaciones, pasillos).
- [ ] Medidas de la visita puestas en el código: velocidad caminando y segundos por ítem
      (`BodegaServicio`), y coeficientes a y b (`CostoUbicacion`).
- [ ] Tiempos "antes" cronometrados en la visita, en una tabla, para compararlos con la simulación.
- [ ] Tabla del comparador repetida en el computador de la presentación
      (`docs/justificacion-estructuras.md`, sección 5).
- [ ] Dejar 2 o 3 pedidos pendientes y uno alistado para la demostración.
- [ ] Ensayar al menos una vez con cronómetro. Cada integrante debe poder explicar **cualquier**
      archivo, no solo el suyo.

---

## 2. Guion de la demostración (≈ 12 minutos)

El hilo conductor es la **pregunta de investigación**: *¿en qué medida un sistema que integra la
ubicación espacial, la priorización de pedidos y la optimización del espacio reduce el tiempo de
alistamiento y los errores de inventario?*

| # | Pantalla | Qué mostrar | Qué decir |
|---|---|---|---|
| 1 | Principal | Barra de estado | "Al abrir se cargan los JSON. Abajo se ve cuántos productos hay, cuántos están bajo el mínimo y cuántos pedidos esperan." |
| 2 | Productos | Árbol de categorías, búsqueda, fila roja | "El árbol tiene dos niveles (`PadreId`). La búsqueda por código usa un `Dictionary`, O(1). Las filas rojas están bajo el stock mínimo." |
| 3 | Productos | Intentar guardar con código repetido o vacío | "Todas las validaciones están en `InventarioServicio`; el formulario solo muestra el mensaje." |
| 4 | Movimientos | Registrar entrada → salida → **Deshacer** dos veces | "Cada movimiento se apila en nuestra `Pila` enlazada. Deshacer saca el último: LIFO, O(1)." |
| 5 | Movimientos | Ajuste con conteo físico | "El ajuste guarda la diferencia con el sistema. Con eso medimos la exactitud del inventario." |
| 6 | Pedidos | Crear un pedido **Urgente** y ver que pasa adelante en la cola | "La cola es un heap mínimo sobre un arreglo. Insertar y extraer cuestan O(log n). A igual prioridad, el más antiguo primero." |
| 7 | Pedidos | **Atender siguiente** → ver la ruta en el mapa | "Dijkstra desde el mostrador; con varias paradas vamos siempre a la más cercana." |
| 8 | Pedidos | **Despachar** y volver a Movimientos | "Al despachar se generan las salidas. Si falta stock de un producto no se despacha nada: nunca queda un pedido a medias." |
| 9 | Calculadora → Montículo | R = 2, h = 1,5 | "Integral doble en polares: πR²h/2. La suma de Riemann lo confirma. El cono da un tercio menos." |
| 10 | Calculadora → Costo | Cemento en D-01-1, luego *Sugerir ubicaciones* | "C(x, y, z) = r(a√(x²+y²) + b·p·z). El gradiente es positivo: conviene acercar y bajar. El sistema sugiere la ubicación libre de menor costo." |
| 11 | Calculadora → Lagrange | Ejemplo con V = 4 m³ | "Sin restricción no cabe; la bisección encuentra λ y reduce sobre todo el cemento, que es el que más volumen ocupa." |
| 12 | Indicadores | Resumen, ABC y **simulación** | "Este es el resultado para la pregunta de investigación: con las ubicaciones sugeridas, el tiempo de alistamiento baja X %." |
| 13 | Comparador | Ejecutar | "Burbuja e Inserción crecen como n²; `List.Sort` como n log n. La ventana no se congela porque usamos `await Task.Run`." |

> **Cierre (1 minuto):** comparar el tiempo **cronometrado** en la visita ("antes") con el tiempo
> **simulado** ("después"), aclarar que la simulación es una estimación y proponer la medición real
> como siguiente paso.

---

## 3. Reparto sugerido por asignatura

Ajusten los nombres; lo importante es que cada asignatura tenga un responsable que la domine.

| Integrante | Asignatura | Temas y archivos que debe dominar |
|---|---|---|
| 1 | Programación de Computadores III | Arquitectura por capas (Formularios → Servicios → Datos), `ArchivoJson` (guardado seguro), `async/await`, manejo de errores (`try/catch`, `Program.cs`), validaciones |
| 2 | Estructura de Datos | `Pila`, `ColaPrioridad`, `Grafo` + Dijkstra, ordenamientos, búsquedas, comparador. Documento `justificacion-estructuras.md` |
| 3 | Cálculo Multivariable | `Volumenes`, `CostoUbicacion` (gradiente), `OptimizacionLagrange`. Documento `modelo-matematico.md` (saber deducir en el tablero) |
| 4 | Metodología de la Investigación | Pregunta, variables (tiempo de alistamiento, exactitud), instrumentos (cronometraje), mediciones "antes" y "después", alcance de la simulación |
| 5 | Fundamentos de Administración | Indicadores (rotación, días de inventario, ABC, ocupación, exactitud), DOFA, plan P-O-D-C |

---

## 4. Recorrido por el código (si piden "muéstrenos cómo funciona")

| Pregunta del jurado | Archivo y método |
|---|---|
| ¿Dónde se guardan los datos? | `Datos/ArchivoJson.cs` → `Cargar`, `GuardarAsync` |
| ¿Cómo se valida un producto? | `Servicios/InventarioServicio.cs` → `Validar` |
| ¿Cómo funciona Deshacer? | `Estructuras/Pila.cs` y `Servicios/MovimientoServicio.cs` → `DeshacerAsync` |
| ¿Cómo se ordenan los pedidos? | `Estructuras/ColaPrioridad.cs` → `VaAntes`, `Subir`, `Bajar` |
| ¿Cómo se calcula la ruta? | `Estructuras/Grafo.cs` → `Dijkstra`; `Servicios/BodegaServicio.cs` → `CalcularRuta` |
| ¿Cómo se dibuja el mapa? | `Formularios/FrmMapaBodega.cs` → `pnlMapa_Paint`, `APantalla` |
| ¿De dónde sale la sugerencia de ubicación? | `Servicios/BodegaServicio.cs` → `SugerirUbicaciones` |
| ¿Cómo se clasifica ABC? | `Servicios/IndicadorServicio.cs` → `ClasificarABC` |
| ¿Por qué no se congela la ventana? | `Formularios/FrmComparadorAlgoritmos.cs` y `FrmIndicadores.cs` → `await Task.Run(...)` |

---

## 5. Preguntas probables y cómo responderlas

### Programación III

- **¿Por qué JSON y no una base de datos?** Es una decisión del plan: el volumen de datos es pequeño,
  los archivos se pueden leer y editar a mano y no hay que instalar nada. La desventaja es que no
  sirve para varios usuarios al mismo tiempo.
- **¿Qué pasa si se va la luz mientras se guarda?** `GuardarAsync` escribe primero un `.tmp` y luego
  reemplaza el archivo con `File.Move`. El JSON nunca queda cortado.
- **¿Por qué `async void` en los botones?** Windows Forms no espera un `Task` en los eventos. Es la
  única excepción a la regla, y por eso cada uno tiene su `try/catch`.
- **¿Qué hace `await Task.Run`?** Manda el cálculo a otro hilo. `await` espera el resultado sin
  bloquear la ventana y vuelve al hilo de la ventana para actualizar las tablas.
- **¿Por qué el formulario no tiene la lógica?** Para poder probarla con xUnit sin abrir ventanas, y
  porque así cada clase tiene una sola responsabilidad.

### Estructura de Datos

- **¿Por qué una pila para Deshacer?** Lo último que se hizo es lo primero que se deshace: LIFO.
- **¿Por qué heap y no una lista ordenada?** Con una lista ordenada insertar cuesta O(n); con un heap,
  insertar y extraer cuestan O(log n).
- **Dibuje el heap después de insertar estos pedidos.** Practicar en el tablero con 5 o 6 pedidos:
  insertar al final y subir; extraer la raíz, poner el último arriba y bajar.
- **¿Por qué Dijkstra O(V²) y no con heap?** Con 13 nodos no se nota la diferencia, y la versión simple
  se puede seguir a mano. Con heap sería O((V + E) log V).
- **¿La ruta es la óptima?** Cada tramo es el más corto (Dijkstra). El **orden** de las paradas usa el
  vecino más cercano, que no siempre es óptimo: el problema exacto es el del agente viajero.
- **¿Por qué la búsqueda binaria necesita la lista ordenada?** Porque decide qué mitad descartar
  comparando con el elemento del medio.
- **¿Por qué se usa `Dictionary` si hay búsqueda binaria?** El diccionario da O(1) promedio y no
  necesita mantener la lista ordenada. La binaria se implementó para compararla con la lineal.

### Cálculo Multivariable

- **Deduzca el volumen del montículo.** Ver `modelo-matematico.md`, sección 2: polares, dA = r dr dθ,
  integral interna hR²/4 y externa × 2π.
- **¿Qué significa el gradiente del costo?** Apunta hacia donde el costo sube más rápido. Todas sus
  componentes son positivas, así que conviene moverse hacia −∇C: más cerca del mostrador y más abajo.
- **¿Qué es λ?** El precio sombra del espacio: cuánto bajaría el costo anual con 1 m³ más de bodega.
- **¿Por qué bisección?** Porque el volumen usado disminuye cuando λ aumenta (función monótona), así
  que siempre se puede partir el intervalo a la mitad hacia el valor que cumple Σ vᵢQᵢ = V.

### Metodología de la Investigación

- **¿Cuáles son las variables?** Independiente: el uso del sistema (ubicación optimizada y priorización).
  Dependientes: tiempo de alistamiento (min/pedido) y exactitud del inventario (% de conteos exactos).
- **¿La reducción es real?** El "después" es una **simulación** con el modelo de tiempo
  (distancia / velocidad + tiempo por ítem), calibrado con lo medido en la visita. Para confirmarlo
  habría que reubicar los productos y volver a cronometrar.
- **¿Qué limitaciones tiene?** Ver la sección 7 de esta guía.

### Fundamentos de Administración

- **¿Qué indica la rotación?** Cuántas veces se vende el inventario en el mes. Los días de inventario
  (30 / rotación) dicen para cuántos días alcanza lo que hay en bodega.
- **¿Para qué sirve el ABC?** Los productos A (80 % del valor vendido) merecen más control, mejores
  ubicaciones y conteos más frecuentes.
- **¿Cómo se mide la exactitud del inventario?** Porcentaje de conteos físicos (ajustes) en los que el
  sistema coincidió con lo contado.

---

## 6. Tareas pendientes del equipo (`TODO estudiante`)

Cada integrante debe tomar al menos una tarea y entregarla en **su propia rama con su propio commit**,
para que quede la evidencia en el historial de Git.

| # | Tarea | Archivo | Dificultad |
|---|---|---|---|
| 1 | Cargar los productos y categorías reales de la ferretería visitada | `Datos/DatosIniciales.cs` (o directamente los `data/*.json`) | Baja |
| 2 | Ajustar las coordenadas de los estantes al plano real | `Datos/DatosIniciales.cs` → `CrearUbicaciones` | Media |
| 3 | Dibujar los pasillos reales (nodos y vecinos) | `Datos/DatosIniciales.cs` → `CrearNodosBodega` | Media |
| 4 | Poner la velocidad y el tiempo por ítem medidos en la visita | `Servicios/BodegaServicio.cs` | Baja |
| 5 | Ajustar y justificar los coeficientes a y b | `Matematica/CostoUbicacion.cs` | Media |
| 6 | Validar que el precio de venta no sea menor que el de compra | `Servicios/InventarioServicio.cs` → `Validar` | Baja |
| 7 | Validar que el stock quepa en la capacidad de la ubicación | `Servicios/InventarioServicio.cs` → `Validar` | Media |
| 8 | Filtrar movimientos por producto o por tipo | `Formularios/FrmMovimientos.cs` | Baja |
| 9 | Botón "Exportar" de la tabla de productos a CSV | `Formularios/FrmProductos.cs` | Media |
| 10 | Botón "Aplicar sugerencias" de ubicación (con confirmación) | `Formularios/FrmCalculadora.cs` | Media |
| 11 | Indicador nuevo: margen de utilidad por categoría | `Servicios/IndicadorServicio.cs` | Media |
| 12 | Guardar el valor del inventario al inicio de cada mes para la rotación | `Servicios/IndicadorServicio.cs` | Alta |
| 13 | Otro criterio de desempate en la cola de prioridad | `Estructuras/ColaPrioridad.cs` → `VaAntes` | Baja |
| 14 | Agregar N = 10.000 al comparador y explicar el resultado | `Algoritmos/Comparador.cs` | Baja |
| 15 | Prueba nueva: apilar 1.000 elementos | `FerreStock.Pruebas/PilaPruebas.cs` | Baja |
| 16 | Colores de las zonas del mapa | `Formularios/FrmMapaBodega.cs` | Baja |
| 17 | Nombres del equipo en "Acerca de"; textos, colores e ícono de la ventana principal | `Formularios/FrmPrincipal.cs` | Baja |

**Recomendación:** si cambian los datos de ejemplo (tareas 1 a 3), corran `dotnet test`. Las pruebas
`DatosInicialesPruebas` revisan que no haya códigos repetidos, que las ubicaciones existan y que no se
supere la capacidad. Algunas pruebas de servicios usan valores de la semilla (por ejemplo, la
distancia de la ruta del pedido 3) y habrá que actualizarlas.

---

## 7. Limitaciones conocidas (mejor decirlas antes de que las pregunten)

- El **tiempo "después" es simulado**, no medido.
- La **rotación** usa el inventario actual en vez del inventario promedio del mes, porque todavía no se
  guardan cierres mensuales (tarea 12).
- Las **sugerencias de ubicación** solo miran el volumen: pueden juntar muchos productos pequeños en el
  mismo espacio barato. Tampoco consideran que algunos productos deban ir juntos.
- El **orden de las paradas** usa el vecino más cercano, que no garantiza la ruta óptima.
- **Deshacer** solo funciona en la sesión actual, y no deshace los despachos de pedidos.
- El sistema es para **un solo usuario**: dos personas con el programa abierto a la vez sobre los mismos
  archivos pueden pisarse los cambios.
