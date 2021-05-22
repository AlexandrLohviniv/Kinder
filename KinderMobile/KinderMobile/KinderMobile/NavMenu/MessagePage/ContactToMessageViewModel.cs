using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.NavMenu.PrivateMessagePage;
using KinderMobile.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.NavMenu.MessagePage
{

    

    public class ContactToMessageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GoToPrivateMessageCommand { get; set; }

        private IEnumerable<ContactInfoDto> contactInfos;
        public IEnumerable<ContactInfoDto> ContactInfos 
        {
            get 
            {
                return contactInfos;
            }
            set 
            {
                contactInfos = value;
                OnPropertyChanged("ContactInfos");
            }
        }


        public ContactToMessageViewModel()
        {


            Task.Run(async ()=> { ContactInfos = await HttpClientImpl.Instance.GetUsersToMessage(HttpClientImpl.Instance.UserId); }).Wait();

            GoToPrivateMessageCommand = new Command<ContactInfoDto>((param) => GoToPrivateMessage(param));
        }


       
        void GoToPrivateMessage(ContactInfoDto anotherUser) 
        {
            NavigationDispetcher.Instance.Navigation.PushModalAsync(new PrivateMessageView(anotherUser));
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
