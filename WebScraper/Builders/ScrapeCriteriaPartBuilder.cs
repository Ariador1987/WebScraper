using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebScraper.Data;

namespace WebScraper.Builders
{
    public class ScrapeCriteriaPartBuilder
    {
        //public string Regex { get; set; }
        //public RegexOptions RegexOption { get; set; }
        private string _regex;
        private RegexOptions _regexOption;

        public ScrapeCriteriaPartBuilder()
        {
            SetDefaults();
        }

        public void SetDefaults()
        {
            _regex = "";
            _regexOption = RegexOptions.None;
        }

        public ScrapeCriteriaPartBuilder WithRegex(string regex)
        {
            _regex = regex;
            return this;
        }

        public ScrapeCriteriaPartBuilder WithRegexOption(RegexOptions regexOption)
        {
            regexOption = _regexOption;
            return this;
        }

        public ScrapeCriteriaPart Build()
        {
            ScrapeCriteriaPart scrapeCriteriaPart = new ();
            scrapeCriteriaPart.Regex = _regex;
            scrapeCriteriaPart.RegexOption = _regexOption;
            return scrapeCriteriaPart;
        }
    }
}
