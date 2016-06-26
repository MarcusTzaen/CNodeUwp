using CNodeUwp.Common;
using CNodeUwp.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace CNodeUwp
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure(Consts.TOPIC_LIST_PAGE_KEY, typeof(MainPage));
            nav.Configure(Consts.TOPIC_DETAIL_PAGE_KEY, typeof(TopicDetail));
            nav.Configure(Consts.FEEDBACK_PAGE_KEY, typeof(Feedback));
            nav.Configure(Consts.ABOUT_PAGE_KEY, typeof(AboutPage));
            nav.Configure(Consts.SCANNING_PAGE_KEY, typeof(Scanning));


            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            //}
            //else
            //{
            //    SimpleIoc.Default.Register<IDataService, DataService>();
            //}

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<TopicDetailViewModel>();
            SimpleIoc.Default.Register<AboutPageViewModel>();
            SimpleIoc.Default.Register<FeedbackViewModel>();
        }

        public MainPageViewModel MainPageVm => ServiceLocator.Current.GetInstance<MainPageViewModel>();

        public TopicDetailViewModel TopicDetailVm => ServiceLocator.Current.GetInstance<TopicDetailViewModel>();

        public AboutPageViewModel AboutPageVm => ServiceLocator.Current.GetInstance<AboutPageViewModel>();

        public FeedbackViewModel FeedbackVm => ServiceLocator.Current.GetInstance<FeedbackViewModel>();
    }
}
