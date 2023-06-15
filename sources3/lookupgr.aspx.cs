using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupgr : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSpWithEmptyChoosen(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
            //bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbstatus, "receipt_sta_id", "receipt_lookup");
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", txsearch.Text));
        arr.Add(new cArrayList("@period_cd", cbperiod.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_search", arr);
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbreceiptno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbreceiptno");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectData('"+lbreceiptno.Text+"');window.close();", true);
    }

    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}