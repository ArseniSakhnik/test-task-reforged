﻿using BackendTestTask.Helpers;
using BackendTestTask.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices.FinnhubAPIService
{
    /// <summary>
    /// Класс для получения данных с Finnhubapi.io
    /// </summary>
    public class FinnhubAPIService : IFinnhubAPIService
    {
        private readonly IOptions<AppSettings> _options;

        public FinnhubAPIService(IOptions<AppSettings> options)
        {
            _options = options;
        }
        /// <summary>
        /// Возвращает ссылку для получения данных через API 
        /// </summary>
        /// <param name="ticker">Symbol для получения интересующей компании</param>
        /// <returns></returns>
        public string GetCompanyProfileUrl(string ticker)
        {
            return $"https://finnhub.io/api/v1/quote?symbol={ticker}&token={_options.Value.FinnhubKey}";
        }
        /// <summary>
        /// Возвращает объект ответа с сервера FinnhubApi
        /// </summary>
        /// <param name="ticker">Symbol для получения интересующей компании</param>
        /// <returns>Ответ с сервера</returns>

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
