using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.ViewModels.Profile {
    public class MyProfilePageClientViewModel : ViewModelBase {

        public string Name { get; set; }
        public string Email { get; set; }

        private IAccountService accountService = AccountService.Instance;

        public MyProfilePageClientViewModel() {
            Models.Client user = (Models.Client)accountService.CurrentUser;

            Name = user.name;
        }
    }
}
