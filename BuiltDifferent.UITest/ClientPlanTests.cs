using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace BuiltDifferent.UITest {
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class ClientPlanTests {
        IApp app;
        Platform platform;

        public ClientPlanTests(Platform platform) {
            this.platform = platform;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            app = AppInitializer.StartApp(platform);

            app.WaitForElement(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), "client");

            app.WaitForElement(e => e.Marked("Password"));
            app.EnterText(e => e.Marked("Password"), "client");

            app.DismissKeyboard();

            app.Tap(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("PreviousWeekButton"));
        }

        [Test, Order(1)]
        public void Test_Complete_Workout() {
            if(platform == Platform.Android) {
                app.Tap(e => e.Text("jogging"));
                app.Tap(e => e.Button("Mark as Done"));
                app.Tap(e => e.Text("jogging"));
                int markAsDoneButtons = app.Query(e => e.Button("Mark as Done")).Length;
                Assert.AreEqual(0, markAsDoneButtons);
            } else {
                return;
            }
        }

        [Test, Order(2)]
        public void Test_Uncomplete_Workout() {
            if(platform == Platform.Android) {
                app.Tap(e => e.Button("Unmark"));
                int markAsDoneButtons = app.Query(e => e.Text("Unmark")).Length;
                Assert.AreEqual(0, markAsDoneButtons);
            } else {
                return;
            }
        }

        [Test, Order(3)]
        public void Test_Week_Selector_Navigation() {
            if(platform == Platform.Android) {
                app.Tap(e => e.Marked("PreviousWeekButton"));
                app.Tap(e => e.Marked("PreviousWeekButton"));
                app.Tap(e => e.Marked("PreviousWeekButton"));
                app.Tap(e => e.Marked("NextWeekButton"));
                app.Tap(e => e.Marked("NextWeekButton"));
                app.Tap(e => e.Marked("NextWeekButton"));
            } else {
                return;
            }
        }

        [Test, Order(4)]
        public void Test_Week_Selector_Loading_Workouts() {
            if(platform == Platform.Android) {
                int joggingWorkoutsPrevious = app.Query(e => e.Text("jogging")).Length;
                app.Tap(e => e.Marked("PreviousWeekButton"));
                app.Tap(e => e.Marked("PreviousWeekButton"));
                app.Tap(e => e.Marked("Day0Button"));

                int joggingWorkoutsAfter = app.Query(e => e.Text("jogging")).Length;

                Assert.AreNotEqual(joggingWorkoutsPrevious, joggingWorkoutsAfter);
            } else {
                return;
            }
        }

        [Test, Order(5)]
        public void Test_Uneaten_Meal() {
            if(platform == Platform.Android) {
                app.Tap(e => e.Text("Meals"));
                app.ScrollDownTo(c => c.Text("Sandwich"));
                app.Tap(e => e.Text("Pancakes"));
                app.Tap(e => e.Text("Unmark"));
                int unmarkButtons = app.Query(e => e.Text("Unmark")).Length;

                Assert.AreEqual(0, unmarkButtons);
            } else {
                return;
            }
        }

        [Test, Order(6)]
        public void Test_Eaten_Meal() {
            if(platform == Platform.Android) {
                app.Tap(e => e.Text("Eaten"));
                int eatenButtons = app.Query(e => e.Text("Eaten")).Length;

                Assert.AreEqual(0, eatenButtons);
            } else {
                return;
            }
        }

    }
}
