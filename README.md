# MSISDN Web Client

Una aplicación multiplataforma desarrollada con .NET MAUI para la gestión de identidades digitales y servicios MSISDN.

## 🚀 Características

- **Multiplataforma**: Compatible con Android, iOS, macOS y Windows
- **Arquitectura MVVM**: Implementada con CommunityToolkit.Mvvm
- **Navegación Shell**: Navegación moderna y fluida
- **Inyección de dependencias**: Configuración completa de servicios
- **APIs REST**: Integración con servicios web
- **Almacenamiento local**: Manejo de datos offline
- **Autenticación**: Sistema de login y gestión de sesiones

## 🏗️ Arquitectura

```
src/
├── Models/          # Modelos de datos
├── ViewModels/      # ViewModels con MVVM
├── Views/           # Páginas XAML
├── Services/        # Servicios de negocio
├── Helpers/         # Utilidades y validadores
├── Controls/        # Controles personalizados
├── Converters/      # Convertidores de datos
├── Resources/       # Recursos (imágenes, estilos, etc.)
└── Platforms/       # Código específico por plataforma
```

## 🛠️ Tecnologías

- **.NET 9.0** - Framework principal
- **.NET MAUI** - UI multiplataforma
- **CommunityToolkit.Maui** - Controles y helpers adicionales
- **CommunityToolkit.Mvvm** - Implementación MVVM
- **Microsoft.Extensions.Http** - Cliente HTTP
- **SQLite** - Base de datos local

## 📋 Requisitos

### Desarrollo
- Visual Studio 2022 17.8+ o Visual Studio Code
- .NET 9.0 SDK
- Workloads de .NET MAUI instalados

### Windows
- Windows 11 versión 22H2 o superior
- Windows App SDK Runtime

### Android
- Android 7.0 (API 24) o superior

### iOS
- iOS 11.0 o superior
- Xcode 14+ (para desarrollo en macOS)

### macOS
- macOS 11.0 o superior

## 🚀 Instalación y Ejecución

1. **Clonar el repositorio:**
   ```bash
   git clone [URL_DEL_REPOSITORIO]
   cd msisdn-web-client
   ```

2. **Restaurar dependencias:**
   ```bash
   dotnet restore
   ```

3. **Compilar el proyecto:**
   ```bash
   dotnet build
   ```

4. **Ejecutar en Windows:**
   ```bash
   dotnet run --project src/MSISDNWebClient.csproj --framework net9.0-windows10.0.19041.0
   ```

5. **Ejecutar en Android (con emulador):**
   ```bash
   dotnet run --project src/MSISDNWebClient.csproj --framework net9.0-android
   ```

## 📱 Funcionalidades

### Páginas principales
- **Welcome**: Página de bienvenida e introducción
- **Onboarding**: Configuración inicial de la aplicación
- **Home**: Dashboard principal con resumen de información
- **Profile**: Gestión del perfil de usuario
- **Explorer**: Exploración de servicios disponibles
- **PersonaDetail**: Detalles de identidades digitales

### Servicios
- **ApiService**: Comunicación con APIs REST
- **AuthService**: Autenticación y autorización
- **CryptoService**: Operaciones criptográficas
- **StorageService**: Almacenamiento local de datos
- **SettingsService**: Configuración de la aplicación
- **NavigationService**: Navegación programática
- **PersonaService**: Gestión de identidades digitales

## 🔧 Configuración

### Variables de entorno
Crear un archivo `appsettings.local.json` en la carpeta `src/` con:

```json
{
  "ApiBaseUrl": "https://tu-api.com/",
  "ApiKey": "tu-api-key",
  "AppName": "MSISDN Web Client"
}
```

### Configuración de desarrollo
El proyecto incluye configuración para:
- Hot Reload en desarrollo
- Logging para debugging
- Exception handlers globales
- Validación de entrada de datos

## 🧪 Testing

Ejecutar las pruebas unitarias:
```bash
dotnet test
```

Las pruebas se encuentran en la carpeta `tests/` e incluyen:
- Tests de ViewModels
- Tests de servicios
- Tests de validadores

## 📖 Estructura de navegación

La aplicación utiliza Shell Navigation con las siguientes rutas:
- `/welcome` - Página de bienvenida
- `/onboarding` - Configuración inicial  
- `/home` - Dashboard principal
- `/profile` - Perfil de usuario
- `/explorer` - Explorador de servicios
- `/persona/{id}` - Detalle de persona/identidad

## ⚠️ Notas importantes

### Problema resuelto: Colors.xaml
Si encuentras problemas de ejecución en Windows, verifica que el archivo `src/Resources/Styles/Colors.xaml` NO tenga el atributo `x:Class`. Los ResourceDictionary puros no deben incluir este atributo.

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la licencia MIT.

## 📞 Soporte

Para soporte o preguntas:
- Crear un [Issue](../../issues) en GitHub
- Revisar la [documentación](docs/)

## 🔄 Changelog

### v1.0.0 (2025-11-06)
- ✅ Implementación inicial multiplataforma
- ✅ Arquitectura MVVM completa
- ✅ Sistema de navegación Shell
- ✅ Servicios de API y autenticación
- ✅ UI/UX responsive
- ✅ Soporte para Windows, Android, iOS y macOS
- ✅ Problema de Colors.xaml resuelto

---

**Desarrollado con ❤️ usando .NET MAUI**