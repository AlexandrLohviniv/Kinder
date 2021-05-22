using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.Popup;
using KinderMobile.Templates;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.NavMenu.PrivateMessagePage
{

    internal class MessageModel
    {
        public string Message { get; set; }
        public bool IsOwnerMessage { get; set; }
        public bool IsNotOwnerMessage { get; set; }
    }

    class MessagePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IEnumerable<MessageModel> messageList;
        public IEnumerable<MessageModel> MessagesList
        {
            get => messageList;
            set
            {
                messageList = value;
                OnPropertyChanged("MessagesList");
            }
        }


        public EventHandler ScrollToEnd;

        public ICommand BackToUserListCommand { get; set; }
        public ICommand sendMsgCommand { get; set; }

        private string m_myMessage;


        private ContactInfoDto m_otherUser;
        public ContactInfoDto OtherUser
        {
            get
            {
                return m_otherUser;
            }
            set
            {
                m_otherUser = value;
                OnPropertyChanged("OtherUser");
            }
        }

        public string MyMessage
        {
            get { return m_myMessage; }
            set
            {
                m_myMessage = value;
                OnPropertyChanged("MyMessage");
            }
        }

        public MessagePageViewModel(ContactInfoDto otherUser)
        {
            sendMsgCommand = new Command(async () => await sendMsg(otherUser.Id));
            BackToUserListCommand = new Command(async () => await backToUserList());

            ChatService.Instance.RecievePrivateMessage(getMessage, getOwnMessage);

            messageList = new List<MessageModel>();
            this.OtherUser = otherUser;
        }

        public async Task sendMsg(int toId)
        {
            if (!string.IsNullOrEmpty(m_myMessage))
            {
                try
                {
                    await ChatService.Instance.SendPrivateMessage(toId, HttpClientImpl.Instance.UserId, m_myMessage);
                    MyMessage = "";
                }
                catch
                {
                    await PopupNavigation.Instance.PushAsync(new PopupView("Something went wrong. Try again later", MessageType.Error));
                }
            }
        }


        public async Task backToUserList()
        {
            await NavigationDispetcher.Instance.Navigation.PopModalAsync();
        }

        public void getOwnMessage(string message)
        {
            AddMessage(message, true);
            ScrollToEnd(MessagesList.LastOrDefault(), null);
        }


        public void getMessage(int fromId, string message)
        {
            if (OtherUser.Id == fromId)
            {
                AddMessage(message, false);
                ScrollToEnd(MessagesList.LastOrDefault(), null);
            }
            else
            {
                //TODO: Notidy that another person send message 
            }

        }

        private void AddMessage(string message, bool isOwner)
        {
            var tempList = MessagesList.ToList();
            tempList.Add(new MessageModel { IsOwnerMessage = isOwner, Message = message, IsNotOwnerMessage = !isOwner });
            MessagesList = new List<MessageModel>(tempList);



        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
