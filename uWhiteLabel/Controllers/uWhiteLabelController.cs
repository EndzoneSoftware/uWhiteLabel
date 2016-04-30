using System;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uWhiteLabel.Helpers;

namespace uWhiteLabel
{
    [PluginController("uWhiteLabel")]
    public class DashboardController : UmbracoAuthorizedApiController
    {

        [HttpGet]
        public object IsWelcomeScreenConfiged()
        {
            bool isConfiged = Configure.IsWelcomeScreenConfigured();

            var data = new { isConfiged = isConfiged };
            return data;
        }

        [HttpGet]
        public object iFrameData()
        {
            string url = Configure.GetIFrameUrl();

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
                Configure.RemoveIFrameUrl();
            }

            Configure.SaveIFrameUrl(url);
            
        }


        [HttpPost]
        public object SaveHtml([FromBody]string html)
        {
            Configure.SaveHtml(html);
            var data = new { Html = html };
            return data;
        }
        [HttpGet]
        public object GetHtml(bool useDefault)
        {
            var html = Configure.GetHtml(useDefault);
            var hasHtml = !String.IsNullOrWhiteSpace(html);
            var data = new { Html = html, HasHtml = hasHtml };
            return data;
        }
        [HttpGet]
        public object GetDefaultHtml()
        {
            var html = Configure.GetDefaultHtml();
            var data = new { Html = html };
            return data;
        }
    }
}