# Contribuir a MSISDN Web Client

¡Gracias por tu interés en contribuir! Este documento proporciona pautas y información sobre cómo contribuir al proyecto.

## 🚀 Formas de Contribuir

- **Reportar bugs**: Usa los [Issues](../../issues) para reportar problemas
- **Sugerir mejoras**: Propón nuevas características o mejoras
- **Corregir bugs**: Envía pull requests con correcciones
- **Mejorar documentación**: Ayuda a mantener la documentación actualizada
- **Agregar tests**: Mejora la cobertura de testing

## 📋 Antes de Contribuir

1. **Busca primero**: Revisa si ya existe un issue o PR relacionado
2. **Abre un issue**: Para cambios grandes, discute primero la propuesta
3. **Fork y clone**: Crea tu fork del repositorio
4. **Branch**: Crea una rama descriptiva para tu trabajo

## 🔧 Configuración de Desarrollo

1. **Prerequisitos**:
   ```bash
   # .NET 9.0 SDK
   dotnet --version
   
   # MAUI workloads
   dotnet workload list
   ```

2. **Clone y setup**:
   ```bash
   git clone https://github.com/TU_USERNAME/msisdn-web-client.git
   cd msisdn-web-client
   dotnet restore
   dotnet build
   ```

3. **Ejecutar tests**:
   ```bash
   dotnet test
   ```

## 📝 Convenciones de Código

### Estilo de código
- Usar **PascalCase** para clases, métodos, propiedades
- Usar **camelCase** para variables locales y parámetros
- Usar **kebab-case** para nombres de archivos XAML
- Usar **regions** para organizar código en archivos grandes

### Naming conventions
```csharp
// Clases
public class UserService { }

// Interfaces (prefijo I)
public interface IApiService { }

// Métodos async (sufijo Async)
public async Task<User> GetUserAsync(int id) { }

// Eventos (sufijo Event/Handler)
public event EventHandler<UserEventArgs> UserChanged;
```

### Estructura de archivos
```
Views/
├── HomePage.xaml
├── HomePage.xaml.cs
ViewModels/
├── HomeViewModel.cs
Services/
├── IUserService.cs
├── UserService.cs
```

## 🏗️ Arquitectura

- **MVVM Pattern**: Separación clara entre View, ViewModel, Model
- **Dependency Injection**: Usar el contenedor de MAUI
- **Shell Navigation**: Para navegación entre páginas
- **CommunityToolkit.Mvvm**: Para implementación MVVM

## ✅ Pull Request Process

1. **Actualiza tu fork**:
   ```bash
   git remote add upstream https://github.com/ORIGINAL_OWNER/msisdn-web-client.git
   git fetch upstream
   git checkout main
   git merge upstream/main
   ```

2. **Crea una rama**:
   ```bash
   git checkout -b feature/amazing-feature
   # o
   git checkout -b fix/bug-description
   ```

3. **Haz tus cambios**:
   - Escribe código limpio y bien documentado
   - Agrega tests si es necesario
   - Actualiza documentación relevante

4. **Testing**:
   ```bash
   dotnet test
   dotnet build --no-restore
   ```

5. **Commit**:
   ```bash
   git add .
   git commit -m "feat: add amazing feature
   
   - Detailed description of changes
   - Why this change is needed
   - Any breaking changes"
   ```

6. **Push y PR**:
   ```bash
   git push origin feature/amazing-feature
   ```
   Luego abre un Pull Request en GitHub.

## 📋 Convención de Commits

Usamos [Conventional Commits](https://www.conventionalcommits.org/):

```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

### Tipos:
- `feat`: Nueva característica
- `fix`: Corrección de bug
- `docs`: Cambios en documentación
- `style`: Cambios de formato (no afectan lógica)
- `refactor`: Refactoring de código
- `test`: Agregar o corregir tests
- `chore`: Tareas de mantenimiento

### Ejemplos:
```bash
feat: add user authentication
fix: resolve navigation issue on Android
docs: update API documentation
style: format code according to guidelines
refactor: simplify user service implementation
test: add unit tests for UserViewModel
chore: update dependencies
```

## 🧪 Testing Guidelines

- **Unit tests**: Para ViewModels y Services
- **Integration tests**: Para flujos completos
- **UI tests**: Para funcionalidad crítica
- **Cobertura**: Mantener >80% de cobertura de código

```csharp
[Test]
public async Task GetUserAsync_ValidId_ReturnsUser()
{
    // Arrange
    var service = new UserService(mockApiService.Object);
    
    // Act
    var result = await service.GetUserAsync(1);
    
    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual(1, result.Id);
}
```

## 🚨 Reportar Issues

Al reportar un bug, incluye:

1. **Descripción clara** del problema
2. **Pasos para reproducir** el issue
3. **Comportamiento esperado** vs actual
4. **Environment**: OS, .NET version, device
5. **Screenshots** si es relevante
6. **Logs** si están disponibles

### Template de Bug Report:
```markdown
## Descripción
Breve descripción del problema

## Pasos para reproducir
1. Ir a...
2. Hacer clic en...
3. Observar error

## Comportamiento esperado
Descripción de lo que debería pasar

## Comportamiento actual
Descripción de lo que pasa actualmente

## Environment
- OS: Windows 11 / Android 13 / iOS 16
- .NET: 9.0
- MAUI: 9.0.0
- Device: Desktop / iPhone 14 / Samsung Galaxy S23

## Screenshots
Si es aplicable

## Logs adicionales
Incluir logs relevantes
```

## 📚 Recursos

- [.NET MAUI Documentation](https://docs.microsoft.com/en-us/dotnet/maui/)
- [MVVM Community Toolkit](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)
- [XAML Guidelines](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/xaml-syntax-in-detail)

## ❓ ¿Preguntas?

Si tienes preguntas:
1. Revisa la [documentación](docs/)
2. Busca en [Issues existentes](../../issues)
3. Abre un nuevo issue con la etiqueta `question`

¡Gracias por contribuir! 🎉