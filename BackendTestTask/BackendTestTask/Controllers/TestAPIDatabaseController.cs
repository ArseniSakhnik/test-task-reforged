using BackendTestTask.APIFetchersServices.FinnhubAPIService;
using BackendTestTask.Helpers;
using BackendTestTask.Services.CompanyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestAPIDatabaseController : ControllerBase
    {

        private readonly ILogger<TestAPIDatabaseController> _logger;
        private readonly IFinnhubAPIService _finnhubAPIService;
        private readonly ICompanyService _companyService;

        public TestAPIDatabaseController(ILogger<TestAPIDatabaseController> logger, IFinnhubAPIService finnhubAPIService, ICompanyService companyService)
        {
            _logger = logger;
            _finnhubAPIService = finnhubAPIService;
            _companyService = companyService;
        }

        //[HttpGet]
        //public IEnumerable<Object> Get()
        //{

        //    //var rng = new Random();
        //    //return Enumerable.Range(1, 5).Select(index => new 
        //    //{
        //    //    Date = DateTime.Now.AddDays(index),
        //    //    TemperatureC = rng.Next(-20, 55),
        //    //    Summary = Summaries[rng.Next(Summaries.Length)]
        //    //})
        //    //.ToArray();

        //    //return CompanyService.GetCompanies();
        //}

        [HttpGet]
        public object Get()
        {
            //var quoteTask = FetchAPIService.AddCompany("a", "a");
            //return quoteTask;
            //var quoteTask = _finnhubAPIService.GetCompanies().Result;
            var quoteTask = _companyService.GetCompanies();
            return true;
            
        }
    }
}
