using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuiltDifferentMobileApp.ViewModels.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientDashboardPage : TabbedPage
    {
        public ClientDashboardPage()
        {
            InitializeComponent();
            Children.Clear();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
         
            var workoutPage = new ClientWorkoutPage
            {
                BindingContext = new ClientWorkoutViewModel()
            };

            var mealPage = new ClientMealPage
            {
                BindingContext = new ClientMealViewModel()
            };

            Children.Add(workoutPage);
            Children.Add(mealPage);
     
        }
    }
}