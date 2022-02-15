using BuiltDifferentMobileApp.ViewModels.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientSummaryReportPage : ContentPage
    {

        public ClientSummaryReportPage()
        {
          
         
            InitializeComponent();
            BindingContext = new ClientSummaryReportViewModel();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var viewModel = (ClientSummaryReportViewModel)BindingContext;
                viewModel.CalculateMeals();
                viewModel.CalculateWorkouts();
            }
        }

    }
}