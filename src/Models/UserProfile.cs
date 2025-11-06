using System;
using System.Collections.Generic;

namespace MSISDNWebClient.Models
{
    /// <summary>
    /// Perfil de usuario completo para MSISDN-WEB
    /// Incluye información pública descentralizada
    /// </summary>
    public class UserProfile
    {
        public string PersonaId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string WebPageContent { get; set; } = string.Empty;
        public List<SocialLink> Links { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsPublic { get; set; } = true;
        public ProfileTheme Theme { get; set; } = ProfileTheme.Dark;

        // Campos privados (no se comparten públicamente)
        public string MSISDN { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserProfile() { }

        public UserProfile(string personaId, string msisdn)
        {
            PersonaId = personaId;
            MSISDN = msisdn;
            DisplayName = $"Usuario {personaId[..8]}";
        }

        /// <summary>
        /// Obtiene una versión pública del perfil (sin datos sensibles)
        /// </summary>
        public UserProfile GetPublicProfile()
        {
            return new UserProfile
            {
                PersonaId = PersonaId,
                DisplayName = DisplayName,
                Bio = Bio,
                AvatarUrl = AvatarUrl,
                WebPageContent = WebPageContent,
                Links = Links,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                IsPublic = IsPublic,
                Theme = Theme,
                // No incluir MSISDN ni Email
            };
        }
    }

    /// <summary>
    /// Representa un enlace social o externo del perfil
    /// </summary>
    public class SocialLink
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Order { get; set; }
    }

    /// <summary>
    /// Temas disponibles para el perfil
    /// </summary>
    public enum ProfileTheme
    {
        Dark,
        Light,
        Neon,
        Minimal
    }
}
