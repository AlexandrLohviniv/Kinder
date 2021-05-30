using KinderMobile.DTOs;
using KinderMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

                
                _instance = new CurrentUser(UserDto, UserPreferenceDto, PhotoDtos);



                ChatService.InitializaClient(HttpClientImpl.Instance.Token);
                Task.Run(() => ChatService.Instance.Connect()).Wait();
            }
            catch { }
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


                Task.Run(() => NavigationDispetcher.Instance.Navigation.PopModalAsync(true));
            }
        }

    }
}
