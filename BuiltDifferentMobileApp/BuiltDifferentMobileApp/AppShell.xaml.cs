using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp {
    public partial class AppShell : Xamarin.Forms.Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ManageWorkoutPage), typeof(ManageWorkoutPage));
            Routing.RegisterRoute(nameof(AddMealPage), typeof(AddMealPage));
            Routing.RegisterRoute(nameof(EditMealPage), typeof(EditMealPage));
            Routing.RegisterRoute(nameof(MealPage), typeof(MealPage));
            Routing.RegisterRoute(nameof(WorkoutPage), typeof(WorkoutPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
           
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            
            
        }
    }
}
