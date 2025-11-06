using Microsoft.Maui;
using Microsoft.UI.Xaml;

namespace MSISDNWebClient.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MSISDNWebClient.MauiProgram.CreateMauiApp();
}
