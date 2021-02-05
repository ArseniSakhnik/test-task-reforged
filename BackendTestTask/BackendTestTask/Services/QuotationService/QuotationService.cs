using BackendTestTask.APIFetchersServices;
using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Models.Responses;
using BackendTestTask.Services.CompanyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
            if (!(DataContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                return;
            }
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

        public List<QuotationResponse> GetQutationsAndCompanies()
        {
            try
            {
                var companies = DataContext.Companies.Include(c => c.Quotations).ToList();

                List<QuotationResponse> quotationResponse = new List<QuotationResponse>();

                foreach (var c in companies.Where(c => c.Quotations.Count > 0))
                { 
                    quotationResponse.Add(new QuotationResponse
                    {
                        CompanyName = c.Name,
                        Ticker = c.Ticker,
                        Price = c.Quotations.Last().Price,
                        CurrencyUnit = c.Quotations.Last().CurrencyUnit,
                        Date = c.Quotations.Last().Date
                    });
                }

                return quotationResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<QuotationResponse> GetQuotationsByTickerAndDate(string ticker, DateTime startDate, DateTime endDate)
        {
            try
            {
                var company = DataContext.Companies.Where(c => c.Ticker == ticker).Include(c => c.Quotations).SingleOrDefault();

                if (company == null)
                {
                    return null;
                }

                List<QuotationResponse> quotationResponse = new List<QuotationResponse>();

                foreach (var c in company.Quotations)
                {
                    if (startDate <= c.Date && c.Date <= endDate)
                    {
                        quotationResponse.Add(new QuotationResponse
                        {
                            CompanyName = company.Name,
                            Ticker = company.Ticker,
                            Price = c.Price,
                            CurrencyUnit = c.CurrencyUnit,
                            Date = c.Date
                        });
                    }
                }

                return quotationResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
