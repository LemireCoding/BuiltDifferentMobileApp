using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace BuiltDifferent.UITest
{
    [TestFixture(Platform.Android)]
    public class CoachClientAcceptDenyTests
    {

        IApp app;
        Platform platform;

        public CoachClientAcceptDenyTests(Platform platform)
        {
            this.platform = platform;
        }

        public void LoginCoach()
        {
            app = AppInitializer.StartApp(platform);

            app.WaitForElement(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), "coach");

            app.WaitForElement(e => e.Marked("Password"));
            app.EnterText(e => e.Marked("Password"), "coach");

            app.DismissKeyboard();

            app.WaitForElement(e => e.Marked("Login"));
            app.Tap(e => e.Marked("Login"));

        }

        public void LoginClient()
        {
            app = AppInitializer.StartApp(platform);

            app.WaitForElement(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), "janedoe12");

            app.WaitForElement(e => e.Marked("Password"));
            app.EnterText(e => e.Marked("Password"), "client");

            app.DismissKeyboard();

            app.WaitForElement(e => e.Marked("Login"));
            app.Tap(e => e.Marked("Login"));
        }

        //[Test]
        //public void OpenRepl()
        //{
        //    app.Repl();
        //}

        [Test, Order(1)]
        public void Canceled_Accept_Client_Request()
        {
            if (platform == Platform.Android)
            {
                LoginCoach();
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("ClientRequests"));
                app.Tap(e => e.Marked("RequestInfo"));
                app.Tap(e => e.Marked("AcceptClientButton"));
                app.WaitForElement(c => c.Marked("Cancel").Parent().Class("AlertDialogLayout"));
                app.Tap("Cancel");
                Assert.IsTrue(app.Query(x => x.Marked("ClientNameRequest").Text("Bob Jones")).Any());
            }
            else return;
        }

        [Test, Order(2)]
        public void Canceled_Deny_Client_Request()
        {
            if (platform == Platform.Android)
            {
                LoginCoach();
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("ClientRequests"));
                app.Tap(e => e.Marked("RequestInfo"));
                app.Tap(e => e.Marked("DenyClientButton"));
                app.WaitForElement(c => c.Marked("Cancel").Parent().Class("AlertDialogLayout"));
                app.Tap("Cancel");
                Assert.IsTrue(app.Query(x => x.Marked("ClientNameRequest").Text("Bob Jones")).Any());
            }
            else return;
        }

        [Test, Order(3)]
        public void Deny_Client_Request()
        {
            if (platform == Platform.Android)
            {
                LoginCoach();
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("ClientRequests"));
                app.Tap(e => e.Marked("RequestInfo"));
                app.Tap(e => e.Marked("DenyClientButton"));
                app.WaitForElement(c => c.Marked("Confirm").Parent().Class("AlertDialogLayout"));
                app.Tap("Confirm");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("We'll let the client know that you cannot accept their request at this time.")).Any());
                app.Tap("OK");
            }
            else return;
        }

        //cant go back 
        [Test, Order(4)]
        public void Accept_Client_Request_Ensure_Client_is_found()
        {
            if (platform == Platform.Android)
            {
                LoginClient();
                app.Tap(e => e.Marked("FindCoachByName"));
                app.EnterText(e => e.Marked("CoachNameField"), "Coach Bob");
                app.DismissKeyboard();
                app.Tap(e => e.Marked("SearchCoach"));
                app.Tap(e => e.Marked("CoachInfoName"));
                app.Tap(e => e.Marked("RequestCoach"));
                app.WaitForElement(c => c.Marked("OK").Parent().Class("AlertDialogLayout"));
                app.Tap("OK");

                //ISSUE cant go BACK
                app.Back();
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("Log out"));

                LoginCoach();
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("ClientRequests"));
                app.Tap(e => e.Marked("RequestInfo"));
                app.Tap(e => e.Marked("AcceptClientButton"));
                app.WaitForElement(c => c.Marked("Confirm").Parent().Class("AlertDialogLayout"));
                app.Tap("Confirm");
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.WaitForElement(c => c.Marked("CoachClients"));
                app.Tap(c => c.Marked("CoachClients"));
                Assert.IsTrue(app.Query(x => x.Marked("ClientName").Text("Jane Doe")).Any());
            }
            else return;
        }

        [Test, Order(4)]
        public void Accept_Client_Request()
        {
            if (platform == Platform.Android)
            {
                LoginCoach();
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("ClientRequests"));
                app.Tap(e => e.Marked("RequestInfo"));
                app.Tap(e => e.Marked("AcceptClientButton"));
                app.WaitForElement(c => c.Marked("Confirm").Parent().Class("AlertDialogLayout"));
                app.Tap("Confirm");
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("Get Coaching! You have a new client.")).Any());
            }
            else return;
        }


    }
}
