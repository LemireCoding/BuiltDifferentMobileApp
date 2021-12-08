using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels
{
    public class WorkoutManageViewModel:ViewModelBase
    {
        public int WorkoutId { get; set; }
        public int CoachId { get; set; }
        public int ClientId { get; set; }
        public string WorkoutType { get; set; }
        public string WorkoutName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
        public int Duration { get; set; }
        public int RestTime { get; set; }
        public DateTime Day { get; set; }

        //Optional fields below
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string VideoLink { get; set; }


        public List<WorkoutType> Types { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public AsyncCommand SaveCommand { get; }
        public WorkoutManageViewModel()
        {
            SaveCommand = new AsyncCommand(SaveWorkout);

            Types=new List<WorkoutType>
        {
            new WorkoutType("Warm Up"),
            new WorkoutType("Cardio"),
            new WorkoutType("Weight Training")
        };
    }

        private async Task SaveWorkout()
        {
            if (string.IsNullOrEmpty(WorkoutName) || string.IsNullOrEmpty(WorkoutType))
            {
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields", "OK");
                return;
            }

            var workout = new Workout();
            var test = JsonConvert.SerializeObject(workout);
            var result = await networkService.PostAsync(APIConstants.PostWorkoutUri(), workout);
            var httpCode = result.StatusCode;

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                await Application.Current.MainPage.DisplayAlert("Good", "Vet Saved", "OK");
            }
        }

    }
}
