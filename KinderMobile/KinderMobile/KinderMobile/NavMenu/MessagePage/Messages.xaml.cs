using KinderMobile.NavMenu.MessagePage;
using PanCardView.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile.NavMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Messages : ContentView
    {
        StackLayout stackLayout = new StackLayout();
        public Messages()
        {
            InitializeComponent();
            this.BindingContext = new ContactToMessageViewModel();

        }

        private void ListView_BindingContextChanged(object sender, EventArgs e)
        {
            ListView messageList = sender as ListView;

            if (messageList.ItemsSource == null || messageList.ItemsSource.Count() == 0)
            {
                foreach (var item in stackLayout.Children.ToList()) 
                {
                    stackLayout.Children.Remove(item);
                }

                Label afterMatchingLbl = new Label()
                {
                    Text = "No Chats yet, wait for like!",
                    FontSize = 30,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                int restTimeHoures = (CurrentUser.Instance.UserDto.LastSeen.AddHours(24) - DateTime.Now).Hours;

                Frame buttonRefreshButton = new Frame()
                {
                    CornerRadius = 50,
                    IsClippedToBounds = true,
                    Padding = 0,
                    Margin = 5,
                    BackgroundColor = Color.FromHex("#4fbe9f"),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };

                TapGestureRecognizer gestureRecognizer = new TapGestureRecognizer(); 


                BindingBase paymentPlanEndingBinding = new Binding("RefreshCommand");

                gestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, paymentPlanEndingBinding);

                buttonRefreshButton.GestureRecognizers.Add(gestureRecognizer);

                Image refreshButton = new Image()
                {
                    Source = "refreshIcon.png",
                    WidthRequest = 100,
                    HeightRequest = 100
                };


                buttonRefreshButton.Content = refreshButton;

                stackLayout.Children.Add(afterMatchingLbl);
                stackLayout.Children.Add(buttonRefreshButton);

                MessagePage.Content = stackLayout;
            }
        }
    }
}