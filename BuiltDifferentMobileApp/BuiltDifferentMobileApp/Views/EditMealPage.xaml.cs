using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuiltDifferentMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(id), nameof(id))]
    public partial class EditMealPage : ContentPage
    {
        public int id { get; set; }
        public EditMealPage()
        {
            InitializeComponent();
            BindingContext = new EditMealViewModel(id);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new EditMealViewModel(id);
        }
    }
}