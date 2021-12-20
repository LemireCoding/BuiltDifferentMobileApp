using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.Services.AccountServices {
    public interface IAccountService {
        AppShellViewModel AppShellViewModel { get; set; }
        string CurrentUserEmail { get; set; }
        Task<bool> SetCurrentUser(HttpResponseMessage user);
        User GetCurrentUser();
        void RemoveCurrentUser();
        string GetCurrentUserRole();
        Task ViewMyProfileCommand();
    }
}
