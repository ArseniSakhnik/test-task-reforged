using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Services.CompanyService
{
    /// <summary>
    /// Класс для работы с данными о компании
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly ILogger<CompanyService> _logger;
        private DataContext DataContext { get; set; }
        public CompanyService(DataContext dataContext, ILogger<CompanyService> logger)
        {
            DataContext = dataContext;
            _logger = logger;
        }
        /// <summary>
        /// Возвращает список компании
        /// </summary>
        /// <returns>Список компаний</returns>
        public List<Company> GetCompanies()
        {
            _logger.LogInformation("Get companies from database");
            try
            {
                _logger.LogInformation("Returning companies from database");
                return DataContext.Companies.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An exception was received while working with the database: {ex.Message}");
                throw ex;
            }
        }
        /// <summary>
        /// Добавляет компанию в базу данных
        /// </summary>
        /// <param name="companyName">Название компании</param>
        /// <param name="ticker">Тикер компании</param>
        /// <returns>true, если компания была добавлена или false, если нет</returns>
        public Company AddCompany(string companyName, string ticker)
        {
            try
            {
                if (companyName.Length > 0 && ticker.Length > 0)
                {
                    var company = DataContext.Companies.Where(c => c.Name == companyName && c.Ticker == ticker).SingleOrDefault();

                    if (company != null)
                    {
                        return null;
                    }

                    company = new Company
                    {
                        Name = companyName,
                        Ticker = ticker
                    };

                    DataContext.Companies.Add(company);
                    DataContext.SaveChanges();

                    return company;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Удаляет компанию
        /// </summary>
        /// <param name="id">Id удаляемой компании</param>
        /// <returns>true, если компания была удалена, или false, если нет</returns>
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
        /// <summary>
        /// Изменяет данные о компании
        /// </summary>
        /// <param name="id">Id изменяемой компании</param>
        /// <param name="companyName">Имя компании</param>
        /// <param name="ticker">Тикер компании</param>
        /// <returns></returns>
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
