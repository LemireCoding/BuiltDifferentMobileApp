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
    public class EditMealViewModel: ViewModelBase
    {
        private int id;
        private string mealName;
        public string MealName { get => mealName; set => SetProperty(ref mealName, value);}
        private string mealType;
        public string MealType { get; set; }
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
        public ObservableRangeCollection<string> Types { get; set; }

        public EditMealViewModel(int id)
        {
            Title = "Edit Meal";
            this.id = id;
            FetchInfo();
            SaveCommand = new AsyncCommand(Save);

            Types = new ObservableRangeCollection<string>
        {
            "Breakfast",
            "Lunch",
            "Dinner",
            "Snack"
        };

        }

        private async void FetchInfo()
        {
            var route = APIConstants.GetMealByIdUri(id);
            var meal = await networkService.GetAsync<Meal>(route);
            MealName = meal.mealName;
            MealType = meal.mealType.ToString();
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
            if (string.IsNullOrEmpty(MealName) || string.IsNullOrEmpty(MealType))
            {
                await Application.Current.MainPage.DisplayAlert("Field Issue", "Please fill ALL of the fields", "OK");
                return;
            }

            //default client/coach ids inserted for now

            //filled string for image link otherwill fail
            var meal = new Meal(id,2, 1, MealName, MealType, Calories, Protein, Carbs, Fat, "Reciepe" , "imageLink", Day, false);
            var test = JsonConvert.SerializeObject(meal);
            var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.PutMealUri(id), meal);
            var httpCode = result.StatusCode;

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                await Application.Current.MainPage.DisplayAlert("Good", "Meal Updated", "OK");
                await AppShell.Current.GoToAsync("..");
            }

            else
                return;
        }
    }
}
