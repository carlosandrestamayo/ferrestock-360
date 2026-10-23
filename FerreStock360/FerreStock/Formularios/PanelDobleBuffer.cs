namespace FerreStock.Formularios
{
    /// <summary>
    /// Un Panel normal con "doble búfer" activado: primero dibuja en memoria y luego muestra
    /// la imagen completa. Así el mapa no parpadea al redimensionar la ventana.
    /// (La propiedad DoubleBuffered es protegida en Panel, por eso se necesita esta pequeña clase.)
    /// </summary>
    public class PanelDobleBuffer : Panel
    {
        public PanelDobleBuffer()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}
