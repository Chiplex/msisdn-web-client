using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSISDNWebClient.Models;
using MSISDNWebClient.Services;

namespace MSISDNWebClient.ViewModels
{
    /// <summary>
    /// ViewModel para el perfil del usuario
    /// </summary>
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly PersonaService _personaService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private UserProfile? currentProfile;

        [ObservableProperty]
        private string displayName = string.Empty;

        [ObservableProperty]
        private string bio = string.Empty;

        [ObservableProperty]
        private string avatarUrl = string.Empty;

        [ObservableProperty]
        private string webPageContent = string.Empty;

        [ObservableProperty]
        private bool isEditMode;

        public ObservableCollection<SocialLink> Links { get; } = new();

        public ProfileViewModel(PersonaService personaService, NavigationService navigationService)
        {
            _personaService = personaService;
            _navigationService = navigationService;
            Title = "Mi Perfil";
        }

        /// <summary>
        /// Carga el perfil del usuario
        /// </summary>
        public async Task LoadProfileAsync()
        {
            IsBusy = true;

            try
            {
                CurrentProfile = await _personaService.GetCurrentProfileAsync();
                
                if (CurrentProfile != null)
                {
                    DisplayName = CurrentProfile.DisplayName;
                    Bio = CurrentProfile.Bio;
                    AvatarUrl = CurrentProfile.AvatarUrl;
                    WebPageContent = CurrentProfile.WebPageContent;

                    Links.Clear();
                    foreach (var link in CurrentProfile.Links.OrderBy(l => l.Order))
                    {
                        Links.Add(link);
                    }
                }
            }
            catch (System.Exception ex)
            {
                SetError($"Error al cargar perfil: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Activa el modo de edición
        /// </summary>
        [RelayCommand]
        private void EnableEditMode()
        {
            IsEditMode = true;
        }

        /// <summary>
        /// Guarda los cambios del perfil
        /// </summary>
        [RelayCommand]
        private async Task SaveProfileAsync()
        {
            if (CurrentProfile == null)
                return;

            IsBusy = true;
            ClearError();

            try
            {
                CurrentProfile.DisplayName = DisplayName;
                CurrentProfile.Bio = Bio;
                CurrentProfile.AvatarUrl = AvatarUrl;
                CurrentProfile.WebPageContent = WebPageContent;
                CurrentProfile.Links = Links.ToList();

                var response = await _personaService.UpdateProfileAsync(CurrentProfile);
                
                if (response.Success)
                {
                    IsEditMode = false;
                    await Application.Current!.MainPage!.DisplayAlert(
                        "Éxito",
                        "Perfil actualizado correctamente",
                        "OK");
                }
                else
                {
                    SetError(response.Message);
                }
            }
            catch (System.Exception ex)
            {
                SetError($"Error al guardar: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Cancela la edición
        /// </summary>
        [RelayCommand]
        private async Task CancelEditAsync()
        {
            IsEditMode = false;
            await LoadProfileAsync(); // Recargar datos originales
        }

        /// <summary>
        /// Añade un nuevo enlace social
        /// </summary>
        [RelayCommand]
        private async Task AddLinkAsync()
        {
            var title = await Application.Current!.MainPage!.DisplayPromptAsync(
                "Nuevo Enlace",
                "Título del enlace:");

            if (string.IsNullOrWhiteSpace(title))
                return;

            var url = await Application.Current.MainPage.DisplayPromptAsync(
                "Nuevo Enlace",
                "URL:");

            if (string.IsNullOrWhiteSpace(url))
                return;

            Links.Add(new SocialLink
            {
                Title = title,
                Url = url,
                Order = Links.Count + 1
            });
        }
    }
}
