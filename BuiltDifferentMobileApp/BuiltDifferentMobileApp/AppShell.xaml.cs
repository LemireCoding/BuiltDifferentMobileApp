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
            Routing.RegisterRoute(nameof(WorkoutPage), typeof(WorkoutPage));
            Routing.RegisterRoute(nameof(ManageMealPage), typeof(ManageMealPage));
            Routing.RegisterRoute(nameof(MealPage), typeof(MealPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
        }
    }
}
