using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Profile {
    public class MyProfilePageClientViewModel : ViewModelBase {

        private string profilePicture;
        public string ProfilePicture
        {
            get => profilePicture;
            set
            {
                SetProperty(ref profilePicture, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }
        public FileResult PhotoPath { get; private set; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        public int StartWeight { get; set; }

        private int currentWeight;
        public int CurrentWeight
        {
            get => currentWeight;
            set {
                SetProperty(ref currentWeight, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }
        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }
        private int UserId;
        private int Id;

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand UploadImageCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageClientViewModel() {
            GetUserInfo();

            isEnabled = false;

            SubmitCommand = new AsyncCommand(Submit);
            UploadImageCommand = new AsyncCommand(Upload);
            EditProfileCommand = new AsyncCommand(Edit);
        }

        private async Task GetUserInfo()
        {

            Models.Client user = (Models.Client)accountService.CurrentUser;
            //var userInfo = await networkService.GetAsync<ClientProfileRecieveDTO>(APIConstants.GetClientProfileUri(user.userId));
            HttpResponseMessage profileResponse = await networkService.GetAsync<HttpResponseMessage>(APIConstants.GetClientProfileUri(user.userId));
            await accountService.SetCurrentUser(profileResponse);
            Models.Client userAfter = (Models.Client)accountService.CurrentUser;
            Id = userAfter.id;
            Name = userAfter.name;
            UserId = userAfter.userId;
            ProfilePicture = userAfter.profilePicture;
            StartWeight = userAfter.startWeight;
            CurrentWeight = userAfter.currentWeight;
        }

        private async Task Edit()
        {
            if (!IsEnabled)
            {
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
                GetUserInfo();
            }
                

        }

        private async Task Upload()
        {
            throw new NotImplementedException();
            //var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            //{
            //    Title = "Please Pick a Profile Picture"
            //});
            //if (result != null)
            //{
            //    var stream = await result.OpenReadAsync();
            //    ProfilePicture = ImageSource.FromStream(() => stream);
            //}
            //OnPropertyChanged("ProfilePicture");

        }

        private async Task Submit()
        {
            

            if (
                string.IsNullOrEmpty(Name)
                )
            {
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields", "OK");
                return;
            }
            //default ids inserted for now
            //empty strings for receipe and image link
            //must have receipe and image link filled

            var profile = new ClientProfileDTO(Name, UserId, CurrentWeight, ProfilePicture);
            var test = JsonConvert.SerializeObject(profile);
            if (IsEnabled)
            {
                IsEnabled = false;
                var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.PutProfileUri(UserId), profile);
                var httpCode = result.StatusCode;

                if (httpCode == System.Net.HttpStatusCode.OK)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Profile Saved", "OK");
                    await GetUserInfo();
                }
                else if (httpCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "An error occured on the server. Please try saving again.", "OK");
                }
                else if (httpCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "An error occured on the server. Please try saving again.", "OK");
                }
                else
                    return;
            }
            else
                return;
            
        }
    }
}
