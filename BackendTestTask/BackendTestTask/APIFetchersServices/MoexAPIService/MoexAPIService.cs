using BackendTestTask.Entities;
using BackendTestTask.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.MoexAPIService
{
    public class MoexAPIService : IMoexAPIService
    {
        public string GetCopmaniesMoexUrl()
        {
            return $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.data=on&iss.only=securities&securities.columns=SECID,PREVADMITTEDQUOTE,FACEUNIT,PREVDATE";
        }

        public async ValueTask<MoexApiResponse> GetMoexCompanies()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = client.GetAsync(GetCopmaniesMoexUrl()).Result)
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            MoexApiResponse moexApiResponse = JsonConvert.DeserializeObject<MoexApiResponse>(data);
                            
                            return moexApiResponse;
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
