﻿using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
using BuiltDifferentMobileApp.Views.Admin;
using BuiltDifferentMobileApp.Views.Client;
using BuiltDifferentMobileApp.Views.Coach;
using BuiltDifferentMobileApp.Views.Login;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Login {
    public class LoginPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        private IAccountService accountService = AccountService.Instance;

        private string email;
        public string Email {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string password;
        public string Password {
            get => password;
            set => SetProperty(ref password, value);
        }

        public AsyncCommand LoginCommand { get; }
        public AsyncCommand RegisterCommand { get; }
        public AsyncCommand ForgotPasswordCommand { get; }

        public LoginPageViewModel() {
            LoginCommand = new AsyncCommand(Login);
            RegisterCommand = new AsyncCommand(Register);
            ForgotPasswordCommand = new AsyncCommand(ForgotPassword);
        }

        private async Task Login() {
            if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password)) {
                await Application.Current.MainPage.DisplayAlert("Required fields", "Please fill out all fields", "OK");
                return;
            }

            IsBusy = true;

            Dictionary<string, string> credentials = new Dictionary<string, string>() {
                { "email", Email },
                { "password", Password },
            };

            HttpStatusCode response = await networkService.LoginAsync(APIConstants.GetLoginUri(), credentials);

            IsBusy = false;

            if(((int)response >= 200) && ((int)response <= 299)) {
                accountService.CurrentUserEmail = Email;

                if(accountService.GetCurrentUserRole() == AccountConstants.Admin) {
                    await Shell.Current.GoToAsync($"//{nameof(AdminMenuPage)}");
                }
                if(accountService.GetCurrentUserRole() == AccountConstants.Coach) {
                    await Shell.Current.GoToAsync($"//{nameof(CoachMenuPage)}");
                }
                if(accountService.GetCurrentUserRole() == AccountConstants.Client) {
                    await Shell.Current.GoToAsync($"//{nameof(ClientMenuPage)}");
                }
            }
            else if((int)response == 404) {
                await Application.Current.MainPage.DisplayAlert("Could not find account", "Please try a different login", "OK");
            }
            else if(response == HttpStatusCode.Unauthorized){
                await Application.Current.MainPage.DisplayAlert("Invalid email or password!", "Please try again.", "OK");
            }
            else if((int)response == 429) {
                await Application.Current.MainPage.DisplayAlert("Maximum login attempts exceeded", "Please try again later.", "OK");
            }
            else {
                await Application.Current.MainPage.DisplayAlert("Unknown error while logging in", "Please try again.", "OK");
            }
        }

        private async Task Register() {
            if(IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        private async Task ForgotPassword() {
            if(IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(ForgotPasswordPage)}");
        }

    }
}
