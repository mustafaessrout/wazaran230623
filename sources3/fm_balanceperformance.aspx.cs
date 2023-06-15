using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_balanceperformance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbreport_SelectedIndexChanged(sender, e);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if(cbreport.SelectedValue=="0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=balnperf&cust=" + hdcust.Value.ToString() + "&rep=" + cbreport.SelectedValue + "');", true);
        }
        else {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=balnperf&salesman=" + hdemp.Value.ToString() + "&rep=" + cbreport.SelectedValue + "');", true);
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
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList1(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        string sEmp = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@job_title", "SALESCD"));
        //arr.Add(new cArrayList("@emp_nm", prefixText));
        cbll bll = new cbll();
        bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
        while (rs.Read())
        {
            sEmp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
            lEmp.Add(sEmp);
        } rs.Close();
        return (lEmp.ToArray());
    }
    protected void cbreport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "0")
        {
            txsalesman.Visible = false;
            txcust.Visible = true;
            lbcust.Visible = true;
            lbcust.Text = "Customer";
            txsalesman.Text = "";          
        }
        else
        {
            txcust.Visible = false;
            txsalesman.Visible = true;            
            lbcust.Text = "Salesman";
            txcust.Text = "";
        }
    }
}