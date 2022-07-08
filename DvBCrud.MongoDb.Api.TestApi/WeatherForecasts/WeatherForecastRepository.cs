using DvBCrud.MongoDB.Repositories;
using DvBCrud.MongoDB.Repositories.Wrappers;
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