using MSISDNWebClient.ViewModels;

namespace MSISDNWebClient.Views
{
    public partial class PersonaDetailPage : ContentPage
    {
        public PersonaDetailPage(PersonaDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
