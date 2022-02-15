using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Coach
{
    public class CoachSummaryReportViewModel : ViewModelBase
    {
        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;
        public CoachReport coachReport { get; set; }
        public List<Models.ClientReportInfo> clientReportInfo { get; set; }
        public AsyncCommand RefreshCommand { get; }

        public CoachSummaryReportViewModel()
        {
            coachReport = new CoachReport();
            clientReportInfo = new List<ClientReportInfo>();
            RefreshCommand = new AsyncCommand(FetchReport);
            FetchReport();

        }

        public async Task FetchReport()
        {
            IsBusy = true;

            int coachId = ((Models.Coach)accountService.CurrentUser).id;

            //coachInfo
            Models.Coach coach = await networkService.GetAsync<Models.Coach>(APIConstants.GetCoachByCoachId(coachId));
            
            //Number of clients
            List<Models.Client> clients = await networkService.GetAsync<List<Models.Client>>(APIConstants.GetClientsForCoachId(coachId));

            if (coach == null || clients == null)
            {
                await Application.Current.MainPage.DisplayAlert($"Error", "There was an issue retrieving some information. Please try again", "OK");
            }
            if(coachReport.coachName != null)
            {
                return;
                IsBusy = false;
            }

            else if (clients.Count > 0)
            {
                coachReport.coachName = coach.name;
                coachReport.numberOfClients = clients.Count;
                var datetime = DateTime.Now;
                coachReport.startDate = datetime.AddDays(-7).Date;
                coachReport.endDate = datetime.Date;

                foreach (Models.Client c in clients)
                {
                    ClientReportInfo client = new ClientReportInfo();
                    client.clientId = c.id;
                    client.clientName = c.name;
                    client.coachId = coachId;
                    client.coachName = coach.name;
                    client.caloriesConsumed = await CalculateCaloriesConsumed(c.id);
                    client.workoutsDone = await CalculateWorkoutsDone(c.id);
                    client.bmi = await CalculateBmi(c);

                    clientReportInfo.Add(client);
                }
                coachReport.clientReports = clientReportInfo;
            }
            else
                return;

            OnPropertyChanged("coachReport");
            IsBusy = false;
        }

        private async Task<double> CalculateCaloriesConsumed(int clientId)
        {
            ObservableRangeCollection<Models.Meal> clientMeals = await networkService.GetAsync<ObservableRangeCollection<Models.Meal>>(APIConstants.GetMealsByClientId(clientId));
            if (clientMeals == null)
            {
                await Application.Current.MainPage.DisplayAlert($"Error", "There was an issue retrieving some information. Please try again", "OK");
            }
            double sum = 0;
            foreach( Models.Meal m in clientMeals)
            {
                if (m.isEaten == true && m.day > coachReport.startDate && m.day< coachReport.endDate)
                    sum += m.calories;
            }
            return sum;
        }

        private async Task<int> CalculateWorkoutsDone(int clientId)
        {
            ObservableRangeCollection<Models.Workout> clientWorkouts = await networkService.GetAsync<ObservableRangeCollection<Models.Workout>>(APIConstants.GetWorkoutsByClientId(clientId));

            if(clientWorkouts == null)
            {
                await Application.Current.MainPage.DisplayAlert($"Error", "There was an issue retrieving some information. Please try again", "OK");
            }
            int sum = 0;
            foreach (Models.Workout w in clientWorkouts)
            {
                if (w.isCompleted == true && w.day > coachReport.startDate && w.day < coachReport.endDate)
                    sum ++;
            }
            return sum;
        }

        private async Task<double> CalculateBmi(Models.Client client)
        {
            double bmi = 0;
            double heightIN = client.height;
            int weightLBS = client.currentWeight;
            bmi = weightLBS / Math.Pow(heightIN, 2) * 703;
            return bmi;
        }
    }
}
