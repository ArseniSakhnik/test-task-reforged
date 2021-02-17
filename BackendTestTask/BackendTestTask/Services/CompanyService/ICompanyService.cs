using BackendTestTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Services.CompanyService
{
    /// <summary>
    /// Интерфейс для добавления сервиса
    /// </summary>
    public interface ICompanyService
    {
        List<Company> GetCompanies();
        Company AddCompany(string companyName, string ticker);
        bool RemoveCompany(int id);
        bool ChangeCompany(int id, string companyName, string ticker);
    }
}
