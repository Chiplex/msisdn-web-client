namespace MSISDNWebClient.Models
{
    /// <summary>
    /// Representa un bloque/componente de la página HTML
    /// </summary>
    public class PageBlock
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Dictionary<string, string> Properties { get; set; } = new();
        public int Order { get; set; }
    }

    /// <summary>
    /// Bloque de plantilla disponible para agregar
    /// </summary>
    public class TemplateBlock
    {
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string HtmlTemplate { get; set; } = string.Empty;
    }

    /// <summary>
    /// Plantilla predefinida completa
    /// </summary>
    public class PredefinedTemplate
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public List<PageBlock> Blocks { get; set; } = new();
    }
}
