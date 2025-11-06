using System;
using Microsoft.Maui.Controls;
using MSISDNWebClient.ViewModels;

namespace MSISDNWebClient.Views
{
    public partial class DashboardPage : ContentPage
    {
    private readonly DashboardViewModel _viewModel;

        public DashboardPage()
        {
            InitializeComponent();
            _viewModel = new DashboardViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel.LoadRecordsCommand.CanExecute(null))
            {
                _viewModel.LoadRecordsCommand.Execute(null);
            }
        }
    }
}