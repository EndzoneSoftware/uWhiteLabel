using System;
using System.Configuration;
using System.IO;

namespace uWhiteLabel.Helpers
{
    public static class Configure
    {
        const string iframeUrlAppKeyName = "uWhiteLabel.iFrame.Url";
        const string pathToSavedHtml = "~/App_Plugins/uWhiteLabel/backoffice/welcome.saved.htm";
        const string pathToDefaultHtml = "~/App_Plugins/uWhiteLabel/backoffice/welcome.default.htm";

        public static bool IsWelcomeScreenConfigured()
        {
            var iFrameConfiged = !String.IsNullOrEmpty(GetIFrameUrl());
            var htmlConfiged = !String.IsNullOrEmpty(GetHtml(false));
            return iFrameConfiged || htmlConfiged;
        }

        public static string GetIFrameUrl()
        {

            Uri url;

            if (Uri.TryCreate(ConfigurationManager.AppSettings[iframeUrlAppKeyName], UriKind.Absolute, out url))
            {
                return url.AbsoluteUri;
            }

            return "";
        }

        public static void SaveIFrameUrl(string url)
        {
            Uri uri;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                AppSettingsHelper.CreateAppSettingsKey(iframeUrlAppKeyName, url);
            }

        }
        public static void RemoveIFrameUrl()
        {
            AppSettingsHelper.RemoveAppSettingsKey(iframeUrlAppKeyName);
        }

        public static void SaveHtml(string html)
        {
            var savedHtmlFile = System.Web.HttpContext.Current.Server.MapPath(pathToSavedHtml);
            File.WriteAllText(savedHtmlFile, html);
        }

        public static string GetHtml(bool useDefault)
        {
            var savedHtmlFile = System.Web.HttpContext.Current.Server.MapPath(pathToSavedHtml);
            var html = (useDefault) ? GetDefaultHtml() : "";
            if (File.Exists(savedHtmlFile))
            {
                html = File.ReadAllText(savedHtmlFile);
            }

            return html;
        }

        public static string GetDefaultHtml()
        {
            var defaultHtmlFile = System.Web.HttpContext.Current.Server.MapPath(pathToDefaultHtml);
            return File.ReadAllText(defaultHtmlFile);
        }
    }
}
