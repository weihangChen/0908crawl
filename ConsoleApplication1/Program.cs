using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Service;
using ConsoleApplication1.Builder;
using ConsoleApplication1.Model.Http;
using ConsoleApplication1.Model.Booking;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            //http://www.booking.com/searchresults.en-gb.html?ss=Rozmalas&checkin_monthday=24&checkin_month=9&checkin_year=2016&checkout_monthday=25&checkout_month=9&checkout_year=2016&room1=A,A&no_rooms=1&group_adults=2&group_children=0&dest_id=&dest_type=&ss_raw=Rozmalas
            string bookingsearch = "http://www.booking.com/searchresults.en-gb.html";


            try
            {
                IRequestBuilder Builder = new LiveRateRequestBuilder();
                var httpService = new HttpService(null, new LiveRateRequestBuilder());
                var liveRateService = new LiveRateService(httpService);






                var authModel = new BasicAuthenticationModel("", "");
                var paramModel = new LiveRateParameterModel("")
                {
                    //checkin = checkinDate,
                    //checkout = checkIn.AddDays(NumberOfNights).ToString("yyyy-MM-dd"),
                    //direct_price = ourPrice.ToString(),
                    //room = "a,a"
                };


                var queryModel = new QueryModel(bookingsearch) { AuthModel = authModel, ParamModel = paramModel };
                var liverate = liveRateService.GetBookingHTMLSearchResult(queryModel).Result;
                Console.WriteLine(liverate);


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
