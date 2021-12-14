using BuiltDifferentMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        public WorkoutPage()
        {
            InitializeComponent();
            BindingContext = new WorkoutViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(BindingContext != null)
            {
                var viewModel = (WorkoutViewModel)BindingContext;
                await viewModel.GetWorkouts();
            }
        }
    }
}