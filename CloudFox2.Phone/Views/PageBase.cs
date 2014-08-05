using CloudFox2.Phone.Common;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CloudFox2.Phone.Views
{
    public abstract class PageBase : Page
    {
        private NavigationHelper navigationHelper;

        public PageBase() : base()
        {
            this.Loaded += OnPageLoaded;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.OnLoadState;
            this.navigationHelper.SaveState += this.OnSaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public void IntialiazeViewModel<TViewModel>() where TViewModel : ViewModelBase
        {
            this.DataContext = AutofacResolver.Resolve<TViewModel>();
        }

        protected void OnLoadState(object sender, LoadStateEventArgs e)
        {
        }

        protected void OnSaveState(object sender, SaveStateEventArgs e)
        {
        }

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendView(this.GetType().Name);
        }
    }
}
