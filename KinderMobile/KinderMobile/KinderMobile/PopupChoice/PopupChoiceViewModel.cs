using Plugin.InputKit.Shared.Controls;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.PopupChoice
{
    class PopupChoiceViewModel:INotifyPropertyChanged
    {
        private readonly PopupChoiceView popupChoice;

        public int selectedRadioButton { get; set; }
        public string InputedText { get; set; }
        public int Years { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public ICommand MakeChoiceCommand { get; }
        public ICommand ReadStringCommand { get; }
        public ICommand ReadYearCommand { get; }

        public PopupChoiceViewModel(PopupChoiceView popupChoice)
        {
            MakeChoiceCommand = new Command(async () => await MakeChoice(selectedRadioButton));
            ReadStringCommand = new Command(async () => await MakeChoice(InputedText));
            ReadYearCommand = new Command(async () => await MakeChoice(Years));

            this.popupChoice = popupChoice;
        }

        async Task MakeChoice(dynamic value)
        {
            popupChoice.Result = value;

            popupChoice.selected.Invoke(popupChoice, null);

            await PopupNavigation.Instance.PopAsync(true);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
