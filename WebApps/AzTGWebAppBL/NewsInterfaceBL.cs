using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using AzTGWebAppEntities;

namespace AzTGWebAppBL
{
    public class NewsInterfaceBL
    {
        static NewsApiClient newsApiClient = new NewsApiClient("dc7a3fa09e834e95a9f888c57dc834e0");

        public LatestNews GetLatestNews(string[] categories)
        {
            LatestNews latestNews = new LatestNews();

            // init with your API key
            

            foreach (string category in categories)
            {
                var articlesResponse = newsApiClient.GetTopHeadlines(new TopHeadlinesRequest
                {
                    Category = GetCategory(category),
                    Country = Countries.IN,
                    Language = Languages.EN,
                });

                if (articlesResponse.Status == Statuses.Ok)
                {
                    List<NewsArticle> newsArticles = new List<NewsArticle>();

                    latestNews.TotalNewsCount.Add(category, articlesResponse.TotalResults);

                    foreach (var newsArticle in articlesResponse.Articles)
                    {
                        NewsArticle article = new NewsArticle
                        {
                            Author = newsArticle.Author,
                            Description = newsArticle.Description,
                            PublishedAt = newsArticle.PublishedAt,
                            Title = newsArticle.Title,
                            Url = newsArticle.Url,
                            UrlToImage = newsArticle.UrlToImage
                        };

                        newsArticles.Add(article);
                    }

                    latestNews.NewsArticles.Add(category, newsArticles);
                }
            }

            return latestNews;
        }

        private static Categories GetCategory(string category)
        {
            Categories newsCategory;

            switch (category)
            {
                case "Business":
                    newsCategory = Categories.Business;
                    break;
                case "Sports":
                    newsCategory = Categories.Sports;
                    break;
                case "Health":
                    newsCategory = Categories.Health;
                    break;
                case "Science":
                    newsCategory = Categories.Science;
                    break;
                case "Technology":
                    newsCategory = Categories.Technology;
                    break;
                default:
                    newsCategory = Categories.Sports;
                    break;
            }

            return newsCategory;
        }
    }
}