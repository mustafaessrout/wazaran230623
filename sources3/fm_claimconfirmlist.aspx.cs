using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

public partial class fm_claimconfirmlist : System.Web.UI.Page
{

    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            //btnsearch_Click(sender, e);
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@search", txsearch.Text));
        bll.vBindingGridToSp(ref grdinvoice, "sp_tclaimconfirm_list", arr);
        bll.vBindingGridToSp(ref grdcashout, "sp_tcashoutrequest_list", arr);
    }
    protected void grdinvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow row in grdinvoice.Rows)
        {
            Label lbfileinv = (Label)row.FindControl("lbfileinv");
            Label lbfileinv_f = (Label)row.FindControl("lbfileinv_f");
            if (lbfileinv.Text == "None")
            {                
                lbfileinv.Attributes.Add("style", "display:none");
            }
            else
            {
                lbfileinv.Attributes.Remove("style");
            }

            if (lbfileinv_f.Text == "None")
            {
                lbfileinv_f.Attributes.Add("style", "display:none");
            }
            else
            {
                lbfileinv_f.Attributes.Remove("style");
            }
        }
    }

    protected void grdcashout_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}