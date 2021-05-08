using KinderMobile.AppFonts;
using KinderMobile.NavMenu.PersonalProfilePage;
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
    public partial class Personal : ContentView
    {
        public Personal()
        {
            InitializeComponent();

            AccountSettings.Text = "\t" + FontIconClass.AccountSettings;
            PremiumBuyBtn.Text = "\t" + FontIconClass.ArrowUp;
            LikeBuyBtn.Text = "\t" + FontIconClass.ThumbUp;

            this.BindingContext = new PersonalViewModel();
        }
    }
}