using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uWhiteLabel.Helpers;
using uWhiteLabel.Models;

namespace Tests.Helpers
{
    [TestClass]
    public class ConfigureLogin_Tests
    {

        const string initialHtml = "test\n<h1>{{greeting}}</h1>\nmore test";
        const string afterFirstRunHtml = "test\n<!--uWhiteLabel-->{1}<h1>{0}</h1><!--/uWhiteLabel-->\nmore test";
        const string imgHtml = "<img src=\"{0}\" />";

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void InjectHtmlIntoLoginView_null()
        {
            ConfigureLogin.InjectHtmlIntoLoginView(null);
        }


        [TestMethod]
        public void GetInjectedHtml_nogreeting_firstrun()
        {
            LoginDetails d = new LoginDetails { LogoUrl = "url", Greeting = "" };

            string expected = String.Format(afterFirstRunHtml, "{{greeting}}", String.Format(imgHtml, d.LogoUrl));
            string html = RunInjectHTMLMethod(d, initialHtml);

            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void GetInjectedHtml_nogreeting_secondrun()
        {
            LoginDetails d = new LoginDetails { LogoUrl = "url", Greeting = "" };

            string html = RunInjectHTMLMethod(d, initialHtml);

            d.Greeting = "changed greeting";

            string expected = String.Format(afterFirstRunHtml, "{{greeting}}", String.Format(imgHtml, d.LogoUrl));

            html = RunInjectHTMLMethod(d, html);

            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void GetInjectedHtml_nologo_firstrun()
        {
            LoginDetails d = new LoginDetails { LogoUrl = "", Greeting = "test greeting" };

            string expected = String.Format(afterFirstRunHtml, d.Greeting, "");
            string html = RunInjectHTMLMethod(d, initialHtml);

            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void GetInjectedHtml_nologo_secondrun()
        {
            LoginDetails d = new LoginDetails { LogoUrl = "", Greeting = "test greeting" };
            
            string html = RunInjectHTMLMethod(d, initialHtml);

            d.Greeting = "changed greeting";
            
            string expected = String.Format(afterFirstRunHtml, d.Greeting, "");

            html = RunInjectHTMLMethod(d, html);

            Assert.AreEqual(expected, html);
        }


        [TestMethod]
        public void GetInjectedHtml_logo_firstrun()
        {
            LoginDetails d = new LoginDetails { LogoUrl = "url", Greeting = "test greeting" };

            string expected = String.Format(afterFirstRunHtml, d.Greeting, String.Format(imgHtml, d.LogoUrl));
            string html = RunInjectHTMLMethod(d, initialHtml);

            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void GetInjectedHtml_logo_secondrun()
        {
            LoginDetails d = new LoginDetails { LogoUrl = "url", Greeting = "test greeting" };

            string current = String.Format(afterFirstRunHtml, d.Greeting, String.Format(imgHtml, d.LogoUrl));

            d.Greeting = "changed greeting";
            d.LogoUrl = "new url";

            string expected = String.Format(afterFirstRunHtml, d.Greeting, String.Format(imgHtml, d.LogoUrl));

            var html = RunInjectHTMLMethod(d, current);

            Assert.AreEqual(expected, html);
        }



        private static string RunInjectHTMLMethod(LoginDetails d, string original)
        {
            var config = new PrivateType(typeof(ConfigureLogin));
            var html = config.InvokeStatic("GetInjectedHtml", d, original) as string;
            return html;
        }
    }
}
