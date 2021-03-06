using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
using BuiltDifferentMobileApp.Views.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach
{
    public class CoachWorkoutViewModel:ViewModelBase
    {
        private int clientId;
        public ObservableRangeCollection<WorkoutDTO> Workouts { get; set; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand <int>EditCommand { get; }
        private DateTime day;
        public DateTime Day {
            get => day;
            set
            {
                SetProperty(ref day, value);
                GetWorkouts();
                
            }
        }

        public string WorkoutPageTitle { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public CoachWorkoutViewModel(string clientName, int clientId)
        {
            WorkoutPageTitle = $"{clientName}";
            this.clientId = clientId;
            Day = DateTime.Now.Date;
            EditCommand = new AsyncCommand<int>(EditWorkout);
            AddCommand = new AsyncCommand(AddWorkout);

            GetWorkouts();
        }

        public async Task GetWorkouts()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<WorkoutDTO>>(APIConstants.GetWorkoutsByClientId(clientId));
            if (result == null || result.Count == 0)
            {
                return;
            }

            Workouts = new ObservableRangeCollection<WorkoutDTO>(result.Where(x => x.day.ToString("MMMM dd, yyyy") == Day.ToString("MMMM dd, yyyy")));
            OnPropertyChanged("Workouts");
        }
        private async Task AddWorkout()
        {
            var route = $"{nameof(AddWorkoutPage)}?clientId={clientId}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task EditWorkout(int id)
        {
                var route = $"{nameof(EditWorkoutPage)}?workoutId={id}";
                await Shell.Current.GoToAsync(route);
            
        }
    }
}
