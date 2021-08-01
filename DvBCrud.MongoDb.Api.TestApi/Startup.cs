using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DStonks.Data.Api.Grpc;
using DStonks.Data.Api.Grpc.Factories;
using DvBCrud.MongoDb.Api.TestApi.WeatherForecasts;
using DvBCrud.MongoDB.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace DvBCrud.MongoDb.Api.TestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            
            services.Configure<MongoSettings>(settings =>
            {
                settings.DatabaseName = Environment.GetEnvironmentVariable(EnvironmentVariables.DbDatabase);
            });
            
            services.AddSingleton<IMongoClient>(c => MongoClientFactory.Build());
            services.AddScoped<WeatherForecastRepository>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DvBCrud.MongoDb.Api.TestApi", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DvBCrud.MongoDb.Api.TestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}