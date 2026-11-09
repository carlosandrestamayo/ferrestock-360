# Guías de estudio por formulario

Estas guías explican **cómo se conectan las clases** del proyecto, formulario por formulario.
La idea es que cualquier integrante del equipo pueda abrir una pantalla en la sustentación y
explicar qué pasa "por debajo" cuando se hace clic en un botón.

## Cómo leer estas guías

1. Abra el programa y la pantalla que va a estudiar.
2. Abra la guía y, al lado, el archivo de código que se menciona.
3. Siga cada "recorrido" (traza) con el dedo: botón → método del formulario → método del servicio → archivo JSON.
4. Al final de cada guía hay **preguntas de sustentación** con su respuesta y **ejercicios** para practicar.

## La regla de oro del proyecto (aplica a todos los formularios)

```
 Formularios/   →   Servicios/   →   Datos/   →   data/*.json
 (muestra y         (reglas y        (lee y        (archivos
  captura)           validaciones)    escribe)      en disco)
                         │
                         ▼
                     Modelos/
             (las "fichas" con los datos:
              Producto, Categoria, ...)
```

- El **formulario** nunca valida reglas del negocio ni toca archivos: solo muestra y pide datos.
- El **servicio** decide si algo es válido y modifica las listas en memoria.
- **ArchivoJson** es la única clase que lee y escribe en disco.
- Los **modelos** son clases con propiedades; viajan entre todas las capas.

## Guías disponibles

| # | Formulario | Guía | Estado |
|---|---|---|---|
| 1 | Productos (`FrmProductos`) | [01-FrmProductos.md](01-FrmProductos.md) | ✅ Lista |
| 2 | Movimientos (`FrmMovimientos`) | [02-FrmMovimientos.md](02-FrmMovimientos.md) | ✅ Lista |
| 3 | Pedidos (`FrmPedidos`) | [03-FrmPedidos.md](03-FrmPedidos.md) | ✅ Lista |
| 4 | Mapa de bodega (`FrmMapaBodega`) | [04-FrmMapaBodega.md](04-FrmMapaBodega.md) | ✅ Lista |
| 5 | Calculadora (`FrmCalculadora`) | [05-FrmCalculadora.md](05-FrmCalculadora.md) | ✅ Lista |
| 6 | Indicadores (`FrmIndicadores`) | — | Pendiente |
| 7 | Comparador de algoritmos (`FrmComparadorAlgoritmos`) | — | Pendiente |
| 8 | Clientes y login (`FrmClientes`, `FrmCliente`, `FrmLogin`) | — | Pendiente |

## Pequeño glosario

| Palabra | Qué significa aquí |
|---|---|
| **Formulario** | Una ventana del programa (una clase que hereda de `Form`). |
| **Control** | Cada elemento de la ventana: botón, caja de texto, tabla, lista desplegable. |
| **Evento** | Algo que hace el usuario (clic, escribir) y que hace que se ejecute un método. |
| **Servicio** | Clase de `Servicios/` que tiene la lógica: reglas, validaciones, búsquedas. |
| **Modelo** | Clase de `Modelos/` que solo guarda datos (como una ficha o una fila de Excel). |
| **En memoria** | Los datos que están en las listas mientras el programa está abierto. |
| **Persistir** | Guardar en el archivo JSON para que los datos no se pierdan al cerrar. |
| **Excepción** | Un error que se "lanza" (`throw`) y se "atrapa" (`catch`) en otro lugar. |
