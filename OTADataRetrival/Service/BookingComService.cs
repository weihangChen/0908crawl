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
using OTADataRetrival.Service;



namespace ConsoleApplication1.Service
{
    public interface IBookingComService
    {
        Task<string> GetBookingHTMLSearchResult(QueryModel queryModel);

        

        Task<HotelData> GetHotelHTMLAsync(BookingComMetaModel metaModel);
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

        


        public async Task<HotelData> GetHotelHTMLAsync(BookingComMetaModel metaModel)
        {
            var result = new HotelData();
            var paramModel = metaModel.ParamModel;

            IRequestBuilder Builder = new BookingComRequestBuilder();
            var authModel = new BasicAuthenticationModel("", "");
            var queryModel = new QueryModel(metaModel.searchUrl) { AuthModel = authModel, ParamModel = paramModel };
            var resultHTML = await GetBookingHTMLSearchResult(queryModel);
            var linkExtractor = new BookingComLinkExtractService();

            var url = linkExtractor.GetHotelUrl(resultHTML, metaModel.hotelId, metaModel.endpoint);
            //if url is empty then there is another middle page
            if (string.IsNullOrEmpty(url))
            {
                var middleurl = linkExtractor.GetHotelUrl1(resultHTML, metaModel.hotelId, metaModel.endpoint);
                queryModel = new QueryModel(middleurl) { AuthModel = authModel, ParamModel = new ParameterModel() };
                var tmpHTML = await GetBookingHTMLSearchResult(queryModel);
                url = linkExtractor.GetHotelUrl(tmpHTML, metaModel.hotelId, metaModel.endpoint);
            }

            if (string.IsNullOrEmpty(url))
                result.Error = "no url can be extracted";
            else
            {

                url = url.Substring(0, url.IndexOf("?"));

                queryModel = new QueryModel(url);
                queryModel.ParamModel = paramModel;

                var html = await GetBookingHTMLSearchResult(queryModel);

               var htmlDoc= AgilityParser.GetParser(html);
                var priceNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='blockdisplay1']");
                if (priceNode == null)
                {
                    result.Msg = "We have no availability on our site for this property";
                }
                else

                    result.PriceData = priceNode.OuterHtml;

            }

            return result;
        }


    }

    public class HotelData
    {
        public string PriceData { get; set; }
        public string Msg { get; set; }
        public string Error { get; set; }
    }

}


