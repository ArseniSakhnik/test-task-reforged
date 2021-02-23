using BackendTestTask.Entities;
using BackendTestTask.Models.Requests;
using BackendTestTask.Services.CompanyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
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
                    return BadRequest("Companies have not been found");
                }
                _logger.LogInformation("Returning a response to a request for get-companies");
                return Ok(companies);
            }
            catch
            {
                _logger.LogInformation("Failed to get-companies");
                return BadRequest("Failed request");
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


            _logger.LogInformation("Starting add-company request");

            if (model.Name.Length == 0 || model.Ticker.Length == 0)
            {
                return BadRequest("Please fill name and ticker fields");
            }

            try
            {
                var company = _companyService.AddCompany(model.Name, model.Ticker);
                if (company != null)
                {
                    _logger.LogWarning("Returning a response to a request for add-company");
                    return Ok(company);
                }
                else
                {
                    _logger.LogWarning("Failed to add-company");
                    return BadRequest("Failed request");
                }
            }
            catch (DuplicateNameException)
            {
                _logger.LogInformation("Failed request");
                return BadRequest("The company with the specified data already exists");
            }
            catch (Exception)
            {
                _logger.LogInformation("Failed request");
                return BadRequest("Failed add company");
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
            _logger.LogInformation("Starting remove-company request");

            if (!(model.Name.Length > 0 && model.Ticker.Length > 0))
            {
                return BadRequest("Please fill name and ticker fields");
            }

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
                    return BadRequest("The company with specified data has not been found");
                }
            }
            catch
            {
                _logger.LogInformation("Failed to remove-company");
                return BadRequest("Failed remove company request");
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
                return BadRequest("Please fill name and ticker fields");
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
                    return BadRequest("Company with specified data has not been found");
                }
            }
            catch (DuplicateNameException)
            {
                _logger.LogInformation("Failed to change-company");
                return BadRequest("Company with specified data has already exists");
            }
            catch
            {
                _logger.LogInformation("Failed to change-company");
                return BadRequest("Failed change company request");
            }
        }
    }
}
