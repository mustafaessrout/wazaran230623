using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_goodreceiptlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_get",arr);
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_goodreceipt_nav.aspx");   // If Connect to Navision
        //Response.Redirect("fm_goodreceipt.aspx");
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_get",arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbreceipt_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbreceipt_no");
        Label lbsalespointcd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbsalespointcd");
        Response.Redirect("fm_goodreceipt.aspx?receipt_no=" + lbreceipt_no.Text + "&salespointcd=" + lbsalespointcd.Text);
    }
    protected void btclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default_2.aspx");
    }
}