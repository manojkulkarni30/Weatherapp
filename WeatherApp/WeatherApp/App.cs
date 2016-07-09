using WeatherApp.ViewModel;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp
{
    public class App : Application
    {
        public App()
        {

            MainPage = new NavigationPage(new WeatherPage()
            {
                BindingContext = new WeatherViewModel()
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
