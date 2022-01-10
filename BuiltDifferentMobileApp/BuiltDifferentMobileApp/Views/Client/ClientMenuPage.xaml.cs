using BuiltDifferentMobileApp.Services.AccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Client {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientMenuPage : ContentPage {

        private IAccountService accountService = AccountService.Instance;
        public ClientMenuPage() {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var user = (Models.Client)accountService.CurrentUser;

            RedirectAsync(user);

        }

        private async Task RedirectAsync(Models.Client user)
        {
            if (user.coachId == 0)
            {
                await Shell.Current.GoToAsync($"//{nameof(ClientCoachSelectionPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(ClientDashboardPage)}");
            }
        }
    }
}