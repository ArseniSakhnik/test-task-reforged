using BackendTestTask.Helpers;
using BackendTestTask.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.MoexAPIService
{
    public class MoexAPIService : IMoexAPIService
    {
        private readonly IOptions<AppSettings> _options;
        public MoexAPIService(IOptions<AppSettings> options)
        {
            _options = options;
        }

        public string GetUrlCompanies()
        {
            return $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.data=on&iss.only=securities&securities.columns=SECID,SHORTNAME";
        }

        public async ValueTask<MoexCompanie> GetMoexCompanies()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(GetUrlCompanies()))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != "{}")
                            {
                                MoexCompanie moexCompanie = JsonConvert.DeserializeObject<MoexCompanie>(data);
                                return moexCompanie;
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
