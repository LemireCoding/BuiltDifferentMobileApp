using BuiltDifferentMobileApp.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Admin {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(CoachId), nameof(CoachId))]
    public partial class AdminApprovedCoachProfilePage : ContentPage {

        public int CoachId { get; set; }

        public AdminApprovedCoachProfilePage() {
            InitializeComponent();
        }

        override protected void OnAppearing() {
            base.OnAppearing();
            BindingContext = new AdminApprovedCoachProfilePageViewModel(CoachId);
        }
    }
}