﻿using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Login {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {

        IAccountService accountService = AccountService.Instance;
        INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public LoginPage() {
            InitializeComponent();

            var viewModel = new LoginPageViewModel();

            if(accountService.CurrentUserEmail != null) {
                viewModel.Email = accountService.CurrentUserEmail;
            }

            BindingContext = viewModel;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            if(BindingContext != null) {
                accountService.RemoveCurrentUser();
                networkService.RemoveJWTToken();

                var viewModel = (LoginPageViewModel)BindingContext;
                viewModel.Email = accountService.CurrentUserEmail;
                viewModel.Password = "";
            }
        }
    }
}