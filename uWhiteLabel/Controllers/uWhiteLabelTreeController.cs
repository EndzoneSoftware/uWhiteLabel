﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;

using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace uWhiteLabel
{
    [Tree("developer", "config", "uWhiteLabel", sortOrder:10)]
    [PluginController("uWhiteLabel")]
    public class uWhiteLabelTreeController : TreeController
    {

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            // check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                var tree = new TreeNodeCollection();
                var node = CreateTreeNode("1", id, queryStrings, "Welcome Screen", "icon-smiley-inverted");
                //see http://issues.umbraco.org/issue/U4-6617
                node.RoutePath = "/developer/config/welcome/edit";
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
