using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.ViewModels.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Coach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoachMealPage : ContentPage
    {
        public CoachMealPage()
        {
            InitializeComponent();
            BindingContext = new CoachMealViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var viewModel = (CoachMealViewModel)BindingContext;
                await viewModel.GetMeals();
            }

        }

    }
}