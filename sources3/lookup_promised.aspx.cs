using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_promised : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tpayment_promised_get");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbpromisedcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbpromisedcode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clo", "window.opener.PromisedSelected('"+lbpromisedcode.Text+"');window.close();", true);
    }
}