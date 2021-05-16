using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.Popup;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Forms;

namespace KinderMobile.MainPage
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LoginUserDto loginInfo;
        public ICommand UsualLoginCommand { get; set; }
        public ICommand GoogleLoginCommand { get; set; }
        public ICommand FacebookLoginCommand { get; set; }


        //IHttpClient http;


        Account account;

        IFacebookClient _facebookService = CrossFacebookClient.Current;

        [Obsolete]
        AccountStore store;

        [Obsolete]
        public MainPageViewModel()
        {
            loginInfo = new LoginUserDto();
            UsualLoginCommand = new Command(async () => await UsualLogin());
            GoogleLoginCommand = new Command(async () => await GoogleLogin());
            FacebookLoginCommand = new Command(async () => await FacebookLogin());


            //http = DependencyService.Get<IHttpClient>();


            store = AccountStore.Create();

        }

        public string Mail
        {
            get
            {
                return loginInfo.Mail;
            }
            set
            {
                loginInfo.Mail = value;
                OnPropertyChanged("Mail");
            }
        }

        public string Password
        {
            get
            {
                return loginInfo.Password;
            }
            set
            {
                loginInfo.Password = value;
                OnPropertyChanged("Password");
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        async Task UsualLogin()
        {
            string mail = Mail;
            string pass = Password;

            bool result = await HttpClientImpl.Instance.authUser(mail, pass);

            if (!result)
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Your password or email is incorrect", MessageType.Error));
            }
            else
            {
                CurrentUser.InstantiateUser(HttpClientImpl.Instance.UserId);

                await NavigationDispetcher.Instance.Navigation.PushModalAsync(new NavPage());
            }

        }


        [Obsolete]
        async Task GoogleLogin()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    await PopupNavigation.Instance.PushAsync(new PopupView("IOS Google login has not been emplemented yet", MessageType.Warning));
                    break;

                case Device.Android:
                    clientId = AuthConstants.GoogleAndroidClientId;
                    redirectUri = AuthConstants.GoogleAndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(AuthConstants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
               clientId,
               String.Empty,
               AuthConstants.GoogleScope,
               new Uri(AuthConstants.GoogleAuthorizeUrl),
               new Uri(redirectUri),
               new Uri(AuthConstants.GoogleAccessTokenUrl),
               null,
               isUsingNativeUI: true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            OAuthLoginPresenter presenter = new OAuthLoginPresenter();
            presenter.Login(authenticator);


        }

        [Obsolete]
        async Task FacebookLogin()
        {
            try
            {

                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    if (e == null) return;

                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookUserDto>(e.Data));
                            var socialLoginData = new NetworkAuthData
                            {
                                Id = facebookProfile.Id,
                                Email = facebookProfile.Email,
                                Picture = facebookProfile.Picture.Data.Url,
                                Name = $"{facebookProfile.FirstName} {facebookProfile.LastName}",
                            };
                            await PopupNavigation.Instance.PushAsync(new PopupView("Well done", MessageType.Notification));
                            break;
                        case FacebookActionStatus.Canceled:
                            await PopupNavigation.Instance.PushAsync(new PopupView("Canceled", MessageType.Warning));
                            break;
                        case FacebookActionStatus.Error:
                            await PopupNavigation.Instance.PushAsync(new PopupView("Error", MessageType.Error));
                            break;
                        case FacebookActionStatus.Unauthorized:
                            await PopupNavigation.Instance.PushAsync(new PopupView("Incorrect input data", MessageType.Error));
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                };

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "picture", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Something went wrong. Try again", MessageType.Error));
                Debug.WriteLine(ex.ToString());
            }

        }



        [Obsolete]
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }


            if (e.IsAuthenticated)
            {

                GoogleUserDto user = null;


                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(AuthConstants.GoogleUserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<GoogleUserDto>(userJson);
                }

                if (account != null)
                {
                    store.Delete(account, AuthConstants.AppName);
                }

                await store.SaveAsync(account = e.Account, AuthConstants.AppName);

                await PopupNavigation.Instance.PushAsync(new PopupView("Well done", MessageType.Notification));
            }


        }

        [Obsolete]
        async void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            await PopupNavigation.Instance.PushAsync(new PopupView("Pizdez", MessageType.Error));
            Debug.WriteLine("Authentication error: " + e.Message);
        }


    }
}
