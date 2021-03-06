﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models.Responses
{
    /// <summary>
    /// Модель ответа для использования в клиентской части
    /// </summary>
    public class QuotationResponse
    {
        public string CompanyName { get; set; }
        public string Ticker { get; set; }
        public double Price { get; set; }
        public string CurrencyUnit { get; set; }
        public DateTime Date { get; set; }

    }
}
