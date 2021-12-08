using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels
{
     public class MealViewModel: ViewModelBase
    {
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
        public MealViewModel()
        {
            EditCommand = new AsyncCommand<int>(EditMeal);
            AddCommand = new AsyncCommand(AddMeal);
            Meals = new ObservableRangeCollection<Meal>();
            MealGroups = new ObservableRangeCollection<Grouping<string, Meal>>();
            initMeals();
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
                x.MealType.Contains("Breakfast"))));
            MealGroups.Add(new Grouping<string, Meal>("Lunch", Meals.Where(x =>
                x.MealType.Contains("Lunch"))));
            MealGroups.Add(new Grouping<string, Meal>("Dinner", Meals.Where(x =>
                x.MealType.Contains("Dinner"))));
            MealGroups.Add(new Grouping<string, Meal>("Snack", Meals.Where(x =>
                x.MealType.Contains("Snack"))));
            
        }
        private void initMeals()
        {
            Meals.Add(new Meal
            {
                MealId = 1,
                Name = "Chicken and Rice",
                Calories = 200,
                Protein = 60.66,
                Carbs = 30.22,
                Fat = 20.12,
                Recipe = "LONG TEXT",
                ClientId = 1,
                CoachId = 1,
                ImageLink = "https://www.recipetineats.com/wp-content/uploads/2018/06/Oven-Baked-Chicken-and-Rice_0.jpg",
                MealType = "Lunch"

            });

            Meals.Add(new Meal
            {
                MealId = 2,
                Name = "Chicken and Rice",
                Calories = 200,
                Protein = 60.66,
                Carbs = 30.22,
                Fat = 20.12,
                Recipe = "LONG TEXT",
                ClientId = 1,
                CoachId = 1,
                ImageLink = "https://www.recipetineats.com/wp-content/uploads/2018/06/Oven-Baked-Chicken-and-Rice_0.jpg",
                MealType = "Lunch"

            });

            Meals.Add(new Meal
            {
                MealId = 3,
                Name = "Chicken and Rice",
                Calories = 200,
                Protein = 60.66,
                Carbs = 30.22,
                Fat = 20.12,
                Recipe = "LONG TEXT",
                ClientId = 1,
                CoachId = 1,
                ImageLink = "https://www.recipetineats.com/wp-content/uploads/2018/06/Oven-Baked-Chicken-and-Rice_0.jpg",
                MealType = "Breakfast"

            });

            Meals.Add(new Meal
            {
                MealId = 4,
                Name = "Chicken and Rice",
                Calories = 200,
                Protein = 60.66,
                Carbs = 30.22,
                Fat = 20.12,
                Recipe = "LONG TEXT",
                ClientId = 1,
                CoachId = 1,
                ImageLink = "https://www.recipetineats.com/wp-content/uploads/2018/06/Oven-Baked-Chicken-and-Rice_0.jpg",
                MealType = "Snack"

            });


            Meals.Add(new Meal
            {
                MealId = 5,
                Name = "Chicken and Rice",
                Calories = 200,
                Protein = 60.66,
                Carbs = 30.22,
                Fat = 20.12,
                Recipe = "LONG TEXT",
                ClientId = 1,
                CoachId = 1,
                ImageLink = "https://www.recipetineats.com/wp-content/uploads/2018/06/Oven-Baked-Chicken-and-Rice_0.jpg",
                MealType = "Dinner"

            });

            
            

        }
    }
}
