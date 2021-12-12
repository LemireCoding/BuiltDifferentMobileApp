using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BuiltDifferent.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .InstalledApp("com.companyname.builtdifferentmobileapp")
                    .EnableLocalScreenshots()
                    .Debug()
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}