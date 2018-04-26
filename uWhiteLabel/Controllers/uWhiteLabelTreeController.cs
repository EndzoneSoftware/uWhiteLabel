using System;
using System.Net.Http.Formatting;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace uWhiteLabel
{
    [Tree("developer", "uwhitelabel-config", "uWhiteLabel", sortOrder:10)]
    [PluginController("uWhiteLabel")]
    public class uWhiteLabelTreeController : TreeController
    {

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            // check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                var tree = new TreeNodeCollection();

                var loginNode = CreateTreeNode("2223", id, queryStrings, "Login Screen", "icon-logout");
                loginNode.AdditionalData.Add("umbVersion", UmbracoVersion.Current);
                loginNode.RoutePath = "/developer/uwhitelabel-config/login/edit";
                tree.Add(loginNode);

                var node = CreateTreeNode("2222", id, queryStrings, "Welcome Screen", "icon-smiley-inverted");
                //see http://issues.umbraco.org/issue/U4-6617
                node.RoutePath = "/developer/uwhitelabel-config/welcomescreen/edit";
                node.AdditionalData.Add("umbVersion", UmbracoVersion.Current);
                tree.Add(node);

                return tree;
            }
            // this tree doesn't support rendering more than 1 level
          throw new NotSupportedException();
        }


        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            return menu;
        }
    }
}
