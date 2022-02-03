﻿using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Ressource;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Profile {
    public class MyProfilePageClientViewModel : ViewModelBase {

        private string profilePicture;
        public string ProfilePicture
        {
            get => profilePicture;
            set
            {
                SetProperty(ref profilePicture, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }
        public FileResult PhotoPath { get; private set; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }
        public int startWeight;
        public int StartWeight
        {
            get => startWeight;
            set
            {
                SetProperty(ref startWeight, value);
            }
        }
        private int currentWeight;
        public int CurrentWeight
        {
            get => currentWeight;
            set {
                SetProperty(ref currentWeight, value);
                OnPropertyChanged(nameof(IsEnabled));

            }
        }
        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }
        private int UserId;
        private int Id;

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand UploadImageCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;
        private INetworkService<HttpResponseMessage> networkService = NetworkService<HttpResponseMessage>.Instance;

        public MyProfilePageClientViewModel() {
            Name = "";
            StartWeight = 0;
            CurrentWeight = 0;
            GetUserInfo();

            isEnabled = false;

            SubmitCommand = new AsyncCommand(Submit);
            UploadImageCommand = new AsyncCommand(Upload);
            EditProfileCommand = new AsyncCommand(Edit);
        }

        private async Task GetUserInfo()
        {
            Models.Client user = (Models.Client)accountService.CurrentUser;
            var userInfo = await networkService.GetAsync<Models.Client>(APIConstants.GetClientProfileUri(user.userId));

            if (userInfo == null)
            {
                await Application.Current.MainPage.DisplayAlert("Could not load client's profile!", "Returning to previous page", "OK");
                await Shell.Current.GoToAsync("..");
                IsBusy = false;
                return;
            }
            else
            {
                accountService.CurrentUser = userInfo;

                Id = userInfo.id;
                Name = userInfo.name;
                UserId = userInfo.userId;
                ProfilePicture = userInfo.profilePicture;
                StartWeight = userInfo.startWeight;
                CurrentWeight = userInfo.currentWeight;
            }            
        }

        private async Task Edit()
        {
            if (!IsEnabled)
            {
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
                GetUserInfo();
            }
        }

        private async Task Upload()
        {
            throw new NotImplementedException();

            //This has been left commented in order to facilitate profilePicture Upload

            //var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            //{
            //    Title = "Please Pick a Profile Picture"
            //});
            //if (result != null)
            //{
            //    var stream = await result.OpenReadAsync();
            //    ProfilePicture = ImageSource.FromStream(() => stream);
            //}
            //OnPropertyChanged("ProfilePicture");
        }

        private async Task Submit()
        {
            if (
                string.IsNullOrEmpty(Name)
                )
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelFieldIssueTitle, AppResource.ViewModelFieldIssueMessage, "OK");
                return;
            }

            var profile = new ClientProfileDTO(Name, UserId, CurrentWeight, ProfilePicture);
            var test = JsonConvert.SerializeObject(profile);
            var result = await networkService.PutAsync<HttpResponseMessage>(APIConstants.PutProfileUri(UserId), profile);
            var httpCode = result.StatusCode;

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelSuccessTitle, AppResource.ViewModelProfileSuccessMessage, "OK");
            }
            else if (httpCode == System.Net.HttpStatusCode.NotFound)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelErrorTitle, AppResource.ViewModelErrorMessage, "OK");
            }
            else if (httpCode == System.Net.HttpStatusCode.InternalServerError)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.ViewModelErrorTitle, AppResource.ViewModelErrorMessage, "OK");
            }
            else
                return;
        }
    }
}
