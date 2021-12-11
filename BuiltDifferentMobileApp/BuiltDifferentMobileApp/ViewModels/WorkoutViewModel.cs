﻿using BuiltDifferentMobileApp.Models;
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
        public ObservableRangeCollection<Workout> Workouts { get; set; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand EditCommand { get; }
       

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public WorkoutViewModel()
        {
            clientId = 1;
            EditCommand = new AsyncCommand(EditWorkout);
            AddCommand = new AsyncCommand(AddWorkout);

            GetWorkouts();

        }

        private async Task GetWorkouts()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<Workout>>(APIConstants.GetWorkoutsByClientId(clientId));
            if (result.Count == 0)
            {
                return;
            }

            Workouts = new ObservableRangeCollection<Workout>(result);
            OnPropertyChanged("Workouts");
        }
        private async Task AddWorkout()
        {
            var route = $"{nameof(ManageWorkoutPage)}";
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

        private async Task EditWorkout()
        {
            var result = await networkService.GetAsync<Workout>(APIConstants.GetWorkoutsUri());
            
            if (result != null)
            {
                var route = $"{nameof(ManageWorkoutPage)}?clientId={clientId}";
                await Shell.Current.GoToAsync(route);
            }
        }
    }
}
