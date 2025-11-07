namespace MSISDNWebClient;

public partial class App : Application
{
    public App()
    {
        try
        {
            InitializeComponent();

            // Capturar excepciones no manejadas
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"💥 Error en App constructor: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack: {ex.StackTrace}");
            throw;
        }
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        try
        {
            var shell = new AppShell();
            
            var window = new Window(shell) { Title = "MSISDN Web Client" };
            return window;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"💥 Error creando ventana: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack: {ex.StackTrace}");
            throw;
        }
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var exception = e.ExceptionObject as Exception;
        System.Diagnostics.Debug.WriteLine($"💥 Excepción no manejada: {exception?.Message}");
        System.Diagnostics.Debug.WriteLine($"Stack: {exception?.StackTrace}");
        
        // Guardar en un archivo de log
        var logPath = Path.Combine(FileSystem.AppDataDirectory, "crash.log");
        File.AppendAllText(logPath, $"\n[{DateTime.Now}] {exception?.Message}\n{exception?.StackTrace}\n");
    }

    private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"💥 Excepción de tarea no observada: {e.Exception?.Message}");
        e.SetObserved();
    }
}

