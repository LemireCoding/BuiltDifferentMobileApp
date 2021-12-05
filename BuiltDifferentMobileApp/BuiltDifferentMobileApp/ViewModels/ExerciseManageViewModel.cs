using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BuiltDifferentMobileApp.ViewModels
{
    public class ExerciseManageViewModel: ViewModelBase
    {
        public AsyncCommand SaveCommand { get; }

        public ExerciseManageViewModel()
        {
            SaveCommand = new AsyncCommand(SaveExercise);
        }

        private async Task SaveExercise()
        {
            throw new NotImplementedException();
        }
    }
}
