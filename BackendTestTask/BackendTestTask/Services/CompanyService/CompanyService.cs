using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private DataContext DataContext { get; set; }
        public CompanyService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public List<Company> GetCompanies()
        {
            return DataContext.Companies.ToList();
        }

        public bool AddCompany(string name, string ticker)
        {
            var company = DataContext.Companies.Where(c => c.Name == name && c.Ticker == ticker).SingleOrDefault();

            if (company == null)
            {
                return false;
            }

            company = new Company
            {
                Name = name,
                Ticker = ticker
            };

            DataContext.Companies.Add(company);

            DataContext.SaveChanges();

            return true;
        }

        public bool ChangeCompany(int id, string name, string ticker)
        {
            var company = DataContext.Companies.Where(c => c.Id == id).SingleOrDefault();

            if (company == null)
            {
                return false;
            }

            company.Name = name;
            company.Ticker = ticker;

            DataContext.SaveChanges();

            return true;
        }

        public bool RemoveCompany(int id)
        {
            var company = DataContext.Companies.Where(c => c.Id == id).SingleOrDefault();

            if (company == null)
            {
                return false;
            }

            DataContext.Companies.Remove(company);
            DataContext.SaveChanges();

            return true;
        }
    }
}
