using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach {
    public class CoachDashboardPageViewModel : ViewModelBase {

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        private List<Models.Client> originalClients { get; set; }
        public ObservableRangeCollection<Models.Client> Clients { get; set; }

        private string searchTerm;
        public string SearchTerm {
            get => searchTerm;
            set {
                SetProperty(ref searchTerm, value);
                FilterResults();
            }
        }

        public AsyncCommand ViewMyProfileCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Models.Client> ViewClientsBoardCommand { get; }

        public CoachDashboardPageViewModel() {

            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
            RefreshCommand = new AsyncCommand(FetchClients);
            ViewClientsBoardCommand = new AsyncCommand<Models.Client>(ViewClientsBoard);

            FetchClients();
        }

        private void FilterResults() {
            if(originalClients == null) return;

            if(string.IsNullOrWhiteSpace(SearchTerm)) {
                Clients.ReplaceRange(originalClients);
                return;
            }

            Clients.ReplaceRange(originalClients.Where(c => c.name.ToLower().Contains(SearchTerm.ToLower().Trim())));
        }

        private async Task ViewClientsBoard(Models.Client client) {
            if(client == null || IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(CoachMenuPage)}?ClientId={client.id}&ClientName={client.name}");
        }

        public async Task FetchClients() {
            IsBusy = true;

            int coachId = ((Models.Coach)accountService.CurrentUser).id;

            List<Models.Client> clients = await networkService.GetAsync<List<Models.Client>>(APIConstants.GetClientsForCoachId(coachId));

            if(clients.Count != 0) {
                originalClients = clients;
                Clients = new ObservableRangeCollection<Models.Client>(clients);
            } else {
                originalClients = null;
                Clients = new ObservableRangeCollection<Models.Client>();
            }

            FilterResults();

            OnPropertyChanged("Clients");
            IsBusy = false;
        }

    }
}
