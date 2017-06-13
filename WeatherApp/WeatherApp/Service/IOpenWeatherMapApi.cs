using Refit;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public interface IOpenWeatherMapApi
    {
        [Get("/weather?lat={latitude}&lon={longitude}&units={unit}&appid=fc9f6c524fc093759cd28d41fda89a1b")]
        Task<WeatherRoot> GetWeatherByLatitudeAndLongitude(double latitude, double longitude, string unit);
    }
}
