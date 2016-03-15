using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using umbraco;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using System.Configuration;
using System;

namespace uWhiteLabel
{
    [PluginController("uWhiteLabel")]
    public class DashboardController : UmbracoAuthorizedApiController
    {

        [HttpGet]
        public object iFrameData()
        {
            Uri url;

            if (Uri.TryCreate(ConfigurationManager.AppSettings["uWhiteLabel.iFrame.Url"], UriKind.Absolute, out url))
            {
                var data = new { Url = url.AbsoluteUri };
                return data;
            }
            throw new Exception("Invalid (or missing) 'uWhiteLabel.iFrame.Url' AppSetting in web.config");
        }
    }
}