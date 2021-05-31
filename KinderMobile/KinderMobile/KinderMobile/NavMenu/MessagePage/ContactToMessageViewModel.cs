using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.NavMenu.PrivateMessagePage;
using KinderMobile.Popup;
using KinderMobile.PopupYesNo;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        public ICommand DeleteChatCommand { get; set; }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    ContactInfos = await HttpClientImpl.Instance.GetUsersToMessage(HttpClientImpl.Instance.UserId);
                    await PopupNavigation.Instance.PushAsync(new PopupView("Updated", MessageType.Notification));
                    IsRefreshing = false;
                });
            }
        }

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
            Task.Run(async () => { ContactInfos = await HttpClientImpl.Instance.GetUsersToMessage(HttpClientImpl.Instance.UserId); }).Wait();

            GoToPrivateMessageCommand = new Command<ContactInfoDto>((param) => GoToPrivateMessage(param));

            DeleteChatCommand = new Command<int>(async (param) => await DeleteChat(param));
        }



        void GoToPrivateMessage(ContactInfoDto anotherUser)
        {
            NavigationDispetcher.Instance.Navigation.PushModalAsync(new PrivateMessageView(anotherUser));
        }

        async Task DeleteChat(int userId)
        {
            await PopupNavigation.Instance.PushAsync(new PopupYesNoView(() =>
            {
                ContactInfos = ContactInfos.Where(u => u.Id != userId);
                var modified = ContactInfos.Where(u => u.Id == userId).ToList();

                Task.Run(async () =>
                {
                    await HttpClientImpl.Instance.GetBackLike(HttpClientImpl.Instance.UserId, userId);
                    contactInfos = modified;
                });

            }, () => { }, "Are uou sure you want to delete?"));


        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
