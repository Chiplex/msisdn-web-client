using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSISDNWebClient.Models;
using MSISDNWebClient.Services;

namespace MSISDNWebClient.ViewModels
{
    /// <summary>
    /// ViewModel para la página principal (Home)
    /// </summary>
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly PersonaService _personaService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private PersonaID? currentIdentity;

        [ObservableProperty]
        private string personaIdDisplay = string.Empty;

        [ObservableProperty]
        private bool isVerified;

        [ObservableProperty]
        private string displayName = string.Empty;

        public HomeViewModel(
            AuthService authService,
            PersonaService personaService,
            NavigationService navigationService)
        {
            _authService = authService;
            _personaService = personaService;
            _navigationService = navigationService;
            Title = "Mi Identidad MSISDN";
        }

        /// <summary>
        /// Carga los datos del usuario
        /// </summary>
        public async Task LoadDataAsync()
        {
            IsBusy = true;

            try
            {
                CurrentIdentity = await _authService.GetCurrentIdentityAsync();
                
                if (CurrentIdentity != null)
                {
                    PersonaIdDisplay = CurrentIdentity.GetShortId();
                    IsVerified = CurrentIdentity.IsVerified;
                }

                var profile = await _personaService.GetCurrentProfileAsync();
                if (profile != null)
                {
                    DisplayName = profile.DisplayName;
                }
            }
            catch (System.Exception ex)
            {
                SetError($"Error al cargar datos: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Navega al perfil del usuario
        /// </summary>
        [RelayCommand]
        private async Task GoToProfileAsync()
        {
            await _navigationService.NavigateToAsync(Routes.Profile);
        }

        /// <summary>
        /// Navega al explorador de personas
        /// </summary>
        [RelayCommand]
        private async Task GoToExplorerAsync()
        {
            await _navigationService.NavigateToAsync(Routes.Explorer);
        }

        /// <summary>
        /// Copia el PersonaID al portapapeles
        /// </summary>
        [RelayCommand]
        private async Task CopyPersonaIdAsync()
        {
            if (CurrentIdentity != null)
            {
                await Clipboard.SetTextAsync(CurrentIdentity.Id);
                // En producción, mostrar un toast o mensaje
                await Application.Current!.MainPage!.DisplayAlert(
                    "Copiado",
                    "PersonaID copiado al portapapeles",
                    "OK");
            }
        }

        /// <summary>
        /// Cierra la sesión
        /// </summary>
        [RelayCommand]
        private async Task LogoutAsync()
        {
            var confirm = await Application.Current!.MainPage!.DisplayAlert(
                "Cerrar Sesión",
                "¿Estás seguro? Se eliminarán todos los datos locales.",
                "Sí",
                "No");

            if (confirm)
            {
                await _authService.LogoutAsync();
                await _navigationService.NavigateToRootAsync(Routes.Welcome);
            }
        }
    }
}
