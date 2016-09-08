using System.Collections.Generic;
using System.Text;
using ConsoleApplication1.Model.Http;
using ConsoleApplication1.Extensions;

namespace ConsoleApplication1.Model.Booking
{
    //for widget
    public class LiveRateJSWidgetParamModel
    {
        public LiveRateJSWidgetParamModel(string hotel_id)
        {
            this.hotel_id = hotel_id;
        }
        public string hotel_id { get; set; }
        public string checkin { get; set; }
        public string checkout { get; set; }
        public string currency { get; set; }
        public int total_rooms { get; set; }
        public int adults_per_room { get; set; }
        public string room { get; set; }
    }

    //for request param
    public class LiveRateParameterModel : ParameterModel
    {
        public LiveRateParameterModel(string hotel_id)
        {
            this.hotel_id = hotel_id;
        }
        public string hotel_id { get; set; }
        public string checkin { get; set; }
        public string checkout { get; set; }
        public string currency { get; set; }
        public string direct_price { get; set; }
        public string room { get; set; }

    }

    //for response mapping
    public class LiveRateResponseModel : ResponseModel
    {
        public Meta Meta { get; set; }
        public Response Response { get; set; }

        public override bool ContainsError
        {
            get
            {
                return !string.IsNullOrEmpty(Response.Error);
            }
        }

        public override string Error
        {
            get
            {
                if (!ContainsError)
                    return "";
                var builder = new StringBuilder();
                builder.Append(string.Format("Error Detail: {0}", Response.Error));
                var query = Meta.Query;
                if (query != null)
                {
                    builder.Append(" / Request Query Parameters: ");
                    Meta.Query.GetPropertyNames().ForEach(x => { builder.Append(string.Format("{0} : {1} / ", x, Meta.Query.GetPropertyValue(x))); });

                }
                return builder.ToString();
            }
        }


    }

    public class Meta
    {
        public string Status { get; set; }
        public Query Query { get; set; }

    }


    public class Query
    {
        public string Hotel_Id { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Total_Rooms { get; set; }
        public string Adults_Per_Room { get; set; }
        public string Children_Per_Room { get; set; }
        public string Currency { get; set; }

    }


    public class Response
    {
        public List<PriceEntity> Prices { get; set; }
        public string Error { get; set; }
        public Response()
        {
            Prices = new List<PriceEntity>();
        }
    }

    public class PriceEntity
    {
        public string Domain { get; set; }
        public double? Price { get; set; }
        public string Currency { get; set; }
        public bool Is_Direct_Price { get; set; }


    }
}

