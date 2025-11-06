using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSISDNWebClient.Models;
using MSISDNWebClient.Services;

namespace MSISDNWebClient.ViewModels
{
    /// <summary>
    /// ViewModel para el proceso de onboarding y autenticación
    /// </summary>
    public partial class OnboardingViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private string msisdn = string.Empty;

        [ObservableProperty]
        private string otpCode = string.Empty;

        [ObservableProperty]
        private bool isOtpSent;

        [ObservableProperty]
        private string otpMessage = string.Empty;

        [ObservableProperty]
        private bool canVerify;

        public OnboardingViewModel(AuthService authService, NavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
            Title = "Crear tu Identidad";
        }

        /// <summary>
        /// Solicita el código OTP
        /// </summary>
        [RelayCommand]
        private async Task RequestOTPAsync()
        {
            if (string.IsNullOrWhiteSpace(Msisdn))
            {
                SetError("Por favor ingresa tu número de teléfono");
                return;
            }

            IsBusy = true;
            ClearError();

            try
            {
                var response = await _authService.RequestOTPAsync(Msisdn);
                
                if (response.Success && response.Data != null)
                {
                    IsOtpSent = true;
                    CanVerify = true;
                    OtpMessage = response.Message;
                    SetError(string.Empty);
                }
                else
                {
                    SetError(response.Message);
                }
            }
            catch (System.Exception ex)
            {
                SetError($"Error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Verifica el código OTP e crea la identidad
        /// </summary>
        [RelayCommand]
        private async Task VerifyOTPAsync()
        {
            if (string.IsNullOrWhiteSpace(OtpCode))
            {
                SetError("Por favor ingresa el código OTP");
                return;
            }

            IsBusy = true;
            ClearError();

            try
            {
                var response = await _authService.VerifyOTPAsync(OtpCode);
                
                if (response.Success && response.Data != null)
                {
                    // OTP verificado, identidad creada
                    await _navigationService.NavigateToRootAsync(Routes.Home);
                }
                else
                {
                    SetError(response.Message);
                }
            }
            catch (System.Exception ex)
            {
                SetError($"Error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        partial void OnMsisdnChanged(string value)
        {
            // Validar formato mientras se escribe
            if (!string.IsNullOrWhiteSpace(value) && !value.StartsWith("+"))
            {
                // Auto-agregar + si no lo tiene
                if (value.Length > 0 && char.IsDigit(value[0]))
                {
                    Msisdn = "+" + value;
                }
            }
        }
    }
}
