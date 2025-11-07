using System;
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
    /// ViewModel para el perfil del usuario con motor de diseño MW-Tag
    /// MW-Tag = Motor de diseño de páginas HTML interactivo tipo WordPress
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
        private string avatarUrl = string.Empty;

        [ObservableProperty]
        private string pageTitle = string.Empty;

        [ObservableProperty]
        private string webPageContent = string.Empty;

        [ObservableProperty]
        private bool showPreview;

        // Control de pestañas
        [ObservableProperty]
        private bool isMyPageTab = true;

        [ObservableProperty]
        private bool isTemplatesTab;

        [ObservableProperty]
        private bool isSettingsTab;

        // Colecciones para el motor MW-Tag
        public ObservableCollection<TemplateBlock> AvailableTemplateBlocks { get; } = new();
        public ObservableCollection<PageBlock> PageBlocks { get; } = new();
        public ObservableCollection<PredefinedTemplate> PredefinedTemplates { get; } = new();

        public ProfileViewModel(PersonaService personaService, NavigationService navigationService)
        {
            _personaService = personaService;
            _navigationService = navigationService;
            Title = "Mi Perfil - MW-Tag";
            
            InitializeTemplateBlocks();
            InitializePredefinedTemplates();
        }

        /// <summary>
        /// Inicializa los bloques de plantilla disponibles en MW-Tag
        /// </summary>
        private void InitializeTemplateBlocks()
        {
            AvailableTemplateBlocks.Add(new TemplateBlock
            {
                Name = "Título",
                Icon = "📝",
                Type = "title",
                HtmlTemplate = "<h1>{content}</h1>"
            });

            AvailableTemplateBlocks.Add(new TemplateBlock
            {
                Name = "Texto",
                Icon = "📄",
                Type = "text",
                HtmlTemplate = "<p>{content}</p>"
            });

            AvailableTemplateBlocks.Add(new TemplateBlock
            {
                Name = "Carrusel",
                Icon = "🎠",
                Type = "carousel",
                HtmlTemplate = "<div class='carousel'>{content}</div>"
            });

            AvailableTemplateBlocks.Add(new TemplateBlock
            {
                Name = "Tabletas",
                Icon = "📋",
                Type = "table",
                HtmlTemplate = "<div class='table-grid'>{content}</div>"
            });

            AvailableTemplateBlocks.Add(new TemplateBlock
            {
                Name = "Zapatos",
                Icon = "👟",
                Type = "product-card",
                HtmlTemplate = "<div class='product-card'>{content}</div>"
            });

            AvailableTemplateBlocks.Add(new TemplateBlock
            {
                Name = "Camisetas",
                Icon = "👕",
                Type = "product-card",
                HtmlTemplate = "<div class='product-card'>{content}</div>"
            });

            AvailableTemplateBlocks.Add(new TemplateBlock
            {
                Name = "Pantalones",
                Icon = "👖",
                Type = "product-card",
                HtmlTemplate = "<div class='product-card'>{content}</div>"
            });
        }

        /// <summary>
        /// Inicializa plantillas predefinidas completas
        /// </summary>
        private void InitializePredefinedTemplates()
        {
            PredefinedTemplates.Add(new PredefinedTemplate
            {
                Name = "Portafolio Profesional",
                Description = "Muestra tus proyectos y habilidades",
                Icon = "💼"
            });

            PredefinedTemplates.Add(new PredefinedTemplate
            {
                Name = "Tienda Online",
                Description = "Vende productos con estilo",
                Icon = "🛒"
            });

            PredefinedTemplates.Add(new PredefinedTemplate
            {
                Name = "CV/Resume",
                Description = "Curriculum vitae profesional",
                Icon = "📝"
            });

            PredefinedTemplates.Add(new PredefinedTemplate
            {
                Name = "Landing Page",
                Description = "Página de aterrizaje moderna",
                Icon = "🚀"
            });
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
                    AvatarUrl = CurrentProfile.AvatarUrl;
                    WebPageContent = CurrentProfile.WebPageContent;
                    PageTitle = string.IsNullOrEmpty(CurrentProfile.DisplayName) 
                        ? "Mi Página" 
                        : CurrentProfile.DisplayName;
                }
            }
            catch (Exception ex)
            {
                SetError($"Error al cargar perfil: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // =============== COMANDOS DE NAVEGACIÓN DE PESTAÑAS ===============

        [RelayCommand]
        private void SelectMyPageTab()
        {
            IsMyPageTab = true;
            IsTemplatesTab = false;
            IsSettingsTab = false;
        }

        [RelayCommand]
        private void SelectTemplatesTab()
        {
            IsMyPageTab = false;
            IsTemplatesTab = true;
            IsSettingsTab = false;
        }

        [RelayCommand]
        private void SelectSettingsTab()
        {
            IsMyPageTab = false;
            IsTemplatesTab = false;
            IsSettingsTab = true;
        }

        // =============== COMANDOS DEL MOTOR MW-Tag ===============

        /// <summary>
        /// Añade un bloque de plantilla a la página (MW-Tag)
        /// </summary>
        [RelayCommand]
        private void AddTemplateBlock(TemplateBlock template)
        {
            var newBlock = new PageBlock
            {
                Type = template.Type,
                Title = template.Name,
                Content = "Nuevo contenido",
                Order = PageBlocks.Count
            };
            
            PageBlocks.Add(newBlock);
            GenerateHtmlFromBlocks();
        }

        /// <summary>
        /// Muestra opciones de un bloque (editar, eliminar, mover)
        /// </summary>
        [RelayCommand]
        private async Task ShowBlockOptionsAsync(PageBlock block)
        {
            if (Application.Current?.Windows.Count == 0) return;
            var page = Application.Current!.Windows[0].Page;
            if (page == null) return;

            var action = await page.DisplayActionSheet(
                "Opciones",
                "Cancelar",
                "Eliminar",
                "Editar",
                "Datos",
                "Incluir dentro",
                "Añadir arriba",
                "Añadir abajo");

            switch (action)
            {
                case "Editar":
                    await EditBlockAsync(block);
                    break;
                case "Eliminar":
                    PageBlocks.Remove(block);
                    GenerateHtmlFromBlocks();
                    break;
                case "Datos":
                    await ShowBlockDataAsync(block);
                    break;
                case "Añadir arriba":
                    // TODO: Implementar
                    break;
                case "Añadir abajo":
                    // TODO: Implementar
                    break;
                case "Incluir dentro":
                    // TODO: Implementar
                    break;
            }
        }

        /// <summary>
        /// Edita un bloque existente
        /// </summary>
        private async Task EditBlockAsync(PageBlock block)
        {
            if (Application.Current?.Windows.Count == 0) return;
            var page = Application.Current!.Windows[0].Page;
            if (page == null) return;

            var title = await page.DisplayPromptAsync(
                "Editar Título",
                "Nuevo título:",
                initialValue: block.Title);

            if (!string.IsNullOrWhiteSpace(title))
            {
                block.Title = title;
            }

            var content = await page.DisplayPromptAsync(
                "Editar Contenido",
                "Nuevo contenido:",
                initialValue: block.Content);

            if (!string.IsNullOrWhiteSpace(content))
            {
                block.Content = content;
                GenerateHtmlFromBlocks();
            }
        }

        /// <summary>
        /// Muestra los datos/propiedades de un bloque
        /// </summary>
        private async Task ShowBlockDataAsync(PageBlock block)
        {
            if (Application.Current?.Windows.Count == 0) return;
            var page = Application.Current!.Windows[0].Page;
            if (page == null) return;

            await page.DisplayAlert(
                "Datos del Bloque",
                $"Tipo: {block.Type}\nTítulo: {block.Title}\nContenido: {block.Content}",
                "OK");
        }

        /// <summary>
        /// Carga una plantilla predefinida completa
        /// </summary>
        [RelayCommand]
        private async Task LoadTemplateAsync(PredefinedTemplate template)
        {
            if (Application.Current?.Windows.Count == 0) return;
            var page = Application.Current!.Windows[0].Page;
            if (page == null) return;

            var confirm = await page.DisplayAlert(
                "Cargar Plantilla",
                $"¿Cargar '{template.Name}'? Esto reemplazará tu diseño actual.",
                "Sí",
                "No");

            if (confirm)
            {
                PageBlocks.Clear();
                foreach (var block in template.Blocks)
                {
                    PageBlocks.Add(block);
                }
                GenerateHtmlFromBlocks();
                
                // Cambiar a la pestaña Mi Página
                SelectMyPageTab();
            }
        }

        /// <summary>
        /// Genera el HTML final a partir de los bloques (Motor MW-Tag)
        /// </summary>
        private void GenerateHtmlFromBlocks()
        {
            if (PageBlocks.Count == 0)
            {
                WebPageContent = string.Empty;
                return;
            }

            var html = @"
                <html>
                <head>
                    <meta name='viewport' content='width=device-width, initial-scale=1'>
                    <meta name='generator' content='MW-Tag - MSISDN Web Tag Engine'>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            padding: 20px;
                            margin: 0;
                            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                            color: white;
                        }
                        .mw-block {
                            background: rgba(255,255,255,0.1);
                            backdrop-filter: blur(10px);
                            border-radius: 15px;
                            padding: 20px;
                            margin: 10px 0;
                        }
                        h1 { margin-top: 0; }
                        .product-card {
                            background: rgba(255,255,255,0.2);
                            padding: 15px;
                            border-radius: 10px;
                            margin: 10px 0;
                        }
                        .carousel {
                            display: flex;
                            overflow-x: auto;
                            gap: 10px;
                        }
                        .table-grid {
                            display: grid;
                            grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
                            gap: 15px;
                        }
                        .mw-tag-footer {
                            text-align: center;
                            font-size: 10px;
                            opacity: 0.5;
                            margin-top: 30px;
                        }
                    </style>
                </head>
                <body>";

            html += $"<h1>{PageTitle}</h1>";

            foreach (var block in PageBlocks.OrderBy(b => b.Order))
            {
                html += $"<div class='mw-block' data-type='{block.Type}'>";
                html += $"<h3>{block.Title}</h3>";
                html += $"<p>{block.Content}</p>";
                html += $"</div>";
            }

            html += "<div class='mw-tag-footer'>Creado con MW-Tag Engine</div>";
            html += "</body></html>";

            WebPageContent = html;
        }

        /// <summary>
        /// Muestra información sobre la capacidad de la página
        /// </summary>
        [RelayCommand]
        private async Task ShowCapacityInfoAsync()
        {
            if (Application.Current?.Windows.Count == 0) return;
            var page = Application.Current!.Windows[0].Page;
            if (page == null) return;

            var size = WebPageContent.Length / 1024.0;
            var percentage = (size / 100.0) * 100;
            
            await page.DisplayAlert(
                "Capacidad de la Página",
                $"Tamaño actual: {size:F2} KB\n" +
                $"Bloques MW-Tag: {PageBlocks.Count}\n" +
                $"Uso: {percentage:F1}%\n" +
                $"Límite máximo: 100 KB",
                "OK");
        }

        // =============== COMANDOS DE GUARDADO ===============

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
                CurrentProfile.AvatarUrl = AvatarUrl;
                CurrentProfile.WebPageContent = WebPageContent;

                var response = await _personaService.UpdateProfileAsync(CurrentProfile);
                
                if (response.Success)
                {
                    if (Application.Current?.Windows.Count > 0)
                    {
                        await Application.Current.Windows[0].Page!.DisplayAlert(
                            "✅ Guardado",
                            "Tu página MW-Tag se ha guardado correctamente",
                            "OK");
                    }
                }
                else
                {
                    SetError(response.Message);
                }
            }
            catch (Exception ex)
            {
                SetError($"Error al guardar: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // =============== COMANDOS DE NAVEGACIÓN ===============

        [RelayCommand]
        private async Task GoToHomeAsync()
        {
            await _navigationService.NavigateToRootAsync(Routes.Home);
        }

        [RelayCommand]
        private async Task GoToNotificationsAsync()
        {
            await _navigationService.NavigateToAsync(Routes.Explorer);
        }
    }
}