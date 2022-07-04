using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.API.Controllers;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDb.Api.TestApi.WeatherForecasts
{
    public class WeatherForecastController : CrudController<WeatherForecast, WeatherForecastRepository>
    {
        public WeatherForecastController(WeatherForecastRepository repository) : base(repository)
        {
        }
    }
}