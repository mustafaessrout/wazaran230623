using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_promotor2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btLogout_Click(object sender, EventArgs e)
    {
        Response.Cookies.Clear();
        Response.Redirect("../fm_loginho2.aspx");
    }
}
