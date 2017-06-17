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
using WeatherApp.Service;
using Xamarin.Forms;

namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : BaseViewModel
    {
        #region Fields
        private WeatherService weatherService;
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

        string temperature = string.Empty;

        public string Temperature
        {
            get => temperature;
            set => SetProperty(ref temperature, value);
        }

        string condition;

        public string Condition
        {
            get => condition;
            set => SetProperty(ref condition, value);
        }

        string minimumTemperature = string.Empty;

        public string MinimumTemperature
        {
            get => minimumTemperature;
            set => SetProperty(ref minimumTemperature, value);
        }

        string maximumTemperature = string.Empty;

        public string MaximumTemperature
        {
            get => maximumTemperature;
            set => SetProperty(ref maximumTemperature, value);
        }
        #endregion

        #region Command
        ICommand getWeather;
        public ICommand GetWeather => getWeather ?? (getWeather = new Command(async () =>
        {
            await GetWeatherInformationAsync();
        }));
        #endregion

        #region Methods
        private bool IsConnectedToNetWork() => CrossConnectivity.Current.IsConnected;

        private async Task<WeatherRoot> GetWeatherByGeographicCoordinatesAsync()
        {
            var currentLocation = await CrossGeolocator.Current.GetPositionAsync(10000);
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
                if (splitInput != null && splitInput.Any())
                    return Regex.IsMatch(splitInput[0], @"^\d+$");
            }
            return Regex.IsMatch(CityOrZipCode, @"^\d+$");
        }

        async Task GetWeatherInformationAsync()
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
