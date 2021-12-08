using BuiltDifferentMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(WorkoutId), nameof(WorkoutId))]
    public partial class ManageWorkoutPage : ContentPage
    {
        public int WorkoutId { get; set; }
        public ManageWorkoutPage()
        {
            InitializeComponent();
            BindingContext = new WorkoutManageViewModel(WorkoutId);
        }
    }
}