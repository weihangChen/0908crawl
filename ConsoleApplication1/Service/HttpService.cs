using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ConsoleApplication1.Service;
using ConsoleApplication1.Model.Http;

namespace ConsoleApplication1.Service
{
    public interface IHttpService
    {
        Task<T> HttpGetAsEntity<T>(QueryModel queryModel);

        Task<string> HttpGetHTML(QueryModel queryModel);
    }
    public class HttpService : IHttpService
    {
        private IExternalIntegrationErrorHandler ErrorHandler;
        private IRequestBuilder Builder;

        public HttpService(IExternalIntegrationErrorHandler ErrorHandler, IRequestBuilder Builder)
        {
            this.ErrorHandler = ErrorHandler;
            this.Builder = Builder;
        }
        public async Task<T> HttpGetAsEntity<T>(QueryModel queryModel)
        {
            T obj = default(T);
            HttpClient client = default(HttpClient);

            try
            {
                using (client = new HttpClient())
                {
                    string url = Builder.BuildHttpClientGet(queryModel, client);
                    HttpResponseMessage response = await client.GetAsync(url);

                    var content = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (HttpRequestException hrex)
            {
                ErrorHandler.HandleError(hrex);
            }
            finally
            {
                if (client != null)
                    client.Dispose();
            }
            return obj;
        }

        public async Task<string> HttpGetHTML(QueryModel queryModel)
        {
            HttpClient client = default(HttpClient);

            try
            {
                using (client = new HttpClient())
                {
                    string url = Builder.BuildHttpClientGet(queryModel, client);
                    HttpResponseMessage response = await client.GetAsync(url);
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
            catch (HttpRequestException hrex)
            {
                ErrorHandler.HandleError(hrex);
            }
            finally
            {
                if (client != null)
                    client.Dispose();
            }
            return "";
        }
    }






    public interface IExternalIntegrationErrorHandler
    {
        void HandleError(HttpRequestException hrex);
    }
    public class ExternalIntegrationErrorHandler : IExternalIntegrationErrorHandler
    {
        //private ILog LogService;

        //public ExternalIntegrationErrorHandler(ILog logService)
        //{
        //  //  LogService = logService;


        //}
        public void HandleError(HttpRequestException hrex)
        {
          //  LogService.Error(hrex.ToString());
        }
    }
}


