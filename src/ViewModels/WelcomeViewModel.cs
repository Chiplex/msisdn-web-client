using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSISDNWebClient.Services;

namespace MSISDNWebClient.ViewModels
{
    /// <summary>
    /// ViewModel para la página de bienvenida
    /// </summary>
    public partial class WelcomeViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly NavigationService _navigationService;

        public WelcomeViewModel(AuthService authService, NavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
            Title = "Bienvenido a MSISDN-WEB";
        }

        /// <summary>
        /// Verifica si ya hay una sesión activa
        /// </summary>
        public async Task InitializeAsync()
        {
            var isAuthenticated = await _authService.IsAuthenticatedAsync();
            if (isAuthenticated)
            {
                // Ya hay sesión, ir directo al home
                await _navigationService.NavigateToRootAsync(Routes.Home);
            }
        }

        /// <summary>
        /// Comando para comenzar el onboarding
        /// </summary>
        [RelayCommand]
        private async Task StartOnboardingAsync()
        {
            await _navigationService.NavigateToAsync(Routes.Onboarding);
        }
    }
}
