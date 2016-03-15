using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uWhiteLabel.Install
{
    public class InstallControl : System.Web.UI.UserControl
    {
        protected string Test;
        protected void Page_Load(object sender, EventArgs e)
        {
            Test = "hello world";
        }
    }
}