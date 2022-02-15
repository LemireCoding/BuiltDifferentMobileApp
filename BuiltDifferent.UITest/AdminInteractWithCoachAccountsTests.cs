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
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            // Login
            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), "admin");

            app.EnterText(e => e.Marked("Password"), "admin");

            app.DismissKeyboard();

            app.WaitForElement(e => e.Marked("Login"));
            app.Tap(e => e.Marked("Login"));
        }

        private void GoToFlyoutPage(string pageFlyoutName)
        {
            app.Tap(c => c.Class("AppCompatImageButton"));
            app.Tap(x => x.Text(pageFlyoutName));
        }

        [Test, Order(1)]
        public void Coach_Submit_certification_for_approve()
        {
            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(c => c.Text("Log out"));
                app.WaitForElement(e => e.Marked("Email"), "chloe.dunwoody@hotmail.com");
                app.ClearText(x => x.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), "chloe.dunwoody@hotmail.com");

                app.ClearText(x => x.Marked("Password"));
                app.WaitForElement(e => e.Marked("Password"), "coach2");
                app.EnterText(e => e.Marked("Password"), "coach2");

                app.DismissKeyboard();

                app.WaitForElement(e => e.Marked("Login"));
                app.Tap(e => e.Marked("Login"));

                app.Tap(x => x.Marked("GenderPicker"));

                app.Tap(x => x.Text("Male"));

                app.EnterText(x => x.Marked("Description"), "COACH DESCRIPTION");

                app.EnterText(x => x.Marked("CoachPricing"), "10.00");

                app.EnterText(x => x.Marked("PayPalLink"), "http://paypal.me.com");

                app.Tap(x => x.Marked("OfferMeals"));

                //MUST SELECT MANUALLY!!!

                app.Tap(x => x.Marked("FilePicker"));

                app.WaitForElement(x => x.Marked("Submit"));
                app.Tap(x => x.Marked("Submit"));

                app.WaitForElement(e => e.Marked("CheckSubmissionStatus"));

                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("Log out"));
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(2)]
        public void Approve_Pending_Coach()
        {
            //ISSUE NO PENDING
            if (platform == Platform.Android)
            {
                GoToFlyoutPage("Pending Coaches");
                app.Tap(e => e.Text("Coach Chloe"));

                app.WaitForElement(x => x.Marked("CoachName"));
                string profileCoachName = app.Query(x => x.Marked("CoachName"))[0].Text;

                app.Tap(x => x.Marked("ApproveCoachButton"));

                app.WaitForElement(c => c.Marked("Confirm").Parent().Class("AlertDialogLayout"));

                app.Tap("Confirm");

                GoToFlyoutPage("Verified Coaches");
                Assert.IsNotNull(app.Query(x => x.Text("Coach Chloe")).FirstOrDefault());
                Assert.AreEqual(profileCoachName, "Coach Chloe");
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(3)]
        public void View_Verified_Coach_Profile()
        {
            if (platform == Platform.Android)
            {
                GoToFlyoutPage("Verified Coaches");
                app.Tap(e => e.Text("Coach Bob"));

                app.WaitForElement(x => x.Marked("CoachName"));
                Assert.AreEqual(app.Query(x => x.Marked("CoachName"))[0].Text, "Coach Bob");
            }
            //if IOS
            else
            {
                return;
            }
        }

        [Test, Order(4)]
        public void Set_Coach_Account_Status()
        {
            if (platform == Platform.Android)
            {
                GoToFlyoutPage("Verified Coaches");
                app.Tap(e => e.Text("Coach Goofy"));

                app.WaitForElement(x => x.Marked("SetCoachAccountStatusButton"));

                string initialButtonText = app.Query(x => x.Marked("SetCoachAccountStatusButton"))[0].Text;

                app.Tap(x => x.Marked("SetCoachAccountStatusButton"));

                string postButtonText = app.Query(x => x.Marked("SetCoachAccountStatusButton"))[0].Text;

                Assert.AreNotEqual(initialButtonText, postButtonText);
            }
            //if IOS
            else
            {
                return;
            }
        }
    }
}
