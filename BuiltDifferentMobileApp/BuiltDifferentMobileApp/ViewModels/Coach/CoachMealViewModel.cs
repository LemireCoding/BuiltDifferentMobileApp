using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
using BuiltDifferentMobileApp.Views.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach
{
     public class CoachMealViewModel: ViewModelBase
    {
        private int clientId;
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<int> EditCommand { get; }
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

        public CoachMealViewModel(string clientName, int clientId)
        {
            MealPageTitle = $"{clientName}";
            this.clientId = clientId;
            EditCommand = new AsyncCommand<int>(EditMeal);
            AddCommand = new AsyncCommand(AddMeal);

            Day = DateTime.Now.Date;

            MealGroups = new ObservableRangeCollection<Grouping<string, Meal>>();
            GetMeals();
        }

        private async Task AddMeal()
        {
            var route = $"{nameof(AddMealPage)}?clientId={clientId}";
           
            await Shell.Current.GoToAsync(route);
        }

        private async Task EditMeal(int id)
        {
            
            var route = $"{nameof(EditMealPage)}?id={id}";

            await Shell.Current.GoToAsync(route);
            
        }

        public void CreateMealGroups()
        {
            
            MealGroups.Clear();
            MealGroups.Add(new Grouping<string, Meal>(AppResource.AddMealViewModelBreakfast, Meals.Where(x =>
                x.mealType.Contains("Breakfast"))));
            MealGroups.Add(new Grouping<string, Meal>(AppResource.AddMealViewModelLunch, Meals.Where(x =>
                x.mealType.Contains("Lunch"))));
            MealGroups.Add(new Grouping<string, Meal>(AppResource.AddMealViewModelDinner, Meals.Where(x =>
                x.mealType.Contains("Dinner"))));
            MealGroups.Add(new Grouping<string, Meal>(AppResource.AddMealViewModelSnack, Meals.Where(x =>
                x.mealType.Contains("Snack"))));

            OnPropertyChanged("MealGroups");
           
            
        }

        public async Task GetMeals()
        {

            var result = await networkService.GetAsync<ObservableRangeCollection<Meal>>(APIConstants.GetMealsByClientId(clientId));
            if (result == null || result.Count == 0)
            {
                return;
            }
            Meals = new ObservableRangeCollection<Meal>(result.Where(x => x.day.ToString("MMMM dd, yyyy") == Day.ToString("MMMM dd, yyyy")));
            CreateMealGroups();
            OnPropertyChanged("Meals");
        }

        
    }
}
