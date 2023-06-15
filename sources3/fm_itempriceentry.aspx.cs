using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class fm_itempriceentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkPriceLevelDtl(arr);
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingFieldValueToCombo(ref cbcusttype, "otlcd");
            dtstart.Text = System.DateTime.Today.ToShortDateString();
            dtend.Text = System.DateTime.Today.ToShortDateString();
            if (Request.QueryString["pl"] != null)
            {
                SqlDataReader rs = null;
                txpricelevelcode.CssClass = "makeitreadonly";
                txpricelevelcode.Text = Request.QueryString["pl"];
                bll.vGetMstPriceLevel(arr, ref rs);
                while (rs.Read())
                {
                   txpricename.Text = rs["pricelevel_nm"].ToString(); 
                   cbcusttype.SelectedValue = rs["otlcd"].ToString();
                   dtstart.Text = rs["start_dt"].ToString();
                    dtend.Text = rs["end_dt"].ToString();
                }rs.Close();

                arr.Clear();
                arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@pricelevel_cd", txpricelevelcode.Text));
                bll.vInsertPriceDtlToWrk(arr);
                arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_twrk_pricelevel_dtl_get", arr);
                
            }
        }
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListItem(string prefixText, int count, string contextKey)
    {
        SqlDataReader rs = null;
        cbll bll = new cbll();
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItemName = new List<string>();
        string sItemWithVal = string.Empty;
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem(arr, ref rs);
        while (rs.Read())
        {
            sItemWithVal = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItemName.Add(sItemWithVal);
        }
        rs.Close();
        return (lItemName.ToArray());
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        string sID = Request.Form[hditem.UniqueID].ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", sID));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@unitprice", txqty.Text));
        bll.vInsertWrkPriceLevelDtl(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_pricelevel_dtl_get", arr);
        txsearch.Text = "";
        txqty.Text = "";
        txsearch.Focus();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "xd", "alert('" + sID + "')", true);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@pricelevel_cd", txpricelevelcode.Text));
        arr.Add(new cArrayList("@pricelevel_nm", txpricename.Text));
        arr.Add(new cArrayList("@start_dt", dtstart.Text));
        arr.Add(new cArrayList("@end_dt", dtend.Text));
        arr.Add(new cArrayList("@otlcd", cbcusttype.SelectedValue.ToString()));
        bll.vInsertMstPriceLevel(arr);

        arr.Clear();
        arr.Add(new cArrayList("@pricelevel_cd", txpricelevelcode.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertPriceLevelDtl(arr);
        Response.Redirect("fm_mstpricelevellist.aspx");
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_pricelevel_dtl_get", arr);
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkPriceLevelDtl(arr);
        arr.Clear();arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_pricelevel_dtl_get", arr);
    }
}