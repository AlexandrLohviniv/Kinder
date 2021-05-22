using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.PopupYesNo
{

    public enum ResponseType 
    {
        Yes,
        No
    }

    class PopupYesNoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public ICommand ActionCommand { get; set; }
        public string Message { get; set; }

        private readonly Action PositiveResponse;
        private readonly Action NegativeResponse;

        public PopupYesNoViewModel(Action PositiveAction, Action NegativeAction, string message)
        {
            ActionCommand = new Command<object>( async (param) => await Action(param));

            this.PositiveResponse = PositiveAction;
            this.NegativeResponse = NegativeAction;

            Message = message;
        }


        async Task Action(object response) 
        {
            ResponseType n_response = (ResponseType)int.Parse(response.ToString());

            switch (n_response) 
            {
                case ResponseType.Yes:
                    PositiveResponse();
                    break;
                case ResponseType.No:
                    NegativeResponse();
                    break;
                default:
                    break;
            }

            await PopupNavigation.Instance.PopAsync(true);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
