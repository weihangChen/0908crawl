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

        Task<string> GetHotelHTML(string endpoint, string searchUrl, string hotelId, string hotelName, int checkin_monthday, int checkin_month, int checkin_year, int checkout_monthday, int checkout_month,
            int checkout_year, string room1, int no_rooms, int group_adults, int group_children);
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





        public async Task<string> GetHotelHTML(string endpoint, string searchUrl, string hotelId, string hotelName, int checkin_monthday, int checkin_month, int checkin_year, int checkout_monthday, int checkout_month,
            int checkout_year, string room1, int no_rooms, int group_adults, int group_children)
        {
            var paramModel = new BookingParamModel(
                    hotelName,
                    checkin_monthday,
                    checkin_month,
                    checkin_year,
                    checkout_monthday,
                    checkout_month,
                    checkout_year,
                    room1,
                    no_rooms,
                    group_adults,
                    group_children
                );

            IRequestBuilder Builder = new BookingCOMRequestBuilder();
            var authModel = new BasicAuthenticationModel("", "");
            var queryModel = new QueryModel(searchUrl) { AuthModel = authModel, ParamModel = paramModel };
            var resultHTML = await GetBookingHTMLSearchResult(queryModel);
            ILinkExtractService linkExtractor = new LinkExtractService();

            var url = linkExtractor.GetHotelUrl(resultHTML, hotelId, endpoint);
            url = url.Substring(0, url.IndexOf("?"));

            queryModel = new QueryModel(url);
            queryModel.ParamModel = paramModel;

            return await GetBookingHTMLSearchResult(queryModel);
        }


    }



}


