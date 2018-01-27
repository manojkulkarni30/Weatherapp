using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            txtTemperature.FontSize = txtCondition.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        }
	}
}
