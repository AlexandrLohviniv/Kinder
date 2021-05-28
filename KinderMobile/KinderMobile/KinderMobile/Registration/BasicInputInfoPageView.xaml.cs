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
    public partial class BasicInputInfoPageView : ContentPage
    {
        public BasicInputInfoPageView()
        {
            InitializeComponent();
            this.BindingContext = new BasicInputInfoPageViewModel();
        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            password2.IsPassword = password1.IsPassword = (sender as Plugin.InputKit.Shared.Controls.CheckBox).IsChecked;
        }
    }
}