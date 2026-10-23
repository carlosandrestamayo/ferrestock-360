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

            // Última red de seguridad: cada formulario maneja sus errores con try/catch, pero si alguno
            // se escapa, en lugar de cerrarse sin explicación el programa muestra un mensaje claro.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ErrorEnLaVentana;
            AppDomain.CurrentDomain.UnhandledException += ErrorEnOtroHilo;

            Application.Run(new FrmPrincipal());
        }

        // Errores que ocurren en el hilo de las ventanas (por ejemplo, dentro de un clic).
        // Después del mensaje el programa sigue funcionando.
        private static void ErrorEnLaVentana(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Ocurrió un error inesperado:\n\n" + e.Exception.Message +
                "\n\nSi se repite, anote qué estaba haciendo y avise al equipo.",
                "FerreStock 360", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Errores en otros hilos (por ejemplo, dentro de un Task.Run sin try/catch).
        // En este caso .NET cierra el programa, pero al menos se informa el motivo.
        private static void ErrorEnOtroHilo(object sender, UnhandledExceptionEventArgs e)
        {
            string mensaje = "Error desconocido";
            if (e.ExceptionObject is Exception ex)
            {
                mensaje = ex.Message;
            }
            MessageBox.Show("Ocurrió un error grave y el programa se cerrará:\n\n" + mensaje,
                "FerreStock 360", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
