using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class ClientWorkoutViewModel : ViewModelBase
    {
        private IAccountService accountService = AccountService.Instance;
        private int clientId;
        public AsyncCommand<int> MarkDone { get; }
        private List<WorkoutDTO> OriginalWorkoutList { get; set; }
        public ObservableRangeCollection<WorkoutDTO> Workouts { get; set; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<int> EditCommand { get; }

        private int collectionViewHeight;
        public int CollectionViewHeight { get => collectionViewHeight; set => SetProperty(ref collectionViewHeight, value); }

        private DateTime SelectedDay { get; set; }

        public string WorkoutPageTitle { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public AsyncCommand<string> WeekdaySelectedCommand { get; set; }
        private int CurrentWeek { get; set; }
        public AsyncCommand<string> ChangeWeekCommand { get; set; }

        private string weekOfText;
        public string WeekOfText { get => weekOfText; set => SetProperty(ref weekOfText, value); }

        private DateTime Day0 { get; set; }
        private bool day0Selected;
        public bool Day0Selected { get => day0Selected; set => SetProperty(ref day0Selected, value); }

        private DateTime Day1 { get; set; }
        private bool day1Selected;
        public bool Day1Selected { get => day1Selected; set => SetProperty(ref day1Selected, value); }

        private DateTime Day2 { get; set; }
        private bool day2Selected;
        public bool Day2Selected { get => day2Selected; set => SetProperty(ref day2Selected, value); }

        private DateTime Day3 { get; set; }
        private bool day3Selected;
        public bool Day3Selected { get => day3Selected; set => SetProperty(ref day3Selected, value); }

        private DateTime Day4 { get; set; }
        private bool day4Selected;
        public bool Day4Selected { get => day4Selected; set => SetProperty(ref day4Selected, value); }

        private DateTime Day5 { get; set; }
        private bool day5Selected;
        public bool Day5Selected { get => day5Selected; set => SetProperty(ref day5Selected, value); }

        private DateTime Day6 { get; set; }
        private bool day6Selected;
        public bool Day6Selected { get => day6Selected; set => SetProperty(ref day6Selected, value); }

        public ClientWorkoutViewModel()
        {
            MarkDone = new AsyncCommand<int>(Done);
            WorkoutPageTitle = $"Your Workout Plan";
            var user = (Models.Client)accountService.CurrentUser;
            this.clientId = user.id;

            CurrentWeek = 0;
            ChangeWeekCommand = new AsyncCommand<string>(ChangeWeek);

            SelectedDay = DateTime.Now.Date;
            SetDayButtonValues((int)SelectedDay.DayOfWeek);

            WeekdaySelectedCommand = new AsyncCommand<string>(WeekdaySelected);
            WeekdaySelected((int)SelectedDay.DayOfWeek);

            AdjustCollectionViewHeight();
            GetWorkouts();
        }

        private void AdjustCollectionViewHeight() {
            if(Workouts == null || Workouts.Count == 0) {
                CollectionViewHeight = 300;
            }
            else if(Workouts.Count >= 5) {
                int newHeight = 350;
                for(int i = 1; i <= Workouts.Count - 5; i++) {
                    newHeight += 25;
                }
                CollectionViewHeight = newHeight;
            } else {
                CollectionViewHeight = 350;
            }
        }

        private Task ChangeWeek(string amount) {
            CurrentWeek += int.Parse(amount);
            SetDayButtonValues((int)SelectedDay.DayOfWeek, CurrentWeek);
            CheckAndSetIfSelected();
            return Task.CompletedTask;
        }

        private void CheckAndSetIfSelected() {
            string selectedDayToString = SelectedDay.ToString("MMMM dd, yyyy");

            if(Day0.ToString("MMMM dd, yyyy") == selectedDayToString) {
                SetCurrentlySelectedDay(0);
            }
            else if(Day1.ToString("MMMM dd, yyyy") == selectedDayToString) {
                SetCurrentlySelectedDay(1);
            } else if(Day2.ToString("MMMM dd, yyyy") == selectedDayToString) {
                SetCurrentlySelectedDay(2);
            } else if(Day3.ToString("MMMM dd, yyyy") == selectedDayToString) {
                SetCurrentlySelectedDay(3);
            } else if(Day4.ToString("MMMM dd, yyyy") == selectedDayToString) {
                SetCurrentlySelectedDay(4);
            } else if(Day5.ToString("MMMM dd, yyyy") == selectedDayToString) {
                SetCurrentlySelectedDay(5);
            } else if(Day6.ToString("MMMM dd, yyyy") == selectedDayToString) {
                SetCurrentlySelectedDay(6);
            } else {
                SetCurrentlySelectedDay(-1);
            }
        }

        private int GetDayOffset(int startingStep, int offset, char operand) {
            int currentStep = startingStep;
            int weeksNavigated = 0;
            if(operand == '-') {
                for(int i = -1; true; i--) {
                    currentStep--;
                    if(currentStep == -1) {
                        currentStep = 6;
                        weeksNavigated--;
                    }

                    if(weeksNavigated == offset) return i;
                }
            }
            else if(operand == '+') {
                for(int i = 1; true; i++) {
                    currentStep++;
                    if(currentStep == 7) {
                        currentStep = 0;
                        weeksNavigated++;
                    }

                    if(weeksNavigated == offset) return i;
                }
            }
            else {
                throw new Exception("Invalid Operand: " + operand);
            }
        }

        private void SetDayButtonValues(int currentDay, int offset = 0) {
            List<DateTime> week = new List<DateTime>() { new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime() };
            bool canLeft = true;
            bool canRight = true;

            if(offset == 0) {
                week[currentDay] = SelectedDay;
            } else if(offset < 0){
                week[6] = SelectedDay.AddDays(GetDayOffset((int)SelectedDay.DayOfWeek, offset, '-'));
            } else if(offset > 0) {
                week[0] = SelectedDay.AddDays(GetDayOffset((int)SelectedDay.DayOfWeek, offset, '+'));
            }
            for(int i = 1; true; i++) {
                if(canLeft) {
                    try {
                        if(offset == 0) {
                            week[currentDay - i] = SelectedDay.AddDays(-i);
                        } else {
                            week[week.Count - 1 - i] = week[6].AddDays(-i);
                        }
                    } catch { canLeft = false; }
                }

                if(canRight) {
                    try {
                        if(offset == 0) {
                            week[currentDay + i] = SelectedDay.AddDays(i);
                        } else {
                            week[i] = week[0].AddDays(i);
                        }
                    } catch { canRight = false; }
                }

                if(!canLeft && !canRight) break;
            }

            Day0 = week[0].Date;
            Day1 = week[1].Date;
            Day2 = week[2].Date;
            Day3 = week[3].Date;
            Day4 = week[4].Date;
            Day5 = week[5].Date;
            Day6 = week[6].Date;

            WeekOfText = $"{Day0:M} - {Day6:M}";
        }

        private void SetCurrentlySelectedDay(int day) {
            switch(day) {
                case 0:
                    Day0Selected = true;
                    Day1Selected = false;
                    Day2Selected = false;
                    Day3Selected = false;
                    Day4Selected = false;
                    Day5Selected = false;
                    Day6Selected = false;
                    SelectedDay = Day0;
                    break;
                case 1:
                    Day0Selected = false;
                    Day1Selected = true;
                    Day2Selected = false;
                    Day3Selected = false;
                    Day4Selected = false;
                    Day5Selected = false;
                    Day6Selected = false;
                    SelectedDay = Day1;
                    break;
                case 2:
                    Day0Selected = false;
                    Day1Selected = false;
                    Day2Selected = true;
                    Day3Selected = false;
                    Day4Selected = false;
                    Day5Selected = false;
                    Day6Selected = false;
                    SelectedDay = Day2;
                    break;
                case 3:
                    Day0Selected = false;
                    Day1Selected = false;
                    Day2Selected = false;
                    Day3Selected = true;
                    Day4Selected = false;
                    Day5Selected = false;
                    Day6Selected = false;
                    SelectedDay = Day3;
                    break;
                case 4:
                    Day0Selected = false;
                    Day1Selected = false;
                    Day2Selected = false;
                    Day3Selected = false;
                    Day4Selected = true;
                    Day5Selected = false;
                    Day6Selected = false;
                    SelectedDay = Day4;
                    break;
                case 5:
                    Day0Selected = false;
                    Day1Selected = false;
                    Day2Selected = false;
                    Day3Selected = false;
                    Day4Selected = false;
                    Day5Selected = true;
                    Day6Selected = false;
                    SelectedDay = Day5;
                    break;
                case 6:
                    Day0Selected = false;
                    Day1Selected = false;
                    Day2Selected = false;
                    Day3Selected = false;
                    Day4Selected = false;
                    Day5Selected = false;
                    Day6Selected = true;
                    SelectedDay = Day6;
                    break;
                default:
                    Day0Selected = false;
                    Day1Selected = false;
                    Day2Selected = false;
                    Day3Selected = false;
                    Day4Selected = false;
                    Day5Selected = false;
                    Day6Selected = false;
                    break;
            }
        }

        private Task WeekdaySelected(string day) {
            SetCurrentlySelectedDay(int.Parse(day));
            CurrentWeek = 0;
            SetDayButtonValues((int)SelectedDay.DayOfWeek);
            FilterWorkouts();
            return Task.CompletedTask;
        }

        private Task WeekdaySelected(int day) {
            SetCurrentlySelectedDay(day);
            CurrentWeek = 0;
            SetDayButtonValues((int)SelectedDay.DayOfWeek);
            FilterWorkouts();
            return Task.CompletedTask;
        }

        public async Task GetWorkouts()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<WorkoutDTO>>(APIConstants.GetWorkoutsByClientId(clientId));
            if (result == null || result.Count == 0 )
            {
                OriginalWorkoutList = null;
                return;
            }
            OriginalWorkoutList = result.ToList();
            FilterWorkouts();
        }

        public void FilterWorkouts() {
            if (
               OriginalWorkoutList == null ||
               Day0Selected == false &&
               Day1Selected == false &&
               Day2Selected == false &&
               Day3Selected == false &&
               Day4Selected == false &&
               Day5Selected == false &&
               Day6Selected == false
            ) return;

            Workouts = new ObservableRangeCollection<WorkoutDTO>(OriginalWorkoutList.Where(x => x.day.ToString("MMMM dd, yyyy") == SelectedDay.ToString("MMMM dd, yyyy")));
            AdjustCollectionViewHeight();
            OnPropertyChanged("Workouts");
        }


        public async Task Done(int id)
        {

            var route = APIConstants.GetWorkoutsByWorkoutId(id);

            var workout = await networkService.GetAsync<Workout>(route);
            if(workout == null) return;

            if (workout.isCompleted == false)
            {
                workout.isCompleted = true;

                var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.UpdateWorkoutByWorkoutId(id), workout);
                if(result == null) return;

                var httpCode = result.StatusCode;

                if (httpCode == System.Net.HttpStatusCode.OK)
                {
                    await Application.Current.MainPage.DisplayAlert("Good", "Workout set as done", "OK");
                    GetWorkouts();
                }
                else
                {
                    return;
                }
            }
            else
            {
                workout.isCompleted = false;

                var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.UpdateWorkoutByWorkoutId(id), workout);
                if(result == null) return;

                var httpCode = result.StatusCode;

                if (httpCode == System.Net.HttpStatusCode.OK)
                {

                    await Application.Current.MainPage.DisplayAlert("Good", "Workout set as not done", "OK");
                    GetWorkouts();
                }
                else
                {
                    return;
                }
            }




        }
    }
}
