using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Model.Http;

namespace ConsoleApplication1.Model.Booking
{
    public class BookingParamModel : ParameterModel
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

        public BookingParamModel(string hotelName, int checkin_monthday, int checkin_month, int checkin_year, int checkout_monthday, int checkout_month, 
            int checkout_year, string room1, int no_rooms, int group_adults,int group_children)
        {
            this.ss = hotelName;
            this.checkin_monthday = checkin_monthday;
            this.checkin_month = checkin_month;
            this.checkin_year = checkin_year;
            this.checkout_monthday = checkout_monthday;
            this.checkout_month = checkout_month;
            this.checkout_year = checkout_year;
            this.room1 = room1;
            this.no_rooms = no_rooms;
            this.group_adults = group_adults;
            this.group_children = group_children;
            this.ss_raw = hotelName;
        }
    }



}
