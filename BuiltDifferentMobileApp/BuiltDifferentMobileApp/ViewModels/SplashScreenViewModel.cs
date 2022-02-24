using BuiltDifferentMobileApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.ViewModels {
    public class SplashScreenViewModel : ViewModelBase {

        public string Logo { get; set; }

        private bool pageLoaded;
        public bool PageLoaded {
            get => pageLoaded;
            set => SetProperty(ref pageLoaded, value);
        }

        public SplashScreenViewModel() {
            Logo = APIConstants.GetLogo(false);
            PageLoaded = false;

            TriggerNextPage();
        }

        public async Task TriggerNextPage() {
            PageLoaded = true;
        }
    }
}
