using BackendTestTask.Entities;
using BackendTestTask.Models.Requests;
using BackendTestTask.Services.CompanyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Controllers
{
    /// <summary>
    /// Контроллер получения данных информации
    /// </summary>
    [Authorize(Roles = Role.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompaniesController> _logger;
        public CompaniesController(ICompanyService companyService, ILogger<CompaniesController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает список компаний из базы данных
        /// </summary>
        /// <returns>Список компаний, если запрос был выполнен, или BadRequest</returns>
        [HttpGet("get-companies")]
        public IActionResult GetCompanies()
        {
            _logger.LogInformation("Starting get-companies request");
            try
            {
                var companies = _companyService.GetCompanies();

                if (companies == null)
                {
                    _logger.LogWarning("Failed to get-companies");
                    return BadRequest(new { message = "Не удалось получить компании" });
                }
                _logger.LogInformation("Returning a response to a request for get-companies");
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Добавляет компанию
        /// </summary>
        /// <param name="model">Запрос с данными компании</param>
        /// <returns>Список компаний, если запрос был выполнен, или BadRequest</returns>
        [HttpPost("add-company")]
        public IActionResult AddCompany([FromBody] CompanyRequestModel model)
        {

            if (!(model.Name.Length > 0 && model.Ticker.Length > 0))
            {
                return BadRequest();
            }

            _logger.LogInformation("Starting add-company request");
            try
            {
                var company = _companyService.AddCompany(model.Name, model.Ticker);
                if (company != null)
                {
                    _logger.LogInformation("Returning a response to a request for add-company");
                    return Ok(company);
                }
                else
                {
                    _logger.LogWarning("Failed to add-company");
                    return BadRequest("Не удалось добавить компанию");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет компанию из базы данных
        /// </summary>
        /// <param name="model">Запрос с данными компании</param>
        /// <returns>Список компаний, если запрос был выполнен, или BadRequest</returns>
        [HttpPost("remove-company")]
        public IActionResult RemoveCompany([FromBody] CompanyRequestModel model)
        {

            if (!(model.Name.Length > 0 && model.Ticker.Length > 0))
            {
                return BadRequest("Invalid input data");
            }

            _logger.LogInformation("Starting remove-company request");
            try
            {
                if (_companyService.RemoveCompany(model.Id))
                {
                    _logger.LogInformation("Returning a response to a request for remove-company");
                    return Ok(model);
                }
                else
                {
                    _logger.LogWarning("Failed to remove-company");
                    return BadRequest("Не удалось удалить запись");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Изменяет компанию в базе данных
        /// </summary>
        /// <param name="model">Запрос с данными компании</param>
        /// <returns>Список компаний, если запрос был выполнен, или BadRequest</returns>
        [HttpPost("change-company")]
        public IActionResult ChangeCompany([FromBody] CompanyRequestModel model)
        {

            if (!(model.Name.Length > 0 && model.Ticker.Length > 0))
            {
                return BadRequest("Invalid input data");
            }
                
            _logger.LogInformation("Starting change-company request");
            try
            {
                if (_companyService.ChangeCompany(model.Id, model.Name, model.Ticker))
                {
                    _logger.LogInformation("Returning a response to a request for change-company");
                    return Ok(model);
                }
                else
                {
                    _logger.LogWarning("Failed to change-company");
                    return BadRequest("Не удалось изменить запись");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
