using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication1.Model.Http;
using ConsoleApplication1.Model.Booking;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Service;
using ConsoleApplication1.Builder;
using ConsoleApplication1.Model.Http;
using ConsoleApplication1.Model.Booking;
using System.IO;
using HtmlAgilityPack;



namespace ConsoleApplication1.Service
{
    public interface IBookingComService
    {
        Task<string> GetBookingHTMLSearchResult(QueryModel queryModel);

        Task<string> GetHotelHTMLAsync(BookingComMetaModel metaModel);
    }
    public class BookingComService : IBookingComService
    {
        private IHttpService HttpService;

        public BookingComService(IHttpService httpService)
        {
            HttpService = httpService;
        }


        public async Task<string> GetBookingHTMLSearchResult(QueryModel queryModel)

        {
            return await HttpService.HttpGetHTML(queryModel);
        }

        


        //public async Task<string> GetHotelHTML(string endpoint, string searchUrl, string hotelId, string hotelName, int checkin_monthday, int checkin_month, int checkin_year, int checkout_monthday, int checkout_month,
        //    int checkout_year, string room1, int no_rooms, int group_adults, int group_children)

        public async Task<string> GetHotelHTMLAsync(BookingComMetaModel metaModel)
        {
            var paramModel = metaModel.ParamModel;

            IRequestBuilder Builder = new BookingCOMRequestBuilder();
            var authModel = new BasicAuthenticationModel("", "");
            var queryModel = new QueryModel(metaModel.searchUrl) { AuthModel = authModel, ParamModel = paramModel };
            var resultHTML = await GetBookingHTMLSearchResult(queryModel);
            ILinkExtractService linkExtractor = new LinkExtractService();

            var url = linkExtractor.GetHotelUrl(resultHTML, metaModel.hotelId, metaModel.endpoint);
            url = url.Substring(0, url.IndexOf("?"));

            queryModel = new QueryModel(url);
            queryModel.ParamModel = paramModel;

            var html = await GetBookingHTMLSearchResult(queryModel);

            
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var result = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='blockdisplay1']").OuterHtml;

            

            return result;
        }


    }



}


