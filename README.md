# FerreStock 360

Proyecto integrador **SIGLO 360 – Equipo 2: Ferretería**.

Sistema de inventario para una ferretería con bodega que integra la **ubicación espacial** de los
productos, la **priorización de pedidos** y la **optimización del espacio**, para reducir el tiempo de
alistamiento y los errores de inventario.

## Requisitos

- Windows 10 u 11.
- [.NET SDK 8](https://dotnet.microsoft.com/download) o más nuevo. Los proyectos apuntan a
  `net8.0-windows`; si el equipo solo tiene .NET 10, igual funcionan gracias a `RollForward`.
- Visual Studio 2022 (o más nuevo) con la carga de trabajo "Desarrollo de escritorio de .NET", si se
  quiere usar el diseñador de formularios.

## Cómo ejecutarlo

Desde Visual Studio: abrir `FerreStock360/FerreStock360.slnx`, elegir `FerreStock` como proyecto de
inicio y presionar F5.

Desde la terminal:

```bash
cd FerreStock360
dotnet run --project FerreStock
```

## Cómo correr las pruebas

```bash
cd FerreStock360
dotnet test
```

Son pruebas xUnit de la pila, la cola de prioridad, los ordenamientos, la búsqueda binaria, Dijkstra,
los volúmenes, Lagrange, el guardado en JSON, los indicadores ABC y los servicios.

## Módulos

| Módulo | Qué hace | Estructura o cálculo principal |
|---|---|---|
| Productos | Crear, editar, eliminar, buscar y filtrar por categoría | `Dictionary`, árbol de categorías |
| Movimientos | Entradas, salidas y ajustes; botón Deshacer | Pila enlazada propia |
| Pedidos | Registrar, atender por prioridad y despachar | Cola de prioridad (heap) propia |
| Mapa de bodega | Plano y ruta de alistamiento de un pedido | Grafo + Dijkstra |
| Calculadora | Montículos, área útil, costo de ubicación, Lagrange | Integrales, gradiente, Lagrange |
| Indicadores | Rotación, ocupación, ABC, bajo mínimo, exactitud, simulación | — |
| Comparador | Burbuja vs. Inserción vs. `List.Sort()`; lineal vs. binaria | Ordenamientos y búsquedas propios |

## Estructura

```
FerreStock360/
├─ FerreStock/              Aplicación Windows Forms
│  ├─ Modelos/              Clases de datos (Producto, Pedido, Ubicacion...)
│  ├─ Datos/                Lectura y escritura de JSON y datos iniciales
│  ├─ Estructuras/          Pila, ColaPrioridad, Grafo (hechas a mano)
│  ├─ Algoritmos/           Ordenamientos, búsquedas y comparador
│  ├─ Matematica/           Volúmenes, costo de ubicación, Lagrange
│  ├─ Servicios/            Lógica de cada módulo (los formularios solo muestran y capturan)
│  ├─ Formularios/          Ventanas
│  └─ data/                 Archivos JSON con los datos
├─ FerreStock.Pruebas/      Pruebas xUnit
└─ docs/                    Documentos de apoyo para la sustentación
```

## Datos

Los datos se guardan en archivos JSON en la carpeta `data` que queda junto al ejecutable
(`FerreStock/bin/Debug/net8.0-windows/data`). La ruta exacta aparece en **Ayuda → Acerca de**.

- Si un archivo no existe, se crea con los datos de ejemplo de `Datos/DatosIniciales.cs`.
- Para volver a los datos de ejemplo, basta con borrar los JSON de esa carpeta.
- Los JSON de `FerreStock/data` se copian a la carpeta de salida al compilar, solo si son más nuevos
  que los que ya están allí.

## Documentos

- [`docs/guia-sustentacion.md`](FerreStock360/docs/guia-sustentacion.md): guion de la demostración,
  reparto por integrante, preguntas frecuentes y tareas para el equipo.
- [`docs/modelo-matematico.md`](FerreStock360/docs/modelo-matematico.md): deducción de las fórmulas.
- [`docs/justificacion-estructuras.md`](FerreStock360/docs/justificacion-estructuras.md): por qué
  se eligió cada estructura de datos y su complejidad.
- [`CLAUDE.md`](CLAUDE.md): plan maestro del proyecto.

## Cómo contribuir

- Trabajar en una rama propia (`git checkout -b nombre/tarea`) y abrir un pull request hacia `main`.
- Un commit por funcionalidad terminada que compile, con mensajes en español:
  `feat: ...`, `fix: ...`, `docs: ...`, `test: ...`.
- Las tareas pendientes están marcadas en el código con `// TODO estudiante:` (lista completa en la
  guía de sustentación).
