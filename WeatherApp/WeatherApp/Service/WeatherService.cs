using Refit;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public class WeatherService
    {
        const string ApiBaseUri = "http://api.openweathermap.org/data/2.5";
        const string AppId = "";
        private readonly IOpenWeatherMapApi openWeatherMapApi;

        public WeatherService()
        {
            openWeatherMapApi = RestService.For<IOpenWeatherMapApi>(ApiBaseUri);
        }

        public async Task<WeatherRoot> GetWeatherByLatitudeAndLongitudeAsyc(double latitude, double longitude, Units unit = Units.Metric)
        {
            return await openWeatherMapApi.GetWeatherByLatitudeAndLongitudeAsync(latitude, longitude, unit.ToString().ToLower(), AppId);
        }

        public async Task<WeatherRoot> GetWeatherByCityAsync(string city, Units unit = Units.Metric)
        {
            return await openWeatherMapApi.GetWeatherByCityAsync(city, unit.ToString().ToLower(), AppId);
        }

        public async Task<WeatherRoot> GetWeatherByZipCodeAsync(string zipCode, Units unit = Units.Metric)
        {
            return await openWeatherMapApi.GetWeatherByZipCodeAsync(zipCode, unit.ToString().ToLower(), AppId);
        }
    }
}
