using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SkateFactory2.WebUI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var wsClient = new SkateFactory2.WebUI.WebService1.WebService1SoapClient();
            string message = wsClient.HelloWorld();
            lblMessage.Text = message;
        }
    }
}