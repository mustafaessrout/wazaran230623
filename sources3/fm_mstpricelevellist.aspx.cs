using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstpricelevellist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grdpricelevel, "sp_tmst_pricelevel_get");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_itempriceentry.aspx");
    }
    protected void grdpricelevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbpricelevelcode = (Label)grdpricelevel.SelectedRow.FindControl("lbpricelevelcode");
        Response.Redirect("fm_itempriceentry.aspx?pl=" + lbpricelevelcode.Text);
    }
    protected void grdpricelevel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdpricelevel.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grdpricelevel, "sp_tmst_pricelevel_get");
    }
}