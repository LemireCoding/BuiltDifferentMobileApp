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
    public partial class AdminApprovedCoachesPage : ContentPage {
        public AdminApprovedCoachesPage() {
            InitializeComponent();
            BindingContext = new AdminApprovedCoachesPageViewModel();
        }

        override protected async void OnAppearing() {
            base.OnAppearing();
            if(BindingContext != null) {
                var viewModel = (AdminApprovedCoachesPageViewModel)BindingContext;
                await viewModel.FetchVerifiedCoaches();
            }
        }
    }
}