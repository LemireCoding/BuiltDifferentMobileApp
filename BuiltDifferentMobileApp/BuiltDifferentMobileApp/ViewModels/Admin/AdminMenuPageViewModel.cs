using BuiltDifferentMobileApp.Services.AccountServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BuiltDifferentMobileApp.ViewModels.Admin {
    public class AdminMenuPageViewModel : ViewModelBase {

        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand ViewMyProfileCommand { get; }
        public AsyncCommand<string> ChangeWeekCommand { get; set; }

        private int CurrentWeek { get; set; }

        private string weekOfText;
        public string WeekOfText { get => weekOfText; set => SetProperty(ref weekOfText, value); }

        private DateTime CurrentDate { get; set; }

        private DateTime Day0 { get; set; }

        private DateTime Day6 { get; set; }

        public AdminMenuPageViewModel() {
            Title = "Reports";
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
            ChangeWeekCommand = new AsyncCommand<string>(ChangeWeek);
            CurrentWeek = 0;
            CurrentDate = DateTime.Now.Date;
            SetWeekOfText(0);
        }

        private Task ChangeWeek(string amount) {
            CurrentWeek += int.Parse(amount);
            SetWeekOfText(CurrentWeek);
            return Task.CompletedTask;
        }

        private void SetWeekOfText(int offset) {
            Day0 = CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek + (int)DayOfWeek.Sunday + (7 * offset));
            Day6 = Day0.AddDays(6);

            WeekOfText = $"{Day0:M} - {Day6:M}";
        }

    }
}
