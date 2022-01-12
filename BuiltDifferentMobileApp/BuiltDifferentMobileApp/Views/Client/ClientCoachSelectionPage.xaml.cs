using BuiltDifferentMobileApp.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientCoachSelectionPage : ContentPage
    {
        public ClientCoachSelectionPage(string coachName, string gender, string coachingType)
        {
            InitializeComponent();
            BindingContext = new ClientCoachSelectionViewModel(coachName, gender,coachingType);
        }
        public ClientCoachSelectionPage()
        {
            InitializeComponent();
            BindingContext = new ClientCoachSelectionViewModel();
        }
    }
}