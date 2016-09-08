using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model.Booking
{
    public class BookingComMetaModel
    {
        public BookingParamModel ParamModel { get; set; }
        public string endpoint { get; set; }
        public string searchUrl { get; set; }
        public string hotelId { get; set; }

        
    }
}
