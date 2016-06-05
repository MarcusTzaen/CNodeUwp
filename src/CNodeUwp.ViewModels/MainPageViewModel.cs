using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace CNodeUwp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; set; }

        private IDialogService _dialogService { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Set(nameof(Title), ref _title, value);
            }
        }

        public RelayCommand GoToAboutCommand
        {
            get
            {
                return new RelayCommand(() =>
                           {
                               _navigationService.NavigateTo("About");
                           });
            }
        }

        public RelayCommand ShowDialogCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _dialogService.ShowMessage("Hello world!", "My Title");
                });
            }
        }

        public MainPageViewModel(
            INavigationService navigationService
            , IDialogService dialogService)
        {
            this.Title = "Welcome to main page";
            _navigationService = navigationService;
            _dialogService = dialogService;
        }
    }
}
