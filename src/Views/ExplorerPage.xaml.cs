using MSISDNWebClient.ViewModels;

namespace MSISDNWebClient.Views
{
    public partial class ExplorerPage : ContentPage
    {
        public ExplorerPage(ExplorerViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (BindingContext is ExplorerViewModel vm)
            {
                await vm.LoadFeaturedAsync();
            }
        }
    }
}
