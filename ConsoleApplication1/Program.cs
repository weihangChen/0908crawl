using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Service;
using ConsoleApplication1.Builder;
using ConsoleApplication1.Model.Http;
using ConsoleApplication1.Model.Booking;
using System.IO;


namespace ConsoleApplication1
{
    class Program
    {
        const string hotelname = "Rozmalas";

        static void Main(string[] args)
        {
            //step1
            //http://www.booking.com/searchresults.en-gb.html?ss=Rozmalas&checkin_monthday=24&checkin_month=9&checkin_year=2016&checkout_monthday=25&checkout_month=9&checkout_year=2016&room1=A,A&no_rooms=1&group_adults=2&group_children=0&dest_id=&dest_type=&ss_raw=Rozmalas
            //step2
            //http://www.booking.com/hotel/lv/rozmalas.en-gb.html?ss=Rozmalas&checkin_monthday=24&checkin_month=9&checkin_year=2016&checkout_monthday=25&checkout_month=9&checkout_year=2016&room1=A%2cA&no_rooms=1&group_adults=2&group_children=0&ss_raw=Rozmalas

            string bookingsearch = "http://www.booking.com/searchresults.en-gb.html";
            var hotelId = "1368239";
            var hrefRegex = "(?i)<a([^>]+)>(.+?)</a>";

            try
            {

                var paramModel = new BookingParamModel()
                {
                    ss = "Rozmalas",
                    checkin_monthday = 24,
                    checkin_month = 9,
                    checkin_year = 2016,
                    checkout_monthday = 25,
                    checkout_month = 9,
                    checkout_year = 2016,
                    room1 = "A,A",
                    no_rooms = 1,
                    group_adults = 2,
                    group_children = 0,
                    ss_raw = "Rozmalas",
                };

                IRequestBuilder Builder = new LiveRateRequestBuilder();
                var httpService = new HttpService(null, new LiveRateRequestBuilder());
                var liveRateService = new LiveRateService(httpService);
                var authModel = new BasicAuthenticationModel("", "");
               
                var queryModel = new QueryModel(bookingsearch) { AuthModel = authModel, ParamModel = paramModel };
                var resultHTML = liveRateService.GetBookingHTMLSearchResult(queryModel).Result;
                


                var linkExtractor = new LinkExtractService();
                var test = linkExtractor.GetHotelUrls(resultHTML, hrefRegex);
                var validLinks = test.Where(x => x.Contains(hotelId));
                foreach(var x in validLinks)
                {
                    int first = x.IndexOf("href") + 7;
                    int last = x.IndexOf('?');
                    string str2 = x.Substring(first, last - first);

                    var url = "http://www.booking.com/" + str2;
                    queryModel = new QueryModel(url);
                    queryModel.ParamModel = paramModel;
                    Console.WriteLine(x);
                    var ttt = liveRateService.GetBookingHTMLSearchResult(queryModel).Result;
                    Console.WriteLine(ttt);


                    var t1 = ttt.Contains("570");
               

                    Console.WriteLine("-------");






                    string path = @"c:\temp\MyTest.html";
                    if (File.Exists(path))
                        File.Delete(path);

                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine(ttt);
                        }
                    }

                }
                //Console.WriteLine(test.Error);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            Console.WriteLine("-----------------------");
            Console.ReadLine();
        }
    }
}



