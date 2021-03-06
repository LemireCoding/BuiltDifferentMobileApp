using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach
{
    public class AddMealViewModel : ViewModelBase
    {
        private int id;
        private int coachId;
        private int clientId;
        private string mealName;
        public string MealName { get => mealName; set => SetProperty(ref mealName, value); }
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
        private DateTime day;
        public DateTime Day { get => day; set => SetProperty(ref day, value); }
        private string imageLink;
        public string ImageLink { get => imageLink; set => SetProperty(ref imageLink, value); }
        public Xamarin.CommunityToolkit.ObjectModel.AsyncCommand SaveCommand { get; }
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        private IAccountService accountService = AccountService.Instance;
        public ObservableRangeCollection<string> Types { get; set; }
        public AddMealViewModel(int clientId)
        {
            this.clientId = clientId;
            var user = (Models.Coach)accountService.CurrentUser;
            coachId = user.id;
            Title = "Add Meal";
            SaveCommand = new AsyncCommand(SaveMeal);
            Types = new ObservableRangeCollection<string>
        {
            AppResource.AddMealViewModelBreakfast,
            AppResource.AddMealViewModelLunch,
            AppResource.AddMealViewModelDinner,
            AppResource.AddMealViewModelSnack
        };
            Day = DateTime.Now.Date;
        }

        public async Task SaveMeal()
        {
            if (
                string.IsNullOrEmpty(MealName) ||
                string.IsNullOrEmpty(MealType)
                )
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelFieldIssueTitle, AppResource.ViewModelFieldIssueMessage, "OK");
                return;
            }
            
            //This is to ensure the data sent is accepted by the backend 
            if(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("fr"))
            {
                if (MealType == "Déjeuner")
                    MealType = "Breakfast";
                else if (MealType == "Dîner")
                    MealType = "Lunch";
                else if (MealType == "Souper")
                    MealType = "Dinner";
                else if (MealType == "Collation")
                    MealType = "Snack";
            }

            var meal = new  MealDTO(clientId,coachId,MealName, MealType, Calories, Protein, Carbs, Fat, "recipe", "imagelink.com", Day, false);
            var test = JsonConvert.SerializeObject(meal);
            var result = await networkService.PostAsync<HttpResponseMessage>(APIConstants.PostMealUri(), meal);
            var httpCode = result.StatusCode;

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelSuccessTitle, AppResource.AddMealSavedTitle, "OK");
                await AppShell.Current.GoToAsync("..");
            }
            else if (httpCode == System.Net.HttpStatusCode.InternalServerError)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelErrorTitle, AppResource.ViewModelErrorMessage, "OK");
            }
            else
                return;
        }
    }
}
