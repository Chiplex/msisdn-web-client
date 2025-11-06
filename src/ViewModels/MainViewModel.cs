using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSISDNWebClient.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
    private string _title = string.Empty;
    private string _statusMessage = string.Empty;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Title = "MSISDN Web Client";
            StatusMessage = "Welcome to the application!";
        }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}