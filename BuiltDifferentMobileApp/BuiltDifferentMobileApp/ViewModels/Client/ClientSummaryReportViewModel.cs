using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
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
    
  
    public class ClientSummaryReportViewModel : ViewModelBase
    {


        public Chart WorkoutsCompleted { get; set; }

        

        private IAccountService accountService = AccountService.Instance;
        private int clientId;
      
       
        private DateTime SelectedDay { get; set; }

       private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public AsyncCommand<string> WeekdaySelectedCommand { get; set; }
        private int CurrentWeek { get; set; }
        public AsyncCommand<string> ChangeWeekCommand { get; set; }

        private string weekOfText;
        public string WeekOfText { get => weekOfText; set => SetProperty(ref weekOfText, value); }

        private string day0Text;
        public string Day0Text { get => day0Text; set => SetProperty(ref day0Text, value); }

        private string day1Text;
        public string Day1Text { get => day1Text; set => SetProperty(ref day1Text, value); }

        private string day2Text;
        public string Day2Text { get => day2Text; set => SetProperty(ref day2Text, value); }

        private string day3Text;
        public string Day3Text { get => day3Text; set => SetProperty(ref day3Text, value); }

        private string day4Text;
        public string Day4Text { get => day4Text; set => SetProperty(ref day4Text, value); }

        private string day5Text;
        public string Day5Text { get => day5Text; set => SetProperty(ref day5Text, value); }

        private string day6Text;
        public string Day6Text { get => day6Text; set => SetProperty(ref day6Text, value); }

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

        public double dailyprogress;
        public double Dailyprogress { get => dailyprogress; set => SetProperty(ref dailyprogress, value); }
        public string dailyprogresstext;
        public string Dailyprogresstext { get => dailyprogresstext; set => SetProperty(ref dailyprogresstext, value); }

        private string startWeight;
        public string StartWeight { get => startWeight; set => SetProperty(ref startWeight, value); }

        private string currentWeight;
        public string CurrentWeight { get => currentWeight; set => SetProperty(ref currentWeight, value); }

        private string differenceInPds;
        public string DifferenceInPds { get => differenceInPds; set => SetProperty(ref differenceInPds, value); }

        private string bmi;
        public string Bmi { get => bmi; set => SetProperty(ref bmi, value); }

        

        public double progressDay0Eaten;
        public double ProgressDay0Eaten { get => progressDay0Eaten; set => SetProperty(ref progressDay0Eaten, value); }

        public double progressDay1Eaten;
        public double ProgressDay1Eaten { get => progressDay1Eaten; set => SetProperty(ref progressDay1Eaten, value); }

        public double progressDay2Eaten;
        public double ProgressDay2Eaten { get => progressDay2Eaten; set => SetProperty(ref progressDay2Eaten, value); }

        public double progressDay3Eaten;
        public double ProgressDay3Eaten { get => progressDay3Eaten; set => SetProperty(ref progressDay3Eaten, value); }

        public double progressDay4Eaten;
        public double ProgressDay4Eaten { get => progressDay4Eaten; set => SetProperty(ref progressDay4Eaten, value); }

        public double progressDay5Eaten;
        public double ProgressDay5Eaten { get => progressDay5Eaten; set => SetProperty(ref progressDay5Eaten, value); }

        public double progressDay6Eaten;
        public double ProgressDay6Eaten { get => progressDay6Eaten; set => SetProperty(ref progressDay6Eaten, value); }


        public double progressDay0Completed;
        public double ProgressDay0Completed { get => progressDay0Completed; set => SetProperty(ref progressDay0Completed, value); }

        public double progressDay1Completed;
        public double ProgressDay1Completed { get => progressDay1Completed; set => SetProperty(ref progressDay1Completed, value); }

        public double progressDay2Completed;
        public double ProgressDay2Completed { get => progressDay2Completed; set => SetProperty(ref progressDay2Completed, value); }

        public double progressDay3Completed;
        public double ProgressDay3Completed { get => progressDay3Completed; set => SetProperty(ref progressDay3Completed, value); }

        public double progressDay4Completed;
        public double ProgressDay4Completed { get => progressDay4Completed; set => SetProperty(ref progressDay4Completed, value); }

        public double progressDay5Completed;
        public double ProgressDay5Completed { get => progressDay5Completed; set => SetProperty(ref progressDay5Completed, value); }

        public double progressDay6Completed;
        public double ProgressDay6Completed { get => progressDay6Completed; set => SetProperty(ref progressDay6Completed, value); }



        public double progressDay0Skip;
        public double ProgressDay0Skip { get => progressDay0Skip; set => SetProperty(ref progressDay0Skip, value); }

        public double progressDay1Skip;
        public double ProgressDay1Skip { get => progressDay1Skip; set => SetProperty(ref progressDay1Skip, value); }

        public double progressDay2Skip;
        public double ProgressDay2Skip { get => progressDay2Skip; set => SetProperty(ref progressDay2Skip, value); }

        public double progressDay3Skip;
        public double ProgressDay3Skip { get => progressDay3Skip; set => SetProperty(ref progressDay3Skip, value); }

        public double progressDay4Skip;
        public double ProgressDay4Skip { get => progressDay4Skip; set => SetProperty(ref progressDay4Skip, value); }

        public double progressDay5Skip;
        public double ProgressDay5Skip { get => progressDay5Skip; set => SetProperty(ref progressDay5Skip, value); }

        public double progressDay6Skip;
        public double ProgressDay6Skip { get => progressDay6Skip; set => SetProperty(ref progressDay6Skip, value); }



        public double progressDay0SkipW;
        public double ProgressDay0SkipW { get => progressDay0SkipW; set => SetProperty(ref progressDay0SkipW, value); }

        public double progressDay1SkipW;
        public double ProgressDay1SkipW { get => progressDay1SkipW; set => SetProperty(ref progressDay1SkipW, value); }

        public double progressDay2SkipW;
        public double ProgressDay2SkipW { get => progressDay2SkipW; set => SetProperty(ref progressDay2SkipW, value); }

        public double progressDay3SkipW;
        public double ProgressDay3SkipW { get => progressDay3SkipW; set => SetProperty(ref progressDay3SkipW, value); }

        public double progressDay4SkipW;
        public double ProgressDay4SkipW { get => progressDay4SkipW; set => SetProperty(ref progressDay4SkipW, value); }

        public double progressDay5SkipW;
        public double ProgressDay5SkipW { get => progressDay5SkipW; set => SetProperty(ref progressDay5SkipW, value); }

        public double progressDay6SkipW;
        public double ProgressDay6SkipW { get => progressDay6SkipW; set => SetProperty(ref progressDay6SkipW, value); }

      

        public AsyncCommand ViewMyProfileCommand { get; }
        public ClientSummaryReportViewModel()
        {
            
            var user = (Models.Client)accountService.CurrentUser;
            this.clientId = user.id;
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
            CurrentWeek = 0;
            ChangeWeekCommand = new AsyncCommand<string>(ChangeWeek);

            SelectedDay = DateTime.Now.Date;
            SetDayButtonValues((int)SelectedDay.DayOfWeek);

            WeekdaySelectedCommand = new AsyncCommand<string>(WeekdaySelected);
            WeekdaySelected((int)SelectedDay.DayOfWeek);

            CalculateWeightProgress(user);
       

        }

        public void CalculateWeightProgress(Models.Client user)
        {
           
            if(user.currentWeight == 0)
            {
                StartWeight = (user.startWeight).ToString();
                CurrentWeight = "/";
                DifferenceInPds = "/";
                double kg = user.startWeight / 2.2046;
                Bmi = String.Format("{0:0.00}", ((kg / user.height / user.height) * 10000));
            } else
            {
                StartWeight = (user.startWeight).ToString();
                DifferenceInPds = (Int64.Parse(CurrentWeight) - Int64.Parse(StartWeight)).ToString();
                CurrentWeight = (user.currentWeight).ToString();

                double kg = user.currentWeight / 2.2046;
                Bmi = String.Format("{0:0.00}", ((kg / user.height / user.height) * 10000));
            }
           

        }

    
       

        private Task ChangeWeek(string amount)
        {
            CurrentWeek += int.Parse(amount);
            SetDayButtonValues((int)SelectedDay.DayOfWeek, CurrentWeek);
            CheckAndSetIfSelected();
            return Task.CompletedTask;
        }

        private void CheckAndSetIfSelected()
        {
            string selectedDayToString = SelectedDay.ToString("MMMM dd, yyyy");

            if (Day0.ToString("MMMM dd, yyyy") == selectedDayToString)
            {
                SetCurrentlySelectedDay(0);
            }
            else if (Day1.ToString("MMMM dd, yyyy") == selectedDayToString)
            {
                SetCurrentlySelectedDay(1);
            }
            else if (Day2.ToString("MMMM dd, yyyy") == selectedDayToString)
            {
                SetCurrentlySelectedDay(2);
            }
            else if (Day3.ToString("MMMM dd, yyyy") == selectedDayToString)
            {
                SetCurrentlySelectedDay(3);
            }
            else if (Day4.ToString("MMMM dd, yyyy") == selectedDayToString)
            {
                SetCurrentlySelectedDay(4);
            }
            else if (Day5.ToString("MMMM dd, yyyy") == selectedDayToString)
            {
                SetCurrentlySelectedDay(5);
            }
            else if (Day6.ToString("MMMM dd, yyyy") == selectedDayToString)
            {
                SetCurrentlySelectedDay(6);
            }
            else
            {
                SetCurrentlySelectedDay(-1);
            }
        }

        private int GetDayOffset(int startingStep, int offset, char operand)
        {
            int currentStep = startingStep;
            int weeksNavigated = 0;
            if (operand == '-')
            {
                for (int i = -1; true; i--)
                {
                    currentStep--;
                    if (currentStep == -1)
                    {
                        currentStep = 6;
                        weeksNavigated--;
                    }

                    if (weeksNavigated == offset) return i;
                }
            }
            else if (operand == '+')
            {
                for (int i = 1; true; i++)
                {
                    currentStep++;
                    if (currentStep == 7)
                    {
                        currentStep = 0;
                        weeksNavigated++;
                    }

                    if (weeksNavigated == offset) return i;
                }
            }
            else
            {
                throw new Exception("Invalid Operand: " + operand);
            }
        }

        private void SetDayButtonValues(int currentDay, int offset = 0)
        {
            List<DateTime> week = new List<DateTime>() { new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime() };
            bool canLeft = true;
            bool canRight = true;

            if (offset == 0)
            {
                week[currentDay] = SelectedDay;
            }
            else if (offset < 0)
            {
                week[6] = SelectedDay.AddDays(GetDayOffset((int)SelectedDay.DayOfWeek, offset, '-'));
            }
            else if (offset > 0)
            {
                week[0] = SelectedDay.AddDays(GetDayOffset((int)SelectedDay.DayOfWeek, offset, '+'));
            }
            for (int i = 1; true; i++)
            {
                if (canLeft)
                {
                    try
                    {
                        if (offset == 0)
                        {
                            week[currentDay - i] = SelectedDay.AddDays(-i);
                        }
                        else
                        {
                            week[week.Count - 1 - i] = week[6].AddDays(-i);
                        }
                    }
                    catch { canLeft = false; }
                }

                if (canRight)
                {
                    try
                    {
                        if (offset == 0)
                        {
                            week[currentDay + i] = SelectedDay.AddDays(i);
                        }
                        else
                        {
                            week[i] = week[0].AddDays(i);
                        }
                    }
                    catch { canRight = false; }
                }

                if (!canLeft && !canRight) break;
            }

            Day0 = week[0].Date;
            Day1 = week[1].Date;
            Day2 = week[2].Date;
            Day3 = week[3].Date;
            Day4 = week[4].Date;
            Day5 = week[5].Date;
            Day6 = week[6].Date;

            Day0Text = $"{Day0:M}";
            Day1Text = $"{Day1:M}";
            Day2Text = $"{Day2:M}";
            Day3Text = $"{Day3:M}";
            Day4Text = $"{Day4:M}";
            Day5Text = $"{Day5:M}";
            Day6Text = $"{Day6:M}";

            CalculateMeals();
            CalculateWorkouts();
            WeekOfText = $"{Day0:M} - {Day6:M}";
        }

        public async Task CalculateWorkouts()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<Workout>>(APIConstants.GetWorkoutsByClientId(clientId));
            if (result == null || result.Count == 0)
            {
                return;
            }

            ObservableRangeCollection<Workout> workoutsDay0 = new ObservableRangeCollection<Workout>(result.Where(x => (x.day.Date == Day0)));
            ObservableRangeCollection<Workout> workoutsDay1 = new ObservableRangeCollection<Workout>(result.Where(x => (x.day.Date == Day1)));
            ObservableRangeCollection<Workout> workoutsDay2 = new ObservableRangeCollection<Workout>(result.Where(x => (x.day.Date == Day2)));
            ObservableRangeCollection<Workout> workoutsDay3 = new ObservableRangeCollection<Workout>(result.Where(x => (x.day.Date == Day3)));
            ObservableRangeCollection<Workout> workoutsDay4 = new ObservableRangeCollection<Workout>(result.Where(x => (x.day.Date == Day4)));
            ObservableRangeCollection<Workout> workoutsDay5 = new ObservableRangeCollection<Workout>(result.Where(x => (x.day.Date == Day5)));
            ObservableRangeCollection<Workout> workoutsDay6 = new ObservableRangeCollection<Workout>(result.Where(x => (x.day.Date == Day6)));

            int totalWorkoutsDay0 = workoutsDay0.Count();
            int totalWorkoutsDay1 = workoutsDay1.Count();
            int totalWorkoutsDay2 = workoutsDay2.Count();
            int totalWorkoutsDay3 = workoutsDay3.Count();
            int totalWorkoutsDay4 = workoutsDay4.Count();
            int totalWorkoutsDay5 = workoutsDay5.Count();
            int totalWorkoutsDay6 = workoutsDay6.Count();

            int workoutsCompletedDay0 = workoutsDay0.Count(x => x.isCompleted == true);
            int workoutsCompletedDay1 = workoutsDay1.Count(x => x.isCompleted == true);
            int workoutsCompletedDay2 = workoutsDay2.Count(x => x.isCompleted == true);
            int workoutsCompletedDay3 = workoutsDay3.Count(x => x.isCompleted == true);
            int workoutsCompletedDay4 = workoutsDay4.Count(x => x.isCompleted == true);
            int workoutsCompletedDay5 = workoutsDay5.Count(x => x.isCompleted == true);
            int workoutsCompletedDay6 = workoutsDay6.Count(x => x.isCompleted == true);

            ProgressDay0Completed = Convert.ToDouble(workoutsCompletedDay0) / Convert.ToDouble(totalWorkoutsDay0);
            ProgressDay0SkipW = 1.0 - ProgressDay0Completed;

            ProgressDay1Completed = Convert.ToDouble(workoutsCompletedDay1) / Convert.ToDouble(totalWorkoutsDay1);
            ProgressDay1SkipW = 1.0 - ProgressDay1Completed;

            ProgressDay2Completed = Convert.ToDouble(workoutsCompletedDay2) / Convert.ToDouble(totalWorkoutsDay2);
            ProgressDay2SkipW = 1.0 - ProgressDay2Completed;

            ProgressDay3Completed = Convert.ToDouble(workoutsCompletedDay3) / Convert.ToDouble(totalWorkoutsDay3);
            ProgressDay3SkipW = 1.0 - ProgressDay3Completed;

            ProgressDay4Completed = Convert.ToDouble(workoutsCompletedDay4) / Convert.ToDouble(totalWorkoutsDay4);
            ProgressDay4SkipW = 1.0 - ProgressDay4Completed;

            ProgressDay5Completed = Convert.ToDouble(workoutsCompletedDay5) / Convert.ToDouble(totalWorkoutsDay5);
            ProgressDay5SkipW = 1.0 - ProgressDay5Completed;

            ProgressDay6Completed = Convert.ToDouble(workoutsCompletedDay6) / Convert.ToDouble(totalWorkoutsDay6);
            ProgressDay6SkipW = 1.0 - ProgressDay6Completed;



            OnPropertyChanged("ProgressDay0Completed");
            OnPropertyChanged("ProgressDay1Completed");
            OnPropertyChanged("ProgressDay2Completed");
            OnPropertyChanged("ProgressDay3Completed");
            OnPropertyChanged("ProgressDay4Completed");
            OnPropertyChanged("ProgressDay5Completed");
            OnPropertyChanged("ProgressDay6Completed");

            OnPropertyChanged("ProgressDay0SkipW");
            OnPropertyChanged("ProgressDay1SkipW");
            OnPropertyChanged("ProgressDay2SkipW");
            OnPropertyChanged("ProgressDay3SkipW");
            OnPropertyChanged("ProgressDay4SkipW");
            OnPropertyChanged("ProgressDay5SkipW");
            OnPropertyChanged("ProgressDay6SkipW");

        }

        public async Task CalculateMeals()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<Meal>>(APIConstants.GetMealsByClientId(clientId));
            if (result == null || result.Count == 0)
            {
                return;
            }

            ObservableRangeCollection<Meal> mealsDay0 = new ObservableRangeCollection<Meal>(result.Where(x => (x.day.Date == Day0)));
            ObservableRangeCollection<Meal> mealsDay1 = new ObservableRangeCollection<Meal>(result.Where(x => (x.day.Date == Day1)));
            ObservableRangeCollection<Meal> mealsDay2 = new ObservableRangeCollection<Meal>(result.Where(x => (x.day.Date == Day2)));
            ObservableRangeCollection<Meal> mealsDay3 = new ObservableRangeCollection<Meal>(result.Where(x => (x.day.Date == Day3)));
            ObservableRangeCollection<Meal> mealsDay4 = new ObservableRangeCollection<Meal>(result.Where(x => (x.day.Date == Day4)));
            ObservableRangeCollection<Meal> mealsDay5 = new ObservableRangeCollection<Meal>(result.Where(x => (x.day.Date == Day5)));
            ObservableRangeCollection<Meal> mealsDay6 = new ObservableRangeCollection<Meal>(result.Where(x => (x.day.Date == Day6)));

            int totalMealsDay0 = mealsDay0.Count();
            int totalMealsDay1 = mealsDay1.Count();
            int totalMealsDay2 = mealsDay2.Count();
            int totalMealsDay3 = mealsDay3.Count();
            int totalMealsDay4 = mealsDay4.Count();
            int totalMealsDay5 = mealsDay5.Count();
            int totalMealsDay6 = mealsDay6.Count();

            int mealsEatenDay0 = mealsDay0.Count(x => x.isEaten == true);
            int mealsEatenDay1 = mealsDay1.Count(x => x.isEaten == true);
            int mealsEatenDay2 = mealsDay2.Count(x => x.isEaten == true);
            int mealsEatenDay3 = mealsDay3.Count(x => x.isEaten == true);
            int mealsEatenDay4 = mealsDay4.Count(x => x.isEaten == true);
            int mealsEatenDay5 = mealsDay5.Count(x => x.isEaten == true);
            int mealsEatenDay6 = mealsDay6.Count(x => x.isEaten == true);

            ProgressDay0Eaten = Convert.ToDouble(mealsEatenDay0) / Convert.ToDouble(totalMealsDay0);
            ProgressDay0Skip = 1.0 - ProgressDay0Eaten;

            ProgressDay1Eaten = Convert.ToDouble(mealsEatenDay1) / Convert.ToDouble(totalMealsDay1);
            ProgressDay1Skip = 1.0 - ProgressDay1Eaten;

            ProgressDay2Eaten = Convert.ToDouble(mealsEatenDay2) / Convert.ToDouble(totalMealsDay2);
            ProgressDay2Skip = 1.0 - ProgressDay2Eaten;

            ProgressDay3Eaten = Convert.ToDouble(mealsEatenDay3) / Convert.ToDouble(totalMealsDay3);
            ProgressDay3Skip = 1.0 - ProgressDay3Eaten;

            ProgressDay4Eaten = Convert.ToDouble(mealsEatenDay4) / Convert.ToDouble(totalMealsDay4);
            ProgressDay4Skip = 1.0 - ProgressDay4Eaten;

            ProgressDay5Eaten = Convert.ToDouble(mealsEatenDay5) / Convert.ToDouble(totalMealsDay5);
            ProgressDay5Skip = 1.0 - ProgressDay5Eaten;

            ProgressDay6Eaten = Convert.ToDouble(mealsEatenDay6) / Convert.ToDouble(totalMealsDay6);
            ProgressDay6Skip = 1.0 - ProgressDay6Eaten;



            OnPropertyChanged("ProgressDay0Eaten");
            OnPropertyChanged("ProgressDay1Eaten");
            OnPropertyChanged("ProgressDay2Eaten");
            OnPropertyChanged("ProgressDay3Eaten");
            OnPropertyChanged("ProgressDay4Eaten");
            OnPropertyChanged("ProgressDay5Eaten");
            OnPropertyChanged("ProgressDay6Eaten");

            OnPropertyChanged("ProgressDay0Skip");
            OnPropertyChanged("ProgressDay1Skip");
            OnPropertyChanged("ProgressDay2Skip");
            OnPropertyChanged("ProgressDay3Skip");
            OnPropertyChanged("ProgressDay4Skip");
            OnPropertyChanged("ProgressDay5Skip");
            OnPropertyChanged("ProgressDay6Skip");

        }

        private void SetCurrentlySelectedDay(int day)
        {
            switch (day)
            {
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

        private Task WeekdaySelected(string day)
        {
            SetCurrentlySelectedDay(int.Parse(day));
            CurrentWeek = 0;
            SetDayButtonValues((int)SelectedDay.DayOfWeek);
            
            return Task.CompletedTask;
        }

        private Task WeekdaySelected(int day)
        {
            SetCurrentlySelectedDay(day);
            CurrentWeek = 0;
            SetDayButtonValues((int)SelectedDay.DayOfWeek);
            
            return Task.CompletedTask;
        }

    }
}
