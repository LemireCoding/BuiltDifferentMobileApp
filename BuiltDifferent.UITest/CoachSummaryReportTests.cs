﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace BuiltDifferent.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class CoachSummaryReportTests
    {
        IApp app;
        Platform platform;

        public CoachSummaryReportTests(Platform platform)
        {
            this.platform = platform;
        }
        [Test]
        public void OpenRepl()
        {
            app.Repl();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            app = AppInitializer.StartApp(platform);

            app.WaitForElement(e => e.Marked("Login"));

            app.WaitForElement(e => e.Marked("Email"));
            app.EnterText(e => e.Marked("Email"), "coach");

            app.WaitForElement(e => e.Marked("Password"));
            app.EnterText(e => e.Marked("Password"), "coach");

            app.DismissKeyboard();

            app.Tap(e => e.Marked("Login"));
        }
        [Test, Order(1)]
        public void Navigate_To_ReportsPage()
        {
            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("CoachReports"));
                app.WaitForElement("Title");
                Assert.IsTrue(app.Query(x => x.Marked("Title").Text("Weekly Report")).Any());
            }
            else return;
        }

        [Test, Order(2)]
        public void Verify_Client_Number()
        {
            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("CoachReports"));
                app.WaitForElement("NumberOfClients");
                Assert.IsTrue(app.Query(x => x.Marked("NumberOfClients").Text("2")).Any());
            }
            else return;
        }

        [Test, Order(3)]
        public void Verify_Clients()
        {
            if (platform == Platform.Android)
            {
                app.Tap(c => c.Class("AppCompatImageButton"));
                app.Tap(e => e.Marked("CoachReports"));
                app.WaitForElement("ClientName");
                Assert.IsTrue(app.Query(x => x.Marked("ClientName").Text("Bob Jones")).Any());
            }
            else return;
        }
    }
}
