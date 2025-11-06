# Arquitectura del Proyecto MSISDN-WEB Client

## Introducción
El proyecto MSISDN-WEB Client es una aplicación multiplataforma desarrollada utilizando .NET MAUI. Su objetivo es proporcionar una interfaz de usuario intuitiva y centrada en el usuario para gestionar registros de MSISDN y perfiles de usuario.

## Estructura del Proyecto
La estructura del proyecto se organiza en varias carpetas que contienen diferentes tipos de archivos:

- **src**: Contiene el código fuente de la aplicación.
  - **Models**: Define las clases que representan los datos de la aplicación, como `MSISDNRecord` y `UserProfile`.
  - **ViewModels**: Contiene las clases que manejan la lógica de presentación, como `MainViewModel`, `DashboardViewModel` y `DetailViewModel`.
  - **Views**: Define las interfaces de usuario para las diferentes pantallas de la aplicación, incluyendo `MainPage`, `DashboardPage` y `DetailPage`.
  - **Services**: Incluye servicios que manejan la lógica de negocio, como `ApiService`, `NavigationService` y `SettingsService`.
  - **Helpers**: Contiene clases de utilidad, como `ValidationHelper`, que proporciona métodos para validar entradas de usuario.
  - **Controls**: Define controles personalizados, como `UserCard`, que se utilizan en las vistas.
  - **Converters**: Incluye convertidores, como `BoolToVisibilityConverter`, que ayudan a transformar datos para la interfaz de usuario.
  - **Resources**: Contiene recursos como fuentes, imágenes y estilos que se utilizan en toda la aplicación.

- **tests**: Contiene el proyecto de pruebas unitarias para asegurar la calidad del código.
  
- **docs**: Incluye documentación relacionada con la arquitectura y los flujos de experiencia de usuario de la aplicación.

## Navegación
La aplicación utiliza un servicio de navegación para gestionar la transición entre diferentes vistas. Esto permite una experiencia de usuario fluida y coherente.

## Estilos y Temas
Los estilos y temas globales se definen en `Styles.xaml` y `Colors.xaml`, lo que permite mantener una apariencia consistente en toda la aplicación.

## Conclusión
La arquitectura del proyecto MSISDN-WEB Client está diseñada para ser modular y escalable, facilitando el mantenimiento y la expansión de la aplicación en el futuro.