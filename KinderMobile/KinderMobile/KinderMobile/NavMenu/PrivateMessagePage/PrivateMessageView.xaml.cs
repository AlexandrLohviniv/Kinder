using KinderMobile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile.NavMenu.PrivateMessagePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivateMessageView : ContentPage
    {
        public PrivateMessageView(ContactInfoDto otherUser)
        {
            InitializeComponent();
            this.BindingContext = new MessagePageViewModel(otherUser);

            (this.BindingContext as MessagePageViewModel).ScrollToEnd += ScrollToEndHandler;

            if ((this.BindingContext as MessagePageViewModel).MessagesList.Count() > 0) 
            {
                ScrollToEndHandler((this.BindingContext as MessagePageViewModel).MessagesList.LastOrDefault(), null);
            }


        }

        void ScrollToEndHandler(object obj, EventArgs args) 
        {
            Device.BeginInvokeOnMainThread(() => { MessageList.ScrollTo(obj, ScrollToPosition.End, false); });
        }

        private void MessageList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (e.Item == (this.BindingContext as MessagePageViewModel).MessagesList.Last())
            {
                (this.BindingContext as MessagePageViewModel).StickToBottom(true);
            }
            else
                (this.BindingContext as MessagePageViewModel).StickToBottom(false);

        }
    }
}