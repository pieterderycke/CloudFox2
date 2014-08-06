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
using CloudFox2.Phone.Util;

namespace CloudFox2.Phone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly SettingManager settingManager;
        private readonly INavigationService navigationService;
        private readonly SyncClient syncClient;

        private readonly Stack<BookmarkViewModel> displayStack;

        public MainViewModel(SettingManager settingManager, INavigationService navigationService, SyncClient syncClient)
        {
            this.settingManager = settingManager;
            this.navigationService = navigationService;
            this.syncClient = syncClient;

            this.displayStack = new Stack<BookmarkViewModel>();

            SignOut = new RelayCommand(SignOutImplementation);
            Refresh = new RelayCommand(RefreshImplementation);
            GoBookmarkUp = new RelayCommand(GoBookmarkUpImplementation);
            GoBookmarkHome = new RelayCommand(GoBookmarkHomeImplementation);

            Bookmarks = new ObservableCollection<BookmarkViewModel>();
        }

        public ICommand SignOut { get; private set; }

        public ICommand Refresh { get; private set; }
        public ICommand GoBookmarkUp { get; private set; }
        public ICommand GoBookmarkHome { get; private set; }

        public ObservableCollection<BookmarkViewModel> Bookmarks { get; private set; }

        public void OpenBookmark(BookmarkViewModel bookmark)
        {
            Bookmarks.Clear();
            Bookmarks.AddAll(bookmark.Children);

            displayStack.Push(bookmark);
        }

        private void SignOutImplementation()
        {
            settingManager.Clear();
            navigationService.NavigateTo<ChooseLoginPage>();
        }

        private async void RefreshImplementation()
        {
            if (settingManager.IsLoggedIn)
            {
                if (!syncClient.IsSignedIn)
                    await syncClient.SignIn(settingManager.UserName, settingManager.Password);

                IEnumerable<Bookmark> bookmarks = await syncClient.GetBookmarks();

                Bookmarks.Clear();
                BookmarkViewModel root = HierarchizeBookmarks(bookmarks);
                Bookmarks.AddAll(root.Children);
                displayStack.Push(root);
            }
        }

        private void GoBookmarkUpImplementation()
        {
            displayStack.Pop();

            Bookmarks.Clear();
            Bookmarks.AddAll(displayStack.Peek().Children);
        }

        private void GoBookmarkHomeImplementation()
        {
            BookmarkViewModel root = displayStack.Last();
            displayStack.Clear();
            displayStack.Push(root);

            Bookmarks.Clear();
            Bookmarks.AddAll(root.Children);
        }

        private BookmarkViewModel HierarchizeBookmarks(IEnumerable<Bookmark> bookmarks)
        {
            Dictionary<string, BookmarkViewModel> viewModels = new Dictionary<string, BookmarkViewModel>();

            foreach(Bookmark bookmark in bookmarks)
            {
                BookmarkViewModel viewModel = new BookmarkViewModel(bookmark.Title);

                viewModels.Add(bookmark.Id, viewModel);
            }

            foreach (Bookmark bookmark in bookmarks)
            {
                BookmarkViewModel viewModel = viewModels[bookmark.Id];

                if(bookmark.Children != null)
                    foreach (string child in bookmark.Children)
                        viewModel.AddChild(viewModels[child]);
            }

            BookmarkViewModel root = new BookmarkViewModel("Root");
            root.AddChild(viewModels["unfiled"]);
            root.AddChild(viewModels["menu"]);
            root.AddChild(viewModels["toolbar"]);

            return root;
        }
    }
}
