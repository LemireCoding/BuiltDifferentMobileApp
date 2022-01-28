using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Login {
    public class RegisterPageViewModel : ViewModelBase {

        INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool RegisterForCoachAccount { get; set; }

        private string errorText;
        public string ErrorText {
            get => errorText;
            set => SetProperty(ref errorText, value);
        }

        public string BadRequest = AppResource.BadRequest;
        public string AccountAlreadyExists = AppResource.AccountAlreadyExists;
        public string UnknownErrorText = AppResource.UnknownErrorText;
        public string MissingInputs = AppResource.MissingInputs;
        public string InvalidEmail = AppResource.InvalidEmail;
        public string DifferentPasswords = AppResource.DifferentPasswords;

        public AsyncCommand RegisterCommand { get; set; }

        public RegisterPageViewModel() {
            
            RegisterCommand = new AsyncCommand(Register);

            FullName = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
        }

        private async Task Register() {
            IsBusy = true;

            if(string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email) ||
               string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword)) {
                ErrorText = MissingInputs;
                IsBusy = false;
                return;
            }

            if(!IsValidEmail(Email.Trim())) {
                ErrorText = InvalidEmail;
                IsBusy = false;
                return;
            }

            if(Password != ConfirmPassword) {
                ErrorText = DifferentPasswords;
                IsBusy = false;
                return;
            }

            string passwordErrors = IsValidPassword(Password);
            if(!string.IsNullOrWhiteSpace(passwordErrors)) {
                ErrorText = passwordErrors;
                IsBusy = false;
                return;
            }

            ErrorText = "";

            Dictionary<string, string> accountDetails = new Dictionary<string, string>() {
                { "name", $"{FullName.Trim()}" },
                { "email", Email.Trim() },
                { "password", Password },
                { "accountType", RegisterForCoachAccount ? "coach" : "client" }
            };

            var response = await networkService.RegisterAsync(APIConstants.GetRegisterNewAccountUri(), accountDetails);

            if(((int)response >= 200) && ((int)response <= 299)) {
                await Shell.Current.GoToAsync("..");
            }
            else if(response == HttpStatusCode.Conflict) {
                ErrorText = AccountAlreadyExists;
            }
            else if(response == HttpStatusCode.BadRequest) {
                ErrorText = BadRequest;
            }
            else {
                ErrorText = UnknownErrorText;
            }

            IsBusy = false;
        }

        private bool IsValidEmail(string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }

        public string IsValidPassword(string password) {
            string passwordRequirements = "";

            if(password.Length < 8) {
                passwordRequirements += AppResource.PasswordRequirementsLength;
            }

            if(!password.Any(char.IsUpper)) {
                passwordRequirements += AppResource.PasswordRequirementUppercase;
            }

            if(!password.Any(char.IsDigit)) {
                passwordRequirements += AppResource.PasswordRequirementDigit;
            }

            if(!password.Any(char.IsPunctuation) && !password.Any(char.IsSymbol)) {
                passwordRequirements += AppResource.PasswordRequirmentSymbol;
            }

            return passwordRequirements;
        }
    }
}
