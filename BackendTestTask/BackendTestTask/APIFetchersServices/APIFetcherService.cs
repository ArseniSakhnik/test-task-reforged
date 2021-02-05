using BackendTestTask.APIFetchersServices.FinnhubAPIService;
using BackendTestTask.APIFetchersServices.MoexAPIService;
using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Models;
using BackendTestTask.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices
{
    public class APIFetcherService : IAPIFetcherService
    {

        private readonly IFinnhubAPIService _finnhubAPIService;
        private readonly IMoexAPIService _moexAPIService;
        private DataContext DataContext { get; set; }

        public APIFetcherService(IFinnhubAPIService finnhubAPIService, IMoexAPIService moexAPIService)
        {
            _finnhubAPIService = finnhubAPIService;
            _moexAPIService = moexAPIService;
        }

        public async Task<Quotation> GetQuotation(Company company)
        {
            FinnhubApiResponse finnhubApiResponse = await _finnhubAPIService.GetCompanyProfileByTicker(company.Ticker);

            if (finnhubApiResponse != null)
            {
                return new Quotation
                {
                    SourceId = 1,
                    CompanyId = company.Id,
                    Price = finnhubApiResponse.c,
                    CurrencyUnit = "USD",
                    Date = DateTime.Now
                };
            }

            MoexApiResponse moexApiResponse = await _moexAPIService.GetMoexCompanies();

            Quotation quotation = null;

            foreach (var m in moexApiResponse.securities.data)
            {
                if ((string)m[0] == company.Ticker)
                {
                    quotation = new Quotation
                    {
                        SourceId = 2,
                        CompanyId = company.Id,
                        Price = (double)m[1],
                        CurrencyUnit = (string)m[2],
                        Date = new DateTime(
                                int.Parse(((string)m[3]).Substring(0, 4)),
                                int.Parse(((string)m[3]).Substring(5, 2)),
                                int.Parse(((string)m[3]).Substring(8, 2)),
                                DateTime.Now.Hour,
                                DateTime.Now.Minute,
                                DateTime.Now.Second
                            )
                    };
                }
            }

            return quotation;

        }
    }
}
