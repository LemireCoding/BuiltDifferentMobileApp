﻿using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BuiltDifferentMobileApp.ViewModels.Client
{
    public class ClientRequestsCenterViewModel : ViewModelBase
    {
        private IAccountService accountService = AccountService.Instance;
        private int clientId;
 
        public ObservableRangeCollection<Request> Requests { get; set; }

        public AsyncCommand <Request>CancelRequest { get; }


        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public ClientRequestsCenterViewModel()
        {
            var user = (Models.Client)accountService.CurrentUser;
            this.clientId = user.id;
            CancelRequest = new AsyncCommand<Request>(Cancel);
            GetRequests();
        }

        public async Task GetRequests()
        {
            var result = await networkService.GetAsync<ObservableRangeCollection<Request>>(APIConstants.GetAllRequestsByClient(clientId));
            if (result == null || result.Count == 0)
            {
                return;
            }

            foreach(Request request in result)
            {
                var coach = await networkService.GetAsync<Models.Coach>(APIConstants.GetCoachByCoachId(request.coachId));
                if (coach == null)
                {
                    request.coachName = "";
                } else
                {
                    request.coachName = coach.name;
                }
            }
            Requests = new ObservableRangeCollection<Request>(result.OrderBy(request => request.status));
            OnPropertyChanged("Requests");
        }

        public async Task Cancel(Request request)
        {
            return;
        }





    }
    }
