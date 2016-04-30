using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using System.Configuration;
using System;
using uWhiteLabel.Helpers;
using System.IO;

namespace uWhiteLabel
{
    [PluginController("uWhiteLabel")]
    public class DashboardController : UmbracoAuthorizedApiController
    {
        const string iframeUrlAppKeyName = "uWhiteLabel.iFrame.Url";
        const string pathToSavedHtml = "~/App_Plugins/uWhiteLabel/backoffice/welcome.saved.htm";
        const string pathToDefaultHtml = "~/App_Plugins/uWhiteLabel/backoffice/welcome.default.htm";

        [HttpGet]
        public object iFrameData()
        {
            Uri url;

            if (Uri.TryCreate(ConfigurationManager.AppSettings[iframeUrlAppKeyName], UriKind.Absolute, out url))
            {
                var data = new { Url = url.AbsoluteUri, HasIframe = true };
                return data;
            }
            return new { HasIframe = false };
        }

        [HttpGet]
        public string SaveiFrameData(string url)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                AppSettingsHelper.RemoveAppSettingsKey(iframeUrlAppKeyName);
                return url;
            }

            Uri uri;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                AppSettingsHelper.CreateAppSettingsKey(iframeUrlAppKeyName, url);
                return url;
            }
            throw new Exception("Cannot save invalid uWhiteLabel iFrame URL");
            
        }


        [HttpPost]
        public object SaveHtml([FromBody]string html)
        {
            var savedHtmlFile = System.Web.HttpContext.Current.Server.MapPath(pathToSavedHtml);
            File.WriteAllText(savedHtmlFile, html);

            var data = new { Html = html };
            return data;
        }
        [HttpGet]
        public object GetHtml(bool useDefault)
        {
            var savedHtmlFile = System.Web.HttpContext.Current.Server.MapPath(pathToSavedHtml);
            var defaultHtmlFile = System.Web.HttpContext.Current.Server.MapPath(pathToDefaultHtml);
            var html = (useDefault) ? File.ReadAllText(defaultHtmlFile) : "";
            if (File.Exists(savedHtmlFile))
            {
                html = File.ReadAllText(savedHtmlFile);
            }
            var hasHtml = !String.IsNullOrWhiteSpace(html);
            var data = new { Html = html, HasHtml = hasHtml };
            return data;
        }
    }
}