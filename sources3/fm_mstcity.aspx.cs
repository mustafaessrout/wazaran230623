using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstcity : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tmst_city_get");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstcityentry.aspx");
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbcitycode = (Label)grd.SelectedRow.FindControl("lbcitycode");
        Response.Redirect("fm_mstcityentry.aspx?city=" + lbcitycode.Text);
    }
}