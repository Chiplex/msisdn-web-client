using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MSISDNWebClient.Models;

namespace MSISDNWebClient.Services
{
    /// <summary>
    /// Servicio para gestionar perfiles de Persona (mock API)
    /// </summary>
    public class PersonaService
    {
        private readonly HttpClient _httpClient;
        private readonly StorageService _storageService;
        
        // Base de datos mock en memoria
        private static readonly Dictionary<string, UserProfile> _mockDatabase = new();

        public PersonaService(StorageService storageService)
        {
            _storageService = storageService;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiEndpoints.BaseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };

            // Inicializar con algunos perfiles de ejemplo
            InitializeMockData();
        }

        /// <summary>
        /// Obtiene un perfil de Persona por su ID
        /// </summary>
        public Task<ApiResponse<UserProfile>> GetPersonaAsync(string personaId)
        {
            return Task.Run(() =>
            {
                try
                {
                    // Simular delay de red
                    Task.Delay(500).Wait();

                    if (_mockDatabase.TryGetValue(personaId, out var profile))
                    {
                        return ApiResponse<UserProfile>.Ok(
                            profile.GetPublicProfile(),
                            "Perfil encontrado");
                    }

                    return ApiResponse<UserProfile>.Error(
                        "Perfil no encontrado",
                        "PERSONA_NOT_FOUND");
                }
                catch (Exception ex)
                {
                    return ApiResponse<UserProfile>.Error($"Error al obtener perfil: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Actualiza el perfil del usuario actual
        /// </summary>
        public Task<ApiResponse<UserProfile>> UpdateProfileAsync(UserProfile profile)
        {
            return Task.Run(async () =>
            {
                try
                {
                    // Simular delay de red
                    await Task.Delay(300);

                    profile.UpdatedAt = DateTime.UtcNow;

                    // Actualizar en "base de datos" mock
                    _mockDatabase[profile.PersonaId] = profile;

                    // Guardar localmente
                    await _storageService.SaveProfileAsync(profile);

                    return ApiResponse<UserProfile>.Ok(
                        profile.GetPublicProfile(),
                        "Perfil actualizado exitosamente");
                }
                catch (Exception ex)
                {
                    return ApiResponse<UserProfile>.Error($"Error al actualizar perfil: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Obtiene el perfil del usuario actual
        /// </summary>
        public async Task<UserProfile?> GetCurrentProfileAsync()
        {
            return await _storageService.GetProfileAsync();
        }

        /// <summary>
        /// Busca perfiles por nombre o ID parcial
        /// </summary>
        public Task<ApiResponse<List<UserProfile>>> SearchPersonasAsync(string query)
        {
            return Task.Run(() =>
            {
                try
                {
                    // Simular delay de red
                    Task.Delay(400).Wait();

                    var results = new List<UserProfile>();
                    var lowerQuery = query.ToLower();

                    foreach (var profile in _mockDatabase.Values)
                    {
                        if (!profile.IsPublic)
                            continue;

                        if (profile.DisplayName.ToLower().Contains(lowerQuery) ||
                            profile.PersonaId.ToLower().Contains(lowerQuery) ||
                            profile.Bio.ToLower().Contains(lowerQuery))
                        {
                            results.Add(profile.GetPublicProfile());
                        }
                    }

                    return ApiResponse<List<UserProfile>>.Ok(
                        results,
                        $"Se encontraron {results.Count} perfiles");
                }
                catch (Exception ex)
                {
                    return ApiResponse<List<UserProfile>>.Error($"Error en búsqueda: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Obtiene perfiles destacados para explorar
        /// </summary>
        public Task<ApiResponse<List<UserProfile>>> GetFeaturedPersonasAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    var featured = new List<UserProfile>();
                    
                    foreach (var profile in _mockDatabase.Values)
                    {
                        if (profile.IsPublic)
                            featured.Add(profile.GetPublicProfile());
                    }

                    return ApiResponse<List<UserProfile>>.Ok(
                        featured,
                        $"{featured.Count} perfiles disponibles");
                }
                catch (Exception ex)
                {
                    return ApiResponse<List<UserProfile>>.Error($"Error: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Obtiene perfiles públicos para el feed
        /// </summary>
        public Task<List<UserProfile>> GetPublicProfilesAsync()
        {
            return Task.Run(() =>
            {
                var publicProfiles = new List<UserProfile>();
                foreach (var profile in _mockDatabase.Values)
                {
                    if (profile.IsPublic)
                        publicProfiles.Add(profile.GetPublicProfile());
                }
                return publicProfiles;
            });
        }

        /// <summary>
        /// Obtiene la lista de contactos del usuario
        /// </summary>
        public Task<List<UserProfile>> GetContactsAsync()
        {
            return Task.Run(() =>
            {
                // Mock: devolver algunos perfiles de ejemplo como contactos
                var contacts = new List<UserProfile>();
                var count = 0;
                foreach (var profile in _mockDatabase.Values)
                {
                    if (count >= 3) break; // Limitar a 3 contactos de ejemplo
                    contacts.Add(profile.GetPublicProfile());
                    count++;
                }
                return contacts;
            });
        }

        /// <summary>
        /// Inicializa datos mock de ejemplo
        /// </summary>
        private void InitializeMockData()
        {
            var exampleProfiles = new[]
            {
                new UserProfile
                {
                    PersonaId = "a1b2c3d4e5f6789012345678901234567890123456789012345678901234567",
                    DisplayName = "Alice Web3",
                    Bio = "Entusiasta de la descentralización y la privacidad digital",
                    AvatarUrl = "https://i.pravatar.cc/150?u=alice",
                    Links = new List<SocialLink>
                    {
                        new() { Title = "GitHub", Url = "https://github.com/alice", Icon = "github", Order = 1 },
                        new() { Title = "Blog", Url = "https://alice.blog", Icon = "web", Order = 2 }
                    },
                    Theme = ProfileTheme.Neon,
                    CreatedAt = DateTime.UtcNow.AddDays(-30)
                },
                new UserProfile
                {
                    PersonaId = "b2c3d4e5f6789012345678901234567890123456789012345678901234567890",
                    DisplayName = "Bob Developer",
                    Bio = "Full-stack developer | Blockchain enthusiast | Coffee lover ☕",
                    AvatarUrl = "https://i.pravatar.cc/150?u=bob",
                    Links = new List<SocialLink>
                    {
                        new() { Title = "Portfolio", Url = "https://bob.dev", Icon = "code", Order = 1 }
                    },
                    Theme = ProfileTheme.Dark,
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new UserProfile
                {
                    PersonaId = "c3d4e5f6789012345678901234567890123456789012345678901234567890ab",
                    DisplayName = "Carol Designer",
                    Bio = "UI/UX Designer | Creating beautiful digital experiences",
                    AvatarUrl = "https://i.pravatar.cc/150?u=carol",
                    Links = new List<SocialLink>
                    {
                        new() { Title = "Dribbble", Url = "https://dribbble.com/carol", Icon = "design", Order = 1 },
                        new() { Title = "Instagram", Url = "https://instagram.com/carol", Icon = "camera", Order = 2 }
                    },
                    Theme = ProfileTheme.Light,
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                }
            };

            foreach (var profile in exampleProfiles)
            {
                _mockDatabase[profile.PersonaId] = profile;
            }
        }
    }
}
