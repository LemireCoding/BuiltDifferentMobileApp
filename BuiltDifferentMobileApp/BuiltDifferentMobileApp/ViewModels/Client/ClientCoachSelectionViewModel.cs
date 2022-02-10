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
using MvvmHelpers.Commands;
using BuiltDifferentMobileApp.Services.AccountServices;
using Xamarin.Forms;
using Newtonsoft.Json;
using BuiltDifferentMobileApp.Views.Client;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class ClientCoachSelectionViewModel : ViewModelBase
    {
        private int clientId;
        private string clientName;
        private string planSelected;
        private string coachName, gender, coachingType;
        public string CoachName { get => coachName; set => SetProperty(ref coachName, value); }
        public AsyncCommand<int> SendRequest { get; }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        public string CoachingType { get => coachingType; set => SetProperty(ref coachingType, value); }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public ObservableRangeCollection<Models.Coach> Coaches { get; set; }
        private IAccountService accountService = AccountService.Instance;
        public ClientCoachSelectionViewModel(string coachN, string gen, string coachingT)
        {
            CoachName = coachN;
            Gender = gen;
            CoachingType = coachingT;
            var user = (Models.Client)accountService.CurrentUser;
            this.clientId = user.id;
            this.clientName = user.name;
            planSelected = "Not Specified";
            SendRequest = new AsyncCommand<int>(Request);
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

        public async Task Request(int id)
        {

            var route = APIConstants.GetCoachByCoachId(id);
            var coach = await networkService.GetAsync<Models.Coach>(route);

           if(coach.isAvailable)
            {
                var routeClientRequests = APIConstants.GetAllRequestsByClient(clientId);
                var clientRequests = await networkService.GetAsync<List<Request>>(routeClientRequests);

                if (clientRequests == null )
                {
                    await Application.Current.MainPage.DisplayAlert("A Problem Occured", "We could not process the request", "OK");
                    return;
                }

                bool hasPending = false; 
                foreach(Request request in clientRequests)
                {
                    if(request.status == "PENDING")
                    {
                        hasPending = true;
                        break;
                    }
                }
                if (!hasPending)
                {
                    var routeSendRequest = APIConstants.PostRequestURI();

                    var request = new RequestDTO("PENDING",id, clientId, clientName, planSelected);
                    var test = JsonConvert.SerializeObject(request);
                    var result = await networkService.PostAsync<HttpResponseMessage>(routeSendRequest, request);
                    var httpCode = result.StatusCode;

                    if (httpCode == System.Net.HttpStatusCode.OK)
                    {
                        await Application.Current.MainPage.DisplayAlert("Request Sent Successfully", "Wait For your Coach to respond", "OK");
                        await Shell.Current.GoToAsync($"{nameof(ClientRequestsCenterPage)}");

                    } else
                    {
                        await Application.Current.MainPage.DisplayAlert("Request Not Sent", "We encountered a problem", "OK");

                    }
                } else
                {
                    await Application.Current.MainPage.DisplayAlert("You Already have a request sent", "Wait For your Coach to respond", "OK");

                    return;
                }
            } else
            {
                return;
            }
        }
    }
}