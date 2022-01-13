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
    public partial class AdminCoachApprovalPage : ContentPage {
        public AdminCoachApprovalPage() {
            InitializeComponent();
            BindingContext = new AdminCoachApprovalPageViewModel();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            if(BindingContext != null) {
                var viewModel = (AdminCoachApprovalPageViewModel)BindingContext;
                await viewModel.FetchPendingCoaches();
            }
        }
    }
}