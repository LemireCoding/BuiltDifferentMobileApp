using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using MvvmHelpers.Commands;
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
    public class AddMealViewModel : ViewModelBase
    {
        private string mealName;
        public string MealName { get => mealName; set => SetProperty(ref mealName, value); }
        private MealType mealType;
        public MealType MealType { get; set; }
        private double calories;
        public double Calories { get => calories; set => SetProperty(ref calories, value); }
        private double protein;
        public double Protein { get => protein; set => SetProperty(ref protein, value); }
        private double carbs;
        public double Carbs { get => carbs; set => SetProperty(ref carbs, value); }
        private double fat;
        public double Fat { get => fat; set => SetProperty(ref fat, value); }
        private string recipe;
        public string Recipe { get => recipe; set => SetProperty(ref recipe, value); }
        private DateTime day;
        public DateTime Day { get => day; set => SetProperty(ref day, value); }
        private string imageLink;
        public string ImageLink { get => imageLink; set => SetProperty(ref imageLink, value); }
        public Xamarin.CommunityToolkit.ObjectModel.AsyncCommand SaveCommand { get; }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public ObservableRangeCollection<MealType> Types { get; set; }
        public AddMealViewModel()
        {

            Title = "Add Meal";
            SaveCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand(Save);
            Types = new ObservableRangeCollection<MealType>
        {
            new MealType("Breakfast"),
            new MealType("Lunch"),
            new MealType("Dinner"),
            new MealType("Snack")
        };

        }

        public async Task Save()
        {
            if (string.IsNullOrEmpty(MealName)|| MealType.ToString().Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields", "OK");
                return;
            }
            //default ids inserted for now
            //empty strings for receipe and image link
            var meal = new MealDTO(1,0,MealName, MealType.Name, Calories, Protein, Carbs, Fat, "", "", day, false);
            var test = JsonConvert.SerializeObject(meal);
            var result = await networkService.PostAsync<Workout>(APIConstants.PostMealUri(), meal);

            if (result != null)
            {
                await Application.Current.MainPage.DisplayAlert("Good", "Meal Saved", "OK");
                await AppShell.Current.GoToAsync("..");

            }
            else
                return; 
        }
    }
}
