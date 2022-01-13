using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Admin {
    public class AdminCoachApprovalProfilePageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public int CoachId { get; set; }
        public string DenyButtonText { get; set; }
        public string ApproveButtonText { get; set; }

        public string Name { get; set; }
        public string Gender { get; set; }
        public string PlansOffered { get; set; }
        public string Description { get; set; }

        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand<string> RespondToCoachApplicationCommand { get; set; }

        public AdminCoachApprovalProfilePageViewModel(int coachId) {
            Title = "Coach Application";
            CoachId = coachId;
            DenyButtonText = "Deny";
            ApproveButtonText = "Approve";

            RefreshCommand = new AsyncCommand(FetchCoachInformation);
            RespondToCoachApplicationCommand = new AsyncCommand<string>(RespondToCoachApplication);

            FetchCoachInformation();
        }

        private async Task RespondToCoachApplication(string action) {
            // TODO: actually respond to coach application
            if(action == ApproveButtonText) {
                await Application.Current.MainPage.DisplayAlert("(PLACEHOLDER) Confirm action", "Are you sure you want to approve this coach?", "Confirm", "Cancel");
            }
            else {
                await Application.Current.MainPage.DisplayAlert("(PLACEHOLDER) Confirm action", "Are you sure you want to deny this coach ?", "Confirm", "Cancel");
            }
        }

        public async Task FetchCoachInformation() {
            IsBusy = true;

            var coach = await networkService.GetAsync<Models.Coach>(APIConstants.GetCoachByIdUri(CoachId));

            if(coach == null) {
                await Application.Current.MainPage.DisplayAlert("Could not load coach's profile!", "Returning to previous page", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            Name = coach.name;
            Description = coach.description;
            Gender = coach.gender;

            PlansOffered = "Plans: ";
            if(coach.offersMeal && coach.offersWorkout) {
                PlansOffered += "Workouts, Meals";
            }
            else if(coach.offersMeal && !coach.offersWorkout) {
                PlansOffered += "Meals";
            }
            else if(!coach.offersMeal && coach.offersWorkout) {
                PlansOffered += "Workouts";
            }

            OnPropertyChanged("Name");
            OnPropertyChanged("Gender");
            OnPropertyChanged("Description");
            OnPropertyChanged("PlansOffered");

            IsBusy = false;
        }

    }
}
