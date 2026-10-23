# Justificación de las estructuras de datos y algoritmos

Este documento explica **qué estructura se usó en cada parte de FerreStock 360, por qué se eligió
y qué alternativa se descartó**. Sirve como evidencia para Estructura de Datos y como guía para la
sustentación: cada sección dice en qué archivo está el código.

## Resumen

| Estructura / algoritmo | Implementación | Dónde se usa | Operación clave | Complejidad |
|---|---|---|---|---|
| Pila enlazada | Propia: `Estructuras/Nodo.cs`, `Estructuras/Pila.cs` | Deshacer movimientos | Apilar / Desapilar | O(1) |
| Cola de prioridad (heap binario mínimo) | Propia: `Estructuras/ColaPrioridad.cs` | Orden de atención de pedidos | Insertar / ExtraerMinimo | O(log n) |
| Burbuja | Propia: `Algoritmos/Ordenamientos.cs` | Comparador de algoritmos | Ordenar | O(n²) |
| Inserción | Propia: `Algoritmos/Ordenamientos.cs` | Comparador de algoritmos | Ordenar | O(n²) |
| Búsqueda lineal | Propia: `Algoritmos/Busquedas.cs` | Comparador de algoritmos | Buscar | O(n) |
| Búsqueda binaria | Propia: `Algoritmos/Busquedas.cs` | Comparador de algoritmos | Buscar en lista ordenada | O(log n) |
| `Dictionary<string, Producto>` | .NET | `InventarioServicio`: buscar producto por código | Buscar | O(1) promedio |
| `List<T>` | .NET | Almacenamiento general y archivos JSON | Recorrer | O(n) |
| Árbol de categorías (2 niveles) | Modelo `Categoria` con `PadreId` | Filtro con `TreeView` en Productos | Recorrer hijos | O(n) |
| Grafo + Dijkstra | Pendiente (semana 3) | Ruta de alistamiento | — | O(V²) |

---

## 1. Pila enlazada → Deshacer movimientos

**Problema:** el bodeguero registra una entrada o salida equivocada y quiere revertirla. Lo natural es
deshacer **la última** acción, después la anterior, y así. Ese comportamiento es exactamente
**LIFO** (último en entrar, primero en salir): una pila.

**Implementación:** cada `Nodo<T>` guarda un valor y la referencia al nodo de abajo. La pila solo
recuerda el nodo de arriba (`tope`).

- `Apilar`: el nodo nuevo apunta al tope anterior y pasa a ser el tope. Son tres asignaciones, sin
  importar cuántos elementos haya → **O(1)**.
- `Desapilar`: se guarda el valor del tope y el tope baja al siguiente → **O(1)**.

**Por qué enlazada y no un arreglo:** con un arreglo, cuando se llena hay que crear otro más grande y
copiar todo (O(n) en ese momento). La versión enlazada nunca copia y es más fácil de dibujar en el
tablero.

**Decisiones de diseño (en `Servicios/MovimientoServicio.cs`):**
- Antes de desapilar se revisa con `VerTope` si el movimiento se puede revertir (por ejemplo, deshacer
  una entrada cuando ya se vendió esa mercancía dejaría el stock negativo). Si no se puede, la pila
  queda igual.
- La pila **no se guarda** en el archivo: solo se puede deshacer lo hecho en la sesión actual.
- Las salidas que genera un **despacho de pedido no entran a la pila**, porque deshacerlas dejaría el
  pedido como "Despachado" sin que la mercancía haya salido.

## 2. Cola de prioridad (heap binario mínimo) → Atención de pedidos

**Problema:** los pedidos no se atienden en orden de llegada. Un pedido urgente de obra va antes que
uno de mostrador, y a igual prioridad se atiende primero el más antiguo.

**Regla de prioridad** (`ColaPrioridad.VaAntes`):
1. Menor número de prioridad primero: Urgente (1) → Mayorista (2) → Mostrador (3).
2. Si empatan, el de fecha más antigua.
3. Si también empatan, el de Id menor.

**Implementación:** el heap es un árbol binario completo guardado en un arreglo:

```
posición:   0     1     2     3     4
           raíz  hijos de 0  hijos de 1
hijo izquierdo de i = 2i + 1     hijo derecho = 2i + 2     padre de i = (i − 1) / 2
```

La propiedad del heap mínimo es: **todo padre va antes que sus hijos**. Por eso el pedido que se
atiende primero siempre está en la posición 0.

- `Insertar`: se pone al final y se **sube** intercambiándolo con su padre mientras vaya antes.
- `ExtraerMinimo`: se saca la raíz, el último elemento pasa a la raíz y se **baja** intercambiándolo
  con el mejor de sus hijos.

Como el árbol está balanceado, su altura es ⌊log₂ n⌋; subir o bajar recorre como máximo esa altura →
**O(log n)**.

