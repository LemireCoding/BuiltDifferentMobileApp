using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

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

        public string Type { get; set; }
        public bool IsAvailable { get; set; }
        public bool OffersMeal { get; set; }
        public bool OffersWorkout { get; set; }
        public int CertificationId { get; set; }
        public string Gender { get; set; }
        public bool IsVerified { get; set; }

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }
        private int UserId;
        private int Id;

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageCoachViewModel() {
            Models.Coach user = (Models.Coach)accountService.CurrentUser;

            Name = user.name;
            Type = user.type;
            IsAvailable = user.isAvailable;
            OffersMeal = user.offersMeal;
            OffersWorkout = user.offersWorkout;
            CertificationId = user.certificationId;
            Gender = user.gender;
            IsVerified = user.isVerified;
            IsEnabled = false;
        }

    }
}
