﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CNodeUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        //private void AppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    lvTopics.ScrollIntoView(lvTopics.Items[0], ScrollIntoViewAlignment.Leading);
        //}
    }
}
