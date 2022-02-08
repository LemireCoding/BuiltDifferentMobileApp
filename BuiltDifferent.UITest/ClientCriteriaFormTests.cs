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
    public class ClientCriteriaTests
    {
        IApp app;
        Platform platform;

        public ClientCriteriaTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            // Login
            app.WaitForElement(e => e.Marked("Email"), "client3");
            app.EnterText(e => e.Marked("Email"), "client3");

            app.WaitForElement(e => e.Marked("Password"), "client3");
            app.EnterText(e => e.Marked("Password"), "client3");

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

        [Test]
        public void OpenRepl()
       {
           app.Repl();
       }

        [Test]
        public void Reach_Selection_Page()
        {
            //T.1

            if (platform == Platform.Android)
            {
                app.Tap(e => e.Marked("FindCoachByName"));
                app.EnterText(e => e.Marked("CoachNameField"), "Coach Bob");
                app.DismissKeyboard();
                app.Tap(e => e.Marked("SearchCoach"));
                app.WaitForElement(e => e.Marked("SelectionTitle"));
                Assert.IsTrue(app.Query(x => x.Marked("SelectionTitle")).Any());

            }
            //if IOS
            else
            {
                return;
            }
        }


       

       
    }
}