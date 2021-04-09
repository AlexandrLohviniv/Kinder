using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KinderMobile
{
    public interface IHttpClient
    {
        Task<List<WeatherDTO>> RetrieveWeatherInfo();
        Task<bool> authUser(string mail, string password);
    }
}
