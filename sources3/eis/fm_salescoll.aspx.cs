using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eis_fm_salescoll : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToComboWithALL(ref cbchannel, "otlcd");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getbyall", "salespointcd", "salespoint_nm");
            bll.vBindingFieldValueToCombo(ref cbyear, "year");
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
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbyear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@otlcd", cbchannel.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_eis_rptsalescoll2", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_eis_salesbycustomer", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void grddtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "ShowProgress();", true);
        List<cArrayList> arr = new List<cArrayList>();
        grddtl.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_eis_salesbycustomer", arr);
    }
}