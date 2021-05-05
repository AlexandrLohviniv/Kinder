using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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


        IHttpClient http;

        public MainPageViewModel()
        {
            loginInfo = new LoginUserDto();
            UsualLoginCommand = new Command(async () => await UsualLogin());
            GoogleLoginCommand = new Command(async () => await GoogleLogin());
            FacebookLoginCommand = new Command(async () => await FacebookLogin());


            http = DependencyService.Get<IHttpClient>();

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
            //string mail = mailTxt.Text;
            //string pass = PassTxt.Text;

            //bool result = await http.authUser(mail, pass);

            //if (!result)
            //    await DisplayAlert("Attention", "Your password or mail is incorrect", "OK");
            //else
            //{
            //    await Navigation.PushModalAsync(new NavPage());

            //}

            await PopupNavigation.Instance.PushAsync(new PopupView());
        }

        async Task GoogleLogin()
        {
            await PopupNavigation.Instance.PushAsync(new PopupView());
        }

        async Task FacebookLogin()
        {
            await PopupNavigation.Instance.PushAsync(new PopupView());
        }

    }
}
