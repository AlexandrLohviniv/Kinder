using KinderMobile.DTOs;
using KinderMobile.Helpers;
using KinderMobile.Popup;
using KinderMobile.PopupChoice;
using PanCardView.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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


        public ObservableCollection<PhotoDto> Photos { get; set; }

        public ICommand PanPositionChangedCommand { get; }

        public ICommand RemoveCurrentItemCommand { get; }
        public ICommand EditBioInfoCommand { get; }

        public ICommand MakeChoiceCommandPreference { get; set; }
        public ICommand MakeChoiceCommandPersonal { get; set; }


        public ICommand UploadImageComand { get; set; }
        public ICommand SetMainPhotoCommand { get; set; }
        public ICommand DeleteCurrentPhotoCommand { get; set; }
        public ICommand UpdateUserInfoCommand { get; set; }

        public ICommand BackButtonCommand { get; set; }

        private PropertyInfo selectedPropertyInfo;

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

            Photos = new ObservableCollection<PhotoDto>(CurrentUser.Instance.PhotoDtos);

            User = CurrentUser.Instance.UserDto;
            User.mainPhotoUrl = User.mainPhotoUrl ?? "defaultUser.jpg";

            userPreferences = CurrentUser.Instance.UserPreferenceDto;

            CurrentUser.Instance.UpdateInfoEvent += (object sender, EventArgs e) =>
             {
                 Photos = new ObservableCollection<PhotoDto>(CurrentUser.Instance.PhotoDtos);
                 User = CurrentUser.Instance.UserDto;
                 User.mainPhotoUrl = User.mainPhotoUrl ?? "defaultUser.jpg";
                 userPreferences = CurrentUser.Instance.UserPreferenceDto;


                 OnPropertyChanged("Photos");
                 OnPropertyChanged("User");
                 OnPropertyChanged("userPreferences");
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
            UploadImageComand = new Command(async () => await UploadImage());
            SetMainPhotoCommand = new Command(async () => await SetMainPhoto());
            DeleteCurrentPhotoCommand = new Command(async () => await DeleteCurrentPhoto());
            UpdateUserInfoCommand = new Command(async () => await UpdateUserInfo());
            BackButtonCommand = new Command(async () => await BackButton());

            
        }

        async Task EditBioInfo()
        {
            EditBioInfo editBio = new EditBioInfo(User.AboutMe);

            editBio.InfoUpdated += (object sender, EventArgs e) =>
            {
                User.AboutMe = (sender as EditBioInfoViewModel).Content;
                OnPropertyChanged("User");
            };

            await NavigationDispetcher.Instance.Navigation.PushModalAsync(editBio);
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


        async Task UploadImage()
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;

            await HttpClientImpl.Instance.UploadPhoto(file, User.Id);
            CurrentUser.Instance.UpdateInfo(HttpClientImpl.Instance.UserId);
        }

        async Task SetMainPhoto()
        {
            bool success = await HttpClientImpl.Instance.ChangeMainPhoto(HttpClientImpl.Instance.UserId, Photos[CurrentIndex].id);
            if (success)
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Main photo is changed", MessageType.Notification));
                CurrentUser.Instance.UpdateInfo(HttpClientImpl.Instance.UserId);
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Something went wrong. Try again", MessageType.Error));
            }
        }


        async Task DeleteCurrentPhoto()
        {
            bool success = await HttpClientImpl.Instance.DeleteCurrentPhoto(HttpClientImpl.Instance.UserId, Photos[CurrentIndex].id);
            if (success)
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Photo is deleted", MessageType.Notification));
                CurrentUser.Instance.UpdateInfo(HttpClientImpl.Instance.UserId);
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Something went wrong. Try again", MessageType.Error));
            }
        }

        async Task UpdateUserInfo()
        {
            bool success = await HttpClientImpl.Instance.UpdateUserInfo(
                HttpClientImpl.Instance.UserId,
                User,
                userPreferences);
            if (success)
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Data is updated", MessageType.Notification));
                CurrentUser.Instance.UpdateInfo(HttpClientImpl.Instance.UserId);
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupView("Something went wrong. Try again", MessageType.Error));
            }
        }

        async Task BackButton() 
        {
            await NavigationDispetcher.Instance.Navigation.PopModalAsync();
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
