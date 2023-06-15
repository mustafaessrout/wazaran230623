using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_po : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
            string sho = Request.Cookies["sp"].Value.ToString();
            if (sho == "0")
            {
                cbsalespoint.Enabled = true;
                cbsalespoint.CssClass = "";
                cbsalespoint.Items.RemoveAt(0);
            }
            else
            {
                cbsalespoint.Enabled = false;
                cbsalespoint.CssClass = "makeitreadonly ro form-control";
            }
            bll.vBindingFieldValueToCombo(ref cbstatus, "po_sta_id");
            cbstatus.SelectedValue = "N";
            cbstatus_SelectedIndexChanged(sender, e);
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", txsearch.Text));
        arr.Add(new cArrayList("@po_sta_id", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_po_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix()", true);
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbpo_no = (Label)grd.SelectedRow.FindControl("lbpo_no");

        Session["loopo_no"] = lbpo_no.Text;
        Session["salespoint"] = cbsalespoint.SelectedValue.ToString();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl3", "closewin()", true);
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        btsearch_Click(sender, e);
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        btsearch_Click(sender, e);
    }
}