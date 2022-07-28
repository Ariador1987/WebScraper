using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebScraper.Data;

namespace WebScraper.Workers
{
    public class Scraper
    {
        public List<string> Scrape(ScrapeCriteria scrapeCriteria)
        {
            List<string> scrappedElements = new List<string>();

            // scraping operation
            MatchCollection matches = Regex.Matches(scrapeCriteria.Data, scrapeCriteria.Regex, scrapeCriteria.RegexOption);

            foreach (Match match in matches)
            {
                // tricky
                if (scrapeCriteria.Parts.Count > 0 is false)
                {
                    scrappedElements.Add(match.Groups[0].Value);
                }
                else
                {
                    foreach (var part in scrapeCriteria.Parts)
                    {
                        Match matchedPart = Regex.Match(match.Groups[0].Value, part.Regex, part.RegexOption);

                        if (matchedPart.Success)
                            scrappedElements.Add(matchedPart.Groups[1].Value);
                    }
                }
            }

            // when all is done return scrappedElements as list of strings
            return scrappedElements;
        }
    }
}
