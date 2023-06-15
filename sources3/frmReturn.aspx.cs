using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class frmReturn : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            bll.vBindingComboToSp(ref cbSalesCD, "sp_tmst_employee_sal_get", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref cbpricelevel_cd, "sp_tmst_pricelevel_get", "pricelevel_cd", "pricelevel_cd");
            bll.vBindingFieldValueToCombo(ref cbUOM, "uom");
            arr.Clear();
            arr.Add(new cArrayList("@reasn_typ", "100"));
            bll.vBindingComboToSp(ref cbreasn_cd, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbsiteType, "stock_typ");
            bll.vBindingFieldValueToCombo(ref cbsiteDest , "siteDest");
            txinvDate.Text = DateTime.Now.ToString("d/M/yyyy");
            txinvDocDate.Text = DateTime.Now.ToString("d/M/yyyy"); 
            cbsiteDest_SelectedIndexChanged(sender, e);
            clearAdd();
            //arr.Clear();
            //arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
            //bll.vBindingComboToSp(ref cbsiteCD, "sp_tmst_site_get", "siteCD", "site_desc", arr);
            
        }
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        if (txreturnCD.Text == "")
        {
            hdrsave(sender, e);
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ReturnID", txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@UOMCD", cbUOM.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inpQuantity", txinpQuantity.Text));
        arr.Add(new cArrayList("@inpQuantityRec", txinpQuantityRec.Text));
        arr.Add(new cArrayList("@inpPrice", txinpPrice.Text));
        arr.Add(new cArrayList("@inpAmount", txinpAmount.Text));
        arr.Add(new cArrayList("@inpexpireDate", DateTime.ParseExact(txinpexpireDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@reasn_cd", cbreasn_cd.SelectedValue));
        bll.vInsertTblReturnDtl(arr);

        clearAdd();
        txsearchitem.Focus();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@returnID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblReturnDtl_get", arr);
    }

    private void clearAdd()
    {
       txsearchitem.Text = "";
       txinpQuantity.Text = "0";
       txinpQuantityRec.Text = "0";
       txinpPrice.Text = "0";
       txinpAmount.Text = "0";
       txinpexpireDate.Text = "";
       
 
    }
    protected void hdrsave(object sender, EventArgs e)
    {
        if (txKey.Text == null || txKey.Text == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sReturnCD = "0";
            arr.Add(new cArrayList("@invDate", DateTime.ParseExact(txinvDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@invDocDate", DateTime.ParseExact(txinvDocDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salesCD", cbSalesCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@siteDest",cbsiteDest.SelectedValue ));
            arr.Add(new cArrayList("@siteCD", hdsiteCD.Value));
            arr.Add(new cArrayList("@siteCDVan", hdsiteCDVan.Value));
            arr.Add(new cArrayList("@siteType", cbsiteType.SelectedValue));
            arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@CustCD", hdcust_cd.Value));
            arr.Add(new cArrayList("@pricelevel_cd", cbpricelevel_cd.SelectedValue));
            arr.Add(new cArrayList("@invReference", txinvReference.Text));
            arr.Add(new cArrayList("@invNote", txinvNote.Text));
            bll.vInserttblReturn(arr, ref sReturnCD);
            txreturnCD.Text = sReturnCD;
            txKey.Text = bll.vLookUp("select ReturnID from tblReturn where ReturnCD='" + sReturnCD + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'"); ;

            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@returnID", txKey.Text));
            bll.vBindingGridToSp(ref grd, "sp_tblReturnDtl_get", arr);

        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@returnID", txKey.Text));
            arr.Add(new cArrayList("@invDate", DateTime.ParseExact(txinvDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@invDocDate", DateTime.ParseExact(txinvDocDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salesCD", cbSalesCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@siteDest", cbsiteDest.SelectedValue));
            arr.Add(new cArrayList("@siteCD", hdsiteCD.Value));
            arr.Add(new cArrayList("@siteCDVan", hdsiteCDVan.Value));
            arr.Add(new cArrayList("@siteType", cbsiteType.SelectedValue));
            arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@CustCD", hdcust_cd.Value));
            arr.Add(new cArrayList("@pricelevel_cd", cbpricelevel_cd.SelectedValue));
            arr.Add(new cArrayList("@invReference", txinvReference.Text));
            arr.Add(new cArrayList("@invNote", txinvNote.Text));
            bll.vUpdatetblReturn(arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@returnID", txKey.Text));
            bll.vBindingGridToSp(ref grd, "sp_tblReturnDtl_get", arr);

        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCust(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lCust = new List<string>();
        arr.Add(new cArrayList("@SalesPointCD", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@cust_nm", prefixText));
        bll.vSearchMstCust2(arr, ref rs);
        while (rs.Read())
        {
            sCust = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " | " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        } rs.Close();
        return (lCust.ToArray());
    }
    protected void txsearchCust_TextChanged(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@CustCD", hdcust_cd.Value));
        //arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.Text));
        //bll.vBindingComboToSp(ref cbARCRefID, "sp_InvoiceAging_get", "SOID", "InvDescription", arr);
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txKey.Text = Convert.ToString(Session["looReturnReturnID"]);
        cbSalesPointCD.SelectedValue = Convert.ToString(Session["looReturnSalespointCD"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@returnID", txKey.Text));
        bll.vGetTblReturn(arr, ref rs);
        while (rs.Read())
        {
            txreturnCD.Text = rs["returnCD"].ToString();
            txinvDocDate.Text = string.Format("{0:d/M/yyyy}", rs["invDocDate"]);
            txinvDate.Text = string.Format("{0:d/M/yyyy}",rs["invDate"]);
            cbSalesCD.SelectedValue = rs["emp_cd"].ToString();
            cbsiteDest.SelectedValue = rs["siteDest"].ToString();
            hdsiteCD.Value = rs["siteCD"].ToString();
            txsearchsiteCD.Text = rs["SITECD"].ToString() + " | " + bll.vLookUp("select whs_nm from tmst_warehouse where whs_cd='" + rs["SITECD"].ToString() + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'");
            hdsiteCDVan.Value = rs["siteCDVan"].ToString();
            txsearchsiteCDVan.Text = rs["siteCDVan"].ToString() + " | " + bll.vLookUp("select (select emp_nm from tmst_employee where emp_cd=tmst_vehicle.emp_cd ) vhc_nm from tmst_vehicle  where vhc_cd='" + rs["siteCDVan"].ToString() + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'");
            cbsiteType.SelectedValue = rs["siteType"].ToString();
            hdcust_cd.Value = rs["CustCD"].ToString();
            txsearchCust.Text = rs["cust_cd"].ToString() + " | " + rs["cust_nm"].ToString();
            cbpricelevel_cd.SelectedValue = rs["pricelevel_cd"].ToString();
            txinvReference.Text = rs["invReference"].ToString();
            txinvNote.Text = rs["invNote"].ToString();

        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@returnID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblReturnDtl_get", arr);
     
       


    }
    
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbSeqID = (Label)grd.Rows[e.RowIndex].FindControl("lbSeqID");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SeqID", lbSeqID.Text));
        bll.vDeleteTblReturnDet(arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@returnID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblReturnDtl_get", arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblitem_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblitem_cd");
        Label lblitem_nm = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblitem_nm");
        Label lblUOMCD = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblUOMCD");
        Label lblinpQuantity = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinpQuantity");
        Label lblinpQuantityRec = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinpQuantityRec");
        Label lblinpPrice = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinpPrice");
        Label lblinpAmount = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinpAmount");
        Label lblinpexpireDate = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinpexpireDate");
        Label lblreasn_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblreasn_cd");

        txsearchitem.Text = lblitem_cd.Text + " | " + lblitem_nm.Text;
        hditem.Value = lblitem_cd.Text;
        cbUOM.SelectedValue = lblUOMCD.Text;
        txinpQuantity.Text = lblinpQuantity.Text;
        txinpQuantity.Text = lblinpQuantity.Text;
        txinpQuantityRec.Text = lblinpQuantityRec.Text;
        txinpPrice.Text = lblinpPrice.Text;
        txinpAmount.Text = lblinpAmount.Text;
        txinpexpireDate.Text = lblinpexpireDate.Text;
        cbreasn_cd.SelectedValue = lblreasn_cd.Text;
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        hdrsave(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data saved successfully !')", true);
    }
    public string GetValue()
    {
        return (txsearchitem.Text);
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> GetListItem(string prefixText, int count)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        List<string> lItem = new List<string>();
        bll.vSearchMstItem(arr, ref rs);
        while (rs.Read())
        { lItem.Add(rs["item_nm"].ToString()); } rs.Close();
        return (lItem);
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
     protected void txsearchitem_TextChanged(object sender, EventArgs e)
     {

     }
     protected void btnew_Click(object sender, EventArgs e)
     {
         Response.Redirect("frmReturn.aspx");
     }
     protected void btDelete_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@returnID", txKey.Text));
         arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
         bll.vDeleteTblReturn(arr);
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Deleted successfully !')", true);
         Response.Redirect("frmReturn.aspx");
     }
     protected void btprint_Click(object sender, EventArgs e)
     {
         List<string> arr = new List<string>();
         arr.Add("{tblreturn.returnCD} = '" + txreturnCD + "' AND {tblreturn.SalesPointCD} = '" + cbSalesPointCD.SelectedValue  + "'");
         Session["lformulaReturn"] = arr;
         Response.Redirect("fm_report.aspx?src=Return");
     }
     protected void txinpQuantityRec_TextChanged(object sender, EventArgs e)
     {
         txinpAmount.Text = Convert.ToString((Convert.ToDecimal(txinpQuantityRec.Text) * Convert.ToDecimal(txinpPrice.Text))); 
     }
     
     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
     public static string[] GetCompletionListsiteCDVan(string prefixText, int count, string contextKey)
     {
         HttpCookie cookieSP;
         cookieSP = HttpContext.Current.Request.Cookies["sp"];
         cbll bll = new cbll();
         SqlDataReader rs = null;
         string svhc_nm = string.Empty;
         List<cArrayList> arr = new List<cArrayList>();
         List<string> lsvhc_nm = new List<string>();
         arr.Add(new cArrayList("@SalesPointCD", cookieSP.Value.ToString()));
         arr.Add(new cArrayList("@vhc_nm", prefixText));
         bll.vSearchMstVehicle(arr, ref rs);
         while (rs.Read())
         {
             svhc_nm = AutoCompleteExtender.CreateAutoCompleteItem(rs["vhc_cd"].ToString() + " | " + rs["vhc_nm"].ToString(), rs["vhc_cd"].ToString());
             lsvhc_nm.Add(svhc_nm);
         } rs.Close();
         return (lsvhc_nm.ToArray());
     }

     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
     public static string[] GetCompletionListsiteCD(string prefixText, int count, string contextKey)
     {
         HttpCookie cookieSP;
         cookieSP = HttpContext.Current.Request.Cookies["sp"];
         cbll bll = new cbll();
         SqlDataReader rs = null;
         string swhs_nm = string.Empty;
         List<cArrayList> arr = new List<cArrayList>();
         List<string> lswhs_nm = new List<string>();
         arr.Add(new cArrayList("@SalesPointCD", cookieSP.Value.ToString()));
         arr.Add(new cArrayList("@whs_nm", prefixText));
         bll.vSearchMstwarehouse(arr, ref rs);
         while (rs.Read())
         {
             swhs_nm = AutoCompleteExtender.CreateAutoCompleteItem(rs["whs_cd"].ToString() + " | " + rs["whs_nm"].ToString(), rs["whs_cd"].ToString());
             lswhs_nm.Add(swhs_nm);
         } rs.Close();
         return (lswhs_nm.ToArray());
     }

     protected void cbsiteDest_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (cbsiteDest.SelectedValue=="1")
         {
             txsearchsiteCD.Visible = true;
             txsearchsiteCDVan.Visible = false;

         }
         else
         {
             txsearchsiteCD.Visible = false;
             txsearchsiteCDVan.Visible = true;
         }
     }
     protected void txinpPrice_TextChanged(object sender, EventArgs e)
     {
         txinpAmount.Text = Convert.ToString((Convert.ToDecimal(txinpQuantityRec.Text) * Convert.ToDecimal(txinpPrice.Text))); 
     }
     protected void txinpQuantity_TextChanged(object sender, EventArgs e)
     {
         txinpQuantityRec.Text = txinpQuantity.Text;
         txinpAmount.Text = Convert.ToString((Convert.ToDecimal(txinpQuantityRec.Text) * Convert.ToDecimal(txinpPrice.Text))); 
     }
}   