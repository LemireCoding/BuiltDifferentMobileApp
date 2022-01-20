using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
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
                OtherGenderInputVisible = selectedGender == "Other";
                OnPropertyChanged("OtherGenderInputVisible");
                OnPropertyChanged();
            }
        }

        public string Description { get; set; }
        public string Pricing { get; set; }
        public bool OfferWorkouts { get; set; }
        public bool OfferMeals { get; set; }
        public string FileName { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        private FileResult Certification { get; set; }
        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand SelectCertificationFileCommand { get; set; }
        public AsyncCommand SubmitFormCommand { get; set; }

        public NewCoachPageViewModel() {
            Title = "Complete Account Setup";
            OtherGender = "";
            OtherGenderInputVisible = false;
            Description = "";
            Pricing = "";
            OfferWorkouts = false;
            OfferMeals = false;
            FileName = "";
            Certification = null;

            GenderPickerList = new List<string>() {
                "Male", "Female", "Other"
            };

            SelectCertificationFileCommand = new AsyncCommand(SelectCertificationFile);
            SubmitFormCommand = new AsyncCommand(SubmitForm);
        }

        private async Task SubmitForm() {
            var multipartFormContent = await GetMultiPartFormContent();
            if(multipartFormContent == null) {
                return;
            }

            var response = await networkService.PostAsyncHttpResponseMessage(APIConstants.UploadCertificationUri(accountService.CurrentUser.id), multipartFormContent, true);

            if(((int)response >= 200) && ((int)response <= 299)) {
                await Application.Current.MainPage.DisplayAlert("", "good", "OK");
            }
        }

        private async Task SelectCertificationFile() {
            var customFileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                { DevicePlatform.iOS, new[] { "application/pdf" } },
                { DevicePlatform.Android, new[] { "application/pdf" } },
            });

            var options = new PickOptions {
                PickerTitle = "Please select your certification (PDF)",
                FileTypes = customFileTypes,
            };

            try {
                var file = await FilePicker.PickAsync(options);

                if(file != null && file.ContentType == "application/pdf") {
                    FileName = $"File Name: {file.FileName}";

                    // Processing inputted file to a stream is not done until the user submits in order to not create a stream closed error
                    // This fixes the user having to select a file before hitting submit every time,
                    // They can hit submit as many times as they want without having to select the file again
                    Certification = file;
                } 
                else {
                    FileName = "Invalid file type. Please select a PDF.";
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
