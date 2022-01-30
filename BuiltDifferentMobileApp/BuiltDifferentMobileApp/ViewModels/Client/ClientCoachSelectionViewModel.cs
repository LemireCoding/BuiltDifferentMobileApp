using BuiltDifferentMobileApp.Services.NetworkServices;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BuiltDifferentMobileApp.Models;
using System.Threading;
using BuiltDifferentMobileApp.Ressource;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class ClientCoachSelectionViewModel : ViewModelBase
    {
        private string coachName, gender, coachingType;
        public string CoachName { get => coachName; set => SetProperty(ref coachName, value); }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        public string CoachingType { get => coachingType; set => SetProperty(ref coachingType, value); }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public ObservableRangeCollection<Models.Coach> Coaches { get; set; }
        public ClientCoachSelectionViewModel(string coachN, string gen, string coachingT)
        {
            CoachName = coachN;
            Gender = gen;
            CoachingType = coachingT;
            GetCoaches();
        }

        public ClientCoachSelectionViewModel()
        {
            
        }

        public async Task GetCoaches()
        {
            if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("fr"))
            {
                if (CoachingType == "Exercices et Repas")
                    CoachingType = "Both";
                else if (CoachingType == "Exercices")
                    CoachingType = "Workouts";
                else if (CoachingType == "Repas")
                    CoachingType = "Meals";
            }

            var query = "";
            if(!string.IsNullOrEmpty(CoachName) || !string.IsNullOrEmpty(Gender) || !string.IsNullOrEmpty(CoachingType))
            {
                query = "?";
                if (!string.IsNullOrEmpty(CoachName))
                {
                    query = query + "name=" + CoachName;
                } else
                {
                    query = query + "gender=" + Gender + "&" + "type=" + CoachingType;
                }
            }
            
            var result = await networkService.GetAsync<ObservableRangeCollection<Models.Coach>>(APIConstants.GetAllAvailableCoach(query));
            if (result == null || result.Count == 0)
            {
                return;
            }

            Coaches = new ObservableRangeCollection<Models.Coach>(result);
            OnPropertyChanged("Coaches");
        }


    }
}