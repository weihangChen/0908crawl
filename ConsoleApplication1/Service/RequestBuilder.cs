using System;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using ConsoleApplication1.Extensions;
using ConsoleApplication1.Model.Http;


namespace ConsoleApplication1.Service
{
    public interface IRequestBuilder
    {
        string BuildHttpClientGet(QueryModel queryModel, HttpClient client);
    }

    public class RequestBuilder
    {
        protected void AppendBasicHeader(BasicAuthenticationModel authModel, HttpClient client)
        {
            var byteArray = System.Text.Encoding.UTF8.GetBytes(authModel.Name + ":" + authModel.Password);
            var base64String = Convert.ToBase64String(byteArray);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
        }


        protected void AppendParams(QueryModel queryModel)
        {
            var builder = new UriBuilder(queryModel.ServiceEndpoint);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            queryModel.ParamModel.GetPropertyNames().ForEach(x =>
            {
                var propertyValue = queryModel.ParamModel.GetPropertyValue(x);
                if (propertyValue != null)
                    query[x] = propertyValue.ToString();
            });
            builder.Query = query.ToString();
            queryModel.UrlRequest = builder.ToString();

        }

    }
}



