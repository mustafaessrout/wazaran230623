using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookupitemcash : System.Web.UI.Page
{
    cbll bll = new cbll();
    double unitprice = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sType = string.Empty;
                lbuomfree.Text = Request.QueryString["u"];
                string sBin = Request.QueryString["bin"];
                string sWhs = Request.QueryString["wh"];
                string suom = Request.QueryString["u"];

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelWrkLookupItem(arr);
                //  bll.vDelSalesOrderFreeItem(arr);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                if (Request.QueryString["t"] != null)
                {
                    sType = Request.QueryString["t"];
                }
                string sFreeItemProduct = "I";
                if (sType == "to")
                {
                    bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                    cbwhs.SelectedValue = sWhs;
                    cbwhs_SelectedIndexChanged(sender, e);
                    cbwhs.Enabled = false;
                    //  cbbin_SelectedIndexChanged(sender, e);
                    sFreeItemProduct = bll.vLookUp("select top 1 rdfreeitem from tmst_discount where disc_cd in (select disc_cd from twrk_salesdiscount where discount_mec='CG' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
                    lbbaldiscount.Text = bll.vLookUp("select sum(free_cash) from twrk_salesdiscount where discount_mec='CG' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                }
                else if (sType == "cn")
                {
                    string sSalesman = Request.QueryString["sal"];
                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vBindingComboToSp(ref cbwhs, "sp_tmst_vehicle_get", "vhc_cd", "vhc_desc", arr);
                    string sVhc = bll.vLookUp("select vhc_cd from tmst_vehicle where emp_cd='" + sSalesman + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    cbwhs.SelectedValue = sVhc;
                    cbwhs_SelectedIndexChanged(sender, e);
                    cbwhs.Enabled = false;
                    //   cbbin_SelectedIndexChanged(sender, e);

                    // cbbin_SelectedIndexChanged(sender, e);
                    sFreeItemProduct = bll.vLookUp("select top 1 rdfreeitem from tmst_discount where disc_cd in (select disc_cd from twrk_canvasdiscount where discount_mec='CG' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
                    lbbaldiscount.Text = bll.vLookUp("select sum(free_cash) from twrk_canvasdiscount where discount_mec='CG' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                }
                bll.vBindingFieldValueToCombo(ref cbuomfree, "uom");
                cbuomfree.SelectedValue = suom;
                cbuomfree.CssClass = "ro";
                cbbin.SelectedValue = sBin;
                cbbin_SelectedIndexChanged(sender, e);
                cbbin.CssClass = "ro";
                // cbwhs_SelectedIndexChanged(sender, e);
                //string sDiscNo = Request.QueryString["dc"];
                
                arr.Clear();
                if (sFreeItemProduct == "I")
                {
                    //arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
                    arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@type", sType));
                    arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@so_cd", Request.QueryString["so_cd"].ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_item_getbywhs", arr);
                }
                else if (sFreeItemProduct == "G")
                {
                    //arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
                    arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@type", sType));
                    arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@so_cd", Request.QueryString["so_cd"].ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_product_getbywhs", arr);
                }
                cbuomfree_SelectedIndexChanged(sender, e);
                arr.Clear();                
                lbuomfree.Text = bll.sGetControlParameter("currency");

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void grdsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            Label lbitemcode = (Label)grdsearch.SelectedRow.FindControl("lbitemcode");
            Label lbitemname = (Label)grdsearch.SelectedRow.FindControl("lbitemname");
            Label lbarabic = (Label)grdsearch.SelectedRow.FindControl("lbarabic");
            Label lbsize = (Label)grdsearch.SelectedRow.FindControl("lbsize");
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@item_nm", lbitemname.Text));
            arr.Add(new cArrayList("@item_arabic", lbarabic.Text));
            arr.Add(new cArrayList("@size", lbsize.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertWrkLookupItem(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "okclose", "window.opener.updpnl()", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "okupd", "window.close()", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdsearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdsearch.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@stype", Request.QueryString["t"].ToString()));
            bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_freeitem_cash_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btprice_Click(string item, string uom, EventArgs e)
    {
        try
        {
            unitprice = 0;
            double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + item + "','" + uom + "')"));
            if (dConv == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no price setup or no setup UOM conversion!','Contact to wazaran admin','warning');", true);
                return;
            }
            double dPrice = 0;
            string sCustType = "";
            sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + Request.QueryString["cust"] + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            double dAdjust = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + item + "','" + Request.QueryString["cust"] + "','" + uom + "','" + Request.Cookies["sp"].Value.ToString() + "')"));
            if (dAdjust > 0)
            {
                dPrice = dAdjust;
            }
            else
            {

                dPrice = bll.dGetItemPrice(item, sCustType, uom);
            }
            if (dPrice == 0)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price not yet setup or item conversion not setup!','Contact wazaran admin','warning');", true);

                return;
            }
            unitprice = dPrice;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            double dTot = 0; double dTemp = 0;
            List<cArrayList> arr = new List<cArrayList>();
            //string sDiscNo = Request.QueryString["dc"].ToString();
            string sUOM = Request.QueryString["u"];
            if (sUOM != cbuomfree.SelectedValue.ToString())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('UOM Free must same with UOM Deliver !','Check UOM','warning');", true);
                return;

            }
            foreach (GridViewRow row in grdsearch.Rows)
            {
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                TextBox txqty = (TextBox)row.FindControl("txqty");
                Label lbstock = (Label)row.FindControl("lbstock");
                if (!double.TryParse(txqty.Text, out dTot))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please entry only numeric !','Numeric Input','warning');", true);
                    return;
                }
                else
                {
                    if (Convert.ToDouble(lbstock.Text) > 0)
                    {
                        if (dTot > Convert.ToDouble(lbstock.Text))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Stock is not enough !','Please re-fill stock','warning');", true);
                            return;
                        }
                    }
                    dTemp += dTot;
                }

            }


            foreach (GridViewRow row in grdsearch.Rows)
            {
                Label lbunitprice = (Label)row.FindControl("lbunitprice");
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                TextBox txqty = (TextBox)row.FindControl("txqty");
                if (txqty.Text != "0")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@free_qty", txqty.Text));
                    arr.Add(new cArrayList("@disc_cd", ""));
                    arr.Add(new cArrayList("@unitprice", lbunitprice.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    if (Request.QueryString["t"] == "to")
                    {
                        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
                        bll.vInsertSalesOrderFreeCashItem(arr);
                    }
                    else if (Request.QueryString["t"] == "cn")
                    {
                        arr.Add(new cArrayList("@vhc_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
                        bll.vInsertWrkCanvasOrderFreeCashItem(arr);
                    }
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "SendData();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (Request.QueryString["t"] == "to")
            {
                arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            else if (Request.QueryString["t"] == "cn")
            {
                arr.Add(new cArrayList("@vhc_cd", cbwhs.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbbin, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);
            }
            cbbin_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdsearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sStock = string.Empty;
                string sStocktwrk = string.Empty;
                string sStockmst = string.Empty;
                double qty;
                Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
                Label lbstock = (Label)e.Row.FindControl("lbstock");
                Label lbunitprice = (Label)e.Row.FindControl("lbunitprice");
                if (Request.QueryString["t"] == "to")
                {
                    btprice_Click(lbitemcode.Text, Request.QueryString["u"], e);
                    lbunitprice.Text = unitprice.ToString();
                    //sStock = bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text +  "','CTN','" + cbuomfree.SelectedValue.ToString() + "',stock_display) from tmst_stock where whs_cd='" + cbwhs.SelectedValue.ToString() + "' and bin_cd='" + cbbin.SelectedValue.ToString() + "' and item_cd='" + lbitemcode.Text + "'");
                    //modify by yanto 12-4-2016
                    //sStockmst = bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','CTN','" + cbuomfree.SelectedValue.ToString() + "',stock_display) from tmst_stock where whs_cd='" + cbwhs.SelectedValue.ToString() + "' and bin_cd='" + cbbin.SelectedValue.ToString() + "' and item_cd='" + lbitemcode.Text + "'");
                    //sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' union all select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "')a");
                    //if (sStockmst == "") { sStockmst = "0"; }
                    //if (sStocktwrk == "") { sStocktwrk = "0"; }
                    //qty=Convert.ToDouble(sStockmst)-Convert.ToDouble(sStocktwrk);
                    DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    sStockmst = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 26-6-2016 
                    string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','CTN','" + cbuomfree.SelectedValue.ToString() + "',1)");
                    if (Request.QueryString["so_cd"] == "NEW")
                        sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' union all select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')a");
                    else
                        sStocktwrk = bll.vLookUp("select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    if (sStockmst == "") { sStockmst = "0"; }
                    if (sStocktwrk == "") { sStocktwrk = "0"; }
                    qty = (Convert.ToDouble(sStockmst) - Convert.ToDouble(sStocktwrk)) * Convert.ToDouble(sQTY);
                    sStock = Convert.ToString(qty);
                    //-------------------------
                }
                else if (Request.QueryString["t"] == "cn")
                {
                    btprice_Click(lbitemcode.Text, Request.QueryString["u"], e);
                    lbunitprice.Text = unitprice.ToString();
                    //sStock = bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','CTN','" + cbuomfree.SelectedValue.ToString() + "',stock_display) from tmst_van_stock where vhc_cd='" + cbwhs.SelectedValue.ToString() + "' and bin_cd='" + cbbin.SelectedValue.ToString() + "' and item_cd='" + lbitemcode.Text + "'");
                    //modify by yanto 12-4-2016
                    //sStockmst = bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','CTN','" + cbuomfree.SelectedValue.ToString() + "',stock_display) from tmst_van_stock where vhc_cd='" + cbwhs.SelectedValue.ToString() + "' and bin_cd='" + cbbin.SelectedValue.ToString() + "' and item_cd='" + lbitemcode.Text + "'");
                    //sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' union all select sum(free_qty) from twrk_canvasorderfreeitem where item_cd='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "')a");
                    //if (sStockmst == "") { sStockmst = "0"; }
                    //if (sStocktwrk == "") { sStocktwrk = "0"; }
                    //qty = Convert.ToDouble(sStockmst) - Convert.ToDouble(sStocktwrk);
                    DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    sStockmst = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue + "','VS','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 26-6-2016 
                    string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','CTN','" + cbuomfree.SelectedValue.ToString() + "',1)");
                    if (Request.QueryString["so_cd"] == "NEW")
                        sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' union all select sum(free_qty) from twrk_canvasorderfreeitem where item_cd='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')a");
                    else
                        sStocktwrk = bll.vLookUp("select sum(free_qty) from twrk_canvasorderfreeitem where item_cd='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    if (sStockmst == "") { sStockmst = "0"; }
                    if (sStocktwrk == "") { sStocktwrk = "0"; }
                    qty = (Convert.ToDouble(sStockmst) - Convert.ToDouble(sStocktwrk)) * Convert.ToDouble(sQTY);
                    sStock = Convert.ToString(qty);
                    //-------------------------
                }
                if (sStock == "")
                {
                    sStock = "0";
                }
                lbstock.Text = sStock;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void vBindingGrid()
    {
        try
        {
            string sType = string.Empty;
            string sFreeItemProduct = "I";
            if (Request.QueryString["t"] != null)
            {
                sType = Request.QueryString["t"];
            }

            List<cArrayList> arr = new List<cArrayList>();
            //string sDiscNo = Request.QueryString["dc"];
            //string sFreeItemProduct = bll.vLookUp("select rdfreeitem from tmst_discount where disc_cd='" + sDiscNo + "'");
            
            if (sType == "to")
            {
                sFreeItemProduct = bll.vLookUp("select top 1 rdfreeitem from tmst_discount where disc_cd in (select disc_cd from twrk_salesdiscount where discount_mec='CG' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
            }
            else
            {
                sFreeItemProduct = bll.vLookUp("select top 1 rdfreeitem from tmst_discount where disc_cd in (select disc_cd from twrk_canvasdiscount where discount_mec='CG' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
            }
            
            arr.Clear();
            if (sFreeItemProduct == "I")
            {
                //arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
                arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
                arr.Add(new cArrayList("@type", sType));
                arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@so_cd", Request.QueryString["so_cd"].ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_item_direct_getbywhs", arr);
            }
            else if (sFreeItemProduct == "G")
            {
                //arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
                arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
                arr.Add(new cArrayList("@type", sType));
                arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@so_cd", Request.QueryString["so_cd"].ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_product_direct_getbywhs", arr);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grdsearch.DataSource = null;
            grdsearch.DataBind();
            vBindingGrid();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookupitemcash");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbuomfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdsearch.DataSource = null;
        grdsearch.DataBind();
        vBindingGrid();
    }
}