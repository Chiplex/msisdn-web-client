using System;
using Microsoft.Maui.Controls;
using MSISDNWebClient.ViewModels;

namespace MSISDNWebClient.Views
{
    public partial class DetailPage : ContentPage
    {
    private readonly DetailViewModel _viewModel;

        public DetailPage()
        {
            InitializeComponent();
            _viewModel = new DetailViewModel();
            BindingContext = _viewModel;
        }
    }
}