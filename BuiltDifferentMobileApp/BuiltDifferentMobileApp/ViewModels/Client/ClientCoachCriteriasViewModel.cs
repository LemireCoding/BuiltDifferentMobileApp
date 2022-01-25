using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Views.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class ClientCoachCriteriasViewModel : ViewModelBase
    {
        private string coachName;
        public string CoachName { get => coachName; set => SetProperty(ref coachName, value); }

        private string coachingType;
        public string CoachingType { get; set; }

        private string gender;
        public string Gender { get; set; }

        public List<string> CoachingTypes { get; set; }

        public List<string> Genders { get; set; }
        public AsyncCommand SearchCommand { get; }
        public ClientCoachCriteriasViewModel()
        {
            CoachingTypes = new List<string> {AppResource.ClientCoachCriteriaCoachingTypesBoth, AppResource.ClientCoachCriteriaCoachingTypesWorkouts, AppResource.ClientCoachCriteriaCoachingTypesMeals };
            Genders = new List<string> { AppResource.ClientCoachCriteriaCoachingGenderM, AppResource.ClientCoachCriteriaCoachingGenderF, AppResource.ClientCoachCriteriaCoachingGenderPNTS };
            SearchCommand = new AsyncCommand(SearchCoach);
        }

        public async Task SearchCoach()
        {
            if(CoachingType == AppResource.ClientCoachCriteriaCoachingTypesBoth)
            {
                CoachingType = "Both";
            }
           await Application.Current.MainPage.Navigation.PushAsync(new ClientCoachSelectionPage(CoachName, Gender.ToLower(), CoachingType.ToLower()));
        }
    }
}
