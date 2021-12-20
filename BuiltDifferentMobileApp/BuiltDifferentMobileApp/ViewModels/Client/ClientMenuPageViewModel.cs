using BuiltDifferentMobileApp.Services.AccountServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BuiltDifferentMobileApp.ViewModels.Client {
    public class ClientMenuPageViewModel : ViewModelBase {

        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand ViewMyProfileCommand { get; }

        public ClientMenuPageViewModel() {
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
        }

    }
}
