using MvvmHelpers;
using Plugin.Connectivity;
using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        #region Fields

        private readonly WeatherService weatherService;

        #endregion

        #region Constructor

        public WeatherViewModel()
        {
            weatherService = new WeatherService();
        }

        #endregion

        #region Properties

        private bool useGps;

        public bool UseGps
        {
            get => useGps;
            set => SetProperty(ref useGps, value);
        }

        private string cityOrZipCode;

        public string CityOrZipCode
        {
            get => cityOrZipCode;
            set => SetProperty(ref cityOrZipCode, value);
        }

        private string temperature = string.Empty;

        public string Temperature
        {
            get => temperature;
            set => SetProperty(ref temperature, value);
        }

        private string condition;

        public string Condition
        {
            get => condition;
            set => SetProperty(ref condition, value);
        }

        private string minimumTemperature = string.Empty;

        public string MinimumTemperature
        {
            get => minimumTemperature;
            set => SetProperty(ref minimumTemperature, value);
        }

        private string maximumTemperature = string.Empty;

        public string MaximumTemperature
        {
            get => maximumTemperature;
            set => SetProperty(ref maximumTemperature, value);
        }

        #endregion

        #region Command

        private ICommand getWeather;
        public ICommand GetWeather => getWeather ?? (getWeather = new Command(async () =>
        {
            await GetWeatherInformationAsync();
        }));

        #endregion

        #region Methods

        private bool IsConnectedToNetWork() => CrossConnectivity.Current.IsConnected;

        private async Task<WeatherRoot> GetWeatherByGeographicCoordinatesAsync()
        {
            var currentLocation = await CrossGeolocator.Current
                .GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            if (currentLocation != null)
            {
                return await weatherService
                    .GetWeatherByLatitudeAndLongitudeAsyc(currentLocation.Latitude, currentLocation.Longitude);
            }
            return null;
        }

        private bool IsZipCode()
        {
            if (CityOrZipCode.Contains(","))
            {
                var splitInput = CityOrZipCode.Split(',');
                if (splitInput?.Any() == true)
                    return Regex.IsMatch(splitInput[0], @"^\d+$");
            }
            return Regex.IsMatch(CityOrZipCode, @"^\d+$");
        }

        private async Task GetWeatherInformationAsync()
        {
            if (IsBusy)
                return;

            WeatherRoot weather = null;
            if (IsConnectedToNetWork())
            {
                IsBusy = true;
                try
                {
                    if (UseGps)
                        weather = await GetWeatherByGeographicCoordinatesAsync();
                    else if (IsZipCode())
                        weather = await weatherService.GetWeatherByZipCodeAsync(CityOrZipCode);
                    else
                        weather = await weatherService.GetWeatherByCityAsync(CityOrZipCode);

                    if (weather != null)
                    {
                        Temperature = $"Temp : {weather.MainWeather?.Temp ?? 0}°C";
                        Condition = $"{weather.CityName}: {weather.Weather[0]?.Description ?? string.Empty}";
                        MinimumTemperature = $"Min Temp: {weather.MainWeather?.MinumumTemperature.ToString() ?? string.Empty}°C";
                        MaximumTemperature = $"Max Temp: {weather.MainWeather?.MaximumTemperature.ToString() ?? string.Empty}°C";
                    }
                    else
                    {
                        Temperature = "Could not get the location";
                        Condition = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    Temperature = "Unable to get the Weather";
                    Condition = string.Empty;
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        #endregion
    }
}
