using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.Services.AccountServices {
    public sealed class AccountService : IAccountService {

        private static readonly Lazy<IAccountService> lazy = new Lazy<IAccountService>(() => new AccountService());

        public static IAccountService Instance { get { return lazy.Value; } }

        private AccountService() { }

        private User user;
        private string role;

        public string CurrentUserEmail { get; set; }
        public AppShellViewModel AppShellViewModel { get; set; }

        public async Task<bool> SetCurrentUser(HttpResponseMessage response) {
            try {
                if(response.IsSuccessStatusCode) {
                    if(response.StatusCode == HttpStatusCode.NoContent) {
                        role = AccountConstants.Admin;
                        AppShellViewModel.UpdateUserRole(AccountConstants.Admin);
                        return true;
                    }

                    string serializedUser = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedUser);

                    if(result.ContainsKey("waitingApproval")) {
                        user = JsonConvert.DeserializeObject<Client>(serializedUser);
                        role = AccountConstants.Client;
                        AppShellViewModel.UpdateUserRole(AccountConstants.Client);
                    }
                    else if(result.ContainsKey("isVerified")) {
                        user = JsonConvert.DeserializeObject<Coach>(serializedUser);
                        role = AccountConstants.Coach;
                        AppShellViewModel.UpdateUserRole(AccountConstants.Coach);
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

        public User GetCurrentUser() {
            return user;
        }

        public string GetCurrentUserRole() {
            return role;
        }

        public void RemoveCurrentUser() {
            user = null;
            role = "";
            AppShellViewModel.RemoveAllUserRoles();
        }
    }
}
