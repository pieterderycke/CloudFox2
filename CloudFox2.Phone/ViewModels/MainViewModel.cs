using CloudFox2.Phone.Common;
using CloudFox2.Phone.Views;
using FxSyncNet;
using FxSyncNet.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CloudFox2.Phone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly SettingManager settingManager;
        private readonly INavigationService navigationService;
        private readonly SyncClient syncClient;

        public MainViewModel(SettingManager settingManager, INavigationService navigationService, SyncClient syncClient)
        {
            this.settingManager = settingManager;
            this.navigationService = navigationService;
            this.syncClient = syncClient;

            SignOut = new RelayCommand(SignOutImplementation);
            Refresh = new RelayCommand(RefreshImplementation);
            Bookmarks = new ObservableCollection<BookmarkViewModel>();
        }

        public ICommand SignOut { get; private set; }

        public ICommand Refresh { get; private set; }

        public ObservableCollection<BookmarkViewModel> Bookmarks { get; private set; }

        private void SignOutImplementation()
        {
            settingManager.Clear();
            navigationService.NavigateTo<ChooseLoginPage>();
        }

        private async void RefreshImplementation()
        {
            Bookmarks.Clear();

            if(!syncClient.IsSignedIn)
                await syncClient.SignIn(settingManager.UserName, settingManager.Password);

            IEnumerable<Bookmark> bookmarks = await syncClient.GetBookmarks();
            foreach(BookmarkViewModel bookmark in HierarchizeBookmarks(bookmarks))
            {
                Bookmarks.Add(bookmark);
            }
        }

        private IEnumerable<BookmarkViewModel> HierarchizeBookmarks(IEnumerable<Bookmark> bookmarks)
        {
            Dictionary<string, BookmarkViewModel> parents = new Dictionary<string, BookmarkViewModel>();

            foreach(Bookmark bookmark in bookmarks)
            {
                BookmarkViewModel model = new BookmarkViewModel(bookmark.Title);

                if (bookmark.ParentId != "places" && bookmark.ParentId != "toolbar" && bookmark.ParentId != "mobile" && bookmark.ParentId != "menu")
                {
                    if (parents.ContainsKey(bookmark.ParentId))
                    {
                        BookmarkViewModel parent = parents[bookmark.ParentId];
                        parent.AddChild(model);
                    }
                }

                parents.Add(bookmark.Id, model);

            }

            return parents["unfiled"].Childs;
        }
    }
}
