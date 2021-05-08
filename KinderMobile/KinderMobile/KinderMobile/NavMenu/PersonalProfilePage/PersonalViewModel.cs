using KinderMobile.DTOs;
using KinderMobile.Popup;
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

        public PersonalViewModel()
        {
            BuySuperlikes = new Command(async () => await GetSuperLikes());
            BuyVipAccount = new Command(async () => await GetVIPAccount());
        }


        async Task GetSuperLikes() 
        {
            await PopupNavigation.Instance.PushAsync(new PopupView("SuperLike purchase is not implemented yet", MessageType.Warning));
        }

        async Task GetVIPAccount()
        {
            await PopupNavigation.Instance.PushAsync(new PopupView("VIP Account purchase is not implemented yet", MessageType.Warning));
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
