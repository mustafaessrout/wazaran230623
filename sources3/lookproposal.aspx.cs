using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookproposal : System.Web.UI.Page
{
    cbll bll = new cbll();  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbstatus.Items.Insert(0, "-- All Status --");
            cbstatus.Items.Insert(1, "Not Yet Approve");
            cbstatus.Items.Insert(2, "Approved");
            cbstatus.Items.Insert(3, "Canceled");            
            bll.vBindingComboToSp(ref cbcode, "sp_tmst_prodcode_get", "prod_code", "prod_code");
            cbcode.Items.Insert(0, "All Code");
            bll.vBindingComboToSp(ref cbYear, "sp_tmst_year_getbyclaim", "fld_valu", "fld_valu");
            List<cArrayList> arr = new List<cArrayList>();
            if (cbstatus.SelectedIndex == 0)
            {
                arr.Add(new cArrayList("@status", null));
            }
            else
            {
                arr.Add(new cArrayList("@status", (cbstatus.SelectedIndex-1).ToString()));
            }            
            arr.Add(new cArrayList("@code", cbcode.SelectedValue.ToString()));
            arr.Add(new cArrayList("@word", txsearchprop.Text));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_lookup_proposal", arr);
            cbstatus_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbstatus.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@status", null));
        }
        else
        {
            arr.Add(new cArrayList("@status", (cbstatus.SelectedIndex - 1).ToString()));
        }
        arr.Add(new cArrayList("@code", cbcode.SelectedValue.ToString()));
        arr.Add(new cArrayList("@word", txsearchprop.Text));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_lookup_proposal", arr);
    }

    protected void grdprop_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        if (cbstatus.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@status", null));
        }
        else
        {
            arr.Add(new cArrayList("@status", (cbstatus.SelectedIndex - 1).ToString()));
        }
        arr.Add(new cArrayList("@code", cbcode.SelectedValue.ToString()));
        arr.Add(new cArrayList("@word", txsearchprop.Text));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_lookup_proposal", arr);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbstatus.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@status", null));
        }
        else
        {
            arr.Add(new cArrayList("@status", (cbstatus.SelectedIndex - 1).ToString()));
        }
        arr.Add(new cArrayList("@code", cbcode.SelectedValue.ToString()));
        arr.Add(new cArrayList("@word", txsearchprop.Text));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_lookup_proposal", arr);
    }
}