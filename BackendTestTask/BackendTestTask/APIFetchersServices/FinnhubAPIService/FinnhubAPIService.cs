using BackendTestTask.Helpers;
using BackendTestTask.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.FinnhubAPIService
{
    public class FinnhubAPIService
    {
        private readonly IOptions<AppSettings> _options;

        public FinnhubAPIService(IOptions<AppSettings> options)
        {
            _options = options;
        }

        public string GetCompanyProfileUrl(string ticker)
        {
            return $"https://finnhub.io/api/v1/quote?symbol={ticker}&token={_options.Value.FinnhubKey}";
        }

        public async ValueTask<FinnhubApiResponse> GetCompanyProfileByTicker(string ticker)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(GetCompanyProfileUrl(ticker)))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != "{}")
                            {
                                FinnhubApiResponse finnhubApiResponse = JsonConvert.DeserializeObject<FinnhubApiResponse>(data);
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
