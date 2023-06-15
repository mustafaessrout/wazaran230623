using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class alert_denied : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbalert.Text = bll.vLookUp("select dbo.fn_getsystemmsg("+Request.QueryString["m"]+")");
        }
    }
    protected void btdef_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btclosingdaily_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_closingdaily.aspx");
    }
    protected void btclosingmonthly_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_closingmonthly.aspx");
    }
    protected void btclosingyearly_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_closingyearly.aspx");
    }
}