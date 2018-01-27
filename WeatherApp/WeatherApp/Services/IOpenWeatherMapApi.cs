using Refit;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IOpenWeatherMapApi
    {
        [Get("/weather?lat={latitude}&lon={longitude}&units={unit}&appid={appId}")]
        Task<WeatherRoot> GetWeatherByLatitudeAndLongitudeAsync(double latitude, double longitude, string unit, string appId);

        [Get("/weather?q={city}&units={unit}&appid={appId}")]
        Task<WeatherRoot> GetWeatherByCityAsync(string city, string unit, string appId);

        [Get("/weather?zip={zipCode}&units={unit}&appid={appId}")]
        Task<WeatherRoot> GetWeatherByZipCodeAsync(string zipCode, string unit, string appId);
    }
}
