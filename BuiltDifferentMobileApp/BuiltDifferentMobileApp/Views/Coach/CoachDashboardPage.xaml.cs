using BuiltDifferentMobileApp.ViewModels.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Coach {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoachDashboardPage : ContentPage {
        public CoachDashboardPage() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            BindingContext = new CoachDashboardPageViewModel();
        }
    }
}