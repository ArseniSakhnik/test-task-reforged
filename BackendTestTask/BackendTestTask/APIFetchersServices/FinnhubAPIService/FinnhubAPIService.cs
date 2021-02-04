using BackendTestTask.Helpers;
using BackendTestTask.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
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
                    using (HttpResponseMessage res = client.GetAsync(GetCompanyProfileUrl(ticker)).Result)
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            FinnhubApiResponse finnhubApiResponse = null;

                            if (data != "{\"c\":0,\"h\":0,\"l\":0,\"o\":0,\"pc\":0,\"t\":0}")
                            {
                                finnhubApiResponse = JsonConvert.DeserializeObject<FinnhubApiResponse>(data);
                            }

                            return finnhubApiResponse;
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
