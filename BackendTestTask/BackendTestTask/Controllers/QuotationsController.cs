using BackendTestTask.Models.Requests;
using BackendTestTask.Services.QuotationService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotationsController : ControllerBase
    {
        private readonly IQuotationService _quotationService;

        public QuotationsController(IQuotationService quotationService)
        {
            _quotationService = quotationService;
        }

        [HttpGet("get-quotations")]
        public IActionResult GetQuotations()
        {
            try
            {
                return Ok(_quotationService.GetQutationsAndCompanies());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get-quotations-by-ticker-and-date")]
        public IActionResult GetQuotationsByTicker([FromBody] QuotationResponse quotationResponse)
        {
            try
            {
                //Я НЕ ЗНАЮ ПОЧЕМУ, НО СЕРВЕР ПЕРЕОБРАЗУЕТ ДАННЫЕ ИЗ JSON С 3Х ЧАСОВЫМ ОПОЗДАНИЕМ TimeZoneInfo.Local = {(UTC+03:00) Москва, Санкт-Петербург}
                DateTime startDate = quotationResponse.StartDate.AddHours(3);
                DateTime endDate = quotationResponse.EndDate.AddHours(3);

                return Ok(_quotationService.GetQuotationsByTickerAndDate(quotationResponse.Ticker, quotationResponse.StartDate, quotationResponse.EndDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
