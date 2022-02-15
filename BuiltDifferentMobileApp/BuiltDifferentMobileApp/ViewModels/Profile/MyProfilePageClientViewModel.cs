using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Profile
{
    public class MyProfilePageClientViewModel : ViewModelBase
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
        public int startWeight;
        public int StartWeight
        {
            get => startWeight;
            set
            {
                SetProperty(ref startWeight, value);
            }
        }
        private int currentWeight;
        public int CurrentWeight
        {
            get => currentWeight;
            set
            {
                SetProperty(ref currentWeight, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private double height;
        public double Height
        {
            get => height;
            set
            {
                SetProperty(ref height, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private int profilePictureId;
        public int ProfilePictureId
        {
            get => profilePictureId;
            set
            {
                SetProperty(ref profilePictureId, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }
        private int userId;
        public int UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }

        public object PreviewPicture { get; set; }

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand UploadImageCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageClientViewModel()
        {
            Name = "";
            StartWeight = 0;
            UserId = 0;
            StartWeight = 0;
            CurrentWeight = 0;
            Height = 0;
            ProfilePictureId = 0;

            ProfilePicture = null;
            PreviewPicture = null;
            

            GetUserInfo();

            IsEnabled = false;

            SubmitCommand = new AsyncCommand(Submit);
            UploadImageCommand = new AsyncCommand(Upload);
            EditProfileCommand = new AsyncCommand(Edit);
        }

        private async Task GetUserInfo()
        {
            Models.Client user = (Models.Client)accountService.CurrentUser;
            var userInfo = await networkService.GetAsync<Models.Client>(APIConstants.GetProfileUri());
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

                UserId = userInfo.id;
                Name = userInfo.name;
                UserId = userInfo.userId;
                StartWeight = userInfo.startWeight;
                CurrentWeight = userInfo.currentWeight;
                ProfilePictureId = userInfo.profilePictureId;
                Height = userInfo.height;

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
            if (string.IsNullOrEmpty(Name) || CurrentWeight == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields. ", "OK");
                return;
            }

            if (IsEnabled)
            {
                IsEnabled = false;

                var multipartFormContent = await GetMultiPartFormContent();

                if (multipartFormContent == null)
                {
                    IsBusy = false;
                }

                var picture = await networkService.PostAsyncHttpResponseMessage(APIConstants.PostUploadProfilePicture(), multipartFormContent, true);

                var profile = new ClientProfileDTO(Name, UserId, CurrentWeight, Height);
               
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