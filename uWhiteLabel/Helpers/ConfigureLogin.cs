using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using uWhiteLabel.Models;

namespace uWhiteLabel.Helpers
{
    public static class ConfigureLogin
    {

        const string pathToLoginView = "/Umbraco/Views/common/dialogs/login.html";

        //saving should perhaps be moved to db table?
        const string pathToLoginJson = "~/App_Plugins/uWhiteLabel/backoffice/login.save.json";
        

        public static void SaveLoginDetails(LoginDetails loginDetails)
        {
            var saveFile = System.Web.HttpContext.Current.Server.MapPath(pathToLoginJson);
            string json = JsonConvert.SerializeObject(loginDetails);
            FileInfo file = new FileInfo(saveFile);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, json);
        }

        public static LoginDetails GetLoginDetails()
        {
            var saveFile = HttpContext.Current.Server.MapPath(pathToLoginJson);
            if (File.Exists(saveFile))
            {
                string json = File.ReadAllText(saveFile);
                return JsonConvert.DeserializeObject<LoginDetails>(json);
            }
            else
            {
                return new LoginDetails();
            }
        }

        /// <summary>
        /// This injects static html into the Umbraco Login view.
        ///  Hopefully this is less error prone than messing with the Umbraco Angular controllers or larger view changes
        /// </summary>
        /// <param name="loginDetails"></param>
        public static void InjectHtmlIntoLoginView(LoginDetails loginDetails)
        {
            if (loginDetails == null)
                throw new ArgumentNullException("loginDetails");

            var loginViewFile = HttpContext.Current.Server.MapPath(pathToLoginView);
            //todo: check file exists?
            string loginViewHTML = File.ReadAllText(loginViewFile);
            loginViewHTML = GetInjectedHtml(loginDetails, loginViewHTML);

            File.WriteAllText(loginViewFile, loginViewHTML);

            //todo: can we clear client dependancy (to force view update) cache here?

        }

        private static string GetInjectedHtml(LoginDetails loginDetails, string loginViewHTML)
        {
            string originalHTML = "<h1>{{greeting}}</h1>";
            string injectHtml = originalHTML; //default is to reset back to original
            var regExInjectHtml = new Regex("<!--uWhiteLabel-->(.*)<!--/uWhiteLabel-->"); //we have used html comments to delimit the injected HTML

            if (!String.IsNullOrWhiteSpace(loginDetails.LogoUrl) || !String.IsNullOrWhiteSpace(loginDetails.Greeting))
            {
                string logoHtml = (String.IsNullOrWhiteSpace(loginDetails.LogoUrl)) ? "" : String.Format("<img src=\"{0}\" />", loginDetails.LogoUrl);
                string greetingsHtml = (String.IsNullOrWhiteSpace(loginDetails.Greeting)) ? originalHTML : String.Format("<h1>{0}</h1>", loginDetails.Greeting);
                injectHtml = string.Format("<!--uWhiteLabel-->{0}{1}<!--/uWhiteLabel-->", logoHtml, greetingsHtml);
            }

            if (loginViewHTML.Contains(originalHTML))
            {
                loginViewHTML = loginViewHTML.Replace(originalHTML, injectHtml);
            }
            else
            {
                loginViewHTML = regExInjectHtml.Replace(loginViewHTML, injectHtml);
            }

            return loginViewHTML;
        }
    }
}
