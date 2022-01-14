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
    public class ProfileTests
    {
        IApp app;
        Platform platform;

        public ProfileTests(Platform platform)
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
        //to find out tree and ids

        [Test]
        public void OpenRepl()
        {
            app.Repl();
        }

        [Test]
        public void navigate_to_profile()
        {
            //T.1.3

            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("Profile"));
                app.WaitForElement(x => x.Marked("Title"));
                Assert.IsTrue(app.Query(x => x.Marked("Title").Text("Your Info")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test]
        public void submit_for_update()
        {
            //T.1.3

            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("Profile"));
                app.Tap(x => x.Marked("CurrentWeight"));
                app.EnterText("150");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Profile Saved")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test]
        public void submit_for_update_empty_field()
        {
            //T.1.3

            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("Profile"));
                app.Tap(x => x.Marked("Name"));
                app.ClearText(x => x.Marked("Name_Container"));
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Please fill ALL of the fields")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }
    }
}
