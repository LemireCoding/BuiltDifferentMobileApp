using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class ClientMealViewModel : ViewModelBase
    {
        private int clientId;
        public AsyncCommand<int> MarkEaten { get; }
        public AsyncCommand<int> ShowRecipe { get; }
        public ObservableRangeCollection<Meal> Meals { get; set; }
        public ObservableRangeCollection<Grouping<string, Meal>> MealGroups { get; set; }
        private Meal selectedMeal;
        public Meal SelectedMeal
        {
            get => selectedMeal;
            set => SetProperty(ref selectedMeal, value);
        }

        private DateTime day;
        public DateTime Day
        {
            get => day;
            set
            {
                SetProperty(ref day, value);
                GetMeals();
            }
        }

        public string MealPageTitle { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        private IAccountService accountService = AccountService.Instance;

        public ClientMealViewModel()
        {
            MarkEaten = new AsyncCommand<int>(Eaten);
            ShowRecipe = new AsyncCommand<int>(Recipe);
            var user = (Models.Client)accountService.CurrentUser;
            this.clientId = user.id;
            Day = DateTime.Now.Date;

            MealGroups = new ObservableRangeCollection<Grouping<string, Meal>>();
            GetMeals();
        }

        public void CreateMealGroups()
        {

            MealGroups.Clear();

            MealGroups.Add(new Grouping<string, Meal>("Breakfast", Meals.Where(x =>
                x.mealType.Contains("Breakfast"))));
            MealGroups.Add(new Grouping<string, Meal>("Lunch", Meals.Where(x =>
                x.mealType.Contains("Lunch"))));
            MealGroups.Add(new Grouping<string, Meal>("Dinner", Meals.Where(x =>
                x.mealType.Contains("Dinner"))));
            MealGroups.Add(new Grouping<string, Meal>("Snack", Meals.Where(x =>
                x.mealType.Contains("Snack"))));

            OnPropertyChanged("MealGroups");


        }

        public async Task GetMeals()
        {

            var result = await networkService.GetAsync<ObservableRangeCollection<Meal>>(APIConstants.GetMealsByClientId(clientId));
            if (result.Count == 0)
            {
                return;
            }
            Meals = new ObservableRangeCollection<Meal>(result.Where(x => x.day.ToString("MMMM dd, yyyy") == Day.ToString("MMMM dd, yyyy")));
            CreateMealGroups();
            OnPropertyChanged("Meals");
        }

        public async Task Eaten(int id)
        {

            var route = APIConstants.GetMealByIdUri(id);
            var meal = await networkService.GetAsync<Meal>(route);

            if(meal.isEaten == false)
            {
                meal.isEaten = true;
                var test = JsonConvert.SerializeObject(meal);
                var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.PutMealUri(id), meal);
                var httpCode = result.StatusCode;

                if (httpCode == System.Net.HttpStatusCode.OK)
                {
                    
                    await Application.Current.MainPage.DisplayAlert("Good", "Meal Eaten", "OK");
                    GetMeals();


                }
                else
                {
                    return;
                }
            }
            else
            {
                meal.isEaten = false;
                var test = JsonConvert.SerializeObject(meal);
                var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.PutMealUri(id), meal);
                var httpCode = result.StatusCode;

                if (httpCode == System.Net.HttpStatusCode.OK)
                {
                    
                    await Application.Current.MainPage.DisplayAlert("Good", "Meal Not Eaten", "OK");
                    GetMeals();


                }
                else
                {
                    return;
                }
            }
           

           
           
        }


        public async Task Recipe(int id)
        {

            var route = $"{nameof(RecipePage)}?mealId={id}";

            await Shell.Current.GoToAsync(route);



        }

    }
}
