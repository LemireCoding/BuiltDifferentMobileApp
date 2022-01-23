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
    public partial class NewCoachPage : ContentPage {
        public NewCoachPage() {
            InitializeComponent();
            BindingContext = new NewCoachPageViewModel();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            if(BindingContext != null) {
                var viewModel = (NewCoachPageViewModel)BindingContext;
                await viewModel.CheckIfSubmissionApproved();
            }
        }
    }
}