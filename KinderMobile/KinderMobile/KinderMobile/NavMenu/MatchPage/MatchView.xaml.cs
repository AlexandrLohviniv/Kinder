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
using TouchEffect;
using System.Linq;

namespace KinderMobile.NavMenu.MatchPage
{
    public partial class MatchView : ContentView
    {
        FlexLayout container = null;


        public MatchView()
        {
            InitializeComponent();
            BindingContext = new MatchPageViewModel();
            (BindingContext as MatchPageViewModel).OnUserUpdateing += (object sender, EventArgs e) =>
              {

                  CardsView_ItemDisappearing(mainCardView, null);
                  
                  Task.Run(async () =>
                  {
                      await PopupNavigation.Instance.PushAsync(new PopupView("Updatated", MessageType.Warning));
                  });
              };


            CardsView_ItemDisappearing(mainCardView, null);
        }

        private void CardsView_ItemDisappearing(CardsView view, PanCardView.EventArgs.ItemDisappearingEventArgs args)
        {
            if (view.ItemsSource == null || view.ItemsSource.Count() == 0)
            {
                foreach (var elem in mainCardViewHolder.Children.ToList())
                {
                    mainCardViewHolder.Children.Remove(elem);
                }

                Label afterMatchingLbl = new Label()
                {
                    Text = "No matches left, come tommorow!",
                    FontSize = 30,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                int restTimeHoures = (CurrentUser.Instance.UserDto.LastSeen.AddHours(24) - DateTime.Now).Hours;

                Label restTimeLbl = new Label()
                {
                    Text = string.Format("The hours till next match: {0}", restTimeHoures),
                    FontSize = 30,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

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

                TapGestureRecognizer gestureRecognizer = new TapGestureRecognizer(); ;

                gestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "UpdateUsers");

                buttonRefreshButton.GestureRecognizers.Add(gestureRecognizer);

                Image refreshButton = new Image()
                {
                    Source = "refreshIcon.png",
                    WidthRequest = 100,
                    HeightRequest = 100
                };


                buttonRefreshButton.Content = refreshButton;


                mainCardViewHolder.Children.Add(afterMatchingLbl);
                mainCardViewHolder.Children.Add(restTimeLbl);
                mainCardViewHolder.Children.Add(buttonRefreshButton);

                if (restTimeHoures >= 24)
                {
                    Task.Run(async () =>
                    {
                        await HttpClientImpl.Instance.UpdateLastSeenTime(HttpClientImpl.Instance.UserId);
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