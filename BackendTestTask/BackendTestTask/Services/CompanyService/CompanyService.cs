using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                return DataContext.Companies.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddCompany(string companyName, string ticker)
        {
            try
            {
                if (companyName.Length > 0 && ticker.Length > 0)
                {
                    var company = DataContext.Companies.Where(c => c.Name == companyName && c.Ticker == ticker).SingleOrDefault();

                    if (company != null)
                    {
                        return false;
                    }

                    company = new Company
                    {
                        Name = companyName,
                        Ticker = ticker
                    };

                    DataContext.Companies.Add(company);
                    DataContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveCompany(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangeCompany(int id, string companyName, string ticker)
        {
            try
            {
                var company = DataContext.Companies.Where(c => c.Id == id).SingleOrDefault();

                if (company == null)
                {
                    return false;
                }

                company.Name = companyName;
                company.Ticker = ticker;

                DataContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
