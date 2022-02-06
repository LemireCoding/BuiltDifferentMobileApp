using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
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
    public class ClientWorkoutViewModel : ViewModelBase
    {
        private IAccountService accountService = AccountService.Instance;
        private int clientId;
        public AsyncCommand<int> MarkDone { get; }
        public ObservableRangeCollection<WorkoutDTO> Workouts { get; set; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<int> EditCommand { get; }
        private DateTime day;
        public DateTime Day
        {
            get => day;
            set
            {
                SetProperty(ref day, value);
                GetWorkouts();

            }
        }

        public string WorkoutPageTitle { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public ClientWorkoutViewModel()
        {
            MarkDone = new AsyncCommand<int>(Done);
            WorkoutPageTitle = $"Your Workout Plan";
            var user = (Models.Client)accountService.CurrentUser;
            this.clientId = user.id;
            Day = DateTime.Now.Date;
          

            GetWorkouts();
        }

        public async Task GetWorkouts()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<WorkoutDTO>>(APIConstants.GetWorkoutsByClientId(clientId));
            if (result == null||result.Count == 0 )
            {
                return;
            }
            Workouts = new ObservableRangeCollection<WorkoutDTO>(result.Where(x => x.day.ToString("MMMM dd, yyyy") == Day.ToString("MMMM dd, yyyy")));
            OnPropertyChanged("Workouts");
        }



        public async Task Done(int id)
        {

            var route = APIConstants.GetWorkoutsByWorkoutId(id);
            var workout = await networkService.GetAsync<Workout>(route);

            if (workout.isCompleted == false)
            {
                workout.isCompleted = true;
                var test = JsonConvert.SerializeObject(workout);
                var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.UpdateWorkoutByWorkoutId(id), workout);
                var httpCode = result.StatusCode;

                if (httpCode == System.Net.HttpStatusCode.OK)
                {

                    await Application.Current.MainPage.DisplayAlert("Good", "Workout set as done", "OK");
                    GetWorkouts();


                }
                else
                {
                    return;
                }
            }
            else
            {
                workout.isCompleted = false;
                var test = JsonConvert.SerializeObject(workout);
                var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.UpdateWorkoutByWorkoutId(id), workout);
                var httpCode = result.StatusCode;

                if (httpCode == System.Net.HttpStatusCode.OK)
                {

                    await Application.Current.MainPage.DisplayAlert("Good", "Workout set as not done", "OK");
                    GetWorkouts();


                }
                else
                {
                    return;
                }
            }




        }
    }
}