**Alternativas descartadas:**

| Opción | Insertar | Sacar el siguiente | Problema |
|---|---|---|---|
| Lista sin ordenar | O(1) | O(n) (hay que buscar el mínimo) | Lento al atender |
| Lista ordenada | O(n) (hay que correr elementos) | O(1) | Lento al registrar |
| **Heap binario** | **O(log n)** | **O(log n)** | Equilibrado en ambas |

**Ejemplo con los datos de prueba** (`data/pedidos.json`): orden de atención 3 (Urgente) → 1 (Mayorista
8:15) → 5 (Mayorista 10:45) → 2 (Mostrador 9:00) → 4 (Mostrador 10:10). Lo verifica la prueba
`PedidoServicioPruebas.Cola_OrdenaPorPrioridadYLuegoPorFecha`.

## 3. Ordenamientos: Burbuja e Inserción

Ambos ordenan los productos por código (`Ordenamientos.CompararPorCodigo`).

- **Burbuja:** compara vecinos y los intercambia si están al revés. En cada pasada el mayor llega al
  final. Si una pasada no hace intercambios, la lista ya está ordenada y se detiene (mejor caso O(n)).
  Peor caso: n(n−1)/2 comparaciones → **O(n²)**.
- **Inserción:** toma cada elemento y lo corre a la izquierda hasta su lugar, como ordenar cartas en
  la mano. También **O(n²)** en el peor caso, pero en la práctica mueve menos datos que Burbuja.

Se comparan contra `List.Sort()` de .NET, que usa *introsort* (combinación de quicksort, heapsort e
inserción) → **O(n log n)**.

## 4. Búsqueda lineal vs. búsqueda binaria

- **Lineal:** revisa uno por uno. No necesita orden. En promedio revisa n/2 elementos → **O(n)**.
- **Binaria:** necesita la lista **ordenada**. Mira el del medio y descarta la mitad que no puede
  contener el código. Cada paso parte el problema en dos → **O(log n)**.

En el comparador se cuentan las **comparaciones** además del tiempo, porque una sola búsqueda es tan
rápida que el reloj casi no la mide.

## 5. Resultados del comparador

Medición de ejemplo en el equipo de desarrollo (los tiempos cambian en cada computador y ejecución;
cada grupo debe repetirla en el suyo y reemplazar esta tabla):

**Ordenamiento (ms)**

| N | Burbuja | Inserción | List.Sort() |
|---:|---:|---:|---:|
| 100 | 0,28 | 0,11 | 0,04 |
| 1.000 | 12,94 | 3,87 | 0,16 |
| 5.000 | 283,67 | 59,11 | 1,41 |

**Búsqueda (1.000 búsquedas de códigos que existen)**

| N | Comparaciones promedio lineal | Comparaciones promedio binaria | log₂ N |
|---:|---:|---:|---:|
| 100 | 50,9 | 5,8 | 6,6 |
| 1.000 | 495,3 | 9,0 | 10,0 |
| 5.000 | 2.577,4 | 11,3 | 12,3 |

**Cómo leer los resultados:**
- De N = 1.000 a N = 5.000 (×5), Burbuja tardó unas 22 veces más. Si fuera exactamente n², serían
  25 veces más. `List.Sort` tardó unas 9 veces más, cerca de lo que predice n log n (≈ 6).
- La búsqueda lineal hace cerca de N/2 comparaciones; la binaria, cerca de log₂ N. Con 5.000 productos
  la binaria hace unas **230 veces menos** comparaciones.

## 6. Estructuras de .NET que sí se usan y por qué

- **`Dictionary<string, Producto>`** en `InventarioServicio`: buscar un producto por código ocurre en
  cada movimiento, pedido y pantalla. Con una tabla hash es O(1) promedio. No es una estructura que se
  evalúe, por eso se usa la de .NET. Se mantiene siempre sincronizada con la lista: agregar y eliminar
  actualizan las dos.
- **`List<T>`**: es lo que se guarda en los archivos JSON y lo que se recorre para llenar las tablas.

## 7. Árbol de categorías

Cada `Categoria` tiene un `PadreId`: `null` si es principal (Construcción, Plomería…) o el Id de su
categoría principal si es subcategoría (Cementos y pegantes, Tubería PVC…). Es un árbol de dos
niveles que se dibuja en el `TreeView` de Productos. Al elegir una categoría principal se muestran
los productos de todas sus subcategorías (`InventarioServicio.PerteneceACategoria`).

## 8. Pendiente (semana 3)

- **Grafo con lista de adyacencia** de los pasillos de la bodega (13 nodos; el nodo 0 es el mostrador)
  y **Dijkstra O(V²)** para la ruta de alistamiento de un pedido.
