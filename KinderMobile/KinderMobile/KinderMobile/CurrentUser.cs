using KinderMobile.DTOs;
using KinderMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using Plugin.Geolocator;

namespace KinderMobile
{
    public class CurrentUser
    {
        private static CurrentUser _instance;
        public static CurrentUser Instance
        {
            get
            {
                return _instance;
            }
        }

        public UserDto UserDto { get; internal set; }
        public UserPreferenceDto UserPreferenceDto { get; internal set; }
        public List<PhotoDto> PhotoDtos { get; internal set; }

        public EventHandler UpdateInfoEvent;

        public static void InstantiateUser(int userId)
        {
            try
            {
                UserDto UserDto = null;
                UserPreferenceDto UserPreferenceDto = null;
                List<PhotoDto> PhotoDtos = null;


                Task.Run(async () => { UserDto = await HttpClientImpl.Instance.GetUserInfo(userId); }).Wait();

                Task.Run(async () => { UserPreferenceDto = await HttpClientImpl.Instance.GetUserPreference(userId); }).Wait();

                Task.Run(async () => { PhotoDtos = await HttpClientImpl.Instance.GetUserPhotos(userId); }).Wait();

                //Task.Run(async () =>
                //{

                //    var locator = CrossGeolocator.Current;
                //    locator.DesiredAccuracy = 100;

                //    var location = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(1000));


                //    //var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                //    //var location = await Geolocation.GetLocationAsync(request);

                //    string latitude = location.Latitude.ToString();
                //    string longtitude = location.Longitude.ToString();

                //    string valueToUpdate = string.Format("{0},{1}", latitude, longtitude);

                //    await HttpClientImpl.Instance.SendGeolocation(UserDto.Id, valueToUpdate);

                //});


                _instance = new CurrentUser(UserDto, UserPreferenceDto, PhotoDtos);



                ChatService.InitializaClient(HttpClientImpl.Instance.Token);
                Task.Run(() => ChatService.Instance.Connect()).Wait();
            }
            catch (Exception e){
                Debug.Write(e.Message);
            }
        }

        public CurrentUser(UserDto UserDto, UserPreferenceDto UserPreferenceDto, List<PhotoDto> PhotoDtos)
        {
            this.UserDto = UserDto;
            this.UserPreferenceDto = UserPreferenceDto;
            this.PhotoDtos = PhotoDtos;
        }

        public void UpdateInfo(int userId)
        {
            Task.Run(async () => { UserDto = await HttpClientImpl.Instance.GetUserInfo(userId); }).Wait();

            Task.Run(async () => { UserPreferenceDto = await HttpClientImpl.Instance.GetUserPreference(userId); }).Wait();

            Task.Run(async () => { PhotoDtos = await HttpClientImpl.Instance.GetUserPhotos(userId); }).Wait();

            UpdateInfoEvent(this, null);
        }


        public static void Logout() 
        {
            if (_instance != null) 
            {
                HttpClientImpl.Instance.ResetUser();
                Task.Run(() => ChatService.Instance.Disconnect()).Wait();


                Task.Run(async () => 
                {
                    Page popedPage = await NavigationDispetcher.Instance.Navigation.PopModalAsync();
                    while (popedPage.GetType() != typeof(KinderMobile.MainPage.MainPage))
                    {
                        popedPage = await NavigationDispetcher.Instance.Navigation.PopModalAsync();
                    }
                });
            }
        }

    }
}
