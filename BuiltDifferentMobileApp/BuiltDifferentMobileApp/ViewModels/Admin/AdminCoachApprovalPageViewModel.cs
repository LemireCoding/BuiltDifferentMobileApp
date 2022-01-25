using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views.Admin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Admin {
    public class AdminCoachApprovalPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public ObservableRangeCollection<Models.Coach> Coaches { get; set; }
        public Models.Coach SelectedCoach { get; set; }

        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand CoachSelectedCommand { get; set; }

        public AdminCoachApprovalPageViewModel() {

            RefreshCommand = new AsyncCommand(FetchPendingCoaches);
            CoachSelectedCommand = new AsyncCommand(CoachSelected);

            FetchPendingCoaches();
        }

        private async Task CoachSelected() {
            if(SelectedCoach == null) return;

            var route = $"{nameof(AdminCoachApprovalProfilePage)}?CoachId={SelectedCoach.id}";
            await Shell.Current.GoToAsync(route);

            SelectedCoach = null;
        }

        public async Task FetchPendingCoaches() {
            IsBusy = true;

            var coaches = await networkService.GetAsync<List<Models.Coach>>(APIConstants.GetPendingCoachesUri());

            if(coaches.Count > 0) {
                Coaches = new ObservableRangeCollection<Models.Coach>(coaches);
            } else {
                Coaches = new ObservableRangeCollection<Models.Coach>();
            }

            OnPropertyChanged("Coaches");

            IsBusy = false;
        }

    }
}
