using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace CNodeUwp.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; set; }

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

        public RelayCommand GoToMainCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigationService.NavigateTo("Main");
                });
            }
        }

        public AboutPageViewModel(
            INavigationService navigationService)
        {
            this.Title = "Welcome to about page";
            _navigationService = navigationService;
        }
    }
}
