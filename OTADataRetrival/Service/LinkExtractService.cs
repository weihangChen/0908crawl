using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;

namespace ConsoleApplication1.Service
{
    public interface ILinkExtractService
    {
        string GetHotelUrl(string html, string hotelId, string endpoint);
        string GetHotelUrl1(string html, string hotelId, string endpoint);
    }
    public class LinkExtractService : ILinkExtractService
    {
        public string GetHotelUrl(string html, string hotelId, string endpoint)
        {
            var result = "";
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var hit = false;
            foreach (HtmlNode link1 in htmlDoc.DocumentNode.SelectNodes("//a[@href]").Where(d =>
                     d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("hotel_name_link")))
            {
                var tmpurl = link1.Attributes["href"].Value.ToLower();
                if (tmpurl.Contains(hotelId))
                {
                    hit = true;
               
                }
                else
                {
                    var hotelName = "Bo Hotel";
                    var tokens = hotelName.ToLower().Split().AsEnumerable().ToList();
                    if (!tokens.Any(x => !tmpurl.Contains(x)))
                    {
                        hit = true;
                     
                    }
                }

                if (hit)
                {
                    HtmlAttribute att = link1.Attributes["href"];
                    foreach (var link in att.Value.Split(' '))
                    {
                        result = link;
                        break;
                    }
                    break;
                }
            }
            if (string.IsNullOrEmpty(result))
                return "";
            return string.Concat(endpoint, result);
        }


        public string GetHotelUrl1(string html, string hotelId, string endpoint)
        {
            var result = "";
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            foreach (HtmlNode link1 in htmlDoc.DocumentNode.SelectNodes("//a[@href]").Where(d =>
                     d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("item_name_link")
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

