using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Plugin.XamarinFormsSaveOpenPDFPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Admin {
    public class AdminApprovedCoachProfilePageViewModel : ViewModelBase {
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public int CoachId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Gender { get; set; }
        public string PlansOffered { get; set; }
        public string Description { get; set; }
        public string Pricing { get; set; }
        public bool IsSuspended { get; set; }
        public MemoryStream Certification { get; set; }

        public AsyncCommand ViewCoachCertificationCommand { get; set; }
        public AsyncCommand SetSuspendedStatusCommand { get; set; }

        public AdminApprovedCoachProfilePageViewModel(int coachId) {
            CoachId = coachId;
            Name = "";
            Gender = "";
            PlansOffered = "";
            Description = "";
            Pricing = "";
            Certification = null;

            ViewCoachCertificationCommand = new AsyncCommand(ViewCoachCertification);
            SetSuspendedStatusCommand = new AsyncCommand(SetSuspendedStatus);

            FetchCoachInformation();
        }

        private async Task SetSuspendedStatus() {
            IsBusy = true;

            var updatedStatus = new AccountStatus(IsSuspended ? AccountStatusConstants.Active : AccountStatusConstants.Suspended);
            var response = await networkService.PutAsync<AccountStatus>(APIConstants.GetUserAccountStatusUri(UserId), updatedStatus);

            if(response == null) {
                // DO NOTHING
            } else {
                IsSuspended = response.accountStatus == AccountStatusConstants.Suspended;
                OnPropertyChanged("IsSuspended");
            }

            IsBusy = false;
        }

        private async Task ViewCoachCertification() {
            if(Certification == null) {
                await Application.Current.MainPage.DisplayAlert("Error Loading Certification", "Could not find certification for this coach!", "OK");
                return;
            }

            await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView($"{Guid.NewGuid()}.pdf", "application/pdf", Certification, PDFOpenContext.InApp);
        }

        public async Task FetchCoachInformation() {
            IsBusy = true;

            var coach = await networkService.GetAsync<Models.Coach>(APIConstants.GetCoachByIdUri(CoachId));
            var certification = await networkService.GetStreamAsync(APIConstants.GetCoachCertificationUri(CoachId));

            if(coach == null) {
                await Application.Current.MainPage.DisplayAlert("Could not load coach's profile!", "Returning to previous page", "OK");
                await Shell.Current.GoToAsync("..");
                IsBusy = false;
                return;
            } else {
                UserId = coach.userId;
                var account = await networkService.GetAsync<AccountStatus>(APIConstants.GetUserAccountStatusUri(UserId));

                if(account == null) {
                    await Application.Current.MainPage.DisplayAlert("Could not load coach's profile!", "Returning to previous page", "OK");
                    await Shell.Current.GoToAsync("..");
                    IsBusy = false;
                    return;
                }

                IsSuspended = account.accountStatus == AccountStatusConstants.Suspended;
            }

            Name = coach.name;
            Description = coach.description;
            Gender = coach.gender;
            Pricing = $"${coach.pricing:F}";

            if(certification != null) {
                Certification = new MemoryStream();
                await certification.CopyToAsync(Certification);
            }

            PlansOffered = "Plans: ";
            if(coach.offersMeal && coach.offersWorkout) {
                PlansOffered += "Workouts, Meals";
            } else if(coach.offersMeal && !coach.offersWorkout) {
                PlansOffered += "Meals";
            } else if(!coach.offersMeal && coach.offersWorkout) {
                PlansOffered += "Workouts";
            }

            OnPropertyChanged("Name");
            OnPropertyChanged("Gender");
            OnPropertyChanged("Description");
            OnPropertyChanged("PlansOffered");
            OnPropertyChanged("Pricing");
            OnPropertyChanged("IsSuspended");

            IsBusy = false;
        }

    }
}
