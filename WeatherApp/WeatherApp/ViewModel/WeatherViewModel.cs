using Plugin.Connectivity;
using Plugin.Geolocator;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Service;
using Xamarin.Forms;

namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherService weatherService;
        public WeatherViewModel()
        {
            weatherService = new WeatherService();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        ICommand getWeather;
        public ICommand GetWeather => getWeather ?? (getWeather = new Command(async () =>
        {
            await GetWeatherInformationAsync();
        }));

        public bool IsConnectedToNetWork() => CrossConnectivity.Current.IsConnected;

        async Task GetWeatherInformationAsync()
        {
            if (IsBusy)
                return;

            if (IsConnectedToNetWork())
            {
                IsBusy = true;
                try
                {
                    var currentLocation = await CrossGeolocator.Current.GetPositionAsync(10000);
                    if (currentLocation != null)
                    {
                        var weather = await weatherService.GetWeatherByLatitudeAndLongitudeAsyc(currentLocation.Latitude, currentLocation.Longitude);
                        Temperature = $"Temp : {weather?.MainWeather?.Temp ?? 0}°C";
                        Condition = $"{weather.CityName}: {weather?.Weather[0]?.Description ?? string.Empty}";
                        MinimumTemperature = $"Min Temp: {weather?.MainWeather?.MinumumTemperature.ToString() ?? string.Empty}°C";
                        MaximumTemperature = $"Max Temp: {weather?.MainWeather?.MaximumTemperature.ToString() ?? string.Empty}°C";
                    }
                    else
                    {
                        Temperature = "Could not get the location";
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

        void OnPropertyChange([CallerMemberName]string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        string temperature = string.Empty;

        public string Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChange();
            }
        }

        string condition;

        public string Condition
        {
            get { return condition; }
            set
            {
                condition = value;
                OnPropertyChange();
            }
        }

        string minimumTemperature = string.Empty;

        public string MinimumTemperature
        {
            get { return minimumTemperature; }
            set
            {
                minimumTemperature = value;
                OnPropertyChange();
            }
        }

        string maximumTemperature = string.Empty;

        public string MaximumTemperature
        {
            get { return maximumTemperature; }
            set
            {
                maximumTemperature = value;
                OnPropertyChange();
            }
        }


        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChange();
            }
        }
    }
}
