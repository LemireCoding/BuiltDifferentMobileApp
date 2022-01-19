﻿using BuiltDifferentMobileApp.Services.AccountServices;
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

        private bool isClient;
        public bool IsClient {
            get => isClient;
            set => SetProperty(ref isClient, value);
        }

        public AppShellViewModel() {
            RemoveAllUserRoles();
        }

        public void UpdateUserRole(string role, bool verified = false) {
            if(role == AccountConstants.Coach) {
                IsClient = false;
                IsAdmin = false;

                if(verified) {
                    IsUnverifiedCoach = false;
                    IsVerifiedCoach = true;
                } else {
                    IsUnverifiedCoach = true;
                    IsVerifiedCoach = false;
                }
            }
            else if(role == AccountConstants.Client) {
                IsClient = true;
                IsUnverifiedCoach = false;
                IsVerifiedCoach = false;
                IsAdmin = false;
            }
            else if(role == AccountConstants.Admin) {
                IsClient = false;
                IsUnverifiedCoach = false;
                IsVerifiedCoach = false;
                IsAdmin = true;
            }
            else {
                RemoveAllUserRoles();
            }
        }

        public void RemoveAllUserRoles() {
            IsClient = false;
            IsUnverifiedCoach = false;
            IsVerifiedCoach = false;
            IsAdmin = false;
        }
    }
}
