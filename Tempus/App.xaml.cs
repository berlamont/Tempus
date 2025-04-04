namespace Tempus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // 			var mdPage = new MasterDetailPage()
			// {
			// 	Title = "Da Weather",
			// 	BindingContext = new WeatherViewModel(),
			// 	Master = new WeatherView(),
			// 	Detail = new NavigationPage(new ForecastView())
			// };

			// MainPage = new NavigationPage(mdPage)
			// {
			// 	BarBackgroundColor = Color.FromHex("3498db"),
			// 	BarTextColor = Color.White
			// };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}