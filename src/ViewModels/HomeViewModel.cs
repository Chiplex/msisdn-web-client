using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSISDNWebClient.Models;
using MSISDNWebClient.Services;

namespace MSISDNWebClient.ViewModels
{
    /// <summary>
    /// ViewModel para la página principal (Home) con vista tipo TikTok
    /// </summary>
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly PersonaService _personaService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private bool isPublicTab = true;

        [ObservableProperty]
        private object? currentProfile;

        public ObservableCollection<ProfileDisplayItem> DisplayedProfiles { get; } = new();

        public HomeViewModel(
            AuthService authService,
            PersonaService personaService,
            NavigationService navigationService)
        {
            _authService = authService;
            _personaService = personaService;
            _navigationService = navigationService;
            Title = "Inicio";
        }

        /// <summary>
        /// Carga los datos del usuario y perfiles
        /// </summary>
        public async Task LoadDataAsync()
        {
            IsBusy = true;

            try
            {
                await LoadProfilesAsync();
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
        /// Carga perfiles según la pestaña activa
        /// </summary>
        private async Task LoadProfilesAsync()
        {
            DisplayedProfiles.Clear();

            if (IsPublicTab)
            {
                // Cargar perfiles públicos (mock)
                var publicProfiles = await _personaService.GetPublicProfilesAsync();
                foreach (var profile in publicProfiles)
                {
                    DisplayedProfiles.Add(new ProfileDisplayItem
                    {
                        DisplayName = profile.DisplayName,
                        AvatarUrl = profile.AvatarUrl,
                        PersonaIdShort = profile.PersonaId.Substring(0, 16) + "...",
                        HtmlContent = GenerateHtmlContent(profile)
                    });
                }
            }
            else
            {
                // Cargar contactos guardados (mock)
                var contacts = await _personaService.GetContactsAsync();
                foreach (var contact in contacts)
                {
                    DisplayedProfiles.Add(new ProfileDisplayItem
                    {
                        DisplayName = contact.DisplayName,
                        AvatarUrl = contact.AvatarUrl,
                        PersonaIdShort = contact.PersonaId.Substring(0, 16) + "...",
                        HtmlContent = GenerateHtmlContent(contact)
                    });
                }
            }
        }

        /// <summary>
        /// Genera contenido HTML a partir del perfil y su plantilla
        /// </summary>
        private string GenerateHtmlContent(UserProfile profile)
        {
            var html = profile.WebPageContent;
            
            // Si no hay contenido personalizado, usar plantilla por defecto
            if (string.IsNullOrWhiteSpace(html))
            {
                html = $@"
                    <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1'>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                color: white;
                                padding: 20px;
                                margin: 0;
                            }}
                            .card {{
                                background: rgba(255,255,255,0.1);
                                backdrop-filter: blur(10px);
                                border-radius: 15px;
                                padding: 20px;
                                margin: 10px 0;
                            }}
                            h2 {{ margin-top: 0; }}
                            .stat {{ 
                                display: inline-block;
                                margin: 10px 20px 10px 0;
                            }}
                            .stat-number {{
                                font-size: 24px;
                                font-weight: bold;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='card'>
                            <h2>Sobre mí</h2>
                            <p>Esta es mi página personalizada en MSISDN-WEB</p>
                        </div>
                        <div class='card'>
                            <h2>Estadísticas</h2>
                            <div class='stat'>
                                <div class='stat-number'>1200</div>
                                <div>Proyectos</div>
                            </div>
                            <div class='stat'>
                                <div class='stat-number'>1200</div>
                                <div>Proyectos</div>
                            </div>
                        </div>
                    </body>
                    </html>
                ";
            }

            return html;
        }

        /// <summary>
        /// Selecciona la pestaña Público
        /// </summary>
        [RelayCommand]
        private async Task SelectPublicTabAsync()
        {
            if (!IsPublicTab)
            {
                IsPublicTab = true;
                await LoadProfilesAsync();
            }
        }

        /// <summary>
        /// Selecciona la pestaña Mis Contactos
        /// </summary>
        [RelayCommand]
        private async Task SelectContactsTabAsync()
        {
            if (IsPublicTab)
            {
                IsPublicTab = false;
                await LoadProfilesAsync();
            }
        }

        /// <summary>
        /// Ir al inicio (recarga)
        /// </summary>
        [RelayCommand]
        private async Task GoToHomeAsync()
        {
            await LoadDataAsync();
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
        /// Navega al explorador de personas (notificaciones en este contexto)
        /// </summary>
        [RelayCommand]
        private async Task GoToExplorerAsync()
        {
            await _navigationService.NavigateToAsync(Routes.Explorer);
        }
    }
}
