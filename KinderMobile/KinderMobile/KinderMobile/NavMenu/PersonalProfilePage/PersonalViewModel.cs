using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.PersonalAccountSettings;
using KinderMobile.Popup;
using KinderMobile.PopupYesNo;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.NavMenu.PersonalProfilePage
{

    

    public class PersonalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

       
        public ICommand BuySuperlikes { get; set; }
        public ICommand BuyVipAccount { get; set; }
        public ICommand GoToPersonalPage { get; set; }
        public ICommand LogoutCommand { get; set; }

        public string MainPhotoUrl { get; set; }

        public PersonalViewModel()
        {
            BuySuperlikes = new Command(async () => await GetSuperLikes());
            BuyVipAccount = new Command(async () => await GetVIPAccount());
            GoToPersonalPage = new Command(async () => await GoToPersonalPageCommand());
            
            LogoutCommand = new Command(async () => await Logout());

            MainPhotoUrl = CurrentUser.Instance.UserDto.mainPhotoUrl ?? "defaultUser.jpg";
        }


        async Task GetSuperLikes() 
        {
            await PopupNavigation.Instance.PushAsync(new PopupView("SuperLike purchase is not implemented yet", MessageType.Warning));
        }

        async Task GetVIPAccount()
        {
            await PopupNavigation.Instance.PushAsync(new PopupView("VIP Account purchase is not implemented yet", MessageType.Warning));
        }

        async Task GoToPersonalPageCommand() 
        {
            await NavigationDispetcher.Instance.Navigation.PushModalAsync(new AccountSettingsView());
        }


        async Task Logout() 
        {
            await PopupNavigation.Instance.PushAsync(new PopupYesNoView(Agree,()=> { }, "Are you sure you wnat to logout?"),true);
        }

        void Agree() 
        {
            CurrentUser.Logout();
        }



        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
