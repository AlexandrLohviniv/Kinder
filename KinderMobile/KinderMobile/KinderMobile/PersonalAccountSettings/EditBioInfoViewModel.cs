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

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ApplyCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditBioInfoViewModel()
        {
            ApplyCommand = new Command(async () => await applyCommand());
            CancelCommand = new Command(async () => await cancelCommand());
        }

        async Task applyCommand() 
        {
            await NavigationDispetcher.Instance.Navigation.PopModalAsync();
        }

        async Task cancelCommand()
        {
            await NavigationDispetcher.Instance.Navigation.PopModalAsync();
        }

    }
}
