using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.ViewModels.Profile {
    public class MyProfilePageCoachViewModel : ViewModelBase {

        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }
        public bool OffersMeal { get; set; }
        public bool OffersWorkout { get; set; }
        public string Certification { get; set; }
        public string Gender { get; set; }
        public bool IsVerified { get; set; }

        private IAccountService accountService = AccountService.Instance;

        public MyProfilePageCoachViewModel() {
            Models.Coach user = (Models.Coach)accountService.GetCurrentUser();

            Name = user.name;
            Type = user.type;
            IsAvailable = user.isAvailable;
            OffersMeal = user.offersMeal;
            OffersWorkout = user.offersWorkout;
            Certification = user.certification;
            Gender = user.gender;
            IsVerified = user.isVerified;
        }

    }
}
