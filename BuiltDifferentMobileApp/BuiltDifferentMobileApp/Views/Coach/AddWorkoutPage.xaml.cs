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
    [QueryProperty(nameof(clientId), nameof(clientId))]
    public partial class AddWorkoutPage : ContentPage
    {
        public int clientId { get; set; }
        public AddWorkoutPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new AddWorkoutViewModel(clientId);
        }
    }
}