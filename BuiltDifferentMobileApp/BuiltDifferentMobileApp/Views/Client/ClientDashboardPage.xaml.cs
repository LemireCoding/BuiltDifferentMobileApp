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
            this.BindingContext = new ClientDashboardPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(Children.Count() == 0) {
                var workoutPage = new ClientWorkoutPage {
                    BindingContext = new ClientWorkoutViewModel()
                };

                var mealPage = new ClientMealPage {
                    BindingContext = new ClientMealViewModel()
                };

                Children.Add(workoutPage);
                Children.Add(mealPage);
            }
            else if(Children.Count() == 2) {
                Children[0].BindingContext = new ClientWorkoutViewModel();
                Children[1].BindingContext = new ClientMealViewModel();
            }
        }
    }
}