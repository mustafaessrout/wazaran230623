using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class fm_mstpricelevel2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbpricelevel, "sp_tmst_pricelevel_get", "pricelevel_cd", "pricelevel_desc");
          //ScriptManager.RegisterStartupScript(Page,Page.GetType(),"dx","dv.Attributes['class'] = 'hiddiv'",true);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        List<string> lItemName = new List<string>();
        SqlDataReader rs = null;
        string sItemVal = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem(arr, ref rs);
        while (rs.Read())
        {
            sItemVal = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItemName.Add(sItemVal);
        }
        rs.Close();
        return (lItemName.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        string sItemCd = Request.Form[hditem.UniqueID].ToString();
      //  dv.Attributes["class"] = "showdiv";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", sItemCd));
        bll.vBindingGridToSp(ref grdpricelevel, "sp_tpricelevel_dtl_get", arr);
        bll.vGetMstItem2(arr, ref rs);
        while (rs.Read())
        {
            lbitemcode.Text = sItemCd; 
            lbitemname.Text = "-" + rs["item_nm"].ToString();
            lbsize.Text=rs["size"].ToString();
        } rs.Close();
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "hiddendv()", true);
    }


    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@pricelevel_cd", cbpricelevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@unitprice", txprice.Text));
        bll.vInsertPricelevelDtl2(arr);
        arr.Clear();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vBindingGridToSp(ref grdpricelevel, "sp_tpricelevel_dtl_get", arr);
    }
    protected void grdpricelevel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
    }
}