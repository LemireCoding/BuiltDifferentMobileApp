using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views.Coach;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach {
    public class NewCoachPageViewModel : ViewModelBase {

        public List<string> GenderPickerList { get; set; }

        public bool OtherGenderInputVisible { get; set; }
        public string OtherGender { get; set; }

        private string selectedGender;
        public string SelectedGender {
            get => selectedGender;
            set {
                selectedGender = value;
                OtherGenderInputVisible = selectedGender == AppResource.OtherGender;
                OnPropertyChanged("OtherGenderInputVisible");
                OnPropertyChanged();
            }
        }

        private string errorText;
        public string ErrorText {
            get => errorText;
            set => SetProperty(ref errorText, value);
        }

        public static readonly string MissingInputs = AppResource.MissingInputs;
        public static readonly string OtherGenderUnspecified = AppResource.SpecifyGenderError;
        public static readonly string MissingCertification = AppResource.MissingCertificationError;
        public static readonly string PricingError = AppResource.PricingError;
        public static readonly string ServerError = AppResource.ViewModelErrorMessage;

        private string pendingApprovalTitle;
        public string PendingApprovalTitle {
            get => pendingApprovalTitle;
            set => SetProperty(ref pendingApprovalTitle, value);
        }

        private string pendingApprovalBody;
        public string PendingApprovalBody {
            get => pendingApprovalBody;
            set => SetProperty(ref pendingApprovalBody, value);
        }

        public static readonly string ApprovalPendingTitle = AppResource.ApprovalPendingTitle;
        public static readonly string ApprovalPendingBody = AppResource.ApprovalPendingBody;
        public static readonly string ApprovalPassedTitle = AppResource.ApprovalPassedTitle;
        public static readonly string ApprovalPassedBody = AppResource.ApprovalPassedBody;
        public static readonly string ApprovalDeniedTitle = AppResource.ApprovalDeniedTitle;
        public static readonly string ApprovalDeniedBody = AppResource.ApprovalDeniedBody;

        public string Description { get; set; }
        public string Pricing { get; set; }
        public bool OfferWorkouts { get; set; }
        public bool OfferMeals { get; set; }
        public string FileName { get; set; }
        public string PayPalLink { get; set; }

        public bool HasSubmittedCertification { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        private FileResult Certification { get; set; }
        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand SelectCertificationFileCommand { get; set; }
        public AsyncCommand SubmitFormCommand { get; set; }
        public AsyncCommand CheckIfSubmissionApprovedCommand { get; set; }

        public NewCoachPageViewModel() {
            Title = AppResource.CompleteAccountSetup;
            OtherGender = "";
            OtherGenderInputVisible = false;
            Description = "";
            Pricing = "";
            OfferWorkouts = false;
            OfferMeals = false;
            FileName = "";
            Certification = null;
            PayPalLink = "";

            GenderPickerList = new List<string>() {
                AppResource.MaleGender, AppResource.FemaleGender, AppResource.OtherGender
            };

            SelectCertificationFileCommand = new AsyncCommand(SelectCertificationFile);
            SubmitFormCommand = new AsyncCommand(SubmitForm);
            CheckIfSubmissionApprovedCommand = new AsyncCommand(CheckIfSubmissionApproved);

            HasSubmittedCertification = ((Models.Coach)accountService.CurrentUser).certificationId > 0;
            if(HasSubmittedCertification) CheckIfSubmissionApproved();
            else {
                PendingApprovalTitle = ApprovalPendingTitle;
                PendingApprovalBody = ApprovalPendingBody;
            }
        }

        public async Task CheckIfSubmissionApproved() {
            if(!HasSubmittedCertification) return;
            IsBusy = true;

            var approval = await networkService.GetAsync<Models.CoachApprovalStatus>(APIConstants.GetCoachApprovalStatusUri(accountService.CurrentUser.id));

            if(approval == null) {
                PendingApprovalTitle = ApprovalPendingTitle;
                PendingApprovalBody = ApprovalPendingBody;
            } else if(approval.approvalStatus == ApprovalConstants.APPROVED) {
                PendingApprovalTitle = ApprovalPassedTitle;
                PendingApprovalBody = ApprovalPassedBody;
                await networkService.UpdateCurrentUser();
                await Task.Delay(1000);
                await Shell.Current.GoToAsync($"//{nameof(CoachDashboardPage)}");
            } else if(approval.approvalStatus == ApprovalConstants.DENIED) {
                PendingApprovalTitle = ApprovalDeniedTitle;
                PendingApprovalBody = ApprovalDeniedBody;
            } else {
                PendingApprovalTitle = ApprovalPendingTitle;
                PendingApprovalBody = ApprovalPendingBody;
            }

            IsBusy = false;
        }

        private async Task SubmitForm() {
            if(HasSubmittedCertification) return;
            IsBusy = true;

            if(string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(Pricing) || !GenderPickerList.Contains(SelectedGender) || string.IsNullOrWhiteSpace(PayPalLink)) {
                ErrorText = MissingInputs;
                IsBusy = false;
                return;
            }

            if(SelectedGender == "Other" && string.IsNullOrEmpty(OtherGender)) {
                ErrorText = OtherGenderUnspecified;
                IsBusy = false;
                return;
            }

            var multipartFormContent = await GetMultiPartFormContent();

            if(multipartFormContent == null) {
                ErrorText = MissingCertification;
                IsBusy = false;
                return;
            }

            double parsedPricing;
            try {
                parsedPricing = double.Parse(Pricing);
            } catch {
                ErrorText = PricingError;
                IsBusy = false;
                return;
            }

            string translatedGender = SelectedGender == AppResource.MaleGender ? "Male" : "Female";

            var coachProfile = new Dictionary<string, string>() {
                { "gender", SelectedGender == AppResource.OtherGender ? OtherGender.Trim() : translatedGender },
                { "description", Description.Trim() },
                { "pricing", parsedPricing.ToString() },
                { "offersWorkout", OfferWorkouts.ToString() },
                { "offersMeal", OfferMeals.ToString() },
                { "payPalLink", PayPalLink.ToString() },
            };

            var uploadProfile = (int)await networkService.PutAsyncHttpResponseMessage(APIConstants.GetCoachByIdUri(accountService.CurrentUser.id), coachProfile);

            if(uploadProfile >= 200 && uploadProfile <= 299) {
                var uploadCertification = (int)await networkService.PostAsyncHttpResponseMessage(APIConstants.UploadCertificationUri(), multipartFormContent, true);

                if(uploadCertification >= 200 && uploadCertification <= 299) {
                    ErrorText = "";
                    HasSubmittedCertification = true;
                    OnPropertyChanged("HasSubmittedCertification");
                }
                else {
                    ErrorText = ServerError;
                }
            }
            else {
                ErrorText = ServerError;
            }

            IsBusy = false;
        }

        private async Task SelectCertificationFile() {
            var customFileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                { DevicePlatform.iOS, new[] { "application/pdf" } },
                { DevicePlatform.Android, new[] { "application/pdf" } },
            });

            var options = new PickOptions {
                PickerTitle = $"{AppResource.PleaseSelectCertification} (PDF)",
                FileTypes = customFileTypes,
            };

            try {
                var file = await FilePicker.PickAsync(options);

                if(file != null && file.ContentType == "application/pdf") {
                    FileName = $"{AppResource.FileNameTextStart} {file.FileName}";

                    // Processing inputted file to a stream is not done until the user submits in order to not create a stream closed error
                    // This fixes the user having to select a file before hitting submit every time,
                    // They can hit submit as many times as they want without having to select the file again
                    Certification = file;
                } 
                else {
                    FileName = AppResource.SelectPDFError;
                }
            }
            // If the user doesn't select a file
            catch(Exception) {
                FileName = "";
            }

            OnPropertyChanged("FileName");
        }

        private async Task<MultipartFormDataContent> GetMultiPartFormContent() {
            try {
                MultipartFormDataContent formContent = new MultipartFormDataContent();

                // Read file contents & set MIME appopriately, application/pdf in this case
                StreamContent fileContent = new StreamContent(await Certification.OpenReadAsync());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(Certification.ContentType);

                // Add it to the form content as "certification" and give the file a random GUID name to preserve privacy
                formContent.Add(fileContent, "certification", Guid.NewGuid().ToString());

                return formContent;
            } catch {
                return null;
            }
        }

    }
}
