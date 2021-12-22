using BuiltDifferentMobileApp.Services.AccountServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BuiltDifferentMobileApp.ViewModels.Admin {
    public class AdminMenuPageViewModel : ViewModelBase {

        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand ViewMyProfileCommand { get; }

        public AdminMenuPageViewModel() {
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
        }

    }
}
