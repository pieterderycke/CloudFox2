using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFox2.Phone.ViewModels
{
    public class BookmarkViewModel
    {
        private readonly IList<BookmarkViewModel> childs;

        public BookmarkViewModel(string title)
        {
            this.childs = new List<BookmarkViewModel>();
            this.Title = title;
        }

        public string Title { get; private set; }

        public IEnumerable<BookmarkViewModel> Childs { get { return childs; } }

        public void AddChild(BookmarkViewModel bookmark)
        {
            childs.Add(bookmark);
        }
    }
}
