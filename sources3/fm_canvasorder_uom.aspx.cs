using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_canvasorder_uom : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dQtyOrder = 0;
    double dSubTotal = 0;
    double dTotShipment = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        if (!IsPostBack)
        {
            try
            {
                string sSalespointtype = bll.vLookUp("select salespoint_typ from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                if (sSalespointtype == "HO")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You have no authorize to create Canvas','You are from Head Office','warning');", true);
                    System.Threading.Thread.Sleep(3000);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rd", "history.back(-1);", true);
                    return;
                }
                List<cArrayList> arr = new List<cArrayList>();
                // Delete al twrk_xx table 
                vDeleteAllWrk();
                bll.vBindingFieldValueToCombo(ref cbuom, "uom", false);
                arr.Clear();
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                cbsalesman_SelectedIndexChanged(sender, e);
                //  txqty.Attributes.Add("onblur", "SetDeliver()");
                dtorder.Text = Request.Cookies["waz_dt"].Value.ToString();
                lbsalespoint.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='so_sta_id' and fld_valu='N'");
                bll.vBindingFieldValueToCombo(ref cbsourceorder, "source_order");
                string sDefBin = bll.vLookUp("select top 1 qry_data from tmap_query where qry_cd='bin_branch'");
                cbbin.SelectedValue = sDefBin;
                bll.vBindingFieldValueToCombo(ref cbsourceorder, "source_order");
                //   bll.vBindingFieldValueToCombo(ref cbvatmethod, "vatmethod");
                txorderno.Text = "NEW";
                txorderno.CssClass = cd.csstextro;
                btnew.CssClass = "btn btn-primary ";
                btsave.CssClass = "btn btn-success";
                btprintinvoice.CssClass = "divhid";
                btprintinvfree.CssClass = "divhid";
                rdonoff_SelectedIndexChanged(sender, e);
                btsave.CssClass = "divhid"; //by yanto 19-11-2016
                txcustomer.CssClass = cd.csstextro;
                cbuom.CssClass = cd.csstextro;
                cbsalesman.CssClass = cd.csstextro;
                txmanualfreeinv.CssClass = cd.csstextro;
                txmanualinv.CssClass = cd.csstextro;
                btprintall.CssClass = "divhid";
                rdonoff.Checked = true;
                dtorder_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string day = Convert.ToString(dtOrd.Day);
                string mon = Convert.ToString(dtOrd.Month);
                string yrs = Convert.ToString(dtOrd.Year);
                string date = mon + "/" + day + "/" + yrs;
                txitemsearch_AutoCompleteExtender.ContextKey = date;
                chcash.Checked = true;
                lbtitlevat.Text = (double.Parse(bll.sGetControlParameter("vat")) * 100).ToString();
                //txqty.Attributes.Add("type", "number");
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    void vDeleteAllWrk()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelWrkSalesOrderDtl(arr);
            bll.vDeleteWrkSalesOrderDtlTax(arr);
            bll.vDelCanvasOrderFreeItem(arr);
            bll.vDelWrkDiscountCash(arr);
            bll.vDelWrkEditFreeCash(arr);
            bll.vDelWrkCanvasDiscount(arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }        
    }
    protected void btadd_Click(object sender, EventArgs e)
    {

        double dresult; SqlDataReader rs = null;
        int cnt = 0;

        DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string day = Convert.ToString(dtOrd.Day);
        string mon = Convert.ToString(dtOrd.Month);
        string yrs = Convert.ToString(dtOrd.Year);
        string date = mon + "/" + day + "/" + yrs;
        cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_item where prod_cd in (select prod_cd from tmst_itemCustomer_Block  where otlcd ='" + hdcust_otlcd.Value.ToString() + "' and  end_dt > '" + date + "'  ) and item_cd = '" + hditem.Value.ToString() + "'  and item_sta_id = 'A'"));
        if (cnt == 1 && cbuom.SelectedValue.ToString() == "PCS")
        {
            lblItemBlock.Text = "Yes";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Item can not sales for this customer in pcs, Contact Faruk','warning');", true);
            return;
        }
        else { lblItemBlock.Text = "No"; }


        //double dQtyOrder = 0;
        //if (lbcredittype.Text != "CASH")
        //{
        //    if (Convert.ToDecimal(lbcredit.Text) < 5000)
        //    {
        //        if (Convert.ToString(lblVatEnglish.Text) == "")
        //        {
        //            txitemsearch.Text = "";
        //            hditem.Value = "";
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item add','Please update english name (vat).','warning');", true);
        //        }
        //        else if (Convert.ToString(lblVatArabic.Text) == "")
        //        {
        //            txitemsearch.Text = "";
        //            hditem.Value = "";
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item add','Please update arabic name (vat).','warning');", true);
        //        }
        //        else if (Convert.ToString(lbvatno.Text) == "")
        //        {
        //            txitemsearch.Text = "";
        //            hditem.Value = "";
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item add','Please update vat number.','warning');", true);
        //        }
        //    }
        //}
        if (hdcust.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select customer!','Customer must be selected','warning');", true);
            return;
        }


        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "newAlert('Item Search','Please select item  !','error','.txitemsearch');", true);
            return;
        }
        if (!double.TryParse(txstockcust.Text, out dresult))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "newAlert('Warning','Please fill customer stock !','warning','.txstockcust');", true);
            return;
        }

        if (!double.TryParse(lbstock.Text, out dresult))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "newAlert('Warning','Please fill customer stock !','warning','.lbstock');", true);
            return;
        }
        if (dresult <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are no stock available!','Please do Internal Transfer to VAN!','warning');", true);
            return;
        }
        if (!double.TryParse(txqty.Text, out dresult))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "newAlert('Warning','Please fill Order Quantity !','warning','.txqty');", true);
            return;
        }

        if (cbuom.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "newAlert('UOM Must be selected','Please select UOM !','warning','.cbuom');", true);
            return;
        }
        if (lbstock.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "newAlert('There is no stock in VAN','Please Loading Stock!','warning','.lbstock');", true);
            return;

        }


        if (bll.vLookUp("select dbo.fn_checkcustblocked('" + hdcust.Value.ToString() + "','S','"+ Request.Cookies["sp"].Value.ToString() + "')") == "ok")
        {
            if (grd.Rows.Count > 0)
            {
                Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
                double dPriceCurrent = Convert.ToDouble(lbtotsubtotal.Text);
                double dPriceCurrentTrx = Convert.ToDouble(lbprice.Text) * Convert.ToDouble(txshipmen.Text);
                double dPriceMaximumu = Convert.ToDouble(bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"));
                if (((dPriceCurrent + dPriceCurrentTrx) > dPriceMaximumu) && (bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'") != "CASH"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not add item again !','Max avl amt transaction is exceeded','warning');", true);
                    return;
                }
            }
            else
            {
                double dPriceCurrent = 0;
                double dPriceMaximumu = Convert.ToDouble(bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"));
                double dPriceCurrentTrx = Convert.ToDouble(lbprice.Text) * Convert.ToDouble(txshipmen.Text);
                if (((dPriceCurrent + dPriceCurrentTrx) > dPriceMaximumu) && (bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'") != "CASH"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not add item again !','Max avl amt transaction is exceeded','warning');", true);
                    return;
                }
            }
        }
        else
        {
            string sPromised = bll.vLookUp("select dbo.fn_checkblockedbydue('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (sPromised != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sPromised + "','Create payment promised!','warning');", true);
                return;
            }
        }

        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@stock_cust", txstockcust.Text));
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
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchCanvasOrderDiscount(arr);
            string sDiscount = "Discount Applied : \n\r";
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetWrkCanvasOrderDiscount(arr, ref rs);
            while (rs.Read())
            {
                sDiscount += rs["disc_cd"].ToString() + ", free qty : " + rs["free_qty"].ToString() + ", free cash : " + rs["free_cash"].ToString() + "\n\r";
            } rs.Close();
            grd.ToolTip = sDiscount;
            lbprice.Text = "";

            txstockcust.Text = "";
            txshipmen.Text = "";
            txqty.Text = "";
            lbstock.Text = "";
            lblItemBlock.Text = "";
            cbuom.SelectedValue = "";
            txitemsearch.Text = "";
            //cbsalesman.Focus();
            btsave.CssClass = "btn btn-success";
            txmanualinv.CssClass = cd.csstext;
            txmanualfreeinv.CssClass = cd.csstext;
            btdisc.CssClass = "btn btn-info";
            string sTotVat = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            Label lbtotvat = (Label)grd.FooterRow.FindControl("lbtotvat");
            lbtotvat.Text = sTotVat;
            lbvat.Text = sTotVat;
            cbbin.Enabled = false;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //txitemsearch.Focus();
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_canvasorder.aspx");
    }
    protected void btdisc_Click(object sender, EventArgs e)
    {
        try
        {

            if (hdcust.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer not yet selected','Please select customer !','warning');", true);
                return;
            }
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item not yet selected ','You have not item to be calculated  !!','warning');", true);

                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Clear();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vDelCanvasOrderFreeItem(arr);
            //bll.vDelWrkCanvasDiscount(arr);
            //arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchCanvasOrderDiscount(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_twrk_canvasdiscount_get", arr);
            grddisc.CssClass = "mygrid";
            //Rebinding discount
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
            if (txorderno.Text == "NEW")
            { //btsave.Visible = true; btnew.Visible = true; 
                btsave.CssClass = "btn btn-primary ";
                btnew.CssClass = "btn btn-primary ";
            }
            else
            {
                //btsave.Visible = false;
                //btnew.Visible = true;
                btsave.CssClass = "divhid";
                btnew.CssClass = "btn btn-primary ";
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btcheck_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "if", "sweetAlert('Discount Info','Discount : RD0001 | Qty :  4.0 | Free : 9.0','info');", true);

        return;
    }
    protected void btfree_Click(object sender, EventArgs e)
    {
        try
        {
            grdfree.CssClass = "mygrid";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfree, "sp_twrk_canvasorderfreeitem_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Session["checkwindow"] = null;
        Response.Redirect("fm_canvasorder.aspx");
    }
    protected void btprice_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdcust.Value.Equals(DBNull.Value) || hdcust.Value == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price can not determined because customer not yet selected !','Customer is not selected','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                txitemsearch.Text = "";
                hditem.Value = "";
                cbuom.SelectedValue = "";
                return;
            }
            int cnt = 0;
            DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string day = Convert.ToString(dtOrd.Day);
            string mon = Convert.ToString(dtOrd.Month);
            string yrs = Convert.ToString(dtOrd.Year);
            string date = mon + "/" + day + "/" + yrs;

            cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_item where prod_cd in (select prod_cd from tmst_itemCustomer_Block  where otlcd ='" + hdcust_otlcd.Value.ToString() + "' and  end_dt > '" + date + "'  ) and item_cd = '" + hditem.Value.ToString() + "'  and item_sta_id = 'A'"));
            if (cnt == 1 && cbuom.SelectedValue.ToString() == "PCS")
            {
                lblItemBlock.Text = "Yes";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Item can not sales for this customer in pcs, Contact Faruk','warning');", true);
                return;
            }
            else { lblItemBlock.Text = "No"; }
            // Adjustment Price
            double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + hditem.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
            if (dConv == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Conversion uom  or Price has not yet setup !','UOM Conversion/Price','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                lbstock.Text = "";
                lbprice.Text = "";
                hditem.Value = "";
                cbuom.SelectedValue = "";
                return;
            }
            string sCustType = "";
            sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            double dPrice = bll.dGetItemPrice(hditem.Value.ToString(), sCustType, cbuom.SelectedValue.ToString());
            if (dPrice == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Conversion uom  or Price has not yet setup !','UOM Conversion/Price','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                lbstock.Text = "";
                lbprice.Text = "";
                hditem.Value = "";
                cbuom.SelectedValue = "";
                return;
            }

            //  double dPrice = 0;
            double dAdjust = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')"));
            if (dAdjust > 0)
            {
                dPrice = dAdjust;
            }
            else { dPrice = 0; }
            lbprice.Text = dPrice.ToString();
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sStock = "0";
            sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + lbvan.Text + "','VS','" + ddate + "')");//by yanto 26-6-2016 
            string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
            string sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' union all select sum(free_qty) from twrk_canvasorderfreeitem where item_cd='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "')a");
            if (sStocktwrk == "") { sStocktwrk = "0"; }
            lbstock.Text = ((Convert.ToDouble(sStock) - Convert.ToDouble(sStocktwrk)) * Convert.ToDouble(sQTY)).ToString();
            if (txqty.Text != "")
            {
                if (Convert.ToDouble(lbstock.Text) > Convert.ToDouble(txqty.Text))
                {
                    txshipmen.Text = txqty.Text;
                }
                else if (Convert.ToDouble(lbstock.Text) < Convert.ToDouble(txqty.Text))
                {
                    txshipmen.Text = lbstock.Text;
                }
                else if (Convert.ToDouble(lbstock.Text) == Convert.ToDouble(txqty.Text))
                {
                    txshipmen.Text = txqty.Text;
                }
            }
            txqty.Text = "";
            txshipmen.Text = "";
            lbprice.Text = "";
            lbstock.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }

    void vGetPrice()
    {
        try
        {
            if (hdcust.Value.Equals(DBNull.Value) || hdcust.Value == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price can not determined because customer not yet selected !','Customer is not selected','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                txitemsearch.Text = "";
                hditem.Value = "";
                return;
            }
            // Adjustment Price
            double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + hditem.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
            if (dConv == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Conversion uom  or Price has not yet setup !','UOM Conversion/Price','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                lbstock.Text = "";
                lbprice.Text = "";
                cbuom.SelectedValue = "";
                return;
            }
            string sCustType = "";
            sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            double dPrice = bll.dGetItemPrice(hditem.Value.ToString(), sCustType, cbuom.SelectedValue.ToString());
            if (dPrice == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Conversion uom  or Price has not yet setup !','UOM Conversion/Price','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                lbstock.Text = "";
                lbprice.Text = "";
                cbuom.SelectedValue = "";
                return;
            }

            //  double dPrice = 0;
            double dAdjust = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')"));
            if (dAdjust > 0)
            {
                dPrice = dAdjust;
            }
            else { dPrice = 0; }
            lbprice.Text = dPrice.ToString();
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sStock = "0";
            sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + lbvan.Text + "','VS','" + ddate + "')");//by yanto 26-6-2016 
            string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
            string sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' union all select sum(free_qty) from twrk_canvasorderfreeitem where item_cd='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "')a");
            if (sStocktwrk == "") { sStocktwrk = "0"; }
            lbstock.Text = ((Convert.ToDouble(sStock) - Convert.ToDouble(sStocktwrk)) * Convert.ToDouble(sQTY)).ToString();
            if (txqty.Text != "")
            {
                if (Convert.ToDouble(lbstock.Text) > Convert.ToDouble(txqty.Text))
                {
                    txshipmen.Text = txqty.Text;
                }
                else if (Convert.ToDouble(lbstock.Text) < Convert.ToDouble(txqty.Text))
                {
                    txshipmen.Text = lbstock.Text;
                }
                else if (Convert.ToDouble(lbstock.Text) == Convert.ToDouble(txqty.Text))
                {
                    txshipmen.Text = txqty.Text;
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

    }
    protected void btprintinvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (txmanualinv.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Manual invoice can not empty','Manual Invoice','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                return;
            }


            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", txorderno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertfreebyitem(arr);

            arr.Clear();
            arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertcanvasorderfreebyitem(arr);

            string Sord = bll.vLookUp("select dbo.fn_orderproblem('" + txinvoiceno.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (Sord == "ok")
            {
                arr.Clear();
                arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
                arr.Add(new cArrayList("@manual_no", txmanualinv.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateDOSalesInvoice(arr);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertRptDosalesInvoice(arr);
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", txorderno.Text));
                arr.Add(new cArrayList("@dosales_sta_id", "C"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateMstDosalesByStatus(arr);
                arr.Clear();
                arr.Add(new cArrayList("@doc_no", txinvoiceno.Text));
                arr.Add(new cArrayList("@print_cd", "CANVINV")); //Canvas INvoice
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //arr.Add(new cArrayList("@qty"))
                bll.vInsertPrintControl(arr);
                string sSOCd = bll.vLookUp("select so_cd from tmst_dosales where inv_no='" + txinvoiceno.Text + "' and so_typ='cn' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                string sDiscAmt = bll.vLookUp("select sum(amt) from tcanvasorder_disccash where so_cd='" + sSOCd + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                arr.Clear();
                arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchSendEmailProdSpv(arr);
                btprintinvoice.Enabled = false;


                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=inv&no=" + txinvoiceno.Text + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are Unitprice problem with this order contact wazaran programmers','Unitprice Issues','warning');", true);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sTermCd = "";
        string sSoNo = "";
        List<cArrayList> arr = new List<cArrayList>();
        if (dtorder.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Order date has not select !','Please order date !','warning');", true);
            return;
        }

        if (hdcust.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer has not selected','Please select cust !','warning');", true);
            return;
        }

        string sExist = bll.vLookUp("select dbo.fn_checkmanualno('canvas','" + txmanualinv.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
        if (sExist != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual No has been used','" + sExist + "','warning');", true);
            return;
        }
        string scd = bll.vLookUp("select  dbo.sfnGetcountdown('"+ Request.Cookies["sp"].Value.ToString() + "')");
        if (scd == "0" && txorderno.Text == "NEW")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Block entry because deadline to daily closing !','Please daily closing !','warning');", true);
            return;
        }
        string sCLMsg = string.Empty;
        double dInvoice = Convert.ToDouble(bll.vLookUp("select dbo.fn_getsuminvoice('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"));
        double sCL = Convert.ToDouble(bll.vLookUp("select credit_limit from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));

        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Enter Item to be ordered !','Order item can not empty ','error');", true);
            return;
        }

        if (txmanualinv.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual invoice can not empty','Manual Invoice','warning');", true);
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        DateTime ddate = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string syear = ddate.Year.ToString();
        string sCount = bll.vLookUp("select count(1) from tdosales_invoice where year(inv_dt)='" + syear + "' and manual_no='" + txmanualinv.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
        if (Convert.ToDouble(sCount) > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual invoice has been used','Manual Invoice already used','warning');", true);
            return;
        }

        // Discount Tax Checked
        if (chdisctax.Checked)
        {
            string tax_no = bll.vLookUp("select isnull(tax_no,'') from tmst_customer where cust_cd = '" + hdcust.Value.ToString() + "'");
            string cr_no = bll.vLookUp("select isnull(cr_no,'') from tmst_customer where cust_cd = '" + hdcust.Value.ToString() + "'");

            if (tax_no == "" || cr_no == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for discount tax " + lbtotDiscTax.ToString() + "%','Update data Tax No & CR No!','warning');", true);
                return;
            }
        }

        try
        {
            sTermCd = bll.vLookUp("select payment_term from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@so_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@so_typ", "CN"));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@term_cd", sTermCd));
            arr.Add(new cArrayList("@currency_cd", bll.sGetControlParameter("currency")));
            arr.Add(new cArrayList("@ref_no", txmanualinv.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@so_sta_id", "D"));
            arr.Add(new cArrayList("@so_source", cbsourceorder.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vhc_cd", hdvan.Value.ToString()));
            arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
            arr.Add(new cArrayList("@rdonoff", rdonoff.Checked));
            arr.Add(new cArrayList("@tabno", txtabno.Text));
            bll.vInsertMstCanvasOrder(arr, ref sSoNo);
            txorderno.Text = sSoNo;
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertCanvasOrderDtl(arr);

            if (rdonoff.Checked)
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                arr.Add(new cArrayList("@so_cd", sSoNo));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchCanvasOrderDiscount(arr);
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", sSoNo));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertCanvasOrderDiscItem(arr);
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", sSoNo));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchGivenDiscount(arr);

                while (true)
                {
                    if (Convert.ToInt32(bll.vLookUp("select count(1) from tmst_dosales where so_cd='" + sSoNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'")) > 0)
                    {
                        bll.vInsertDOcanvasorderfreeinvoiceitem(arr);
                        break;
                    }
                }
            }
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@original_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@invoice_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@loading_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@app_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@app_sta_id", "A"));
            arr.Add(new cArrayList("@paidbycash", chcash.Checked));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertCanvasInfo(arr);
            // Walking in the moon
            txinvoiceno.Text = bll.vLookUp("select inv_no from tmst_dosales where so_cd='" + sSoNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            double dQty = 0;
            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            arr.Add(new cArrayList("@qty", dQty));
            arr.Add(new cArrayList("@driver_receipt_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInserttdosalesinvoice_driver(arr);
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchVat(arr);
            arr.Clear();
            arr.Add(new cArrayList("@stockcard_typ", "SALES"));
            arr.Add(new cArrayList("@refno", txinvoiceno.Text));
            bll.vBatchStockCard(arr);
            // -----------------------------
            Uri urlnya = Request.Url;
            string host = urlnya.GetLeftPart(UriPartial.Authority);
            Random rnd = new Random();
            int token = rnd.Next(1000, 9999);
            double tokenmail = rnd.Next();
            List<string> lapp = bll.lGetApproval("salesorder", 1);
            //------------------------------------------------------------------------------------------
            btprintinvoice.CssClass = "btn btn-success";
            btprintinvfree.CssClass = "divhid";
            btsave.CssClass = "divhid";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New canvas order successfull created', 'Canvas Order No. " + sSoNo + "','success');", true);

            grd.Columns[8].Visible = false;
            string sDONo = bll.vLookUp("select do_no from tmst_dosales where so_cd='" + sSoNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            string sInvNo = bll.vLookUp("select inv_no from tmst_dosales where do_no='" + sDONo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }


    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {
            hdcust_otlcd.Value = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            Response.Cookies["otlcd"].Value = hdcust_otlcd.Value;

            string sSalesmanCode = string.Empty;
            if (hdcust.Value.ToString() == "")
            {
                if (txcustomer.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please type for search customer ! | يرجى كتابة العملاء','Search Custoemr','warning');", true);
                    return;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "newAlert('Alert','Please type for search customer ! | يرجى كتابة العملاء','warning','.txcustomer');", true);
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "tabErr();", true);

                    //return;
                }
            }

            // Enh : 22 June 2019 : Customer Transfer Blocked - CIN
            string sCustomerTransferBlock = bll.vLookUp("select dbo.fn_customertransferpending('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sCustomerTransferBlock != "ok")
            {
                hdcust.Value = ""; txcustomer.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused there is pending in customer transfer ','" + sCustomerTransferBlock + "','warning');", true);
                return;
            }

            string sSalesBlock = bll.vLookUp("select dbo.fn_checksalesblock('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sSalesBlock != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused by document not completed','" + sSalesBlock + "','warning');", true);
                return;
            }

            // Enh : 9 Oct 2017 : Change BO Checking logic - IAG
            string sCheckDeniedBO = bll.vLookUp("select dbo.fn_checkdenytransaction('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if ((sCheckDeniedBO != "ok"))
            {
                string sCheckTablet = bll.vLookUp("select dbo.fn_checktofromtablet('" + hdcanvas.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                if (sCheckTablet != "ok")
                {
                    hdcust.Value = ""; txcustomer.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Transaction Denied!','" + sCheckDeniedBO + "','warning');", true);
                    return;
                }
            }

            // Discount Tax Checked
            if (chdisctax.Checked)
            {
                string tax_no = bll.vLookUp("select isnull(tax_no,'') from tmst_customer where cust_cd = '" + hdcust.Value.ToString() + "'");
                string cr_no = bll.vLookUp("select isnull(cr_no,'') from tmst_customer where cust_cd = '" + hdcust.Value.ToString() + "'");

                if (tax_no == "" || cr_no == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for discount tax " + lbtotDiscTax.ToString() + "%','Update data Tax No & CR No!','warning');", true);
                    return;
                }
            }

            // lbdiscex.Text = bll.vLookUp("select dbo.fn_checkdiscountexcluded('" + hdcust.Value.ToString() + "')");
            List<cArrayList> arr = new List<cArrayList>();
            System.Data.SqlClient.SqlDataReader rs = null;
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstCustomer(arr, ref rs);
            while (rs.Read())
            {
                lbaddress.Text = rs["addr"].ToString();
                var culture = System.Globalization.CultureInfo.GetCultureInfo("id-ID");
                lbcredit.Text = bll.vLookUp("select dbo.fn_getcreditlimit('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");//string.Format(culture, "{0:n}", Convert.ToDouble(rs["credit_limit"]));
                lbcusttype.Text = rs["otlnm"].ToString();
                lblVatArabic.Text = (rs["vat_custarabic"].Equals(DBNull.Value) ? "N/A" : rs["vat_custarabic"].ToString());
                lblVatEnglish.Text = (rs["vat_custname"].Equals(DBNull.Value) ? "N/A" : rs["vat_custname"].ToString());
                lbcredittype.Text = rs["cuscate_cd"].ToString();
                lbterm.Text = rs["term"].ToString();
                sSalesmanCode = rs["salesman_cd"].ToString();
                txcustomer.Text = rs["cust_cd"].ToString() + " : " + rs["cust_nm"].ToString();
                lbcuscate.Text = rs["cuscate_cd"].ToString();
                lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
                //  string sRemain =  bll.vLookUp("select sum(balance) from tdosales_invoice where inv_sta_id in ('N','P','R') and cust_cd='" + hdcust.Value.ToString() + "'");
                lbremain.Text = rs["creditremain"].ToString();
                string sPromised = bll.vLookUp("select dbo.fn_getlastpromised('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbpromised.Text = sPromised; //bll.vLookUp("select dbo.fn_getpaymentpromised('" + hdcust.Value.ToString() + "')");
                lbcustcate.Text = rs["cusgrcd"].ToString();
                lbmaxamt.Text = bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");//"0"; //bll.vLookUp("select dbo.fn_getmaxtransamt('" + hdcust.Value.ToString() + "')");
                lboverdue.Text = bll.vLookUp("select dbo.fn_getoverdue('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");//bll.vLookUp("select dbo.fn_getamtoverdueinv('" + hdcust.Value.ToString() + "')");
                lbvatno.Text = (rs["tax_no"].Equals(DBNull.Value) ? "N/A" : rs["tax_no"].ToString());
            }
            rs.Close();
            cbsalesman.SelectedValue = sSalesmanCode;
            cbsalesman_SelectedIndexChanged(sender, e);
            txcustomer.CssClass = "form-control input-group-sm ro";
            cbbin.CssClass = cd.csstext;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void btsearchso_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();window.open('lookup_co.aspx','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
    }
    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string svhccode = "";
            lbvan.Text = "There is no VAN for this salesman";
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstVehicleByEmpcd(arr, ref rs);
            while (rs.Read())
            {
                svhccode = rs["vhc_cd"].ToString();
                hdvan.Value = svhccode;
                lbvan.Text = rs["vhc_cd"].ToString();

            }
            rs.Close();
            arr.Clear();
            arr.Add(new cArrayList("@vhc_cd", svhccode));
            bll.vBindingComboToSp(ref cbbin, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);
            txcustomer_AutoCompleteExtender.ContextKey = cbsalesman.SelectedValue.ToString();
            try
            {
                cbbin.SelectedValue = "GS";
            }
            catch (Exception ex)
            {
                cbbin.SelectedValue = "BINGS";
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int cnt = 0;
            DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string day = Convert.ToString(dtOrd.Day);
            string mon = Convert.ToString(dtOrd.Month);
            string yrs = Convert.ToString(dtOrd.Year);
            string date = mon + "/" + day + "/" + yrs;
            cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_item where prod_cd in (select prod_cd from tmst_itemCustomer_Block  where otlcd ='" + hdcust_otlcd.Value.ToString() + "' and  end_dt > '" + date + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' ) and item_cd = '" + hditem.Value.ToString() + "'  and item_sta_id = 'A'"));
            if (cnt == 1 && cbuom.SelectedValue.ToString() == "PCS")
            {
                lblItemBlock.Text = "Yes";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Item can not sales for this customer in pcs, Contact Administrator','warning');", true);
                return;
            }
            else
            {
                lblItemBlock.Text = "No";
            }
            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item not yet selected !','Choose Item','warning');", true);
                lbprice.Text = "";
                lbstock.Text = "";
                return;
            }
            vGetPrice();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void cbsourceorder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbsourceorder.SelectedValue == "MAN")
        {
            txtabno.CssClass = cd.csstextro;
            txcustomer.CssClass = cd.csstext;
            cbsalesman.CssClass = cd.csstext;
            cbuom.CssClass = cd.csstext;
            cbbin.CssClass = cd.csstext;
            txmanualinv.CssClass = cd.csstext;
            txmanualinv.Focus();
        }
        else
        {
            txtabno.CssClass = cd.csstext;
            txcustomer.CssClass = cd.csstextro;
            txmanualfreeinv.CssClass = cd.csstextro;
            txmanualinv.CssClass = cd.csstextro;
            cbbin.CssClass = cd.csstextro;
            cbuom.CssClass = cd.csstextro;
            cbsalesman.CssClass = cd.csstextro;
        }
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelWrkSalesOrderDtl(arr); arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    //protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{

    //}
    protected void grddisc_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbdisccode = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbdisccode");
            Label lbfreeqty = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbfreeqty");
            Label lbuomfree = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbuomfree");
            HiddenField hdmec = (HiddenField)grddisc.Rows[e.NewSelectedIndex].FindControl("hdmec");
            HiddenField hddiscuse = (HiddenField)grddisc.Rows[e.NewSelectedIndex].FindControl("hddiscuse");
            string sdiscqty = string.Empty;
            double dfreeqty = Convert.ToDouble(lbfreeqty.Text);
            sdiscqty = bll.vLookUp("select isnull(sum(free_qty),0) from twrk_canvasorderfreeitem where  disc_cd='" + lbdisccode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (sdiscqty == "") { sdiscqty = "0"; }
            double dbalfreeqty = dfreeqty - Convert.ToDouble(sdiscqty);
            if ((dfreeqty - Convert.ToDouble(sdiscqty)) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount entry complete','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                return;
            }
            if (hdmec.Value.ToString() == "CH")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not available for Cash Discount','Only discount mechanis by item avalaible !','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                return;
            }
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_lookupitem.aspx?dc=" + lbdisccode.Text + "&du=" + hddiscuse.Value.ToString() + "&f=" + dbalfreeqty + "&t=cn&sal=" + cbsalesman.SelectedValue.ToString() + "&u=" + lbuomfree.Text + "');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_lookupitem.aspx?so_cd=" + txorderno.Text.Trim() + "&dc=" + lbdisccode.Text + "&du=" + hddiscuse.Value.ToString() + "&f=" + dbalfreeqty + "&t=cn&sal=" + cbsalesman.SelectedValue.ToString() + "&u=" + lbuomfree.Text + "');", true); //modify daryanto 12-10-2016
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_lookupitem.aspx?so_cd=" + txorderno.Text.Trim() + "&dc=" + lbdisccode.Text + "&du=" + hddiscuse.Value.ToString() + "&f=" + dbalfreeqty + "&t=cn&sal=" + cbsalesman.SelectedValue.ToString() + "&u=" + lbuomfree.Text + "&wh=" + hdvan.Value + "&cust=" + hdcust.Value.ToString() + "&bin=" + cbbin.SelectedValue.ToString() + "');", true); //modify daryanto 12-10-2016
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
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
            int cnt = 0;
            cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_item where prod_cd in (select prod_cd from tmst_itemCustomer_Block  where otlcd ='" + otlcd.Value.ToString() + "' and  end_dt > '" + contextKey.ToString() + "'  ) and item_cd = '" + rs["item_cd"].ToString() + "'  and item_sta_id = 'A'"));
            if (cnt == 1)
            {
                sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "|" + rs["item_nm"].ToString() + "|" + rs["size"].ToString() + "|" + rs["branded_nm"].ToString() + "| Item Block For Pcs -" + " Yes ", rs["item_cd"].ToString());

            }
            else
            {
                sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "|" + rs["item_nm"].ToString() + "|" + rs["size"].ToString() + "|" + rs["branded_nm"].ToString() + "| Item Block For Pcs -" + " No ", rs["item_cd"].ToString());
            }
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void txqty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dOrder = 0;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "ShowProgress();", true);
            double dPrice = Convert.ToDouble(lbprice.Text);
            double dStock = Convert.ToDouble(lbstock.Text);
            if (!double.TryParse(txqty.Text, out dOrder))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Order must be numeric','Check qty order!','warning');", true);
                return;
            }
            if (dOrder > dStock)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Order can not bigger than stock available','Pls do stock in van!','warning');", true);
                return;
            }
            txshipmen.Text = txqty.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbqtyorder = (Label)e.Row.FindControl("lbqtyorder");
            Label lbsubtotal = (Label)e.Row.FindControl("lbsubtotal");
            Label lbshipment = (Label)e.Row.FindControl("lbshipment");
            Label lbdiscamt = (Label)e.Row.FindControl("lbdiscamt");
            dQtyOrder += Convert.ToDouble(lbqtyorder.Text);
            dSubTotal += Convert.ToDouble(lbsubtotal.Text);
            dTotShipment += Convert.ToDouble(lbshipment.Text);

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
            //lbtotdiscount.Text = "90";
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> sarr = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sorderno = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        arr.Add(new cArrayList("@so_cd", prefixText));
        bll.vSearchCanvasOrder(arr, ref rs);
        while (rs.Read())
        {
            sorderno = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["so_cd"].ToString() + "-" + rs["so_dt"].ToString() + "-" + rs["remark"].ToString(), rs["so_cd"].ToString());
            sarr.Add(sorderno);
        } rs.Close();
        return (sarr.ToArray());
    }
    protected void btprintinvfree_Click(object sender, EventArgs e)
    {
        try
        {
            if (txmanualfreeinv.Text == "")
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newAlert", "newAlert('Manual Invoice Free can not empty','Invoice No." + txinvoiceno.Text + "','warning','.txmanualfreeinv');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            arr.Add(new cArrayList("@manual_no", txmanualfreeinv.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateDoSalesInvoiceFree(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport1('fm_report2.aspx?src=invf&noi=" + txinvoiceno.Text + "','fm_report2.aspx?src=invf2&noi=" + txinvoiceno.Text + "');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void bttabsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('fm_tabcanvasorder.aspx');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

    }
    protected void rdonoff_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rdonoff.Checked)
        //{
        //    //btdisc.Visible = true;
        //    btdisc.CssClass = "btn btn-primary ";
        //    grdfree.DataSource = null;
        //    grdfree.DataBind();
        //    // grddisc.Visible = true;
        //    grddisc.CssClass = "mygrid";
        //    // grdfree.Visible = true;
        //    grdfree.CssClass = "mygrid";
        //    // rdonoff.BorderColor = System.Drawing.Color.Green;
        //}
        //else if (!rdonoff.Checked)
        //{
        //    //List<cArrayList> arr = new List<cArrayList>();
        //    //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //    vDeleteAllWrk();
        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //    bll.vBindingGridToSp(ref grdfree, "sp_twrk_canvasorderfreeitem_get", arr);
        //    bll.vBindingGridToSp(ref grddisc, "sp_twrk_canvasdiscount_get", arr);
        //    //grdfree.Visible = false;
        //    grddisc.CssClass = "divhid";
        //    //  grddisc.Visible = false;
        //    grdfree.CssClass = "divhid";

        //    //btdisc.Visible = false;
        //    btdisc.CssClass = "divhid";
        //    // rdonoff.BorderColor = System.Drawing.Color.Red;
        //}
        grddisc.CssClass = "divhid";
        grdfree.CssClass = "divhid";
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        try
        {
            // txorderno.Enabled = false;
            SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelWrkSalesOrderDtl(arr);
            bll.vDelWrkCanvasDiscount(arr); arr.Clear();
            arr.Add(new cArrayList("@so_cd", hdcanvas.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstCanvasorder(arr, ref rs);
            while (rs.Read())
            {
                string prevstk = bll.vLookUp("select prevstk from tcanvasorder_info where so_cd='" + hdcanvas.Value.ToString() + "' and salespointcd='" + rs["salespointcd"].ToString() + "'");
                if (prevstk == "") { prevstk = "false"; }
                //chprevstk.Checked = Boolean.Parse(prevstk);
                txtabno.Text = rs["tabno"].ToString();
                txtabno.CssClass = cd.csstextro;
                dtorder.Text = Convert.ToDateTime(rs["so_dt"]).ToString("d/M/yyyy");
                txmanualinv.Text = rs["ref_no"].ToString();
                txmanualinv.CssClass = cd.csstextro;
                lbsalespoint.Text = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + rs["salespointcd"].ToString() + "'");
                txinvoiceno.Text = bll.vLookUp("select inv_no from tmst_dosales where so_cd='" + hdcanvas.Value.ToString() + "' and salespointcd='" + rs["salespointcd"].ToString() + "'");
                txmanualfreeinv.Text = bll.vLookUp("select top 1 manual_no from tdosalesinvoice_free where inv_no='" + txinvoiceno.Text + "' and salespointcd='" + rs["salespointcd"].ToString() + "'");
                if (txmanualfreeinv.Text != "")
                { txmanualfreeinv.CssClass = "form-control  ro"; }
                //rdonoff.Checked = (rs["rdonoff"] == "ON" ? true : false);
                rdonoff_SelectedIndexChanged(sender, e);
                cbsourceorder.SelectedValue = rs["so_source"].ToString();
                txremark.Text = rs["remark"].ToString();
                hdcust.Value = rs["cust_cd"].ToString();
                btsearch_Click(sender, e);
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", hdcanvas.Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertWrkSalesOrderDtlFromOrder(arr);
                txremark.CssClass = "ro";

                string sDoNo = bll.vLookUp("select inv_no from tmst_dosales where so_typ='cn' and so_cd='" + hdcanvas.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                // string sManualInv = bll.vLookUp("select manual_no from tdosales_invoice where inv_no='" + sDoNo + "'");
                dtorder.CssClass = "ro";
                txcustomer.CssClass = "form-control input-group-sm ro";
                // txmanualinv.Text = sManualInv;
                if (txmanualinv.Text == "")
                {
                    txmanualinv.CssClass = cd.csstext; ;
                }
                else
                {
                    txmanualinv.CssClass = cd.csstextro;
                }

                if (rs["so_sta_id"].ToString() == "D")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    // bll.vBatchCanvasOrderDiscount(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@so_cd", hdcanvas.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    // bll.vBatchEditCanvasDiscount(arr); arr.Clear();

                    bll.vInsertWrkCanvasdiscountFromCore(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vBindingGridToSp(ref grddisc, "sp_twrk_canvasdiscount_get", arr);

                    bll.vBindingGridToSp(ref grdfree, "sp_twrk_canvasorderfreeitem_get", arr);
                    grdfree.CssClass = "mGrid";
                    if (hdcanvas.Value.ToString() == "")
                    {
                        // btsave.Visible = true;
                        // btprintinvfree.Visible = false;
                        btsave.CssClass = "btn  btn-warning";
                        btprintinvfree.CssClass = "divhid";
                        btprintinvoice.CssClass = "divhid";
                        btdisc.CssClass = "divhid";


                    }
                    else
                    {
                        //btsave.Visible = false;
                        //btprintinvoice.Visible = true;
                        btsave.CssClass = "divhid";
                        btprintinvfree.CssClass = "btn btn-info ";
                        btprintinvoice.CssClass = "btn btn-info ";
                        btdisc.CssClass = "divhid";

                    }
                }
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='so_sta_id' and fld_valu='" + rs["so_sta_id"].ToString() + "'");
            }
            rs.Close();
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
            grd.Columns[12].Visible = false;
            txorderno.Text = hdcanvas.Value.ToString();
            txorderno.CssClass = cd.csstextro;
            grddisc.CssClass = "mGrid";
            string sApp = bll.vLookUp("select app_sta_id from tcanvasorder_info where so_cd='" + hdcanvas.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (sApp == "W")
            {
                //btsave.Visible = false;
                btsave.CssClass = "divhid";
                //btprintinvfree.Visible = false;
                btprintinvfree.CssClass = "divhid";
                // btprintinvoice.Visible = false;
                btprintinvoice.CssClass = "divhid";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This canvasorder need approval !','You can not proceed until get approved !','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

            }
            else
            {
                // btsave.Visible = false;
                btsave.CssClass = "divhid";
                //btprintinvfree.Visible = true;
                btprintinvfree.CssClass = "btn btn-info ";
                //btprintinvoice.Visible = true;
                btprintinvoice.CssClass = "btn btn-info ";
            }
            DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string day = Convert.ToString(dtOrd.Day);
            string mon = Convert.ToString(dtOrd.Month);
            string yrs = Convert.ToString(dtOrd.Year);
            string date = mon + "/" + day + "/" + yrs;
            txitemsearch_AutoCompleteExtender.ContextKey = dtorder.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }

    private void vSendEmail(string sCustCode, string sToken, string sReceived, string sCCReceived)
    {
        SqlDataReader rs = null;
        string sHttp = bll.sGetControlParameter("link_branch");
        List<cArrayList> arr = new List<cArrayList>();
        string sLastDate = string.Empty; ;
        string sLastAmount = string.Empty;
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGetLastTrans(arr, ref rs);
        while (rs.Read())
        {
            sLastAmount = rs["trn_amt"].ToString();
            sLastDate = rs["trn_typ"].ToString() + "," + rs["trn_dt"].ToString();
        } rs.Close();
        string sBody = "Dear Branch Spv, \n\r Need Approval for Transaction Block " + hdcust.Value.ToString() +
                  "<table style='width:100%'><tr style='background-color:silver'><td>Code Customer</td><td>:</td><td>" + hdcust.Value.ToString() + "</td><td>Salesman Code</td><td>:</td><td>" + cbsalesman.SelectedValue.ToString() + "</td></tr>" +
                  "<tr><td>Customer Name</td><td>:</td><td>" + txcustomer.Text + "</td><td>Salesman Name</td><td>:</td><td>" + cbsalesman.SelectedItem.Text + "</td></tr>" +
                  "<tr style='background-color:silver'><td>Type Customer</td><td>:</td><td>" + lbcusttype.Text + "</td><td>Customer Balance</td><td>:</td><td>" + "44" + "</td></tr>" +
                  "<tr><td>Credit Type</td><td>:</td><td>" + lbcredit.Text + "</td><td>New Inv Amount</td><td>:</td><td>" + bll.vLookUp("select sum(unitprice * qty) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr style='background-color:silver'><td>Credit Limit</td><td>:</td><td>" + lbcredit.Text + "</td><td>Remain Limit</td><td>:</td><td>" + lbremain.Text + "</td></tr>" +
                  "<tr><td>Last Transaction</td><td>:</td><td>" + sLastDate + "</td><td>Amount Last Trans</td><td>:</td><td>" + sLastAmount + "</td></tr></table><br>";
        //  sBody += "Please click <a href='landingpage.aspx?sta=A&trnname=salesorder&appcode=" + sToken + "'>Approve</a> for approved OR <a href='landingpage.aspx?sta=R&trnname=salesorder&appcode=" + sToken + "'>Reject</a> for rejected !";


        bll.vSendMail(sCCReceived, "cc:Canvas Order Request Need Approval", sBody);
        sBody += "Please click <a href='" + sHttp + @"/landingpage.aspx?sta=A&trnname=canvasorder&appcode=" + sToken + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Approve</a> for approved OR <a href='" + sHttp + "/landingpage.aspx?sta=R&trnname=canvasorder&appcode=" + sToken + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Reject</a> for rejected !";
        bll.vSendMail(sReceived, "Canvas Order Order Request Need Approval", sBody);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //  vSendEmail(hdcust.Value.ToString(), "1232", "irwan.agusyono@gmail.com");
    }
    protected void grddisc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grddisc.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_twrk_canvasdiscount_get", arr);
            TextBox txfreeqty = (TextBox)grddisc.Rows[e.NewEditIndex].FindControl("txfreeqty");
            TextBox txfreecash = (TextBox)grddisc.Rows[e.NewEditIndex].FindControl("txfreecash");
            HiddenField hdmec = (HiddenField)grddisc.Rows[e.NewEditIndex].FindControl("hdmec");
            double dfreeqty; double dfreecash;
            double.TryParse(txfreecash.Text, out dfreecash);
            double.TryParse(txfreeqty.Text, out dfreeqty);
            if (hdmec.Value.ToString() == "FG")
            {
                txfreecash.CssClass = "form-control input-sm ro";
                txfreeqty.CssClass = cd.csstext;
            }
            if (hdmec.Value.ToString() == "CH")
            {
                txfreeqty.CssClass = "form-control input-sm ro";
                txfreecash.CssClass = "form-control input-sm makeitreadwrite";
            }
            if (hdmec.Value.ToString() == "PC")
            {
                txfreeqty.CssClass = cd.csstext;
                txfreecash.CssClass = cd.csstext;
            }
            //if (dfreeqty == 0)
            //{ txfreeqty.CssClass = "ro"; txfreecash.CssClass = "makeitreadwrite"; }
            //if (dfreecash == 0)
            //{ txfreecash.CssClass = "ro"; txfreeqty.CssClass = "makeitreadwrite"; }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void grddisc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddisc.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_canvasdiscount_get", arr);
    }
    protected void grddisc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lbdisccode = (Label)grddisc.Rows[e.RowIndex].FindControl("lbdisccode");
            TextBox txfreeqty = (TextBox)grddisc.Rows[e.RowIndex].FindControl("txfreeqty");
            TextBox txfreecash = (TextBox)grddisc.Rows[e.RowIndex].FindControl("txfreecash");
            HiddenField hdmec = (HiddenField)grddisc.Rows[e.RowIndex].FindControl("hdmec");
            double dFreeOri = Convert.ToDouble(bll.vLookUp("select isnull(free_ori,0) from twrk_canvasdiscount where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and disc_cd='" + lbdisccode.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
            List<cArrayList> arr = new List<cArrayList>();
            double dFree = 0;
            if (hdmec.Value.ToString() == "FG")
            {
                if (!double.TryParse(txfreeqty.Text, out dFree))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free qty must numeric','Free Qty','warning');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "newAlert('Free qty must numeric','Free Qty','warning','.txfreeqty');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                    return;
                }
                else
                {
                    if (dFree < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free qty can not less then zero','Free Qty','warning');", true);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "newAlert('Free qty can not less then zero','Free Qty','warning','.txfreeqty');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                        return;
                    }

                    if (dFree > dFreeOri)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free qty can not bigger than Originial','Free Qty','warning');", true);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "newAlert('Free qty can not bigger than Originial','Free Qty','warning','.txfreeqty');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                        return;
                    }

                }
            }
            else if ((hdmec.Value.ToString() == "CH") || (hdmec.Value.ToString() == "PC"))
            {
                if (!double.TryParse(txfreecash.Text, out dFree))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free cash must numeric','Free Cash','warning');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "newAlert('Free cash must numeric','Free cash','warning','.txfreecash');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                    return;
                }
                else
                {

                    if (dFree < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free cash can not less then zero','Free Cash','warning');", true);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "newAlert('Free cash can not less then zero','Free cash','warning','.txfreecash');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                        return;
                    }
                    if (dFree > dFreeOri)
                    {
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free cash can not bigger than original','Free Cash','warning');", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "newAlert('Free cash can not bigger than original','Free cash','warning','.txfreecash');", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                            return;
                        }
                    }
                }
            }
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
            arr.Add(new cArrayList("@qty", dFree));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertWrkEditFreeCash(arr);
            arr.Clear();
            arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@free_qty", txfreeqty.Text));
            arr.Add(new cArrayList("@free_cash", txfreecash.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateWrkCanvasDiscount(arr);
            grddisc.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_twrk_canvasdiscount_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void bttabsearch_Click1(object sender, EventArgs e)
    {

    }
    protected void grdfree_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (txorderno.Text != "NEW")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free item Can not delete','Free','warning');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "newAlert('Free item Can not delete','Free','warning','.txorderno');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

                return;
            }
            Label lblitem_cd = (Label)grdfree.Rows[e.RowIndex].FindControl("lblitem_cd");
            Label lbldisc_cd = (Label)grdfree.Rows[e.RowIndex].FindControl("lbldisc_cd");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", lblitem_cd.Text));
            arr.Add(new cArrayList("@disc_cd", lbldisc_cd.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDeletetwrk_canvasorderfreeitem(arr);
            btfree_Click(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void chprevstk_CheckedChanged(object sender, EventArgs e)
    {
        DateTime ddate = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        var dateAsString = ddate.ToString("yyyy-MM-dd");
        string swhs_cd = bll.vLookUp("select whs_cd from tstockopname_schedule where whs_cd='" + lbvan.Text + "' and schedule_dt='" + dateAsString + "'");
        if (swhs_cd != lbvan.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please entri schedule jaret !','Prev stk can not changed ','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

            //hprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
            return;
        }
        if (grd.Rows.Count != 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item already inserted !','Prev stk can not changed ','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

            // if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
            return;
        }

    }
    protected void btprintall_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_printall.aspx?printtype=canvas");
    }
    protected void chdisc_CheckedChanged(object sender, EventArgs e)
    {
        //if (rdonoff.Checked)
        //{
        //    //btdisc.Visible = true;
        //    btdisc.CssClass = "btn btn-primary ";
        //    grdfree.DataSource = null;
        //    grdfree.DataBind();
        //    grddisc.CssClass = "mygrid";
        //    grdfree.CssClass = "mygrid";
        //    // rdonoff.BorderColor = System.Drawing.Color.Green;
        //}
        //else if (!rdonoff.Checked)
        //{

        //    vDeleteAllWrk();
        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //    bll.vBindingGridToSp(ref grdfree, "sp_twrk_canvasorderfreeitem_get", arr);
        //    bll.vBindingGridToSp(ref grddisc, "sp_twrk_canvasdiscount_get", arr);
        //    grddisc.CssClass = "divhid";
        //    grdfree.CssClass = "divhid";
        //    btdisc.CssClass = "divhid";
        //    //rdonoff.BorderColor = System.Drawing.Color.Red;
        //}
        //grddisc.CssClass = "divhid";
        //grdfree.CssClass = "divhid";
    }
    protected void txqty_TextChanged1(object sender, EventArgs e)
    {
        txshipmen.Text = txqty.Text;
    }
    protected void btreset_Click(object sender, EventArgs e)
    {
        lbprice.Text = "";
        txshipmen.Text = "";
        cbuom.SelectedValue = "CTN";
        cbuom_SelectedIndexChanged(sender, e);
        //txstockcust.Focus();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }

    protected void chdisctax_CheckedChanged(object sender, EventArgs e)
    {

    }


}