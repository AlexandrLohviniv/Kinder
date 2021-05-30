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
    public partial class PeopleNearBy : ContentView
    {
        private PeopleNearByViewModel viewModel;
        public PeopleNearBy()
        {
            InitializeComponent();
            viewModel = new PeopleNearByViewModel();
            this.BindingContext = viewModel;
        }
        private void InitializeCircles()
        {
            bool makeUpheal = false;
            int count = 0;
            List<UserDto> tempUserList = new List<UserDto>();
            for(int i = 0; i < viewModel.NearByUsers.Count; i++)
            {
                count += 1;
                tempUserList.Add(viewModel.NearByUsers[i]);
                if(count == 4)
                {
                    FlexLayout flexLayout = new FlexLayout();
                    flexLayout.Direction = FlexDirection.Row;
                    if (makeUpheal)
                        flexLayout.JustifyContent = FlexJustify.SpaceAround;
                    else
                        flexLayout.JustifyContent = FlexJustify.SpaceBetween;
                    foreach(UserDto u in tempUserList)
                    {
                        ImageButton img = new ImageButton();
                        img.Source = u.mainPhotoUrl;
                        img.BorderColor = Color.IndianRed;
                        img.CornerRadius = 25;
                        img.HeightRequest = 50;
                        img.WidthRequest = 50;
                        flexLayout.Children.Add(img);
                    }
                    DisplayOtherUsersFlexLayout.Children.Add(flexLayout);
                }
            }
        }
    }
}