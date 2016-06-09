using CNodeUwp.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CNodeUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopicDetail : Page
    {
        public TopicDetailViewModel TopicDetailVm
        {
            get
            {
                return (TopicDetailViewModel)DataContext;
            }
        }
        public TopicDetail()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    e.Handled = true;
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Messenger.Default.Send<string, TopicDetailViewModel>(e.Parameter.ToString());
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(TopicDetailVm);
            base.OnNavigatedFrom(e);
        }


    }
}
