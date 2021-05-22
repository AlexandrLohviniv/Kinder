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

        }

        void ScrollToEndHandler(object obj, EventArgs args) 
        {
            Device.BeginInvokeOnMainThread(() => { MessageList.ScrollTo(obj, ScrollToPosition.End, true); });
        }
    }
}