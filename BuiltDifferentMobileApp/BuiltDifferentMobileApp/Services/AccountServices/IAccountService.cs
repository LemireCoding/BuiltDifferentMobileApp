using BuiltDifferentMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.Services.AccountServices {
    public interface IAccountService {
        string CurrentUserEmail { get; set; }
        Task<bool> SetCurrentUser(HttpResponseMessage user);
        User GetCurrentUser();
        void RemoveCurrentUser();
        string GetCurrentUserRole();
    }
}
