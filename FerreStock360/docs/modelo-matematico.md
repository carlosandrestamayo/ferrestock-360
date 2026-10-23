# Modelo matemático de la bodega

Este documento muestra **la deducción a mano** de cada fórmula que usa FerreStock 360. El software no
deduce nada: solo evalúa el resultado final. Sirve como evidencia para Cálculo Multivariable y como
guía para la sustentación. Al final de cada sección se indica el archivo donde está programado.

> **Nota para el equipo:** los datos de ejemplo son de una ferretería ficticia. Después de la visita,
> reemplacen las medidas y repitan los cálculos con los datos reales.

---

## 1. La bodega en ℝ³

Se toma el **mostrador como origen** O = (0, 0, 0). Cada punto de la bodega se describe con:

- x: metros hacia la derecha del mostrador,
- y: metros hacia el fondo,
- z: altura del nivel del estante sobre el piso.

En los datos de ejemplo la bodega mide 26 m × 16 m y los estantes tienen tres niveles, con
z = 0,2 m (estiba en el piso), 1,0 m y 1,8 m. Por ejemplo, el cemento está en D-01-1 = (22, 4, 0,2).

La **distancia en planta** de un estante al mostrador es la norma del vector (x, y):

$$d(x, y) = \sqrt{x^2 + y^2}$$

Para D-01-1: d = √(22² + 4²) = √500 ≈ 22,36 m.

Para calcular rutas no se usa esa distancia en línea recta, porque el bodeguero camina por los pasillos.
Los pasillos forman un **grafo**: los nodos son cruces y las aristas son tramos rectos, cuyo peso es la
distancia euclidiana entre sus extremos (ver `docs/justificacion-estructuras.md`, sección 8).

📁 `Datos/DatosIniciales.cs`, `Servicios/BodegaServicio.cs` (`Distancia`)

---

## 2. Volumen de un montículo de arena (integral doble en polares)

### 2.1 Modelo

La arena descargada forma un montículo redondo. Se modela con un **paraboloide**: en el centro la
altura es h y en el borde (radio R) la altura es 0:

$$z(r) = h\left(1 - \frac{r^2}{R^2}\right), \quad 0 \le r \le R$$

Verificación: z(0) = h y z(R) = h(1 − 1) = 0. ✔

### 2.2 Planteamiento

El volumen bajo la superficie es la integral doble de la altura sobre el círculo de radio R. Como el
montículo es simétrico alrededor del eje z, conviene usar **coordenadas polares**, donde dA = r dr dθ:

$$V = \iint_{D} z \, dA = \int_0^{2\pi} \int_0^R h\left(1 - \frac{r^2}{R^2}\right) r \, dr \, d\theta$$

(Es lo mismo que la integral triple $\iiint dV$ con 0 ≤ z ≤ h(1 − r²/R²).)

### 2.3 Integral interna (respecto a r)

$$\int_0^R h\left(r - \frac{r^3}{R^2}\right) dr
= h\left[\frac{r^2}{2} - \frac{r^4}{4R^2}\right]_0^R
= h\left(\frac{R^2}{2} - \frac{R^2}{4}\right)
= \frac{hR^2}{4}$$

### 2.4 Integral externa (respecto a θ)

El resultado anterior no depende de θ:

$$V = \int_0^{2\pi} \frac{hR^2}{4} \, d\theta = \frac{hR^2}{4} \cdot 2\pi = \boxed{\frac{\pi R^2 h}{2}}$$

### 2.5 Alternativa cónica

Si los lados del montículo fueran rectos (cono), z(r) = h(1 − r/R):

$$V_{cono} = \int_0^{2\pi}\int_0^R h\left(r - \frac{r^2}{R}\right) dr\, d\theta
= 2\pi h\left(\frac{R^2}{2} - \frac{R^2}{3}\right) = \frac{\pi R^2 h}{3}$$

**Comparación:** V_paraboloide / V_cono = (1/2) / (1/3) = 1,5. Con el mismo radio y la misma altura, el
paraboloide tiene un **50 % más** de arena, porque sus lados son "redondeados" y no rectos. Para
estimar cuánta arena queda, conviene medir el montículo y escoger el modelo que más se parezca.

