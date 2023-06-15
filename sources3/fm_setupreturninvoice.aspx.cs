using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_setupreturninvoice : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tcustomer_returninvoice_get");
        }
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vInsertCustomerReturnInvoice(arr);
        bll.vBindingGridToSp(ref grd, "sp_tcustomer_returninvoice_get");
        txcustomersearch.Text = string.Empty;
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcode = (Label)grd.Rows[e.RowIndex].FindControl("lbcustcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        bll.vDelCustomerReturnInvoice(arr);
        bll.vBindingGridToSp(ref grd, "sp_tcustomer_returninvoice_get");
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        //bll.vSearchCustomerBySales(arr, ref rs);
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
}