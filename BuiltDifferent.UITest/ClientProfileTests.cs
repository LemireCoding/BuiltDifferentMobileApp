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
    public class ClientProfileTests
    {
        IApp app;
        Platform platform;

        public ClientProfileTests(Platform platform)
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
        public void navigate_to_profile()
        {

            if (platform == Platform.Android)
            {
                app.WaitForElement(x => x.Marked("Title"));
                Assert.IsTrue(app.Query(x => x.Marked("Title").Text("Your Profile")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(2)]        
        public void submit_for_update()
        {

            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("EditButton"));
                app.Tap(x => x.Marked("CurrentWeight"));
                app.ClearText(x => x.Marked("CurrentWeight"));
                app.EnterText("150");
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

        //[Test,Order(3)]
        //public void submit_for_update_empty_field()
        //{

        //    if (platform == Platform.Android)
        //    {
        //        app.Back();
        //        app.WaitForElement(e => e.Marked("Profile"));
        //        app.Tap(x => x.Marked("Profile"));
        //        app.WaitForElement(x => x.Marked("EditButton"));
        //        app.Tap(x => x.Marked("EditButton"));
        //        app.Tap(x => x.Marked("Name"));
        //        app.ClearText(x => x.Marked("Name_Container"));
        //        app.DismissKeyboard();
        //        app.ScrollDownTo(x => x.Marked("Submit"));
        //        app.Tap(x => x.Marked("Submit"));
        //        app.WaitForElement("message");
        //        Assert.IsTrue(app.Query(x => x.Id("message").Text("Please fill ALL of the fields.")).Any());
        //        app.Tap("OK");
        //    }
        //    //if IOS
        //    else
        //    {
        //        return;
        //    }
        //}

        [Test, Order(4)]
        public void submit_for_update_weight_change()
        {

            if (platform == Platform.Android)
            {
                app.WaitForElement(x => x.Marked("EditButton"));
                app.Tap(x => x.Marked("EditButton"));
                app.Tap(x => x.Marked("CurrentWeight"));
                app.ClearText(x => x.Marked("CurrentWeight"));
                app.EnterText("150");
                app.DismissKeyboard();
                app.ScrollDownTo(x => x.Marked("Submit"));
                app.Tap(x => x.Marked("Submit"));
                app.WaitForElement(x => x.Marked("message"));
                app.Tap("OK");
                app.WaitForElement(x => x.Marked("CurrentWeight"));
                Assert.IsTrue(app.Query(x => x.Marked("CurrentWeight").Text("150")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test,Order(5)]
        public void edit_mode_off_on_Appearing()
        {
             

            if (platform == Platform.Android)
            {
                app.Back();
                app.WaitForElement(e => e.Marked("Profile"));
                app.Tap(x => x.Marked("Profile"));
                Assert.IsFalse(app.Query(x => x.Marked("CurrentWeight")).FirstOrDefault().Enabled);
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(6)]
        public void enter_edit_mode()
        {
             

            if (platform == Platform.Android)
            {
                app.Back();
                app.WaitForElement(e => e.Marked("Profile"));
                app.Tap(x => x.Marked("Profile"));
                app.Tap(x => x.Marked("EditButton"));
                Assert.IsTrue(app.Query(x => x.Marked("CurrentWeight")).FirstOrDefault().Enabled);
            }
            //if IOS
            else
            {
                return;
            }
        }
        // MUST MANUALLY SELECT Picture FOR THIS TEST
        [Test, Order(7)]
        public void Upload_Picture()
        {
            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("EditButton"));
                app.Tap(x => x.Marked("ProfilePicturePicker"));

                app.ScrollDownTo(x => x.Marked("Submit"));
                app.WaitForElement(x => x.Marked("Submit"));
                app.Tap(x => x.Marked("Submit"));
                var ProfilePicturePreview = app.Query(x => x.Marked("ProfilePicturePreview"));
                Assert.IsNotEmpty(ProfilePicturePreview);
            }
            else
            {
                return;
            }
        }
    }
}
