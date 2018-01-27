using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApp.Models
{
    public class Coord
    {
        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }
    }

    public class Weather
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "main")]
        public string Main { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }
    }

    public class Main
    {
        [JsonProperty(PropertyName = "temp")]
        public double Temp { get; set; }
        [JsonProperty(PropertyName = "pressure")]
        public double Pressure { get; set; }
        [JsonProperty(PropertyName = "humidity")]
        public int Humidity { get; set; }
        [JsonProperty(PropertyName = "temp_min")]
        public double MinumumTemperature { get; set; }
        [JsonProperty(PropertyName = "temp_max")]
        public double MaximumTemperature { get; set; }
        [JsonProperty(PropertyName = "sea_level")]
        public double SeaLevel { get; set; }
        [JsonProperty(PropertyName = "grnd_level")]
        public double GroundLevel { get; set; }
    }

    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }
        [JsonProperty(PropertyName = "deg")]
        public double WindDirectionDegrees { get; set; }
    }

    public class Clouds
    {
        [JsonProperty(PropertyName = "all")]
        public int Cloudiness { get; set; }
    }

    public class WeatherRoot
    {
        [JsonProperty(PropertyName = "coord")]
        public Coord Coordinates { get; set; } = new Coord();
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; } = new List<Weather>();
        [JsonProperty("main")]
        public Main MainWeather { get; set; } = new Main();
        [JsonProperty("wind")]
        public Wind Wind { get; set; } = new Wind();
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; } = new Clouds();
        [JsonProperty("id")]
        public int CityId { get; set; }
        [JsonProperty("name")]
        public string CityName { get; set; }
    }
}
