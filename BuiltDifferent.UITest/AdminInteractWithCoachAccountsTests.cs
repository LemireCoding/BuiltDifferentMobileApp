using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace BuiltDifferent.UITest {
    [TestFixture(Platform.Android)]
    public class AdminInteractWithCoachAccountsTests {

        IApp app;
        Platform platform;

        public AdminInteractWithCoachAccountsTests(Platform platform) {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest() {
            app = AppInitializer.StartApp(platform);

            // Login
            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), "admin");

            app.EnterText(e => e.Marked("Password"), "admin");

            app.DismissKeyboard();

            app.WaitForElement(e => e.Marked("Login"));
            app.Tap(e => e.Marked("Login"));
        }

        private void GoToFlyoutPage(string pageFlyoutName) {
            app.Tap(c => c.Class("AppCompatImageButton"));
            app.Tap(x => x.Text(pageFlyoutName));
        }

        [Test]
        public void Approve_Pending_Coach() {
            if(platform == Platform.Android) {
                GoToFlyoutPage("Pending Coaches");
                app.Tap(e => e.Text("Coach Samuel"));

                app.WaitForElement(x => x.Marked("CoachName"));
                string profileCoachName = app.Query(x => x.Marked("CoachName"))[0].Text;

                app.Tap(x => x.Marked("ApproveCoachButton"));

                app.WaitForElement(c => c.Marked("Confirm").Parent().Class("AlertDialogLayout"));

                app.Tap("Confirm");

                GoToFlyoutPage("Verified Coaches");
                Assert.IsNotNull(app.Query(x => x.Text("Coach Samuel")).FirstOrDefault());
                Assert.AreEqual(profileCoachName, "Coach Samuel");
            }
            //if IOS
            else {
                return;
            }
        }

        [Test]
        public void View_Verified_Coach_Profile() {
            if(platform == Platform.Android) {
                GoToFlyoutPage("Verified Coaches");
                app.Tap(e => e.Text("Coach Bob"));

                app.WaitForElement(x => x.Marked("CoachName"));
                Assert.AreEqual(app.Query(x => x.Marked("CoachName"))[0].Text, "Coach Bob");
            }
            //if IOS
            else {
                return;
            }
        }
    }
}
