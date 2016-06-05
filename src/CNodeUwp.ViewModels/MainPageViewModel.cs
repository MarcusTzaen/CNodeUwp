using CNodeUwp.Common;
using CNodeUwp.Models.Topic.Version1;
using CNodeUwp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using System.Threading;

namespace CNodeUwp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; set; }

        private IDialogService _dialogService { get; set; }

        private NotifyTaskCompletion<ObservableCollection<TopicResponse>> _topics;
        public NotifyTaskCompletion<ObservableCollection<TopicResponse>> Topics
        {
            get { return _topics; }
            set
            {
                Set(nameof(Topics), ref _topics, value);
            }
        }

        public RelayCommand FilterCommand
        {
            get
            {
                return new RelayCommand(() =>
                           {
                               //await GetTopics(1, TopicTabType.All);
                               //_navigationService.NavigateTo("About");
                           });
            }
        }

        public MainPageViewModel(
            INavigationService navigationService
            , IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            GetTopics(1, TopicTabType.All);
        }

        private void GetTopics(int pageNumber, TopicTabType tab, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = TopicService.GetTopicListAsync(new TopicPageRequest()
            {
                Page = pageNumber,
                Tab = tab,
            }, cancellationToken);
            Topics = new NotifyTaskCompletion<ObservableCollection<TopicResponse>>(response);
        }
    }
}
