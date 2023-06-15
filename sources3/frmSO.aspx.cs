using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class frmSO : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            

            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@sinStatusID", 0));
            //arr.Add(new cArrayList("@LastStatusID", 0));
            //bll.vBindingComboToSp2(ref cbStatusID, "sprGetTrsStatus", "StatusID", "staName", arr, "");
            txstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='SOStatusID' and fld_valu=0");
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            //bll.vBindingComboToSp(ref cbitem_cd, "sp_tmst_item_get", "item_cd", "item_cd");
            //txitem_SN.Text = bll.vLookUp("select item_nm from tmst_item where item_cd='" + cbitem_cd.SelectedValue.ToString() + "'");
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            bll.vBindingComboToSp(ref cbSalesCD, "sp_tmst_employee_sal_get", "emp_cd", "emp_desc", arr);
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
            //txsalespoint_nm.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where SalesPointCD='" + txSalesPointCD.Text + "'");
            //bll.vBindingComboToSp(ref cbpricelevel_cd, "sp_tmst_pricelevel_get", "pricelevel_cd", "pricelevel_cd");
            bll.vBindingFieldValueToCombo(ref cbpricelevel_cd, "otlcd");
            
            //arr.Clear();
            //arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
            ////bll.vBindingComboToSp(ref cbCustCD, "sp_tmst_customer_get", "Cust_CD", "cust_desc", arr);
            //txaddr.Text = bll.vLookUp("select addr from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
            //txcreditLimit.Text = bll.vLookUp("select credit_limit from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
            //cbpricelevel_cd.SelectedValue = bll.vLookUp("select pricelevel_cd from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
            //cbpayment_term.SelectedValue = bll.vLookUp("select payment_term from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
            //cbSalesCD.SelectedValue = bll.vLookUp("select salesman_cd from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'"); ;
            //arr.Clear();
            //arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
            //bll.vBindingComboToSp(ref cbsiteCD, "sp_tmst_warehouse_get", "whs_CD", "whs_nm", arr);
            //arr.Clear();
            //arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
            //bll.vBindingComboToSp(ref cbsiteCDVan, "sp_tmst_vehicle_get", "vhc_cd", "vhc_nm", arr);


            bll.vBindingFieldValueToCombo(ref cbsiteType, "bin_cd");
          
            
            bll.vBindingFieldValueToCombo(ref cbUnitCD, "uom");
            bll.vBindingFieldValueToCombo(ref cborderby, "orderby");
            bll.vBindingFieldValueToCombo(ref cbpayment_term, "payment_term");
            bll.vBindingFieldValueToCombo(ref cbotlcd, "OtlCD");
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@soID", txKey.Text));
            bll.vBindingGridToSp(ref grdSO, "sp_tblSODtl_get",arr);
            txsopDiscount.Text = "0";
            txsopPrice.Text = "0";
            txsopQuantityStock.Text = "0";
            txsopQuantity.Text = "0";
            txsopQuantityDelivery.Text = "0";
            //txsearchsiteCDVan.CssClass = "makeitreadonly";
            //txsearchsiteCDVan.ReadOnly = true;

            txsoDate.Text = DateTime.Now.ToString("d/M/yyyy");
            txsoDocDate.Text = DateTime.Now.ToString("d/M/yyyy");
            //DateTime dt = DateTime.ParseExact(DateTime.Now.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //MakeReadOnly();
            //cbStatusID.Visible = false;
            string sSrc = Request.QueryString["src"].ToString();
            switch (sSrc)
            {
                case "SO":
                    txsearchsiteCDVan.Visible = false;
                    txsearchsiteCD.Visible = true;
                    arr.Clear();
                    arr.Add(new cArrayList("@fld_nm", "tranType"));
                    arr.Add(new cArrayList("@hiddendata", 0));
                    bll.vBindingFieldValueToCombo(ref cbtranType, arr);
                    lbTittle.Text = "SALES ORDER PROCESS";
                    break;
                case "Canvass":
                    txsearchsiteCD.Visible = false;
                    txsearchsiteCDVan.Visible = true;
                    arr.Clear();
                    arr.Add(new cArrayList("@fld_nm", "tranType"));
                    arr.Add(new cArrayList("@hiddendata", 1));
                    bll.vBindingFieldValueToCombo(ref cbtranType, arr);
                    lbTittle.Text = "CANVASS PROCESS";
                    break;
            }
            lbDriver.Visible = false;
            cbdriver.Visible = false;
            lbInvoiceCD.Visible = false;
            txInvoiceCD.Visible = false;
            lbreferenceInv.Visible = false;
            txreferenceInv.Visible = false;
            btprint.Visible = false;
            btprintInv.Visible = false;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@qry_CD", "driver"));
            bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_sal_get", "emp_cd", "emp_desc", arr);

        }
    }
   
    protected void btAdd_Click(object sender, EventArgs e)
    {

        if (txSOCD.Text == "")
        {
            hdrsave(sender, e);
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SOID",txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD",cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@UnitCD", cbUnitCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@sopQuantityStock", txsopQuantityStock.Text));
        arr.Add(new cArrayList("@sopQuantity", txsopQuantity.Text));
        arr.Add(new cArrayList("@sopQuantityDelivery", txsopQuantityDelivery.Text));
        arr.Add(new cArrayList("@sopPrice", txsopPrice.Text));
        arr.Add(new cArrayList("@sopDiscount", txsopDiscount.Text));
        bll.vInsertTblSODtl(arr);
        clearAdd();
        txsearchitem.Focus();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdSO, "sp_tblSODtl_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdProd, "sp_tblSODtlDisc_get", arr);
        
        
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        hdrsave(sender, e);
        //if (txstatus.Text=="Entry")
        //{ 
        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@varTransName", "SO"));
        //    arr.Add(new cArrayList("@bigID",  txKey.Text));
        //    arr.Add(new cArrayList("@sinAftStatusID", "1"));
        //    bll.vUpdTransStatus(arr);
        //    txstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='SOStatusID' and fld_valu=1");         
        //}

        MakeReadOnly();
        buttonDisabled();
        btprint.CssClass = "button2 print";
        btprint.Enabled = true;
        btprint.Visible = true;
        btprintInv.Visible = false;
        UnvisibleInvoice();
        disableInput();
        txsopQuantityDelivery.Enabled = true;
        btAdd.Enabled = true;
        btEdit.CssClass = "button2 edit";
        btEdit.Enabled = true;
        if (txstatus.Text != "ENTRY")
        {
            visibleInvoice();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data saved successfully !')", true);
    }
    protected void hdrsave(object sender, EventArgs e)
    {
        if (txKey.Text == null || txKey.Text == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sSOCD = "0";
            //   arr.Add(new cArrayList("@SOCD", txSOCD.Text));
            //arr.Add(new cArrayList("@JournalTypeCD", txJournalTypeCD.Text));
            
            arr.Add(new cArrayList("@soDocDate", DateTime.ParseExact(txsoDocDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@soDate", DateTime.ParseExact(txsoDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@SalesCD", cbSalesCD.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@siteDest", cbsiteDest.SelectedValue.ToString()));
            arr.Add(new cArrayList("@siteCD", hdsiteCD.Value));
            arr.Add(new cArrayList("@siteCDVan", hdsiteCDVan.Value));
            arr.Add(new cArrayList("@siteType", cbsiteType.SelectedValue.ToString()));
            arr.Add(new cArrayList("@CurrencyID", 1));
            arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@CustCD", hdcust_cd.Value));
            arr.Add(new cArrayList("@creditLimit", txcreditLimit.Text));
            arr.Add(new cArrayList("@pricelevel_cd", cbpricelevel_cd.SelectedValue));
            arr.Add(new cArrayList("@payment_term", cbpayment_term.SelectedValue));
            arr.Add(new cArrayList("@soDueDate", DateTime.ParseExact(txsoDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@reference", txreference.Text));
            arr.Add(new cArrayList("@referenceInv", txreferenceInv.Text));
            arr.Add(new cArrayList("@orderby", cborderby.SelectedValue));
            arr.Add(new cArrayList("@driver", cbdriver.SelectedValue));
            arr.Add(new cArrayList("@tranType", cbtranType.SelectedValue));
            arr.Add(new cArrayList("@soPO", txsoPO.Text));
            arr.Add(new cArrayList("@soPODate", txsoPODate.Text));
            bll.vInserttblSO(arr, ref sSOCD);
            txSOCD.Text = sSOCD;
            txKey.Text = bll.vLookUp("select SOID from tblso where SOCD='" + sSOCD + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'"); ;
            txInvoiceCD.Text = bll.vLookUp("select InvoiceCD from tblso where SOCD='" + sSOCD + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'"); ; ;
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@soID", txKey.Text));
            bll.vBindingGridToSp(ref grdSO, "sp_tblSODtl_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@soID", txKey.Text));
            bll.vBindingGridToSp(ref grdProd, "sp_tblSODtlDisc_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@soID", txKey.Text));
            bll.vBindingGridToSp(ref grdDisc, "sp_tblSODtlDisc_get2", arr);
        
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@SOID", txKey.Text));
            arr.Add(new cArrayList("@soDocDate", DateTime.ParseExact(txsoDocDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@soDate", DateTime.ParseExact(txsoDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@SalesCD", cbSalesCD.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@siteDest", cbsiteDest.SelectedValue.ToString()));
            arr.Add(new cArrayList("@siteCD", hdsiteCD.Value));
            arr.Add(new cArrayList("@siteCDVan", hdsiteCDVan.Value));
            arr.Add(new cArrayList("@siteType", cbsiteType.SelectedValue.ToString()));
            arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@CustCD", hdcust_cd.Value));
            arr.Add(new cArrayList("@creditLimit",Convert.ToDecimal(txcreditLimit.Text)));
            arr.Add(new cArrayList("@pricelevel_cd", cbpricelevel_cd.SelectedValue));
            arr.Add(new cArrayList("@payment_term", cbpayment_term.SelectedValue));
            arr.Add(new cArrayList("@soDueDate", DateTime.ParseExact(txsoDueDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@reference", txreference.Text));
            arr.Add(new cArrayList("@referenceInv", txreferenceInv.Text));
            arr.Add(new cArrayList("@orderby", cborderby.SelectedValue));
            arr.Add(new cArrayList("@driver", cbdriver.SelectedValue));
            arr.Add(new cArrayList("@tranType", cbtranType.SelectedValue));
            arr.Add(new cArrayList("@soPO", txsoPO.Text));
            arr.Add(new cArrayList("@soPODate", txsoPODate.Text));
            bll.vUpdatetblSO(arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@soID", txKey.Text));
            bll.vBindingGridToSp(ref grdSO, "sp_tblSODtl_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@soID", txKey.Text));
            bll.vBindingGridToSp(ref grdProd, "sp_tblSODtlDisc_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@soID", txKey.Text));
            bll.vBindingGridToSp(ref grdDisc, "sp_tblSODtlDisc_get2", arr);
            
        
        }
    }
    //protected void cbitem_cd_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@prod_cd",hditem.Value.ToString()));
    //    txitem_SN.Text = bll.vLookUp("select item_nm from tmst_item where item_cd='" + cbitem_cd.SelectedValue.ToString() + "'");
        
    //}



    public void fillCustCDData()
    {
        if (hdcust_cd.Value!="")
        { 
        List<cArrayList> arr3 = new List<cArrayList>();
        arr3.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
        txaddr.Text = bll.vLookUp("select addr from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
        //txaddr2.Text = bll.vLookUp("select addr2 from tmst_customer where CustCD='" + cbCustomerCD.Text + "'");
        txcreditLimit.Text = bll.vLookUp("select credit_limit from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
        cbpricelevel_cd.SelectedValue = bll.vLookUp("select otlcd from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
        cbotlcd.SelectedValue = bll.vLookUp("select otlcd from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
        cbpayment_term.SelectedValue = bll.vLookUp("select payment_term from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
        cbSalesCD.SelectedValue = bll.vLookUp("select salesman_cd from tmst_customer where Cust_CD='" + hdcust_cd.Value + "'");
        DateTime dt = DateTime.ParseExact(txsoDocDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        dt = dt.AddDays(Convert.ToDouble(cbpayment_term.SelectedValue));
        txsoDueDate.Text = String.Format("{0:d/M/yyyy}", dt);
       // DateTime dt;
        //   dt = Convert.ToDateTime(String.Format("{0:M/d/yyyy}",txsoDocDate.Text)).AddDays(Convert.ToDouble(cbpayment_term.SelectedValue));
      //  txsoDueDate.Text = String.Format("{0:d/M/yyyy}", dt);
        }
    }
    //protected void cbsiteCD_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txsiteNm.Text = bll.vLookUp("select SITENM from tmst_site where SITECD='" + cbsiteCD.Text + "'");
    //}
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txKey.Text = Convert.ToString(Session["looSOSOID"]);
        cbSalesPointCD.SelectedValue = Convert.ToString(Session["looSOSalespointCD"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@SOID", txKey.Text));
        bll.vGetTblSO(arr, ref rs);
        while (rs.Read())
        {
            txSOCD.Text = rs["SOCD"].ToString();
            txInvoiceCD.Text = rs["InvoiceCD"].ToString();
            //txJournalTypeCD.Text = rs["JournalTypeCD"].ToString();
            //DateTime.ParseExact(DateTime.Now.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            txsoDocDate.Text = string.Format("{0:d/M/yyyy}", rs["soDocDate"]);
            txsoDate.Text = string.Format("{0:d/M/yyyy}", rs["soDate"]);
            cbSalesCD.SelectedValue = rs["emp_cd"].ToString();
            //txfullname.Text = rs["fullname"].ToString();
            //cbsiteDest.SelectedValue = rs["siteDest"].ToString();
            hdsiteCD.Value = rs["siteCD"].ToString();
            hdsiteCDVan.Value = rs["siteCDVan"].ToString();
            cbsiteType.SelectedValue = rs["siteType"].ToString();
            //txsiteNm.Text = rs["siteNm"].ToString();
            //txCurrencyID.Text = rs["CurrencyID"].ToString();
            cbSalesPointCD.SelectedValue = rs["SalesPointCD"].ToString();
            //txsalespoint_nm.Text = rs["salespoint_nm"].ToString();
            hdcust_cd.Value = rs["CustCD"].ToString();
            txsearchCust.Text = rs["cust_cd"].ToString() + " | " + rs["cust_nm"].ToString();
            txaddr.Text = rs["addr"].ToString();
            txcreditLimit.Text = Convert.ToDecimal(rs["creditLimit"].ToString()).ToString("#,##0"); 
            cbpricelevel_cd.SelectedValue = rs["pricelevel_cd"].ToString();
            cbpayment_term.SelectedValue = rs["payment_term"].ToString();
            txreference.Text = rs["reference"].ToString();
            txreferenceInv.Text = rs["referenceInv"].ToString();
            txsoDueDate.Text = string.Format("{0:d/M/yyyy}", rs["soDueDate"]);
            cborderby.SelectedValue = rs["orderby"].ToString();
            cbdriver.SelectedValue = rs["driver"].ToString();
            txstatus.Text = rs["statusSO"].ToString();
            cbotlcd.SelectedValue = rs["otlcd"].ToString();
            txsoPO.Text = rs["soPO"].ToString();
            txsoPODate.Text = string.Format("{0:d/M/yyyy}", rs["soPODate"]);
        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdSO, "sp_tblSODtl_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdProd, "sp_tblSODtlDisc_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdDisc, "sp_tblSODtlDisc_get2", arr);
        //int ntarget = int.Parse(bll.vLookUp("select isnull(sum(qty),0) from tblSalesTargetDet where slsTargetID='" + txslsTargetID.Text + "'"));
        //lbltotal.Text = string.Format("{0:#,#}", ntarget);
        //cbStatusID.Visible = true;
        string sSrc = Request.QueryString["src"].ToString();
        switch (sSrc)
        {
            case "SO":
                txsearchsiteCDVan.Visible = false;
                txsearchsiteCD.Visible = true;
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "tranType"));
                arr.Add(new cArrayList("@hiddendata", 0));
                bll.vBindingFieldValueToCombo(ref cbtranType, arr);
                break;
            case "Canvass":
                txsearchsiteCD.Visible = false;
                txsearchsiteCDVan.Visible = true;
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "tranType"));
                arr.Add(new cArrayList("@hiddendata", 1));
                bll.vBindingFieldValueToCombo(ref cbtranType, arr);
                break;
        }
        if ((txstatus.Text == "ENTRY") && (cbtranType.SelectedValue == "1")) 
        {
            buttonEnabled();
            //btprint.CssClass = "makeitreadonly";
            //btprint.Enabled = false;
            //btprintInv.CssClass = "makeitreadonly";
            //btprintInv.Enabled = false;
            //btprintInv.Visible = false;
            btprint.Visible = false;
            btprintInv.Visible = false;
            MakeReadOnly();
            UnvisibleInvoice();

            btAdd.CssClass = "makeitreadonly";
            btAdd.Enabled = false; 
        }

        else if ((txstatus.Text == "ENTRY") && (cbtranType.SelectedValue != "1")) 
        {
            buttonDisabled();
            btprint.Visible = true;
            btprint.CssClass = "button2 print";
            btprint.Enabled = true;
            btprintInv.Visible = false;
            MakeReadOnly();
            visibleInvoice();
            disableInput();
            txsopQuantityDelivery.ReadOnly = false;
            btEdit.CssClass = "button2 edit";
            btEdit.Enabled = true;
        }
        else if (txstatus.Text == "LOADING" )
        {
            buttonDisabled();
            btprint.Visible = false;
            btprintInv.CssClass = "button2 print";
            btprintInv.Enabled = true;
            btprintInv.Visible = true;
            MakeReadOnly();
            visibleInvoice();
            disableInput();
            txsopQuantityDelivery.ReadOnly = false;
            btEdit.CssClass = "button2 edit";
            btEdit.Enabled = true;
        }
        else 
        {
            buttonDisabled();
            MakeReadOnly();
            visibleInvoice();
            disableInput();
            btprint.Visible = false;
            btprintInv.Visible = false;
        }
           

    }
    protected void bttmp2_Click(object sender, EventArgs e)
    { 
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdDisc, "sp_tblSODtlDisc_get2", arr);
    }
    protected void txpriceLvlCD_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        string sSrc = Request.QueryString["src"].ToString();
        switch (sSrc)
        {
            case "SO":
                Response.Redirect("frmSO.aspx?src=SO");
                break;
            case "Canvass":
                Response.Redirect("frmSO.aspx?src=Canvass");
                break;
        }
        
        //cbStatusID.Visible = false;
        MakeReadWrite();
        buttonEnabled();
        btprint.CssClass = "makeitreadonly";
        btprint.Enabled = false;

    }
    protected void grdSO_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbsopSeqID = (Label)grdSO.Rows[e.RowIndex].FindControl("lbsopSeqID");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sopSeqID", lbsopSeqID.Text));
        bll.vDeleteTblSODet(arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdSO, "sp_tblSODtl_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdProd, "sp_tblSODtlDisc_get", arr);
        //int ntarget = int.Parse(bll.vLookUp("select isnull(sum(STDqty),0) from tblSalesTargetSPDet where slsTargetSPID='" + txslsTargetSPID.Text + "'"));
        //lbltotal.Text = string.Format("{0:#,#}", ntarget);
    }
    protected void grdSO_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblitem_cd = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblitem_cd");
        Label lblitem_nm = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblitem_nm");
        Label lblsopQuantity = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblsopQuantity");
        Label lblsopQuantityDelivery = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblsopQuantityDelivery");
        Label lblsopQuantityStock = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblsopQuantityStock");
        Label lblUnitCD = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblUnitCD");
        Label lblsopPrice = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblsopPrice");
        Label lblsopDiscount = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblsopDiscount");
        Label lblsopAmount = (Label)grdSO.Rows[e.NewSelectedIndex].FindControl("lblsopAmount");
        
        txsearchitem.Text = lblitem_cd.Text + " | " +lblitem_nm.Text;
        hditem.Value = lblitem_cd.Text;
        //txitem_SN.Text = lblitem_nm.Text;
        txsopQuantity.Text = lblsopQuantity.Text;
        txsopQuantityDelivery.Text = lblsopQuantityDelivery.Text;
        txsopQuantityStock.Text = lblsopQuantityStock.Text;
        cbUnitCD.SelectedValue = lblUnitCD.Text;
        txsopPrice.Text = lblsopPrice.Text;
        txsopDiscount.Text = lblsopDiscount.Text;
        txsopAmount.Text = lblsopAmount.Text;
        
    }
    protected void txsopPrice_TextChanged(object sender, EventArgs e)
    {
        txsopAmount.Text = Convert.ToString((Convert.ToDecimal(txsopPrice.Text) * Convert.ToDecimal(txsopQuantityDelivery.Text)) - Convert.ToDecimal(txsopDiscount.Text));
        pricelevel();
    }
    
    protected void txsopDiscount_TextChanged(object sender, EventArgs e)
    {
        txsopAmount.Text = Convert.ToString((Convert.ToDecimal(txsopPrice.Text) * Convert.ToDecimal(txsopQuantity.Text)) - Convert.ToDecimal(txsopDiscount.Text)); 
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SOID", txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        bll.vDeleteTblSO(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Deleted successfully !')", true);
        Response.Redirect("frmSO.aspx");
    }
    void MakeReadOnly()
    {
        txSOCD.CssClass = "makeitreadonly";
        txSOCD.ReadOnly = true;
       txInvoiceCD.CssClass = "makeitreadonly";
       txInvoiceCD.ReadOnly = true;
        txsoDocDate.CssClass = "makeitreadonly";
        txsoDocDate.ReadOnly = true;
        txsoDate.CssClass = "makeitreadonly";
        txsoDate.ReadOnly = true;
        cbSalesCD.CssClass = "makeitreadonly";
        cbSalesCD.Enabled = false;
        //cbsiteDest.CssClass = "makeitreadonly";
        //cbsiteDest.Enabled = false;
        txsearchsiteCD.CssClass = "makeitreadonly";
        txsearchsiteCD.ReadOnly = true;
        txsearchsiteCDVan.CssClass = "makeitreadonly";
        txsearchsiteCDVan.ReadOnly = true;
        cbsiteType.CssClass = "makeitreadonly";
        cbsiteType.Enabled = false;
        cbSalesPointCD.CssClass = "makeitreadonly";
        cbSalesPointCD.Enabled = false;
        txsearchCust.CssClass = "makeitreadonly";
        txsearchCust.ReadOnly = true;
        txaddr.CssClass = "makeitreadonly";
        txaddr.ReadOnly = true;
        txcreditLimit.CssClass = "makeitreadonly";
        txcreditLimit.ReadOnly = true;
        cbpricelevel_cd.CssClass = "makeitreadonly";
        cbpricelevel_cd.Enabled = false;
        cbpayment_term.CssClass = "makeitreadonly";
        cbpayment_term.Enabled = false;
        txsoDueDate.CssClass = "makeitreadonly";
        txsoDueDate.ReadOnly = true;
        txreference.CssClass = "makeitreadonly";
        txreference.ReadOnly = true;
        cborderby.CssClass = "makeitreadonly";
        cborderby.Enabled = false;
       txreferenceInv.CssClass = "makeitreadonly";
       txreferenceInv.ReadOnly = true;
       cbdriver.CssClass = "makeitreadonly";
       cbdriver.Enabled = false;
       cbtranType.CssClass = "makeitreadonly";
       cbtranType.Enabled = false;
       txsoPO.CssClass = "makeitreadonly";
       txsoPO.ReadOnly = true;
       txsoPODate.CssClass = "makeitreadonly";
       txsoPODate.ReadOnly = true;
    }
    void MakeReadWrite()
    {
        txSOCD.CssClass = "makeitreadonly";
        txSOCD.ReadOnly = true;
        txInvoiceCD.CssClass = "makeitreadonly";
        txInvoiceCD.ReadOnly = true;
        txsoDocDate.CssClass = "makeitreadwrite";
        txsoDocDate.ReadOnly = false;
        txsoDate.CssClass = "makeitreadwrite";
        txsoDate.ReadOnly = false;
        cbSalesCD.CssClass = "makeitreadwrite";
        cbSalesCD.Enabled = true;
        //cbsiteDest.CssClass = "makeitreadwrite";
        //cbsiteDest.Enabled = true;
        txsearchsiteCD.CssClass = "makeitreadwrite";
        txsearchsiteCD.ReadOnly = false;
        txsearchsiteCDVan.CssClass = "makeitreadwrite";
        txsearchsiteCDVan.ReadOnly = false;
        cbsiteType.CssClass = "makeitreadwrite";
        cbsiteType.Enabled = true;
        cbSalesPointCD.CssClass = "makeitreadwrite";
        cbSalesPointCD.Enabled = true;
        txsearchCust.CssClass = "makeitreadwrite";
        txsearchCust.ReadOnly = false;
        txaddr.CssClass = "makeitreadwrite";
        txaddr.ReadOnly = false;
        txcreditLimit.CssClass = "makeitreadwrite";
        txcreditLimit.ReadOnly = false;
        cbpricelevel_cd.CssClass = "makeitreadwrite";
        cbpricelevel_cd.Enabled = true;
        cbpayment_term.CssClass = "makeitreadwrite";
        cbpayment_term.Enabled = true;
        txsoDueDate.CssClass = "makeitreadwrite";
        txsoDueDate.ReadOnly = false;
        txreference.CssClass = "makeitreadwrite";
        txreference.ReadOnly = false;
        cborderby.CssClass = "makeitreadwrite";
        cborderby.Enabled = true;
        txreferenceInv.CssClass = "makeitreadwrite";
        txreferenceInv.ReadOnly = false;
        cbdriver.CssClass = "makeitreadwrite";
        cbdriver.Enabled = true;
        cbtranType.CssClass = "makeitreadwrite";
        cbtranType.Enabled = true;
        txsoPO.CssClass = "makeitreadwrite";
        txsoPO.ReadOnly = false;
        txsoPODate.CssClass = "makeitreadwrite";
        txsoPODate.ReadOnly = false;
    }
    void buttonDisabled()
    {
        btAdd.CssClass = "makeitreadonly";
        btAdd.Enabled = false;
        btEdit.CssClass = "makeitreadonly";
        btEdit.Enabled = false;
        btDelete.CssClass = "makeitreadonly";
        btDelete.Enabled = false;
        btsave.CssClass = "makeitreadonly";
        btsave.Enabled = false;
        btprint.CssClass = "makeitreadonly";
        btprint.Enabled = false;
        btprintInv.CssClass = "makeitreadonly";
        btprintInv.Enabled = false;
        
    }
    void buttonEnabled ()
    {
        btAdd.CssClass = "button2 add";
        btAdd.Enabled = true;
        btEdit.CssClass = "button2 edit";
        btEdit.Enabled = true;
        btDelete.CssClass = "button2 delete";
        btDelete.Enabled = true;
        btsave.CssClass = "button2 save";
        btsave.Enabled = true;
        btprint.CssClass = "button2 print";
        btprint.Enabled = true;
        btprintInv.CssClass = "button2 print";
        btprintInv.Enabled = true;
    }
    void clearAdd()
    {
        txsearchitem.Text = "";
        txsopQuantity.Text = "0";
        txsopQuantityDelivery.Text = "0";
        txsopQuantityStock.Text = "0";
        txsopPrice.Text = "0";
        txsopDiscount.Text = "0";
        txsopAmount.Text = "0";
    }
    void visibleInvoice()
    {
        lbInvoiceCD.Visible = true;
        txInvoiceCD.Visible = true;
        lbreferenceInv.Visible = true;
        txreferenceInv.Visible = true;
        lbDriver.Visible = true;
        cbdriver.Visible = true;

    }
    void UnvisibleInvoice()
    {
        lbInvoiceCD.Visible = false;
        txInvoiceCD.Visible = false;
        lbreferenceInv.Visible = false;
        txreferenceInv.Visible = false;
        lbDriver.Visible = false;
        cbdriver.Visible = false;

    }
    void enabledInput()
    {
        txsearchitem.Enabled = true;
        cbUnitCD.Enabled = true;
        txsopQuantity.Enabled = true;
        txsopQuantityDelivery.Enabled = true;
        txsopQuantityStock.Enabled = true;
        txsopPrice.Enabled = true;
        txsopDiscount.Enabled = true;
        txsopAmount.Enabled = true;
        btAdd.Enabled = true;
    }
    void disableInput()
    {
        txsearchitem.Enabled = false;
        cbUnitCD.Enabled = false;
        txsopQuantity.Enabled = false;
        txsopQuantityDelivery.Enabled = false;
        txsopQuantityStock.Enabled = false;
        txsopPrice.Enabled = false;
        txsopDiscount.Enabled = false;
        txsopAmount.Enabled = false;
        btAdd.Enabled = false;
    }
    protected void btEdit_Click(object sender, EventArgs e)
    {

        if (txstatus.Text == "LOADING" )
        {
            MakeReadOnly();
            visibleInvoice();
            disableInput();
            txreferenceInv.CssClass = "makeitreadwrite";
            txreferenceInv.ReadOnly = false;
            txsopQuantityDelivery.CssClass = "makeitreadwrite";
            txsopQuantityDelivery.ReadOnly = false;
            txsopQuantityDelivery.Enabled = true;
            cbdriver.CssClass = "makeitreadwrite";
            cbdriver.Enabled = true;
            btAdd.CssClass = "button2 add";
            btAdd.Enabled = true;
            btsave.CssClass = "button2 save";
            btsave.Enabled = true;

        }
        else
        {
            MakeReadWrite();
            txSOCD.CssClass = "makeitreadonly";
            txSOCD.ReadOnly = true;
            btAdd.CssClass = "button2 add";
            btAdd.Enabled = true;
        }
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
    public string GetValue()
    {
        return (txsearchitem.Text);
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
        fillCustCDData();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@varTransName", "SO"));
        arr.Add(new cArrayList("@bigID", txKey.Text));
        arr.Add(new cArrayList("@sinAftStatusID", "1"));
        bll.vUpdTransStatus(arr);
        txstatus.Text = bll.vLookUp("select staName from tblstatus where StatusID=1");
        lbDriver.Visible = true;
        cbdriver.Visible = true;
        lbInvoiceCD.Visible = true;
        txInvoiceCD.Visible = true;
        lbreferenceInv.Visible = true;
        txreferenceInv.Visible = true;
        List<string> arr2 = new List<string>();
        //List<cArrayList> arr2 = new List<cArrayList>();
        //arr2.Add(new cArrayList("@bigID", txKey.Text));
        //arr2.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr2.Add("{tblso.SOCD} = '" + txSOCD.Text + "'");
        Session["lformulaSO"] = arr2;
        Response.Redirect("fm_report.aspx?src=so");
    }


    protected void txsoDocDate_TextChanged(object sender, EventArgs e)
    {
        txsoDate.Text = txsoDocDate.Text;
        txsoDueDate.Text= txsoDocDate.Text;
    }
    protected void btprintInv_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@varTransName", "SO"));
        arr.Add(new cArrayList("@bigID", txKey.Text));
        arr.Add(new cArrayList("@sinAftStatusID", "2"));
        bll.vUpdTransStatus(arr);
        txstatus.Text = bll.vLookUp("select staName from tblstatus where StatusID=2");

        List<string> arr2 = new List<string>();
        arr2.Add("{tblso.InvoiceCD} = '" + txInvoiceCD.Text + "'");
        Session["lformulaInvoice"] = arr2;
        Response.Redirect("fm_report.aspx?src=Invoice");
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
    
    protected void txsopQuantityDelivery_TextChanged(object sender, EventArgs e)
    {
        qtyship();
    }
    private void pricelevel()
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@cust_cd", hdcust_cd.Value));
        arr.Add(new cArrayList("@item_cd", hditem.Value));
        bll.vGetpricelevel(arr, ref rs);
        while (rs.Read())
        {
            txsopPrice.Text = rs["unitprice"].ToString();
        } rs.Close();
    }
    protected void txSOCD_TextChanged(object sender, EventArgs e)
    {
        string noo = bll.vLookUp("select soid from tblso where socd='" + txSOCD.Text + "' AND SalespointCD='" + Request.Cookies["sp"].Value+"'");
        
        Session["looSOSOID"] = noo;
        Session["looSOSalespointCD"] = Request.Cookies["sp"].Value;
        bttmp_Click(sender,  e);
    }
    protected void txreferenceInv_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btDisc_Click(object sender, EventArgs e)
    {

    }
    protected void grdProd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindow2", "openwindow2()", true);
        Label lbprod_cd = (Label)grdProd.Rows[e.NewSelectedIndex].FindControl("lbprod_cd");
        Label lbqty = (Label)grdProd.Rows[e.NewSelectedIndex].FindControl("lbqty");
       
        Session["looDiscCust_cd"] = hdcust_cd.Value;
        Session["looDiscSoDocDate"] = DateTime.ParseExact(txsoDocDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        Session["looDiscProd_cd"] = lbprod_cd.Text;
        Session["looDiscQty"] = lbqty.Text;
        Session["looDiscSalespointCD"] = cbSalesPointCD.SelectedValue;
        Session["looDiscSOID"] = txKey.Text;
        
        
    }
    protected void btDisCal_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SOID", txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        bll.vInserttblSOdtlDiscCH(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Automatic Discount Updated !')", true);
    }
    protected void grdDisc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbSeqID = (Label)grdDisc.Rows[e.RowIndex].FindControl("lbSeqID");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SeqID", lbSeqID.Text));
        bll.vDeletetblSODtlDisc(arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@soID", txKey.Text));
        bll.vBindingGridToSp(ref grdDisc, "sp_tblSODtlDisc_get2", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Delete successfully !')", true);
    }
    protected void txsopQuantity_TextChanged(object sender, EventArgs e)
    {
        qtyship();
    }
    protected void qtyship()
    {
        string sSrc = Request.QueryString["src"].ToString();
        string stock_display = "0";

        switch (sSrc)
        {
            case "SO":
                stock_display = bll.vLookUp("select tmst_stock.stock_display from tmst_stock where item_cd='" + hditem.Value + "' and whs_cd='" + hdsiteCD.Value + "' and bin_cd='" + cbsiteType.SelectedValue + "'");
                break;
            case "Canvass":
                stock_display = bll.vLookUp("select tmst_van_stock.stock_display from tmst_van_stock where item_cd='" + hditem.Value + "' and whs_cd='" + hdsiteCDVan.Value + "' and bin_cd='" + cbsiteType.SelectedValue + "'");
                break;
        }
     if (stock_display=="")  
     {
         stock_display="0";
     }

        if (Convert.ToDouble(stock_display) > Convert.ToDouble(txsopQuantity.Text))
        {
            txsopQuantityDelivery.Text = txsopQuantity.Text;
        }
        else
        {
            txsopQuantityDelivery.Text = stock_display.ToString();
        }
        pricelevel();
        txsopAmount.Text = Convert.ToString((Convert.ToDecimal(txsopPrice.Text) * Convert.ToDecimal(txsopQuantityDelivery.Text)) - Convert.ToDecimal(txsopDiscount.Text));
        
    }
}

