using BackendTestTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Services.CompanyService
{
    public interface ICompanyService
    {
        List<Company> GetCompanies();
        bool AddCompany(string companyName, string ticker);
        bool RemoveCompany(int id);
        bool ChangeCompany(int id, string companyName, string ticker);
    }
}
