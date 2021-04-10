using KinderMobile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LikePage : ContentPage
    {
        IHttpClient http;
        List<UserDto> users;

        public LikePage()
        {
            InitializeComponent();
            http = DependencyService.Get<IHttpClient>();
            DisplayUserInfo();
        }
        private async void DisplayUserInfo()
        {
            users = await http.getAllUsers();            
            UserDto user = users.FirstOrDefault();
            AgeLabel.Text = (DateTime.Now - user.DateOfBith).ToString();
            UserNameLabel.Text = user.FirstName + " " + user.LastName;
        }
    }
}