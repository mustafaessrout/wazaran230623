using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_custedit : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbcustcate, "sp_tmst_customercategory_get","custcate_cd","custcate_nm");
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            bll.vBindingFieldValueToCombo(ref cbchannel, "otlcd");
        }
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
            lbcustcode.Text = rs["cust_cd"].ToString();
            txcustname.Text = rs["cust_nm"].ToString();
            lbadderss.Text = rs["addr"].ToString();
            lbcity.Text = rs["city_cd"].ToString();
            cbcustcate.SelectedValue = rs["cuscate_cd"].ToString();
            txtop.Text = rs["payment_term"].ToString();
            txcl.Text = rs["credit_limit"].ToString();
            cbcusgrcd.SelectedValue = rs["cusgrcd"].ToString();
            cbchannel.SelectedValue = rs["otlcd"].ToString();
        }
        rs.Close();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@credit_limit", txcl.Text));
        arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cuscate_cd", cbcustcate.SelectedValue.ToString()));
        arr.Add(new cArrayList("@payment_term", txtop.Text));
        arr.Add(new cArrayList("@otlcd", cbchannel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cust_nm", txcustname.Text));
        bll.vUpdateMstCustomerByApps(arr);
        arr.Clear();
        arr.Add(new cArrayList("@audit_object", "tmst_customer"));
        arr.Add(new cArrayList("@audit_typ", "U"));
        arr.Add(new cArrayList("@reasn", txreason.Text));
        arr.Add(new cArrayList("@executedby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertAuditTrail(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Customer has been successfully changed!','"+txcust.Text+"','success');", true);
    }
}