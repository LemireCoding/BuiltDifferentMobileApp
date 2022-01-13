using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Views.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Client {
    public class ClientMenuPageViewModel : ViewModelBase {

        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand ViewMyProfileCommand { get; }

        public ClientMenuPageViewModel()
        {
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
        }
    }
}
