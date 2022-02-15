using BuiltDifferentMobileApp.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Admin {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminMenuPage : ContentPage {
        public AdminMenuPage() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            BindingContext = new AdminMenuPageViewModel();
        }
    }
}