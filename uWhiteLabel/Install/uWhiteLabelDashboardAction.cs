using System.Configuration;
using System.Web.Configuration;
using System.Xml;
using umbraco.interfaces;
using Umbraco.Core.IO;
using Umbraco.Core;
using uWhiteLabel.Properties;

namespace uWhiteLabel.Install
{
    public class uWhiteLabelDashboardAction : IPackageAction
    {
        private const string startupSectionXPth = "//section[@alias='StartupDashboardSection']";
        // can we assume that our tab is always caption="Welcome"?? (What if something else uses this?)
        private const string welcomeTabXPath = "//section[@alias='StartupDashboardSection']/tab[@caption='Welcome']";


        public string Alias()
        {
            return "uWhiteLabelDashboard";
        }

        public bool Execute(string packageName, XmlNode xmlData)
        {
            string xmlTab = Resources.DashboardWelcomeTab;

            string dbConfig = SystemFiles.DashboardConfig;
            XmlDocument dashboardFile = XmlHelper.OpenAsXmlDocument(dbConfig);

            XmlNode existingWelcomeTab = dashboardFile.SelectSingleNode(welcomeTabXPath);

            if (existingWelcomeTab == null)
            {
                XmlNode section = dashboardFile.SelectSingleNode(startupSectionXPth);

                XmlDocumentFragment xfrag = dashboardFile.CreateDocumentFragment();
                xfrag.InnerXml = xmlTab;

                section.PrependChild(xfrag);

                dashboardFile.Save(IOHelper.MapPath(dbConfig));
            }

            return true;

        }


        public bool Undo(string packageName, XmlNode xmlData)
        {
            
            string dbConfig = SystemFiles.DashboardConfig;
            XmlDocument dashboardFile = XmlHelper.OpenAsXmlDocument(dbConfig);

            XmlNode section = dashboardFile.SelectSingleNode(welcomeTabXPath);

            if (section != null)
            {

                dashboardFile.SelectSingleNode(startupSectionXPth).RemoveChild(section);
                dashboardFile.Save(IOHelper.MapPath(dbConfig));
            }

            return true;

            return true;
        }


        public XmlNode SampleXml()
        {
            var xml = "<Action runat=\"install\" undo=\"true\" alias=\"uWhiteLabelDashboard\" />";
            XmlDocument x = new XmlDocument();
            x.LoadXml(xml);
            return x;
        }
    }
}