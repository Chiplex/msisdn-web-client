# Motor de Plantillas HTML - Módulo Futuro

## Descripción General
El Motor de Plantillas será un módulo independiente que permitirá a los usuarios crear y personalizar sus páginas web de forma visual e interactiva, similar a WordPress.

## Características Planificadas

### 1. Editor Visual WYSIWYG
- Arrastrar y soltar componentes
- Edición en tiempo real
- Vista previa instantánea
- Diseño responsive automático

### 2. Biblioteca de Componentes
- **Componentes Básicos:**
  - Textos (h1, h2, p, etc.)
  - Imágenes
  - Botones
  - Contenedores/Secciones
  - Separadores

- **Componentes Avanzados:**
  - Galerías de imágenes
  - Carruseles
  - Tablas de contenido
  - Estadísticas/Contadores
  - Formularios de contacto
  - Botones de redes sociales

### 3. Sistema de Plantillas Predefinidas
- Portafolio profesional
- Tarjeta de presentación
- CV/Resume
- Landing page
- Blog personal
- Tienda/Catálogo

### 4. Personalización
- **Colores y Tipografía:**
  - Paletas de colores predefinidas
  - Selector de fuentes Google Fonts
  - Temas (claro/oscuro/neón/minimal)

- **Estilos CSS:**
  - Editor CSS inline
  - Clases CSS personalizadas
  - Animaciones y transiciones

### 5. Gestión de Contenido
- Variables dinámicas (nombre, avatar, stats)
- Contenido multilenguaje
- SEO básico (meta tags)
- Open Graph para compartir

## Integración con el Sistema

### Almacenamiento
```csharp
public class TemplateModule
{
    public string TemplateId { get; set; }
    public string TemplateName { get; set; }
    public string HtmlContent { get; set; }
    public string CssContent { get; set; }
    public Dictionary<string, string> Variables { get; set; }
    public List<Component> Components { get; set; }
}
```

### Renderizado
El contenido HTML generado se guardará en `UserProfile.WebPageContent` y se mostrará en:
- HomePage (feed tipo TikTok con scroll horizontal)
- Vista de perfil público
- Compartir en redes sociales

## Flujo de Usuario

1. Usuario accede a "Perfil" → "Abrir Editor de Plantillas"
2. Selecciona una plantilla base o comienza desde cero
3. Arrastra componentes al canvas
4. Personaliza textos, colores y estilos
5. Vista previa en tiempo real
6. Guarda y publica

## Arquitectura Técnica

### Componentes a Crear
- `TemplateEditorPage.xaml` - Página principal del editor
- `TemplateEditorViewModel.cs` - Lógica del editor
- `TemplateService.cs` - Servicio de gestión de plantillas
- `ComponentLibrary/` - Carpeta con componentes HTML
- `TemplateRenderer.cs` - Motor de renderizado HTML

### Dependencias
- WebView para vista previa
- Sistema de drag & drop (MAUI gestures)
- Parser HTML/CSS
- Sistema de variables y plantillas

## Prioridad
🔴 Alta - Funcionalidad core del sistema tipo TikTok

## Notas de Implementación
- Mantener plantillas ligeras (< 100KB)
- Sanitizar HTML para seguridad
- Caching de plantillas renderizadas
- Exportación como HTML standalone
- Backup automático de cambios
