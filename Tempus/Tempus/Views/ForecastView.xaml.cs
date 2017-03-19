using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tempus.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForecastView : ContentPage
	{
		public ForecastView()
		{
			InitializeComponent();

			if(Device.OS == TargetPlatform.iOS)
				Icon = new FileImageSource{ File = "ic_wb_sunny_yellow_light_24dp.png"};
		}
	}
}
