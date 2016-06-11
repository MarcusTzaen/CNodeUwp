using CNodeUwp.Common;
using Edi.UWP.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace CNodeUwp.ViewModels
{
    public class FeedbackViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; set; }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                Set(nameof(Description), ref _description, value);
            }
        }

        private bool _withDeviceInfo = true;
        public bool WithDeviceInfo
        {
            get
            {
                return _withDeviceInfo;
            }
            set
            {
                Set(nameof(WithDeviceInfo), ref _withDeviceInfo, value);
            }
        }

        public RelayCommand SendCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string subject = $"[UWP][CNode][{Utils.GetAppVersion()}]反馈";
                    string body = Description;
                    if (WithDeviceInfo)
                    {
                        var deviceInfo = new EasClientDeviceInformation();
                        body += deviceInfo.ToDescription();
                    }
                    Tasks.OpenEmailComposeAsync("Marcus.M.Tzaen@outlook.com", subject, body);
                });
            }
        }

        public FeedbackViewModel(
            INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
