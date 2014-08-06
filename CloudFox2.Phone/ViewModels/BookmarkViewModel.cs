using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFox2.Phone.ViewModels
{
    public class BookmarkViewModel
    {
        private readonly IList<BookmarkViewModel> children;

        public BookmarkViewModel(string title)
        {
            this.children = new List<BookmarkViewModel>();
            this.Title = title;
        }

        public string Title { get; private set; }

        public IEnumerable<BookmarkViewModel> Children { get { return children; } }

        public void AddChild(BookmarkViewModel bookmark)
        {
            children.Add(bookmark);
        }
    }
}
