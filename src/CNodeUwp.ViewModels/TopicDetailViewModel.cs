using CNodeUwp.Common;
using CNodeUwp.Models.Topic.Version1;
using CNodeUwp.Services.Topic.Version1;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Threading;

namespace CNodeUwp.ViewModels
{
    public class TopicDetailViewModel : LoginViewModel
    {
        private INavigationService _navigationService { get; set; }

        private IDialogService _dialogService { get; set; }

        public string TopicId { get; set; }

        private NotifyTaskCompletion<TopicDetailResponse> _topic;
        public NotifyTaskCompletion<TopicDetailResponse> Topic
        {
            get { return _topic; }
            set
            {
                Set(nameof(Topic), ref _topic, value);
            }
        }
        
        public RelayCommand RefreshCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    //GetTopics(1);
                });
            }
        }

        public TopicDetailViewModel(
            INavigationService navigationService
            , IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            MessengerInstance.Register<string>(this, (topicId) =>
            {
                GetTopics(topicId);
            });
        }

        private void GetTopics(string topicId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = TopicService.GetTopicDetailAsync(new TopicDetailRequest()
            {
                TopicId = topicId,
            }, cancellationToken);
            Topic = new NotifyTaskCompletion<TopicDetailResponse>(response);
        }
    }
}
