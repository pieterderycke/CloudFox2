using CloudFox2.Phone.Common;
using CloudFox2.Phone.ViewModels;
using CloudFox2.Phone.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CloudFox2.Phone.Views
{
    public sealed partial class MainPage : PageBase
    {
        private SettingManager settingManager;

        public MainPage()
        {
            this.InitializeComponent();
            this.IntialiazeViewModel<MainViewModel>();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.settingManager = AutofacResolver.Resolve<SettingManager>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(!settingManager.IsLoggedIn)
                Frame.Navigate(typeof(ChooseLoginPage));

            ((MainViewModel)DataContext).Refresh.Execute(null);
        }

        private void OnBookmarkTapped(object sender, TappedRoutedEventArgs e)
        {
            MainViewModel viewModel = (MainViewModel)DataContext;
            BookmarkViewModel bookmark = (BookmarkViewModel)((TextBlock)sender).DataContext;

            viewModel.OpenBookmark(bookmark);
        }
    }
}
