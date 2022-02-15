using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.ViewModels.Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.UITest;

namespace BuiltDifferent.UITest
{
    [TestFixture(Platform.Android)]
    public class MealProgressionBar
    {

        IApp app;
        Platform platform;

        public MealProgressionBar(Platform platform)
        {
            this.platform = platform;
        }

        public void LoginClient()
        {
            app = AppInitializer.StartApp(platform);

            app.WaitForElement(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), "client");

            app.WaitForElement(e => e.Marked("Password"));
            app.EnterText(e => e.Marked("Password"), "client");

            app.DismissKeyboard();

            app.WaitForElement(e => e.Marked("Login"));
            app.Tap(e => e.Marked("Login"));
        }


        [Test, Order(1)]
        public void Progress_Visible()
        {
            if (platform == Platform.Android)
            {
                LoginClient();
                app.Tap(x => x.Marked("Meals"));
                app.WaitForElement(x => x.Marked("DailyProgressText"));
                Assert.IsTrue(app.Query(x => x.Marked("DailyProgressText")).Length > 0);


            }
            else return;
        }


        [Test, Order(2)]
        public void Progress_Changes_On_Item_Done_Or_Undone()
        {
            if (platform == Platform.Android)
            {
                LoginClient();
                app.Tap(x => x.Marked("Meals"));
                app.WaitForElement(x => x.Marked("DailyProgressText"));
                string originalProgress = app.Query(x => x.Marked("DailyProgressText"))[0].Text;
                app.Tap(e => e.Marked("mealItemTitle"));
                app.ScrollDownTo(x => x.Marked("MarkEatenButton"));
                app.WaitForElement(x => x.Marked("MarkEatenButton"));
                app.Tap(e => e.Marked("MarkEatenButton"));
                app.Tap(c => c.Class("AppCompatImageButton"));

                app.Tap(e => e.Marked("Log out"));

                LoginClient();
                app.Tap(x => x.Marked("Meals"));
                app.WaitForElement(x => x.Marked("DailyProgressText"));
                string newProgress = app.Query(x => x.Marked("DailyProgressText"))[0].Text;
                Assert.AreNotEqual(originalProgress, newProgress);
            }
            else return;
        }




    }
}
