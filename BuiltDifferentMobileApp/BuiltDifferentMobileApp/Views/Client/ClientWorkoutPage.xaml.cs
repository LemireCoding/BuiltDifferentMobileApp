using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.ViewModels.Client;

namespace BuiltDifferentMobileApp.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientWorkoutPage : ContentPage
    {
        public ClientWorkoutPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var viewModel = (ClientWorkoutViewModel)BindingContext;
                await viewModel.GetWorkouts();
            }
        }
    }
}