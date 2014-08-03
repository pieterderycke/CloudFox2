using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFox2.Phone.Common
{
    public interface INavigationService
    {
        void NavigateTo<TPage>();
    }
}
