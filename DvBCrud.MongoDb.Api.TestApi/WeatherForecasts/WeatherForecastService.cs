using DvBCrud.MongoDb.Services;

namespace DvBCrud.MongoDb.Api.TestApi.WeatherForecasts;

public class WeatherForecastService : Service<WeatherForecastDataModel, WeatherForecastRepository, WeatherForecastDataModel, WeatherForecastConverter> 
{
    public WeatherForecastService(WeatherForecastRepository repository, WeatherForecastConverter converter) : base(repository, converter)
    {
    }
}