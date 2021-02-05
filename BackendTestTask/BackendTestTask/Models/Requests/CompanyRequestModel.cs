using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models.Requests
{
    /// <summary>
    /// Модель запроса с данными о компании
    /// </summary>
    public class CompanyRequestModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
    }
}
