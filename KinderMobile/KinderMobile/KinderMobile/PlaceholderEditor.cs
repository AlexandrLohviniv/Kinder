using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KinderMobile
{
    public class PlaceholderEditor : Editor
    {
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create("Placeholder", typeof(string), typeof(string), "");

        public PlaceholderEditor()
        {
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
    }
}
