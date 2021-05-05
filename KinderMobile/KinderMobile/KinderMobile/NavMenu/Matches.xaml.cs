using KinderMobile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile.NavMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Matches : ContentView
    {

        IHttpClient http;
        List<UserDto> users;
        public Matches()
        {
            InitializeComponent();
            http = DependencyService.Get<IHttpClient>();
            DisplayUserInfo();
            InitializeTaps(PhotoGrid);
        }
        private void InitializeTaps(Grid grid)
        {
            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) => {
                UserNameLabel.Text += "tapped";
            };
            grid.GestureRecognizers.Add(tgr);
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