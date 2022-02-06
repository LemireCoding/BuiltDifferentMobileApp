using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach
{
    public class AddWorkoutViewModel:ViewModelBase
    {
        private int id;
        private int coachId;
        private int clientId;
        private WorkoutType workoutType;
        private string workoutName;
        private string sets;
        private string reps;
        private string weight;
        private string duration;
        private string restTime;
        private DateTime day;
        private string description;
        private bool isCompleted;
        private string videoLink;
       
        public WorkoutType WorkoutType { get => workoutType; set => SetProperty(ref workoutType, value); }
        public string WorkoutName { get => workoutName; set => SetProperty(ref workoutName, value); }
        public string Sets { get => sets; set => SetProperty(ref sets, value); }
        public string Reps { get => reps; set => SetProperty(ref reps, value); }
      
        public string Duration { get => duration; set => SetProperty(ref duration, value); }
        public string RestTime { get => restTime; set => SetProperty(ref restTime, value); }
        public DateTime Day { get => day; set => SetProperty(ref day, value); }

        //Optional fields below
        public string Description { get => description; set => SetProperty(ref description, value); }
        public bool IsCompleted { get => isCompleted; set => SetProperty(ref isCompleted, value); }
        public string VideoLink { get => videoLink; set => SetProperty(ref videoLink, value); }

        private IAccountService accountService = AccountService.Instance;
        public List<WorkoutType> Types { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public AsyncCommand SaveCommand { get; }
        public AddWorkoutViewModel(int clientId)
        {
            this.clientId = clientId;
            var user = (Models.Coach)accountService.CurrentUser;
            coachId = user.id;
            SaveCommand = new AsyncCommand(SaveWorkout);
            Day = DateTime.Now.Date;
            VideoLink = "";
            IsCompleted = false;
            Types = new List<WorkoutType>
        {
           
            new WorkoutType(AppResource.AddWorkoutTypeWarmUp),
            new WorkoutType(AppResource.AddWorkoutTypeCardio),
            new WorkoutType(AppResource.AddWorkoutTypeWeightTraining)
        };
            
    }

        private async Task SaveWorkout()
        {
            if (string.IsNullOrEmpty(WorkoutName)
                || string.IsNullOrEmpty(WorkoutType.Name)
                || string.IsNullOrEmpty(Sets)
                || string.IsNullOrEmpty(Reps)
                || string.IsNullOrEmpty(Duration)
                || string.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelFieldIssueTitle, AppResource.ViewModelFieldIssueMessage, "OK");
                return;
            }

            if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("fr"))
            {
                if (WorkoutType.Name == "Échauffement")
                    WorkoutType.Name = "Warm Up";
                else if (WorkoutType.Name == "Cardio")
                    WorkoutType.Name = "Cardio";
                else if (WorkoutType.Name == "Entraînement de musculation")
                    WorkoutType.Name = "Weight Training";
            }

            var workout = new Workout(coachId, clientId, WorkoutType.Name, WorkoutName, Convert.ToInt32(Sets), Convert.ToInt32(Reps), Convert.ToInt32(Duration), Convert.ToInt32(RestTime),Day,Description, IsCompleted,VideoLink);
            var result = await networkService.PostAsync<HttpResponseMessage>(APIConstants.PostWorkoutUri(), workout);
            var httpCode = result.StatusCode;

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelSuccessTitle, AppResource.AddWorkoutSaved, "OK");
                await AppShell.Current.GoToAsync("..");
            }
            else if (httpCode == System.Net.HttpStatusCode.InternalServerError)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelErrorTitle, AppResource.ViewModelErrorMessage, "OK");
            }
            else
                return;
        }

    }
}
