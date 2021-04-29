using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KinderMobile.Helpers
{
    public class NavigationDispetcher
    {
        private static NavigationDispetcher _instance;

        private INavigation _navigation;

        public static NavigationDispetcher Instance =>
                      _instance ?? (_instance = new NavigationDispetcher());

        public INavigation Navigation =>
                     _navigation ?? throw new Exception("NavigationDispatcher is not initialized");

        public void Initialize(INavigation navigation)
        {
            _navigation = navigation;
        }
    }
}
