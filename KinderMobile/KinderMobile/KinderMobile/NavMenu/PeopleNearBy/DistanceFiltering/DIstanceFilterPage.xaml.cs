using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile.NavMenu.PeopleNearBy.DistanceFiltering
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DIstanceFilterPage : PopupPage
    {
        public EventHandler onClosing;

        public DIstanceFilterPage(EventHandler m_onClosing)
        {
            InitializeComponent();

            this.onClosing += m_onClosing;

            this.BindingContext = new DIstanceFilterPageViewModel(this);
        }
    }
}