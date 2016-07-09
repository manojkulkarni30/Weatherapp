using Refit;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public class WeatherService
    {
        const string WeatherCoordinatesUri = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units={2}&appid={3}";

        public async Task<WeatherRoot> GetWeatherByLatitudeAndLongitudeAsyc(double latitude, double longitude, Units unit = Units.Metric)
        {
            var openWeatherMapApi = RestService.For<IOpenWeatherMapApi>("http://api.openweathermap.org/data/2.5");

            return await openWeatherMapApi.GetWeatherByLatitudeAndLongitude(latitude, longitude, unit.ToString().ToLower());
        }
    }
}
