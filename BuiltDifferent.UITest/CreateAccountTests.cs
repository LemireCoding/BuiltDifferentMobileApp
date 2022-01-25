using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using NUnit.Framework;
using BuiltDifferentMobileApp.ViewModels.Login;
using BuiltDifferentMobileApp.Ressource;

namespace BuiltDifferent.UITest {
    [TestFixture(Platform.Android)]
    public class CreateAccountTests {

        IApp app;
        Platform platform;

        private const string FULL_NAME = "Example Name";
        private const string VALID_EMAIL = "example@mail.com";
        private const string INVALID_EMAIL = "ex@.com";

        private const string VALID_PASSWORD = "vErYsTR0ngPas$1901";
        private const string INVALID_PASSWORD = "veryStrongPassword";
        private const string EXISTING_EMAIL = "alreadyexisting@mail.com";

        public CreateAccountTests(Platform platform) {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest() {
            app = AppInitializer.StartApp(platform);

            app.WaitForElement(e => e.Marked("Register"));
            app.Tap(e => e.Marked("Register"));
        }

        [Test]
        public void Register_Successfully_And_Login() {
            if(platform == Platform.Android) {
                //REGISTER
                app.WaitForElement(e => e.Marked("FullName"));
                app.EnterText(e => e.Marked("FullName"), FULL_NAME);

                app.WaitForElement(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), VALID_EMAIL);

                app.WaitForElement(e => e.Marked("Password"));
                app.EnterText(e => e.Marked("Password"), VALID_PASSWORD);

                app.WaitForElement(e => e.Marked("ConfirmPassword"));
                app.EnterText(e => e.Marked("ConfirmPassword"), VALID_PASSWORD);

                app.DismissKeyboard();

                app.WaitForElement(e => e.Marked("RegisterButton"));
                app.Tap(e => e.Marked("RegisterButton"));

                //LOGIN
                app.WaitForElement(e => e.Marked("Login"));

                app.WaitForElement(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), VALID_EMAIL);

                app.WaitForElement(e => e.Marked("Password"));
                app.EnterText(e => e.Marked("Password"), VALID_PASSWORD);

                app.DismissKeyboard();

                app.Tap(e => e.Marked("Login"));

                string errors = app.Query(label => label.Marked("ErrorText"))[0].Text;
                Assert.IsEmpty(errors);
            }
            else {
                return;
            }
        }

        // This test is SUPER long but is still faster than splitting up the code and running the tests separately
        // (It's basically 5 tests)
        [Test]
        public void Test_All_Error_Messages() {
            if(platform == Platform.Android) {
                string missingInputsError;
                string invalidEmailError;
                string invalidPasswordError;
                string notMatchingPasswordError;
                string emailAlreadyInUseError;

                // CREATE PRE-EXISTING ACC
                app.WaitForElement(e => e.Marked("FullName"));
                app.EnterText(e => e.Marked("FullName"), FULL_NAME);

                app.WaitForElement(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), EXISTING_EMAIL);

                app.WaitForElement(e => e.Marked("Password"));
                app.EnterText(e => e.Marked("Password"), VALID_PASSWORD);

                app.WaitForElement(e => e.Marked("ConfirmPassword"));
                app.EnterText(e => e.Marked("ConfirmPassword"), VALID_PASSWORD);

                app.DismissKeyboard();

                app.WaitForElement(e => e.Marked("RegisterButton"));
                app.Tap(e => e.Marked("RegisterButton"));

                app.WaitForElement(e => e.Marked("Register"));
                app.Tap(e => e.Marked("Register"));

                //TESTS
                app.WaitForElement(e => e.Marked("RegisterButton"));
                app.Tap(e => e.Marked("RegisterButton"));

                missingInputsError = app.Query(label => label.Marked("ErrorText"))[0].Text;

                app.WaitForElement(e => e.Marked("FullName"));
                app.EnterText(e => e.Marked("FullName"), FULL_NAME);

                app.WaitForElement(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), INVALID_EMAIL);

                app.WaitForElement(e => e.Marked("Password"));
                app.EnterText(e => e.Marked("Password"), VALID_PASSWORD);

                app.WaitForElement(e => e.Marked("ConfirmPassword"));
                app.EnterText(e => e.Marked("ConfirmPassword"), VALID_PASSWORD);

                app.DismissKeyboard();

                app.WaitForElement(e => e.Marked("RegisterButton"));
                app.Tap(e => e.Marked("RegisterButton"));

                invalidEmailError = app.Query(label => label.Marked("ErrorText"))[0].Text;

                app.WaitForElement(e => e.Marked("Email"));
                app.ClearText(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), VALID_EMAIL);

                app.WaitForElement(e => e.Marked("Password"));
                app.ClearText(e => e.Marked("Password"));
                app.EnterText(e => e.Marked("Password"), INVALID_PASSWORD);

                app.DismissKeyboard();

                app.WaitForElement(e => e.Marked("RegisterButton"));
                app.Tap(e => e.Marked("RegisterButton"));

                notMatchingPasswordError = app.Query(label => label.Marked("ErrorText"))[0].Text;

                app.WaitForElement(e => e.Marked("ConfirmPassword"));
                app.ClearText(e => e.Marked("ConfirmPassword"));
                app.EnterText(e => e.Marked("ConfirmPassword"), INVALID_PASSWORD);

                app.DismissKeyboard();

                app.WaitForElement(e => e.Marked("RegisterButton"));
                app.Tap(e => e.Marked("RegisterButton"));

                invalidPasswordError = app.Query(label => label.Marked("ErrorText"))[0].Text;

                app.WaitForElement(e => e.Marked("Email"));
                app.ClearText(e => e.Marked("Email"));
                app.EnterText(e => e.Marked("Email"), EXISTING_EMAIL);

                app.WaitForElement(e => e.Marked("Password"));
                app.ClearText(e => e.Marked("Password"));
                app.EnterText(e => e.Marked("Password"), VALID_PASSWORD);

                app.WaitForElement(e => e.Marked("ConfirmPassword"));
                app.ClearText(e => e.Marked("ConfirmPassword"));
                app.EnterText(e => e.Marked("ConfirmPassword"), VALID_PASSWORD);

                app.DismissKeyboard();

                app.WaitForElement(e => e.Marked("RegisterButton"));
                app.Tap(e => e.Marked("RegisterButton"));

                emailAlreadyInUseError = app.Query(label => label.Marked("ErrorText"))[0].Text;

                Assert.AreEqual(missingInputsError, AppResource.MissingInputs);
                Assert.AreEqual(invalidEmailError, AppResource.InvalidEmail);
                Assert.IsNotEmpty(invalidPasswordError);
                Assert.AreEqual(notMatchingPasswordError, AppResource.DifferentPasswords);
                Assert.AreEqual(emailAlreadyInUseError, AppResource.AccountAlreadyExists);
            } else {
                return;
            }
        }

    }
}
