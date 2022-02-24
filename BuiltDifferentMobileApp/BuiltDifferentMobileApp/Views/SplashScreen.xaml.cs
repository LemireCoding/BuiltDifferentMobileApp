using BuiltDifferentMobileApp.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage {

        public SplashScreen() {
            InitializeComponent();
        }

        private async void PageLoaded(object sender, ToggledEventArgs e) {
            await Task.Delay(500);
            await Logo.FadeTo(100, 1000);
            await Task.Delay(500);
            await Logo.FadeTo(0, 1000);
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}