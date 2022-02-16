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
    public class MealRecipeTest
    {
        IApp app;
        Platform platform;

        public MealRecipeTest(Platform platform)
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
        }

        private void SaveScreenshot([CallerMemberName] string title = "", [CallerLineNumber] int lineNumber = -1)
        {
            FileInfo screenshot = app.Screenshot(title);
            if (TestEnvironment.IsTestCloud == false)
            {
                File.Move(screenshot.FullName, Path.Combine(screenshot.DirectoryName, $"{title}-{lineNumber}{screenshot.Extension}"));
            }
        }

        [Test, Order(1)]
        public void Test_View_Recipe()
        {
            if (platform == Platform.Android)
            {
                app.Tap(e => e.Text("Meals"));
                app.Tap(x => x.Text("We"));
                
                app.Tap(e => e.Text("Pancakes"));
                app.ScrollDownTo(x => x.Marked("viewRecipeButton"));
                app.Tap(e => e.Marked("viewRecipeButton"));
                app.WaitForElement(x => x.Marked("MealTitleRecipe"));
                Assert.IsTrue(app.Query(x => x.Marked("MealTitleRecipe").Text("Pancakes")).Any());
            }
            else
            {
                return;
            }
        }
    }
}