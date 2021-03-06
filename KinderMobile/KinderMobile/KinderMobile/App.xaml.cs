using KinderMobile.Helpers;
using KinderMobile.NavMenu.MatchPage;
using KinderMobile.PersonalAccountSettings;
using KinderMobile.Registration;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage.MainPage();

            NavigationDispetcher.Instance.Initialize(MainPage.Navigation);
            if (!string.IsNullOrEmpty(HttpClientImpl.Instance.Token))
            {
                CurrentUser.InstantiateUser(HttpClientImpl.Instance.UserId);
                if (CurrentUser.Instance != null)
                {
                    Task.Run(() => NavigationDispetcher.Instance.Navigation.PushModalAsync(new NavPage())).Wait();
                }

            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
