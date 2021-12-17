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
    public class MealTests
    {
        IApp app;
        Platform platform;

        public MealTests(Platform platform)
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

        //[Test]
        //public void OpenRepl()
        //{
        //    app.Repl();
        //}


        [Test]
        public void Meal_page_visible()
        {
            //T.1

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Text("Meals"));
                bool result = app.Query(e => e.Marked("MealPageTitle")).Any();
                Assert.IsTrue(result);

            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test]
        public void AddMeal_page_visible()
        {
            //T.1.2

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Text("Meals"));
                app.Tap("AddButton");
                bool result = app.Query(e => e.Marked("AddMealPageTitle")).Any();
                Assert.IsTrue(result);
            }
            //if IOS
            else
            {
                return;
            }
        }


        [Test]
        public void Add_meal_missing_fields_error_mssg()
        {
            //T.1.3

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Text("Meals"));
                app.Tap(x => x.Marked("AddButton"));
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
        [Test]
        public void Add_meal_with_valid_fields_confirmation_mssg ()
        {
            //T.1.4

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Text("Meals"));
                app.Tap("AddButton");
                app.Tap(x => x.Marked("MealNameField"));
                app.EnterText("Test Meal Name");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("MealTypeField"));
                app.Tap(x => x.Text("Breakfast"));
                app.ScrollDownTo(c => c.Marked("SaveButton"));
                app.Tap(x => x.Marked("SaveButton"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Meal Saved")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test]
        public void Add_meal_with_valid_fields_display_new_meal_on_dashboard()
        {
            //T.1.5

            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Text("Meals"));
                app.Tap("AddButton");
                app.Tap(x => x.Marked("MealNameField"));
                app.EnterText("Test Meal Name");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("MealTypeField"));
                app.Tap(x => x.Text("Breakfast"));
                app.ScrollDownTo(c => c.Marked("SaveButton"));
                app.Tap(x => x.Marked("SaveButton"));
                app.WaitForElement("message");
                app.Tap(x => x.Text("OK"));
                app.WaitForElement(x => x.Marked("MealName"));
                Assert.AreSame("Test Meal Name", app.Query(x => x.Marked("MealName").Text("Test Meal Name")));

            }
            //if IOS
            else
            {
                return;
            }
        }
    }
}