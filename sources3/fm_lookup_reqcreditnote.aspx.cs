using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_reqcreditnote : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbcn, "creditdebit");
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@arcn_no", txsearch.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@arcn_type", cbcn.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tblARCN_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix()", true);
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbarcn_no = (Label)grd.SelectedRow.FindControl("lbarcn_no");

        Session["looarcn_no"] = lbarcn_no.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}