using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class fm_mstdiscountentry2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkSalespoint(arr);
            bll.vDelWrkCustType(arr);
            bll.vDelWrkCusGrCD(arr);
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm", "ALL");
            bll.vBindingFieldValueToCombo(ref cbcusttype, "otlcd");
           // bll.vBindingFieldValueToCombo(ref cbgroupcustomer, "CusGrCd", "ALL");
            arr.Clear(); arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingFieldValueToCombo(ref cbdisctype, "disc_app");
            bll.vBindingComboToSp(ref cbbranded, "sp_tmst_product_get", "prod_cd","prod_nm", arr);
            cbbranded_SelectedIndexChanged(sender, e);
            //Get Salespoint Infomation
            SqlDataReader rs = null;
            string sLocCd = "";
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstSalespoint(arr, ref rs);
            while (rs.Read())
            {
                lbsp.Text = rs["salespoint_nm"].ToString();
                sLocCd = rs["loc_cd"].ToString();
            } rs.Close(); cbdiscby_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkItem(arr);
           // rdkinditem.SelectedValue = "I";
           // rdkinditem_SelectedIndexChanged(sender, e);
            //arr.Clear();
            //arr.Add(new cArrayList("@loc_cd_parent", sLocCd));
            //arr.Add(new cArrayList("@level_no", 3));
            //bll.vBindingComboToSp(ref cbcity, "sp_tmst_location_get", "loc_cd", "loc_nm", arr, "ALL");
            
        }
    }
   
  
    protected void btaddsp_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbsalespoint.SelectedValue.ToString() == "ALL")
        {
            arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkSalespointAll(arr);
        }
        else
        { 
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespoint_nm", bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'")));
            bll.vInsertWrkSalespoint(arr);
        }
        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
       // bll.vBindingComboToSp(ref cbcity, "sp_tmst_location_get3", "loc_cd", "loc_nm", arr, "ALL");
    }
    protected void btaddcusttype_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbcusttype.SelectedValue.ToString()=="ALL")
        {
            arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkCustTypeAll(arr);
        }
        else { 
       
        arr.Add(new cArrayList("@cust_typ", cbcusttype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@typ_nm", cbcusttype.SelectedItem.Text.ToString()));
        bll.vInsertTwrkCustType(arr);
        }
        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcusttype, "sp_twrk_custtype_get", arr);
        //Out for customer type
        bll.vBindingComboToSp(ref cbgroupcustomer, "sp_lookupcusgrcd", "cusgrcd", "cusgrnm", arr,"ALL");
    }
    protected void grdcusttype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcusttype = (Label)grdcusttype.Rows[e.RowIndex].FindControl("lbcusttype");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_typ", lbcusttype.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkCustType(arr);
        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcusttype, "sp_twrk_custtype_get", arr);
    }
    protected void grdsp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbsalespointcode = (Label)grdsp.Rows[e.RowIndex].FindControl("lbsalespointcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", lbsalespointcode.Text));
        bll.vDelWrkSalespoint(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
    }
    protected void btaddgroup_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@CusGrCD", cbgroupcustomer.SelectedValue.ToString()));
        arr.Add(new cArrayList("@CusGrCD_nm", cbgroupcustomer.SelectedItem.Text.ToString()));
        bll.vInsertWrkCustGrCD(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdgroupcust, "sp_twrk_cusgrcd_get", arr);

    }
    protected void grdgroupcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcusgrcd = (Label)grdgroupcust.Rows[e.RowIndex].FindControl("lbcusgrcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cusgrcd", lbcusgrcd.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkCusGrCD(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdgroupcust, "sp_twrk_cusgrcd_get",arr);
    }
    protected void cbbranded_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd_parent", cbbranded.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_no", 2));
        bll.vBindingComboToSp(ref cbgroupproduct, "sp_tmst_product_get","prod_cd","prod_nm", arr,  "ALL");
    }
    protected void cbgroupproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 3));
        arr.Add(new cArrayList("@prod_cd_parent", cbgroupproduct.SelectedValue.ToString()));
      //  bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr,"ALL");
    }
    protected void grdsp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
    }
    protected void grdsp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdsp.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get",arr);
    }
    //protected void grdsp_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    //{
    //    Label lbsalespointcode = (Label)grdsp.Rows[e.RowIndex].FindControl("lbsalespointcode");
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
    //    arr.Add(new cArrayList("@salespointcd", lbsalespointcode.Text));
    //    bll.vDelWrkSalespoint(arr);
    //    arr.Clear()

    //}
    protected void grdgroupcust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcusttype, "sp_twrk_custtype_get", arr);
    }
    protected void cbcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 4));
       // arr.Add(new cArrayList("@loc_cd_parent", cbcity.SelectedValue.ToString()));
       // bll.vBindingComboToSp(ref cbdistric, "sp_tmst_location_get", "loc_cd", "loc_nm", arr, "ALL");
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
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
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        //Response.Redirect("fm_mstdiscountentry2.aspx?ip=" + hditem.Value.ToString());
        //ScriptManager.RegisterStartupScript(Page,Page.GetType(),"xx","alert('" +  hditem.Value.ToString() + "')",true);
    }
    protected void btsearch_Click1(object sender, EventArgs e)
    {

    }


    protected void cbdiscby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbdiscby.SelectedValue == "I")
        {
            dvop1.Visible = true;
            dvop2.Visible = false;
        }
        else
        {
            dvop1.Visible = false;
            dvop2.Visible = true;
        }
    }
    protected void btaddit_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        bll.vInsertWrkItem(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditemdiscount, "sp_twrk_item_get", arr);
    }
    protected void btaddprod_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbgroupproduct.SelectedValue.ToString()));
        bll.vInsertWrkItem2(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditemdiscount, "sp_twrk_item_get", arr);
    }
    protected void grditemdiscount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grditemdiscount.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditemdiscount, "sp_twrk_item_get", arr);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", txdiscountcode.Text));
        arr.Add(new cArrayList("@min_qty", txqty.Text));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@disc_method", cbdisctype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@disc_pct", txpct.Text));
        arr.Add(new cArrayList("@disc_qty", txdiscqty.Text));
        arr.Add(new cArrayList("@disc_amt", txamt.Text));
        bll.vInsertDiscountFormula(arr);
        bll.vBindingGridToSp(ref grddiscount, "sp_tdiscount_formula_get");
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        string sItem = string.Empty;
        SqlDataReader rs = null;
        cbll bll = new cbll();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        List<string> lItem = new List<string>();
        bll.vGetMstItem2(arr, ref rs);
        while (rs.Read())
        { 
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_nm"].ToString(),rs["item_cd"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return lItem.ToArray();
    }
}