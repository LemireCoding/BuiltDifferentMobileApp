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

        //will look at for IOS testing(will replicate what is seen on actual physical device

       /**
        * It seems impossible to get to the Meal page by clicking the tabbed page with the tests 
        * -> in our other test it fails also. Could not find a way to access the working id
        * 
        [Test]
        public void Set_Meal_Done()
        {
            //T.1

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Text("Meals"));
                app.Tap(e => e.Marked("MealName"));
                app.Tap(e => e.Id("NoResourceEntry-52"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Meal Eaten")).Any());

            }
            //if IOS
            else
            {
                return;
            }
        }

        **/





    }
}