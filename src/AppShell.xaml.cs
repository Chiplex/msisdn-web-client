using MSISDNWebClient.Views;
using MSISDNWebClient.Services;

namespace MSISDNWebClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("🔷 Inicializando AppShell...");
                InitializeComponent();
                System.Diagnostics.Debug.WriteLine("✅ InitializeComponent completado");

                // Registrar rutas de navegación
                Routing.RegisterRoute(Routes.Welcome, typeof(WelcomePage));
                Routing.RegisterRoute(Routes.Onboarding, typeof(OnboardingPage));
                Routing.RegisterRoute(Routes.Home, typeof(HomePage));
                Routing.RegisterRoute(Routes.Profile, typeof(ProfilePage));
                Routing.RegisterRoute(Routes.Explorer, typeof(ExplorerPage));
                Routing.RegisterRoute(Routes.PersonaDetail, typeof(PersonaDetailPage));
                
                System.Diagnostics.Debug.WriteLine("✅ Rutas registradas");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"💥 Error en AppShell: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack: {ex.StackTrace}");
                throw;
            }
        }
    }
}
