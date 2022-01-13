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
    class ForgotPasswordTests
    {
        IApp app;
        Platform platform;

        public ForgotPasswordTests(Platform platform)
        {
            this.platform = platform;
        }

        //[Test]
        //public void OpenRepl()
        //{
        //    app.Repl();
        //}

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
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
        public void navigate_to_forgot_Password_Page()
        {
            //T.1.1

            if (platform == Platform.Android)
            {
                app.WaitForElement(e => e.Marked("ForgotPassword"));
                app.Tap(e => e.Marked("ForgotPassword"));
                bool result = app.Query(e => e.Marked("ForgotPasswordTitle")).Any();
                Assert.IsTrue(result);
                
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test]
        public void enter_valid_email()
        {
            //T.1.1

            if (platform == Platform.Android)
            {
                app.WaitForElement(e => e.Marked("ForgotPassword"));
                app.Tap(e => e.Marked("ForgotPassword"));
                app.WaitForElement(e => e.Marked("Email"));
                app.Tap(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), "chloe.dunwoody@hotmail.com");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement("SuccessMessage");
                Assert.IsTrue(app.Query(x => x.Marked("SuccessMessage").Text("Thank You! If you have signed up with us previously, we will be sending you an email shortly with a link to reset your password.")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test]
        public void enter_invalid_email()
        {
            //T.1.1

            if (platform == Platform.Android)
            {
                app.WaitForElement(e => e.Marked("ForgotPassword"));
                app.Tap(e => e.Marked("ForgotPassword"));
                app.Tap(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), "abc");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement("ErrorMessage");
                Assert.IsTrue(app.Query(x => x.Marked("ErrorMessage").Text("Please enter a valid email.")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }
        [Test]
        public void leave_field_empty()
        {
            //T.1.1

            if (platform == Platform.Android)
            {
                app.WaitForElement(e => e.Marked("ForgotPassword"));
                app.Tap(e => e.Marked("ForgotPassword"));
                app.Tap(x => x.Marked("Submit"));
                Assert.IsTrue(app.Query(x => x.Marked("ErrorMessage").Text("Please fill all required fields.")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test]
        public void email_not_found()
        {
            //T.1.1

            if (platform == Platform.Android)
            {
                app.WaitForElement(e => e.Marked("ForgotPassword"));
                app.Tap(e => e.Marked("ForgotPassword"));
                app.Tap(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), "abc@hotmail.com");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement("ErrorMessage");
                Assert.IsTrue(app.Query(x => x.Marked("ErrorMessage").Text("There was an unknown issue communicating with the server. Please try again later.")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }
    }
}
