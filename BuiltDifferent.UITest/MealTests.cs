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
            app.Tap(x => x.Marked("Meals"));
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
        public void Add_meal_missing_fields_error_mssg()
        {
             

            if (platform == Platform.Android)
            {
                app.ScrollDownTo(c => c.Marked("SaveButton"));
                app.Tap(x => x.Marked("SaveButton"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Please fill ALL of the fields.")).Any());
                app.Tap("OK");
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(2)]
        public void AddMeal_page_visible()
        {
             

            if (platform == Platform.Android)
            {
                app.ScrollUpTo(x => x.Marked("AddMealPageTitle"));
                Assert.IsTrue(app.Query(e => e.Marked("AddMealPageTitle").Text("Add Meal")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(3)]
        public void Add_meal_with_valid_fields_confirmation_mssg ()
        {

            if (platform == Platform.Android)
            {
                app.Tap(x => x.Marked("MealNameField"));
                app.EnterText("Test Meal Name");
                app.DismissKeyboard();
                app.Tap(x => x.Marked("MealTypeField"));
                app.Tap(x => x.Text("Breakfast"));
                app.ScrollDownTo(c => c.Marked("SaveButton"));
                app.Tap(x => x.Marked("SaveButton"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Meal Saved.")).Any());
                app.Tap("OK");
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(4)]
        public void Add_meal_with_valid_fields_display_new_meal_on_dashboard()
        {

            if (platform == Platform.Android)
            {
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
                app.WaitForElement(x => x.Marked("MealPageTitle"));
                app.ScrollDownTo(x => x.Text("Test Meal Name"));
                Assert.IsTrue(app.Query(x => x.Text("Test Meal Name")).Any());
            }
            //if IOS
            else
            {
                return;
            }
        }
    }
}