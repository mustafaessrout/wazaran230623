using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_dolist : System.Web.UI.Page
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
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            string sho = Request.Cookies["sp"].Value.ToString();
            if (sho == "0")
            {
                cbsalespoint.Enabled = true;
                cbsalespoint.CssClass = "";
                //cbsalespoint.Items.RemoveAt(0);
            }
            else
            {
                cbsalespoint.Enabled = false;
                cbsalespoint.CssClass = "makeitreadonly ro form-control";
            }
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grddo, "sp_tmst_do_get", arr);
        }
        
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        
        if (txsearch.Text==null ||txsearch.Text=="" )
        {
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grddo, "sp_tmst_do_get",arr);
        }
        else
        { 
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@do_no", txsearch.Text));
            bll.vBindingGridToSp(ref grddo, "sp_tmst_do_get",arr);
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_do.aspx");
    }
    protected void grddo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grddo.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grddo, "sp_tmst_do_get",arr);
    }
    protected void grddo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdono = (Label)grddo.Rows[e.NewSelectedIndex].FindControl("lbdono");
        Response.Redirect("fm_do.aspx?do=" + lbdono.Text+"&sp=" + cbsalespoint.SelectedValue.ToString());
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        btsearch_Click(sender, e);
    }
}