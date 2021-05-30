using KinderMobile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreferencesPageView : ContentPage
    {
        public PreferencesPageView(RegisterUserDto registerUserDto)
        {
            InitializeComponent();

            this.BindingContext = new PreferenceInfoPageViewModel(registerUserDto);
        }
    }
}