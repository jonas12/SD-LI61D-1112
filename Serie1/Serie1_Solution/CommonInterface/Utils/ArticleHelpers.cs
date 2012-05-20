using System;

namespace CommonInterface.Utils
{
    public static class ArticleHelpers
    {
        public static bool IsDefault(this Article a)
        {
            return a.Equals(default(Article));
        }
    }
}
