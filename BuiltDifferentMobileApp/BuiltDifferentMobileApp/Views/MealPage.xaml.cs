using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealPage : ContentPage
    {
        public MealPage()
        {
            
            InitializeComponent();
            BindingContext = new MealViewModel();
        }
    }
}