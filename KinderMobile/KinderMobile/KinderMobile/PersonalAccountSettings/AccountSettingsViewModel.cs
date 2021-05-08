using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.PopupChoice;
using PanCardView.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.PersonalAccountSettings
{

    public class UrlData
    {
        public string Url { get; set; }

        public UrlData(string url)
        {
            this.Url = url;
        }
    }

    public class AccountSettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public UserPreferenceDto userPreferences { get; set; }
        public UserDto User { get; set; }

        private int _currentIndex;


        public ObservableCollection<UrlData> Photos { get; set; } = new ObservableCollection<UrlData>();

        public ICommand PanPositionChangedCommand { get; }

        public ICommand RemoveCurrentItemCommand { get; }
        public ICommand EditBioInfoCommand { get; }
        
        public ICommand MakeChoiceCommandPreference { get; set; }
        public ICommand MakeChoiceCommandPersonal { get; set; }


        private PropertyInfo selectedPropertyInfo;

        string m_BioInfo;
        public string BioInfo
        {
            get
            {
                if (string.IsNullOrEmpty(m_BioInfo))
                {
                    m_BioInfo = "Please, enter something about you";
                }
                return m_BioInfo;
            }
            set
            {
                m_BioInfo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentIndex)));
            }
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentIndex)));
            }
        }

        public bool IsAutoAnimationRunning { get; set; }

        public bool IsUserInteractionRunning { get; set; }

        public AccountSettingsViewModel()
        {
            Photos.Add(new UrlData("me1.jpg"));
            Photos.Add(new UrlData("me2.jpg"));
            Photos.Add(new UrlData("me3.jpg"));

            User = new UserDto()
            {
                AboutMe = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut " +
                "labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                DateOfBith = new DateTime(2002, 7, 31),
                FirstName = "Alex",
                LastSeen = new DateTime(2021, 6, 14),
                Email = "ato31ato@gmail.com",
                LastName = "Vladimirovich",
                mainPhotoUrl = "https://lh3.googleusercontent.com/ogw/ADGmqu8UW-4D9eyYKqKftvx7No75dCHBilOgadmE28VFKw=s32-c-mo",
                NickName = "Flexim",
                Role = Role.SimpleUser,
                Sex = Sexuality.Male
            };

            userPreferences = new UserPreferenceDto()
            {
                BabyRate = Rate.Negative,
                DrinkingRate = Rate.Negative,
                HeightRate = 120,
                PetsRate = Rate.Negative,
                RelationshipRate = Rate.Positive,
                Sex = Sexuality.Male,
                SmokeRate = Rate.Neutral
            };

            PanPositionChangedCommand = new Command(v =>
            {
                if (IsAutoAnimationRunning || IsUserInteractionRunning)
                {
                    return;
                }

                var index = CurrentIndex + (bool.Parse(v.ToString()) ? 1 : -1);
                if (index < 0 || index >= Photos.Count)
                {
                    return;
                }
                CurrentIndex = index;
            });

            RemoveCurrentItemCommand = new Command(() =>
            {
                if (!Photos.Any())
                {
                    return;
                }
                Photos.RemoveAt(CurrentIndex.ToCyclicalIndex(Photos.Count));
            });

            EditBioInfoCommand = new Command(async () => await EditBioInfo());
            MakeChoiceCommandPreference = new Command<string>(async (propName) => await MakeChoice(propName, userPreferences));
            MakeChoiceCommandPersonal = new Command<string>(async (propName) => await MakeChoice(propName, User));
        }

        async Task EditBioInfo()
        {
            await NavigationDispetcher.Instance.Navigation.PushModalAsync(new EditBioInfo());
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
                selectedPropertyInfo.SetValue(model, newValue);

                OnPropertyChanged("userPreferences");
                OnPropertyChanged("User");
            };

            try
            {
                object value = selectedPropertyInfo.GetValue(model);
                PopupChoiceView choiceView = null;
                if (propInfoType == typeof(Rate))
                {
                    Rate rate = (Rate)value;
                    choiceView = PopupChoiceView.CreateChoiceView(rate);
                    choiceView.selected += choiceWrite;

                    await PopupNavigation.Instance.PushAsync(choiceView);


                }
                else if (propInfoType == typeof(Sexuality))
                {
                    Sexuality sexuality = (Sexuality)value;
                    choiceView = PopupChoiceView.CreateChoiceView(sexuality);
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
