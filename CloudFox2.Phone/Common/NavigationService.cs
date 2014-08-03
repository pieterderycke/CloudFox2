using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CloudFox2.Phone.Common
{
    public class NavigationService : INavigationService
    {
        public void NavigateTo<TPage>()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(TPage));
        }
    }
}
