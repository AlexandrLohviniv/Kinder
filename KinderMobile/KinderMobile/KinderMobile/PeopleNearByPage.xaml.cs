using KinderMobile.AppFonts;
using KinderMobile.DTOs;
using KinderMobile.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TouchEffect;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeopleNearByPage : ContentPage
    {
        private PeopleNearByViewModel viewModel;
        private TapGestureRecognizer gestureRecognizer;
        private TapGestureRecognizer filtersRecognizer;
        private StackLayout _mainLayout;
        private int radius = 0;


        EventHandler filterPageClosingPreessing;

        public PeopleNearByPage()
        {

            filterPageClosingPreessing += (object sender, EventArgs args) =>
              {
                  radius = (int)sender;
                  Task.Run(async()=> await PopupNavigation.Instance.PushAsync(new PopupView("The distance is: " + radius, MessageType.Notification)));
              };

            viewModel = new PeopleNearByViewModel();
            this.BindingContext = viewModel;
            gestureRecognizer = new TapGestureRecognizer();
            gestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "TapCommand");
            filtersRecognizer = new TapGestureRecognizer();
            filtersRecognizer.Tapped += (s, e) => {
                Task.Run( async()=> await PopupNavigation.Instance.PushAsync(new DistanceFilterPage(filterPageClosingPreessing)));
            };

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _mainLayout = new StackLayout();
            _mainLayout.Orientation = StackOrientation.Vertical;
            _mainLayout.Spacing = 0;
            _mainLayout.Children.Add(initializeFiltersButton());            
            InitializeCircles();
            this.Content = _mainLayout;
        }
        private FlexLayout initializeFiltersButton()
        {
            StackLayout container = new StackLayout();
            container.Spacing = 3;
            container.Orientation = StackOrientation.Horizontal;
            Label icon = new Label();
            icon.Text = FontIconClass.BluetoothSettings;
            icon.FontSize = 30;
            Label text = new Label();
            text.Text = "Filters";
            text.FontSize = 30;
            container.Children.Add(icon);
            container.Children.Add(text);
            Frame f = new Frame();
            f.Content = container;
            f.CornerRadius = 30;
            f.BorderColor = Color.Blue;
            f.GestureRecognizers.Add(filtersRecognizer);
            FlexLayout flexLayout = new FlexLayout();
            flexLayout.Children.Add(f);
            flexLayout.Direction = FlexDirection.RowReverse;
            flexLayout.Margin = 5;
            return flexLayout;
        }
        private void InitializeCircles()
        {

            bool makeUpheal = false;
            int count = 0;
            List<UserDto> tempUserList = new List<UserDto>();
            for (int i = 0; i < viewModel.NearByUsers.Count; i++)
            {
                count += 1;
                tempUserList.Add(viewModel.NearByUsers[i]);
                if (makeUpheal && count == 3)
                {
                    count = 0;
                    InitializeFlexLayout(tempUserList, ref makeUpheal);
                    tempUserList = new List<UserDto>();
                }
                if(count == 4)
                {
                    count = 0;
                    InitializeFlexLayout(tempUserList, ref makeUpheal);
                    tempUserList = new List<UserDto>();
                }
            }
            if(count != 0)
            {
                InitializeFlexLayout(tempUserList, ref makeUpheal);
            }
           
        }
        private void InitializeFlexLayout(List<UserDto> users, ref bool makeUpheal)
        {
            FlexLayout flexLayout = new FlexLayout();
            flexLayout.Direction = FlexDirection.Row;
            flexLayout.Padding = 20;
            if (makeUpheal)
            {
                flexLayout.JustifyContent = FlexJustify.SpaceAround;
                makeUpheal = false;
            }
            else
            {
                flexLayout.JustifyContent = FlexJustify.SpaceBetween;
                makeUpheal = true;
            }
            foreach (UserDto u in users)
            {
                ImageButton img = new ImageButton();
                img.Source = u.mainPhotoUrl;
                img.BorderColor = Color.IndianRed;
                img.CornerRadius = 100;
                img.BorderWidth = 3;
                img.HeightRequest = 100;
                img.WidthRequest = 100;
                img.Command = viewModel.TapCommand;
                flexLayout.Children.Add(img);
            }
            _mainLayout.Children.Add(flexLayout);
        }
    }
}