using CloudFox2.Phone.Common;
using CloudFox2.Phone.Views;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CloudFox2.Phone.ViewModels
{
    public class ChooseLoginViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public ChooseLoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.SignIn = new RelayCommand(() => { navigationService.NavigateTo<LoginPage>(); });
        }

        public ICommand SignIn { get; private set; }
    }
}
