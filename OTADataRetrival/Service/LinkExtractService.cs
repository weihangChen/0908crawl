using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ConsoleApplication1.Service
{
    public interface ILinkExtractService
    {
        string GetHotelUrl(string html, string hotelId, string endpoint);
    }
    public class LinkExtractService: ILinkExtractService
    {
        public string GetHotelUrl( string html, string hotelId, string endpoint)
        {
            var result = "";
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            foreach (HtmlNode link1 in htmlDoc.DocumentNode.SelectNodes("//a[@href]").Where(d =>
                     d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("hotel_name_link url")
                     && d.Attributes["href"].Value.Contains(hotelId)))
            {
                HtmlAttribute att = link1.Attributes["href"];
                foreach (var link in att.Value.Split(' '))
                {
                    result = link;
                    break;
                   
                }
            }
            return string.Concat(endpoint, result);
        }

        
    }

  



}

