using System;
using System.Collections.Generic;

namespace CommonInterface.Utils
{
    public static class ArticleHelpers
    {
        public static bool IsDefault(this Article a)
        {
            return a.Equals(default(Article));
        }

        public static Article GetArticle(ref List<IPeer> peers, string title)
        {
            Article article;

            foreach (IPeer p in peers)
            {
                article = p.GetArticleBy(title);

                if (!article.IsDefault())
                    return article;
            }

            return default(Article);
        }
    }
}
