using BackendTestTask.Models.Requests;
using BackendTestTask.Services.QuotationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Controllers
{
    /// <summary>
    /// Контроллер информации котировок
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class QuotationsController : ControllerBase
    {
        private readonly IQuotationService _quotationService;
        private readonly ILogger<QuotationsController> _logger;

        public QuotationsController(IQuotationService quotationService, ILogger<QuotationsController> logger)
        {
            _quotationService = quotationService;
            _logger = logger;
        }

        /// <summary>
        /// Получает список котировок
        /// </summary>
        /// <returns>Список котировок, если запрос был выполнен, или BadRequest</returns>
        [HttpGet("get-quotations")]
        public IActionResult GetQuotations()
        {
            _logger.LogInformation("Starting get-quotations request");
            try
            {
                _logger.LogInformation("Returning a response to a request for get-quotations");
                return Ok(_quotationService.GetQutationsAndCompanies());
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to get-quotations");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получает список котировок по тикеру
        /// </summary>
        /// <param name="quotationResponse">Модель запроса котировок</param>
        /// <returns>Список котировок, если запрос был выполнен, или BadRequest</returns>
        [HttpPost("get-quotations-by-ticker-and-date")]
        public IActionResult GetQuotationsByTicker([FromBody] QuotationResponse quotationResponse)
        {
            _logger.LogInformation("Starting get-quotations-by-ticker-and-date request");
            try
            {
                _logger.LogInformation("Returning a response to a request for get-quotations-by-ticker-and-date");
                //Я НЕ ЗНАЮ ПОЧЕМУ, НО СЕРВЕР ПЕРЕОБРАЗУЕТ ДАННЫЕ ИЗ JSON С 3Х ЧАСОВЫМ ОПОЗДАНИЕМ ПРИТОМ, ЧТО UTC везде выдает правильный
                DateTime startDate = quotationResponse.StartDate.AddHours(3);
                DateTime endDate = quotationResponse.EndDate.AddHours(3);

                return Ok(_quotationService.GetQuotationsByTickerAndDate(quotationResponse.Ticker, quotationResponse.StartDate, quotationResponse.EndDate));
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to get-quotations-by-ticker-and-date request");
                return BadRequest(ex.Message);
            }
        }

    }
}
