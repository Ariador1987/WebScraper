using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.RegularExpressions;
using WebScraper.Builders;
using WebScraper.Data;
using WebScraper.Workers;

namespace WebScraper
{
    class Program
    {
        private const string Method = "search";

        static  void Main(string[] args)
        {
            Console.WriteLine("Please enter which city you would like to scrape information from:");
            var craigsListCity = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the CraigsList category that you would like to scrape:");
            var craigsListCategoryName = Console.ReadLine() ?? "";


            //using (HttpResponseMessage response = await client.GetAsync($"http://{craigsListCity.Replace(" ", "")}.craigslist.org/{Method}/{craigsListCategoryName}"))
            using (WebClient client = new WebClient())
            {
                string content = client.DownloadString($"http://{craigsListCity.Replace(" ", "")}.craigslist.org/{Method}/{craigsListCategoryName}");
                ScrapeCriteria scrapeCriteria = new ScrapeCriteriaBuilder()
                    .WithData(content)
                    .WithRegex(@"<a href=\""(.*?)\"" data-id=\""(.*?)\"" class=\""result-title hdrlink\"">(.*?)</a>")
                    .WithRegexOption(RegexOptions.ExplicitCapture)
                    .WithPart(new ScrapeCriteriaPartBuilder()
                        .WithRegex(@">(.*?)</a>")
                        .WithRegexOption(RegexOptions.Singleline)
                        .Build())
                    .WithPart(new ScrapeCriteriaPartBuilder()
                        .WithRegex(@"href=\""(.*?)\""")
                        .WithRegexOption(RegexOptions.Singleline)
                        .Build())
                    .Build();

                Scraper scraper = new Scraper();

                var scrapedElements = scraper.Scrape(scrapeCriteria);
                if (scrapedElements.Count > 0)
                {
                    foreach (var scrapedElement in scrapedElements)
                    {
                        Console.WriteLine(scrapedElement);
                    }
                }
                else
                {
                    Console.WriteLine("No matches for the specified scrape criteria.");
                }
            }

            Console.ReadLine();
        }
    }
}