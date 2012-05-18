using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonInterface.Utils
{
    public static class ArticleHelper
    {
        public static bool IsDefault(this Article a)
        {
            return a.Equals(default(Article));
        }
    }
}
