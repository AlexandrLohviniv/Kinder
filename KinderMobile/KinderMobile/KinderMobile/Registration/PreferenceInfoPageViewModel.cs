using KinderMobile.DTOs;
using KinderMobile.PopupChoice;
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

        private PropertyInfo selectedPropertyInfo;

        public PreferenceInfoPageViewModel()
        {
            MakeChoiceCommandPreference = new Command<string>(async (propName) => await MakeChoice(propName, userPreferences));

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


        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
