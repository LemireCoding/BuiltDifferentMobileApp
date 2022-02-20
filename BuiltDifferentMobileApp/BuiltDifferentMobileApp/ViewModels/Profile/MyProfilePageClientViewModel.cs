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
        private string profilePicture;
        public string ProfilePicture
        {
            get => profilePicture;
            set
            {
                SetProperty(ref profilePicture, value);
            }
        }

        private string bannerPicture;
        public string BannerPicture {
            get => bannerPicture;
            set {
                SetProperty(ref bannerPicture, value);
            }
        }

        private FileResult ProfilePictureFile { get; set; }
        private ImageSource previewProfilePicture;
        public ImageSource PreviewProfilePicture
        {
            get => previewProfilePicture;
            set {
                SetProperty(ref previewProfilePicture, value);
                FieldEdited = true;
            }
        }

        private FileResult BannerPictureFile { get; set; }
        private ImageSource previewBannerPicture;
        public ImageSource PreviewBannerPicture {
            get => previewBannerPicture;
            set {
                SetProperty(ref previewBannerPicture, value);
                FieldEdited = true;
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }

        private string editName;
        public string EditName {
            get => editName;
            set {
                SetProperty(ref editName, value);
                FieldEdited = true;
            }
        }

        private string email;
        public string Email {
            get => email;
            set {
                SetProperty(ref email, value);
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
            }
        }

        private int editCurrentWeight;
        public int EditCurrentWeight {
            get => editCurrentWeight;
            set {
                SetProperty(ref editCurrentWeight, value);
                FieldEdited = true;
            }
        }

        private double height;
        public double Height
        {
            get => height;
            set
            {
                SetProperty(ref height, value);
            }
        }

        private double editHeight;
        public double EditHeight {
            get => editHeight;
            set {
                SetProperty(ref editHeight, value);
                FieldEdited = true;
            }
        }

        private bool editMode;
        public bool EditMode {
            get => editMode;
            set => SetProperty(ref editMode, value);
        }

        private bool fieldEdited;
        public bool FieldEdited {
            get => fieldEdited;
            set => SetProperty(ref fieldEdited, value);
        }

        private int userId;
        public int UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }

        private string logo;
        public string Logo {
            get => logo;
            set => SetProperty(ref logo, value);
        }

        private string joinDate;
        public string JoinDate {
            get => joinDate;
            set => SetProperty(ref joinDate, value);
        }

        public AsyncCommand<string> UploadImageCommand { get; }
        public AsyncCommand GoBackCommand { get; }
        public AsyncCommand<string> EditProfileHeaderCommand { get; }

        public static readonly string HEIGHT_HEADER = "HEIGHT_HEADER";
        public static readonly string WEIGHT_HEADER = "WEIGHT_HEADER";

        public static readonly string PROFILE_PICTURE = "PROFILE_PICTURE";
        public static readonly string BANNER_PICTURE = "BANNER_PICTURE";

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageClientViewModel()
        {
            Logo = APIConstants.GetLogo(false);

            var userInfo = (Models.Client)accountService.CurrentUser;
            Name = userInfo.name;
            UserId = userInfo.userId;
            StartWeight = userInfo.startWeight;
            CurrentWeight = userInfo.currentWeight;
            Height = userInfo.height;
            Email = accountService.CurrentUserEmail;
            JoinDate = "";

            EditName = Name;
            EditHeight = Height;
            EditCurrentWeight = CurrentWeight;
            FieldEdited = false;

            ProfilePicture = APIConstants.GetProfilePictureUri(UserId);
            BannerPicture = APIConstants.GetProfileBannerPictureUri(UserId);

            ProfilePictureFile = null;
            BannerPictureFile = null;

            EditMode = false;

            UploadImageCommand = new AsyncCommand<string>(UploadImage);
            GoBackCommand = new AsyncCommand(GoBack);
            EditProfileHeaderCommand = new AsyncCommand<string>(EditProfileHeader);
            
            GetUserInfo(true);
        }

        private async Task GoBack() {
            await Shell.Current.GoToAsync("..");
        }

        public async Task GetUserInfo(bool fromNetwork = true) {
            if(fromNetwork) {
                await networkService.UpdateCurrentUser();
                JoinDate = $"Joined {(await networkService.GetAsync<UserJoinDate>(APIConstants.GetUserJoinDate(UserId))).joinDate.ToString("MMMM yyyy")}";
            }

            var userInfo = (Models.Client)accountService.CurrentUser;

            if(userInfo != null) {
                Name = userInfo.name;
                UserId = userInfo.userId;
                StartWeight = userInfo.startWeight;
                CurrentWeight = userInfo.currentWeight;
                Height = userInfo.height;
            }
        }

        public void LoadEditInfo() {
            EditName = Name;
            EditHeight = Height;
            EditCurrentWeight = CurrentWeight;
            PreviewProfilePicture = ProfilePicture;
            PreviewBannerPicture = BannerPicture;
            ProfilePictureFile = null;
            BannerPictureFile = null;
            FieldEdited = false;
        }

        private async Task EditProfileHeader(string header) {
            if(IsBusy) return;

            string result = "";

            if(header == HEIGHT_HEADER) {
                result = await Application.Current.MainPage.DisplayPromptAsync(
                    "Edit Height", "Please enter your updated height", initialValue: EditHeight.ToString(), maxLength: 6, keyboard: Keyboard.Numeric
                );
            }
            else if(header == WEIGHT_HEADER) {
                result = await Application.Current.MainPage.DisplayPromptAsync(
                    "Edit Weight", "Please enter your updated weight", initialValue: EditCurrentWeight.ToString(), maxLength: 5, keyboard: Keyboard.Numeric
                );
            }

            if(!string.IsNullOrEmpty(result)) {
                if(header == HEIGHT_HEADER) {
                    EditHeight = double.Parse(result);
                }
                else if(header == WEIGHT_HEADER) {
                    EditCurrentWeight = Convert.ToInt32(Math.Round(Convert.ToDouble(result)));
                }
            }
        }

        private async Task UploadImage(string type)
        {
            if(IsBusy) return;

            var options = new MediaPickerOptions {
                Title = type == PROFILE_PICTURE ? "Please select your profile picture" : "Please select your banner picture"
            };

            try
            {
                var picture = await MediaPicker.PickPhotoAsync(options);

                if (picture != null)
                {
                    var stream = await picture.OpenReadAsync();

                    if(type == PROFILE_PICTURE) {
                        ProfilePictureFile = picture;
                        PreviewProfilePicture = ImageSource.FromStream(() => stream);
                    } else {
                        BannerPictureFile = picture;
                        PreviewBannerPicture = ImageSource.FromStream(() => stream);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private async Task<MultipartFormDataContent> GetMultiPartFormContent(bool isProfilePicture)
        {
            try
            {
                MultipartFormDataContent formContent = new MultipartFormDataContent();

                StreamContent content;
                if(isProfilePicture) {
                    content = new StreamContent(await ProfilePictureFile.OpenReadAsync());
                    content.Headers.ContentType = new MediaTypeHeaderValue(ProfilePictureFile.ContentType);

                    formContent.Add(content, "profilePicture", Guid.NewGuid().ToString());
                }
                else {
                    content = new StreamContent(await BannerPictureFile.OpenReadAsync());
                    content.Headers.ContentType = new MediaTypeHeaderValue(BannerPictureFile.ContentType);

                    formContent.Add(content, "bannerPicture", Guid.NewGuid().ToString());
                }

                return formContent;
            }
            catch
            {
                return null;
            }
        }

        public async Task SubmitEditedFields() {
            IsBusy = true;

            if(ProfilePictureFile != null) {
                var payload = await GetMultiPartFormContent(true);
                if(payload != null) {
                    await networkService.PostAsyncHttpResponseMessage(APIConstants.PostUploadProfilePicture(), payload, true);
                    OnPropertyChanged(nameof(ProfilePicture));
                }
            }

            if(BannerPictureFile != null) {
                var payload = await GetMultiPartFormContent(false);
                if(payload != null) {
                    await networkService.PostAsyncHttpResponseMessage(APIConstants.PostUploadProfileBannerPicture(), payload, true);
                    OnPropertyChanged(nameof(BannerPicture));
                }
            }

            if(EditHeight != Height || EditCurrentWeight != CurrentWeight || EditName != Name) {
                var profile = new ClientProfileDTO(EditName, UserId, EditCurrentWeight, EditHeight);
                await networkService.PutAsync<HttpResponseMessage>(APIConstants.PutProfileUri(), profile);

                ((Models.Client)accountService.CurrentUser).name = EditName;
                ((Models.Client)accountService.CurrentUser).currentWeight = EditCurrentWeight;
                ((Models.Client)accountService.CurrentUser).height = EditHeight;
                await GetUserInfo(false);
            }

            IsBusy = false;
        }
    }
}