using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_takeorderentry_ho2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dQtyOrder = 0;
    double dSubTotal = 0;
    double dTotShipment = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["salespoint"] = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            dtorder.Text = System.DateTime.Now.ToString("d/M/yyyy");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get ", "salespointcd", "salespoint_nm");
            cbsalespoint.Items.Remove(cbsalespoint.Items.FindByValue("0"));
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingFieldValueToCombo(ref cbsourseinfo, "source_info");
            bll.vDelWrkSalesOrderDtl(arr);
            bll.vDelSalesOrderFreeItem(arr);
            //bll.vDelWrkDiscountCash(arr);
            //bll.vDelWrkSalesDiscount(arr);
            //bll.vDelTwrkEditFreeCash(arr);
            vInit();
            cbsalespoint_SelectedIndexChanged(sender, e);
        }
    }

    void vInit()
    {
        txremark.Text = "";
        //txremark.CssClass = "form-control  ro require";
        txorderno.Text = "NEW";
        txorderno.CssClass = "form-control input-group-sm ro";
        btnew.CssClass = "btn btn-success btn-new";
        btsave.CssClass = "btn btn-warning btn-save";
        txinvoiceno.CssClass = "form-control  ro";
        btedit.CssClass = "divhid";
        btcancel.CssClass = "divhid";
        //txcustomer.CssClass = "form-control  ro";
        txitemsearch.CssClass = "form-control input-sm  ro";
        cbuom.CssClass = "form-control input-sm ro";
        txstockcust.CssClass = "form-control input-sm  ro";
        txqty.CssClass = "form-control input-sm   ro";
        txshipmen.CssClass = "form-control input-sm  ro";
        btdisc.CssClass = "divhid";
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Session["salespoint"].ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
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
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Session["salespoint"].ToString()));
        arr.Add(new cArrayList("@item_cd", prefixText));
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void btsearchso_Click(object sender, EventArgs e)
    {

    }
    protected void cbsourseinfo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txitemsearch_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hditem.Value.ToString() == "")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item not yet selected!','Choose Item','warning');", true);
            lbprice.Text = "";
            lbstock.Text = "";
            return;
        }

        btprice_Click(sender, e);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void btprice_Click(object sender, EventArgs e)
    {
        if (hdcust.Value.Equals(DBNull.Value) || hdcust.Value == "")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Price can not determined because customer not yet selected !','warning');", true);
            txitemsearch.Text = "";
            hditem.Value = "";
            return;
        }

        double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + hditem.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
        if (dConv == 0)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no price setup or no setup UOM conversion!','Contact to wazaran admin','warning');", true);
            lbstock.Text = "";
            lbprice.Text = "";
            cbuom.SelectedValue = "";
            return;
        }
        
        //Check Adjustment Price
        double dPrice = 0;
        string sCustType = "";
        sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");
        double dAdjust = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice_ho ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "','"+cbsalespoint.SelectedValue.ToString()+"')"));
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
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price not yet setup or item conversion not setup!','Contact wazaran admin','warning');", true);
            lbstock.Text = "";
            lbprice.Text = "";
            cbuom.SelectedValue = "";
            //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "HideProgress();", true);
            return;
        }
        //string sPrice = bll.vLookUp("select unitprice from tcustomertype_price where item_cd='" + hditem.Value.ToString() + "' and cust_typ='" + sCustType + "'");
        lbprice.Text = dPrice.ToString();
        //string sStock = bll.vLookUp("select stock_display from tmst_stock where item_cd='" + hditem.Value.ToString() + "'  and bin_cd='" + cbbin.SelectedValue.ToString() + "' and (whs_cd in (select qry_data from tmap_query where qry_cd='whs_branch') and bin_cd in (select qry_data from tmap_query where qry_cd='bin_branch'))");
        //string sStock = bll.vLookUp("select dbo.fn_checkcurrentstock('"+hditem.Value.ToString()+"','"+cbwhs.SelectedValue.ToString()+"','"+cbbin.SelectedValue.ToString()+"','stockdisplay')");
        //if (sStock == null || sStock == "") { sStock = "0"; }

        //string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
        //string sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' union all select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "')a");
        //if (sStocktwrk == "") { sStocktwrk = "0"; }
        //lbstock.Text = ((Convert.ToDouble(sStock) * Convert.ToDouble(sQTY)) - Convert.ToDouble(sStocktwrk)).ToString(); 
        //DateTime ddate = DateTime.Now.ToString();
        string sStock = "0";
        //if (chprevstk.Checked == true)
        //{
        //    sStock = bll.vLookUp("select dbo.[sfnGetStock]('" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate + "')");//by yanto 25-6-2016 
        //}
        //else
        //{
        //sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
        //}

        if (sStock == null || sStock == "") { sStock = "0"; }

        //string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
        //string sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' union all select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "')a");
        //if (sStocktwrk == "") { sStocktwrk = "0"; }
        //lbstock.Text = ((Convert.ToDouble(sStock) - Convert.ToDouble(sStocktwrk)) * Convert.ToDouble(sQTY)).ToString();
        //if (txqty.Text != "")
        //{
        //    if (Convert.ToDouble(lbstock.Text) > Convert.ToDouble(txqty.Text))
        //    {
        //        txshipmen.Text = txqty.Text;
        //    }
        //    else
        //    {
        //        txshipmen.Text = lbstock.Text;
        //    }
        //}
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        double dresult; System.Data.SqlClient.SqlDataReader rs = null; 

        if (hditem.Value.ToString() == "")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Search','Please select item  !','warning');", true);
            return;
        }
        if (cbuom.SelectedValue.ToString() == "")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('UOM is not yet selected','Please select UOM !','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@stock_cust", 0));
            arr.Add(new cArrayList("@unitprice", lbprice.Text));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            arr.Add(new cArrayList("@stock_amt", 0));
            arr.Add(new cArrayList("@qty_shipment", txqty.Text));
            bll.vInsertWrkSalesORderDtl(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
            arr.Clear();
            //arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBatchTakeOrderDiscount(arr);
            //string sDiscount = "Discount Applied : \n\r";
            //arr.Clear();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vGetWrkSalesOrderDiscount(arr, ref rs);
            //while (rs.Read())
            //{
            //    sDiscount += rs["disc_cd"].ToString() + ", free qty : " + rs["free_qty"].ToString() + ", free cash : " + rs["free_cash"].ToString() + "\n\r";
            //} rs.Close();
            //grd.ToolTip = sDiscount;
            lbprice.Text = "";
            txitemsearch.Text = "";
            txstockcust.Text = "";
            txshipmen.Text = "";
            txqty.Text = "";
            lbstock.Text = "";
            cbuom.SelectedValue = "";
            btdisc.CssClass = "btn btn-primary";
            string sTotVat = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            string sSubTotal = bll.vLookUp("select sum(qty*unitprice) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            Label lbtotvat = (Label)grd.FooterRow.FindControl("lbtotvat");
            Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
            lbtotvat.Text = sTotVat;
            lbtotsubtotal.Text = sSubTotal;
            string sVAT = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            lbvat.Text = sVAT;
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Add Item Take Order HO");
        }
        finally
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "ItemClear();", true);
        }

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void btdisc_Click(object sender, EventArgs e)
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
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));       //-
        // bll.vDeleteWrkDiscountRelated(arr);                                                   //-
        //---------------------------------------------------------------------------------------
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBatchTakeOrderDiscount(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
        grd.DataSource = null;
        grd.DataBind();
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
        grddisc.CssClass = "table tables-striped mygrid";
        btsave.CssClass = "btn btn-warning btn-save";
        btnew.CssClass = "btn btn-success btn-new";


    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_takeorderentry_ho2.aspx");
    }
    protected void btedit_Click(object sender, EventArgs e)
    {

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sTermCd = "";
        string sSoNo = ""; string sCLMsg = string.Empty;

        if (hdcust.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer must selected!','Customer has not selected','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            return;
        }
        if (dtorder.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Order date has not select !','Please order date !','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            return;
        }
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Enter Item to be ordered !','Order item can not empty ','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            return;
        }
        try
        {
            sTermCd = bll.vLookUp("select payment_term from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@so_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@so_typ", "TO"));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salesman_cd", ""));
            arr.Add(new cArrayList("@term_cd", sTermCd));
            arr.Add(new cArrayList("@currency_cd", "SAR"));
            arr.Add(new cArrayList("@ref_no", txmanualinv.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@so_sta_id", "N"));
            arr.Add(new cArrayList("@so_source", cbsourceorder.SelectedValue.ToString()));
            arr.Add(new cArrayList("@rdonoff", "OFF"));
            arr.Add(new cArrayList("@manual_no", txmanualinv.Text));
            if (hdto.Value.ToString() == "") //New data 
            {
                bll.vInsertMstSalesOrder(arr, ref sSoNo);
            }
            //else
            //{   //-------------------------------------This is delete from previouse transaction
            //    sSoNo = hdto.Value.ToString();
            //    arr.Clear();
            //    arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
            //    bll.vDelSalesorderDtl(arr);
            //    bll.vDelDOSalesInvoiceDiscCash(arr);
            //    bll.vDelSalesOrderDiscCash(arr);
            //    bll.vDelSalesOrderDisccashitem(arr);
            //    bll.vDelSalesorderShipment(arr);
            //    bll.vDelDosalesInvoiceFree(arr);
            //    bll.vDelDosalesDtl(arr);
            //    bll.vDelDosalesinvoiceDtl(arr);
            //    bll.vDelSalesOrderDiscItem(arr);
            //    bll.vDelSalesOrderDiscHist(arr);
            //    bll.vUpdateDosalesInvoiceByZero(arr);
            //    //-------------------------------------------------------------------------------
            //}
            txorderno.Text = sSoNo;
            txinvoiceno.Text = bll.vLookUp("select inv_no from tmst_dosales_ho where so_cd='"+sSoNo+"'");
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vInsertSalesOrderDtl(arr);
            arr.Clear();
            //arr.Add(new cArrayList("@so_cd", sSoNo));
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vInsertSalesOrderShipment(arr);

            arr.Clear();
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            //This is not running batch takeorder ---------------------------------------------
            bll.vBatchTakeOrderDiscount(arr);
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBatchEditDiscount(arr);

            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vInsertSalesorderdiscitem_ins(arr);

            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vInsertSalesOrderDiscHist(arr);
            
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            //bll.vBatchGivenDiscount(arr);

            // Check if invoice already created ------------------------------------------------------------------------
            //while (true)
            //{
            //    if (Convert.ToInt32(bll.vLookUp("select count(1) from tmst_dosales where so_cd='" + sSoNo + "'")) > 0)
            //    {
            //        bll.vInsertDosalesInvoiceFreeFromFreeItem(arr);
            //        break;
            //    }
            //}
            //      }
            txinvoiceno.Text = bll.vLookUp("select inv_no from tmst_dosales where so_cd='" + sSoNo + "'");
            txinvoiceno.CssClass = cd.csstextro;
            arr.Clear();
            //arr.Add(new cArrayList("@typ", "TO"));
            //arr.Add(new cArrayList("@so_cd", sSoNo));
            //arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            ////  if (rdonoff.SelectedValue.ToString() == "ON")
            //if (chdisc.Checked)
            //{
            //    //   bll.vInsertTdosalesinvoicedisccash(arr);
            //}

            //arr.Clear();
            //arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            //arr.Add(new cArrayList("@manual_no", txmanualinv.Text));
            //bll.vUpdateDoSalesInvoice(arr);
            //---------------------------------------------------------------
            ////VAT Enhancement : 19 Oct 2017
            //arr.Clear();
            //arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            //bll.vBatchVat(arr);
            //--------------------------------------------------------------
            //SqlDataReader rs = null;
            //// 2017-1-11
            //arr.Clear(); string sMsg = string.Empty;
            //arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            //bll.vGetDosalesInvoiceByDueDate(arr, ref rs);
            //while (rs.Read())
            //{
            //    if (sMsg == string.Empty)
            //    { sMsg = sMsg + rs["inv_no"].ToString(); }
            //    else
            //    { sMsg = sMsg + "," + rs["inv_no"].ToString(); }

            //}
            //rs.Close();
            
            //dtdelivery.CssClass = cd.csstextro;
            btsave.CssClass = "divhid";
            //txmanualno.CssClass = cd.csstextro;
            txitemsearch.CssClass = cd.csstextro;
            txqty.CssClass = cd.csstextro;
            hditem.Value = "";
            txstockcust.CssClass = cd.csstextro;
            cbuom.CssClass = cd.csstextro;
            txshipmen.CssClass = cd.csstextro;
            grd.Columns[8].Visible = false;
            //hddo.Value = bll.vLookUp("select do_no from tmst_dosales where so_cd='" + sSoNo + "'");
            //btprint.CssClass = "btn btn-info";
            btdisc.CssClass = "divhid";
            // Activated driver  - 2 JAN 2015 IA----------------------------------------------------------------
            arr.Clear();
            //cbdriver.CssClass = cd.csstext;
            //cbvehicle.CssClass = cd.csstext;
            
            txmaninvfreeno.CssClass = cd.csstext;
            txmanualinv.CssClass = cd.csstext;
            lbvat.Text = bll.vLookUp("select sum(vat) from tdosalesinvoice_dtl where inv_no='" + txinvoiceno.Text + "'");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Take order has been saved', 'Take Order no : " + sSoNo + "', 'success');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Save Take Order");
        }
        finally
        {
            //// New Enhancement : 12-June-17 by IAG
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@so_cd", sSoNo));
            ////bll.vBatchZeroInvFromDiscount(arr);
            //bll.vBatchApprovedPanda();

            //foreach (GridViewRow row in grddisc.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        List<string> lapproval = bll.lGetApproval("claimhead", 1);
            //        Label lbfreecash = (Label)row.FindControl("lbfreecash");
            //        Label lbfreeqty = (Label)row.FindControl("lbfreeqty");
            //        Label lbdisccode = (Label)row.FindControl("lbdisccode");
            //        if ((Convert.ToDouble(lbfreecash.Text) == 0) && (Convert.ToDouble(lbfreeqty.Text) == 0))
            //        {
            //            string sSubject = "Discount deactivated " + lbdisccode.Text + " in " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString());
            //            string sMailText = "Dear Claim Team,\n\r\n\r There is some discount deactivated in Sales Order below \n\r\n\r";
            //            sMailText += "<table><tr style='background-color:green'><td>Discount Code</td><td>Disc Rmrk</td><td>Customer</td>Order No</td></tr>";
            //            sMailText += "<td>" + lbdisccode.Text + "</td><td>" + bll.vLookUp("select remark from tmst_discount where disc_cd='" + lbdisccode.Text + "'") + "</td><td>" + txcustomer.Text + "</td><td>" + txorderno.Text + "</td></tr></table>";
            //            sMailText += "\r\n Please take care and do needfull action to excluded this discount for next transaction.\r\n\n\r Wazaran Admin";
            //            bll.vSendMail(lapproval[1], sSubject, sMailText);
            //        }
            //    }
            //}
        }

    }
    protected void btcancel_Click(object sender, EventArgs e)
    {

    }
    protected void btupdman_Click(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        if (txcustomer.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Alert','Please type for search customer ! | يرجى كتابة العملاء','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            lbaddress.Text = rs["addr"].ToString();
            var culture = System.Globalization.CultureInfo.GetCultureInfo("id-ID");
            //lbcredit.Text = bll.vLookUp("select dbo.fn_getcreditlimit('" + hdcust.Value.ToString() + "')");
            lbcusttype.Text = rs["otlnm"].ToString();
            lbcustgroup.Text = rs["cusgrnm"].ToString();
            lbterm.Text = rs["term"].ToString();
            //sSalesmanCode = rs["salesman_cd"].ToString();
            lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
            txcustomer.CssClass = "form-control input-group-sm ro";
            //txclremain.Text = bll.vLookUp("select dbo.fn_remaincl('" + hdcust.Value.ToString() + "')");
            lbcredittype.Text = rs["cuscate_cd"].ToString();
            //lbcustcate.Text = rs["cuscate_cd"].ToString();
            string sCustCate = rs["cuscate_cd"].ToString();
            lbterm.Text = rs["payment_term"].ToString();
            //lbpromised.Text = "0"; // bll.vLookUp("select dbo.fn_getpaymentpromised('" + hdcust.Value.ToString() + "')");
            //lbmaxtrans.Text = "0"; //bll.vLookUp("select dbo.fn_getmaxtransamt('"+hdcust.Value.ToString()+"')");
            //lboverdue.Text = bll.vLookUp("select dbo.fn_getoverdue('" + hdcust.Value.ToString() + "')");
            // lbgroupcl.Text = "0";//bll.vLookUp("select dbo.fn_getgrpclbycust('" + hdcust.Value.ToString() + "')");
            // lbgroupovd.Text = "0";// bll.vLookUp("select dbo.fn_getbalanceduegrpbycust('" + hdcust.Value.ToString() + "')");
            // sPromised = bll.vLookUp("select dbo.fn_getlastpromised('" + hdcust.Value.ToString() + "')");
            // lbpromised.Text = sPromised;
            // lbcusgrcd.Text = bll.sGetFieldValue("cusgrcd", rs["cusgrcd"].ToString());
            //lbmaxtrans.Text = bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "')");
            //lbbalance.Text = bll.vLookUp("select dbo.fn_getbalanceinvoice('" + hdcust.Value.ToString() + "')");
            //lbcapbalance.Text = bll.vLookUp("select dbo.fn_checkcapbalance('" + hdcust.Value.ToString() + "')");
            lbvatno.Text = (rs["tax_no"].Equals(DBNull.Value) ? "N/A" : rs["tax_no"].ToString());

        }
        rs.Close();
        //cbsalesman.SelectedValue = sSalesmanCode;
        //cbsalesman_SelectedIndexChanged(sender, e);
        cbsalesman.CssClass = "form-control  ro";
        cbsalesman.Enabled = false;
        txitemsearch.CssClass = "form-control input-sm ";
        cbuom.CssClass = "form-control input-sm ";
        txqty.CssClass = "form-control input-sm ";

    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grddisc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grddisc.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
        TextBox txfreecash = (TextBox)grddisc.Rows[e.NewEditIndex].FindControl("txfreecash");
        TextBox txfreeqty = (TextBox)grddisc.Rows[e.NewEditIndex].FindControl("txfreeqty");
        //if (Convert.ToDouble(txfreecash.Text) == 0)
        //{
        //    txfreecash.CssClass = "ro";
        //    txfreeqty.CssClass = "makeitreadwrite";
        //    //txfreecash.Enabled = false;
        //    //txfreeqty.Enabled = true;
        //}
        //else { txfreeqty.CssClass = "ro"; txfreecash.CssClass = "makeitreadwrite"; }
        HiddenField hdmec = (HiddenField)grddisc.Rows[e.NewEditIndex].FindControl("hdmec");
        if (hdmec.Value.ToString() == "FG")
        {
            txfreecash.CssClass = "ro form-control input-sm";
            txfreecash.Enabled = false;
            txfreeqty.CssClass = "makeitreadwrite form-control input-sm";
            txfreeqty.Enabled = true;
        }
        if (hdmec.Value.ToString() == "CH")
        {
            txfreeqty.CssClass = "ro form-control input-sm";
            txfreeqty.Enabled = false;
            txfreecash.CssClass = "makeitreadwrite form-control input-sm";
            txfreecash.Enabled = true;
        }
        if (hdmec.Value.ToString() == "PC")
        {
            txfreeqty.CssClass = "ro form-control input-sm";
            txfreeqty.Enabled = false;
            txfreecash.CssClass = "makeitreadwrite form-control input-sm";
            txfreecash.Enabled = true;
        }
    }
    protected void grddisc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddisc.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
    }
    protected void grddisc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grddisc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbdisccode = (Label)grddisc.Rows[e.RowIndex].FindControl("lbdisccode");
        TextBox txfreeqty = (TextBox)grddisc.Rows[e.RowIndex].FindControl("txfreeqty");
        TextBox txfreecash = (TextBox)grddisc.Rows[e.RowIndex].FindControl("txfreecash");

        double dFree;
        if (!double.TryParse(txfreeqty.Text, out dFree))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free Quantity must numeric','Free Qty','warning');", true);
            return;
        }

        if (!double.TryParse(txfreecash.Text, out dFree))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free Cash must numeric','Free Cash','warning');", true);
            return;
        }
        double dfreeqty; double dfreecash;

        double.TryParse(txfreeqty.Text, out dfreeqty);
        double.TryParse(txfreecash.Text, out dfreecash);
        //if ((dfreecash<0) && (dfreeqty < 0))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free Can not less or same with zero','Free','warning');", true);
        //    return;
        //}

        //if ((dfreecash == 0) && (dfreeqty == 0))
        //{ 
        //    // Send email to musa team : 2 AGT , by IAG
        //    string sSubject = "Discount changed to Zero in " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString());
        //    string sMailText = "Dear Claim Team,/n/r/n/r" + "There is discount changed in ";
        //}

        string sMec = bll.vLookUp("select discount_mec from twrk_salesdiscount where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and disc_cd='" + lbdisccode.Text + "'").ToString();
        double dFreeOri = Convert.ToDouble(bll.vLookUp("select free_ori from twrk_salesdiscount where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and disc_cd='" + lbdisccode.Text + "'"));
        if ((sMec == "CH") || (sMec == "PC"))
        {
            if (dFreeOri < dfreecash)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free cash can not bigger than original value','Free Cash value','warning');", true);
                return;
            }
        }
        else if (sMec == "FG")
        {
            if (dFreeOri < dfreeqty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free item can not bigger than original value','Free Item value','warning');", true);
                return;
            }
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
        arr.Add(new cArrayList("@free_qty", txfreeqty.Text));
        arr.Add(new cArrayList("@free_cash", txfreecash.Text));
        bll.vUpdateWrkSalesDiscount(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
        //arr.Add(new cArrayList("@qty", txfreecash.Text));
        //if (Convert.ToDouble(txfreecash.Text) > 0)
        //{
        //    bll.vInsertWrkEditFreeCash(arr);
        //}
        grddisc.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
    }
    protected void grddisc_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdisccode = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbdisccode");
        Label lbfreeqty = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbfreeqty");
        Label lbuomfree = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbuomfree");
        HiddenField hdmec = (HiddenField)grddisc.Rows[e.NewSelectedIndex].FindControl("hdmec");
        HiddenField hddiscuse = (HiddenField)grddisc.Rows[e.NewSelectedIndex].FindControl("hddiscuse");
        string sdiscqty = string.Empty;
        sdiscqty = bll.vLookUp("select isnull(sum(free_qty),0) from twrk_salesorderfreeitem where  disc_cd='" + lbdisccode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
        double dfreeqty = Convert.ToDouble(lbfreeqty.Text);
        if (sdiscqty == "") { sdiscqty = "0"; }
        double dbalfreeqty = dfreeqty - Convert.ToDouble(sdiscqty);
        if ((dfreeqty - Convert.ToDouble(sdiscqty)) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount entry complete','warning');", true);
            return;
        }
        if ((hdmec.Value.ToString() == "CH") || (hdmec.Value.ToString() == "PC"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not available for Cash Discount','Only discount mechanis by item avalaible !','warning');", true);
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
            return;
        }

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_lookupitem.aspx?dc=" + lbdisccode.Text + "&du=" + hddiscuse.Value.ToString() + "&f=" + dbalfreeqty + "&t=to&u=" + lbuomfree.Text + "&wh="+cbwhs.SelectedValue.ToString() +"&bin="+cbbin.SelectedValue.ToString()+"');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_lookupitem_ho.aspx?so_cd=" + txorderno.Text.Trim() + "&dc=" + lbdisccode.Text + "&du=" + hddiscuse.Value.ToString() + "&f=" + dbalfreeqty + "&t=to&u=" + lbuomfree.Text + "&cust=" + hdcust.Value.ToString() + "&sp="+cbsalespoint.SelectedValue.ToString()+"');", true); 
    }
    protected void grdfree_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void chdisc_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btfree_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["salespoint"] = cbsalespoint.SelectedValue.ToString();
    }
}