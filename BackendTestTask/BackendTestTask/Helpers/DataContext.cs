using BackendTestTask.Entities;
using BackendTestTask.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace BackendTestTask.Helpers
{
    /// <summary>
    /// Класс для соединения с базой данных
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<User> Users { get; set; }
        private ILogger<DataContext> _logger;

        public DataContext(DbContextOptions<DataContext> options, ILogger<DataContext> logger) : base(options)
        {
            _logger = logger;
            ///Общее, Пункт 4. Наверное не лучшее решение. Исключение появляется когда бэк и фронт работаю не через корс. Не знаю почему.   
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"The following exception was thrown during database creation: {ex.Message}");
            }
        }
        /// <summary>
        /// Определяет ограничения бд и задает первоначальные данные
        /// </summary>
        /// <param name="modelBuilder"></param>
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

            modelBuilder.Entity<Company>().HasIndex(c => c.Ticker).IsUnique();

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<Source>().HasData(
                    new { Id = 1, Name = "Finnhub", BaseAPIUrl = "https://finnhub.io/api/v1/" },
                    new { Id = 2, Name = "Московская биржа", BaseAPIUrl = "https://iss.moex.com/iss/engines/" }
                );

            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Username = "admin", Password = "admin", Role = Role.Admin },
                    new User { Id = 2, Username = "user", Password = "user", Role = Role.User }
                );


            modelBuilder.Entity<Company>().HasData(
                    new Company { Id = 1, Name = "Сбербанк России", Ticker = "SBER" },
                    new Company { Id = 2, Name = "Газпром", Ticker = "GAZP" },
                    new Company { Id = 3, Name = "Совкомфлот", Ticker = "FLOT" },
                    new Company { Id = 4, Name = "Tesla Inc", Ticker = "TSLA" },
                    new Company { Id = 5, Name = "Apple Inc", Ticker = "AAPL" },
                    new Company { Id = 6, Name = "Amazon.com Inc", Ticker = "AMZN" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
