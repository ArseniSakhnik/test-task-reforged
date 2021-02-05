using BackendTestTask.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendTestTask.Services.QuotationService
{
    public interface IQuotationService
    {
        Task UpdateQuotations();
        List<QuotationResponse> GetQutationsAndCompanies();

        List<QuotationResponse> GetQuotationsByTickerAndDate(string ticker, DateTime startDate, DateTime endDate);
    }
}
