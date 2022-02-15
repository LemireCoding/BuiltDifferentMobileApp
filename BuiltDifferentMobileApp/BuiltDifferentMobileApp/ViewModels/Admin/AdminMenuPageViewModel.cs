using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Admin {
    public class AdminMenuPageViewModel : ViewModelBase {

        private List<UserDateRoleDTO> Users { get; set; }
        private List<CertificationDetailsDTO> Certifications { get; set; }

        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        private IAccountService accountService = AccountService.Instance;

        public AsyncCommand ViewMyProfileCommand { get; }
        public AsyncCommand<string> ChangeWeekCommand { get; set; }

        private int currentWeek;
        public int CurrentWeek { get => currentWeek; set => SetProperty(ref currentWeek, value); }

        private string weekOfText;
        public string WeekOfText { get => weekOfText; set => SetProperty(ref weekOfText, value); }

        private DateTime CurrentDate { get; set; }

        private DateTime Day0 { get; set; }

        private DateTime Day6 { get; set; }

        public Chart WeeklyGrowth { get; set; }
        public Chart ClientCoachPie { get; set; }
        public Chart CoachesApplying { get; set; }

        private int totalCoaches;
        public int TotalCoaches { get => totalCoaches; set => SetProperty(ref totalCoaches, value); }

        private int totalClients;
        public int TotalClients { get => totalClients; set => SetProperty(ref totalClients, value); }

        public AdminMenuPageViewModel() {
            Title = "Reports";
            ViewMyProfileCommand = new AsyncCommand(accountService.ViewMyProfileCommand);
            ChangeWeekCommand = new AsyncCommand<string>(ChangeWeek);
            CurrentWeek = 0;
            TotalClients = 0;
            CurrentDate = DateTime.Now.Date;
            SetWeekOfText(0);
            GetUserTotals();
            FetchData();
        }

        private async Task FetchData() {
            var users = await networkService.GetAsync<List<UserDateRoleDTO>>(APIConstants.GetUsersCreatedBetweenRange(Day0, Day6));
            var certifications = await networkService.GetAsync<List<CertificationDetailsDTO>>(APIConstants.GetCertificationStatusesCreatedBetweenRange(Day0, Day6));

            if(users == null) {
                Users = new List<UserDateRoleDTO>();
            } else {
                Users = new List<UserDateRoleDTO>(users);
            }

            if(certifications == null) {
                Certifications = new List<CertificationDetailsDTO>();
            } else {
                Certifications = new List<CertificationDetailsDTO>(certifications);
            }

            if(users != null || Certifications != null) await FilterData();
        }

        private async Task<int> GetUserCount(DateTime endDate) {
            var users = await networkService.GetAsync<List<UserDateRoleDTO>>(APIConstants.GetUsersCreatedBetweenRange(new DateTime(2021, 1, 1), endDate));
            return users != null ? users.Count() : 0;
        }

        private async Task GetUserTotals() {
            var userTotals = await networkService.GetAsync<List<UserDateRoleDTO>>(APIConstants.GetUsersCreatedBetweenRange(new DateTime(2021, 1, 1), Day6.AddDays(1)));
            if(userTotals != null) {
                TotalClients = userTotals.Count(u => u.role.ToLower() == "client");
                TotalCoaches = userTotals.Count(u => u.role.ToLower() == "coach");
            }
        }

        private async Task FilterData() {
            List<ChartEntry> entries = new List<ChartEntry>();
            List<ChartEntry> roles = new List<ChartEntry>();
            List<ChartEntry> certifications = new List<ChartEntry>();

            int preUserCount = await GetUserCount(Day0.AddDays(-1));
            for(int i = 0; i < 7; i++) {
                DateTime addedDate = Day0.Date.AddDays(i).Date;
                var query = Users.Where(u => u.dateCreated.Date == addedDate);
                preUserCount += query.Count();

                entries.Add(new ChartEntry(preUserCount) {
                    Label = addedDate.ToString("M"),
                    ValueLabel = preUserCount.ToString(),
                    Color = SKColor.Parse("#2c3e50")
                });
            }

            var coaches = Users.Where(u => u.role.ToLower() == "coach");
            var clients = Users.Where(u => u.role.ToLower() == "client");

            roles.Add(new ChartEntry(coaches.Count()) {
                Label = "Coaches",
                ValueLabel = coaches.Count().ToString(),
                Color = SKColor.Parse("#77d065"),
                ValueLabelColor = SKColor.Parse("#77d065")
            });

            roles.Add(new ChartEntry(clients.Count()) {
                Label = "Clients",
                ValueLabel = clients.Count().ToString(),
                Color = SKColor.Parse("#b455b6"),
                ValueLabelColor = SKColor.Parse("#b455b6")
            });

            for(int i = 0; i < 7; i++) {
                DateTime addedDate = Day0.Date.AddDays(i).Date;
                var query = Certifications.Where(c => c.dateCreated.Date == addedDate);

                certifications.Add(new ChartEntry(query.Count()) {
                    Label = addedDate.ToString("M"),
                    ValueLabel = query.Count().ToString(),
                    Color = SKColor.Parse("#2c3e50")
                });
            }

            WeeklyGrowth = new LineChart() { Entries = entries, LabelTextSize = 30, LineMode = LineMode.Straight, ValueLabelOrientation = Orientation.Horizontal, MaxValue = preUserCount * 2, MinValue = 0 };
            ClientCoachPie = new PieChart() { Entries = roles, LabelTextSize = 30 };
            CoachesApplying = new BarChart() { Entries = certifications, LabelTextSize = 30 };

            OnPropertyChanged("WeeklyGrowth");
            OnPropertyChanged("ClientCoachPie");
            OnPropertyChanged("CoachesApplying");
        } 

        private async Task ChangeWeek(string amount) {
            int increase = int.Parse(amount);
            if(CurrentWeek + increase > 0) return;

            CurrentWeek += increase;
            SetWeekOfText(CurrentWeek);
            await FetchData();
        }

        private void SetWeekOfText(int offset) {
            Day0 = CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek + (int)DayOfWeek.Sunday + (7 * offset));
            Day6 = Day0.AddDays(6);

            WeekOfText = $"{Day0:M} - {Day6:M}";
        }

    }
}
