using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models.Requests
{
    /// <summary>
    /// Модель запроса с информацией о котировках
    /// </summary>
    public class QuotationResponse
    {
        [Required]
        public string Ticker { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
