using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_balanceconfirm : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        { 
            
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        List<cArrayList> arr = new List<cArrayList>();
        HttpCookie cok;
        cok= HttpContext.Current.Request.Cookies["sp"];
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        cbll bll = new cbll(); System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sEmp = string.Empty;
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        { 
          sEmp=AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString() + "-" + rs["credit_limit"].ToString(),rs["cust_cd"].ToString());
          lCust.Add(sEmp);
        } rs.Close();
        return (lCust.ToArray());
    }
    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        { 
            lbaddress.Text = rs["addr"].ToString();
            lbcity.Text = rs["city_cd"].ToString();
            lbsalesmancode.Text = rs["salesman_cd"].ToString();
            lbmerchan.Text = rs["merchandizer_cd"].ToString();
        } rs.Close();
    }
}