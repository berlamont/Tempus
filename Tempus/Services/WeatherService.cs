using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tempus.Models;
using static Newtonsoft.Json.JsonConvert;

namespace Tempus.Services
{
    public enum Units
    {
        Imperial,
        Metric
    }

    public class WeatherService
    {
        const string WeatherCoordinatesUri = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units={2}&appid=e0b47d14605dc62037471b998977e4c9";
        const string WeatherCityUri = "http://api.openweathermap.org/data/2.5/weather?q={0}&units={1}&appid=e0b47d14605dc62037471b998977e4c9";
        const string ForecaseUri = "http://api.openweathermap.org/data/2.5/forecast?id={0}&units={1}&appid=e0b47d14605dc62037471b998977e4c9";
        const string ForecaseCoordinatesUri = "http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units={2}&appid=e0b47d14605dc62037471b998977e4c9";

        public static async Task<WeatherRoot> GetWeather(double latitude, double longitude, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(WeatherCoordinatesUri, latitude, longitude, units.ToString().ToLower());
                var json = await client.GetStringAsync(url).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherRoot>(json);
            }
        }

        public static async Task<WeatherRoot> GetWeather(string city, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(WeatherCityUri, city, units.ToString().ToLower());
                var json = await client.GetStringAsync(url).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherRoot>(json);
            }
        }

        public Task<WeatherForecastRoot> GetForecast(WeatherRoot weather, Units units = Units.Imperial)
        {
            if (weather.CityId == 0)
                return GetForecast(weather.Coordinates.Latitude, weather.Coordinates.Longitude, units);


            return GetForecast(weather.CityId, units);
        }

        public static async Task<WeatherForecastRoot> GetForecast(int id, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(ForecaseUri, id, units.ToString().ToLower());
                var json = await client.GetStringAsync(url).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherForecastRoot>(json);
            }
        }

        public async Task<WeatherForecastRoot> GetForecast(double lat, double lon, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(ForecaseCoordinatesUri, lat, lon, units.ToString().ToLower());
                var json = await client.GetStringAsync(url).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherForecastRoot>(json);
            }
        }
    }
}
