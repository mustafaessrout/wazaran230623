using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookupcontract : System.Web.UI.Page
{

    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String strProposal = Request.QueryString["prop"];

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@contract_no", null));
            txsearchprop.Text = strProposal;
            arr.Add(new cArrayList("@word", txsearchprop.Text));
            bll.vBindingGridToSp(ref grdcontract, "sp_lookup_contract_cashout", arr);

            txsearchprop.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", null));
        arr.Add(new cArrayList("@word", txsearchprop.Text));
        bll.vBindingGridToSp(ref grdcontract, "sp_lookup_contract_cashout", arr);
    }
    protected void grdcontract_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcontract.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", null));
        arr.Add(new cArrayList("@word", txsearchprop.Text));
        bll.vBindingGridToSp(ref grdcontract, "sp_lookup_contract_cashout", arr);
    }

}