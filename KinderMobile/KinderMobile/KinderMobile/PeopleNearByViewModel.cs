using KinderMobile.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace KinderMobile
{
    public class PeopleNearByViewModel : INotifyPropertyChanged
    {
        private List<UserDto> nearByUsers;
        public List<UserDto> NearByUsers
        {
            get => nearByUsers;
        }
        public PeopleNearByViewModel()
        {
            Task.Run(async () =>
            {
                nearByUsers = await HttpClientImpl.Instance.getAllUsers();
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
