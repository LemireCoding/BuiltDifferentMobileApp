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
    public class MarkAsDoneWorkout
    {
        IApp app;
        Platform platform;

        public MarkAsDoneWorkout(Platform platform)
        {
            this.platform = platform;
        }

        [OneTimeSetUp]
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


        [Test, Order(1)]
        public void Test_Complete_Workout()
        {
            if (platform == Platform.Android)
            {
                app.Tap(e => e.Text("Tu"));
                app.Tap(e => e.Text("jogging"));
                app.ScrollDownTo(e => e.Marked("MarkDoneButton"));
                app.Tap(e => e.Marked("MarkDoneButton"));
                app.Tap(e => e.Text("jogging"));
                int markAsDoneButtons = app.Query(e => e.Button("Mark as Done")).Length;
                Assert.AreEqual(0, markAsDoneButtons);
            }
            else
            {
                return;
            }
        }
    }
}
