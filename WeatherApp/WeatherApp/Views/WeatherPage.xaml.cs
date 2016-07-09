using Xamarin.Forms;

namespace WeatherApp.Views
{
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();
            txtTemperature.FontSize = txtCondition.FontSize=  Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        }
    }
}
