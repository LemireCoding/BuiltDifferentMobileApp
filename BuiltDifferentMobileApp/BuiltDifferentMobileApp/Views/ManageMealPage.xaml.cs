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
    [QueryProperty(nameof(mealId), nameof(mealId))]
    public partial class ManageMealPage : ContentPage
    {
        public int mealId { get; set; }
        public ManageMealPage()
        {
            InitializeComponent();
       
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new MealManageViewModel(mealId);
        }
    }
}