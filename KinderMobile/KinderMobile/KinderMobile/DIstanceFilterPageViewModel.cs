using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile
{
    class DIstanceFilterPageViewModel : INotifyPropertyChanged
    {
        private readonly DistanceFilterPage page;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Distance { get; set; }
        public ICommand GetDistance { get; }


        public DIstanceFilterPageViewModel(DistanceFilterPage page)
        {
            GetDistance = new Command(async () => { await ReadDistance(Distance); });
            this.page = page;
        }

        async Task ReadDistance(int value) 
        {
            page.onClosing.Invoke(Distance, null);

            await PopupNavigation.Instance.PopAsync(true);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
