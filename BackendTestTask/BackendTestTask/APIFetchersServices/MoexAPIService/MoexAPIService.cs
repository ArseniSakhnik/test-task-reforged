using BackendTestTask.Entities;
using BackendTestTask.Models.Responses;
using BackendTestTask.Services.SourceService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.MoexAPIService
{
    /// <summary>
    /// Класс для получения moex
    /// </summary>
    public class MoexAPIService : IMoexAPIService
    {

        private readonly ISourceService _sourceService;

        public MoexAPIService(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }

        /// <summary>
        /// Возвращает ссылку для подключения к API moex
        /// </summary>
        /// <returns></returns>
        public string GetCopmaniesMoexUrl()
        {
            var source = _sourceService.GetSourceByName("Московская биржа");
            return source.BaseAPIUrl + $"stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.data=on&iss.only=securities&securities.columns=SECID,PREVADMITTEDQUOTE,FACEUNIT,PREVDATE";
        }

        /// <summary>
        /// Получает ответ с сервера moex
        /// </summary>
        /// <returns>Ответ с сервера moex</returns>
        //public async ValueTask<MoexApiResponse> GetMoexCompanies()
        //{
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            using (HttpResponseMessage res = client.GetAsync(GetCopmaniesMoexUrl()).Result)
        //            {
        //                using (HttpContent content = res.Content)
        //                {
        //                    var data = await content.ReadAsStringAsync();

        //                    //MoexApiResponse moexApiResponse = JsonConvert.DeserializeObject<MoexApiResponse>(data);

        //                    JObject parsedObject = JObject.Parse(data);

        //                    var popupjson = parsedObject["securities"]["data[0]"].SelectToken("");

        //                    var moexApiResponse = JsonConvert.DeserializeObject<MoexApiResponse>(popupjson);
                            
        //                    return moexApiResponse;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async ValueTask<MoexApiResponse> GetCompanyProfileByTicker(string ticker)
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

                            JObject parsedObject = JObject.Parse(data);

                            var popupjson = parsedObject.SelectToken($"$.securities.data[?(@[0] == '{ticker}')]").ToString();

                            List<string> propertiesOfObject = JsonConvert.DeserializeObject<List<string>>(popupjson);

                            var moexApiResponse = new MoexApiResponse
                            {
                                Price = double.Parse(propertiesOfObject[1], CultureInfo.InvariantCulture),
                                CurrencyUnit = propertiesOfObject[2],
                                Date = DateTime.Parse(propertiesOfObject[3])
                            };

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
