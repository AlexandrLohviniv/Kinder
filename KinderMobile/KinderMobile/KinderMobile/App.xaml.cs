using KinderMobile.Helpers;
using KinderMobile.PersonalAccountSettings;
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

            if (!string.IsNullOrEmpty(HttpClientImpl.Instance.Token))
                MainPage = new NavPage();
            else
                MainPage = new MainPage.MainPage();

            NavigationDispetcher.Instance.Initialize(MainPage.Navigation);

            // MainPage = new NavPage();

            //MainPage = new AccountSettingsView();
            //MainPage = new EditBioInfo();
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
