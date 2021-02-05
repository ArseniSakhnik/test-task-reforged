using BackendTestTask.Helpers;
using BackendTestTask.Services.QuotationService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendTestTask.Services.UpdateQuotationService
{
    /// <summary>
    /// Класс для обновления котировок
    /// </summary>
    public class UpdateQuotationService : BackgroundService
    {
        public IServiceProvider Services { get; }
        public IOptions<AppSettings> _options { get; }

        private Timer _timer;
        public UpdateQuotationService(IServiceProvider services, IOptions<AppSettings> options)
        {
            Services = services;
            _options = options;
        }
        /// <summary>
        /// Выполняет работу в асинхронном режиме
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(_options.Value.UpdateMinutes));
        }
        /// <summary>
        /// Обновляет котировки
        /// </summary>
        /// <param name="o"></param>
        private void DoWork(Object o)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IQuotationService>();

                scopedProcessingService.UpdateQuotations();
            }
        }
    }
}
