using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BuiltDifferentMobileApp.ViewModels
{
    public class WorkoutViewModel:ViewModelBase
    {
        public AsyncCommand SaveCommand { get; }
        public WorkoutViewModel()
        {

            SaveCommand = new AsyncCommand(SaveWorkout);
        }

        private async Task SaveWorkout()
        {
            throw new NotImplementedException();
        }
    }
}
