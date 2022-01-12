using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.Views.Client;
using BuiltDifferentMobileApp.Views.Coach;
using BuiltDifferentMobileApp.Views.Login;
using BuiltDifferentMobileApp.Views.Profile;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp {
    public partial class AppShell : Xamarin.Forms.Shell {
        public AppShell() {
            InitializeComponent();

            // Coach pages
            Routing.RegisterRoute(nameof(AddWorkoutPage), typeof(AddWorkoutPage));
            Routing.RegisterRoute(nameof(EditWorkoutPage), typeof(EditWorkoutPage));
            Routing.RegisterRoute(nameof(AddMealPage), typeof(AddMealPage));
            Routing.RegisterRoute(nameof(EditMealPage), typeof(EditMealPage));
            Routing.RegisterRoute(nameof(CoachMealPage), typeof(CoachMealPage));
            Routing.RegisterRoute(nameof(CoachWorkoutPage), typeof(CoachWorkoutPage));
            Routing.RegisterRoute(nameof(CoachMenuPage), typeof(CoachMenuPage));
            Routing.RegisterRoute(nameof(CoachDashboardPage), typeof(CoachDashboardPage));

            // Client pages
            Routing.RegisterRoute(nameof(ClientMenuPage), typeof(ClientMenuPage));
            Routing.RegisterRoute(nameof(ClientCoachCriteriasPage), typeof(ClientCoachCriteriasPage));
            Routing.RegisterRoute(nameof(ClientCoachSelectionPage), typeof(ClientCoachSelectionPage));
            Routing.RegisterRoute(nameof(ClientDashboardPage), typeof(ClientDashboardPage));

            // Profile pages
            Routing.RegisterRoute(nameof(MyProfilePageAdmin), typeof(MyProfilePageAdmin));
            Routing.RegisterRoute(nameof(MyProfilePageCoach), typeof(MyProfilePageCoach));
            Routing.RegisterRoute(nameof(MyProfilePageClient), typeof(MyProfilePageClient));

            // Login page
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            
            
        }
    }
}
