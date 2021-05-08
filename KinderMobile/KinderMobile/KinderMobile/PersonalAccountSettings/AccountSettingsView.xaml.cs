using KinderMobile.AppFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile.PersonalAccountSettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountSettingsView : ContentPage
    {
        

        public AccountSettingsView()
        {
            InitializeComponent();

            this.BindingContext = new AccountSettingsViewModel();

            MainBtn.Text = "\t" + FontIconClass.CheckUnderlineCircle;
            DeleteBtn.Text = "\t" + FontIconClass.Delete;
        }
    }
}