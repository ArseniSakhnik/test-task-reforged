using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models.Responses
{
    ///<summary>
    ///Модель ответа с сервера Moex
    ///</summary>
    public class MoexApiResponse
    {
        public double Price { get; set; }
        public string CurrencyUnit { get; set; }
        public DateTime Date { get; set; }
    }
}