### 2.6 Ejemplo numérico

R = 2 m, h = 1,5 m:

- Paraboloide: V = π · 4 · 1,5 / 2 = 3π ≈ **9,425 m³**
- Cono: V = π · 4 · 1,5 / 3 = 2π ≈ **6,283 m³**
- Con la arena de pega a $90.000/m³, el montículo vale ≈ $848.000 (paraboloide) o $565.000 (cono).

### 2.7 Comprobación numérica (suma de Riemann)

La calculadora también aproxima la integral partiendo el círculo en n anillos y n tajadas y sumando
z(rᵢ) · rᵢ · Δr · Δθ en cada pedacito (rᵢ = punto medio del anillo). Con n = 100 da 9,4252 m³, que
coincide con la fórmula hasta la cuarta cifra.

📁 `Matematica/Volumenes.cs` (`MonticuloParabolico`, `MonticuloConico`, `MonticuloParabolicoNumerico`)

---

## 3. Área útil y ocupación (integral doble)

El área de una región D es $A = \iint_D 1 \, dA$. Para la bodega rectangular [0, a] × [0, b]:

$$A_{bodega} = \int_0^b \int_0^a dx\, dy = a \cdot b$$

El **área útil** es la de la bodega menos la de los pasillos, porque en los pasillos no se puede guardar
nada. Con k pasillos rectangulares de ancho w y largo ℓ:

$$A_{útil} = a\,b - k\,w\,\ell$$

El **volumen útil** se obtiene multiplicando por la altura útil H de los estantes, y la **ocupación** es:

$$\text{Ocupación} = \frac{V_{ocupado}}{V_{útil}} \times 100\%, \qquad V_{ocupado} = \sum_i \text{stock}_i \cdot v_i$$

**Ejemplo:** a = 26, b = 16, 3 pasillos de 1,5 m × 26 m, H = 2 m:

- A_bodega = 416 m², A_pasillos = 117 m², **A_útil = 299 m²**, V_útil = 598 m³.
- El inventario de ejemplo ocupa 16,19 m³ → ocupación del volumen útil ≈ 2,7 %.
- Medida contra la capacidad de los estantes registrados (96 m³), la ocupación es ≈ 16,9 %.
  Este es el valor que usa el módulo de Indicadores.

📁 `Matematica/Volumenes.cs` (`AreaUtil`, `VolumenUtil`, `PorcentajeOcupacion`)

---

## 4. Costo de ubicación (función de varias variables y gradiente)

### 4.1 La función

¿Cuánto "cuesta" tener un producto en el punto (x, y, z)? Depende de dos cosas:

1. **Caminar:** cada venta obliga a ir hasta el estante; eso es proporcional a la distancia en planta.
2. **Subir peso:** guardar y bajar productos pesados de niveles altos cuesta esfuerzo (y es riesgoso);
   eso es proporcional a peso × altura.

Ambas cosas se repiten tantas veces como ventas haya. Por eso:

$$C(x, y, z) = r\left(a\sqrt{x^2 + y^2} + b\,p\,z\right)$$

| Símbolo | Significado | Valor en el software |
|---|---|---|
| r | ventas del mes del producto | `Producto.VentasMes` |
| p | peso de una unidad (kg) | `Producto.PesoKg` |
| a | peso de la distancia | 1 |
| b | peso de la altura | 0,1 (subir 1 kg a 1 m "equivale" a caminar 0,1 m) |

### 4.2 Derivadas parciales

Sea d = √(x² + y²). Por la regla de la cadena, ∂d/∂x = x/d y ∂d/∂y = y/d. Entonces:

$$\frac{\partial C}{\partial x} = \frac{r\,a\,x}{\sqrt{x^2+y^2}}, \qquad
\frac{\partial C}{\partial y} = \frac{r\,a\,y}{\sqrt{x^2+y^2}}, \qquad
\frac{\partial C}{\partial z} = r\,b\,p$$

