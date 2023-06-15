using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_goodreceiptHO : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceiptho_get", arr);
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbreceipt_no = (Label)grd.SelectedRow.FindControl("lbreceipt_no");

        Session["looreceipt_no"] = lbreceipt_no.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}