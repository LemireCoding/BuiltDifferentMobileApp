using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp {
    public partial class App : Application {

        private IAccountService accountService = AccountService.Instance;

        public App() {
            InitializeComponent();

            var appShell = new AppShell();
            var viewModel = new AppShellViewModel();

            appShell.BindingContext = viewModel;
            accountService.AppShellViewModel = viewModel;

            MainPage = appShell;
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
