using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Geolocator;
using Plugin.TextToSpeech;
using Tempus.Helpers;
using Tempus.Models;
using Tempus.Services;
using Xamarin.Forms;

namespace Tempus.ViewModels
{
	public class WeatherViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		WeatherService WeatherService { get; } = new WeatherService();

		string _location = Settings.City;

		public string Location
		{
			get => _location;
			set
			{
				_location = value;
				OnPropertyChanged();
				Settings.City = value;
			}
		}

		bool _useGps;

		public bool UseGps
		{
			get => _useGps;
			set
			{
				_useGps = value;
				OnPropertyChanged();
			}
		}


		bool _isImperial = Settings.IsImperial;

		public bool IsImperial
		{
			get => _isImperial;
			set
			{
				_isImperial = value;
				OnPropertyChanged();
				Settings.IsImperial = value;
			}
		}


		string _temp = string.Empty;

		public string Temp
		{
			get => _temp;
			set
			{
				_temp = value;
				OnPropertyChanged();
			}
		}

		string _condition = string.Empty;

		public string Condition
		{
			get => _condition;
			set
			{
				_condition = value;
				OnPropertyChanged();
			}
		}


		bool _isBusy = false;

		public bool IsBusy
		{
			get => _isBusy;
			set
			{
				_isBusy = value;
				OnPropertyChanged();
			}
		}

		WeatherForecastRoot _forecast;

		public WeatherForecastRoot Forecast
		{
			get => _forecast;
			set
			{
				_forecast = value;
				OnPropertyChanged();
			}
		}


		ICommand _getWeather;

		public ICommand GetWeatherCommand =>
			_getWeather ?? (_getWeather = new Command(async () => await ExecuteGetWeatherCommand().ConfigureAwait(false)));

		public async Task ExecuteGetWeatherCommand()
		{
			if (!IsBusy)
			{
				IsBusy = true;
				try
				{
					WeatherRoot weatherRoot;
					var units = IsImperial ? Units.Imperial : Units.Metric;


					if (UseGps)
					{
						var gps = await CrossGeolocator.Current.GetPositionAsync(10000).ConfigureAwait(false);
						weatherRoot = await WeatherService.GetWeather(gps.Latitude, gps.Longitude, units).ConfigureAwait(false);
					}
					else
					{
						//Get weather by city
						weatherRoot = await WeatherService.GetWeather(Location.Trim(), units).ConfigureAwait(false);
					}


					//Get forecast based on cityId
					Forecast = await WeatherService.GetForecast(weatherRoot, units).ConfigureAwait(false);

					var unit = IsImperial ? "F" : "C";
					Temp = $"Temp: {weatherRoot?.MainWeather?.Temperature ?? 0}°{unit}";
					if (weatherRoot != null)
						Condition = $"{weatherRoot.Name}: {weatherRoot.Weather?[0]?.Description ?? string.Empty}";

					CrossTextToSpeech.Current.Speak(Temp + " " + Condition);
				}
				catch (Exception ex)
				{
					Temp = "Unable to get Weather: " + ex.Message;
				}
				finally
				{
					IsBusy = false;
				}
			}
		}

		public void OnPropertyChanged([CallerMemberName] string name = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}