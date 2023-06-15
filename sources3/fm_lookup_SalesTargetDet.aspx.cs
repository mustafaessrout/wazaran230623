using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class fm_lookup_SalesTargetDet : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@slsTargetCDNM", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblSalesTargetDet_get3", arr);

    }
    protected void btok_Click(object sender, EventArgs e)
    {
       
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbslstargetDetID = (Label)grd.SelectedRow.FindControl("lbslstargetDetID");

        Session["looslstargetDetID"] = lbslstargetDetID.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@slsTargetCDNM", prefixText));
        bll.vSearchtblSalesTargetDet(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["slsTargetCD"].ToString() +  rs["prod_nm2"].ToString(), rs["slsTargetCD"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }
}