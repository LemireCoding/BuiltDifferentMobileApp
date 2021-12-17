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

        /**[Test]
        public void OpenRepl()
        {
            app.Repl();
        }
        **/
        //will look at for IOS testing(will replicate what is seen on actual physical device
        //to find out tree and ids 
       
        
        [Test]
        public void Workout_page_visible()
        {
            //T.1
          
              if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Id("NoResourceEntry-11"));
                app.SwipeRightToLeft();
                bool result = app.Query(e => e.Marked("WorkoutTitle")).Any();
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
                app.SwipeRightToLeft();
                app.Tap("AddButton");
                
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
               
                app.SwipeRightToLeft();
                app.Tap("AddButton");
                app.ScrollDownTo(c => c.Text("Create"));
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
        /**
        [Test]
        public void Add_workout_success()
        {
        //    T.4

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Id("NoResourceEntry-11"));

                app.SwipeRightToLeft();
                app.Tap("AddButton");
                app.Tap(x => x.Marked("WorkoutName"));
                app.EnterText("Test Workout Name");
                app.DismissKeyboard();
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
                app.ScrollDownTo(c => c.Text("Create"));
                app.Tap(x => x.Marked("SaveButton"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Workout Saved")).Any());
            }
           //if IOS
            else
            {
                return;
            }
        }
        **/
        
    }

}