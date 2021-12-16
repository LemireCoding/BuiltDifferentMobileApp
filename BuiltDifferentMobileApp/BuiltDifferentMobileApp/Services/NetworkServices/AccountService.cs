using BuiltDifferentMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.Services.NetworkServices {
    public sealed class AccountService : IAccountService {

        private static readonly Lazy<AccountService> lazy = new Lazy<AccountService>(() => new AccountService());

        public static AccountService Instance { get { return lazy.Value; } }

        private AccountService() { }

        private User user;
        private string userType;

        public async Task<bool> SetCurrentUser(HttpResponseMessage response) {
            try {
                if(response.IsSuccessStatusCode) {
                    if(response.StatusCode == HttpStatusCode.NoContent) {
                        userType = AccountConstants.Admin;
                        return true;
                    }

                    string serializedUser = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedUser);

                    if(result.ContainsKey("waitingApproval")) {
                        user = JsonConvert.DeserializeObject<Client>(serializedUser);
                        userType = AccountConstants.Client;
                    } else if(result.ContainsKey("isVerified")) {
                        user = JsonConvert.DeserializeObject<Coach>(serializedUser);
                        userType = AccountConstants.Client;
                    } else {
                        return false;
                    }

                    return true;
                }

                return false;
            } catch {
                return false;
            }
        }

        public object GetCurrentUser() {
            return user;
        }

        public string GetCurrentUserType() {
            return userType;
        }
    }
}
