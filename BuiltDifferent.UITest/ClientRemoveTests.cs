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
    public class ClientRemoveTests
    {
        IApp app;
        Platform platform;

        public ClientRemoveTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            // Login
            app.WaitForElement(e => e.Marked("Email"), "coach");
            app.EnterText(e => e.Marked("Email"), "coach");

            app.WaitForElement(e => e.Marked("Password"), "coach");
            app.EnterText(e => e.Marked("Password"), "coach");

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

        [Test, Order(1)]
        public void Canceled_Client_Delete()
        {
            if (platform == Platform.Android)
            {
                app.SwipeRightToLeft(e => e.Marked("Client"));
                app.WaitForElement(c => c.Marked("Cancel").Parent().Class("AlertDialogLayout"));
                app.Tap("Cancel");
                Assert.IsTrue(app.Query(x => x.Marked("ClientName").Text("Bob Jones")).Any());
            }
            else return;
        }

        [Test, Order(2)]
        public void Canceled_Client_Remove()
        {
            if (platform == Platform.Android)
            {
                if (platform == Platform.Android)
                {

                    app.SwipeRightToLeft(e => e.Marked("Client"));
                    app.WaitForElement(c => c.Marked("Cancel").Parent().Class("AlertDialogLayout"));
                    app.Tap("Confirm");
                    app.WaitForElement(c => c.Marked("OK").Parent().Class("AlertDialogLayout"));
                    app.Tap("OK");
                    Assert.IsFalse(app.Query(x => x.Marked("ClientName").Text("Bob Jones")).Any());
                }
                else return;
            }
        }
        //will look at for IOS testing(will replicate what is seen on actual physical device
        //to find out tree and ids

        //[Test]
        //public void OpenRepl()
        //{
        //    app.Repl();
        //}
    }
}
