using Tempus.ViewModels;
using Tempus.Views;
using static System.Diagnostics.Debug;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Tempus
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			var mdPage = new MasterDetailPage()
			{
				Title = "Da Weather",
				BindingContext = new WeatherViewModel(),
				Master = new WeatherView(),
				Detail = new NavigationPage(new ForecastView())
			};

			MainPage = new NavigationPage(mdPage)
			{
				BarBackgroundColor = Color.FromHex("3498db"),
				BarTextColor = Color.White
			};
		}

		protected override void OnStart()
		{
			base.OnStart();
			WriteLine("Application OnStart");
		}

		protected override void OnSleep()
		{
			base.OnSleep();
			WriteLine("Application OnSleep");
		}

		protected override void OnResume()
		{
			base.OnResume();
			WriteLine("Application OnResume");
		}
	}
}

