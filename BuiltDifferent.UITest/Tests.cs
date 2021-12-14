using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

/*
 * Test Ids can be found within each test method 
 * Please place TestId after first curly brace 
 * Format = T.#   
 * 
 * exemple:
 * 
 *  public methodName()
 *  {
 *      i.e. T.1
 *      {
 * 
 * 
 */

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
        public void Workout_page_visible()
        {
            //T.1

            if (platform == Platform.Android)
            {
            app.Tap(c => c.Class("AppCompatImageButton"));
            app.Tap(x => x.Id("NoResourceEntry-11"));
            bool result = app.Query(e => e.Marked("WelcomeTitle")).Any();
            Assert.IsTrue(result);

            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test]
        public void Add_workout_page_visible()
        {
            //T.2

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Id("NoResourceEntry-11"));
                app.Tap("AddButton");
                app.WaitForElement("AddWorkoutTitle");
                bool result = app.Query(e => e.Marked("AddWorkoutTitle")).Any();

            }
            //if IOS
            else
            {
                return;
            }

        }

        [Test]
        public void Add_workout_missing_fields_error_mssg()
        {
            //T.3

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Id("NoResourceEntry-11"));
                app.Tap("AddButton");
                app.ScrollDownTo(c => c.Marked("SaveButton"));
                app.Tap(x => x.Marked("SaveButton"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Please fill ALL of the fields")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }
        //[Test]
        //public void Add_workout_success()
        //{
        //    T.4

        //    if (platform == Platform.Android)
        //    {
        //        app.Tap(c => c.Class("AppCompatImageButton"));
        //        app.Tap(x => x.Marked("NoResourceEntry-11"));
        //        app.Tap("AddButton");
        //        app.EnterText("Test Workout Name");
        //        app.DismissKeyboard();
        //        app.Tap("Warm Up");
        //        app.ScrollDownTo(c => c.Marked("SaveButton"));
        //        app.Tap(x => x.Marked("SaveButton"));
        //        app.WaitForElement("message");
        //        Assert.IsTrue(app.Query(x => x.Id("message").Text("Vet Saved")).Any());
        //    }
        //    //if IOS
        //    else
        //    {
        //        return;
        //    }
        //}
    }
}