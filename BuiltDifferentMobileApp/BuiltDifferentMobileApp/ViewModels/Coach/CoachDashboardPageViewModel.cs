using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views.Coach;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach {
    public class CoachDashboardPageViewModel : ViewModelBase {

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        private int coachId;

        public ObservableRangeCollection<Models.Client> Clients { get; set; }

        public AsyncCommand ViewMyProfileCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Models.Client> ViewClientsBoardCommand { get; }

        public CoachDashboardPageViewModel() {
            Title = "Clients";

            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
            RefreshCommand = new AsyncCommand(FetchClients);
            ViewClientsBoardCommand = new AsyncCommand<Models.Client>(ViewClientsBoard);

            coachId = ((Models.Coach)accountService.CurrentUser).id;

            FetchClients();
        }

        private async Task ViewClientsBoard(Models.Client client) {
            if(client == null || IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(CoachMenuPage)}?ClientId={client.id}&ClientName={client.name}");
        }

        public async Task FetchClients() {
            IsBusy = true;

            List<Models.Client> clients = await networkService.GetAsync<List<Models.Client>>(APIConstants.GetClientsForCoachId(coachId));

            if(clients.Count != 0) {
                Clients = new ObservableRangeCollection<Models.Client>(clients);
            } else {
                Clients = new ObservableRangeCollection<Models.Client>();
            }

            OnPropertyChanged("Clients");
            IsBusy = false;
        }

    }
}
