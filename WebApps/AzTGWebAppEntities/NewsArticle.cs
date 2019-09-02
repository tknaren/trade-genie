using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzTGWebAppEntities
{
    public class LatestNews
    {
        public IDictionary<string, int> TotalNewsCount;
        public IDictionary<string, ICollection<NewsArticle>> NewsArticles { get; set; }
    }

    public class NewsArticle
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}