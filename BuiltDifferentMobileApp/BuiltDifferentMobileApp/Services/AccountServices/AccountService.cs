using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.Views.Profile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.AccountServices {
    public sealed class AccountService : IAccountService {

        private static readonly Lazy<IAccountService> lazy = new Lazy<IAccountService>(() => new AccountService());

        public static IAccountService Instance { get { return lazy.Value; } }

        private AccountService() { }

        public User CurrentUser { get; set; }
        public string CurrentUserRole { get; set; }
        public string CurrentUserEmail { get; set; }
        public AppShellViewModel AppShellViewModel { get; set; }

        public async Task<bool> SetCurrentUser(HttpResponseMessage response) {
            try {
                if(response.IsSuccessStatusCode) {
                    if(response.StatusCode == HttpStatusCode.NoContent) {
                        CurrentUserRole = AccountConstants.Admin;
                        AppShellViewModel.UpdateUserRole(AccountConstants.Admin);
                        return true;
                    }

                    string serializedUser = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedUser);

                    if(result.ContainsKey("waitingApproval")) {
                        CurrentUser = JsonConvert.DeserializeObject<Client>(serializedUser);
                        CurrentUserRole = AccountConstants.Client;
                        AppShellViewModel.UpdateUserRole(AccountConstants.Client);
                    }
                    else if(result.ContainsKey("isVerified")) {
                        CurrentUser = JsonConvert.DeserializeObject<Coach>(serializedUser);
                        CurrentUserRole = AccountConstants.Coach;
                        AppShellViewModel.UpdateUserRole(AccountConstants.Coach, ((Coach)CurrentUser).isVerified);
                    }
                    else {
                        return false;
                    }

                    return true;
                }

                return false;
            } catch {
                return false;
            }
        }

        public void RemoveCurrentUser() {
            CurrentUser = null;
            CurrentUserRole = "";
            AppShellViewModel.RemoveAllUserRoles();
        }

        public async Task ViewMyProfileCommand() {
            if(CurrentUserRole == AccountConstants.Coach) {
                await Shell.Current.GoToAsync($"{nameof(MyProfilePageCoach)}");
            }
            if(CurrentUserRole == AccountConstants.Client) {
                await Shell.Current.GoToAsync($"{nameof(MyProfilePageClient)}");
            }
            if(CurrentUserRole == AccountConstants.Admin) {
                await Shell.Current.GoToAsync($"{nameof(MyProfilePageAdmin)}");
            }
        }
    }
}
