using BackendTestTask.Helpers;
using BackendTestTask.Services.CompanyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Controllers
{
    [ApiController]
    [Authorize]
    [EnableCors("ClientPermission")]
    [Route("[controller]")]
    public class TestAPIDatabaseController : ControllerBase
    {

        private readonly ILogger<TestAPIDatabaseController> _logger;
        private readonly ICompanyService _companyService;

        public TestAPIDatabaseController(ILogger<TestAPIDatabaseController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet("get")]
        public object Get()
        {
            //var quoteTask = FetchAPIService.AddCompany("a", "a");
            //return quoteTask;
            //var quoteTask = _finnhubAPIService.GetCompanies().Result;
            //var quoteTask = _companyService.GetCompanies();
            return true;
            
        }
    }
}
