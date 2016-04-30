using Umbraco.Core;
using Umbraco.Web.Trees;
using uWhiteLabel.Helpers;

namespace uWhiteLabel.Events
{
    public class uWhiteLabelTreeEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);

            TreeControllerBase.RootNodeRendering += TreeControllerBase_RootNodeRendering;
        }

        void TreeControllerBase_RootNodeRendering(TreeControllerBase sender, TreeNodeRenderingEventArgs e)
        {
            if (sender.TreeAlias == "uwhitelabel-config")
            {
                if (!Configure.IsWelcomeScreenConfigured())
                    e.Node.CssClasses.Add("icon-warning");
            }
        }
    }
}
