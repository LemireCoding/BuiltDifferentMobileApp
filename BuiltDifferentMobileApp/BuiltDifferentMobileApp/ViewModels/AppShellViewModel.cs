using BuiltDifferentMobileApp.Services.AccountServices;
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
            RemoveAllUserRoles();
        }

        public void UpdateUserRole(string role) {
            if(role == AccountConstants.Coach) {
                IsClient = false;
                IsCoach = true;
                IsAdmin = false;
            }
            else if(role == AccountConstants.Client) {
                IsClient = true;
                IsCoach = false;
                IsAdmin = false;
            }
            else if(role == AccountConstants.Admin) {
                IsClient = false;
                IsCoach = false;
                IsAdmin = true;
            }
            else {
                RemoveAllUserRoles();
            }
        }

        public void RemoveAllUserRoles() {
            IsClient = false;
            IsCoach = false;
            IsAdmin = false;
        }
    }
}
