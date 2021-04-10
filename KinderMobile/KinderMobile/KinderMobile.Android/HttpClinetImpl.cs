using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KinderMobile.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        public async Task<bool> authUser(string mail, string password)
        {
            LoginUserDto loginDto = new LoginUserDto()
            {
                Mail = mail,
                Password = password
            };

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            string json = JsonConvert.SerializeObject(loginDto, serializerSettings);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await clinet.PostAsync("http://10.0.2.2:5000/Login", content);

            if (response.IsSuccessStatusCode) 
            {
            //string result = await response.Content.ReadAsStringAsync(); 
            //TODO: save jwt token
                return true;
            }

            return false;
        }

        public async Task<List<UserDto>> getAllUsers()
        {
            HttpResponseMessage response = await clinet.GetAsync("http://10.0.2.2:5000/MatchPage/users");

            List<UserDto> output = new List<UserDto>();

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                output = JsonConvert.DeserializeObject<List<UserDto>>(content);
            }

            return output;
        }
    }
}