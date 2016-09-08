namespace ConsoleApplication1.Model.Http
{
    public class QueryModel
    {
        public QueryModel(string serviceEndpoint)
        {
            ServiceEndpoint = serviceEndpoint;
        }
        public string ServiceEndpoint { get; set; }
        public string UrlRequest { get; set; }

        public AuthenticationModel AuthModel { get; set; }

        public ParameterModel ParamModel { get; set; }

    }

    public abstract class ParameterModel
    {
        //public string hotel_id { get; set; }
        //public string checkin { get; set; }
        //public string checkout { get; set; }
        //public string room { get; set; }
        //public string direct_price { get; set; }

    }
}

