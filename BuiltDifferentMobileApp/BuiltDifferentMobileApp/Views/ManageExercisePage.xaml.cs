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
    public partial class ManageExercisePage : ContentPage
    {
        public ManageExercisePage()
        {
            InitializeComponent();
            BindingContext = new ExerciseManageViewModel();
        }
    }
}