using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RestaurantBilling.Droid.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        //[Test]
        //public void AppLaunches()
        //{
        //    app.Screenshot("First screen.");
        //    app.Repl();
        //}

        [Test]
        public void AppCreatesBillSuccessfully()
        {
            //**********click on creat bill to create a new bill************
            //app.WaitForElement(c=>c.Marked("Create Bill")); //working
            //app.Tap(c => c.Marked("Create Bill"));

            app.Screenshot("Main screen.");

            app.WaitForElement(c => c.Id("createBillButton"));           
            app.Tap(c => c.Id("createBillButton"));

            //app.Repl(); //this shows the element tree when put " tree " command on command prompt

            app.WaitForElement(c => c.Id("editTextCustomerEmail"));
            app.Tap(c => c.Id("editTextCustomerEmail"));
            app.EnterText("MyEmail");


            app.Tap(c => c.Id("editTextSubTotal"));
            app.EnterText("200");


            //app.Query(c => c.Id("seekBarGratuity").Invoke("progress", 25));
            if (app.Query(e => e.Id("seekBarGratuity")).Length > 0)
            {                
                app.Tap(e => e.Id("seekBarGratuity"));
                app.Query(c => c.Id("seekBarGratuity").Invoke("progress", 25));
            }

            app.Screenshot("create bill screen.");

            Assert.Greater(app.Query(e => e.Text("100")).Length, 0, "Tip is not 100");
            Assert.Greater(app.Query(e => e.Text("300")).Length, 0, "Total is not 300");

            app.Tap(c => c.Id("saveButton"));

            //**********verify that mainform is appeared************
            app.WaitForElement(c => c.Id("createBillButton"));

            //********** Fo to view bills to check last bill is added or not********
            app.Tap(c => c.Id("viewBillsButton"));

            app.Repl();
            app.WaitForElement(c => c.Id("textViewCustomerEmail"));
            Assert.Greater(app.Query(e => e.Text("MyEmail")).Length, 0, "Email is not appeared in list view, expected 'MyEmail'");
            Assert.Greater(app.Query(e => e.Text("300")).Length, 0, "Total is not appeared in listview which is 300");
            Assert.Greater(app.Query(e => e.Text("100")).Length, 0, "Tip is not appeared in listview which is  100");
            Assert.Greater(app.Query(e => e.Text("200")).Length, 0, "SubTotal is not appeared in listview which is 200");
        }
    }
}

