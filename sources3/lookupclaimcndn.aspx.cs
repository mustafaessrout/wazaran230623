using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupclaimcndn : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tclaim_reqcndn_get");
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblcndncode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblcndncode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clo", "window.opener.SelectClaim('"+lblcndncode.Text+"');window.close();", true);
    }
}