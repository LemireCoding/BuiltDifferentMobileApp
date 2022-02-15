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
    public class CoachProfileTests
    {
        IApp app;
        Platform platform;

        public CoachProfileTests(Platform platform)
        {
            this.platform = platform;
        }

        [OneTimeSetUp]
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

            app.WaitForElement(e => e.Marked("Profile"));
            app.Tap(x => x.Marked("Profile"));
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

        //[Test]
        //public void OpenRepl()
        //{
        //    app.Repl();
        //}

        [Test, Order(1)]
        public void edit_mode_off_on_Appearing()
        {
             

            if (platform == Platform.Android)
            {
                app.WaitForElement("NameInput");
                Assert.IsFalse(app.Query(x => x.Marked("NameInput")).FirstOrDefault().Enabled);
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(2)]
        public void submit_for_update_name_change()
        {
             

            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("EditButton"));
                app.Tap(x => x.Marked("NameInput"));
                app.ClearText(x => x.Marked("NameInput"));
                app.EnterText("Blahblah");
                app.DismissKeyboard();
                app.ScrollDownTo(x => x.Marked("Submit"));
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement("message");
                app.Tap(x => x.Text("OK"));
                app.Back();
                app.Tap(x => x.Marked("Profile"));
                Assert.IsTrue(app.Query(x => x.Marked("NameInput").Text("Blahblah")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }


        [Test, Order(3)]
        public void enter_edit_mode()
        {
            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("EditButton"));
                Assert.IsTrue(app.Query(x => x.Marked("NameInput")).FirstOrDefault().Enabled);
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(4)]
        public void submit_for_update()
        {
             

            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("NameInput"));
                app.ClearText(x => x.Marked("NameInput"));
                app.EnterText("Blah");
                app.DismissKeyboard();
                app.ScrollDownTo(x => x.Marked("Submit"));
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Profile Saved")).Any());
                app.Tap("OK");
            }
            //if IOS
            else
            {
                return;
            }
        }

        // MUST MANUALLY SELECT Picture FOR THIS TEST
        [Test, Order(5)]
        public void Upload_Picture()
        {
            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("EditButton"));
                app.ScrollUpTo(x => x.Marked("ProfilePicturePicker"));
                app.Tap(x => x.Marked("ProfilePicturePicker"));

                app.ScrollDownTo(x => x.Marked("Submit"));
                app.WaitForElement(x => x.Marked("Submit"));
                app.Tap(x => x.Marked("Submit"));
                app.ScrollUpTo(x => x.Marked("ProfilePicturePreview"));
                var ProfilePicturePreview = app.Query(x => x.Marked("ProfilePicturePreview"));
                Assert.IsNotEmpty(ProfilePicturePreview);
            }
            else
            {
                return;
            }
        }

        [Test, Order(6)]
        public void submit_for_update_empty_field()
        {


            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("EditButton"));
                app.ScrollUpTo(x => x.Marked("NameInput"));
                app.Tap(x => x.Marked("NameInput"));
                app.ClearText(x => x.Marked("NameInput"));
                app.DismissKeyboard();
                app.ScrollDownTo(x => x.Marked("Submit"));
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Please fill ALL of the fields")).Any());
                app.Tap("OK");
            }
            //if IOS
            else
            {
                return;
            }
        }
    }
}
