﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(KinderMobile.Droid.HttpClinetImpl))]
namespace KinderMobile.Droid
{
    public class HttpClinetImpl : IHttpClient
    {

        HttpClient clinet;

        public HttpClinetImpl()
        {
            clinet = new HttpClient();
        }

        public async Task<List<WeatherDTO>> RetrieveWeatherInfo()
        {
            HttpResponseMessage response = await clinet.GetAsync("http://10.0.2.2:5000/WeatherForecast");

            List<WeatherDTO> output = new List<WeatherDTO>();

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                output = JsonConvert.DeserializeObject<List<WeatherDTO>>(content);
            }

            return output;
        }
    }
}