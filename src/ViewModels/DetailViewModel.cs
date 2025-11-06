using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSISDNWebClient.ViewModels
{
    public class DetailViewModel : INotifyPropertyChanged
    {
    private string _msisdnDetail = string.Empty;
    public string MSISDNDetail
        {
            get => _msisdnDetail;
            set
            {
                if (_msisdnDetail != value)
                {
                    _msisdnDetail = value;
                    OnPropertyChanged();
                }
            }
        }

        public DetailViewModel()
        {
            // Initialize properties or load data here
        }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}