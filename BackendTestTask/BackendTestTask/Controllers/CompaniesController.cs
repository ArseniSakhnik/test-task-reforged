using BackendTestTask.Entities;
using BackendTestTask.Models.Requests;
using BackendTestTask.Services.CompanyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("get-companies")]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _companyService.GetCompanies();

                if (companies == null)
                {
                    return BadRequest(new { message = "Не удалось получить компании" });
                }

                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-company")]
        public IActionResult AddCompany([FromBody] CompanyRequestModel model)
        {
            try
            {
                if (_companyService.AddCompany(model.Name, model.Ticker))
                {
                    return Ok(_companyService.GetCompanies());
                }
                else
                {
                    return BadRequest("Не удалось добавить компанию");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("remove-company")]
        public IActionResult RemoveCompany([FromBody] CompanyRequestModel model)
        {
            try
            {
                if (_companyService.RemoveCompany(model.Id))
                {
                    return Ok(_companyService.GetCompanies());
                }
                else
                {
                    return BadRequest("Не удалось удалить запись");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("change-company")]
        public IActionResult ChangeCompany([FromBody] CompanyRequestModel model)
        {
            try
            {
                if (_companyService.ChangeCompany(model.Id, model.Name, model.Ticker))
                {
                    return Ok(_companyService.GetCompanies());
                }
                else
                {
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
