using System;
using Microsoft.Maui.Controls;
using MSISDNWebClient.ViewModels;

namespace MSISDNWebClient.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Aquí puedes agregar lógica adicional cuando la página aparece
        }

        private async void OnNavigateToDashboard(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }

        private async void OnNavigateToDetail(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetailPage());
        }
    }
}