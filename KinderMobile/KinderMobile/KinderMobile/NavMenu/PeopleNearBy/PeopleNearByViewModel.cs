using KinderMobile.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.NavMenu.PeopleNearBy
{
    public class PeopleNearByViewModel : INotifyPropertyChanged
    {
        private int taps = 0;
        private List<UserDto> nearByUsers;
        private ICommand tapCommand;
        private string labelText = "SomeString";


        public EventHandler filterPageClosingPreessing;
        private int radius = 0;
        private readonly Action drawCircles;

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
        public PeopleNearByViewModel(Action drawCircles)
        {            
            Task.Run(async() => 
            {
                nearByUsers = await HttpClientImpl.Instance.GetUsersByDistance(HttpClientImpl.Instance.UserId);

                foreach (var item in nearByUsers)
                {
                    nearByUsers.Add(item);
                }

            }).Wait();


            filterPageClosingPreessing += (object sender, EventArgs args) =>
            {
                radius = (int)sender * 10000;
                Task.Run(async () => 
                {
                    nearByUsers = await HttpClientImpl.Instance.GetUsersByDistance(HttpClientImpl.Instance.UserId, radius);
                    drawCircles();
                });
            };


            tapCommand = new Command(OnTapped);
            this.drawCircles = drawCircles;
        }
        void OnTapped(object s)
        {
            taps++;
            LabelText = taps.ToString();
        }

        public ICommand TapCommand
        {
            get
            {
                return tapCommand;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
