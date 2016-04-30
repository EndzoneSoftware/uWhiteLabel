using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using System.Configuration;
using System;
using uWhiteLabel.Helpers;

namespace uWhiteLabel
{
    [PluginController("uWhiteLabel")]
    public class DashboardController : UmbracoAuthorizedApiController
    {
        const string iframeUrlAppKeyName = "uWhiteLabel.iFrame.Url";

        [HttpGet]
        public object iFrameData()
        {
            Uri url;

            if (Uri.TryCreate(ConfigurationManager.AppSettings[iframeUrlAppKeyName], UriKind.Absolute, out url))
            {
                var data = new { Url = url.AbsoluteUri };
                return data;
            }
            throw new Exception(String.Format("Invalid (or missing) '{0}' AppSetting in web.config", iframeUrlAppKeyName));
        }

        [HttpGet]
        public string SaveiFrameData(string url)
        {
            Uri uri;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                AppSettingsHelper.CreateAppSettingsKey(iframeUrlAppKeyName, url);
                return url;
            }
            throw new Exception("Cannot save invalid uWhiteLabel iFrame URL");
            
        }
    }
}