using BuiltDifferentMobileApp.ViewModels.Coach;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace BuiltDifferent.UITest {
    [TestFixture(Platform.Android)]
    public class CoachApplyForApprovalTests {

        IApp app;
        Platform platform;

        public const string PENDING_COACH_EMAIL = "coach5";
        public const string PENDING_COACH_PASSWORD = "coach5";

        public CoachApplyForApprovalTests(Platform platform) {
            this.platform = platform;
        }

        private string CheckErrorText() {
            app.WaitForElement(e => e.Marked("ErrorText"));
            return app.Query(label => label.Marked("ErrorText"))[0].Text;
        }

        [OneTimeSetUp]
        public void OneTimeSetup() {
            app = AppInitializer.StartApp(platform);

            app.WaitForElement(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), PENDING_COACH_EMAIL);

            app.WaitForElement(e => e.Marked("Password"));
            app.EnterText(e => e.Marked("Password"), PENDING_COACH_PASSWORD);

            app.DismissKeyboard();

            app.Tap(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("Submit"));
        }

        [Test, Order(1)]
        public void Test_Missing_Inputs_Error() {
            if(platform == Platform.Android) {
                app.Tap(e => e.Marked("Submit"));

                string error = CheckErrorText();

                Assert.AreEqual(error, NewCoachPageViewModel.MissingInputs);
            } else {
                return;
            }
        }

        [Test, Order(2)]
        public void Test_Gender_Unspecified_Error() {
            if(platform == Platform.Android) {
                app.Tap(x => x.Marked("GenderPicker"));

                app.Tap(x => x.Text("Other"));

                app.EnterText(x => x.Marked("Description"), "COACH DESCRIPTION");

                app.EnterText(x => x.Marked("CoachPricing"), "69.00");

                app.DismissKeyboard();

                app.Tap(x => x.Marked("Submit"));

                string error = CheckErrorText();
                Assert.AreEqual(error, NewCoachPageViewModel.OtherGenderUnspecified);

            } else {
                return;
            }
        }

        [Test, Order(3)]
        public void Test_Missing_Certification_Error() {
            if(platform == Platform.Android) {
                app.EnterText(x => x.Marked("CustomGender"), "Coach");

                app.Tap(x => x.Marked("Submit"));

                string error = CheckErrorText();
                Assert.AreEqual(error, NewCoachPageViewModel.MissingCertification);
            } else {
                return;
            }
        }

        // MUST MANUALLY SELECT CERTIFICATION FOR THIS TEST
        [Test, Order(4)]
        public void Test_Successful_Application() {
            if(platform == Platform.Android) {
                app.Tap(x => x.Marked("FilePicker"));

                app.WaitForElement(x => x.Marked("Submit"));
                app.Tap(x => x.Marked("Submit"));

                app.WaitForElement(e => e.Marked("CheckSubmissionStatus"));
                string buttonText = app.Query(label => label.Marked("CheckSubmissionStatus"))[0].Text;

                Assert.IsNotEmpty(buttonText);
            } else {
                return;
            }
        }

    }
}
