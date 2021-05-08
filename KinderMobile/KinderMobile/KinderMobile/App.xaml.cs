using KinderMobile.Helpers;
using KinderMobile.PersonalAccountSettings;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new MainPage.MainPage();
            // MainPage = new NavPage();

            MainPage = new AccountSettingsView();
            //MainPage = new EditBioInfo();
            NavigationDispetcher.Instance.Initialize(MainPage.Navigation);
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
