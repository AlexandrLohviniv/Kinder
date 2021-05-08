using System;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;

namespace KinderMobile
{
    
    public partial class NavPage : ContentPage
    {
        public NavPage()
        {
            InitializeComponent();

            PeopleNearbySection.Text = "\U000f034e";
            MatchSection.Text = "\U000f1571";
            MessageSection.Text = "\U000f0369";
            PersonalSection.Text = "\U000f0009";
        }
    }
}