using System;
using Xunit;
using Xunit.Abstractions;
using AzTGWebAppBL;
using AzTGWebAppEntities;

namespace AzTGWebAppTest
{
    public class NewsInterfaceBLTest
    {
        private readonly ITestOutputHelper output;

        public NewsInterfaceBLTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData(new string[] {"Business", "Health", "Sports"}, 1)]
        //[InlineData("Health", 1)]
        //[InlineData("Sports", 1)]
        public void CanGetLatestNews(string[] newsCategory, int expectedNewsCount)
        {
            //Arrange
            var sut = new NewsInterfaceBL();

            //Act
            var result = sut.GetLatestNews(newsCategory);

            //Assert
            foreach (string category in newsCategory)
            {
                Assert.True(result.TotalNewsCount[category] > expectedNewsCount);

                output.WriteLine(newsCategory + ": " + result.TotalNewsCount);

                foreach (NewsArticle newsArticle in result.NewsArticles[category])
                {
                    output.WriteLine("Author: " + newsArticle.Author);
                    output.WriteLine("Description: " + newsArticle.Description);
                    output.WriteLine("Title: " + newsArticle.Title);
                    output.WriteLine("Url: " + newsArticle.Url);
                }
            }
        }
    }
}
