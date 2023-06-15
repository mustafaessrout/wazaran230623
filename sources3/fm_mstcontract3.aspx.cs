using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstcontract3 : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dQtyOrder = 0;
    double dSubTotal = 0;
    double dTotShipment = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelWrkSalesOrderDtl(arr);
                arr.Clear();
                string sSystemDate = Request.Cookies["waz_dt"].Value.ToString();
                lbsalespoint.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='so_sta_id' and fld_valu='N'");
                dtorder.Text = Request.Cookies["waz_dt"].Value.ToString();
                dtdelivery.Text = Request.Cookies["waz_dt"].Value.ToString();
                vNew();
                
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                cbwhs_SelectedIndexChanged(sender, e);
                string sDefBin = bll.vLookUp("select top 1 qry_data from tmap_query where qry_cd='bin_branch'");
                cbbin.SelectedValue = sDefBin;

                BindEmployeConditional();
                bll.vBindingComboToSp(ref cbdisctyp, "sp_tpromotion_dtl_get", "promo_typ", "promotyp_nm");
                bll.vBindingFieldValueToCombo(ref cbuom, "uom", false);
                vPercentage.Visible = false;
                vTotalQty.Visible = false;
                vItem.Visible = false;
                cbtype_SelectedIndexChanged(sender, e);
                txqty.Attributes.Add("onblur", "ShowProgress();SetDeliver()");
                txcustomer.Focus();
                btsave.Visible = false;
                btprint.Visible = false;
                btprintinvoice.Visible = false;
                hdto.Value = "";

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    void vNew()
    {
        //txcustomer.CssClass = "form-control input-group-sm";
        ////txinvoiceno.Enabled = true;
        ////txinvoiceno.CssClass = "makeitreadwrite";
        //txitemsearch.CssClass = "form-control input-sm";
        //txmanualinv.Enabled = true;
        //txmanualinv.CssClass = "makeitreadwrite";
        txorderno.CssClass = "form-control  ro";
        //txqty.CssClass = "form-control input-sm  ";
        //txremark.CssClass = "form-control ";
        // txshipmen.Enabled = true;
        // txshipmen.CssClass = "makeitreadwrite";
        //txstockcust.CssClass = "form-control input-sm ";
        dtorder.CssClass = "form-control  ro";
        //cbsalesman.CssClass = "form-control ";
        //cbbin.CssClass = "form-control ";
        //2-JAN-2016
        //cbdriver.CssClass = "form-control  ro";
        //cbuom.CssClass = "form-control input-sm";
    }

    void BindEmployeConditional()
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            if (cbbin.SelectedValue == "FA")
            {
                arr.Add(new cArrayList("@qry_cd", "rentTransport"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            }
            else
            {
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
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
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString() + " - " + rs["cust_arabic"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        HttpCookie otlcd;
        otlcd = HttpContext.Current.Request.Cookies["otlcd"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        arr.Add(new cArrayList("@item_cd", prefixText));
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "|" + rs["item_nm"].ToString() + "|" + rs["size"].ToString() + "|" + rs["branded_nm"].ToString() + "| Item Block For Pcs -" + " No ", rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void btsearchso_ServerClick(object sender, EventArgs e)
    {

    }

    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            arr.Add(new cArrayList("@qry_cd", "showbinorder"));
            bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_getbytype", "bin_cd", "bin_nm", arr);
            cbwhs.CssClass = cd.csstextro;
            cbwhs.Enabled = false;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dtdelivery_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sSystemDate = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtSystemDate = DateTime.ParseExact(sSystemDate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime last_date = new DateTime(dtSystemDate.Year, dtSystemDate.Month, DateTime.DaysInMonth(dtSystemDate.Year, dtSystemDate.Month));
            DateTime cdtdelivery = DateTime.ParseExact(dtdelivery.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (cdtdelivery < dtSystemDate)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Delivery date can not less then system date!','Delivery Date','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                dtdelivery.Text = dtSystemDate.Day + "/" + dtSystemDate.Month + "/" + dtSystemDate.Year;
                return;
            }
            else if (cdtdelivery > last_date)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Delivery date can not greater then end of month!','Delivery Date','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                dtdelivery.Text = last_date.Day + "/" + last_date.Month + "/" + last_date.Year;
                return;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sSalesmanCode = string.Empty;
            if (txcustomer.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Alert','Please type for search customer ! | يرجى كتابة العملاء','warning');", true);
                return;
            }            
                      
            // Enh : 22 June 2019 : Customer Transfer Blocked - CIN
            string sCustomerTransferBlock = bll.vLookUp("select dbo.fn_customertransferpending('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sCustomerTransferBlock != "ok")
            {
                hdcust.Value = ""; txcustomer.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused there is pending in customer transfer ','" + sCustomerTransferBlock + "','warning');", true);
                return;
            }

            hdcust_otlcd.Value = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            Response.Cookies["otlcd"].Value = hdcust_otlcd.Value;

            //----------------------------------------------
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            cbsalesman_SelectedIndexChanged(sender, e);
            System.Data.SqlClient.SqlDataReader rs = null;
            arr.Clear();
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstCustomer(arr, ref rs);
            while (rs.Read())
            {
                lbaddress.Text = rs["addr"].ToString();
                var culture = System.Globalization.CultureInfo.GetCultureInfo("id-ID");
                lbcusttype.Text = rs["otlnm"].ToString();
                lbcustgroup.Text = rs["cusgrnm"].ToString();
                lbterm.Text = rs["term"].ToString();
                sSalesmanCode = rs["salesman_cd"].ToString();
                lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
                txcustomer.CssClass = "form-control input-group-sm ro";
                lbcredittype.Text = rs["cuscate_cd"].ToString();
                string sCustCate = rs["cuscate_cd"].ToString();
                lbterm.Text = rs["payment_term"].ToString();
                lbvatno.Text = (rs["tax_no"].Equals(DBNull.Value) ? "N/A" : rs["tax_no"].ToString());
                lblVatArabic.Text = (rs["vat_custarabic"].Equals(DBNull.Value) ? "N/A" : rs["vat_custarabic"].ToString());
                lblVatEnglish.Text = (rs["vat_custname"].Equals(DBNull.Value) ? "N/A" : rs["vat_custname"].ToString());

            }
            rs.Close();
            cbsalesman.SelectedValue = sSalesmanCode;
            cbsalesman_SelectedIndexChanged(sender, e);
            cbsalesman.CssClass = "form-control  ro";
            cbsalesman.Enabled = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            //return;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcustomer_AutoCompleteExtender.ContextKey = cbsalesman.SelectedValue.ToString();
    }

    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbtype.SelectedValue == "PC")
            {
                vPercentage.Visible = true;
                dtstartsales.CssClass = "form-control ";
                dtendsales.CssClass = "form-control ";
            }
            else
            {
                vPercentage.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btcalc_Click(object sender, EventArgs e)
    {
        try
        {
            string avbInvoice = "", dtStart = "", dtEnd = "", actualSales = "";
            double sales = 0, pct = 0, free = 0;

            if (txcustomer.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Alert','Please type for search customer ! | يرجى كتابة العملاء','warning');", true);
                return;
            }

            if (dtstartsales.Text == "" || dtendsales.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Alert','Pleas Select Start and End Date ! ','warning');", true);
                return;
            }

            DateTime dtStartDate = DateTime.ParseExact(dtstartsales.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtEndDate = DateTime.ParseExact(dtendsales.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtStart = dtStartDate.Year + "-" + dtStartDate.Month + "-" + dtStartDate.Day;
            dtEnd = dtEndDate.Year + "-" + dtEndDate.Month + "-" + dtEndDate.Day;

            avbInvoice = bll.vLookUp("select a.contract_no from tmst_contract a inner join tcontract_customer b on a.contract_no=b.contract_no where b.cust_cd='"+hdcust.Value.ToString()+"' and a.start_dt='"+ dtStart +"' and a.end_dt='"+ dtEnd +"' and a.approval not in ('N','E')");

            if (avbInvoice != "")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Alert','This Customer already get free/bonus at this period !','warning');", true);
                //return;
            }

            actualSales = bll.vLookUp("exec sp_sum_actualsales_date '" + Request.Cookies["sp"].Value.ToString() + "','" + hdcust.Value.ToString() + "','"+dtStart+"','"+dtEnd+"'");

            sales = Convert.ToDouble(actualSales);
            pct = Convert.ToDouble(txpercentage.Text);
            //free = Math.Round((sales * pct / 100), 0, MidpointRounding.AwayFromZero); 
            free = (sales * pct / 100);

            dtstartsales.CssClass = "form-control  ro";
            dtendsales.CssClass = "form-control  ro";
            vTotalQty.Visible = true;
            txtotalqty.Text = free.ToString();
            hdtotalqty.Value = free.ToString();
            txtotalsales.Text = actualSales;
            vItem.Visible = true;
            txtotalqty.CssClass = "form-control ";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void txitemsearch_TextChanged(object sender, EventArgs e)
    {
        lbprice.Text = "0";
        lbstock.Text = "0";
    }

    protected void btreset_Click(object sender, EventArgs e)
    {
        cbuom.SelectedValue = "CTN";
        cbuom_SelectedIndexChanged(sender, e);
        //txqty.Focus();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }

    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item not yet selected!','Choose Item','warning');", true);
                lbprice.Text = "";
                lbstock.Text = "";
                return;
            }
            int cnt = 0;
            DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string day = Convert.ToString(dtOrd.Day);
            string mon = Convert.ToString(dtOrd.Month);
            string yrs = Convert.ToString(dtOrd.Year);
            string date = yrs + "-" + mon + "-" + day;
            cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_item where prod_cd in (select prod_cd from tmst_itemCustomer_Block  where otlcd ='" + hdcust_otlcd.Value.ToString() + "' and  end_dt > '" + date + "'  ) and item_cd = '" + hditem.Value.ToString() + "'  and item_sta_id = 'A'"));
            if (cnt == 1 && cbuom.SelectedValue.ToString() == "PCS")
            {
                lblItemBlock.Text = "Yes";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Item can not sales for this customer in pcs !','warning');", true);
                return;
            }
            else
            {
                lblItemBlock.Text = "No";
            }
            btprice_Click(sender, e);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btprice_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdcust.Value.Equals(DBNull.Value) || hdcust.Value == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Price can not determined because customer not yet selected !','warning');", true);
                txitemsearch.Text = "";
                hditem.Value = "";
                return;
            }
            int cnt = 0;
            DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string day = Convert.ToString(dtOrd.Day);
            string mon = Convert.ToString(dtOrd.Month);
            string yrs = Convert.ToString(dtOrd.Year);
            string date = yrs + "-" + mon + "-" + day;

            cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_item where prod_cd in (select prod_cd from tmst_itemCustomer_Block  where otlcd ='" + hdcust_otlcd.Value.ToString() + "' and  end_dt > '" + date + "'  ) and item_cd = '" + hditem.Value.ToString() + "'  and item_sta_id = 'A'"));
            if (cnt == 1 && cbuom.SelectedValue.ToString() == "PCS")
            {
                lblItemBlock.Text = "Yes";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Item can not sales for this customer in pcs, Contact Faruk','warning');", true);
                return;
            }
            else { lblItemBlock.Text = "No"; }
            double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + hditem.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
            if (dConv == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no price setup or no setup UOM conversion!','Contact to wazaran admin','warning');", true);
                lbstock.Text = "";
                lbprice.Text = "";
                cbuom.SelectedValue = "";
                return;
            }
            string sItemBlock = bll.vLookUp("select dbo.fn_checkitemblock('" + hdcust.Value.ToString() + "','" + hditem.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sItemBlock != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item:" + hditem.Value.ToString() + "','Blocked for customer : " + hdcust.Value.ToString() + "','warning');", true);
                return;
            }



            //Check Adjustment Price
            double dPrice = 0;
            string sCustType = "";
            sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            double dAdjust = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')"));
            if (dAdjust > 0)
            {
                dPrice = dAdjust;
            }
            else
            {

                dPrice = bll.dGetItemPrice(hditem.Value.ToString(), sCustType, cbuom.SelectedValue.ToString());
            }
            if (dPrice == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price not yet setup or item conversion not setup!','Contact wazaran admin','warning');", true);
                lbstock.Text = "";
                lbprice.Text = "";
                cbuom.SelectedValue = "";
                //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "HideProgress();", true);
                return;
            }
            lbprice.Text = dPrice.ToString();
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sStock = "0";
            sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");

            if (sStock == null || sStock == "") { sStock = "0"; }

            string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
            string sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' union all select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')a");
            if (sStocktwrk == "") { sStocktwrk = "0"; }
            lbstock.Text = ((Convert.ToDouble(sStock) - Convert.ToDouble(sStocktwrk)) * Convert.ToDouble(sQTY)).ToString();
            if (txqty.Text != "")
            {
                if (Convert.ToDouble(lbstock.Text) > Convert.ToDouble(txqty.Text))
                {
                    txshipmen.Text = txqty.Text;
                }
                else
                {
                    txshipmen.Text = lbstock.Text;
                }
            }
            //txqty.Focus();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {

            double dresult; SqlDataReader rs = null;
            int cnt = 0;

            DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string day = Convert.ToString(dtOrd.Day);
            string mon = Convert.ToString(dtOrd.Month);
            string yrs = Convert.ToString(dtOrd.Year);
            string date = yrs + "-" + mon + "-" + day;
            //cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_item where prod_cd in (select prod_cd from tmst_itemCustomer_Block  where otlcd ='" + hdcust_otlcd.Value.ToString() + "' and  end_dt > '" + date + "'  ) and item_cd = '" + hditem.Value.ToString() + "'  and item_sta_id = 'A'"));
            //if (cnt == 1 && cbuom.SelectedValue.ToString() == "PCS")
            //{
            //    lblItemBlock.Text = "Yes";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Item can not sales for this customer in pcs, Contact Faruk','warning');", true);
            //    return;
            //}
            //else { lblItemBlock.Text = "No"; }
            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Search','Please select item  !','warning');", true);
                return;
            }
            if (!double.TryParse(lbstock.Text, out dresult))
            {
                //This changed by IA : Only Wholesaler can bypass stock 
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There no stock available, but you can still make data entry','Please fill warehouse/van stock !','warning');", true);
                return;
            }
            if (dresult < 0) { dresult = 0; }
            if (dresult <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There no stock available','Please do stock in or make shipment zero from other TO !','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            if (!double.TryParse(txqty.Text, out dresult))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Please fill Order Quantity !','warning');", true);
                return;
            }
            if (dresult == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Order qty can not be 0','Qty Order','warning');", true);
                return;
            }
            if (cbuom.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('UOM is not yet selected','Please select UOM !','warning');", true);
                return;
            }

            double totshipment = 0;
            double nQtyOrder = Convert.ToDouble(txtotalqty.Text);
            if (grd.Rows.Count > 0) { totshipment = Convert.ToDouble((grd.FooterRow.FindControl("lbtotshipment") as Label).Text); }
            else { totshipment = Convert.ToDouble(txshipmen.Text); }
            
            if (totshipment > nQtyOrder)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Total Qty Free/Bonus Deliver','is "+txtotalqty.Text+" CTN','warning');", true);
                return;
            }
            if (txshipmen.Text == "")
            { txshipmen.Text = "0"; }
            if (Convert.ToDouble(txqty.Text) < Convert.ToDouble(txshipmen.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Shipment qty can not bigger than order !','Shipment must less or same with Order','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@stock_cust", "0"));
            arr.Add(new cArrayList("@unitprice", lbprice.Text));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            arr.Add(new cArrayList("@stock_amt", lbstock.Text));
            arr.Add(new cArrayList("@qty_shipment", txshipmen.Text));
            bll.vInsertWrkSalesORderDtl(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
            arr.Clear();
            lbprice.Text = "";
            txitemsearch.Text = "";
            txshipmen.Text = "";
            txqty.Text = "";
            lbstock.Text = "";
            lblItemBlock.Text = "";
            cbuom.SelectedValue = "";
            string sTotVat = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (grd.Rows.Count > 0)
            {
                Label lbtotvat = (Label)grd.FooterRow.FindControl("lbtotvat");
                lbtotvat.Text = sTotVat;
            }
            string sVAT = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //lbvat.Text = sVAT;
            btsave.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDeleteWrkSalesOrderDtl(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
            bll.vDelSalesOrderFreeItem(arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            Label lbitemcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitemcode");
            Label lbitemname = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitemname");
            Label lbqtyorder = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbqtyorder");
            Label lbstockcust = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbstockcust");
            Label lbstockamt = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbstockamt");
            Label lbtotshipment = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbshipment");
            Label lbprice0 = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbprice0");
            Label lbuom = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbuom");
            Label lbsubtotal1 = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbsubtotal");
            hditem.Value = lbitemcode.Text; txitemsearch.Text = lbitemname.Text;
            txitemsearch.CssClass = "form-control input-sm";
            txitemsearch.Enabled = true;
            txqty.Text = lbqtyorder.Text;
            txqty.CssClass = cd.csstext;
            DateTime ddate = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            double dstock = 0;
            if (Convert.ToDouble(lbstockamt.Text) < 0) { lbstockamt.Text = "0"; }
            dstock = Convert.ToDouble(lbstockamt.Text) + Convert.ToDouble(bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')"));
            lbstock.Text = dstock.ToString();
            txshipmen.Text = lbtotshipment.Text;
            txshipmen.CssClass = cd.csstextro;
            lbprice.Text = lbprice0.Text;
            cbuom.SelectedValue = lbuom.Text;
            cbuom.CssClass = "form-control input-sm";
            cbuom.Enabled = true;
            txshipmen.CssClass = "form-control input-sm ";
            txshipmen.Enabled = true;
            // txqty.CssClass = "makeitreadwrite";
            //btnew.Visible = true;
            //btnew.CssClass = "btn btn-primary ";
            //btsave.Visible = false;
            //btsave.CssClass = "divhid";
            //btprint.Visible = false;
            //btprint.CssClass = "divhid";
            //btprintfreeinv.Visible = false;
            //btprintfreeinv.CssClass = "divhid";
            //btprintinvoice.Visible = false;
            //btprintinvoice.CssClass = "divhid";
            //lbsubtotal.Text = lbsubtotal1.Text;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbqtyorder = (Label)e.Row.FindControl("lbqtyorder");
                Label lbsubtotal = (Label)e.Row.FindControl("lbsubtotal");
                Label lbshipment = (Label)e.Row.FindControl("lbshipment");
                Label lbdisc = (Label)e.Row.FindControl("lbdisc");
                Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
                dQtyOrder += Convert.ToDouble(lbqtyorder.Text);
                dSubTotal += Convert.ToDouble(lbsubtotal.Text);
                dTotShipment += Convert.ToDouble(lbshipment.Text);
                lbsubtotal.Text = "0.00";
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotqtyorder = (Label)e.Row.FindControl("lbtotqtyorder");
                Label lbtotsubtotal = (Label)e.Row.FindControl("lbtotsubtotal");
                Label lbtotshipment = (Label)e.Row.FindControl("lbtotshipment");
                Label lbtotdiscount = (Label)e.Row.FindControl("lbtotdiscount");
                lbtotqtyorder.Text = dQtyOrder.ToString();
                lbtotsubtotal.Text = dSubTotal.ToString();
                lbtotshipment.Text = dTotShipment.ToString();
                lbtotsubtotal.Text = "0.00";
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string sTermCd = "";
            string sSoNo = ""; string sCLMsg = string.Empty;

            if (hdcust.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer must selected!','Customer has not selected','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            if (dtorder.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Order date has not select !','Please order date !','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            if (dtdelivery.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Delivery date has not select !','Delivery date !','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Enter Item to be free/bonus !','free/bonus item can not empty ','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            string scd = bll.vLookUp("select dbo.fn_checkdeadlinetime('" + Request.Cookies["sp"].Value.ToString() + "')");
            if (scd != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Block entry because deadline to daily closing !','Please daily closing !','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            
            btsave.Visible = false;
            btprint.Visible = true;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@contract_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@type", cbtype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@rditem", "P"));
            arr.Add(new cArrayList("@rdfreeitem", "I"));
            arr.Add(new cArrayList("@rdcust", "C"));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstartsales.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtendsales.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@contract_term", "1"));
            arr.Add(new cArrayList("@contract_payment", "IB"));
            arr.Add(new cArrayList("@totqty", txtotalqty.Text));
            arr.Add(new cArrayList("@totamt", "0"));
            arr.Add(new cArrayList("@created_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@approval", "N"));
            arr.Add(new cArrayList("@deleted", "0"));
            arr.Add(new cArrayList("@remark", txremark.Text));
            if (hdto.Value.ToString() == "") //New data 
            {
                bll.vInsertMstContractOrder(arr, ref sSoNo);
            }
            else
            {   //-------------------------------------This is delete from previouse transaction
                sSoNo = hdto.Value.ToString();
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelSalesorderDtl(arr);
                bll.vDelSalesorderShipment(arr);
                bll.vDelDosalesDtl(arr);
                //-------------------------------------------------------------------------------
            }
            txorderno.Text = sSoNo;
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
            arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
            bll.vInsertContractOrderDtl(arr);


            
            dtdelivery.CssClass = cd.csstextro;
            txmanualno.CssClass = cd.csstextro;
            txitemsearch.CssClass = cd.csstextro;
            txqty.CssClass = cd.csstextro;
            hditem.Value = "";
            cbuom.CssClass = cd.csstextro;
            txshipmen.CssClass = cd.csstextro;
            grd.Columns[7].Visible = false;
            btprint.CssClass = "btn btn-info";
            arr.Clear();
            cbdriver.CssClass = cd.csstext;
            txmanualinv.CssClass = cd.csstext;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Order Free/Bonus has been saved', 'Order Free/Bonus no : " + sSoNo + "', 'success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("newto", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','New Order Free/Bonus','warning');", true);
            return;
        }
        // hdto.Value = "";
        Response.Redirect("fm_mstcontract3.aspx");
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();

            if (bll.nCheckAccess("loadto", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Print Loading Order Free/Bonus !!','warning');", true);
                return;
            }


            //return;

            if (txmanualno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Loading manual no can not empty','Manual No. must be filled before print !!','warning');", true);
                return;
            }
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('order free/bonus has not yet selected','Please select SO will be delivered !','warning');", true);
                return;
            }
            PrintLoading();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void PrintLoading()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (txmanualno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Loading manual no can not empty','Manual No. must be filled before print !!','warning');", true);
                return;
            }
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('order free/bonus has not yet selected','Please select SO will be delivered !','warning');", true);
                return;
            }

            string sSystemDate = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtSystemDate = DateTime.ParseExact(sSystemDate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime cdtdelivery = DateTime.ParseExact(dtdelivery.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (cdtdelivery != dtSystemDate)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Delivery date should be same with system date','Please check delivery date !!','warning');", true);
                return;
            }

            arr.Clear();
            arr.Add(new cArrayList("@so_cd", txorderno.Text));
            arr.Add(new cArrayList("@approval", "L"));
            arr.Add(new cArrayList("@manual_no", txmanualno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateMstContractOrder(arr);
            arr.Clear();
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='so_sta_id' and fld_valu='L'");
            btsave.Visible = false;
            btprint.Visible = false;
            btprintinvoice.Visible = true;
            btprintinvoice.CssClass = "btn btn-default ";
            btprint.CssClass = "divhid";
            txmanualno.CssClass = "form-control  ro";
            txmanualno.Enabled = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=sofree&so=" + txorderno.Text + "','fm_report2.aspx?src=so1free&so=" + txorderno.Text + "');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void PrintInvoice()
    {
        try
        {

            if (bll.nCheckAccess("invto", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Print invoice Order Free/Bonus !!','warning');", true);
                return;
            }
            if (txmanualinv.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual No','Please fill manual invoice !!','warning');", true);
                return;
            }
            

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", txorderno.Text));
            arr.Add(new cArrayList("@approval", "DR"));
            arr.Add(new cArrayList("@manual_no", txmanualinv.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@year", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year));
            arr.Add(new cArrayList("@month", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month));
            bll.vUpdateMstContractInvoiceOrder(arr);

            arr.Clear();
            btprintinvoice.Visible = false;
            btprintinvoice.CssClass = "divhid";
            btprint.CssClass = "divhid";
            btsave.CssClass = "divhid";
            btnew.CssClass = "btn btn-success ";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=invtofree&no=" + txorderno.Text + "&amt=0','fm_report2.aspx?src=invto1free&no=" + txorderno.Text + "&amt=0');", true);

            
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcontract3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btprintinvoice_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("invto", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Print invoice Order Free/Bonus !!','warning');", true);
            return;
        }
        return;
    }

    protected void btprintloading_Click(object sender, EventArgs e)
    {
        PrintLoading();
    }

    protected void btprintinvoice2_Click(object sender, EventArgs e)
    {
        PrintInvoice();
    }

    protected void txtotalqty_TextChanged(object sender, EventArgs e)
    {
        double dOutQty = 0, dOutLimit = 0;
        dOutLimit = double.Parse(hdtotalqty.Value.ToString());

        if (!double.TryParse(txtotalqty.Text, out dOutQty))
        {
            txtotalqty.Text = dOutLimit.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty Free must be numeric','Check Qty','warning');", true);
            return;
        }

        if (dOutQty > dOutLimit || dOutQty < 0)
        {
            txtotalqty.Text = dOutLimit.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty Free must less than : "+dOutLimit.ToString()+"','Check Qty','warning');", true);
            return;
        }
    }
}