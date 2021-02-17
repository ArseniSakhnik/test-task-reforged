using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models
{
    /// <summary>
    /// Модель ответа с сервера Finnhub
    /// </summary>
    public class FinnhubApiResponse
    {
        [JsonProperty("c")]
        public double Price { get; set; }
        public string CurrencyUnit { get; set; } = "USD";
    }
}
