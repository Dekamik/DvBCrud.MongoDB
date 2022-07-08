using DvBCrud.MongoDb.Repositories;
using DvBCrud.MongoDb.Repositories.Wrappers;
using MongoDB.Driver;

namespace DvBCrud.MongoDb.Api.TestApi.WeatherForecasts
{
    public class WeatherForecastRepository : Repository<WeatherForecastDataModel>
    {
        public WeatherForecastRepository(IMongoCollectionWrapperFactory factory) : base(factory)
        {
        }
    }
}