using System;
using DvBCrud.MongoDb.Api.TestApiOld.WeatherForecasts;
using DvBCrud.MongoDB.Repositories.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace DvBCrud.MongoDb.Api.TestApiOld
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

            services.AddSingleton<IMongoClient>(c => MongoClientFactory.Create());
            services.AddScoped<IMongoCollectionWrapperFactory, MongoCollectionWrapperFactory>();
            services.AddScoped(x => 
                new WeatherForecastRepository(
                    x.GetRequiredService<IMongoClient>(), 
                    x.GetRequiredService<IMongoCollectionWrapperFactory>(),
                    Environment.GetEnvironmentVariable(EnvironmentVariables.DbDatabase)));
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DvBCrud.MongoDb.Api.TestApiOld", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}