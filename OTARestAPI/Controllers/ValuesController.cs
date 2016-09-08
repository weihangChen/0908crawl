using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ConsoleApplication1.Service;
using ConsoleApplication1.Builder;
using ConsoleApplication1.Model.Booking;

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


        //example of json becing sent in
        //contenttype is application/json
        //        {
        //	"endpoint":"http://booking.com",
        //	"searchUrl": "http://www.booking.com/searchresults.en-gb.html",
        //  	"hotelId": 1368239,
        //    "ParamModel":
        //     {
        //  			"ss":"Rozmalas",
        //  			"checkin_monthday":24,
        //  			"checkin_month":9,
        //  			"checkin_year":2016,
        //  			"checkout_monthday":25,
        //  			"checkout_month":9,
        //  			"checkout_year":2016,
        //  			"room1":"A,A",
        //  			"no_rooms":1,
        //  			"group_adults":2,
        //  			"group_children":0,
        //  			"ss_raw":"Rozmalas"
        //  		}


        //}
        // POST api/values
        public async Task<IHttpActionResult> Post(BookingComMetaModel metaModel)
        {
            var res = new ResponseEntity();
            try
            {
                IHttpService httpService = new HttpService(null, new BookingCOMRequestBuilder());
                IBookingComService bookingComService = new BookingComService(httpService);
                res.data = await bookingComService.GetHotelHTMLAsync(metaModel);
                
            }
            catch (Exception e)
            {
                res.error = e.ToString();
            }

            return Ok(res);
        }


    }
}
