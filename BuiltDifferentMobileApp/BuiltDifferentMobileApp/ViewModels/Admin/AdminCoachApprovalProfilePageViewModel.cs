using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Plugin.XamarinFormsSaveOpenPDFPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
        public string Pricing { get; set; }
        public MemoryStream Certification { get; set; }

        public AsyncCommand<string> RespondToCoachApplicationCommand { get; set; }
        public AsyncCommand ViewCoachCertificationCommand { get; set; }

        public AdminCoachApprovalProfilePageViewModel(int coachId) {
            Title = "Coach Application";
            CoachId = coachId;
            DenyButtonText = "Deny";
            ApproveButtonText = "Approve";
            Name = "";
            Gender = "";
            PlansOffered = "";
            Description = "";
            Pricing = "";
            Certification = new MemoryStream();

            RespondToCoachApplicationCommand = new AsyncCommand<string>(RespondToCoachApplication);
            ViewCoachCertificationCommand = new AsyncCommand(ViewCoachCertification);

            FetchCoachInformation();
        }

        private async Task ViewCoachCertification() {
            if(Certification == null) return;

            await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView($"{Guid.NewGuid()}.pdf", "application/pdf", Certification, PDFOpenContext.InApp);
        }

        private async Task RespondToCoachApplication(string action) {
            int response;

            if(action == ApproveButtonText) {
                var accepted = await Application.Current.MainPage.DisplayAlert("Approve Coach", "Are you sure you want to approve this coach?", "Confirm", "Cancel");

                if(!accepted) return;

                response = (int)await networkService.PutAsyncHttpResponseMessage(APIConstants.GetApproveDenyCoachUri(CoachId), new CoachApprovalStatus(ApprovalConstants.APPROVED));
            }
            else {
                var accepted = await Application.Current.MainPage.DisplayAlert("Deny Coach", "Are you sure you want to deny this coach?", "Confirm", "Cancel");

                if(!accepted) return;

                response = (int)await networkService.PutAsyncHttpResponseMessage(APIConstants.GetApproveDenyCoachUri(CoachId), new CoachApprovalStatus(ApprovalConstants.DENIED));
            }

            if(response >= 200 && response <= 299) {
                await Shell.Current.GoToAsync("..");
            } else {
                await Application.Current.MainPage.DisplayAlert($"Server Error ({response})", "Could not accept/deny coach. Please try again.", "OK");
            }
        }

        public async Task FetchCoachInformation() {
            var coach = await networkService.GetAsync<Models.Coach>(APIConstants.GetCoachByIdUri(CoachId));
            var certification = await networkService.GetStreamAsync(APIConstants.GetCoachCertificationUri(CoachId));

            if(coach == null || certification == null) {
                await Application.Current.MainPage.DisplayAlert("Could not load coach's profile!", "Returning to previous page", "OK");
                await Shell.Current.GoToAsync("..");
                IsBusy = false;
                return;
            }

            Name = coach.name;
            Description = coach.description;
            Gender = coach.gender;
            Pricing = $"${coach.pricing:F}";

            await certification.CopyToAsync(Certification);

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
            OnPropertyChanged("Pricing");
        }

    }
}
