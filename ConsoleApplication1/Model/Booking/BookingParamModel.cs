using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Model.Http;

namespace ConsoleApplication1.Model.Booking
{
    class BookingParamModel : ParameterModel
    {
        public string ss { get; set; }
        public int checkin_monthday { get; set; }
        public int checkin_month { get; set; }
        public int checkin_year { get; set; }
        public int checkout_monthday { get; set; }
        public int checkout_month { get; set; }
        public int checkout_year { get; set; }
        public string room1 { get; set; }
        public int no_rooms { get; set; }
        public int group_adults { get; set; }
        public int group_children { get; set; }
        public string ss_raw { get; set; }
    }



}
