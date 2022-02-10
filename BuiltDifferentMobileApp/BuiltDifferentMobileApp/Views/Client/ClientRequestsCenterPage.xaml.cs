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
    public partial class ClientRequestsCenterPage : ContentPage
    {
        public ClientRequestsCenterPage()
        {
            InitializeComponent();
            BindingContext = new ClientRequestsCenterViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var viewModel = (ClientRequestsCenterViewModel)BindingContext;
                await viewModel.GetRequests();
            }
        }
    }
}