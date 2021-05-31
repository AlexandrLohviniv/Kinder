using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DistanceFilterPage : PopupPage
    {
        private int radius;
        private TapGestureRecognizer getBackRecognizer;
        public DistanceFilterPage(ref int radius)
        {
            this.radius = radius;
            getBackRecognizer = new TapGestureRecognizer();
            getBackRecognizer.Tapped += async (s, e) => {
                await Navigation.PopModalAsync();
            };
            Label displayLabel = new Label
            {
                Text = String.Format("{0} Km", radius),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 50,
                FontFamily = "Comic Sans MS",
                TextColor = Color.LightBlue,
                FontAttributes = FontAttributes.Bold
            };
            Slider slider = new Slider
            {
                Maximum = 1000,
                Value = radius,
                ThumbColor = Color.LightBlue,
                MaximumTrackColor = Color.Blue,
                MinimumTrackColor = Color.Blue,
                HeightRequest = 20,
                WidthRequest = 190,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            slider.ValueChanged += (sender, args) =>
            {
                displayLabel.Text = String.Format("{0} Km", args.NewValue);
                this.radius = 0;
            };

            Frame buttonFrame = new Frame();
            buttonFrame.BorderColor = Color.LightBlue;
            buttonFrame.HeightRequest = 30;
            buttonFrame.Content = new Label()
            {
                Text = "Back",
                FontSize = 30,
                FontFamily = "Comic Sans MS",
            };
            buttonFrame.GestureRecognizers.Add(getBackRecognizer);
            StackLayout stackLayout = new StackLayout()
            {
                Children = { displayLabel, slider, buttonFrame },
                Margin = new Thickness(150, 200),
                WidthRequest = 250,
                BackgroundColor = Color.Snow,
                Spacing = 10
            };
            //frame.BackgroundColor 
            Content = stackLayout;
        }
    }
}