using BackendTestTask.APIFetchersServices.FinnhubAPIService;
using BackendTestTask.APIFetchersServices.MoexAPIService;
using BackendTestTask.Helpers;
using BackendTestTask.Services.CompanyService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = "Server=(localdb)\\mssqllocaldb;Database=test_task;Trusted_Connection=True;";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy.WithOrigins("https://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);



            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IFinnhubAPIService, FinnhubAPIService>();
            services.AddScoped<IMoexAPIService, MoexAPIService>();
            //services.AddScoped<IFinnhubAPIService, FinnhubAPIService>();
            //services.AddScoped<IMoexAPIService, MoexAPIService>();
            //services.AddScoped<IFetchAPIService, FetchAPIService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
