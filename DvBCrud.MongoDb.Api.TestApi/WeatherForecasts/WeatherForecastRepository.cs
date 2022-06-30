using DvBCrud.MongoDB.Repositories;
using DvBCrud.MongoDB.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DvBCrud.MongoDb.Api.TestApi.WeatherForecasts
{
    public class WeatherForecastRepository : Repository<WeatherForecast>
    {
        public WeatherForecastRepository(IMongoClient client, IOptions<MongoSettings> options) : base(client, options)
        {
        }
    }
}