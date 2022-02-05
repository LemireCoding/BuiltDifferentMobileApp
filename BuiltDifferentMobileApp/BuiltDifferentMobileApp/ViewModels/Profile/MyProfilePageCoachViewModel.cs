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
            PreviewPicture = null;


            ProfilePicture = null;


            GetUserInfo();

            isEnabled = false;

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

           
            var pic = await networkService.GetStreamAsync(APIConstants.GetProfilePictureUri());

            PreviewPicture = ImageSource.FromStream(() => pic);
            //{
            //    MemoryStream ms = new MemoryStream(byteArrayIn);
            //    Image returnImage = Image.FromStream(ms);
            //    return returnImage;
            //}
            //var picture = pic.ReadAsByteArrayAsync();
            //Image img = new Image();
            //img.Source = pic.Source;

            //var stream = await pic.OpenReadAsync();

            //PreviewPicture = ImageSource.FromStream(() => stream);
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

                await networkService.PostAsyncHttpResponseMessage(APIConstants.PostUploadProfilePicture(), multipartFormContent, true );

                var profile = new CoachProfileDTO(Name, UserId, Type, IsAvailable, OffersMeal, OffersWorkout, CertificationId, Gender, IsVerified, Description, Pricing, ProfilePictureId);
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
