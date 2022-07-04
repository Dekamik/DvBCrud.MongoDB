using DvBCrud.MongoDB.API.Controllers;

namespace DvBCrud.MongoDb.Api.TestApi.WeatherForecasts
{
    public class WeatherForecastController : CrudController<WeatherForecast, WeatherForecastRepository>
    {
        public WeatherForecastController(WeatherForecastRepository repository) : base(repository)
        {
        }
    }
}