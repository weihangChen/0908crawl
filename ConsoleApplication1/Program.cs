using System;
using ConsoleApplication1.Service;
using ConsoleApplication1.Builder;
using System.IO;
using System.Diagnostics;
using ConsoleApplication1.Model.Booking;


//step1
//http://www.booking.com/searchresults.en-gb.html?ss=Rozmalas&checkin_monthday=24&checkin_month=9&checkin_year=2016&checkout_monthday=25&checkout_month=9&checkout_year=2016&room1=A,A&no_rooms=1&group_adults=2&group_children=0&dest_id=&dest_type=&ss_raw=Rozmalas
//step2
//http://www.booking.com/hotel/lv/rozmalas.en-gb.html?ss=Rozmalas&checkin_monthday=24&checkin_month=9&checkin_year=2016&checkout_monthday=25&checkout_month=9&checkout_year=2016&room1=A%2cA&no_rooms=1&group_adults=2&group_children=0&ss_raw=Rozmalas
//test url
//public static string url = "http://www.booking.com/hotel/lv/rozmalas.en-gb.html?ss=Rozmalas&checkin_monthday=24&checkin_month=9&checkin_year=2016&checkout_monthday=25&checkout_month=9&checkout_year=2016&room1=A%2cA&no_rooms=1&group_adults=2&group_children=0&ss_raw=Rozmalas";


namespace ConsoleApplication1
{
    class Program
    {
        //        Bo Hotel 
        // JBL Concept AB(1406)

        //751123

        const string searchUrl = "http://www.booking.com/searchresults.en-gb.html";
        const string endpoint = "http://booking.com";
        //const string hotelId = "1368239";
        const string hotelName = "Bo Hotel";
        const string hotelId = "751123";
        //const string hotelName = "Rozmalas";


        const int checkin_monthday = 24;
        const int checkin_month = 9;
        const int checkin_year = 2016;
        const int checkout_monthday = 25;
        const int checkout_month = 9;
        const int checkout_year = 2016;
        const string room1 = "A,A";
        const int no_rooms = 1;
        const int group_adults = 2;
        const int group_children = 0;
        static Stopwatch stopwatch = new Stopwatch();


        static void Main(string[] args)
        {

            try
            {
                stopwatch.Start();
                IHttpService httpService = new HttpService(null, new BookingCOMRequestBuilder());
                IBookingComService bookingComService = new BookingComService(httpService);

                var paramModel = new BookingParamModel(hotelName, checkin_monthday,
                        checkin_month, checkin_year, checkout_monthday, checkout_month, checkout_year, room1, no_rooms,
                        group_adults, group_children);
                var metaModel = new BookingComMetaModel { hotelId = hotelId, endpoint = endpoint, searchUrl = searchUrl, ParamModel = paramModel };

                var r = bookingComService.GetHotelHTMLAsync(metaModel).Result;
                stopwatch.Stop();
                Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed);
                if (!string.IsNullOrEmpty(r.PriceData))
                    PostProcess(r.PriceData);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();
        }

        static void PostProcess(string html)
        {
            string path = @"c:\temp\MyTest.html";
            if (File.Exists(path))
                File.Delete(path);

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(html);
                }
            }
        }
    }
}



