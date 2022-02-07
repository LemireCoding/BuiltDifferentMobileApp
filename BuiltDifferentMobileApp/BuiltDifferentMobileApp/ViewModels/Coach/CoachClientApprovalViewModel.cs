using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach
{
    public class CoachClientApprovalViewModel : ViewModelBase
    {
        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public ObservableRangeCollection<Models.RequestClientInfo> ClientRequests { get; set; }
        public Models.Request SelectedRequest { get; set; }

        public string DenyButtonText { get; set; }
        public string AcceptButtonText { get; set; }

        public AsyncCommand RefreshCommand { get; set; }
        public string ButtonText { get; set; }
        public int ClientId { get; set; }
        public int CoachId { get; set; }
        public string ClientName { get; set; }

        public AsyncCommand<Models.RequestClientInfo> RespondToClientRequestAcceptCommand { get; set; }
        public AsyncCommand<Models.RequestClientInfo> RespondToClientRequestDenyCommand { get; set; }

        public CoachClientApprovalViewModel()
        {

            //FIGURE WHY its sending a 1 for coachId

            this.CoachId = ((Models.Coach)accountService.CurrentUser).id;
            ClientName = "";
            RefreshCommand = new AsyncCommand(FetchPendingRequests);
            RespondToClientRequestAcceptCommand = new AsyncCommand<Models.RequestClientInfo>(RespondToClientRequestAccept);
            RespondToClientRequestDenyCommand = new AsyncCommand<Models.RequestClientInfo>(RespondToClientRequestDeny);
            FetchPendingRequests();
        }

        private async Task RespondToClientRequestAccept(Models.RequestClientInfo request)
        {
            int response;
            var accepted = await Application.Current.MainPage.DisplayAlert("Accept Client", "Are you sure you want to accept this client?", "Confirm", "Cancel");

            if (!accepted) return;

            response = (int)await networkService.PutAsyncHttpResponseMessage(APIConstants.UpdateApproveDenyRequestUri(request.clientId), new ClientApprovalStatus(ApprovalConstants.APPROVED));

            if (response >= 200 && response <= 299)
            {
                await Application.Current.MainPage.DisplayAlert("Client Request Accepted", "Get Coaching! you have a new client.", "OK");
                FetchPendingRequests();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"Server Error ({response})", "Could not accept/deny client. Please try again.", "OK");
            }
        }

        private async Task RespondToClientRequestDeny(Models.RequestClientInfo request)
        {
            int response;
            var accepted = await Application.Current.MainPage.DisplayAlert("Deny Client", "Are you sure you want to deny this client?", "Confirm", "Cancel");

            if (!accepted) return;

            response = (int)await networkService.PutAsyncHttpResponseMessage(APIConstants.UpdateApproveDenyRequestUri(request.clientId), new ClientApprovalStatus(ApprovalConstants.DENIED));

            if (response >= 200 && response <= 299)
            {
                await Application.Current.MainPage.DisplayAlert("Client Request Denied", "We'll let the client know that you cannot accept his request at this time.", "OK");
                FetchPendingRequests();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"Server Error ({response})", "Could not accept/deny client. Please try again.", "OK");
            }
        }

    public async Task FetchPendingRequests()
        {
            IsBusy = true;

            var requests = await networkService.GetAsync<List<Models.RequestClientInfo>>(APIConstants.GetPendingRequestsUri(CoachId));

            if (requests == null)
            {
                ClientRequests = new ObservableRangeCollection<Models.RequestClientInfo>();
            }
            else if (requests.Count > 0)
            {
                ClientRequests = new ObservableRangeCollection<Models.RequestClientInfo>(requests);
            }
            else
            {
                ClientRequests = new ObservableRangeCollection<Models.RequestClientInfo>();
            }

            OnPropertyChanged("ClientRequests");

            IsBusy = false;
        }
    }
}
