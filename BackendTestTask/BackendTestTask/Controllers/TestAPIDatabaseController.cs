﻿using BackendTestTask.APIFetchersServices;
using BackendTestTask.APIFetchersServices.FinnhubAPIService;
using BackendTestTask.APIFetchersServices.MoexAPIService;
using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Services.CompanyService;
using BackendTestTask.Services.QuotationService;
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
        private readonly IMoexAPIService _moexAPIService;
        private readonly IFinnhubAPIService _finnhubAPIService;
        private readonly IAPIFetcherService _aPIFetcherService;
        private readonly IQuotationService _quotationService;

        public TestAPIDatabaseController(ILogger<TestAPIDatabaseController> logger, 
            ICompanyService companyService, 
            IMoexAPIService moexAPIService, 
            IFinnhubAPIService finnhubAPIService, 
            IAPIFetcherService aPIFetcherService,
            IQuotationService quotationService)
        {
            _logger = logger;
            _companyService = companyService;
            _moexAPIService = moexAPIService;
            _finnhubAPIService = finnhubAPIService;
            _aPIFetcherService = aPIFetcherService;
            _quotationService = quotationService;
        }

        [HttpGet("get")]
        [AllowAnonymous]
        public object Get()
        {
            //var quoteTask = FetchAPIService.AddCompany("a", "a");
            //return quoteTask;
            //var quoteTask = _finnhubAPIService.GetCompanies().Result;
            //var quoteTask = _companyService.GetCompanies();

            

            var test = _quotationService.GetQutationsAndCompanies();
            
            return test;
            
        }
    }
}
