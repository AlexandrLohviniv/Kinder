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


            InitWeatherInfo();

        }

        async void InitWeatherInfo()
        {
            weatherInfo = await http.RetrieveWeatherInfo();
            WeatherForecast.ItemsSource = weatherInfo;
        }
    }
}
