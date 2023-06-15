using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_customer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@custcd", txsearch.Text));
        arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get", arr);

    }
    protected void btok_Click(object sender, EventArgs e)
    {
       
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbCustCD = (Label)grd.SelectedRow.FindControl("lbCustCD");
        //Label lbCustNM = (Label)grd.SelectedRow.FindControl("lbCustNM");
        //Label lbaddr1 = (Label)grd.SelectedRow.FindControl("lbaddr1");
        //Label lbcity = (Label)grd.SelectedRow.FindControl("lbcity");
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //arr.Add(new cArrayList("@salesPointCD", Request.Cookies["salespoint_cd"].Value));
        //arr.Add(new cArrayList("@CustCD", lbCustCD.Text));
        //arr.Add(new cArrayList("@CustNM", lbCustNM.Text));
        //arr.Add(new cArrayList("@addr1", lbaddr1.Text));
        //arr.Add(new cArrayList("@city", lbcity.Text));
        //bll.vInsertWrkLookupCustomer(arr);
        Session["looCustomerCustCD"] = lbCustCD.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}