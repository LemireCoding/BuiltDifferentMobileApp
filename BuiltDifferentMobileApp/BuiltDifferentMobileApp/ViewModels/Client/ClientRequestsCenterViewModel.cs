using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class ClientRequestsCenterViewModel : ViewModelBase
    {
        private IAccountService accountService = AccountService.Instance;
        private int clientId;
 
        public ObservableRangeCollection<Request> Requests { get; set; }

        public AsyncCommand <Request>CancelRequest { get; }

        public AsyncCommand ViewMyProfileCommand { get; }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public ClientRequestsCenterViewModel()
        {
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
            var user = (Models.Client)accountService.CurrentUser;
            this.clientId = user.id;
            CancelRequest = new AsyncCommand<Request>(Cancel);
            GetRequests();
        }

        public ClientRequestsCenterViewModel(string test)
        {
            
        }

        public async Task GetRequests()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<Request>>(APIConstants.GetAllRequestsByClient(clientId));
            if (result == null || result.Count == 0)
            {
                return;
            }

            foreach(Request request in result)
            {
                var coach = await networkService.GetAsync<Models.Coach>(APIConstants.GetCoachByCoachId(request.coachId));
                if (coach == null)
                {
                    request.coachName = "";
                } else
                {
                    request.coachName = coach.name;
                }
            }
            Requests = new ObservableRangeCollection<Request>(result.OrderBy(request => request.status));
            OnPropertyChanged("Requests");
        }

        public async Task Cancel(Request request)
        {
            Requests.Remove(request);
            bool response = await networkService.DeleteAsync(APIConstants.DeleteRequestURI(request.id));
            if (response)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "The request to this coach was canceled", "OK");
                GetRequests();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Could not cancel request to coach", "OK");
            }
        }





    }
    }
