using System.Collections.Generic;
using Monova.Web.Models;

namespace Monova.Web.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