$$\nabla C = \left( \frac{r a x}{d},\; \frac{r a y}{d},\; r b p \right)$$

### 4.3 Interpretación del gradiente

En la bodega x, y, z ≥ 0 y r, a, b, p > 0, así que **las tres componentes del gradiente son positivas**:
el costo crece al alejarse del mostrador y al subir de nivel. La dirección de máximo descenso es
**−∇C**, que apunta hacia el origen y hacia abajo. Conclusión práctica: **los productos de alta venta
deben ir cerca del mostrador, y los pesados en el nivel bajo.**

Además, ∂C/∂z = r·b·p no depende de la posición: subir un nivel cuesta lo mismo en cualquier parte de
la bodega, pero cuesta mucho más para un producto pesado y muy vendido como el cemento.

### 4.4 Ejemplo: cemento gris 50 kg en D-01-1

r = 120, p = 50, (x, y, z) = (22, 4, 0,2), d = √500 ≈ 22,36:

- C = 120 · (22,36 + 0,1 · 50 · 0,2) = 120 · 23,36 ≈ **2.803**
- ∇C ≈ (120 · 22/22,36, 120 · 4/22,36, 120 · 0,1 · 50) ≈ **(118,1; 21,5; 600)**

Si se mueve a A-01-1 = (4, 4, 0,2): d = √32 ≈ 5,66 y C = 120 · (5,66 + 1) ≈ **799**, un 71 % menos.

### 4.5 Cómo elige el software la ubicación sugerida

Minimizar C para cada producto por separado haría que todos quisieran la misma ubicación. Por eso se
usa una regla **voraz** con capacidad limitada:

1. Se ordenan los productos por r·(a + b·p), que mide cuánto aumenta su costo por cada metro que se
   alejan o suben. Los más "sensibles" escogen primero.
2. Cada producto toma la ubicación de menor C donde todavía quepa su volumen (stock × volumen unitario).
3. Se descuenta ese volumen de la capacidad de la ubicación.

**Limitación:** la capacidad se mide solo en volumen. Muchos productos pequeños pueden quedar sugeridos
en el mismo espacio barato (por ejemplo, A-01-1), algo que en la realidad no siempre es práctico.
Una mejora sería limitar también la cantidad de referencias por ubicación.

📁 `Matematica/CostoUbicacion.cs`, `Servicios/BodegaServicio.cs` (`SugerirUbicaciones`)

---

## 5. Cantidad a pedir con espacio limitado (multiplicadores de Lagrange)

### 5.1 El problema

Para cada producto i se decide cuántas unidades Qᵢ pedir cada vez. Hay dos costos opuestos:

- **Hacer pedidos:** se hacen Dᵢ/Qᵢ pedidos al año y cada uno cuesta Sᵢ → Dᵢ·Sᵢ/Qᵢ.
- **Guardar mercancía:** en promedio hay Qᵢ/2 unidades en bodega y cada una cuesta hᵢ al año → hᵢ·Qᵢ/2.

Pedidos grandes ahorran transporte pero ocupan espacio. La bodega tiene un volumen V disponible:

$$\min_{Q} \; C(Q) = \sum_{i} \left( \frac{D_i S_i}{Q_i} + \frac{h_i Q_i}{2} \right)
\quad \text{sujeto a} \quad g(Q) = \sum_i v_i Q_i - V \le 0$$

### 5.2 El lagrangiano

$$L(Q, \lambda) = \sum_i \left( \frac{D_i S_i}{Q_i} + \frac{h_i Q_i}{2} \right) + \lambda \left( \sum_i v_i Q_i - V \right), \quad \lambda \ge 0$$

### 5.3 Condición de primer orden

Se deriva respecto a cada Qᵢ y se iguala a cero:

$$\frac{\partial L}{\partial Q_i} = -\frac{D_i S_i}{Q_i^2} + \frac{h_i}{2} + \lambda v_i = 0$$

Se despeja Qᵢ:

