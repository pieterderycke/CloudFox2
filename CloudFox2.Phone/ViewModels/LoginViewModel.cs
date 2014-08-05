using CloudFox2.Phone.Common;
using CloudFox2.Phone.Views;
using FxSyncNet;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CloudFox2.Phone.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly SettingManager settingManager;
        private readonly INavigationService navigationService;

        public LoginViewModel(SettingManager settingManager, INavigationService navigationService, SyncClient syncClient)
        {
            this.settingManager = settingManager;
            this.navigationService = navigationService;

            this.SignIn = new RelayCommand(async () => 
            {
                await syncClient.SignIn(UserName, Password);
                settingManager.Save();

                navigationService.NavigateTo<MainPage>();
            });
        }

        public string UserName 
        { 
            get
            {
                return settingManager.UserName;
            }
            set
            {
                settingManager.UserName = value;
            }
        }

        public string Password
        {
            get
            {
                return settingManager.Password;
            }
            set
            {
                settingManager.Password = value;
            }
        }

        public ICommand SignIn { get; private set; }
    }
}
