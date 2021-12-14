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
    [QueryProperty(nameof(workoutId), nameof(workoutId))]
    public partial class EditWorkoutPage : ContentPage
    {
        public int workoutId { get; set; }
       
        public EditWorkoutPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new EditWorkoutViewModel(workoutId);
        }
    }
}