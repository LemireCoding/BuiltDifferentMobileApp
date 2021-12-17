using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.ViewModels {
    public class MyProfilePageClientViewModel : ViewModelBase {

        public string Name { get; set; }
        public string Email { get; set; }

        private IAccountService accountService = AccountService.Instance;

        public MyProfilePageClientViewModel() {
            Client user = (Client)accountService.GetCurrentUser();

            Name = user.name;
        }
    }
}
