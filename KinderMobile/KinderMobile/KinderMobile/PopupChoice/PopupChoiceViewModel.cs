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
        private readonly RadioButtonGroupView choiceList;
        private readonly FormView formView;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public ICommand MakeChoiceCommand { get; }
        public ICommand ReadValueCommand { get; }
        
        public PopupChoiceViewModel(PopupChoiceView popupChoice, RadioButtonGroupView choiceList, FormView formView)
        {
            MakeChoiceCommand = new Command(async () => await MakeChoice());
            ReadValueCommand = new Command(async () => await ReadValue());

            this.popupChoice = popupChoice;
            this.choiceList = choiceList;
            this.formView = formView;
        }

        async Task MakeChoice()
        {
            popupChoice.Result = (int)choiceList.SelectedIndex;

            popupChoice.selected.Invoke(popupChoice, null);

            await PopupNavigation.Instance.PopAsync(true);
        }

        async Task ReadValue()
        {
            popupChoice.Result = Convert.ToInt32((formView.Children[0] as AdvancedEntry).Text);

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
