using BuiltDifferentMobileApp.Ressource;
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

        private string AccountNotFoundText = AppResource.AccountNotFoundText;
        private string IncorrectLoginText = AppResource.IncorrectLoginText;
        private string LoginAttempsExceededText = AppResource.LoginAttempsExceededText;
        private string UnknownErrorText = AppResource.UnknownErrorText;
        private string MissingInputs = AppResource.MissingInputs;
        private const string AccountSuspendedText = "This account has been suspended.";

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
                } else if(accountService.CurrentUserRole == AccountConstants.Coach) {
                    if(((Models.Coach)accountService.CurrentUser).isVerified) {
                        await Shell.Current.GoToAsync($"//{nameof(CoachDashboardPage)}");
                    } else {
                        await Shell.Current.GoToAsync($"//{nameof(NewCoachPage)}");
                    }
                } else if(accountService.CurrentUserRole == AccountConstants.Client) {
                    if(((Models.Client)accountService.CurrentUser).coachId == 0)
                    {
                        await Shell.Current.GoToAsync($"//{nameof(ClientCoachCriteriasPage)}");
                    } else
                    {
                        await Shell.Current.GoToAsync($"//{nameof(ClientDashboardPage)}");
                    }  
                }
            } else if((int)response == 404) {
                ErrorText = AccountNotFoundText;
            } else if(response == HttpStatusCode.Unauthorized) {
                ErrorText = IncorrectLoginText;
            } else if(response == HttpStatusCode.Forbidden) {
                ErrorText = AccountSuspendedText;
            } else if((int)response == 429) {
                ErrorText = LoginAttempsExceededText;
            } else {
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
