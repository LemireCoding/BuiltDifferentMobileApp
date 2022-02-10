using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Payment
{
    public class PaymentViewModel: ViewModelBase
    {
        private string clientName;
        public string ClientName
        {
            get => clientName;
            set
            {
                SetProperty(ref clientName, value);
            }
        }

        private string coachName;
        public string CoachName
        {
            get => coachName;
            set
            {
                SetProperty(ref coachName, value);

            }
        }
        public double price;
        public double Price
        {
            get => price;
            set
            {
                SetProperty(ref price, value);
            }
        }
        public string PayPalLink { get; set; }

        public AsyncCommand MakeAPaymentCommand { get; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        private IAccountService accountService = AccountService.Instance;

        public PaymentViewModel()
        {
            CoachName = "";
            ClientName = "";
            Price = 0.0;
            PayPalLink = "";

            FetchInfo();            
            MakeAPaymentCommand = new AsyncCommand(Payment);
        }
        public async Task FetchInfo()
        {
            var user = (Models.Client)accountService.CurrentUser;
            var coach = await networkService.GetAsync<Models.CoachPaymentDTO>(APIConstants.GetCoachByIdUri(user.coachId));
            if (coach == null)
            {
                await Application.Current.MainPage.DisplayAlert("Could not load coach's profile!", "Returning to previous page", "OK");
                IsBusy = false;
                return;
            }
            else
            {
                CoachName = coach.name;
                ClientName = user.name;
                Price = coach.pricing;
                PayPalLink = coach.payPalLink;
                IsBusy = false;
            }
        }

        private async Task Payment()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(PayPalLink))
                {
                    await Application.Current.MainPage.DisplayAlert("Could not load coach's profile!", "Returning to previous page", "OK");
                    await Shell.Current.GoToAsync("..");
                    IsBusy = false;
                    return;
                }
                await Browser.OpenAsync(new Uri(PayPalLink), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}","OK");
            }

        }
    }
}
