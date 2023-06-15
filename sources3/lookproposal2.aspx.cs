using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookproposal2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tmst_proposal_get2");
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbproposalcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbpropno");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clsk", "window.opener.SelectProposal('"+lbproposalcode.Text+"');window.close();", true);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_proposal_get2");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@PROP_NO", txsearchprop.Text));
        bll.vBindingGridToSp(ref grd, "sp_tproposal_get", arr);
    }
}