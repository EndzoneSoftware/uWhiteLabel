using System;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uWhiteLabel.Helpers;
using uWhiteLabel.Models;

namespace uWhiteLabel
{
    [PluginController("uWhiteLabel")]
    public class DashboardController : UmbracoAuthorizedApiController
    {

        [HttpGet]
        public object IsWelcomeScreenConfiged()
        {
            bool isConfiged = ConfigureWelcome.IsWelcomeScreenConfigured();

            var data = new { isConfiged = isConfiged };
            return data;
        }

        [HttpGet]
        public object iFrameData()
        {
            string url = ConfigureWelcome.GetIFrameUrl();

            if (!String.IsNullOrEmpty(url))
            {
                var data = new { Url = url, HasIframe = true };
                return data;
            }
            return new { HasIframe = false };
        }

        [HttpGet]
        public void SaveiFrameData(string url)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                ConfigureWelcome.RemoveIFrameUrl();
            }

            ConfigureWelcome.SaveIFrameUrl(url);
            
        }


        [HttpPost]
        public object SaveHtml([FromBody]string html)
        {
            ConfigureWelcome.SaveHtml(html);
            var data = new { Html = html };
            return data;
        }


        [HttpPost]
        public object SaveLoginDetails(LoginDetails loginDetails)
        {
            ConfigureLogin.SaveLoginDetails(loginDetails);
            ConfigureLogin.InjectHtmlIntoLoginView(loginDetails);

            var data = new { logoUrl = loginDetails.LogoUrl, greetings = loginDetails.Greeting };
            return data;
        }


        [HttpGet]
        public object GetLoginDetails()
        {
            return ConfigureLogin.GetLoginDetails();
        }


        [HttpGet]
        public object GetHtml(bool useDefault)
        {
            var html = ConfigureWelcome.GetHtml(useDefault);
            var hasHtml = !String.IsNullOrWhiteSpace(html);
            var data = new { Html = html, HasHtml = hasHtml };
            return data;
        }
        [HttpGet]
        public object GetDefaultHtml()
        {
            var html = ConfigureWelcome.GetDefaultHtml();
            var data = new { Html = html };
            return data;
        }
    }
}