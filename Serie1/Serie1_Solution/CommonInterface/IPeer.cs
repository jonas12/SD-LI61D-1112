using System.Collections.Generic;

namespace CommonInterface
{
    public interface IPeer
    {
        List<Article> Articles { get; set; }
        Article GetArticleBy(string title);
    }
}
