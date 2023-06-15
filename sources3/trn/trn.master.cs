using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class trn_master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if ((Request.Cookies["exh_dt"] == null) || (Request.Cookies["exh_cd"]==null))
            //{
            //    Response.Redirect("fm_login.aspx");
            //}
        }

    }
    protected void btLogout_Click(object sender, EventArgs e)
    {
        Response.Cookies.Clear();
        Response.Redirect("../fm_loginho2.aspx");
    }
}
