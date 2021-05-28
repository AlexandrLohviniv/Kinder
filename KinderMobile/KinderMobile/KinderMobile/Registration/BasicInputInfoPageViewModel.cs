using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.Registration
{
    class BasicInputInfoPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GoToMainPageCommand { get; set; }

        public ICommand GoToPreferencePageCommand { get; set; }

        public int SelectedGender { get; set; }

        private RegisterUserDto m_userToRegister;
        public RegisterUserDto UserToRegister
        {
            get
            {
                if (m_userToRegister == null)
                    m_userToRegister = new RegisterUserDto()
                    {
                        Role = Role.SimpleUser
                    };

                return m_userToRegister;
            }
            set
            {
                m_userToRegister = value;
            }
        }


        public bool IsInputValid { get; set; }
        public string SecondPass { get; set; }

        public BasicInputInfoPageViewModel()
        {
            GoToMainPageCommand = new Command(async () => await GoToMainPage());
            GoToPreferencePageCommand = new Command(async () => await GoToPreferencePage());
        }

        public async Task GoToMainPage()
        {
            await NavigationDispetcher.Instance.Navigation.PopModalAsync();
        }

        public async Task GoToPreferencePage()
        {
            if (!IsInputValid || string.IsNullOrEmpty(UserToRegister.AboutMe))
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Enter in all fields valid values", MessageType.Error));
                return;
            }

            if (UserToRegister.Password != SecondPass)
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Second password input is incorrect", MessageType.Error));
                return;
            }

            UserToRegister.Sex = (Sexuality)SelectedGender;


            await NavigationDispetcher.Instance.Navigation.PushModalAsync(new PreferencesPageView(UserToRegister));
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
