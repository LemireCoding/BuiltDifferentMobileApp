using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace BuiltDifferent.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class ClientPaymentTests
    {
        IApp app;
        Platform platform;

        public ClientPaymentTests(Platform platform)
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
        [Test,Order(1)]
        public void Navigate_to_payment()
        {
            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Marked("Payment"));
                Assert.IsTrue(app.Query(x => x.Marked("Title").Text("Payment Info")).Any());
            }
            else return;
        }

        [Test, Order(2)]
        public void Navigate_to_payment_paypal()
        {
            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(x => x.Marked("Payment"));
                app.Tap(x => x.Marked("PaymentButton"));
                Thread.Sleep(2000);
                //according to stackOverflow, it is not possible to test a browser unless it is placed in a WebView. Not the case here.
                //the query below was an atempt at texting.
                //app.Query (w => w.WebView ().InvokeJS ("document.getElementById('css-163u2e0 ppvx_text--heading-md___5-6-6-beta-0').value"))
            }
            else return;
        }

    }
}
