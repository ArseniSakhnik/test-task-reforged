using BackendTestTask.APIFetchersServices;
using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Services.CompanyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendTestTask.Services.QuotationService
{
    public class QuotationService : IQuotationService
    {
        private DataContext DataContext { get; set; }
        private readonly IAPIFetcherService _aPIFetcherService;
        private readonly ICompanyService _companyService;
        public QuotationService(DataContext dataContext, IAPIFetcherService aPIFetcherService, ICompanyService companyService)
        {
            DataContext = dataContext;
            _aPIFetcherService = aPIFetcherService;
            _companyService = companyService;
        }

        public async Task UpdateQuotations()
        {
            try
            {
                var companies = _companyService.GetCompanies();

                foreach (var c in companies)
                {
                    var quotation = await _aPIFetcherService.GetQuotation(c);
                    if (quotation != null)
                    { 
                        DataContext.Quotations.Add(quotation);
                    }
                }

                DataContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
