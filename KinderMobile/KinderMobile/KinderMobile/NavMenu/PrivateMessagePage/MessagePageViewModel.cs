using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.Popup;
using KinderMobile.PopupYesNo;
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
    class MessagePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<MessageModel> messageList;
        public ObservableCollection<MessageModel> MessagesList
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
        public ICommand deleteMessageCommand { get; set; }



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

        private bool isBottomReached = true;
        public void StickToBottom(bool isBottom)
        {
            isBottomReached = isBottom;
        }

        public MessagePageViewModel(ContactInfoDto otherUser)
        {


            sendMsgCommand = new Command(async () => await sendMsg(otherUser.Id));
            BackToUserListCommand = new Command(async () => await backToUserList());
            deleteMessageCommand = new Command<Guid>(async (param) => await deleteMessage(param));


            ChatService.Instance.RecievePrivateMessage(getMessage, getOwnMessage);
            ChatService.Instance.DeleteMessageResponse(DeleteOtherMessage, DeleteOwnMessage);


            Task.Run(async () => { messageList = await HttpClientImpl.Instance.GetMessageThread(HttpClientImpl.Instance.UserId, otherUser.Id); }).Wait();


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
                bool local_bottomReached = isBottomReached;

                AddMessage(message, false);
                if (local_bottomReached)
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
            MessagesList = new ObservableCollection<MessageModel>(tempList);
        }

        private async Task deleteMessage(Guid messageId)
        {
            await PopupNavigation.Instance.PushAsync(new PopupYesNoView(() => 
            {
                DeletMessage(messageId);
            },
            () => { }, "Are you sure you want to delete?"));
        }


        public void DeleteOtherMessage(int fromId, Guid messageId)
        {
            if (OtherUser.Id == fromId)
            {
                MessageModel model = MessagesList.FirstOrDefault(m => m.MessageId == messageId);

                MessagesList.Remove(model);
            }
        }

        public void DeleteOwnMessage(Guid messageId)
        {
            MessageModel model = MessagesList.FirstOrDefault(m => m.MessageId == messageId);

            MessagesList.Remove(model);
        }


        public void DeletMessage(Guid messageId) 
        {
            Task.Run(async ()=> await ChatService.Instance.DeleteMessage(OtherUser.Id,HttpClientImpl.Instance.UserId, messageId));
        }


        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
