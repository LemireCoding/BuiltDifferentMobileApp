using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.Services.AccountServices {
    public interface IAccountService {
        User CurrentUser { get; set; }
        string CurrentUserRole { get; set; }
        AppShellViewModel AppShellViewModel { get; set; }
        string CurrentUserEmail { get; set; }
        Task<bool> SetCurrentUser(HttpResponseMessage user);
        void RemoveCurrentUser();
        Task ViewMyProfileCommand();
    }
}
