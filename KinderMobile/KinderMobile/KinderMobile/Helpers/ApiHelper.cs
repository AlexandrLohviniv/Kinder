using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace KinderMobile.Helpers
{
    public static class ApiHelper
    {
        public static async Task<HttpResponseMessage> SendModel<T>(this T model, HttpClient client, string url)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            string json = JsonConvert.SerializeObject(model, serializerSettings);


            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);

            return response;
        }



        public static async Task<T> GetData<T>(this HttpClient client, string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);

            dynamic output = null;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                output = JsonConvert.DeserializeObject<T>(content);
            }

            return output;
        }


        public static async Task<HttpResponseMessage> SendMedia(this FileResult media,HttpClient client ,string url)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await media.OpenReadAsync()), "file", media.FileName);

            var response = await client.PostAsync(url, content);

            return response;
        }

    }
}
