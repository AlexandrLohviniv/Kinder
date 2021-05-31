using KinderMobile.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile
{
    public class PeopleNearByViewModel : INotifyPropertyChanged
    {
        private int taps = 0;
        private List<UserDto> nearByUsers;
        private ICommand tapCommand;
        private string labelText = "SomeString";

        public string LabelText
        {
            get => labelText;
            set
            {
                labelText = value;
                OnPropertyChanged("LabelText");
            }
        }
        public List<UserDto> NearByUsers
        {
            get => nearByUsers;
        }
        public PeopleNearByViewModel()
        {
            nearByUsers = new List<UserDto>();
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"

            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            nearByUsers.Add(new UserDto()
            {
                mainPhotoUrl = "defaultUser.png"
            });
            tapCommand = new Command(OnTapped);

        }
        void OnTapped(object s)
        {
            taps++;
            LabelText = taps.ToString();
        }
 
        public ICommand TapCommand
        {
            get { return tapCommand; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
