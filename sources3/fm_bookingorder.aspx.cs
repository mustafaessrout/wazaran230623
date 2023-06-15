using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_bookingorder : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dBalance = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            lbremaincl.Text = bll.vLookUp("select dbo.[fn_getremaincl]('"+hdcust.Value.ToString()+"')");
            lbcl.Text = rs["credit_limit"].ToString();
            lbcredit.Text = rs["cuscate_cd"].ToString();
            lbchannel.Text = rs["otlcd"].ToString();
        }
        rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_get3", arr);
        if (Convert.ToDouble(lbremaincl.Text)< 0)
        {
            img.ImageUrl = "~/red.png";
        }
        else
        {
            img.ImageUrl = "~/green.png";
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbbalance = (Label)e.Row.FindControl("lbbalance");
            dBalance += Convert.ToDouble(lbbalance.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotbalance = (Label)e.Row.FindControl("lbtotbalance");
            lbtotbalance.Text = dBalance.ToString();
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_get3", arr);
    }
}