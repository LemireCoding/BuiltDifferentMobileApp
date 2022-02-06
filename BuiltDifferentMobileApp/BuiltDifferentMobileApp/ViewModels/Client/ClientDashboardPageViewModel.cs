using BuiltDifferentMobileApp.Services.AccountServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class ClientDashboardPageViewModel : ViewModelBase
    {
        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand ViewMyProfileCommand { get; }
        public ClientDashboardPageViewModel()
        {
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);

        }

      


    }
}
