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
    public class WorkoutTests
    {
        IApp app;
        Platform platform;

        public WorkoutTests(Platform platform)
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

            app.Tap(x => x.Text("Board"));
            app.Tap("AddButton");
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
        public void Add_workout_page_visible()
        {

            if (platform == Platform.Android)
            {
                app.WaitForElement(e => e.Marked("AddWorkoutTitle"));
                Assert.IsTrue(app.Query(e => e.Marked("AddWorkoutTitle").Text("Add Workout")).Any());
            }
            //if IOS
            else
            {
                return;
            }

        }

        [Test, Order(2)]
        public void Add_workout_missing_fields_error_mssg()
        {

            if (platform == Platform.Android)
            {
                app.ScrollDownTo(c => c.Marked("SaveButton"));
                app.Tap(x => x.Marked("SaveButton"));
                app.WaitForElement(x => x.Id("message"));
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Please fill ALL of the fields.")).Any());
                app.Tap("OK");
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(3)]
        public void Add_workout_success()
        {

            if (platform == Platform.Android)
            {
                app.ScrollUpTo(x => x.Marked("WorkoutName"));
                app.Tap(x => x.Marked("WorkoutName"));
                app.EnterText("Test Workout Name");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("WorkoutTypePicker"));
                app.Tap(x => x.Text("Cardio"));
                app.Tap(x => x.Marked("Sets"));
                app.EnterText("1");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("Reps"));
                app.EnterText("1");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("Duration"));
                app.EnterText("1");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("RestTime"));
                app.EnterText("1");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("Description"));
                app.EnterText("Test Description");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("VideoLink"));
                app.EnterText("youtube.com");
                app.DismissKeyboard();
                app.ScrollDownTo(c => c.Marked("SaveButton"));
                app.Tap(x => x.Marked("SaveButton"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Workout Saved.")).Any());
                app.Tap(x => x.Text("OK"));
            }
            //if IOS
            else
            {
                return;
            }
        }
    }
}