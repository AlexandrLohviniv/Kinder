using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.Popup;
using KinderMobile.PopupChoice;
using KinderMobile.PopupYesNo;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.Registration
{
    public class PreferenceInfoPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public UserPreferenceDto userPreferences { get; set; }

        public ICommand MakeChoiceCommandPreference { get; set; }

        public ICommand GoToBasicSettingsCommand { get; set; }
        public ICommand RegisterUserCommand { get; set; }

        private PropertyInfo selectedPropertyInfo;

        RegisterUserDto UserToRegister { get; set; }


        public PreferenceInfoPageViewModel(RegisterUserDto userToRegister)
        {
            MakeChoiceCommandPreference = new Command<string>(async (propName) => await MakeChoice(propName, userPreferences));

            GoToBasicSettingsCommand = new Command(async () => await GoToBasicSettings());
            RegisterUserCommand = new Command(async () => await RegisterUser());

            userPreferences = new UserPreferenceDto()
            {
                BabyRate = Rate.Negative,
                DrinkingRate = Rate.Negative,
                HeightRate = 125,
                PetsRate = Rate.Negative,
                RelationshipRate = Rate.Negative,
                Sex = Sexuality.NotDefined,
                SmokeRate = Rate.Negative
            };
            UserToRegister = userToRegister;
        }


        async Task MakeChoice<T>(string propName, T model)
        {
            Type _type = model.GetType();

            selectedPropertyInfo = _type.GetProperty(propName);

            Type propInfoType = selectedPropertyInfo.PropertyType;

            EventHandler choiceWrite = (object sender, EventArgs e) =>
            {
                PopupChoiceView resultedView = sender as PopupChoiceView;
                object newValue = resultedView.Result;

                if (selectedPropertyInfo.PropertyType == typeof(int))
                {
                    newValue = Convert.ToInt32(newValue);
                }
                else if (selectedPropertyInfo.PropertyType == typeof(DateTime))
                {
                    newValue = DateTime.Now.AddYears(-Convert.ToInt32(newValue));
                }

                selectedPropertyInfo.SetValue(model, newValue);

                OnPropertyChanged("userPreferences");
                OnPropertyChanged("User");
            };

            try
            {
                object value = selectedPropertyInfo.GetValue(model);
                PopupChoiceView choiceView = null;

                if (propInfoType == typeof(Sexuality))
                {
                    Sexuality sexuality = (Sexuality)value;
                    choiceView = PopupChoiceView.CreateChoiceView(sexuality);
                    choiceView.selected += choiceWrite;

                    await PopupNavigation.Instance.PushAsync(choiceView);
                }
                else if (propInfoType == typeof(Rate))
                {
                    Rate rate = (Rate)value;
                    choiceView = PopupChoiceView.CreateChoiceView(rate);
                    choiceView.selected += choiceWrite;

                    await PopupNavigation.Instance.PushAsync(choiceView);

                }
                else if (propInfoType == typeof(int))
                {
                    int age = (int)value;
                    choiceView = PopupChoiceView.CreateChoiceView(age);
                    choiceView.selected += choiceWrite;

                    await PopupNavigation.Instance.PushAsync(choiceView);
                }
                else if (propInfoType == typeof(DateTime))
                {
                    DateTime birthday = (DateTime)value;
                    choiceView = PopupChoiceView.CreateChoiceView(birthday);
                    choiceView.selected += choiceWrite;

                    await PopupNavigation.Instance.PushAsync(choiceView);
                }
                else if (propInfoType == typeof(string))
                {
                    string name = (string)value;
                    choiceView = PopupChoiceView.CreateChoiceView(name);
                    choiceView.selected += choiceWrite;

                    await PopupNavigation.Instance.PushAsync(choiceView);
                }


            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.InnerException);
            }
        }


        private async Task GoToBasicSettings()
        {
            await PopupNavigation.Instance.PushAsync(new PopupYesNoView(() =>
            {
                Task.Run(async () => { await NavigationDispetcher.Instance.Navigation.PopModalAsync(); });
            }, () => { }, "All your choice will be lost. Are you sure?"));
        }

        private async Task RegisterUser()
        {
            if (UserToRegister.Preferences == null)
                UserToRegister.Preferences = new List<UserPreferenceDto>();

            UserToRegister.Preferences.Add(userPreferences);


            await PopupNavigation.Instance.PushAsync(new PopupYesNoView(() =>
            {
                Task.Run(async () => {
                    bool result = await HttpClientImpl.Instance.RegisterUser(UserToRegister);
                    if (result)
                    {
                        await PopupNavigation.Instance.PushAsync(new PopupView("You are succesfully registered", MessageType.Notification));

                        Page popedPage= await NavigationDispetcher.Instance.Navigation.PopModalAsync();
                        
                        await NavigationDispetcher.Instance.Navigation.PopModalAsync(); //TODO: go to main page
                    }
                    else 
                    {
                        await PopupNavigation.Instance.PushAsync(new PopupView("Something went wrong. Try again later", MessageType.Error));
                    }
                });
            }, 
            () => {}, "Register?"));
        }


        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
