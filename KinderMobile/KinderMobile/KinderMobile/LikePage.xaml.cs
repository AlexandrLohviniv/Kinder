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

        public LikePage()
        {
            InitializeComponent();
            http = DependencyService.Get<IHttpClient>();

            GetAllUsers();

        }

        public async void GetAllUsers()
        {
            List<UserDto> users = await http.getAllUsers();

        }
    }
}