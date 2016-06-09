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
            nav.Configure(Consts.ABOUT_PAGE_KEY, typeof(AboutPage));


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
        }

        public MainPageViewModel MainPageVm => ServiceLocator.Current.GetInstance<MainPageViewModel>();

        public TopicDetailViewModel TopicDetailVm => ServiceLocator.Current.GetInstance<TopicDetailViewModel>();

        public AboutPageViewModel AboutPageVm => ServiceLocator.Current.GetInstance<AboutPageViewModel>();
    }
}
