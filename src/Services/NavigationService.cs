using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MSISDNWebClient.Services;

public class NavigationService
{
    public async Task NavigateToAsync(string route, bool animate = true)
    {
        try
        {
            await Shell.Current.GoToAsync(route, animate);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error de navegación: {ex.Message}");
        }
    }

    public async Task GoBackAsync(bool animate = true)
    {
        try
        {
            await Shell.Current.GoToAsync("..", animate);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al retroceder: {ex.Message}");
        }
    }

    public async Task NavigateToRootAsync(string route)
    {
        try
        {
            var absoluteRoute = $"//{route}";
            await Shell.Current.GoToAsync(absoluteRoute);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error de navegación a raíz: {ex.Message}");
        }
    }
}

public static class Routes
{
    public const string Welcome = "welcome";
    public const string Onboarding = "onboarding";
    public const string Home = "home";
    public const string Profile = "profile";
    public const string Explorer = "explorer";
    public const string PersonaDetail = "personadetail";
}
