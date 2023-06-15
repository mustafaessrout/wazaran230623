using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class lookup_CashierPettyCash : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd",hdfEmployee.Value));
         bll.vBindingGridToSp(ref grd, "sp_temployee_cashOut_get", arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField hdfdoc_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdfdoc_cd");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectData('" + hdfdoc_cd.Value + "');window.close();", true);
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployee(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sEmployee = string.Empty;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["empName"].ToString(), rs["emp_cd"].ToString());
            lCust.Add(sEmployee);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void btShowItemCashout_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", Convert.ToString(hdfEmployee.Value)));
        bll.vBindingGridToSp(ref grd, "sp_temployee_cashOut_get", arr);
    }
}

		
