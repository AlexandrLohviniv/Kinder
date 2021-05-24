using KinderMobile.AppFonts;
using KinderMobile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace KinderMobile.NavMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Matches : ContentView
    {
        public Matches()
        {
            InitializeComponent();
            ReturnBackButton.Text = FontIconClass.KeyboardReturn;
            SuperLikeButton.Text = FontIconClass.Star;
            LikeButton.Text = FontIconClass.Heart;
            NopeButton.Text = FontIconClass.ShieldCross;
            AddToTopButton.Text = FontIconClass.LightningBolt;
            AddTabLines();
            AddNameAndAge("Alex", 19);
            AddPreferences();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;
            //tapGestureRecognizer.NumberOfTapsRequired = 2;
            LikeButton.GestureRecognizers.Add(tapGestureRecognizer);
            //Like.GestureRecognizers.Add(tapGestureRecognizer);
        }
        private void AddTabLines()
        {
            int imgCount = 3;
            var width = DeviceDisplay.MainDisplayInfo.Width / imgCount - 10;
            for (int i = 0; i < imgCount; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.WidthRequest = width;
                ellipse.HeightRequest = TabLinesFlexLayout.Height;
                ellipse.BackgroundColor = Color.Gray;
                ellipse.Margin = 3;
                TabLinesFlexLayout.Children.Add(ellipse);
            }
        }
        private void AddNameAndAge(string Name, int Age)
        {
            Label nameLabel = new Label() { Style = (Style)Application.Current.Resources["DescriptionLabel"] };
            nameLabel.Text = Name;
            Label commaLabel = new Label() { Style = (Style)Application.Current.Resources["DescriptionLabel"] };
            commaLabel.Text = ",";
            Label ageLabel = new Label() { Style = (Style)Application.Current.Resources["DescriptionLabel"] };
            ageLabel.Text = Age.ToString();
            ageLabel.Margin = 5;
            AgeNameFlexLayout.Children.Add(nameLabel);
            AgeNameFlexLayout.Children.Add(commaLabel);
            AgeNameFlexLayout.Children.Add(ageLabel);
        }
        private void AddPreferences()
        {
            List<String> preferences = new List<string>();
            preferences.Add("Music");
            preferences.Add("Dancing");
            preferences.Add("Politics");
            preferences.Add("Science");
            foreach (string s in preferences)
            {
                Button button = new Button();
                button.BackgroundColor = Color.DarkGray;
                button.IsEnabled = false;
                button.CornerRadius = 30;
                button.Text = s;
                button.FontSize = 15;
                button.Margin = new Thickness(5, 5);
                button.TextColor = Color.Snow;
                PreferencesFlexLayout.Children.Add(button);
            }
        }
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Like.IsVisible = true;
            Thread.Sleep(1000);
            Like.IsVisible = false;
        }

    }
}