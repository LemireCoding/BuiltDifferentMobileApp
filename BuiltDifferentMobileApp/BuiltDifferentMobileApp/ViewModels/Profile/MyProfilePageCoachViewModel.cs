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
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Profile {
    public class MyProfilePageCoachViewModel : ViewModelBase {

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
        private int CoachId;
        private int UserId;

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageCoachViewModel() {
            Models.Coach user = (Models.Coach)accountService.CurrentUser;

            Name = user.name;
            Type = user.type;
            CoachId = user.id;
            UserId = user.userId;
            IsAvailable = user.isAvailable;
            OffersMeal = user.offersMeal;
            OffersWorkout = user.offersWorkout;
            CertificationId = user.certificationId;
            Gender = user.gender;
            IsVerified = user.isVerified;
            Description = user.description;
            Pricing = user.pricing;
            IsEnabled = false;
            SubmitCommand = new AsyncCommand(Submit);
            EditProfileCommand = new AsyncCommand(Edit);
        }

        private async Task Edit()
        {
            if (!IsEnabled)
            {
                IsEnabled = true;
            }
        }

        private async Task Submit()
        {

            if (IsEnabled)
            {
                IsEnabled = false;
            }

            if (
                string.IsNullOrEmpty(Name)
                )
            {
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields", "OK");
                return;
            }
            HttpResponseMessage response = await networkService.GetAsync<HttpResponseMessage>(APIConstants.GetCoachByCoachId(CoachId));
            //default ids inserted for now
            //empty strings for receipe and image link
            //must have receipe and image link filled

            var profile = new CoachProfileDTO(Name, UserId, Type, IsAvailable,OffersMeal,OffersWorkout,CertificationId,Gender,IsVerified,Description,Pricing);
            var test = JsonConvert.SerializeObject(profile);
            var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.PutProfileUri(UserId), profile);
            var httpCode = result.StatusCode;

            if (accountService.CurrentUserRole == AccountConstants.Coach && httpCode == System.Net.HttpStatusCode.OK)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Profile Saved", "OK");
                await AppShell.Current.GoToAsync("..");
            }
            else if (httpCode == System.Net.HttpStatusCode.NotFound)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occured on the server. Please try saving again.", "OK");
            }
            else if (httpCode == System.Net.HttpStatusCode.InternalServerError)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occured on the server. Please try saving again.", "OK");
            }
            else if(accountService.CurrentUserRole != AccountConstants.Coach){
                await Application.Current.MainPage.DisplayAlert("Error", "An error occured you are not logged in as a coach", "OK");
            }
            else
                return;
        }
    }
   
}
