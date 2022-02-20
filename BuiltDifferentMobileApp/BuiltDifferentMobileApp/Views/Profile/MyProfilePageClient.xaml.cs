using BuiltDifferentMobileApp.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Profile {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfilePageClient : ContentPage {
        public MyProfilePageClient() {
            InitializeComponent();
            BindingContext = new MyProfilePageClientViewModel();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if(BindingContext == null) {
                BindingContext = new MyProfilePageClientViewModel();
            } else {
                await ((MyProfilePageClientViewModel)BindingContext).GetUserInfo();
            }
        }
    }
}