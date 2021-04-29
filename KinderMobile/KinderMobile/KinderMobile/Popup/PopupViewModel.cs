using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.Popup
{
    class PopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public ICommand CloseCommand { get; set; }

        private readonly MessageType messageType;

        private string message;

        public string Message
        {
            get { return message; }
            set 
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }


        private Color backGroundColor;
        public Color BackGroundColor 
        {
            get { return backGroundColor; }
            set 
            {
                backGroundColor = value;
                OnPropertyChanged("BackGroundColor");
            }
        }

        public PopupViewModel(string message, MessageType messageType)
        {
            this.messageType = messageType;
            this.message = message;

            CloseCommand = new Command(async () => await CloseNotificationAsync());

            switch (messageType) 
            {
                case MessageType.Error:
                    BackGroundColor = Color.Red;
                    break;
                case MessageType.Notification:
                    BackGroundColor = Color.Gray;
                    break;
                case MessageType.Warning:
                    BackGroundColor = Color.Yellow;
                    break;
                default:
                    BackGroundColor = Color.Transparent;
                    break;

            }
            
        }


        async Task CloseNotificationAsync() 
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
