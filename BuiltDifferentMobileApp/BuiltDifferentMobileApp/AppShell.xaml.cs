using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.Views.Admin;
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

            // Admin pages
            Routing.RegisterRoute(nameof(AdminCoachApprovalProfilePage), typeof(AdminCoachApprovalProfilePage));
            Routing.RegisterRoute(nameof(AdminCoachApprovalPage), typeof(AdminCoachApprovalPage));
            Routing.RegisterRoute(nameof(AdminApprovedCoachProfilePage), typeof(AdminApprovedCoachProfilePage));
            Routing.RegisterRoute(nameof(AdminApprovedCoachesPage), typeof(AdminApprovedCoachesPage));
            Routing.RegisterRoute(nameof(AdminMenuPage), typeof(AdminMenuPage));

            // Coach pages
            Routing.RegisterRoute(nameof(AddWorkoutPage), typeof(AddWorkoutPage));
            Routing.RegisterRoute(nameof(EditWorkoutPage), typeof(EditWorkoutPage));
            Routing.RegisterRoute(nameof(AddMealPage), typeof(AddMealPage));
            Routing.RegisterRoute(nameof(EditMealPage), typeof(EditMealPage));
            Routing.RegisterRoute(nameof(CoachMealPage), typeof(CoachMealPage));
            Routing.RegisterRoute(nameof(CoachWorkoutPage), typeof(CoachWorkoutPage));
            Routing.RegisterRoute(nameof(CoachMenuPage), typeof(CoachMenuPage));
            Routing.RegisterRoute(nameof(CoachDashboardPage), typeof(CoachDashboardPage));
            Routing.RegisterRoute(nameof(NewCoachPage), typeof(NewCoachPage));

            // Client pages
         
            Routing.RegisterRoute(nameof(RecipePage), typeof(RecipePage));
            Routing.RegisterRoute(nameof(ClientMealPage), typeof(ClientMealPage));
            Routing.RegisterRoute(nameof(ClientWorkoutPage), typeof(ClientWorkoutPage));
            Routing.RegisterRoute(nameof(ClientDashboardPage), typeof(ClientDashboardPage));
            Routing.RegisterRoute(nameof(ClientCoachSelectionPage), typeof(ClientCoachSelectionPage));
            Routing.RegisterRoute(nameof(ClientCoachCriteriasPage), typeof(ClientCoachCriteriasPage));


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
