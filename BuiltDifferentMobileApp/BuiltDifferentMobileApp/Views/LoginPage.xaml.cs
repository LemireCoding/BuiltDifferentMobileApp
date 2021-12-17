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

namespace BuiltDifferentMobileApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {

        IAccountService accountService = AccountService.Instance;
        INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public LoginPage() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            if(BindingContext != null) {
                accountService.RemoveCurrentUser();
                networkService.RemoveJWTToken();
            }
        }
    }
}