namespace MSISDNWebClient.Models
{
    /// <summary>
    /// Elemento de visualización de perfil para el feed tipo TikTok
    /// </summary>
    public class ProfileDisplayItem
    {
        public string DisplayName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string PersonaIdShort { get; set; } = string.Empty;
        public string HtmlContent { get; set; } = string.Empty;
    }
}
