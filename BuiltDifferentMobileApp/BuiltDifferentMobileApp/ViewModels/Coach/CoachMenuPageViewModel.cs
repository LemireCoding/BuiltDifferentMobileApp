using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels {
    public class CoachMenuPageViewModel : ViewModelBase {

        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand ViewMyProfileCommand { get; }

        public CoachMenuPageViewModel() {
            ViewMyProfileCommand = new AsyncCommand(ViewMyProfile);
        }

        private async Task ViewMyProfile() {
            string role = accountService.GetCurrentUserRole();

            if(role == AccountConstants.Coach) {
                await Shell.Current.GoToAsync($"{nameof(MyProfilePageCoach)}");
            }
            if(role == AccountConstants.Client) {
                await Shell.Current.GoToAsync($"{nameof(MyProfilePageClient)}");
            }
            if(role == AccountConstants.Admin) {
                await Shell.Current.GoToAsync($"{nameof(MyProfilePageAdmin)}");
            }
        }
    }
}
