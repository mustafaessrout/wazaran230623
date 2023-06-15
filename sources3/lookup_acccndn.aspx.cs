using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class lookup_acccndn : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@ref_no", (txtRefNo.Text == "" ? null: txtRefNo.Text)));
        arr.Add(new cArrayList("@inv_no", (txtInvoice.Text == "" ? null : txtInvoice.Text)));
        arr.Add(new cArrayList("@cust_cd", (txtCustomer.Text == "" ? null : txtCustomer.Text.Split('-')[0])));
        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceCNDN_get", arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblCNDN_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblCNDN_no");
        Label lblinv_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinv_no");
        Label lblrefho_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblrefho_no");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectData('" + lblCNDN_no.Text + "|" + lblinv_no.Text + "|" + lblrefho_no.Text + "');window.close();", true);
    }
}

		
