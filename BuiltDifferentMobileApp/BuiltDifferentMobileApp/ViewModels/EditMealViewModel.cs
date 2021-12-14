using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels
{
    public class EditMealViewModel: ViewModelBase
    {
        private int id;
        private string mealName;
        public string MealName { get => mealName; set => SetProperty(ref mealName, value);}
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
        public string ImageLink { get => imageLink; set => SetProperty(ref imageLink, value) ; }
        private DateTime day;
        public DateTime Day { get => day; set => SetProperty(ref day, value); }
        public AsyncCommand SaveCommand { get; }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;


        public EditMealViewModel(int id)
        {
            this.id = id;
            Title = "Edit Meal";
            FetchInfo();
            SaveCommand = new AsyncCommand(Save);
           
        }

        private async void FetchInfo()
        {
            var route = APIConstants.GetMealByIdUri(id);
            var meal = await networkService.GetAsync<MealDTO>(route);
            MealName = meal.mealName;
            MealType = meal.mealType;
            Calories = meal.calories;
            Protein = meal.protein;
            Carbs = meal.carbs;
            Fat = meal.fat;
            Recipe = meal.recipe;
            ImageLink = meal.imageLink;
            Day = meal.day;
        }

        public async Task Save()
        {
            if (string.IsNullOrEmpty(MealName) || MealType.ToString().Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields", "OK");
                return;
            }

            //default ids inserted for now
            //empty strings for receipe and image link
            var meal = new MealDTO(1, 0, MealName, MealType.Name.ToString(), Calories, Protein, Carbs, Fat, "", "", Day, false);
            var test = JsonConvert.SerializeObject(meal);
            var result = await networkService.UpdateAsync<Meal>(APIConstants.PutMealUri(id), meal);

            if (result != null)
            {
                await Application.Current.MainPage.DisplayAlert("Good", "Meal Updated", "OK");
                await AppShell.Current.GoToAsync("..");

            }
            else
                return;

        }

       

    }
}
