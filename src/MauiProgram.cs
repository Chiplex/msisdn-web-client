using Microsoft.Extensions.Logging;
using MSISDNWebClient.Services;
using MSISDNWebClient.ViewModels;
using MSISDNWebClient.Views;
using CommunityToolkit.Maui;

namespace MSISDNWebClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Registrar HttpClient
            builder.Services.AddSingleton<HttpClient>();

            // Registrar Servicios (Singleton - única instancia)
            builder.Services.AddSingleton<CryptoService>();
            builder.Services.AddSingleton<StorageService>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<PersonaService>();
            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<SettingsService>();

            // Registrar ViewModels (Transient - nueva instancia cada vez)
            builder.Services.AddTransient<WelcomeViewModel>();
            builder.Services.AddTransient<OnboardingViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<ExplorerViewModel>();
            builder.Services.AddTransient<PersonaDetailViewModel>();

            // Registrar Views (Transient)
            builder.Services.AddTransient<WelcomePage>();
            builder.Services.AddTransient<OnboardingPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ExplorerPage>();
            builder.Services.AddTransient<PersonaDetailPage>();

            return builder.Build();
        }
    }
}
