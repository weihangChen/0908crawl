using System.Net.Http;
using ConsoleApplication1.Model.Http;
using ConsoleApplication1.Service;

namespace ConsoleApplication1.Builder
{
    public class LiveRateRequestBuilder : RequestBuilder, IRequestBuilder
    {
        public string BuildHttpClientGet(QueryModel queryModel, HttpClient client)
        {
            AppendBasicHeader(queryModel.AuthModel as BasicAuthenticationModel, client);
            AppendParams(queryModel);
            return queryModel.UrlRequest;
        }

    }
}
