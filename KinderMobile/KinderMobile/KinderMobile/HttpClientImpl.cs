using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.Popup;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace KinderMobile
{
    public class HttpClientImpl
    {

        private const string serverAddr = "192.168.1.104:5000";

        private static HttpClientImpl _instance;

        public static HttpClientImpl Instance =>
                     _instance ?? (_instance = new HttpClientImpl());


        HttpClient client;

        public string m_lastError;
        public string LastError
        {
            get
            {
                return m_lastError;
            }
        }

        private string m_token;
        public string Token
        {
            get
            {
                if (m_token == null)
                {
                    new Task(async () =>
                    {
                        m_token = await SecureStorage.GetAsync("token");
                    }).RunSynchronously();
                }

                return m_token;
            }
            internal set
            {
                new Task(async () =>
                {
                    m_token = value;
                    await SecureStorage.SetAsync("token", m_token);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", m_token);
                }).RunSynchronously();
            }
        }

        private int m_userId;
        public int UserId
        {
            get
            {
                if (m_userId == 0)
                {
                    if (Token != null)
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var tokenS = handler.ReadToken(Token) as JwtSecurityToken;
                        int id = int.Parse(tokenS.Claims.First(claim => claim.Type == "unique_name").Value);
                        m_userId = id;
                    }
                }

                return m_userId;
            }
        }


        public HttpClientImpl()
        {
            client = new HttpClient();

            if (!string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            }

        }


        public void ResetUser() 
        {
            SecureStorage.Remove("token");
            m_userId = 0;
        }

        public async Task<bool> authUser(string mail, string password)
        {
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
                return false;

            LoginUserDto loginDto = new LoginUserDto()
            {
                Mail = mail,
                Password = password
            };

            HttpResponseMessage response = await loginDto.SendModel(client, $"http://{serverAddr}/Login");


            if (response.IsSuccessStatusCode)
            {
                var responsePatter = new { token = "" };

                string result = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeAnonymousType(result, responsePatter);

                Token = apiResponse.token;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                
                return true;
            }

            return false;
        }

        public async Task<List<UserDto>> getAllUsers()
        {
            List<UserDto> output = await client.GetData<List<UserDto>>($"http://{serverAddr}/MatchPage/users");

            return output;
        }

        public async Task UploadPhoto(FileResult file, int userId)
        {
            string url = $"http://{serverAddr}/Photo/{userId}/AddPhoto";

            HttpResponseMessage response = await file.SendMedia(client, url);

            if (response.IsSuccessStatusCode)
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("The photo is successfully loaded", MessageType.Notification));
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("something went wrong. Try again", MessageType.Error));
            }

        }

        public async Task<UserDto> GetUserInfo(int userId)
        {
            string userInfoUrl = $"http://{serverAddr}/User/{userId}/Info";
            string userMainPhotoUrl = $"http://{serverAddr}/Photo/{userId}/getMainPhoto";

            UserDto userInfo = await client.GetData<UserDto>(userInfoUrl);

            PhotoDto mainPhoto = await client.GetData<PhotoDto>(userMainPhotoUrl);

            if(mainPhoto != null)
                userInfo.mainPhotoUrl = mainPhoto.ImgPath;

            return userInfo;
        }

        public async Task<UserPreferenceDto> GetUserPreference(int userId)
        {
            string url = $"http://{serverAddr}/User/{userId}/Preference";

            UserPreferenceDto userPreference = await client.GetData<UserPreferenceDto>(url);

            return userPreference;
        }

        public async Task<List<PhotoDto>> GetUserPhotos(int userId)
        {
            string url = $"http://{serverAddr}/Photo/{userId}/getAllPhotos";

            List<PhotoDto> photos = await client.GetData<List<PhotoDto>>(url);

            if (photos == null)
                photos = new List<PhotoDto>();

            return photos;
        }

        public async Task<bool> ChangeMainPhoto(int userId, int photoId)
        {
            string url = $"http://{serverAddr}/Photo/{userId}/setMain/{photoId}";

            HttpResponseMessage response = await client.PostAsync(url, null);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCurrentPhoto(int userId, int photoId)
        {
            string url = $"http://{serverAddr}/Photo/{userId}/DeletePhoto/{photoId}";

            HttpResponseMessage response = await client.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserInfo(int userId, UserDto user, UserPreferenceDto preferences)
        {

            var sendingContent = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex,
                DateOfBith = user.DateOfBith,
                AboutMe = user.AboutMe,
                NickName = user.NickName,
                Preferences = preferences
            };


            string url = $"http://{serverAddr}/User/{userId}/UpdateInfo";


            HttpResponseMessage response = await sendingContent.SendModel(client, url);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<ContactInfoDto>> GetUsersToMessage(int userId) 
        {
            string url = $"http://{serverAddr}/Like/{userId}/pairs";

            List<ContactInfoDto> result = await client.GetData<List<ContactInfoDto>>(url);

            result = result ?? new List<ContactInfoDto>();

            return result;
        }


        public async Task<ObservableCollection<MessageModel>> GetMessageThread(int userId, int toId) 
        {
            string url = $"http://{serverAddr}/Message/{userId}/messageThread/{toId}";

            List<MessageDto> messages = await client.GetData<List<MessageDto>>(url);

            ObservableCollection<MessageModel> messagesToReturn = new ObservableCollection<MessageModel>();

            messages.ForEach(m =>
            {
                messagesToReturn.Add(new MessageModel() 
                {
                    MessageId = m.MessageId,
                    Message = m.Text,
                    IsOwnerMessage = m.SenderId == userId,
                    IsNotOwnerMessage = m.SenderId == toId
                }); 
            });

            return messagesToReturn;
        }
    }
}
