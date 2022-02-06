using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.ViewModels.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Coach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ClientId), nameof(ClientId))]
    [QueryProperty(nameof(ClientName), nameof(ClientName))]
    public partial class CoachMenuPage : TabbedPage
    {

        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public CoachMenuPage()
        {
            InitializeComponent();
            BindingContext = new CoachMenuPageViewModel();
            Children.Clear();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("fr"))
            {
                ((CoachMenuPageViewModel)BindingContext).Title = "Le " + AppResource.ClientDashboardTitle + " à " + $"{ClientName}";

            }
            else
            {
                if (Children.Count != 2)
                {
                    if (ClientName.EndsWith("s"))
                    {
                        ClientName += "'";
                    }
                    else
                    {
                        ClientName += "'s";
                    }

                ((CoachMenuPageViewModel)BindingContext).Title = $"{ClientName}" + AppResource.ClientDashboardTitle;

                }
            }
               
            var workoutPage = new CoachWorkoutPage {
                BindingContext = new CoachWorkoutViewModel(ClientName, ClientId)
            };

            var mealPage = new CoachMealPage {
                BindingContext = new CoachMealViewModel(ClientName, ClientId)
            };

            Children.Add(workoutPage);
            Children.Add(mealPage);
            
        }

    }
}