using FerreStock.Formularios;

namespace FerreStock
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada de la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configura estilos visuales, DPI y fuente por defecto de Windows Forms.
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmPrincipal());
        }
    }
}
