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
    public partial class WeatherView : ContentPage
    {
        public WeatherView()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.iOS)
                Icon = new FileImageSource { File = "ic_cloud_blue_grey_300_24p.png" };
        }
    }
}
