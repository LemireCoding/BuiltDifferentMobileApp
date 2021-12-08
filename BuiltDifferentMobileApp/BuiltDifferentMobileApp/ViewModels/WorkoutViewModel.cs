using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
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
        public AsyncCommand AddCommand { get; }
        public AsyncCommand EditCommand { get; }
        public ObservableRangeCollection<Workout> Workouts { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public WorkoutViewModel()
        {
            EditCommand = new AsyncCommand(EditWorkout);
            AddCommand = new AsyncCommand(AddWorkout);

        }

        private async Task AddWorkout()
        {
            var route = $"{nameof(ManageWorkoutPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task EditWorkout()
        {
            var result = await networkService.GetAsync(APIConstants.GetWorkoutsUri());
            var httpCode = result.StatusCode;
            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                var route = $"{nameof(ManageWorkoutPage)}?clientId={clientId}";
                await Shell.Current.GoToAsync(route);
            }
        }
    }
}
