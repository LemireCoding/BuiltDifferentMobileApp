using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
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
    public class ForgotPasswordPageViewModel : ViewModelBase {
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string UnknownErrorText = AppResource.UnknownErrorText;
        public string MissingInputs = AppResource.MissingInputs;
        private string BadEmail = AppResource.BadEmail;
        private string ConfirmSentEmailText = AppResource.ConfirmSentEmailText;

        private string errorText;
        public string ErrorText
        {
            get => errorText;
            set => SetProperty(ref errorText, value);
        }

        private string successText;
        public string SuccessText
        {
            get => successText;
            set => SetProperty(ref successText, value);
        }

        public AsyncCommand SubmitCommand { get; }
        public ForgotPasswordPageViewModel() {
            SubmitCommand = new AsyncCommand(Submit);

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async Task Submit()
        {
            if (IsBusy) return;

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorText = MissingInputs;
                return;
            }

            if (!IsValidEmail(Email.Trim()))
            {
                ErrorText = BadEmail;
                IsBusy = false;
                return;
            }

            IsBusy = true;

            Dictionary<string, string> credentials = new Dictionary<string, string>() {
                { "email", Email },
            };

            var response = await networkService.PostAsyncHttpResponseMessage(APIConstants.PostForgotPasswordUri(), credentials);

            if (((int)response>= 200) && ((int)response <= 299))
            {
                ErrorText = "";
                SuccessText = ConfirmSentEmailText;
            }
            else
            {
                ErrorText = UnknownErrorText;
            }

            IsBusy = false;
        }
    }
}
