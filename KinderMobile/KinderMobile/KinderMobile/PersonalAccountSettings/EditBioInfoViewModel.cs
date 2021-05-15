using KinderMobile.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.PersonalAccountSettings
{
    class EditBioInfoViewModel : INotifyPropertyChanged
    {
        private readonly EditBioInfo view;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ApplyCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public String Content { get; set; }

        public EditBioInfoViewModel(string content, EditBioInfo view)
        {
            ApplyCommand = new Command(async () => await applyCommand());
            CancelCommand = new Command(async () => await cancelCommand());

            this.Content = content;
            this.view = view;

            OnPropertyChanged("Content");
        }

        async Task applyCommand() 
        {
            view.InfoUpdated(this, null);
            await NavigationDispetcher.Instance.Navigation.PopModalAsync();
        }

        async Task cancelCommand()
        {
            await NavigationDispetcher.Instance.Navigation.PopModalAsync();
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
