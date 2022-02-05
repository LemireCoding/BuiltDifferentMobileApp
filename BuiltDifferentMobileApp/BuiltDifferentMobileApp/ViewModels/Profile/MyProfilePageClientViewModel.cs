using BuiltDifferentMobileApp.Models;
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

namespace BuiltDifferentMobileApp.ViewModels.Profile {
    public class MyProfilePageClientViewModel : ViewModelBase {

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
        public object PreviewPicture { get; set; }

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand UploadImageCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageClientViewModel() {
            Name = "";
            StartWeight = 0;
            CurrentWeight = 0;
            ProfilePictureId = 0;

            ProfilePicture = null;
            PreviewPicture = null;

            GetUserInfo();

            isEnabled = false;

            SubmitCommand = new AsyncCommand(Submit);
            UploadImageCommand = new AsyncCommand(Upload);
            EditProfileCommand = new AsyncCommand(Edit);
        }

        private async Task GetUserInfo()
        {
            Models.Client user = (Models.Client)accountService.CurrentUser;
            var userInfo = await networkService.GetAsync<Models.Client>(APIConstants.GetProfileUri());
            accountService.CurrentUser = userInfo;

            Name = userInfo.name;
            UserId = userInfo.userId;
            StartWeight = userInfo.startWeight;
            CurrentWeight = userInfo.currentWeight;
            ProfilePictureId = userInfo.profilePictureId;

            var pic = await networkService.GetStreamAsync(APIConstants.GetProfilePictureUri());

            PreviewPicture = ImageSource.FromStream(() => pic);
            OnPropertyChanged("PreviewPicture");

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
            var customFileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                { DevicePlatform.iOS, new[] { "image/png" } },
                { DevicePlatform.Android, new[] { "image/png" } },
            });

            var options = new PickOptions
            {
                PickerTitle = "Please select your profilePicture (png)",
                FileTypes = customFileTypes,
            };

            try
            {
                var file = await FilePicker.PickAsync(options);

                if (file != null && file.ContentType == "image/png")
                {
                    var stream = await file.OpenReadAsync();

                    PreviewPicture = ImageSource.FromStream(() => stream);
                    OnPropertyChanged("PreviewPicture");

                    ProfilePicture = file;
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Image format", "Please select a png file.", "OK");
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

                // Read file contents & set MIME appopriately, image/png
                StreamContent fileContent = new StreamContent(await ProfilePicture.OpenReadAsync());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(ProfilePicture.ContentType);

                // Add it to the form content as "profilePicture"
                formContent.Add(fileContent, "profilePicture", Guid.NewGuid().ToString());

                return formContent;
            }
            catch
            {
                return null;
            }
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

                var profile = new ClientProfileDTO(Name, UserId, CurrentWeight, ProfilePictureId);
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
            }
            else
                return;
        }
    }
}