$$\frac{D_i S_i}{Q_i^2} = \frac{h_i + 2\lambda v_i}{2}
\;\;\Longrightarrow\;\;
\boxed{Q_i(\lambda) = \sqrt{\frac{2 D_i S_i}{h_i + 2 \lambda v_i}}}$$

**Comprobación de que es un mínimo:** ∂²C/∂Qᵢ² = 2DᵢSᵢ/Qᵢ³ > 0, así que la función es convexa.

### 5.4 Cómo se encuentra λ

- **Caso 1, la restricción no se activa (λ = 0):** Qᵢ = √(2DᵢSᵢ/hᵢ) es la fórmula clásica del lote
  económico (EOQ). Si Σ vᵢQᵢ ≤ V, ya está.
- **Caso 2, la restricción se activa (λ > 0):** hay que encontrar el λ que cumple Σ vᵢ Qᵢ(λ) = V.
  Como Qᵢ(λ) **disminuye** cuando λ aumenta, el volumen usado también disminuye, y se puede usar
  **bisección**:
  1. Se busca un λ_alto con el que sí quepa (empezando en 1 y duplicando).
  2. Se toma λ_medio = (λ_bajo + λ_alto)/2. Si con λ_medio no cabe, λ_bajo = λ_medio; si cabe,
     λ_alto = λ_medio.
  3. Se repite hasta que el intervalo sea muy pequeño.

**Interpretación de λ:** es el *precio sombra* del espacio, es decir, cuánto bajaría el costo anual
si la bodega tuviera 1 m³ más.

### 5.5 Ejemplo con 3 productos

Demanda anual = ventas del mes × 12; h = 25 % del precio de compra; S = $50.000 por pedido.

| Producto | D | S | h | v (m³) | Q (λ = 0) | v·Q (m³) |
|---|---:|---:|---:|---:|---:|---:|
| Cemento gris 50 kg | 1.440 | 50.000 | 7.000 | 0,036 | 143,4 | 5,16 |
| Varilla 1/2" | 1.800 | 50.000 | 5.500 | 0,0008 | 180,9 | 0,14 |
| Tubo PVC 1/2" | 840 | 50.000 | 2.375 | 0,0027 | 188,1 | 0,51 |
| **Total** | | | | | | **5,82** |

Con V = 4 m³ **no cabe** (5,82 > 4). La bisección da λ ≈ 125.681 y:

| Producto | Q óptimo | Volumen (m³) |
|---|---:|---:|
| Cemento | 94,7 | 3,410 |
| Varilla | 177,7 | 0,142 |
| Tubo PVC | 165,9 | 0,448 |
| **Total** | | **4,000** |

El cemento es el que más se reduce porque es el que más volumen ocupa por unidad (v grande en el
denominador). La varilla casi no cambia.

📁 `Matematica/OptimizacionLagrange.cs`

---

## 6. Tiempo de alistamiento y simulación "antes / después"

Para responder la pregunta de investigación, el software estima el tiempo de alistar un pedido:

$$T = \frac{\text{distancia de la ruta}}{v_{caminata}} + n_{ítems} \cdot t_{ítem}$$

con v_caminata = 0,8 m/s y t_ítem = 30 s. **Estos valores deben reemplazarse por los medidos en la
visita** (`BodegaServicio.VelocidadMetrosPorSegundo` y `SegundosPorItem`).

La ruta sale del mostrador, recorre todas las paradas y vuelve. Se calcula con Dijkstra y la regla del
vecino más cercano. Con los 5 pedidos de ejemplo:

| | Distancia promedio | Tiempo promedio |
|---|---:|---:|
| Ubicaciones actuales ("antes") | 52,1 m | 2,19 min |
| Ubicaciones sugeridas por C(x, y, z) ("después") | 18,5 m | 1,49 min |
| **Reducción** | **64 %** | **32 %** |

Esta es una **simulación**: la medición "después" en la ferretería real debe hacerse cronometrando
pedidos reales una vez se reubiquen los productos.

📁 `Servicios/BodegaServicio.cs` (`CalcularRuta`, `SimularAntesDespues`), `Servicios/IndicadorServicio.cs`
