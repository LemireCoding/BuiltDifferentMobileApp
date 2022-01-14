using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.ViewModels.Profile {
    public class MyProfilePageClientViewModel : ViewModelBase {

        private object profilePicture;
        public object ProfilePicture
        {
            get => profilePicture;
            set => SetProperty(ref profilePicture, value);
        }
        public FileResult PhotoPath { get; private set; }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int StartWeight { get; set; }

        private int currentWeight;
        public int CurrentWeight
        {
            get => currentWeight;
            set => SetProperty(ref currentWeight, value);
        }

        public AsyncCommand SubmitCommand { get; }
        public AsyncCommand UploadImageCommand { get; }
        public AsyncCommand EditProfileCommand { get; }

        private IAccountService accountService = AccountService.Instance;

        public MyProfilePageClientViewModel() {
            Models.Client user = (Models.Client)accountService.CurrentUser;

            Name = user.name;
            ProfilePicture = user.profilePicture;
            StartWeight = user.startWeight;
            CurrentWeight = user.currentWeight;

            SubmitCommand = new AsyncCommand(Submit);
            UploadImageCommand = new AsyncCommand(Upload);
            EditProfileCommand = new AsyncCommand(Edit);
        }

        private Task Edit()
        {
            throw new NotImplementedException();
        }

        private async Task Upload()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please Pick a Profile Picture"
            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                ProfilePicture = ImageSource.FromStream(() => stream);
            }
            OnPropertyChanged("ProfilePicture");

        }

        private Task Submit()
        {
            throw new NotImplementedException();
        }
    }
}
