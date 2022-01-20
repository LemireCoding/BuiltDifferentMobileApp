using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Profile {
    public class MyProfilePageCoachViewModel : ViewModelBase {

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

        private string type;
        public string Type
        {
            get => type;
            set
            {
                SetProperty(ref type, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private bool isAvailable;
        public bool IsAvailable
        {
            get => isAvailable;
            set
            {
                SetProperty(ref isAvailable, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private bool offersMeal;
        public bool OffersMeal
        {
            get => offersMeal;
            set
            {
                SetProperty(ref offersMeal, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private bool offersWorkout;
        public bool OffersWorkout
        {
            get => offersWorkout;
            set
            {
                SetProperty(ref offersWorkout, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private int certificationId;
        public int CertificationId
        {
            get => certificationId;
            set
            {
                SetProperty(ref certificationId, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                SetProperty(ref gender, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private bool isVerified;
        public bool IsVerified
        {
            get => isVerified;
            set
            {
                SetProperty(ref isVerified, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                SetProperty(ref description, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private double pricing;
        public double Pricing
        {
            get => pricing;
            set
            {
                SetProperty(ref pricing, value);
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

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand UploadImageCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageCoachViewModel() {

            GetUserInfo();

            isEnabled = false;

            SubmitCommand = new AsyncCommand(Submit);
            UploadImageCommand = new AsyncCommand(Upload);
            EditProfileCommand = new AsyncCommand(Edit);
        }

        private async Task GetUserInfo()
        {
            Models.Coach user = (Models.Coach)accountService.CurrentUser;
            var userInfo = await networkService.GetAsync<Models.Coach>(APIConstants.GetProfileUri());
            accountService.CurrentUser = userInfo;

            ProfilePicture = userInfo.profilePicture;
            Name = userInfo.name;
            UserId = userInfo.userId;
            Type = userInfo.type;
            IsAvailable = userInfo.isAvailable;
            OffersMeal = userInfo.offersMeal;
            OffersWorkout = userInfo.offersWorkout;
            CertificationId = userInfo.certificationId;
            Gender = userInfo.gender;
            IsVerified = userInfo.isVerified;
            Description = userInfo.description;
            Pricing = userInfo.pricing;
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
            //This has been left commented in order to facilitate profilePicture Upload

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

            var profile = new CoachProfileDTO(Name, UserId, Type, IsAvailable, OffersMeal, OffersWorkout, CertificationId, Gender, IsVerified, Description, Pricing, ProfilePicture);
            if (IsEnabled)
            {
                IsEnabled = false;
                var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.PutProfileUri(), profile);
                var httpCode = result.StatusCode;

                if (httpCode == System.Net.HttpStatusCode.OK)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Profile Saved", "OK");
                    await GetUserInfo();
                }
                else if (httpCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "An error occurred on the server. Please try saving again.", "OK");
                }
                else if (httpCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "An error occurred on the server. Please try saving again.", "OK");
                }
                else
                    return;
            }
            else
                return;
        }
    }
   
}
