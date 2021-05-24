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
    public partial class EditBioInfo : ContentPage
    {

        public EventHandler InfoUpdated;

        public EditBioInfo(string currentInfo)
        {
            InitializeComponent();
            this.BindingContext = new EditBioInfoViewModel(currentInfo, this);
        }
    }
}