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
    /// <summary>
    /// Сервис для получения данных API
    /// </summary>
    public class APIFetcherService : IAPIFetcherService
    {

        private readonly IFinnhubAPIService _finnhubAPIService;
        private readonly IMoexAPIService _moexAPIService;

        public APIFetcherService(IFinnhubAPIService finnhubAPIService, IMoexAPIService moexAPIService)
        {
            _finnhubAPIService = finnhubAPIService;
            _moexAPIService = moexAPIService;
        }

        /// <summary>
        /// Возвращает котировку компании
        /// </summary>
        /// <param name="company">Компания, котировку которой необходимо узнать</param>
        /// <returns>Котировку с moex, если она была найдена, котировку с finnhub или null</returns>
        public async Task<Quotation> GetQuotation(Company company)
        {
            FinnhubApiResponse finnhubApiResponse = await _finnhubAPIService.GetCompanyProfileByTicker(company.Ticker);

            if (finnhubApiResponse != null)
            {
                return new Quotation
                {
                    SourceId = 1,
                    CompanyId = company.Id,
                    Price = finnhubApiResponse.Price,
                    CurrencyUnit = finnhubApiResponse.CurrencyUnit,
                    Date = DateTime.Now
                };
            }

            MoexApiResponse moexApiResponse = await _moexAPIService.GetCompanyProfileByTicker(company.Ticker);

            if (moexApiResponse != null)
            {
                return new Quotation
                {
                    SourceId = 2,
                    CompanyId = company.Id,
                    Price = moexApiResponse.Price,
                    CurrencyUnit = moexApiResponse.CurrencyUnit,
                    Date = moexApiResponse.Date
                };
            }

            return null;
        }
    }
}
