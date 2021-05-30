using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using KinderMobile.DTOs;
using KinderMobile.Popup;
using PanCardView;
using PanCardView.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KinderMobile.NavMenu.MatchPage
{
    public partial class MatchView : ContentView
    {
        FlexLayout container = null;

        public MatchView()
        {
            InitializeComponent();
            BindingContext = new MatchPageViewModel();
            
            CardsView_ItemDisappearing(mainCardView, null);
        }

        private void CardsView_ItemDisappearing(CardsView view, PanCardView.EventArgs.ItemDisappearingEventArgs args)
        {
            if (view.ItemsSource.Count() == 0)
            {
                mainCardViewHolder.Children.Remove(mainCardView);

                Label afterMatchingLbl = new Label()
                {
                    Text = "No matches left, come tommorow!",
                    FontSize = 30,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                int restTimeHoures = CurrentUser.Instance.UserDto.LastSeen.Hour + 24 - DateTime.Now.Hour;

                Label restTimeLbl = new Label()
                {
                    Text = string.Format("The hours till next match: {0}", restTimeHoures),
                    FontSize = 30,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                mainCardViewHolder.Children.Add(afterMatchingLbl);
                mainCardViewHolder.Children.Add(restTimeLbl);

                if (restTimeHoures >= 24)
                {
                    Task.Run(async () =>
                    {
                        await HttpClientImpl.Instance.UpdateLastSeenTime(HttpClientImpl.Instance.UserId);
                        await PopupNavigation.Instance.PushAsync(new PopupView("The next matches will be ready in 24 hours", MessageType.Notification));
                    });
                }
            }
        }

        private void mainCardView_ItemAppeared(CardsView view, PanCardView.EventArgs.ItemAppearedEventArgs args)
        {
            if (args.Index < 0)
                return;

            UserPreferenceDto userPreference = new UserPreferenceDto();
            container.Children.Clear();
            Task.Run(async () =>
            {
                userPreference = await HttpClientImpl.Instance.GetUserPreference((view.ItemsSource.FindValue(args.Index) as UserDto).Id);
                PropertyInfo[] properties = userPreference.GetType().GetProperties();

                foreach (var prop in properties)
                {
                    int value = (int)prop.GetValue(userPreference);
                    if (value == 0)
                    {
                        Frame frame = new Frame()
                        {
                            BackgroundColor = Color.FromHex("#0a8b70"),
                            CornerRadius = 50,
                            IsClippedToBounds = true,
                            Margin = 5

                        };

                        Label label = new Label()
                        {
                            Text = prop.Name,
                            FontSize = 20,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        };

                        frame.Content = label;

                     
                        container.Children.Add(frame);
                    }
                }

            }).Wait();
        }

        private void prefHolder_BindingContextChanged(object sender, EventArgs e)
        {
            container = sender as FlexLayout;
        }
    }
}