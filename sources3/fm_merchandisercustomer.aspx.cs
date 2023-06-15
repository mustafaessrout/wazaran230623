using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_merchandisercustomer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetMerchandiser(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@emp_nm", prefixText));
        arr.Add(new cArrayList("@job_title_cd", "merchandcd"));         
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstEmployeeByJobTitleQry(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        rs.Close();
        return (lEmployee.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCustomer(string prefixText, int count, string contextKey)
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
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }


    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@merchandiser_cd", hdmerchandiser.Value));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vInsertCustomerMerchandiser(arr);
        arr.Clear();
        arr.Add(new cArrayList("@merchandiser_cd", hdmerchandiser.Value));
        bll.vBindingGridToSp(ref grd, "sp_tcustomer_merchandiser_get", arr);
        txmerchandiser.CssClass = cd.csstextro;
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@merchandiser_cd", hdmerchandiser.Value));
        bll.vBindingGridToSp(ref grd, "sp_tcustomer_merchandiser_get", arr);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_merchandisercustomer.aspx");
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcode = (Label)grd.Rows[e.RowIndex].FindControl("lbcustcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        arr.Add(new cArrayList("@merchandiser_cd", hdmerchandiser.Value));
        bll.vDelCustomerMerchandiser(arr);
        arr.Clear();
        arr.Add(new cArrayList("@merchandiser_cd", hdmerchandiser.Value));
        bll.vBindingGridToSp(ref grd, "sp_tcustomer_merchandiser_get", arr);
    }
}