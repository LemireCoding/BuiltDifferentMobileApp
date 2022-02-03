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

        private bool isVerifiedCoach;
        public bool IsVerifiedCoach {
            get => isVerifiedCoach;
            set => SetProperty(ref isVerifiedCoach, value);
        }

        private bool isUnverifiedCoach;
        public bool IsUnverifiedCoach {
            get => isUnverifiedCoach;
            set => SetProperty(ref isUnverifiedCoach, value);
        }



        private bool isClientWithCoach;
        public bool IsClientWithCoach
        {
            get => isClientWithCoach;
            set => SetProperty(ref isClientWithCoach, value);
        }

        private bool isClientWithoutCoach;
        public bool IsClientWithoutCoach
        {
            get => isClientWithoutCoach;
            set => SetProperty(ref isClientWithoutCoach, value);
        }

        public AppShellViewModel() {
            RemoveAllUserRoles();
        }

        public void UpdateUserRole(string role, bool verified = false, bool hasCoach = false) {
            if(role == AccountConstants.Coach) {
                IsClientWithCoach = false;
                IsClientWithoutCoach = false;
                IsAdmin = false;
                isClientWithCoach = false;
                if(verified) {
                    IsUnverifiedCoach = false;
                    IsVerifiedCoach = true;
                } else {
                    IsUnverifiedCoach = true;
                    IsVerifiedCoach = false;
                }
            }
            else if(role == AccountConstants.Client) {
               
                IsUnverifiedCoach = false;
                IsVerifiedCoach = false;
                IsAdmin = false;
                if (hasCoach)
                {
                    IsClientWithoutCoach = false;
                    IsClientWithCoach = true;
                }
                else
                {
                    IsClientWithoutCoach = true;
                    IsClientWithCoach = false;
                }
            }
            else if(role == AccountConstants.Admin) {
                IsClientWithCoach = false;
                IsClientWithoutCoach = false;
                IsUnverifiedCoach = false;
                IsVerifiedCoach = false;
                IsAdmin = true;
              
            }
            else {
                RemoveAllUserRoles();
            }
        }

        public void RemoveAllUserRoles() {
            IsClientWithCoach = false;
            IsClientWithoutCoach = false;
            IsUnverifiedCoach = false;
            IsVerifiedCoach = false;
            IsAdmin = false;
        }
    }
}
