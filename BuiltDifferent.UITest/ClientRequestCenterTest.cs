using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.ViewModels.Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.UITest;

namespace BuiltDifferent.UITest
{
    [TestFixture(Platform.Android)]
    public class ClientRequestCenterTest
    {

        IApp app;
        Platform platform;

        public ClientRequestCenterTest(Platform platform)
        {
            this.platform = platform;
        }

        public void LoginClient()
        {
            app = AppInitializer.StartApp(platform);

            app.WaitForElement(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), "client3");

            app.WaitForElement(e => e.Marked("Password"));
            app.EnterText(e => e.Marked("Password"), "client3");

            app.DismissKeyboard();

            app.WaitForElement(e => e.Marked("Login"));
            app.Tap(e => e.Marked("Login"));
        }

        [Test]
        public void OpenRepl()
        {
            app.Repl();
        }

        [Test, Order(1)]
        public void Delete_Newly_Created_Pending_Request()
        {
            if (platform == Platform.Android)
            {
                LoginClient();
                app.Tap(e => e.Marked("FindCoachByName"));
                app.EnterText(e => e.Marked("CoachNameField"), "Coach Bob");
                app.DismissKeyboard();
                app.Tap(e => e.Marked("SearchCoach"));
                app.WaitForElement(e => e.Marked("SelectionTitle"));
                app.Tap(e => e.Marked("CoachInfoName"));
                app.Tap(e => e.Marked("RequestCoach"));
                app.WaitForElement(c => c.Marked("OK").Parent().Class("AlertDialogLayout"));
                app.Tap("OK");
                app.Tap(e => e.Marked("CoachNameLabel"));
                app.Tap(e => e.Marked("CancelButton"));
                app.WaitForElement("message");
                Assert.IsTrue(app.Query(x => x.Id("message").Text("The request to this coach was canceled")).Any());

            }
            else return;
        }

        [Test, Order(2)]
        public void Test_Requests_List()
        {
            ClientRequestsCenterViewModel model = new ClientRequestsCenterViewModel("tests");
            model.Requests = new ObservableRangeCollection<Request>();
            Assert.IsTrue(model.Requests.Count() == 0);

            Request request = new Request(1, "PENDING", 1, 3, DateTime.Now, DateTime.Now, "Coach Bob");
            model.Requests.Add(request);
            Assert.IsTrue(model.Requests.Count() == 1);
        }


    }
}
