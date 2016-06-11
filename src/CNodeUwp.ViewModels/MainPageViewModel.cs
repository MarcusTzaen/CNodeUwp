using CNodeUwp.Common;
using CNodeUwp.Models.Topic.Version1;
using CNodeUwp.Services.Common.Version1;
using CNodeUwp.Services.Topic.Version1;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using Windows.UI.Xaml.Controls;

namespace CNodeUwp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; set; }

        private IDialogService _dialogService { get; set; }

        private TopicTabType TabType { get; set; } = TopicTabType.All;

        private PaginatedCollection<TopicResponse> _topics;
        public PaginatedCollection<TopicResponse> Topics
        {
            get { return _topics; }
            set
            {
                Set(nameof(Topics), ref _topics, value);
            }
        }

        private bool _isLoadingCompleted;
        public bool IsLoadingCompleted
        {
            get { return _isLoadingCompleted; }
            set
            {
                Set(nameof(IsLoadingCompleted), ref _isLoadingCompleted, value);
            }
        }

        public RelayCommand<string> FilterCommand
        {
            get
            {
                return new RelayCommand<string>((tab) =>
                           {
                               TopicTabType tabType = TopicTabType.All;
                               if (Enum.TryParse(tab, out tabType))
                               {
                                   TabType = tabType;
                                   GetTopics();
                               }

                           });
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    GetTopics();
                });
            }
        }

        public RelayCommand FeedbackCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigationService.NavigateTo(Consts.FEEDBACK_PAGE_KEY, this);
                });
            }
        }

        public RelayCommand<ItemClickEventArgs> GoToDetailCommand
        {
            get
            {
                return new RelayCommand<ItemClickEventArgs>((args) =>
                {
                    var topicResponse = args.ClickedItem as TopicResponse;
                    _navigationService.NavigateTo(Consts.TOPIC_DETAIL_PAGE_KEY, topicResponse.Id);
                });
            }
        }

        public MainPageViewModel(
            INavigationService navigationService
            , IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            GetTopics();
        }

        private void GetTopics()
        {
            Topics = new PaginatedCollection<TopicResponse>((c) =>
            {
                return TopicService.GetTopicListAsync(new TopicPageRequest()
                {
                    Page = c,
                    Tab = TabType,
                });
            });
        }
    }
}
