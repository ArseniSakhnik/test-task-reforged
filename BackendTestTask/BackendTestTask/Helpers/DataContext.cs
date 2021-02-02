using BackendTestTask.APIFetchersServices.FinnhubAPIService;
using BackendTestTask.APIFetchersServices.MoexAPIService;
using BackendTestTask.Entities;
using BackendTestTask.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Source> Sources { get; set; }
        private IFinnhubAPIService _finnhubAPIService;
        private IMoexAPIService _moexAPIService;

        public DataContext(DbContextOptions<DataContext> options, IFinnhubAPIService finnhubAPIService, IMoexAPIService moexAPIService) : base(options)
        {
            _finnhubAPIService = finnhubAPIService;
            _moexAPIService = moexAPIService;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quotation>()
                .HasOne(q => q.Source)
                .WithMany(s => s.Quotations)
                .HasForeignKey(q => q.SourceId);

            modelBuilder.Entity<Quotation>()
                .HasOne(q => q.Company)
                .WithMany(c => c.Quotations)
                .HasForeignKey(q => q.CompanyId);

            modelBuilder.Entity<Source>().HasData(
                new { Id = 1, Name = "Finnhub", BaseAPIUrl = "https://finnhub.io/api/v1/" },
                new { Id = 2, Name = "Московская биржа", BaseAPIUrl = "https://iss.moex.com/iss/reference/" }
                );

            modelBuilder.Entity<Company>().HasIndex(c => c.Ticker).IsUnique();

            List<object> companies = new List<object>();

            List<string> tickersUsed = new List<string>();

            var companieStockSymbols = _finnhubAPIService.GetCompanies().Result;


            for (int i = 0; i < companieStockSymbols.Count; i++)
            {
                string ticker = companieStockSymbols[i].symbol;
                companies.Add(
                        new
                        {
                            Id = i + 1,
                            Name = companieStockSymbols[i].description,
                            Ticker = ticker
                        }
                    );
                tickersUsed.Add(ticker);
            }

            MoexCompanie moexCompanies = _moexAPIService.GetMoexCompanies().Result;

            int j = companies.Count + 1;

            foreach (var s in moexCompanies.securities.data)
            {
                for (int i = 0; i < s.Count; i += 2, j++)
                {
                    string ticker = s[i];
                    if (!tickersUsed.Contains(ticker))
                    {
                        companies.Add(new { Id = j, Ticker = s[i], Name = s[i + 1] });
                    }
                }
            }

            modelBuilder.Entity<Company>().HasData(companies);



            base.OnModelCreating(modelBuilder);
        }
    }
}
