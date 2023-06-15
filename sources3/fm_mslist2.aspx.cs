using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class fm_mslist2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                bll.vBindingGridToSp(ref grditem, "sp_tmst_item_get");
                //   arr.Add(new cArrayList("@level_no", 1));
                //   bll.vBindingComboToSp(ref cbbrand, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
                //    cbbrand_SelectedIndexChanged(sender, e);

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mslist2");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void grditem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grditem.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_nm", txsearch.Text));
            bll.vBindingGridToSp(ref grditem, "sp_tmst_item_search", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mslist2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grditem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    Label lbitemcode = (Label)grditem.SelectedRow.FindControl("lbitemcode");
        //    Response.Redirect("fm_mstentry2.aspx?item=" + lbitemcode.Text);
        //}
    }
    protected void grditem_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
        Label lblitemcode = (Label)grditem.Rows[e.NewSelectedIndex].FindControl("lbitemcode");
        Response.Redirect("fm_mstentry2.aspx?item=" + lblitemcode.Text );
    }
    protected void cbbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 2));
      //  arr.Add(new cArrayList("@prod_cd_parent", cbbrand.SelectedValue.ToString()));
    //    bll.vBindingComboToSp(ref cbgroupproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr, "ALL");
    }
    protected void cbgroupproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 3));
    //    arr.Add(new cArrayList("@prod_cd_parent", cbgroupproduct.SelectedValue.ToString()));
    //    bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr, "ALL");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_nm", txsearch.Text));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tmst_item_search", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mslist2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    //[System.Web.Script.Services.ScriptMethod()]
    //[System.Web.Services.WebMethod]
    //public static List<string> GetItemList(string prefixText, int count)
    //{
    //    SqlDataReader rs = null;
    //    cbll bll = new cbll();
    //    List<cArrayList> arr = new List<cArrayList>();
    //    List<string> itemname = new List<string>();
    //    arr.Add(new cArrayList("@item_nm", prefixText));
    //    bll.vSearchMstItem(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        itemname.Add(rs["item_nm"].ToString());
    //    }
    //    rs.Close();
    //    return (itemname);
    //}
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstentry2.aspx");
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        { 
           sItem=AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | "  + rs["item_nm"].ToString() , rs["item_cd"].ToString());
           lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=it');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=MSTSL');", true); 
    }
   
}