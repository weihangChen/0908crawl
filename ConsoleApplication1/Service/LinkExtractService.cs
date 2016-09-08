using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplication1.Service
{
    class LinkExtractService
    {
        public List<string> GetHotelUrls(string html, string siteHrefRegex)
        {

            Regex rgx = new Regex(siteHrefRegex, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(html);
            var allUrls = new List<string>();
            foreach (Match match in matches)
            {
                allUrls.Add(match.Value);
            }

            return allUrls;

        }




        //public LinkExtractionResult GetHotelUrl(string html, string siteHrefRegex, string hotelId)
        //{
        //    var result = new LinkExtractionResult();
        //    //string pattern = string.Format(@"<a>[\s\S]*a>", hotelIdBookingCom);

        //    string pattern = string.Format(siteHrefRegex, hotelId);

        //    Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
        //    MatchCollection matches = rgx.Matches(html);
        //    if (matches.Count == 0)
        //    {
        //        result.Error = string.Format("hotel {0} can't be found", hotelId);

        //    }
        //    //else if (matches.Count != 1)
        //    //{
        //    //    result.Error = "id is unique, only one link should be found";
        //    //}

        //    else
        //    {
        //        foreach (Match match in matches)
        //        {
        //            result.FoundUrl = match.Value;
        //            Console.WriteLine(match.Value);
        //            //break;
        //        }
        //    }
        //    return result;

        //}
    }

    class LinkExtractionResult
    {
        public string Error { get; set; }
        public string FoundUrl { get; set; }
    }



}

