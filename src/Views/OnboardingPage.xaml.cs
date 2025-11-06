using MSISDNWebClient.ViewModels;

namespace MSISDNWebClient.Views
{
    public partial class OnboardingPage : ContentPage
    {
        public OnboardingPage(OnboardingViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
