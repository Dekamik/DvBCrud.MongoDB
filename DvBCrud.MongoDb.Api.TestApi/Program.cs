using DvBCrud.MongoDb.Api.TestApi;
using DvBCrud.MongoDb.Api.TestApi.WeatherForecasts;
using DvBCrud.MongoDb.Helpers;
using DvBCrud.MongoDb.Repositories.Wrappers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();

var (client, databaseName) = MongoClientFactory.Create(EnvironmentVariables.DbConnectionString ?? throw new InvalidOperationException());
builder.Services.AddSingleton<IMongoClient>(c => client);
builder.Services.AddScoped<IMongoCollectionWrapperFactory>(x => 
    new MongoCollectionWrapperFactory(
        x.GetRequiredService<IMongoClient>(), 
        databaseName));
builder.Services.AddScoped<WeatherForecastRepository>();
builder.Services.AddScoped<WeatherForecastConverter>();
builder.Services.AddScoped<WeatherForecastService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
