using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eis_fm_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btlogin_Click(object sender, EventArgs e)
    {
        Response.Cookies["usr_nm"].Value = "2540:IRWAN AGUSYONO";
        Response.Cookies["usr_id"].Value = "2540";
        Response.Redirect("default.aspx");
    }
}