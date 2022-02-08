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

        private List<Models.ClientWithProgress> originalClients { get; set; }
        public ObservableRangeCollection<Models.ClientWithProgress> ClientsOnBoard { get; set; }

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
        public AsyncCommand<Models.ClientWithProgress> ViewClientsBoardCommand { get; }

        public CoachDashboardPageViewModel() {

            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
            RefreshCommand = new AsyncCommand(FetchClients);
            ViewClientsBoardCommand = new AsyncCommand<Models.ClientWithProgress>(ViewClientsBoard);

            FetchClients();
        }

        private void FilterResults() {
            if(originalClients == null) return;

            if(string.IsNullOrWhiteSpace(SearchTerm)) {
                ClientsOnBoard.ReplaceRange(originalClients);
                return;
            }

            ClientsOnBoard.ReplaceRange(originalClients.Where(c => c.name.ToLower().Trim().Contains(SearchTerm.ToLower().Trim())));
        }

        private async Task ViewClientsBoard(Models.ClientWithProgress client) {
            if(client == null || IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(CoachMenuPage)}?ClientId={client.id}&ClientName={client.name}");
        }

        public async Task FetchClients() {
            IsBusy = true;

            int coachId = ((Models.Coach)accountService.CurrentUser).id;

            List<Models.Client> clients = await networkService.GetAsync<List<Models.Client>>(APIConstants.GetClientsForCoachId(coachId));

            if(clients == null) {
                originalClients = null;
                ClientsOnBoard = new ObservableRangeCollection<Models.ClientWithProgress>();
            }
            else if(clients.Count != 0) {

                List<Models.ClientWithProgress> orig = new List<Models.ClientWithProgress>();
                List<Models.ClientWithProgress> onBoard = new List<Models.ClientWithProgress>();

                foreach (Models.Client client in clients)
                {
                    int completedMeals = 0;
                    int totalMeals = 0;
                    int completedWorkouts = 0;
                    int totalWorkouts = 0;

                    List<Models.Workout> workoutsByClient = await networkService.GetAsync<List<Models.Workout>>(APIConstants.GetWorkoutsByClientId(client.id));
                    if (workoutsByClient == null)
                    {
                        totalWorkouts = 0;
                        completedWorkouts = 0;
                    }
                    else if(workoutsByClient.Count != 0)
                    {
                        totalWorkouts = workoutsByClient.Count(workout => (workout.day).ToString("yyyy-MM-dd") == DateTime.Now.Date.ToString("yyyy-MM-dd"));
                        completedWorkouts = workoutsByClient.Count(workout => (workout.isCompleted == true) && ((workout.day).ToString("yyyy-MM-dd") == DateTime.Now.Date.ToString("yyyy-MM-dd")));
                    }

                    List<Models.Meal> mealsByClient = await networkService.GetAsync<List<Models.Meal>>(APIConstants.GetMealsByClientId(client.id));
                    if (mealsByClient == null)
                    {
                        totalMeals = 0;
                        completedMeals = 0;
                    }else if (mealsByClient.Count != 0)
                    {
                        totalMeals = mealsByClient.Count(meal => (meal.day).ToString("yyyy-MM-dd") == DateTime.Now.Date.ToString("yyyy-MM-dd"));
                        completedMeals = mealsByClient.Count(meal => (meal.isEaten == true) && ((meal.day).ToString("yyyy-MM-dd") == DateTime.Now.Date.ToString("yyyy-MM-dd")));
                    }

                    
                    
                    int totalTasks = totalWorkouts + totalMeals;
                    int completedTasks = completedMeals + completedWorkouts;

                    string percentage = "";
                    if(totalTasks != 0)
                    {
                        percentage = ((completedTasks * 100) / totalTasks).ToString() + " %";
                    } else
                    {
                        percentage = "NaN";
                    }
                    

                    Models.ClientWithProgress clientWithProgress = new Models.ClientWithProgress(
                        client.id,
                        client.name,
                        client.userId,
                        client.coachId,
                        client.waitingApproval,
                        client.startWeight,
                        client.currentWeight,
                        client.profilePicture,
                        percentage);

                    orig.Add(clientWithProgress);
                    onBoard.Add(clientWithProgress);
                
                }
                originalClients = orig;
                ClientsOnBoard = new ObservableRangeCollection<Models.ClientWithProgress>(onBoard);


            } else {
                originalClients = null;
               
                ClientsOnBoard = new ObservableRangeCollection<Models.ClientWithProgress>();
            }

            FilterResults();
            
         
            OnPropertyChanged("ClientsOnBoard");
            IsBusy = false;
        }

    }
}
