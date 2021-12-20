using System;
using System.Collections.Generic;
using System.Text;

namespace BuiltDifferentMobileApp.ViewModels {
    public class AppShellViewModel : ViewModelBase {

        private bool isAdmin;
        public bool IsAdmin {
            get => isAdmin;
            set => SetProperty(ref isAdmin, value);
        }

        private bool isCoach;
        public bool IsCoach {
            get => isCoach;
            set => SetProperty(ref isCoach, value);
        }

        private bool isClient;
        public bool IsClient {
            get => isClient;
            set => SetProperty(ref isClient, value);
        }

        public AppShellViewModel() {
            IsCoach = true;
            IsClient = true;
            IsAdmin = true;
        }
    }
}
