using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public WorkoutViewModel()
        {
            clientId = 1;
            Day = DateTime.Now.Date;
            EditCommand = new AsyncCommand<int>(EditWorkout);
            AddCommand = new AsyncCommand(AddWorkout);

            GetWorkouts();

        }

        public async Task GetWorkouts()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<WorkoutDTO>>(APIConstants.GetWorkoutsByClientId(clientId));
            if (result.Count == 0)
            {
                return;
            }

            Workouts = new ObservableRangeCollection<WorkoutDTO>(result.Where(x => x.day.ToString("MMMM dd, yyyy") == Day.ToString("MMMM dd, yyyy")));
            OnPropertyChanged("Workouts");
        }
        private async Task AddWorkout()
        {
            var route = $"{nameof(AddWorkoutPage)}";
            await Shell.Current.GoToAsync(route);
        }

        /** 
         
            <FlexLayout BackgroundColor="Gray" Padding="5">
                <Button Text="12" BackgroundColor="Red" Margin="5" CornerRadius="100"/>
                <Button Text="12 Mon" BackgroundColor="Red" Margin="5" CornerRadius="100"/>
                <Button Text="12 Mon" BackgroundColor="Red" Margin="5" CornerRadius="100"/>
                <Button Text="12 Mon" BackgroundColor="Red" Margin="5" CornerRadius="100"/>
                <Button Text="12 Mon" BackgroundColor="Red" Margin="5" CornerRadius="100"/>
                <Button Text="12 Mon" BackgroundColor="Red" Margin="5" CornerRadius="100"/>
                <Button Text="12 Mon" BackgroundColor="Red" Margin="5" CornerRadius="100"/>
            </FlexLayout>
         
         */

        private async Task EditWorkout(int id)
        {
                var route = $"{nameof(EditWorkoutPage)}?workoutId={id}";
                await Shell.Current.GoToAsync(route);
            
        }
    }
}
