using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace KinderMobile
{
    public partial class MainPage : ContentPage
    {

        IHttpClient http;

        List<WeatherDTO> weatherInfo;
      
        public MainPage()
        {
            InitializeComponent();

            http = DependencyService.Get<IHttpClient>();

            //InitWeatherInfo();

        }

        async void InitWeatherInfo()
        {
            weatherInfo = await http.RetrieveWeatherInfo();
            //WeatherForecast.ItemsSource = weatherInfo;
        }

        private async void ShowPopup(object sender, EventArgs e)
        {
            string mail = mailTxt.Text;
            string pass = PassTxt.Text;

            bool result = await http.authUser(mail, pass);

            if (!result)
                await DisplayAlert("Attention", "Your password or mail is incorrect", "OK");
            else
            {
                await Navigation.PushModalAsync(new NavPage());
                //await DisplayAlert("Well done", "You guessed your password", "OK");
            }
        }
    }
}
