
using Alcheme.Data.Common.Interfaces;
using Alcheme.Data.Common.Model;
using Alcheme.Data.Common.MongoDb;
using Alcheme.Data.Common.Services;
using Alcheme.WebApi.Contracts;
using Alcheme.WebApi.HealthCheck;
using Alcheme.WebApi.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Alcheme.WebApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);

            services.AddSingleton<IDbClient, DbClient>();
            services.Configure<AlchemeDbConfig>(Configuration.GetSection("AlchemeDatabase"));
            services.AddTransient<IDocumentServices, DocumentServices>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Alcheme.WebApi", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson(son =>
            {
                son.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                son.SerializerSettings.ContractResolver = new SerializeContractResolver<ICollection<object>>();
                son.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                son.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss.fff";
            });

            services.AddApiVersioning(aVer =>
            {
                aVer.DefaultApiVersion = new ApiVersion(2021, 3);
                aVer.AssumeDefaultVersionWhenUnspecified = true;
                aVer.ReportApiVersions = true;
                aVer.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("alcheme-api-Version"), new QueryStringApiVersionReader());
            });

            //Health Check
            services.AddHealthChecks().AddMongoDb(Configuration["AlchemeDatabase:ConnectionString"]);
                 //.AddSqlServer(Configuration["ConnectionStrings:MarketConsole"]);

            services.AddSingleton<IHealthCheckPublisher, HealthReportCachePublisher>();
            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                int period = Configuration.GetValue<int>("HealthCheckCacheFrequencyInSeconds");
                options.Period = TimeSpan.FromSeconds(period > 0 ? period : 30);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alcheme.WebApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = HealthCheckResponseWriter.WriteResponse
            });

            app.UseHealthChecks("/health/latest", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = (context, _) => HealthCheckResponseWriter.WriteResponse(context, HealthReportCachePublisher.Latest)
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
