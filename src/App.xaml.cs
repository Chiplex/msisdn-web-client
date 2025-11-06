namespace MSISDNWebClient;

public partial class App : Application
{
    public App()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("🔷 Inicializando App...");
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("✅ App InitializeComponent completado");

            // Capturar excepciones no manejadas
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            System.Diagnostics.Debug.WriteLine("✅ Exception handlers registrados");
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
            System.Diagnostics.Debug.WriteLine("🔷 Creando ventana...");
            var shell = new AppShell();
            System.Diagnostics.Debug.WriteLine("✅ AppShell creado");
            
            var window = new Window(shell)
            {
                Title = "MSISDN Web Client"
            };
            System.Diagnostics.Debug.WriteLine("✅ Ventana creada");
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

