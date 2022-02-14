using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Profile
{
    public class MyProfilePageCoachViewModel : ViewModelBase
    {

        private FileResult profilePicture;
        public FileResult ProfilePicture
        {
            get => profilePicture;
            set
            {
                SetProperty(ref profilePicture, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

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

        private int profilePictureId;
        public int ProfilePictureId
        {
            get => profilePictureId;
            set => SetProperty(ref profilePictureId, value);
        }

        private string payPalLink;
        public string PayPalLink
        {
            get => payPalLink;
            set
            {
                SetProperty(ref payPalLink, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        public object PreviewPicture { get; set; }

        private int UserId;

        public ObservableRangeCollection<string> TypesOfServices { get; set; }
        public ObservableRangeCollection<string> GenderTypes { get; set; }

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand UploadImageCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageCoachViewModel()
        {

            Name = "";
            Type = "";
            IsAvailable = false;
            OffersMeal = false;
            OffersWorkout = false;
            CertificationId = 0;
            Gender = "";
            IsVerified = false;
            Description = "";
            Pricing = 0.0;
            ProfilePictureId = 0;
            PayPalLink = "";

            PreviewPicture = null;
            ProfilePicture = null;

            GetUserInfo();

            IsEnabled = false;

            SubmitCommand = new AsyncCommand(Submit);
            UploadImageCommand = new AsyncCommand(Upload);
            EditProfileCommand = new AsyncCommand(Edit);

            TypesOfServices = new ObservableRangeCollection<string>
        {
            "Meals Only",
            "Workouts Only",
            "Both"
        };
            GenderTypes = new ObservableRangeCollection<string>
            {
                "Male",
                "Female",
                "Other"
            };
        }

        private async Task GetUserInfo()
        {
            Models.Coach user = (Models.Coach)accountService.CurrentUser;
            var userInfo = await networkService.GetAsync<Models.Coach>(APIConstants.GetProfileUri());
            if (userInfo == null)
            {
                await Application.Current.MainPage.DisplayAlert("Could not load client's profile!", "Returning to previous page", "OK");
                await Shell.Current.GoToAsync("..");
                IsBusy = false;
                return;
            }
            else
            {
                accountService.CurrentUser = userInfo;

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
                PayPalLink = userInfo.payPalLink;


                var pic = await networkService.GetStreamAsync(APIConstants.GetProfilePictureUri());
                if (pic == null)
                {
                    return;
                }
                PreviewPicture = ImageSource.FromStream(() => pic);
                OnPropertyChanged("PreviewPicture");
            }
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
            var options = new MediaPickerOptions
            {
                Title = "Please select your Profile Picture"
            };

            try
            {
                var picture = await MediaPicker.PickPhotoAsync(options);

                if (picture != null && picture.ContentType == "image/gif" || picture.ContentType == "image/png" || picture.ContentType == "image/jpeg" || picture.ContentType == "image/bmp" || picture.ContentType == "image/jpg")
                {
                    var stream = await picture.OpenReadAsync();

                    PreviewPicture = ImageSource.FromStream(() => stream);
                    OnPropertyChanged("PreviewPicture");

                    ProfilePicture = picture;
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Image format", "Please select a valid image. (jpg|jpeg|png|gif|bmp)", "OK");
            }
            catch (Exception)
            {
                return;
            }
        }

        private async Task<MultipartFormDataContent> GetMultiPartFormContent()
        {
            try
            {
                MultipartFormDataContent formContent = new MultipartFormDataContent();

                // Read picture contents & set MIME appopriately, image/*
                StreamContent pictureContent = new StreamContent(await ProfilePicture.OpenReadAsync());
                pictureContent.Headers.ContentType = new MediaTypeHeaderValue(ProfilePicture.ContentType);

                // Add it to the form content as "profilePicture"
                formContent.Add(pictureContent, "profilePicture", Guid.NewGuid().ToString());

                return formContent;
            }
            catch
            {
                return null;
            }
        }

        private async Task Submit()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields", "OK");
                return;
            }

            if (IsEnabled)
            {
                IsEnabled = false;

                var multipartFormContent = await GetMultiPartFormContent();

                if (multipartFormContent == null)
                {
                    IsBusy = false;
                    return;
                }

                await networkService.PostAsyncHttpResponseMessage(APIConstants.PostUploadProfilePicture(), multipartFormContent, true);

                var profile = new CoachProfileDTO(Name, UserId, Type, IsAvailable, OffersMeal, OffersWorkout, CertificationId, Gender, IsVerified, Description, Pricing, ProfilePictureId, PayPalLink);

                if (profile == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Sorry! We are having an issue retrieving your profile. Please try again.", "OK");
                }

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
                else return;
            }
            else
                return;
        }
    }
}
