using CloudFox2.Phone.Common;
using CloudFox2.Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CloudFox2.Phone.Views
{
    public sealed partial class ChooseLoginPage : PageBase
    {
        public ChooseLoginPage()
        {
            this.InitializeComponent();
            this.IntialiazeViewModel<ChooseLoginViewModel>();
        }
    }
}
