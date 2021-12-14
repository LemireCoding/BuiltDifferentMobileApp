using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels
{
    public class EditWorkoutViewModel : ViewModelBase
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
        public Workout EditingWorkout { get; set; }
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


        public List<WorkoutType> Types { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public AsyncCommand SaveCommand { get; }
        public EditWorkoutViewModel(int id)
        {
            this.id = id;
            getCurrentWorkout(id);
            SaveCommand = new AsyncCommand(SaveWorkout);

            Types = new List<WorkoutType>
        {

            new WorkoutType("Warm Up"),
            new WorkoutType("Cardio"),
            new WorkoutType("Weight Training")
        };

        }

        private async Task getCurrentWorkout(int id)
        {
            var result = await networkService.GetAsync<Workout>(APIConstants.GetWorkoutsByWorkoutId(id));
            if (result == null)
            {
                await Application.Current.MainPage.DisplayAlert("Workout Not Found", "Please try again", "OK");
            }

            EditingWorkout = new Workout(result.coachId, result.clientId, result.workoutType, result.workoutName, result.sets, result.reps, result.duration, result.restTime, result.day, result.description, result.isCompleted, result.videoLink);

            WorkoutName = EditingWorkout.workoutName;
            Types.Add(new WorkoutType(EditingWorkout.workoutType));
            WorkoutType = Types[Types.Count - 1];
            Sets = EditingWorkout.sets.ToString();
            Reps = EditingWorkout.reps.ToString();
            Duration = EditingWorkout.duration.ToString();
            RestTime = EditingWorkout.restTime.ToString();
            Description = EditingWorkout.description;
            IsCompleted = EditingWorkout.isCompleted;
            VideoLink = EditingWorkout.videoLink;
            Day = EditingWorkout.day;
            OnPropertyChanged("EditingWorkout");
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
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields", "OK");
                return;
            }

            var workout = new Workout(1, 1, WorkoutType.Name, WorkoutName, Convert.ToInt32(Sets), Convert.ToInt32(Reps), Convert.ToInt32(Duration), Convert.ToInt32(RestTime), Day, Description, isCompleted, VideoLink);
            var result = await networkService.PutAsync(APIConstants.UpdateWorkoutByWorkoutId(id), workout);
            var httpCode = result.StatusCode;

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                await Application.Current.MainPage.DisplayAlert("Good", "Workout Saved", "OK");
            }
        }
    }
    }
