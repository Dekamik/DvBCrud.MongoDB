using DvBCrud.MongoDB.API.Controllers;

namespace DvBCrud.MongoDb.Api.TestApi.WeatherForecasts
{
    public class WeatherForecastController : CrudController<WeatherForecastDataModel, WeatherForecastService>
    {
        public WeatherForecastController(WeatherForecastService service) : base(service)
        {
        }
    }
}