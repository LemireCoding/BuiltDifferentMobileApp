using BuiltDifferentMobileApp.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(mealId), nameof(mealId))]
    public partial class RecipePage : ContentPage
    {
        public int mealId { get; set; }
        public RecipePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new RecipePageViewModel(mealId);
        }
    }
}