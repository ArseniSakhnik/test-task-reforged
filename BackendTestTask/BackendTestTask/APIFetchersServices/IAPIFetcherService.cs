using BackendTestTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.APIFetchersServices
{
    /// <summary>
    /// Интерфейс для добавления сервиса
    /// </summary>
    public interface IAPIFetcherService
    {
        Task<Quotation> GetQuotation(Company company);
    }
}
