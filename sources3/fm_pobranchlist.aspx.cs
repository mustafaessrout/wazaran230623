using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data.SqlClient;

public partial class fm_pobranchlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbpostatus, "po_sta_id");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        SqlDataReader rs = null;
        List<string> lPoName = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sPoName = string.Empty;
        cbll bll = new cbll();
        arr.Add(new cArrayList("@po_no", prefixText));
        bll.vSearchMstPo(arr,ref rs);
        while (rs.Read())
        { 
            sPoName = AutoCompleteExtender.CreateAutoCompleteItem(rs["po_no"].ToString() + " | "  + rs["remark"].ToString()  ,rs["po_no"].ToString());
            lPoName.Add(sPoName);
        } rs.Close();
        return (lPoName.ToArray());
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_po.aspx");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
       // SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        if (hdpo.Value != null)
        { 
            arr.Add(new cArrayList("@po_no", hdpo.Value.ToString()));
        }
        arr.Add(new cArrayList("@po_sta_id", cbpostatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdpo, "sp_tmst_po_get", arr);

    }
    protected void grdpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbpono = (Label)grdpo.Rows[grdpo.SelectedIndex].FindControl("lbpono");
        Response.Redirect("fm_po.aspx?po=" + lbpono.Text);
    }
    protected void grdpo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdpo.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", txsearch.Text));
        bll.vBindingGridToSp(ref grdpo, "sp_tmst_po_get", arr);
    }
}