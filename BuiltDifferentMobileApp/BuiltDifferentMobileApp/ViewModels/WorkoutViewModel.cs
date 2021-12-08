using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
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
    public class WorkoutViewModel:ViewModelBase
    {
        private int clientId;
        private int workoutId;
        public AsyncCommand AddCommand { get; }
        public AsyncCommand EditCommand { get; }
        public AsyncCommand WeekdayCommand { get; }
        public ObservableRangeCollection<Workout> Workouts { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public WorkoutViewModel()
        {

            EditCommand = new AsyncCommand(EditWorkout);
            AddCommand = new AsyncCommand(AddWorkout);
            WeekdayCommand = new AsyncCommand(WeekdayDisplay);
            //Mock data for View
            clientId = 1;
            Workouts = new ObservableRangeCollection<Workout>()
            {
                new Workout(111,121,123,"Cardio", "Running", 3,10,0,20,120,new DateTime(2020, 12, 22),"description", false,"http://link.com"),
                 new Workout(222,212,123,"Weight Training", "Shrugs", 1,20,0,30,120,new DateTime(2020, 12, 23),"description", false,"http://link.com")
            };
        }

        private async Task WeekdayDisplay()
        {
            //will need to add field day of week to retrieve in uri 
            var result = await networkService.GetAsync(APIConstants.GetWorkoutsUri(this.clientId));
            var httpCode = result.StatusCode;
            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                string serialized = await result.Content.ReadAsStringAsync();
                Workouts = new ObservableRangeCollection<Workout>(JsonConvert.DeserializeObject<List<Workout>>(serialized));
                OnPropertyChanged("Workouts");
            }

        }
            

        private async Task AddWorkout()
        {
            var route = $"{nameof(ManageWorkoutPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task EditWorkout()
        {
            var result = await networkService.GetAsync(APIConstants.GetWorkoutsUri(clientId));
            var httpCode = result.StatusCode;
            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                var route = $"{nameof(ManageWorkoutPage)}?WorkoutId={workoutId}";
                await Shell.Current.GoToAsync(route);
            }
        }
    }
}
