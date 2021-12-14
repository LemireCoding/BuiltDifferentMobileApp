using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.ViewModels
{
    public class EditMealViewModel: ViewModelBase
    {
        private int id;
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
        private string imageLink;
        public string ImageLink { get => imageLink; set => SetProperty(ref imageLink, value); }
        private DateTime day;
        public DateTime Day { get => day; set => SetProperty(ref day, value); }
        public AsyncCommand SaveCommand { get; }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;


        public EditMealViewModel(int id)
        {
            id = this.id;
            Title = "Edit Meal";
            FetchInfo();
            SaveCommand = new AsyncCommand(Save);

        }

        private async void FetchInfo()
        {
            var route = APIConstants.GetMealByIdUri(id);
            var meal = await networkService.GetAsync<Meal>(route);
            mealName = meal.mealName;
            calories = meal.calories;
            protein = meal.protein;
            carbs = meal.carbs;
            fat = meal.fat;
            recipe = meal.recipe;
            imageLink = meal.imageLink;
            day = meal.day;    
        }

        public async Task Save()
        {


            await AppShell.Current.GoToAsync("..");

        }

    }
}
