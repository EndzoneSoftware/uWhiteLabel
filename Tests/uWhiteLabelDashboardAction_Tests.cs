using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uWhiteLabel.Install;
using System.Xml;
using Umbraco.Core.IO;
using Umbraco.Core;

namespace Tests
{
    [TestClass]
    public class uWhiteLabelDashboardAction_Tests
    {
        [TestMethod]
        public void Execute_Undo_NoError()
        {
            var action = new uWhiteLabelDashboardAction();
            var result = action.Execute("uWhiteLabel", null);
            Assert.IsTrue(result);

            string dbConfig = SystemFiles.DashboardConfig;
            XmlDocument dashboardFile = XmlHelper.OpenAsXmlDocument(dbConfig);
            XmlNode section = dashboardFile.SelectSingleNode("//section[@alias='StartupDashboardSection']/tab[@caption='Welcome']");
            Assert.IsNotNull(section);

            result = action.Undo("uWhiteLabel", null);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Undo_NoError()
        {
            var action = new uWhiteLabelDashboardAction();
            var result = action.Undo("uWhiteLabel", null);
            Assert.IsTrue(result);
        }
    }
}
