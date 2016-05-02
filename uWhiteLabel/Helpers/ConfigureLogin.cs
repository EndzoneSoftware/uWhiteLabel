using System;
using System.Configuration;
using System.IO;
using uWhiteLabel.Models;
using Newtonsoft.Json;
using System.Web;

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
            File.WriteAllText(saveFile, json);
        }

        public static LoginDetails GetLoginDetails()
        {
            var saveFile = HttpContext.Current.Server.MapPath(pathToLoginJson);
            string json = File.ReadAllText(saveFile);
            return JsonConvert.DeserializeObject<LoginDetails>(json);
        }

        /// <summary>
        /// This injects static html into the Umbraco Login view.
        ///  Hopefully this is less error prone than messing with the Umbraco controllers or larger view changes
        /// </summary>
        /// <param name="loginDetails"></param>
        public static void InjectHtmlIntoLoginView(LoginDetails loginDetails)
        {
            var loginViewFile = HttpContext.Current.Server.MapPath(pathToLoginView);
            //todo: check file exists?
            string loginViewHTML = File.ReadAllText(loginViewFile);

            string originalHTML = "<h1>{{greeting}}</h1>";
            //todo check for empty and only inject if not (for both)
            string injectHtml = string.Format("<img src=\"{0}\"><h1>{1}</h1>", loginDetails.LogoUrl, loginDetails.Greeting);
            if (loginViewHTML.Contains(originalHTML))
            {
                string newHTML = loginViewHTML.Replace(originalHTML, injectHtml);
                File.WriteAllText(loginViewFile, newHTML);
            }
            else
            {
                throw new NotImplementedException();
            }

            //todo: can we clear client dependancy (to force view update) cache here?

        }
    }
}
