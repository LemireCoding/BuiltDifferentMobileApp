using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class RecipePageViewModel : ViewModelBase
    {
        private int mealId;
        private string recipe;
        public string Recipe { get => recipe; set => SetProperty(ref recipe, value); }

        private string mealName;
        public string MealName { get => mealName; set => SetProperty(ref mealName, value); }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;


        public RecipePageViewModel(int id)
        {
            this.mealId = id;
            FetchInfo();

        }

        private async void FetchInfo()
        {
            var route = APIConstants.GetMealByIdUri(mealId);

            var meal = await networkService.GetAsync<Meal>(route);
            if(meal != null)
            {
                MealName = meal.mealName;

                Recipe = meal.recipe;
            }
            else
            {
                MealName = "Meal Not Found";

                Recipe = "Recipe Not Found";
            }
           
        }

       
    }
}
