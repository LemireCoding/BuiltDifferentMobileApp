using BuiltDifferentMobileApp.Services.AccountServices;
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

        private const string AccountNotFoundText = "The email you entered does not belong to an account. Please check your email and try again.";
        private const string IncorrectLoginText = "Sorry, your password was incorrect. Please check your password and try again.";
        private const string LoginAttempsExceededText = "You have reached exceeded the maximum login attempts. Please try again later.";
        private const string UnknownErrorText = "There was an unknown issue communicating with the server. Please try again later.";
        private const string MissingInputs = "Please fill all required fields.";

        private string errorText;
        public string ErrorText {
            get => errorText;
            set => SetProperty(ref errorText, value);
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
                ErrorText = MissingInputs;
                return;
            }

            IsBusy = true;

            Dictionary<string, string> credentials = new Dictionary<string, string>() {
                { "email", Email },
                { "password", Password },
            };

            HttpStatusCode response = await networkService.LoginAsync(APIConstants.GetLoginUri(), credentials);

            if(((int)response >= 200) && ((int)response <= 299)) {
                ErrorText = "";
                accountService.CurrentUserEmail = Email;

                if(accountService.CurrentUserRole == AccountConstants.Admin) {
                    await Shell.Current.GoToAsync($"//{nameof(AdminMenuPage)}");
                }
                else if(accountService.CurrentUserRole == AccountConstants.Coach) {
                    await Shell.Current.GoToAsync($"//{nameof(CoachDashboardPage)}");
                }
                else if(accountService.CurrentUserRole == AccountConstants.Client) {
                    await Shell.Current.GoToAsync($"//{nameof(ClientMenuPage)}");
                }
            }
            else if((int)response == 404) {
                ErrorText = AccountNotFoundText;
            }
            else if(response == HttpStatusCode.Unauthorized){
                ErrorText = IncorrectLoginText;
            }
            else if((int)response == 429) {
                ErrorText = LoginAttempsExceededText;
            }
            else {
                ErrorText = UnknownErrorText;
            }

            IsBusy = false;
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
