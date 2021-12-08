using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels
{
     public class MealViewModel: ViewModelBase
    {
        private int clientId;

        public ObservableRangeCollection<Meal> Meals { get; set; }
        public ObservableRangeCollection<Grouping<string, Meal>> MealGroups { get; set; }
        private Meal selectedMeal;
        public Meal SelectedMeal
        {
            get => selectedMeal;
            set => SetProperty(ref selectedMeal, value);
        }
       
        public AsyncCommand AddCommand { get; }
        public AsyncCommand <int>EditCommand { get; }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public MealViewModel()
        {
            clientId = 1;
            EditCommand = new AsyncCommand<int>(EditMeal);
            AddCommand = new AsyncCommand(AddMeal);
            Meals = new ObservableRangeCollection<Meal>();
            MealGroups = new ObservableRangeCollection<Grouping<string, Meal>>();
            getMeals();
            createMealGroups();
           
        }

        private async Task AddMeal()
        {
            var route = $"{nameof(ManageMealPage)}?mealId=0";
           
            await Shell.Current.GoToAsync(route);
        }

        private async Task EditMeal(int id)
        {
            var route = $"{nameof(ManageMealPage)}?mealId={id}";

            await Shell.Current.GoToAsync(route);
        }

        public void createMealGroups()
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
           
            
        }
        private async Task getMeals()
        {

            var result = await networkService.GetAsync<ObservableRangeCollection<Meal>>(APIConstants.GetMealsByClientId(clientId));
            if (result.Count == 0)
            {
                return;
            }
            Meals = result;
            
        }

        
    }
}
