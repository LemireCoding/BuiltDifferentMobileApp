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

        private async void EditProfileButton_Clicked(object sender, EventArgs e) {
            ((MyProfilePageClientViewModel)BindingContext).EditMode = true;
            ((MyProfilePageClientViewModel)BindingContext).LoadEditInfo();
            EditProfilePopup.TranslateTo(0, 0, 200);
        }

        private async void CancelLabel_Tapped(object sender, EventArgs e) {
            ((MyProfilePageClientViewModel)BindingContext).EditMode = false;
            EditProfilePopup.TranslateTo(0, 800, 200);
        }

        private async void SaveLabel_Tapped(object sender, EventArgs e) {
            await ((MyProfilePageClientViewModel)BindingContext).SubmitEditedFields();
            ((MyProfilePageClientViewModel)BindingContext).EditMode = false;
            EditProfilePopup.TranslateTo(0, 800, 200);
        }

    }
}