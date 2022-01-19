using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

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

            GenderPickerList = new List<string>() {
                "Male", "Female", "Other"
            };

            SelectCertificationFileCommand = new AsyncCommand(SelectCertificationFile);
            SubmitFormCommand = new AsyncCommand(SubmitForm);
        }

        private async Task SubmitForm() {
            
        }

        private async Task SelectCertificationFile() {
            
        }
    }
}
