using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Services.SourceService
{
    public class SourceService : ISourceService
    {
        private DataContext DataContext { get; set; }
        private readonly ILogger<SourceService> _logger;

        public SourceService(DataContext dataContext, ILogger<SourceService> logger)
        {
            DataContext = dataContext;
            _logger = logger;
        }

        public Source GetSourceByName(string name)
        {
            _logger.LogInformation("Get source by username from database");
            try
            {
                var source = DataContext.Sources.Where(s => s.Name == name).SingleOrDefault();
                _logger.LogInformation("Return source");
                return source;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"An exception was received while working with the database: {ex.Message}");
                throw ex;
            }
        }
    }
}
