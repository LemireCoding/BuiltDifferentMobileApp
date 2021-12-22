﻿using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using BuiltDifferentMobileApp.Views;
using BuiltDifferentMobileApp.Views.Profile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach {
    public class CoachMenuPageViewModel : ViewModelBase {

        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand ViewMyProfileCommand { get; }

        public CoachMenuPageViewModel() {
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
        }

    }
}
