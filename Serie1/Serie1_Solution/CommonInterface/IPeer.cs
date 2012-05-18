using System.Collections.Generic;

namespace CommonInterface
{
    public interface IPeer
    {
        IEnumerable<Article> Articles { get; set; }
        Article GetArticleBy(string title);
    }
}
