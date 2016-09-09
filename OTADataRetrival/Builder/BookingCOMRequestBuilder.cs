using System.Net.Http;
using ConsoleApplication1.Model.Http;
using ConsoleApplication1.Service;
using System.Web;

namespace ConsoleApplication1.Builder
{
    public class BookingCOMRequestBuilder : RequestBuilder, IRequestBuilder
    {
        public string BuildHttpClientGet(QueryModel queryModel, HttpClient client)
        {
            //AppendBasicHeader(queryModel.AuthModel as BasicAuthenticationModel, client);
            SimulateBrowserSetting(client);
            AppendParams(queryModel);
            return HttpUtility.UrlDecode(queryModel.UrlRequest);
        }

       
    }
}
