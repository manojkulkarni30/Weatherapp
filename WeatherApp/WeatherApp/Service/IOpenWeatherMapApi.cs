using Refit;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public interface IOpenWeatherMapApi
    {
        [Get("/weather?lat={latitude}&lon={longitude}&units={unit}&appid=<YOUR_KEY>")]
        Task<WeatherRoot> GetWeatherByLatitudeAndLongitude(double latitude, double longitude, string unit);
    }
}
