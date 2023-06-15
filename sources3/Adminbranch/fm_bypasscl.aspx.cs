using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_bypasscl : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { vBindingGrid(); }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
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
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txremark.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Remark can not empty','Check Remark','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@blocked_typ", "S"));
        arr.Add(new cArrayList("@blocked_dt", bll.vLookUp("select dbo.fn_getsystemdate()")));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@remark", txremark.Text));
        bll.vInsertCustomerBlock(arr);
        vBindingGrid();
        hdcust.Value = "";
        txcust.Text = "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@blocked_typ", "S"));
        bll.vBindingGridToSp(ref grd, "sp_tcustomer_blocked_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcode = (Label)grd.Rows[e.RowIndex].FindControl("lbcustcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        arr.Add(new cArrayList("@blocked_typ", "S"));
        bll.vDelCustomerBlocked(arr);
        vBindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@blocked_typ", "S"));
        bll.vBindingGridToSp(ref grd, "sp_tcustomer_blocked_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        System.Data.SqlClient.SqlDataReader rs = null;
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            txcreditlimit.Text = rs["credit_limit"].ToString();
            txdue.Text = bll.vLookUp("select [dbo].[fn_getoverduecustomer]('" + hdcust.Value.ToString() + "')");
            txlstorder.Text = bll.vLookUp("select [dbo].[fn_getlstorderdt]('" + hdcust.Value.ToString() + "')");
            txtop.Text = rs["payment_term"].ToString();
            txbalance.Text = bll.vLookUp("select dbo.fn_getbalanceinvoice('" + hdcust.Value.ToString() + "')");
            txcreditlimit.Enabled = false;
            txdue.Enabled = false;
            txlstorder.Enabled=false;
            txbalance.Enabled = false;
            txtop.Enabled = false;
        } rs.Close();
        rs = null;
    }
}