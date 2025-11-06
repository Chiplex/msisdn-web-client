using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSISDNWebClient.Models;

namespace MSISDNWebClient.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private ObservableCollection<MSISDNRecord> _msisdnRecords = new();
        private MSISDNRecord? _selectedRecord;

        public ObservableCollection<MSISDNRecord> MSISDNRecords
        {
            get => _msisdnRecords;
            set => SetProperty(ref _msisdnRecords, value);
        }

    public MSISDNRecord? SelectedRecord
        {
            get => _selectedRecord;
            set => SetProperty(ref _selectedRecord, value);
        }

        public ICommand LoadRecordsCommand { get; }
        public ICommand NavigateToDetailCommand { get; }

        public DashboardViewModel()
        {
            MSISDNRecords = new ObservableCollection<MSISDNRecord>();
            LoadRecordsCommand = new RelayCommand(LoadRecords);
            NavigateToDetailCommand = new RelayCommand(NavigateToDetail);
        }

        private void LoadRecords()
        {
            // Logic to load MSISDN records from the service
        }

        private void NavigateToDetail()
        {
            // Logic to navigate to the detail page with the selected record
        }
    }
}