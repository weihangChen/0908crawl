using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication1.Model.Http;
using ConsoleApplication1.Model.Booking;



namespace ConsoleApplication1.Service
{
    public interface ILiveRateService
    {
        Task<LiveRateResponseModel> GetLiveRateResponseModel(QueryModel queryModel);
    }
    public class LiveRateService : ILiveRateService
    {
        private IHttpService HttpService;

        public LiveRateService(IHttpService httpService)
        {
            HttpService = httpService;
        }
        public async Task<LiveRateResponseModel> GetLiveRateResponseModel(QueryModel queryModel)

        {
            return await HttpService.HttpGetAsEntity<LiveRateResponseModel>(queryModel);
        }



        public async Task<string> GetBookingHTMLSearchResult(QueryModel queryModel)

        {
            return await HttpService.HttpGetHTML(queryModel);
        }


    }








}


