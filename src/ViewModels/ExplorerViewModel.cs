using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSISDNWebClient.Models;
using MSISDNWebClient.Services;

namespace MSISDNWebClient.ViewModels
{
    /// <summary>
    /// ViewModel para explorar perfiles de personas
    /// </summary>
    public partial class ExplorerViewModel : BaseViewModel
    {
        private readonly PersonaService _personaService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private string searchQuery = string.Empty;

        [ObservableProperty]
        private bool isSearching;

        public ObservableCollection<UserProfile> Personas { get; } = new();

        public ExplorerViewModel(PersonaService personaService, NavigationService navigationService)
        {
            _personaService = personaService;
            _navigationService = navigationService;
            Title = "Explorar Personas";
        }

        /// <summary>
        /// Carga perfiles destacados
        /// </summary>
        public async Task LoadFeaturedAsync()
        {
            IsBusy = true;

            try
            {
                var response = await _personaService.GetFeaturedPersonasAsync();
                
                if (response.Success && response.Data != null)
                {
                    Personas.Clear();
                    foreach (var persona in response.Data)
                    {
                        Personas.Add(persona);
                    }
                }
                else
                {
                    SetError(response.Message);
                }
            }
            catch (System.Exception ex)
            {
                SetError($"Error al cargar perfiles: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Busca perfiles por query
        /// </summary>
        [RelayCommand]
        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                await LoadFeaturedAsync();
                return;
            }

            IsSearching = true;
            ClearError();

            try
            {
                var response = await _personaService.SearchPersonasAsync(SearchQuery);
                
                if (response.Success && response.Data != null)
                {
                    Personas.Clear();
                    foreach (var persona in response.Data)
                    {
                        Personas.Add(persona);
                    }

                    if (response.Data.Count == 0)
                    {
                        SetError("No se encontraron resultados");
                    }
                }
                else
                {
                    SetError(response.Message);
                }
            }
            catch (System.Exception ex)
            {
                SetError($"Error en búsqueda: {ex.Message}");
            }
            finally
            {
                IsSearching = false;
            }
        }

        /// <summary>
        /// Navega al detalle de un perfil
        /// </summary>
        [RelayCommand]
        private async Task ViewPersonaAsync(UserProfile persona)
        {
            if (persona == null)
                return;

            await _navigationService.NavigateToAsync(
                $"{Routes.PersonaDetail}?personaId={persona.PersonaId}");
        }
    }
}
