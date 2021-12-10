using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BuiltDifferent.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
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


        [Test]
        public void Workout_page_visible()
        {
            app.Tap(c => c.Class("AppCompatImageButton"));
            app.Tap(x => x.Id("NoResourceEntry-11"));
            bool result = app.Query(e => e.Marked("WelcomeTitle")).Any();
            Assert.IsTrue(result);
        }

        [Test]
        public void Add_workout_page_visible()
        {
            app.Tap(c => c.Class("AppCompatImageButton"));
            app.Tap(x => x.Id("NoResourceEntry-11"));
            app.Tap("AddButton");
            app.WaitForElement("AddWorkoutTitle");
            bool result = app.Query(e => e.Marked("AddWorkoutTitle")).Any();

        }

        [Test]
        public void Add_workout_enter_all_fields_error_mssg()
        {
            app.Tap(c => c.Class("AppCompatImageButton"));
            app.Tap(x => x.Id("NoResourceEntry-11"));
            app.Tap("AddButton");
            app.ScrollDownTo(c => c.Marked("SaveButton"));
            app.Tap(x => x.Marked("SaveButton"));
            app.WaitForElement("message");
            Assert.IsTrue(app.Query(x => x.Id("message").Text("Please fill ALL of the fields")).Any());
        }

    }
}

