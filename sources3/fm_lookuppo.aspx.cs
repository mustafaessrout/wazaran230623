using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookuppo : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbstatus, "po_sta_id");
            arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkLookupPO(arr);
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", txSearch.Text));
        arr.Add(new cArrayList("@po_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdsearch, "sp_tmst_po_get",arr);
        grddtl.DataSource = null;
        grddtl.DataBind();
    }
    protected void grdsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbpono = (Label)grdsearch.SelectedRow.FindControl("lbpono");
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        bll.vBindingGridToSp(ref grddtl, "sp_tpo_dtl_get", arr);
    }
    protected void btok_Click(object sender, EventArgs e)
    {
        Label lblpono = (Label)grdsearch.SelectedRow.FindControl("lbpono");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@po_no", lblpono.Text));
        bll.vInsertWrkLookupPO(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl3", "closewin()", true);
    }
}