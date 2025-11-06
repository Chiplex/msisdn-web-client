using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSISDNWebClient.Models;
using MSISDNWebClient.Services;
using System.Threading.Tasks;

namespace MSISDNWebClient.ViewModels
{
    /// <summary>
    /// ViewModel para mostrar el detalle de un perfil de persona
    /// </summary>
    [QueryProperty(nameof(PersonaId), "personaId")]
    public partial class PersonaDetailViewModel : BaseViewModel
    {
        private readonly PersonaService _personaService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private string personaId = string.Empty;

        [ObservableProperty]
        private UserProfile? persona;

        [ObservableProperty]
        private string displayName = string.Empty;

        [ObservableProperty]
        private string bio = string.Empty;

        [ObservableProperty]
        private string avatarUrl = string.Empty;

        public PersonaDetailViewModel(PersonaService personaService, NavigationService navigationService)
        {
            _personaService = personaService;
            _navigationService = navigationService;
            Title = "Perfil de Persona";
        }

        /// <summary>
        /// Se llama cuando cambia el PersonaId (navegación con parámetros)
        /// </summary>
        partial void OnPersonaIdChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _ = LoadPersonaAsync();
            }
        }

        /// <summary>
        /// Carga los datos de la persona
        /// </summary>
        private async Task LoadPersonaAsync()
        {
            IsBusy = true;
            ClearError();

            try
            {
                var response = await _personaService.GetPersonaAsync(PersonaId);
                
                if (response.Success && response.Data != null)
                {
                    Persona = response.Data;
                    DisplayName = response.Data.DisplayName;
                    Bio = response.Data.Bio;
                    AvatarUrl = response.Data.AvatarUrl;
                }
                else
                {
                    SetError(response.Message);
                }
            }
            catch (System.Exception ex)
            {
                SetError($"Error al cargar persona: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Vuelve atrás
        /// </summary>
        [RelayCommand]
        private async Task GoBackAsync()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
