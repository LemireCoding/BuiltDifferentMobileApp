using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace BuiltDifferent.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class MarkAsDoneMeal
    {
        IApp app;
        Platform platform;

        public MarkAsDoneMeal(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            // Login
            app.WaitForElement(e => e.Marked("Email"), "client");
            app.EnterText(e => e.Marked("Email"), "client");

            app.WaitForElement(e => e.Marked("Password"), "client");
            app.EnterText(e => e.Marked("Password"), "client");

            app.DismissKeyboard();

            app.WaitForElement(e => e.Marked("Login"));
            app.Tap(e => e.Marked("Login"));
        }

        private void SaveScreenshot([CallerMemberName] string title = "", [CallerLineNumber] int lineNumber = -1)
        {
            FileInfo screenshot = app.Screenshot(title);
            if (TestEnvironment.IsTestCloud == false)
            {
                File.Move(screenshot.FullName, Path.Combine(screenshot.DirectoryName, $"{title}-{lineNumber}{screenshot.Extension}"));
            }
        }

      

        [Test]
        public void Set_Meal_Done()
        {
            //T.1

            if (platform == Platform.Android)
            {
              
                app.Tap(x => x.Marked("Meals"));
                app.Tap(e => e.Marked("MealName"));
                app.Tap(e => e.Id("NoResourceEntry-54"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Meal Eaten")).Any());

            }
            //if IOS
            else
            {
                return;
            }
        }

    





    }
}