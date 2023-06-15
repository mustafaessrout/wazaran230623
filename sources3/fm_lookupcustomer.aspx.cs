using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class fm_lookupcustomer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkLookupCustomer(arr);
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@custcd", txsearch.Text));
        arr.Add(new cArrayList("@salespointcd" , Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsearch, "sp_tmst_customer_search", arr);
    }
    protected void grdsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbcust = (Label)grdsearch.SelectedRow.FindControl("lbcodecustomer");
        Label lbcustname = (Label)grdsearch.SelectedRow.FindControl("lbcustomername");
        Label lbcustarabic = (Label)grdsearch.SelectedRow.FindControl("lbarabic");
        Label lbaddr = (Label)grdsearch.SelectedRow.FindControl("lbaddr1");
        Label lbcity = (Label)grdsearch.SelectedRow.FindControl("lbcity");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", lbcust.Text));
        arr.Add(new cArrayList("@cust_nm", lbcustname.Text));
        arr.Add(new cArrayList("@addr", lbaddr.Text));
        arr.Add(new cArrayList("@city", lbcity.Text));
        bll.vInsertWrkLookupCustomer(arr);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "er", "window.opener.updpnl2('" + lbcust.Text  +  "')", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close()",true);
    }
    protected void grdsearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdsearch.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@custcd", txsearch.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsearch, "sp_tmst_customer_search", arr);
    }
}