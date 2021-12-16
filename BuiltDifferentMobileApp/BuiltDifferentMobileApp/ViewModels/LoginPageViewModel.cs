using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels {
    public class LoginPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public string Email { get; set; }
        public string Password { get; set; }

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

            bool loggedIn = await networkService.LoginAsync(APIConstants.GetLoginUri(), credentials);

            IsBusy = false;

            if(loggedIn) {
                await Shell.Current.GoToAsync($"//{nameof(MenuPage)}");
            } else {
                await Application.Current.MainPage.DisplayAlert("Failed to log in", "Invalid email or password", "OK");
            }
        }

        private async Task Register() {
            if(IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        private async Task ForgotPassword() {
            if(IsBusy) return;

            await Application.Current.MainPage.DisplayAlert("Not implemented yet!", "Forgot Password", "OK");
        }

    }
}
