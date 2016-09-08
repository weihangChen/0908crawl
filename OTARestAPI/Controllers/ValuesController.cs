using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ConsoleApplication1.Service;
using ConsoleApplication1.Builder;

namespace OTARestAPI.Controllers
{
    public class ResponseEntity
    {
        public ResponseEntity()
        {
            id = Guid.NewGuid().ToString();
        }
        public string id { get; set; }
        public string data { get; set; }
        public string error { get; set; }
    }



    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public async Task<IHttpActionResult> Post([FromBody]string value)
        {

            var res = new ResponseEntity();



            const string hotelName = "Rozmalas";
            const string endpoint = "http://booking.com";
            const string hotelId = "1368239";
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
            const string searchUrl = "http://www.booking.com/searchresults.en-gb.html";



            try
            {
                IHttpService httpService = new HttpService(null, new BookingCOMRequestBuilder());
                IBookingComService bookingComService = new BookingComService(httpService);


                res.data = await bookingComService.GetHotelHTML(endpoint, searchUrl, hotelId, hotelName, checkin_monthday,
                        checkin_month, checkin_year, checkout_monthday, checkout_month, checkout_year, room1, no_rooms,
                        group_adults, group_children);



            }
            catch (Exception e)
            {
                res.error = e.ToString();
            }




            return Ok(res);
        }

        
    }
}
