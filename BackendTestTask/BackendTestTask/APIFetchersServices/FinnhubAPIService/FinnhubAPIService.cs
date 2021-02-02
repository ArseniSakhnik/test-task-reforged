using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.FinnhubAPIService
{
    public class FinnhubAPIService : IFinnhubAPIService
    {
        private readonly IOptions<AppSettings> _options;
        public FinnhubAPIService(IOptions<AppSettings> options)
        {
            _options = options;
        }

        public string GetUrlCompanies(string key)
        {
            return $"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={key}";
        }

        public string GetUrlCompanieProfile(string symbol, string key)
        {
            return $"https://finnhub.io/api/v1/stock/profile2?symbol={symbol}&token={key}";
        }

        public async ValueTask<List<StockSymbol>> GetCompanies()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(GetUrlCompanies(_options.Value.FinnhubKey)))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != "{}")
                            {

                                List<StockSymbol> stockSymbols = JsonConvert.DeserializeObject<List<StockSymbol>>(data);

                                return stockSymbols;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<string> GetCompanyNameByTicker(string ticker)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(GetUrlCompanieProfile(ticker, _options.Value.FinnhubKey)))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != "{}")
                            {
                                var dataObj = JObject.Parse(data);

                                return $"{dataObj["name"]}";
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
