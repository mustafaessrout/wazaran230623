using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
public partial class fm_takeorderentry2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dQtyOrder = 0;
    double dSubTotal = 0;
    double dTotShipment = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        if (!IsPostBack)
        {
            try
            {
                if (bll.vLookUp("select dbo.fn_gettransblock('" + Request.Cookies["SP"].Value.ToString() + "')") != "ok")
                {
                    Response.Redirect("alert_denied.aspx?m=1");
                    return;
                }

                string sSystemDate = Request.Cookies["waz_dt"].Value.ToString();
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelWrkSalesOrderDtl(arr);
                bll.vDelSalesOrderFreeItem(arr);
                bll.vDelWrkDiscountCash(arr);
                bll.vDelWrkSalesDiscount(arr);
                bll.vDelTwrkEditFreeCash(arr);
                bll.vDeleteWrkSalesOrderDtlTax(arr);
                //bll.vBindingFieldValueToCombo(ref cbuom, "uom");
                bll.vBindingFieldValueToCombo(ref cbuom, "uom", false);
                arr.Clear();
                txqty.Attributes.Add("onblur", "ShowProgress();SetDeliver()");
                dtorder.Text = Request.Cookies["waz_dt"].Value.ToString();
                dtdelivery.Text = Request.Cookies["waz_dt"].Value.ToString();
                lbsalespoint.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='so_sta_id' and fld_valu='N'");
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                // cbwhs.CssClass = "ro";
                cbwhs_SelectedIndexChanged(sender, e);
                string sDefBin = bll.vLookUp("select top 1 qry_data from tmap_query where qry_cd='bin_branch'");
                cbbin.SelectedValue = sDefBin;
                arr.Clear();

                BindEmployeConditional();
                //arr.Add(new cArrayList("@qry_cd", "driver"));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //  bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ", arr);
                bll.vBindingFieldValueToCombo(ref cbsourceorder, "source_order");
                bll.vBindingFieldValueToCombo(ref cbsourseinfo, "source_info");
                cbsourceorder_SelectedIndexChanged(sender, e);
                //   cbdriver_SelectedIndexChanged(sender, e);
                cbsourseinfo_SelectedIndexChanged(sender, e);
                vInit();
                //    rdonoff.SelectedValue = "ON";
                arr.Clear();
                arr.Add(new cArrayList("@qry_cd", "merchandcd"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbmerchendizer, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                //  bll.vBindingFieldValueToCombo(ref cbvatmethod, "vatmethod");
                cbvehicle.CssClass = "form-control  ro";
                btprint.CssClass = "divhid";
                btprintinvoice.CssClass = "divhid";
                btprintfreeinv.CssClass = "divhid";
                btsave.CssClass = "divhid";
                btnew.CssClass = "btn btn-success btn-new";
                hdto.Value = "";
                cbsourseinfo.CssClass = "form-control ro";
                // cbvatmethod.CssClass = "form-control ro";
                txmanualinv.CssClass = "form-control ro require";
                txmanualno.CssClass = "form-control ro";
                //txpocust.CssClass = cd.csstextro;
                //dtcustpo.CssClass = cd.csstextro;
                hdto.Value = "";
                chdisc.Checked = true;
                chcash.Checked = true;
                DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string day = Convert.ToString(dtOrd.Day);
                string mon = Convert.ToString(dtOrd.Month);
                string yrs = Convert.ToString(dtOrd.Year);
                string date = yrs + "-" + mon + "-" + day;
                txitemsearch_AutoCompleteExtender.ContextKey = date;
                showProforma.Visible = false;
                lbtitlevat.Text = (double.Parse(bll.sGetControlParameter("vat")) * 100).ToString();
                //txqty.Attributes.Add("type", "number");
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
                Response.Redirect("fm_ErrorPage.aspx");
            }
            
        }
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
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    void CleanWrk()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelWrkSalesOrderDtl(arr);
            //bll.vDelWrkdiscountcash(arr);
            bll.vDelSalesOrderFreeItem(arr);
            bll.vDelWrkDiscountCash(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void vInit()
    {
        txremark.Text = "";
        txremark.CssClass = "form-control  ro require";
        txorderno.Text = "NEW";
        txorderno.CssClass = "form-control input-group-sm ro";
        btnew.CssClass = "btn btn-success btn-new";
        btsave.CssClass = "btn btn-warning btn-save";
        txinvoiceno.CssClass = "form-control  ro";
        btprint.CssClass = "divhid";
        btprintfreeinv.CssClass = "divhid";
        btprintinvoice.CssClass = "divhid";
        btedit.CssClass = "divhid";
        btcancel.CssClass = "divhid";
        txtabno.CssClass = "form-control  ro";
        txcustomer.CssClass = "form-control  ro";
        txitemsearch.CssClass = "form-control input-sm  ro";
        cbuom.CssClass = "form-control input-sm ro";
        txstockcust.CssClass = "form-control input-sm  ro";
        txqty.CssClass = "form-control input-sm   ro";
        txshipmen.CssClass = "form-control input-sm  ro";
        btdisc.CssClass = "divhid";
    }

    void vNew()
    {
        txcustomer.CssClass = "form-control input-group-sm";
        ////txinvoiceno.Enabled = true;
        ////txinvoiceno.CssClass = "makeitreadwrite";
        txitemsearch.CssClass = "form-control input-sm";
        //txmanualinv.Enabled = true;
        //txmanualinv.CssClass = "makeitreadwrite";
        txorderno.CssClass = "form-control  ro";
        txqty.CssClass = "form-control input-sm  ";
        txremark.CssClass = "form-control ";
        // txshipmen.Enabled = true;
        // txshipmen.CssClass = "makeitreadwrite";
        txstockcust.CssClass = "form-control input-sm ";
        dtorder.CssClass = "form-control  ro";
        cbsalesman.CssClass = "form-control ";
        cbbin.CssClass = "form-control ";
        //2-JAN-2016
        cbdriver.CssClass = "form-control  ro";
        cbuom.CssClass = "form-control input-sm";
    }
    void DisableControl()
    {
        txcustomer.CssClass = "form-control input-group-sm ro";
        //txinvoiceno.Enabled = false;
        //txinvoiceno.CssClass = "makeitreadonly";
        txitemsearch.CssClass = "form-control input-sm ro";
        //txmanualinv.Enabled = false;
        //txmanualinv.CssClass = "makeitreadonly";
        txorderno.CssClass = cd.csstextro;
        txqty.CssClass = cd.csstextro;
        txremark.CssClass = cd.csstextro;
        txshipmen.CssClass = cd.csstextro;
        txstockcust.CssClass = cd.csstextro;
        dtorder.CssClass = cd.csstextro;
        cbsalesman.CssClass = cd.csstextro;
        cbbin.CssClass = cd.csstextro;
        cbdriver.CssClass = cd.csstextro;

    }
    void EnableControl()
    {
        txcustomer.CssClass = "form-control input-group-sm";
        ////txinvoiceno.Enabled = true;
        ////txinvoiceno.CssClass = "makeitreadwrite";
        txitemsearch.CssClass = "form-control input-sm ";
        //txmanualinv.Enabled = true;
        //txmanualinv.CssClass = "makeitreadwrite";
        txorderno.CssClass = "form-control ";
        txqty.CssClass = "form-control input-sm  ";
        txremark.CssClass = "form-control ";
        txshipmen.CssClass = "form-control input-sm ";
        txstockcust.CssClass = "form-control input-sm ";
        dtorder.CssClass = "form-control  ro";
        cbsalesman.CssClass = "form-control  ro";
        cbbin.CssClass = "form-control ";
        cbdriver.CssClass = "form-control ";
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
        // 2019-03-26
        // Can not dulpcate PO number in same year --
        if (lbcusttype.Text == "Key Account" && lblPO.Text == "Enter PO Number")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter PO number','Wrong PO " + txpocust.Text + "','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            //return;

        }
        else if (lbcusttype.Text == "Key Account")
        {
            if (lbcustcate.Text != "SP")
            {
                //if (txpocust.Text.TrimEnd().TrimStart() != "")
                //{
                //    if (hdto.Value == "")
                //    {
                //        string sPO = bll.vLookUp("select dbo.fn_checkDuplicatePOSameYear('" + txpocust.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
                //        if (sPO != "ok")
                //        {
                //            lblPO.ForeColor = System.Drawing.Color.Red;
                //            lblPO.Text = "Invalid PO Number";
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not duplicate PO number in same year!','Invalid PO Number','warning');", true);
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                //            return;
                //        }
                //        else
                //        {
                //            lblPO.ForeColor = System.Drawing.Color.Green;
                //            lblPO.Text = "Valid PO Number";
                //        }
                //    }
                //    else
                //    {
                //        string currPO = bll.vLookUp("select pocust_no from tsalesorder_info where so_cd='"+hdto.Value+"' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                //        if(txpocust.Text.TrimEnd().TrimStart() != currPO)
                //        {
                //            string sPO = bll.vLookUp("select dbo.fn_checkDuplicatePOSameYear('" + txpocust.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
                //            if (sPO != "ok")
                //            {
                //                lblPO.ForeColor = System.Drawing.Color.Red;
                //                lblPO.Text = "Invalid PO Number";
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not duplicate PO number in same year!','Invalid PO Number','warning');", true);
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                //                return;
                //            }
                //            else
                //            {
                //                lblPO.ForeColor = System.Drawing.Color.Green;
                //                lblPO.Text = "Valid PO Number";
                //            }
                //        }
                //        else
                //        {
                //            lblPO.ForeColor = System.Drawing.Color.Green;
                //            lblPO.Text = "Valid PO Number";
                //        }
                //    }
                    
                //}
                //
                //else if (lblPO.Text == "Enter PO Number" || lblPO.Text == "Invalid PO Number")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Enter Valid PO number,' Wrong PO " + txpocust.Text + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                //    return;

                //}
            }
        }

        //Mod : 5 Mar 2018
        // Add Checking for existing PO Customer No
        //if (lbcusttype.Text == "Key Account")
        //{
        //    if (txpocust.Text.TrimEnd().TrimStart() != "")
        //    {
        //        string sPO = bll.vLookUp("select dbo.fn_checkexistingpocust('" + txpocust.Text + "')");
        //        if (sPO != "ok")
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sPO + "','" + txpocust.Text + "','warning');", true);
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //            return;
        //        }
        //    }

        //}
        double dInvoice = Convert.ToDouble(bll.vLookUp("select dbo.fn_getsuminvoice('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"));
        double sCL = Convert.ToDouble(bll.vLookUp("select credit_limit from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));
        //Check No Legal Customer____________________________________________________________________________
        string sCustCate = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
        Label sSubTotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
        if (sCustCate == "NL")
        {
            double dNL = Convert.ToDouble(sCL) - dInvoice - Convert.ToDouble(sSubTotal.Text);
            if (dNL < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer No Legal can not make transaction exceeded SAR 2000','Check Amount Transaction','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Enter Item to be ordered !','Order item can not empty ','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            return;
        }
        //  if (rdonoff.SelectedValue.ToString() == "ON") __________________________________
        if (chdisc.Checked)
        {
            if (grddisc.Rows.Count > 0)
            {
                foreach (GridViewRow row in grddisc.Rows)
                {
                    HiddenField hdmec = (HiddenField)row.FindControl("hdmec");
                    Label lbfreeqty = (Label)row.FindControl("lbfreeqty");
                    Label lbfreecash = (Label)row.FindControl("lbfreecash");
                    if (hdmec.Value.ToString() == "FG")
                    {
                        if ((grdfree.Rows.Count == 0) && (Convert.ToDouble(lbfreeqty.Text) > 0))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Free must be selected !','select free item','warning');", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "HideProgress();", true);
                            return;
                        }
                    }
                }
            }
        }
        //Enh - Stop transaction when this is more than clock 12:00 PM
        string scd = bll.vLookUp("select dbo.fn_checkdeadlinetime('"+ Request.Cookies["sp"].Value.ToString() + "')");
        if (scd != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Block entry because deadline to daily closing !','Please daily closing !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            return;
        }
        // Credit limit more than 2000 for NL

        if (dInvoice >= sCL)
        {
            sCLMsg = "Credit Limit Exceeded";
            Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
        }

        
        if (hdto.Value == "")
        {
            //string sPOCust = bll.vLookUp("select dbo.fn_checkpocust('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            //if (sPOCust == "EXISTS")
            //{
            //    if ((txpocust.Text == "") || (dtcustpo.Text == ""))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer must put number and date PO!','Put PO number and Date!','warning');", true);
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            //        return;
            //    }
            //}
        } 


        btsave.CssClass = "divhid";

            sTermCd = bll.vLookUp("select payment_term from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@so_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@so_typ", "TO"));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@term_cd", sTermCd));
            arr.Add(new cArrayList("@currency_cd", bll.sGetControlParameter("currency")));
            arr.Add(new cArrayList("@ref_no", txmanualno.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@so_sta_id", "N"));
            arr.Add(new cArrayList("@so_source", cbsourceorder.SelectedValue.ToString()));
            arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
            // arr.Add(new cArrayList("@rdonoff", rdonoff.SelectedValue.ToString()));
            if (chdisc.Checked)
            { arr.Add(new cArrayList("@rdonoff", "ON")); }
            else
            { arr.Add(new cArrayList("@rdonoff", "OFF")); }
            arr.Add(new cArrayList("@manual_no", txmanualno.Text));
            arr.Add(new cArrayList("@tabno", txtabno.Text));
            //   arr.Add(new cArrayList("@vatmethod", cbvatmethod.SelectedValue.ToString()));
            if (hdto.Value.ToString() == "") //New data 
            {
                bll.vInsertMstSalesOrder(arr, ref sSoNo);
            }
            else
            {   //-------------------------------------This is delete from previouse transaction
                sSoNo = hdto.Value.ToString();
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelSalesorderDtl(arr);
                bll.vDelDOSalesInvoiceDiscCash(arr);
                bll.vDelSalesOrderDiscCash(arr);
                bll.vDelSalesOrderDisccashitem(arr);
                bll.vDelSalesorderShipment(arr);
                bll.vDelDosalesInvoiceFree(arr);
                bll.vDelDosalesDtl(arr);
                bll.vDelDosalesinvoiceDtl(arr);
                bll.vDelSalesOrderDiscItem(arr);
                bll.vDelSalesOrderDiscHist(arr);
                bll.vUpdateDosalesInvoiceByZero(arr);
                //-------------------------------------------------------------------------------
            }
            txorderno.Text = sSoNo;
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertSalesOrderDtl(arr);
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertSalesOrderShipment(arr);
            arr.Clear();
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            // if (rdonoff.SelectedValue.ToString() == "ON")
            if (chdisc.Checked)
            {
                //This is not running batch takeorder ---------------------------------------------
                if (chdirect.Checked == true) { bll.vBatchTakeOrderDiscountDirect(arr); } else { bll.vBatchTakeOrderDiscount(arr); }
                //bll.vBatchTakeOrderDiscount(arr);
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", sSoNo));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                if (chdirect.Checked == true) { bll.vBatchEditDiscountDirect(arr); } else { bll.vBatchEditDiscount(arr); }
                //bll.vBatchEditDiscount(arr);

            }
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            // if (rdonoff.SelectedValue.ToString() == "ON")
            if (chdisc.Checked)
            {
                bll.vInsertSalesorderdiscitem_ins(arr);
                // IAG : 25-MAR-2017 : Invoice Ledger actiaved
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", sSoNo));
                // bll.vInsertDosalesInvoiceLedger(arr);
            }
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //if (rdonoff.SelectedValue.ToString() == "ON")
            if (chdisc.Checked)
            {
                bll.vInsertSalesOrderDiscHist(arr);
            }
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", sSoNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //if (rdonoff.SelectedValue.ToString() == "ON")
            if (chdisc.Checked)
            {
                bll.vBatchGivenDiscount(arr);
            }

            // Check if invoice already created ------------------------------------------------------------------------
            while (true)
            {
                if (Convert.ToInt32(bll.vLookUp("select count(1) from tmst_dosales where so_cd='" + sSoNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'")) > 0)
                {
                    if (chdirect.Checked == true) { bll.vInsertDosalesInvoiceFreeFromFreeItemDirect(arr); } else { bll.vInsertDosalesInvoiceFreeFromFreeItem(arr); }
                    //bll.vInsertDosalesInvoiceFreeFromFreeItem(arr);
                    break;
                }
            }
            //      }
            txinvoiceno.Text = bll.vLookUp("select inv_no from tmst_dosales where so_cd='" + sSoNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
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
            arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            arr.Add(new cArrayList("@manual_no", txmanualinv.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateDoSalesInvoice(arr);
            //---------------------------------------------------------------
            //VAT Enhancement : 19 Oct 2017
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            if (chdirect.Checked == true) { bll.vBatchVatDirect(arr); } else { bll.vBatchVat(arr); }
            //bll.vBatchVat(arr);
            //--------------------------------------------------------------
            SqlDataReader rs = null;
            // 2017-1-11
            arr.Clear(); string sMsg = string.Empty;
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetDosalesInvoiceByDueDate(arr, ref rs);
            while (rs.Read())
            {
                if (sMsg == string.Empty)
                { sMsg = sMsg + rs["inv_no"].ToString(); }
                else
                { sMsg = sMsg + "," + rs["inv_no"].ToString(); }

            }
            rs.Close();
            if (((sMsg != string.Empty) || (sCLMsg != string.Empty)) && !chcash.Checked)
            // if (!chcash.Checked)
            {
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", sSoNo));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@original_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@invoice_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@loading_dt", DateTime.ParseExact(dtdelivery.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@app_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@app_sta_id", "A")); // Default app_sta_id='W'
                arr.Add(new cArrayList("source_info", cbsourseinfo.SelectedValue.ToString()));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@paidbycash", chcash.Checked));
                arr.Add(new cArrayList("@directsales", chdirect.Checked));
                arr.Add(new cArrayList("@prevstk", 0));
                // Walking on the moon ---------------------------------------21-Jan-18-------------------------------------
                string sOtlCode = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                if (bll.vLookUp("select dbo.fn_checkordermustwithpo('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')") == "ok") // This exist 
                {
                    arr.Add(new cArrayList("@pocust_no", txpocust.Text.TrimEnd().TrimStart().Replace(" ", String.Empty)));
                    arr.Add(new cArrayList("@pocust_dt", System.DateTime.ParseExact(dtcustpo.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                }
                //----------------------------------------------------------------------------------------------------------
                if (cbsourseinfo.SelectedValue.ToString() == "MERCHANDIZER")
                {
                    arr.Add(new cArrayList("merchandizer_cd", cbmerchendizer.SelectedValue.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("merchandizer_cd", null));
                }
                bll.vInsertSalesorderInfo(arr);
                btprintinvoice.CssClass = "divhid";
                btprintfreeinv.CssClass = "divhid";
                //btprint.Visible = false;
                btprint.CssClass = "btn btn-default ";
                List<string> lapproval = bll.lGetApproval("salesorder", 1); // Before changed
                // 11 Jul 2016 , change destination approval, to direct spv : By IAG
                //------------------------------------------------------------------------------------------
                double dSelisih = 0;

                if ((sCL - dInvoice) < 0)
                {
                    dSelisih = dInvoice - sCL;
                }
                //Riyadh 30 Sep 2016
                double dThreshold = Convert.ToDouble(bll.sGetControlParameter("pctlimitcredit"));

                btprint.CssClass = "divhid";
                //----------------------------------- End Changed ------------------------------------------
                Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
                double dTotAmt = Convert.ToDouble(lbtotsubtotal.Text);
                //Random nRdm = new Random();
                //int token = nRdm.Next(1000, 9999);
                //double tokenmail = nRdm.Next();
                //Uri urlnya = Request.Url;
                //string host = urlnya.GetLeftPart(UriPartial.Authority);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", sSoNo));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@original_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@invoice_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@loading_dt", DateTime.ParseExact(dtdelivery.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@app_dt", DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@app_sta_id", "A"));
                arr.Add(new cArrayList("source_info", cbsourseinfo.SelectedValue.ToString()));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@paidbycash", chcash.Checked));
                arr.Add(new cArrayList("@directsales", chdirect.Checked));
                arr.Add(new cArrayList("@prevstk", 0));
                // Walking on the moon ---------------------------------------21-DEC-18-------------------------------------Othman
                string sOtlCode = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                if (bll.vLookUp("select dbo.fn_checkordermustwithpo('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')") == "ok") // This exist 
                {
                    arr.Add(new cArrayList("@pocust_no", txpocust.Text.TrimEnd().TrimStart().Replace(" ", String.Empty)));
                    arr.Add(new cArrayList("@pocust_dt", ((dtcustpo.Text == "") ? "" : System.DateTime.ParseExact(dtcustpo.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString()) ));
                }
                //----------------------------------------------------------------------------------------------------------
                if (cbsourseinfo.SelectedValue.ToString() == "MERCHANDIZER")
                {
                    arr.Add(new cArrayList("merchandizer_cd", cbmerchendizer.SelectedValue.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("merchandizer_cd", null));
                }
                bll.vInsertSalesorderInfo(arr);
                btprintinvoice.CssClass = "divhid";
                btprintfreeinv.CssClass = "divhid";
                //btprint.Visible = true;
                btprint.CssClass = "btn btn-default ";

            }
            // Check promised : 11 Nov 2017 : Purposed : Promised New Flow and Rule
            if (lbpromisedno.Text != "")
            {
                arr.Clear();
                arr.Add(new cArrayList("@promised_cd", lbpromisedno.Text));
                arr.Add(new cArrayList("@promised_sta_id", "T"));
                arr.Add(new cArrayList("@old_promised_sta_id", "N"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdatePromisePaymentByStatus(arr);
            }

            // Additonal for Special Customer : 17 Nov 2019 : Add Discount without disc_cd for NE from tcontrol_parameter 
            if (lbcustcate.Text == "SP")
            {
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", sSoNo));
                arr.Add(new cArrayList("@proforma_inv", hdproforma.Value));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchDiscountSpecialCustomer(arr);
            }

            dtdelivery.CssClass = cd.csstextro;
            btsave.CssClass = "divhid";
            txmanualno.CssClass = cd.csstextro;
            txitemsearch.CssClass = cd.csstextro;
            txqty.CssClass = cd.csstextro;
            hditem.Value = "";
            txstockcust.CssClass = cd.csstextro;
            cbuom.CssClass = cd.csstextro;
            txshipmen.CssClass = cd.csstextro;
            grd.Columns[8].Visible = false;
            hddo.Value = bll.vLookUp("select do_no from tmst_dosales where so_cd='" + sSoNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            btprint.CssClass = "btn btn-info";
            btdisc.CssClass = "divhid";
            // Activated driver  - 2 JAN 2015 IA----------------------------------------------------------------
            arr.Clear();
            cbdriver.CssClass = cd.csstext;
            cbvehicle.CssClass = cd.csstext;
            BindEmployeConditional();
            //arr.Add(new cArrayList("@qry_cd", "driver"));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ", arr);
            cbdriver_SelectedIndexChanged(sender, e);
            txmaninvfreeno.CssClass = cd.csstext;
            txmanualinv.CssClass = cd.csstext;
            lbvat.Text = bll.vLookUp("select sum(vat) from tdosalesinvoice_dtl where inv_no='" + txinvoiceno.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Take order has been saved', 'Take Order no : " + sSoNo + "', 'success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            // New Enhancement : 12-June-17 by IAG
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@so_cd", sSoNo));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBatchZeroInvFromDiscount(arr);
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
    protected void cbsotype_SelectedIndexChanged(object sender, EventArgs e)
    {

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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Item can not sales for this customer in pcs, Contact Faruk','warning');", true);
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
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcustomer_AutoCompleteExtender.ContextKey = cbsalesman.SelectedValue.ToString();
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
            //--------------- IA 19-Sep-16 : Delete all wrk for re calculation discount--------------
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));       //-
                                                                                                  // bll.vDeleteWrkDiscountRelated(arr);                                                   //-
                                                                                                  //---------------------------------------------------------------------------------------
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            if (chdirect.Checked == true) { bll.vBatchTakeOrderDiscountDirect(arr); } else { bll.vBatchTakeOrderDiscount(arr); }
            //bll.vBatchTakeOrderDiscount(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
            grd.DataSource = null;
            grd.DataBind();
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);

            // Direct Sales Discount 
            if (chdirect.Checked)
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grddirect, "sp_twrk_salesdiscount_direct_get", arr);
            }

            grddisc.CssClass = "table tables-striped mygrid";
            btsave.CssClass = "btn btn-warning btn-save";
            btnew.CssClass = "btn btn-success btn-new";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
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
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString() + " - " + rs["cust_arabic"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
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

            string sSalesBlock = bll.vLookUp("select dbo.fn_checksalesblock('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sSalesBlock != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused by document not completed','" + sSalesBlock + "','warning');", true);
                hdcust.Value = ""; txcustomer.Text = "";
                return;
            }
            //By IAG - 9 Oct 2016
            lbmessage.Text = bll.vLookUp("select dbo.fn_checksalesblockwarning('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (lbmessage.Text != "ok")
            {
                lbcust.Text = txcustomer.Text + "(" + bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + ")";
                popupcust.Show();
            }
            hdcust_otlcd.Value = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            Response.Cookies["otlcd"].Value = hdcust_otlcd.Value;

            // Enh : 9 Oct 2017 : Change BO Checking logic - IAG
            string sCheckDeniedBO = bll.vLookUp("select dbo.fn_checkdenytransaction('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if ((sCheckDeniedBO != "ok"))
            {
                string sCheckTablet = bll.vLookUp("select dbo.fn_checktofromtablet('" + hdto.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                if (sCheckTablet != "ok")
                {
                    hdcust.Value = ""; txcustomer.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Transaction Denied!','" + sCheckDeniedBO + "','warning');", true);
                    return;
                }
            }

            string sPromised = bll.vLookUp("select dbo.fn_checkpaymentpromised('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sPromised != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused by promised over due','Payment promised must applied!','warning');", true);
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

            lbcapcredit.Text = bll.vLookUp("select dbo.fn_checkcl('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            lbpromisedno.Text = bll.vLookUp("select promised_cd from tpayment_promised where cust_cd='" + hdcust.Value.ToString() + "' and promised_sta_id='N' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (lbpromisedno.Text == "")
            { lbpromisedno.Text = "There is no promised for this customer"; }
            //Walking on the moon 
            if (bll.vLookUp("select dbo.fn_checkpocust('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')") == "EXISTS")
            {
                txpocust.CssClass = cd.csstext;
                dtcustpo.CssClass = cd.csstext;
            }
            else { txpocust.CssClass = cd.csstext; dtcustpo.CssClass = cd.csstext; }
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
                lbcredit.Text = bll.vLookUp("select dbo.fn_getcreditlimit('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbcusttype.Text = rs["otlnm"].ToString();
                if (rs["otlnm"].ToString() == "Key Account")
                {
                    lblPO.Text = "Enter PO Number";
                    lblPO.ForeColor = System.Drawing.Color.Green;
                }
                lbcustgroup.Text = rs["cusgrnm"].ToString();
                lbterm.Text = rs["term"].ToString();
                sSalesmanCode = rs["salesman_cd"].ToString();
                lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
                txcustomer.CssClass = "form-control input-group-sm ro";
                txclremain.Text = bll.vLookUp("select dbo.fn_remaincl('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbcredittype.Text = rs["cuscate_cd"].ToString();
                lbcustcate.Text = rs["cuscate_cd"].ToString();
                string sCustCate = rs["cuscate_cd"].ToString();
                lbterm.Text = rs["payment_term"].ToString();
                lbpromised.Text = "0"; // bll.vLookUp("select dbo.fn_getpaymentpromised('" + hdcust.Value.ToString() + "')");
                lbmaxtrans.Text = "0"; //bll.vLookUp("select dbo.fn_getmaxtransamt('"+hdcust.Value.ToString()+"')");
                lboverdue.Text = bll.vLookUp("select dbo.fn_getoverdue('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                // lbgroupcl.Text = "0";//bll.vLookUp("select dbo.fn_getgrpclbycust('" + hdcust.Value.ToString() + "')");
                // lbgroupovd.Text = "0";// bll.vLookUp("select dbo.fn_getbalanceduegrpbycust('" + hdcust.Value.ToString() + "')");
                sPromised = bll.vLookUp("select dbo.fn_getlastpromised('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbpromised.Text = sPromised;
                // lbcusgrcd.Text = bll.sGetFieldValue("cusgrcd", rs["cusgrcd"].ToString());
                lbmaxtrans.Text = bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbbalance.Text = bll.vLookUp("select dbo.fn_getbalanceinvoice('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbcapbalance.Text = bll.vLookUp("select dbo.fn_checkcapbalance('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbsuspense.Text = bll.vLookUp("select dbo.fn_getsuspense('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbvatno.Text = (rs["tax_no"].Equals(DBNull.Value) ? "N/A" : rs["tax_no"].ToString());
                lblVatArabic.Text = (rs["vat_custarabic"].Equals(DBNull.Value) ? "N/A" : rs["vat_custarabic"].ToString());
                lblVatEnglish.Text = (rs["vat_custname"].Equals(DBNull.Value) ? "N/A" : rs["vat_custname"].ToString());

            }
            rs.Close();
            cbsalesman.SelectedValue = sSalesmanCode;
            cbsalesman_SelectedIndexChanged(sender, e);
            cbsalesman.CssClass = "form-control  ro";
            cbsalesman.Enabled = false;

            // Additonal for Special Customer 
            if (lbcustcate.Text == "SP")
            {
                cbbin.SelectedValue = "NES";
                cbbin.CssClass = "form-control  ro";
                cbbin.Enabled = false;
                showProforma.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Search Item or UOM not yet selected!','Search Item & Select UOM','warning');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no price setup or no setup UOM conversion!','Contact to wazaran admin','warning');", true);
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
            //string sPrice = bll.vLookUp("select unitprice from tcustomertype_price where item_cd='" + hditem.Value.ToString() + "' and cust_typ='" + sCustType + "'");
            lbprice.Text = dPrice.ToString();
            //string sStock = bll.vLookUp("select stock_display from tmst_stock where item_cd='" + hditem.Value.ToString() + "'  and bin_cd='" + cbbin.SelectedValue.ToString() + "' and (whs_cd in (select qry_data from tmap_query where qry_cd='whs_branch') and bin_cd in (select qry_data from tmap_query where qry_cd='bin_branch'))");
            //string sStock = bll.vLookUp("select dbo.fn_checkcurrentstock('"+hditem.Value.ToString()+"','"+cbwhs.SelectedValue.ToString()+"','"+cbbin.SelectedValue.ToString()+"','stockdisplay')");
            //if (sStock == null || sStock == "") { sStock = "0"; }

            //string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
            //string sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' union all select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + hditem.Value.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "')a");
            //if (sStocktwrk == "") { sStocktwrk = "0"; }
            //lbstock.Text = ((Convert.ToDouble(sStock) * Convert.ToDouble(sQTY)) - Convert.ToDouble(sStocktwrk)).ToString(); 
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sStock = "0";
            //if (chprevstk.Checked == true)
            //{
            //    sStock = bll.vLookUp("select dbo.[sfnGetStock]('" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate + "')");//by yanto 25-6-2016 
            //}
            //else
            //{
            sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
                                                                                                                                                                                                                                                                                                            //}

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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
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
        cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_item where prod_cd in (select prod_cd from tmst_itemCustomer_Block  where otlcd ='" + hdcust_otlcd.Value.ToString() + "' and  end_dt > '" + date + "'  ) and item_cd = '" + hditem.Value.ToString() + "'  and item_sta_id = 'A'"));
        if (cnt == 1 && cbuom.SelectedValue.ToString() == "PCS")
        {
            lblItemBlock.Text = "Yes";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Item can not sales for this customer in pcs, Contact Administrator','warning');", true);
            return;
        }
        else { lblItemBlock.Text = "No"; }

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
        if (dresult < 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There no stock available','Please do stock in or make shipment zero from other TO !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            return;
        }
        if (!double.TryParse(txstockcust.Text, out dresult))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Please fill customer stock !','warning');", true);
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

        double dMaxTrans = 0; //PEN IT FOR PAYMENT PROMISED
        if (!double.TryParse(lbmaxtrans.Text, out dMaxTrans))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Max transaction available','Can not detected','warning');", true);
            return;
        }

        int nQtyOrder = Convert.ToInt16(bll.sGetControlParameter("maxqtyorderto"));
        if (grd.Rows.Count > nQtyOrder)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The items per order','is 13','warning');", true);
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

        // Modified 27-11-2019 : Overdue Customer can't make transaction (all type: cash/credit)
        //if (!chcash.Checked)
        //{
            if (bll.vLookUp("select dbo.fn_checkcustblocked('" + hdcust.Value.ToString() + "','S','"+ Request.Cookies["sp"].Value.ToString() + "')") == "ok")
            {

                double dRowTrans = Convert.ToDouble(txshipmen.Text) * Convert.ToDouble(lbprice.Text);
                double dOverDue = Convert.ToDouble(lboverdue.Text);
                if (grd.Rows.Count > 0)
                {
                    Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
                    dRowTrans += Convert.ToDouble(lbtotsubtotal.Text);
                }
                if (dRowTrans > dMaxTrans) // OPEN IT FOR PAYMENT PROMISED
                {
                    if (dOverDue != 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Not Allow to Continue the Process! Please Check Customer Due Invoice or Remain Customer Credit Limit!',' Max amt trans:" + lbmaxtrans.Text + "','warning');", true);
                        return;
                    }                    
                }

                if (grd.Rows.Count > 0)
                {
                    Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
                    double dPriceCurrent = Convert.ToDouble(lbtotsubtotal.Text);
                    double dPriceCurrentTrx = Convert.ToDouble(lbprice.Text) * Convert.ToDouble(txshipmen.Text);
                    double dPriceMaximumu = Convert.ToDouble(bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"));
                    if (((dPriceCurrent + dPriceCurrentTrx) > dPriceMaximumu) && (bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") != "CASH"))
                    {
                        if (bll.vLookUp("select distinct 1 from tcustomer_blocked where cust_cd='" + hdcust.Value.ToString() + "' and convert(date,end_dt)>=(select dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')) and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") != "1")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not add item again !','Max avl amt transaction is exceeded','warning');", true);
                            return;
                        }
                    }
                }
                else
                {
                    double dPriceCurrent = 0;
                    double dPriceMaximumu = Convert.ToDouble(bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"));
                    double dPriceCurrentTrx = Convert.ToDouble(lbprice.Text) * Convert.ToDouble(txshipmen.Text);
                    if (((dPriceCurrent + dPriceCurrentTrx) > dPriceMaximumu) && (bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") != "CASH"))
                    {
                        if (bll.vLookUp("select distinct 1 from tcustomer_blocked where cust_cd='" + hdcust.Value.ToString() + "' and convert(date,end_dt)>=(select dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')) and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") != "1")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not add item again !','Max avl amt transaction is exceeded','warning');", true);
                            return;
                        }
                    }
                }
            }
            else
            {
                //double dPriceCurrent = 0;
                //double dPriceMaximumu = Convert.ToDouble(bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "')"));
                //double dPriceCurrentTrx = Convert.ToDouble(lbprice.Text) * Convert.ToDouble(txshipmen.Text);
                //if (((dPriceCurrent + dPriceCurrentTrx) > dPriceMaximumu) && (bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'") != "CASH"))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not add item again !','Max avl amt transaction is exceeded','warning');", true);
                //    return;
                //}
            }

        //}
        // Modified 27-11-2019 : Overdue Customer can't make transaction (all type: cash/credit)

        List<cArrayList> arr = new List<cArrayList>();
        
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
            bll.vBatchTakeOrderDiscount(arr);
            string sDiscount = "Discount Applied : \n\r";
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetWrkSalesOrderDiscount(arr, ref rs);
            while (rs.Read())
            {
                sDiscount += rs["disc_cd"].ToString() + ", free qty : " + rs["free_qty"].ToString() + ", free cash : " + rs["free_cash"].ToString() + "\n\r";
            }
            rs.Close();
            grd.ToolTip = sDiscount;
            lbprice.Text = "";
            txitemsearch.Text = "";
            txstockcust.Text = "";
            txshipmen.Text = "";
            txqty.Text = "";
            lbstock.Text = "";
            lblItemBlock.Text = "";
            cbuom.SelectedValue = "";
            btdisc.CssClass = "btn btn-primary";
            string sTotVat = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            if (grd.Rows.Count > 0)
            {
                Label lbtotvat = (Label)grd.FooterRow.FindControl("lbtotvat");
                lbtotvat.Text = sTotVat;
            }    
            string sVAT = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            lbvat.Text = sVAT;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "ItemClear();", true);
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("newto", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','New Take Order','warning');", true);
            return;
        }
        // hdto.Value = "";
        Response.Redirect("fm_takeorderentry2.aspx");
    }
    protected void grddisc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grddisc_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void btfree_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            //string sChek = bll.vLookUp("select dbo.fn_checkstocktoavl('" + hdto.Value.ToString() + "')");
            //if (@sChek != "ok")
            //{
            //    Response.Redirect("fm_loadingconfirm.aspx?ids=" + hdto.Value.ToString());

            //    return;
            //}
            List<cArrayList> arr = new List<cArrayList>();

            if (bll.nCheckAccess("loadto", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Print Loading Take Order !!','warning');", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sales order has not yet selected','Please select SO will be delivered !','warning');", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sales order has not yet selected','Please select SO will be delivered !','warning');", true);
                return;
            }

            string sLoading = bll.vLookUp("select dbo.fn_checkstockloading('" + txorderno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sLoading != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are no stock enough for Item Ordered','" + sLoading + "','warning');", true);
                return;
            }
            // Modify IA : 19-JUL-2016
            //-------------------------------------------------------------------------------------
            //double dStockAVL = 0;
            //foreach (GridViewRow row in grd.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        Label lbitemcode = (Label)row.FindControl("lbitemcode");
            //        //dStockAVL = Convert.ToDouble( bll.vLookUp("select isnull(dbo.sfnGetStock('"+lbitemcode.Text+"','"+cbbin.SelectedValue.ToString()+"','"+cbwhs.SelectedValue.ToString()+"','0',dbo.fn_getsystemdate()),0)"));
            //        arr.Clear();
            //        arr.Add(new cArrayList("@so_cd", txorderno.Text));
            //        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            //        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            //        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
            //        bll.vUpdateTSalesorderdtlbystockavl(arr);
            //    }


            //}
            //-------------------------------------------------------------------------------------
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divmsg');", true);
            string sSystemDate = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtSystemDate = DateTime.ParseExact(sSystemDate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime cdtdelivery = DateTime.ParseExact(dtdelivery.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (cdtdelivery != dtSystemDate)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Delivery order date should be same with system date','Please check delivery date !!','warning');", true);
                return;
            }
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", txorderno.Text));
            arr.Add(new cArrayList("@so_sta_id", "L"));
            arr.Add(new cArrayList("@ref_no", txmanualno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateMstSalesOrder(arr);
            arr.Clear();
            arr.Add(new cArrayList("@do_no", hddo.Value.ToString()));
            arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
            // arr.Add(new cArrayList("@vehicle_cd", cbvehicle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@dosales_sta_id", "L"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@ref_no", txmanualno.Text));
            bll.vUpdateMstDoSales(arr);
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='so_sta_id' and fld_valu='L'");
            //btsave.Visible = false;
            btsave.CssClass = "divhid";
            //btprint.Visible = false;
            //btprintinvoice.Visible = true;
            btprintinvoice.CssClass = "btn btn-default ";
            btprint.CssClass = "divhid";
            btprintfreeinv.CssClass = "divhid";
            txmanualno.CssClass = "form-control  ro";
            txmanualno.Enabled = false;
            //txmanualno.CssClass = "makeitreadonly";
            //txmanualinv.CssClass = "makeitreadwrite";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=so&so=" + txorderno.Text + "','fm_report2.aspx?src=so1&so=" + txorderno.Text + "');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void PrintInvoice()
    {
        try
        {
        
        if (bll.nCheckAccess("invto", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Print invoice Take Order !!','warning');", true);
            return;
        }
        if (txmanualinv.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual No','Please fill manual invoice !!','warning');", true);
            return;
        }

        string sManualNo = bll.vLookUp("select dbo.fn_checkmanualno('invoice','" + txmanualinv.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')"); //bll.vLookUp("select dbo.fn_getmanualno('" + txmanualinv.Text + "','" + txorderno.Text + "')");
        if (sManualNo != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This manual no. " + txmanualinv.Text + "  already used','Please use another !!','warning');", true);
            return;
        }

        string sLoading = bll.vLookUp("select dbo.fn_checkstockloading('" + txorderno.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
        if (sLoading != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are no stock enough for Item Ordered','" + sLoading + "','warning');", true);
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
            bll.vInsertsalesorderfreebyitem(arr);
            string Sord = bll.vLookUp("select dbo.fn_orderproblem('" + txinvoiceno.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (Sord == "ok")
              {
                  arr.Clear();
                  arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                  arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
                  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                  bll.vInsertRptDosalesInvoice(arr);
                  arr.Clear();
                  arr.Add(new cArrayList("@so_cd", txorderno.Text));
                  arr.Add(new cArrayList("@so_sta_id", "C"));
                  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                  bll.vUpdateMstSalesOrder(arr);
                  arr.Clear();
                  arr.Add(new cArrayList("@so_cd", txorderno.Text));
                  arr.Add(new cArrayList("@manual_no", txmanualinv.Text));
                  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                  bll.vUpdateDosalesInvoiceByManualNo(arr);
                  arr.Clear();
                  arr.Add(new cArrayList("@doc_no", txinvoiceno.Text));
                  arr.Add(new cArrayList("@print_cd", "TOINV")); //Canvas INvoice
                  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                  bll.vInsertPrintControl(arr);
                  arr.Clear();
                  arr.Add(new cArrayList("@so_cd", txorderno.Text));
                  arr.Add(new cArrayList("@vhc_cd", cbvehicle.SelectedValue.ToString()));
                  arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
                  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                  bll.vUpdateMstDosalesByDriver(arr);
                  //Walking on the moon : 18-Dec-2017
                  arr.Clear();
                  arr.Add(new cArrayList("@dosales_sta_id", "R"));
                  arr.Add(new cArrayList("@so_cd", txorderno.Text));
                  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                  bll.vUpdateMstDosalesByStatus(arr);
                  // End Of Walking on the moon :
                  arr.Clear();
                  string sSOCd = bll.vLookUp("select so_cd from tmst_dosales where inv_no='" + txinvoiceno.Text + "' and so_typ='to' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                  string sDiscAmt = bll.vLookUp("select sum(amt) from tsalesorder_disccash where so_cd='" + sSOCd + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                //btprintfreeinv.CssClass = "btn btn-info ";
                    btprintfreeinv.CssClass = "divhid";
                    btprintinvoice.CssClass = "divhid";
                  btprint.CssClass = "divhid";
                  btsave.CssClass = "divhid";
                  btnew.CssClass = "btn btn-success ";
                  btedit.CssClass = "divhid";
                  btcancel.CssClass = "divhid";
                  arr.Clear();
                  arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
                  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                  //bll.vBatchSendEmailProdSpv(arr);
                  // StockCard ONDELIVERY
                  // arr.Clear();
                  // arr.Add(new cArrayList("@refno", txinvoiceno.Text));
                  // arr.Add(new cArrayList("@stockcard_typ", "ONDELIVERY"));
                  // bll.vBatchStockCard(arr);
                  // int freeitem = 0;
                  // freeitem = int.Parse(bll.vLookUp("select count(*) from tdosalesinvoice_free where inv_no='"+txinvoiceno.Text+"'"));
                  // if (freeitem > 0)
                  // {
                  // arr.Clear();
                  // arr.Add(new cArrayList("@refno", txinvoiceno.Text));
                  // arr.Add(new cArrayList("@stockcard_typ", "ONDELIVERYFREE"));
                  // bll.vBatchStockCard(arr);
                  // }
                  //--------------------- 
                  ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=invto&no=" + txinvoiceno.Text + "&amt=" + sDiscAmt + "','fm_report2.aspx?src=invto1&no=" + txinvoiceno.Text + "&amt=" + sDiscAmt + "');", true);
              }
              else
              {
                  ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are Unitprice problem with this order contact wazaran programmers','Unitprice Issues','warning');", true);
              }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprintinvoice_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("invto", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Print invoice Take Order !!','warning');", true);
            return;
        }
        return;
    }
    protected void btsearchso_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "window.open('lookup_to.aspx','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetDynamicContent(string contextKey)
    {
        return ("Empret");
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
            bll.vDelWrkSalesDiscount(arr);
            bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
            btdisc_Click(sender, e);
            bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            arr.Add(new cArrayList("@qry_cd", "showbinorder"));
            //bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_getbytype", "bin_cd", "bin_nm", arr);
            cbwhs.CssClass = cd.csstextro;
            cbwhs.Enabled = false;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", cbdriver.SelectedValue.ToString()));
            string sVhc = bll.vLookUp("select vhc_cd from tmst_vehicle where emp_cd='" + cbdriver.SelectedValue.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (sVhc == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Driver has not vehicle setup !','Please setup first in vehicle driver master ','warning');", true);
                return;
            }
            else
            {
                bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ");
                cbvehicle.SelectedValue = sVhc;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sSOStaID = bll.vLookUp("select so_sta_id from tmst_salesorder where so_cd='" + hdto.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            string checkPayment = bll.vLookUp("select count(*) from tpayment_dtl where inv_no =( select inv_no from tmst_dosales where so_cd='" + hdto.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "') and payment_no not in (select payment_no  from tmst_payment where payment_sta_id='L' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "') and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            if (Convert.ToDecimal(checkPayment) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This take order (" + hdto.Value.ToString() + ") have payment, can't canceled !','Can't canceled " + hdto.Value.ToString() + " ','warning');", true);
                return;
            }

            else if (sSOStaID == "L")
            {

                List<string> lapproval = bll.lGetApproval("cancelto", 1);
                //Random rnd = new Random();
                //int token = rnd.Next(1000, 9999);
                //string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") + token.ToString();
                ////string sSMSMsg = "#Cancel TO:" + hdto.Value.ToString() + ", need approval,  pls reply Y/N" + stoken.ToString();
                //string sSMSMsg = "This is only information to you.Please login to Wazaran Approval Systems to make approval/ rejection. Please contact BSPV Or Faruk for approval/ rejection";
                ////    cd.vSendSms(sSMSMsg, lapproval[0]);
                //arr.Add(new cArrayList("@doc_typ", "cancelto"));
                //arr.Add(new cArrayList("@token", stoken.ToString()));
                //arr.Add(new cArrayList("@doc_no", hdto.Value.ToString()));
                //arr.Add(new cArrayList("@to", lapproval[0]));
                //arr.Add(new cArrayList("@msg", sSMSMsg));
                //bll.vInsertSmsOutbox(arr);
                //bll.vInsertSMSSent(arr);
                arr.Clear();
                //string sBody = "Dear Branch SPV, /n/r" + hdto.Value.ToString() + " need cancelled, please answer from you mobile phone";
                //string sBody = "Dear Branch SPV, /n/r" + hdto.Value.ToString() + " need cancelled, This is only information to you.Please login to <a title = 'Wazaran Approval Systems' href = 'http://172.16.1.26:8089/' target = '_blank' rel = 'noopener'> Wazaran Approval Systems</a> to make approval/ rejection.</span></p> . Please contact BSPV for approval/ rejection";
                //sBody += bll.vLookUp("select dbo.fn_getsalesorderdtl('" + hdto.Value.ToString() + "')");
                //bll.vSendMail(lapproval[1], "Cancel " + hdto.Value.ToString() + " request", sBody);
                //arr.Clear();
                //arr.Add(new cArrayList("@trxcd", "cancelto"));
                //arr.Add(new cArrayList("@token", stoken.ToString()));
                //arr.Add(new cArrayList("@doc_no", hdto.Value.ToString()));
                //bll.vInsertEmailSent(arr);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Cancel TO " + hdto.Value.ToString() + " on Pending','Please wait until approved','success');", true);
                btprint.CssClass = "divhid";
                btprintfreeinv.CssClass = "divhid";
                btprintinvoice.CssClass = "divhid";
                btedit.CssClass = "divhid";
                btcancel.CssClass = "divhid";

                //return;
            }
            //  List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@so_sta_id", "E"));
            arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateMstSalesOrderByStatus(arr);
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
            arr.Add(new cArrayList("@cancel_sta_id", "W"));
            arr.Add(new cArrayList("@cancel_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@cancelby", Request.Cookies["usr_id"].Value));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertSalesOrderCancel(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "if", "sweetAlert('Cancel TO Succedded,Please wait until approved','TO No. " + txorderno.Text + "','success');", true);
            btsave.CssClass = "divhid";
            btedit.CssClass = "divhid";
            btprint.CssClass = "divhid";
            btprintinvoice.CssClass = "divhid";
            btprintfreeinv.CssClass = "divhid";
            btnew.CssClass = "btn btn-primary ";
            btcancel.CssClass = "divhid";
            lbstatus.Text = "Cancelled";
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btcheck_Click(object sender, EventArgs e)
    {
        //System.Data.SqlClient.SqlDataReader rs = null;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "if", "sweetAlert('Discount Info','Discount : RD0001 | Qty :  4.0 | Free : 9.0','info');", true);
        return;
    }
    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {
        txitemsearch.Text = "";
        lbprice.Text = "";
        txstockcust.Text = "";
        txqty.Text = "";
        lbstock.Text = "";
        txshipmen.Text = "";
        BindEmployeConditional();

    }
    protected void cbsourceorder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbsourceorder.SelectedValue.ToString() == "TAB")
        {
            DisableControl();
            bttabsearch.Attributes.Add("class", "btn btn-primary");
            bttabsearchPnl.Visible = true;
            txtabnoPnl.CssClass = "input-group";
            btsearchso.Attributes.Add("class", "divhid");
            btsearchsoPnl.Visible = false;
            txordernoPnl.CssClass = "";
            cbwhs.CssClass = "form-control  ro";
            cbwhs.Enabled = false;
            cbsourseinfo.CssClass = "form-control  ro";
            cbsourseinfo.Enabled = false;
            cbdriver.CssClass = cd.csstextro;
            dtdelivery.CssClass = "form-control  ro";
            //  cbvatmethod.CssClass = "form-control ro";
        }
        else
        {
            vNew();
            bttabsearch.Attributes.Add("class", "divhid");
            bttabsearchPnl.Visible = false;
            txtabnoPnl.CssClass = "";
            btsearchso.Attributes.Add("class", "btn btn-primary");
            btsearchsoPnl.Visible = true;
            txordernoPnl.CssClass = "input-group";
            cbdriver.CssClass = cd.csstext;
            dtdelivery.CssClass = "form-control";
            //  cbvatmethod.CssClass = "form-control";
        }
        hdto.Value = "";
        txorderno.Text = "";
        txorderno.CssClass = "form-control input-group-sm ro";
        txorderno.Text = "NEW";
        cbwhs.CssClass = "form-control ";
        cbwhs.Enabled = true;
        CleanWrk();
        cbsourseinfo.CssClass = "form-control ";
        cbsourseinfo.Enabled = true;
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
    try{
        SqlDataReader rs = null;
        string sStaID = string.Empty; string sRefNo = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vDelWrkSalesOrderDtl(arr);
        bll.vDelSalesOrderFreeItem(arr);
        bll.vDelWrkDiscountCash(arr);
        bll.vDelWrkSalesDiscount(arr);
        bll.vDelTwrkEditFreeCash(arr);
        bll.vDeleteWrkSalesOrderDtlTax(arr);
        arr.Clear();
        arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGetMstSalesOrder(arr, ref rs);
        while (rs.Read())
        {
            txtabno.Text = rs["tabno"].ToString();
            hdcust.Value = rs["cust_cd"].ToString();
            txcustomer.Text = rs["cust_cd"].ToString() + " : " + rs["cust_nm"].ToString();
            txcustomer.CssClass = "form-control input-group-sm ro";
            lbstatus.Text = rs["so_sta_nm"].ToString();
            txmanualno.Text = rs["manual_no"].ToString();
            txmanualno.CssClass = "form-control  ro";
            txmanualno.Enabled = false;
            sRefNo = rs["ref_no"].ToString();
            btsearch_Click(sender, e);
            sStaID = rs["so_sta_id"].ToString();
            hdcust.Value = rs["cust_cd"].ToString();
            string sInvNo = bll.vLookUp("select inv_no from tmst_dosales where so_cd='" + hdto.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            txmanualinv.Text = bll.vLookUp("select manual_no from tdosales_invoice where inv_no='" + sInvNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            txmaninvfreeno.Text = bll.vLookUp("select distinct manual_no from tdosalesinvoice_free where inv_no='" + sInvNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            cbbin.SelectedValue = rs["bin_cd"].ToString();
            cbbin.CssClass = "form-control  ro";
            cbbin.Enabled = false;
            dtorder.Text = Convert.ToDateTime(rs["so_dt"]).ToString("d/M/yyyy");
            string sdtdelivery = bll.vLookUp("select isnull((select loading_dt from tsalesorder_info where so_cd='" + hdto.Value.ToString() + "'),[dbo].[fn_getsystemdate]('"+ Request.Cookies["sp"].Value.ToString() + "'))");

            //dtdelivery.Text = DateTime.ParseExact(sdtdelivery, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture).ToString();
            DateTime oDate = DateTime.Parse(sdtdelivery);
            dtdelivery.Text = oDate.Day + "/" + oDate.Month + "/" + oDate.Year;
            //dtdelivery.Text = Convert.ToDateTime(sdtdelivery).ToString("d/M/yyyy");
            dtdelivery.CssClass = "form-control  ro";
            if (rs["rdonoff"].ToString() == "ON")
            { chdisc.Checked = true; }
            else { chdisc.Checked = false; }
            chdisc_CheckedChanged(sender, e);
            cbsourceorder.SelectedValue = rs["so_source"].ToString();
            cbsourceorder.CssClass = "form-control  ro";
            cbsourceorder.Enabled = false;
            cbwhs.CssClass = "form-control  ro";
            cbwhs.Enabled = false;
            lbvat.Text = bll.vLookUp("select sum(vat) from tdosalesinvoice_dtl where inv_no='" + sInvNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                if (rs["pocust_no"].ToString() != "")
                {
                    txpocust.Text = rs["pocust_no"].ToString();
                    dtcustpo.Text = Convert.ToDateTime(rs["pocust_dt"]).ToString("d/M/yyyy");
                }            
            }
        rs.Close();
        txinvoiceno.Text = bll.vLookUp("select inv_no from tmst_dosales where so_cd='" + hdto.Value.ToString() + "' and salespointcd = '"+ Request.Cookies["sp"].Value.ToString() + "'");
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vInsertWrkSalesOrderDtlFromSODtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        grd.CssClass = "table table-striped mygrid"; //grd.Enabled = false;
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
        Label lbtotvat = (Label)grd.FooterRow.FindControl("lbtotvat");
        lbtotvat.Text = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
        //--------------------------------------Binding Discount
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        // bll.vBatchTakeOrderDiscount(arr);
        //------------- 14 Feb , Add Discount Edit enhancement feature -------------------
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
        // bll.vBatchEditDiscount(arr);
        //--------------------------------------------------------------------------------
        // Enhanced : 19-May-2016 by IA
        // Function : To direct get binding discount to wrk from core
        arr.Clear();
        arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        // bll.vBatchCore2WrkDiscount(arr);
        //--- Modify IA : 23-JUL-16
        bll.vInsertWrkSalesDiscountFromCore(arr);
        //-------------------------------------------------------------------------------
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
        grddisc.CssClass = "table table-striped mygrid";
        grdfree.CssClass = "table table-striped mygrid";
        //grddisc.Enabled = false;
        //grdfree.Enabled = false;
        if (grddisc.Rows.Count > 0) // Remark By IA : 19 May 2016
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@so_cd", hdto.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //   bll.vInsertWrkSalesOrderFreeItemFromCore(arr);
            //grdfree.Visible = true;
            grdfree.CssClass = "table table-striped mygrid";
            //grdfree.Enabled = false;
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);
        }
        //------------------------------------------------------
        
        if (sStaID == "N")
        {
            //btprint.Visible = true;
            btprintinvoice.CssClass = "divhid";
            btprint.CssClass = "btn btn-info";
            btprintinvoice.CssClass = "divhid";
            btprintfreeinv.CssClass = "divhid";
            btsave.CssClass = "divhid";
            btcancel.CssClass = "btn btn-default ";
            btedit.CssClass = "btn btn-warning ";
            grd.CssClass = "table table-striped mygrid";
            //------------------------------- Start Fill discount calc
            //arr.Clear();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            // bll.vBatchTakeOrderDiscount(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
            // -------------------------------------------------------
            btdisc.CssClass = "divhid";
            // grdfree.Visible = false;
            grdfree.CssClass = "divhid";
            txmanualno.CssClass = "form-control ";
            txmanualno.Enabled = true;
        }
        else if (sStaID == "L")
        {
            cbdriver.CssClass = "form-control ";
            cbdriver.Enabled = true;
            bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ");
            arr.Clear();
            BindEmployeConditional();
            cbvehicle.CssClass = "form-control ";
            cbvehicle.Enabled = true;
            string sDriver = bll.vLookUp("select driver_cd from tmst_dosales where driver_cd='" + hdto.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            btdisc.CssClass = "divhid";
            txmanualno.Text = sRefNo;
            txmanualno.CssClass = "form-control  ro";
            txmanualno.Enabled = false;
            arr.Clear();
            btprintinvoice.CssClass = "btn btn-default ";
            btprint.CssClass = "btn btn-default ";
            txmanualinv.Enabled = false;
            txmanualinv.CssClass = "form-control  ro";
            txmaninvfreeno.Enabled = false;
            txmaninvfreeno.CssClass = "form-control  ro";
            btsave.CssClass = "divhid";
            btcancel.CssClass = "btn btn-default ";
            btedit.CssClass = "btn btn-warning ";
            grd.CssClass = "table table-striped mygrid";
            grdfree.CssClass = "divhid";
            btprint.CssClass = "divhid";
            btprintinvoice.CssClass = "btn btn-default ";
            btprintfreeinv.CssClass = "divhid";
            //btedit.Style.Add("display", "none");

        }
        else if (sStaID == "C") //Completed
        {
            arr.Clear();
            bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ");
            arr.Clear();
            BindEmployeConditional();
            //arr.Add(new cArrayList("@qry_cd", "driver"));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            cbvehicle.CssClass = "form-control  ro";
            cbvehicle.Enabled = false;
            cbdriver_SelectedIndexChanged(sender, e);
            //btnew.Visible = true;
            btnew.CssClass = "btn btn-primary ";
            //btprice.Visible = false;
            //  btprintfreeinv.CssClass = "btn btn-default ";
            btsave.CssClass = "divhid";
            btprint.CssClass = "divhid";
            btadd.Visible = false;
            btcancel.CssClass = "divhid";
            btdisc.CssClass = "divhid";
            btedit.CssClass = "divhid";
                btprintfreeinv.CssClass = "divhid";
                //btprintfreeinv.CssClass = "btn btn-default ";
            btprintinvoice.CssClass = "divhid";
            grd.Columns[11].Visible = false;
            btedit.CssClass = "divhid";
            grd.CssClass = "table table-striped mygrid";
            btdisc.CssClass = "divhid";
            //grdfree.Visible = true;
            grdfree.CssClass = "table table-striped mygrid";
            txmaninvfreeno.CssClass = "form-control  ro";
            txmaninvfreeno.Enabled = false;
        }
        else if (sStaID == "E")
        {
            //btprint.Visible = false;
            btprint.CssClass = "divhid";
            //btprintfreeinv.Visible = false;
            btprintfreeinv.CssClass = "divhid";
            //btsave.Visible = false;
            btsave.CssClass = "divhid";
            //btnew.Visible = true;
            //btprintinvoice.Visible = false;
            btprintinvoice.CssClass = "divhid";
            //btdisc.Visible = false;
            btdisc.CssClass = "divhid";
            btcancel.CssClass = "divhid";
            grd.CssClass = "table table-striped mygrid";
            btdisc.CssClass = "divhid";
            btedit.Style.Add("display", "none");
        }
        // 14-Feb-16, IA, Get Free Item for this discount ------------------------------------------------------
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);
        grdfree.CssClass = "table table-striped mygrid";
        //---------------------------------------------------------------------------------
        string sAppSta = bll.vLookUp("select app_sta_id from tsalesorder_info where so_cd='" + hdto.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() +"'");
        if (sAppSta == "W")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('SO No." + hdto.Value.ToString() + " is waiting for approval Branch SPV','You can not proceed this TO','warning');", true);
            //btprint.Visible = false;
            btprint.CssClass = "divhid";
            //btprintfreeinv.Visible = false;
            btprintfreeinv.CssClass = "divhid";
            //btprintinvoice.Visible = false;
            btprintinvoice.CssClass = "divhid";
            btdisc.CssClass = "divhid";
        }
        DateTime dtOrd = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string day = Convert.ToString(dtOrd.Day);
        string mon = Convert.ToString(dtOrd.Month);
        string yrs = Convert.ToString(dtOrd.Year);
        string date = yrs + "-" + mon + "-" + day;
        txitemsearch_AutoCompleteExtender.ContextKey = date;
        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void PrintFreeInvoice()
    {
        try
        {
            string sCheck = bll.vLookUp("select dbo.fn_checkexistfreeitem('" + txinvoiceno.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (sCheck != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no free item in this order!','" + hdto.Value.ToString() + "','warning');", true);
                return;
            }
            if (txmaninvfreeno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual Invoice Free can not empty','Invoice No." + txinvoiceno.Text + "','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
            arr.Add(new cArrayList("@manual_no", txmaninvfreeno.Text));
            bll.vUpdateDoSalesInvoiceFree(arr);
            txmaninvfreeno.CssClass = "form-control ";
            txmaninvfreeno.Enabled = true;
            btprintfreeinv.CssClass = "divhid";
            btprint.CssClass = "divhid";
            btprintinvoice.CssClass = "divid";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport1('fm_report2.aspx?src=invf&noi=" + txinvoiceno.Text + "','fm_report2.aspx?src=invf2&noi=" + txinvoiceno.Text + "');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt16(bll.vLookUp("select dbo.fn_checkaccess('freeto','" + Request.Cookies["usr_id"].Value.ToString() + "')")) == 0)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "sweetAlert('You have no access to print free inv','Please request to Admin','warning');", true);
            return;
        }
        return;
        //if (txmaninvfreeno.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "sweetAlert('Manual Invoice Free can not empty','Invoice No." + txinvoiceno.Text + "','warning');", true);
        //    return;
        //}
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
        //arr.Add(new cArrayList("@manual_no", txmaninvfreeno.Text));
        //bll.vUpdateDoSalesInvoiceFree(arr);
        //txmaninvfreeno.CssClass = "makeitreadonly";
        ////txmaninvfreeno.Enabled = false;
        //btprintfreeinv.CssClass = "divhid";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport1('fm_report2.aspx?src=invf&noi=" + txinvoiceno.Text + "','fm_report2.aspx?src=invf2&noi=" + txinvoiceno.Text + "');", true);
    }
    protected void grddisc_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField hdmec = (HiddenField)grddisc.Rows[e.NewSelectedIndex].FindControl("hdmec");
        if (hdmec.Value.ToString() != "FG")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free item is not available for percentage and cash','Only Free Good Mechanism','warning');", true);
            return;
        }

    }
    protected void txitemsearch_TextChanged(object sender, EventArgs e)
    {
        txstockcust.Text = "0";
        lbprice.Text = "0";
        lbstock.Text = "0";
        btCustPO_ServerClick(sender, e);
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
                if (hdto.Value.ToString() != "")
                {
                    lbdisc.Text = bll.vLookUp("select dbo.fn_getdisccasitem('" + hdto.Value.ToString() + "','" + lbitemcode.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')").ToString();
                }
                else
                {
                    lbdisc.Text = bll.vLookUp("select disc_cash from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and item_cd='" + lbitemcode.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
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
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void rdonoff_SelectedIndexChanged(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vDelCanvasOrderFreeItem(arr);
        //// bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);
        //if (rdonoff.SelectedValue.ToString() == "ON")
        //{
        //  //  btdisc.Visible = true;
        //    btdisc.CssClass = "btn btn-primary";
        //   // grddisc.Visible = true;
        //    grddisc.CssClass = "ro";
        //   // grdfree.Visible = true;
        //    grdfree.CssClass = "ro";
        //    btsave.CssClass = "divhid";
        //}
        //else
        //{
        //    //btdisc.Visible = false;
        //    //grddisc.Visible = false;
        //    //grdfree.Visible = false;
        //    //btsave.CssClass = "button2 save";
        //    btdisc.CssClass = "divhid";
        //    grddisc.CssClass = "divhid";
        //    grdfree.CssClass = "divhid";
        //    btsave.CssClass = "btn btn-info";
        //}
    }
    protected void cbsourseinfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (cbsourseinfo.SelectedValue.ToString() == "TAB")
        //{ DisableControl(); }
        //else { vNew(); }
        //hdto.Value = ""; txorderno.Text = "";
        //CleanWrk();
        if (cbsourseinfo.SelectedValue.ToString() == "MERCHANDIZER")
        {
            lbsourseinfo.Visible = true;
            cbmerchendizer.Visible = true;
            cbmerchendizerPnl.Visible = true;
        }
        else
        {
            lbsourseinfo.Visible = false;
            cbmerchendizer.Visible = false;
            cbmerchendizerPnl.Visible = false;
        }
    }
    protected void bttabsearch_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lo", "popupwindow('lookuptab_to.aspx');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('fm_tabsalesorder.aspx');", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = e.NewEditIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
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
            txstockcust.Text = lbstockcust.Text;
            txstockcust.CssClass = "form-control input-sm ";
            txstockcust.Enabled = true;
            txitemsearch.CssClass = "form-control input-sm";
            txitemsearch.Enabled = true;
            txqty.Text = lbqtyorder.Text;
            txqty.CssClass = cd.csstext;
            DateTime ddate = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            double dstock = 0;
            if (Convert.ToDouble(lbstockamt.Text) < 0) { lbstockamt.Text = "0"; }
            dstock = Convert.ToDouble(lbstockamt.Text) + Convert.ToDouble(bll.vLookUp("select dbo.[sfnGetStockBooking]('"+ Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')"));//by yanto 25-6-2016 
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
            btnew.CssClass = "btn btn-primary ";
            //btsave.Visible = false;
            btsave.CssClass = "divhid";
            //btprint.Visible = false;
            btprint.CssClass = "divhid";
            //btprintfreeinv.Visible = false;
            btprintfreeinv.CssClass = "divhid";
            //btprintinvoice.Visible = false;
            btprintinvoice.CssClass = "divhid";

            //lbsubtotal.Text = lbsubtotal1.Text;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        //  vSendEmail(hdcust.Value.ToString(), sToken);
    }

    //private void vSendEmail(string sCustCode, string sToken, string sReceived)
    //{
    //    SqlDataReader rs = null;
    //    string sHttp = bll.sGetControlParameter("link_branch");
    //    List<cArrayList> arr = new List<cArrayList>();
    //    string sLastDate = string.Empty; ;
    //    string sLastAmount = string.Empty;
    //    arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
    //    bll.vGetLastTrans(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        sLastAmount = rs["trn_amt"].ToString();
    //        sLastDate = rs["trn_typ"].ToString() + "," + rs["trn_dt"].ToString();
    //    } rs.Close();
    //    string sBody = "Dear Branch Spv, \n\r Need Approval for Transaction Block " + hdcust.Value.ToString() +
    //              "<table style='width:100%'><tr style='background-color:silver'><td>Code Customer</td><td>:</td><td>" + hdcust.Value.ToString() + "</td><td>Salesman Code</td><td>:</td><td>" + cbsalesman.SelectedValue.ToString() + "</td></tr>" +
    //              "<tr><td>Customer Name</td><td>:</td><td>" + txcustomer.Text + "</td><td>Salesman Name</td><td>:</td><td>" + cbsalesman.SelectedItem.Text + "</td></tr>" +
    //              "<tr style='background-color:silver'><td>Type Customer</td><td>:</td><td>" + lbcusttype.Text + "</td><td>Customer Balance</td><td>:</td><td>" + bll.vLookUp("select dbo.fn_getinvbalance('" + hdcust.Value.ToString() + " ')") + "</td></tr>" +
    //              "<tr><td>Credit Type</td><td>:</td><td>" + lbcredittype.Text + "</td><td>New Inv Amount</td><td>:</td><td>" + bll.vLookUp("select sum(unitprice * qty) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'") + "</td></tr>" +
    //              "<tr style='background-color:silver'><td>Credit Limit</td><td>:</td><td>" + lbcredit.Text + "</td><td>Remain Limit</td><td>:</td><td>" + txclremain.Text + "</td></tr>" +
    //              "<tr><td>Last Transaction</td><td>:</td><td>" + sLastDate + "</td><td>Amount Last Trans</td><td>:</td><td>" + sLastAmount + "</td></tr></table><br>";
    //    sBody += "Please click <a href='" + sHttp + @"/landingpage.aspx?sta=A&trnname=salesorder&appcode=" + sToken + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Approve</a> for approved OR <a href='" + sHttp + @"/landingpage.aspx?sta=R&trnname=salesorder&appcode=" + sToken + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Reject</a> for rejected !";

    //    bll.vSendMail(sReceived, "Sales Order Request Need Approval", sBody);

    //}

    private void vSendEmail(string sCustCode, string sToken, string sReceived, string sCCReceived)
    {
        try
        {
            SqlDataReader rs = null;
            string sHttp = bll.sGetControlParameter("link_branch");
            List<cArrayList> arr = new List<cArrayList>();
            string sLastDate = string.Empty; ;
            string sLastAmount = string.Empty;
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            bll.vGetLastTrans(arr, ref rs);
            while (rs.Read())
            {
                sLastAmount = rs["trn_amt"].ToString();
                sLastDate = rs["trn_typ"].ToString() + "," + rs["trn_dt"].ToString();
            }
            rs.Close();
            string sBody = "Dear Branch Spv, \n\r Need Approval for Transaction Block " + hdcust.Value.ToString() +
                      "<table style='width:100%'><tr style='background-color:silver'><td>Code Customer</td><td>:</td><td>" + hdcust.Value.ToString() + "</td><td>Salesman Code</td><td>:</td><td>" + cbsalesman.SelectedValue.ToString() + "</td></tr>" +
                      "<tr><td>Customer Name</td><td>:</td><td>" + txcustomer.Text + "</td><td>Salesman Name</td><td>:</td><td>" + cbsalesman.SelectedItem.Text + "</td></tr>" +
                      "<tr style='background-color:silver'><td>Type Customer</td><td>:</td><td>" + lbcusttype.Text + "</td><td>Customer Balance</td><td>:</td><td>" + bll.vLookUp("select dbo.fn_getinvbalance('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')") + "</td></tr>" +
                      "<tr><td>Credit Type</td><td>:</td><td>" + lbcredittype.Text + "</td><td>New Inv Amount</td><td>:</td><td>" + bll.vLookUp("select sum(unitprice * qty) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'") + "</td></tr>" +
                      "<tr style='background-color:silver'><td>Credit Limit</td><td>:</td><td>" + lbcredit.Text + "</td><td>Remain Limit</td><td>:</td><td>" + txclremain.Text + "</td></tr>" +
                      "<tr><td>Last Transaction</td><td>:</td><td>" + sLastDate + "</td><td>Amount Last Trans</td><td>:</td><td>" + sLastAmount + "</td></tr></table><br>";
            //  sBody += "Please click <a href='landingpage.aspx?sta=A&trnname=salesorder&appcode=" + sToken + "'>Approve</a> for approved OR <a href='landingpage.aspx?sta=R&trnname=salesorder&appcode=" + sToken + "'>Reject</a> for rejected !";
            bll.vSendMail(sCCReceived, "cc:Sales Order Request Need Approval", sBody);
            sBody += "Please click <a href='" + sHttp + @"/landingpage.aspx?sta=A&trnname=salesorder&appcode=" + sToken + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Approve</a> for approved OR <a href='" + sHttp + @"/landingpage.aspx?sta=R&trnname=salesorder&appcode=" + sToken + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Reject</a> for rejected !";
            bll.vSendMail(sReceived, "Sales Order Request Need Approval", sBody);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }


    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("editto", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Edit Take Order','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelSalesOrderFreeItem(arr);
            bll.vDelWrkDiscountCash(arr);
            bll.vDelWrkSalesDiscount(arr);
            bll.vDelWrkSalesorderFreeItem(arr);
            bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
            bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);
            //----------------------------------------------------------------------------------
            txitemsearch.CssClass = "form-control input-sm";
            txitemsearch.Enabled = true;
            txqty.CssClass = "form-control input-sm   ro";
            txqty.Enabled = false;
            cbuom.CssClass = "form-control input-sm";
            cbuom.Enabled = true;
            txstockcust.CssClass = "form-control input-sm ";
            txstockcust.Enabled = true;
            txshipmen.CssClass = "form-control input-sm ";
            txshipmen.Enabled = true;
            grd.CssClass = "makeitreadwrite";
            btsave.CssClass = "divhid";
            btnew.CssClass = "btn btn-primary ";
            btedit.CssClass = "divhid";
            btprint.CssClass = "divhid";
            btprintinvoice.CssClass = "divhid";
            btprintfreeinv.CssClass = "divhid";
            btprint.CssClass = "divhid";
            btdisc.CssClass = "btn btn-primary";
            btcancel.CssClass = "btn btn-default ";
            dtdelivery.CssClass = "form-control";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grddisc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grddisc.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
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
        if (hdmec.Value.ToString() == "CG")
        {
            txfreeqty.CssClass = "ro form-control input-sm";
            txfreeqty.Enabled = false;
            txfreecash.CssClass = "ro form-control input-sm";
            txfreecash.Enabled = false;
        }

    }
    protected void grddisc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddisc.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);

    }
    protected void grddisc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if ((e.Row.RowType == DataControlRowType.DataRow) && ((e.Row.RowState == DataControlRowState.Edit)))
        //{
        //    TextBox txfreecash = (TextBox)e.Row.FindControl("txfreecash");
        //    TextBox txfreeqty = (TextBox)e.Row.FindControl("txfreeqty");
        //   // txfreecash.CssClass = "ro";
        //    txfreecash.Enabled = false;
        //   // txfreeqty.CssClass = "ro";
        //    txfreeqty.Enabled = true;
        //    if (Convert.ToDouble(txfreecash.Text) == 0)
        //    {
        //        //txfreecash.CssClass = "ro";
        //        //txfreeqty.CssClass = "makeitreadwrite";
        //        txfreecash.Enabled = false;
        //        txfreeqty.Enabled = true;
        //    }
        //    else { txfreeqty.CssClass = "ro"; txfreecash.CssClass = "makeitreadwrite"; }
        //}


    }
    protected void grddisc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
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

            string sMec = bll.vLookUp("select discount_mec from twrk_salesdiscount where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and disc_cd='" + lbdisccode.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'").ToString();
            double dFreeOri = Convert.ToDouble(bll.vLookUp("select free_ori from twrk_salesdiscount where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and disc_cd='" + lbdisccode.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));
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
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
            arr.Add(new cArrayList("@free_qty", txfreeqty.Text));
            arr.Add(new cArrayList("@free_cash", txfreecash.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
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
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grddisc_SelectedIndexChanging1(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbdisccode = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbdisccode");
            Label lbfreeqty = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbfreeqty");
            Label lbuomfree = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbuomfree");
            HiddenField hdmec = (HiddenField)grddisc.Rows[e.NewSelectedIndex].FindControl("hdmec");
            HiddenField hddiscuse = (HiddenField)grddisc.Rows[e.NewSelectedIndex].FindControl("hddiscuse");
            string sdiscqty = string.Empty;
            sdiscqty = bll.vLookUp("select isnull(sum(free_qty),0) from twrk_salesorderfreeitem where  disc_cd='" + lbdisccode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_lookupitem.aspx?so_cd=" + txorderno.Text.Trim() + "&dc=" + lbdisccode.Text + "&du=" + hddiscuse.Value.ToString() + "&f=" + dbalfreeqty + "&t=to&u=" + lbuomfree.Text + "&wh=" + cbwhs.SelectedValue.ToString() + "&cust=" + hdcust.Value.ToString() + "&bin=" + cbbin.SelectedValue.ToString() + "');", true); //modify daryanto 12-10-2016

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void txqty_TextChanged(object sender, EventArgs e)
    {

    }
    protected void grdfree_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (txorderno.Text != "NEW")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free item Can not delete','Free','warning');", true);
                return;
            }
            Label lblitem_cd = (Label)grdfree.Rows[e.RowIndex].FindControl("lblitem_cd");
            Label lbldisc_cd = (Label)grdfree.Rows[e.RowIndex].FindControl("lbldisc_cd");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", lblitem_cd.Text));
            arr.Add(new cArrayList("@disc_cd", lbldisc_cd.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDeletetwrk_salesorderfreeitem(arr);
            btfree_Click(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btupdman_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "op", "alert('" + hdmanualno.Value.ToString() + "');", true);
    }
    protected void btprintloading_Click(object sender, EventArgs e)
    {
        PrintLoading();
    }
    protected void btprintinvoice2_Click(object sender, EventArgs e)
    {
        PrintInvoice();
    }
    protected void btprintfreeinv2_Click(object sender, EventArgs e)
    {
        PrintFreeInvoice();
    }
    protected void chdisc_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdto.Value.ToString() != "")
            {
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Discount On Off is not impacted on TO already created','','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelCanvasOrderFreeItem(arr);
            // bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);
            if (chdisc.Checked)
            {
                //  btdisc.Visible = true;
                btdisc.CssClass = "btn btn-primary";
                // grddisc.Visible = true;
                grddisc.CssClass = "ro";
                // grdfree.Visible = true;
                grdfree.CssClass = "ro";
                btsave.CssClass = "divhid";
            }
            else
            {
                //btdisc.Visible = false;
                //grddisc.Visible = false;
                //grdfree.Visible = false;
                //btsave.CssClass = "button2 save";
                btdisc.CssClass = "divhid";
                grddisc.CssClass = "divhid";
                grdfree.CssClass = "divhid";
                btsave.CssClass = "button2 save";
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btdrivertaken_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_drivertaken.aspx?to=" + hdto.Value.ToString() + "');", true);
    }
    protected void btcopyorder_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_subtakeorder.aspx?to=" + hdto.Value.ToString());
    }
    protected void chprevstk_CheckedChanged(object sender, EventArgs e)
    {
        //DateTime ddate = DateTime.ParseExact(dtorder.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //var dateAsString = ddate.ToString("yyyy-MM-dd");
        //string swhs_cd = bll.vLookUp("select whs_cd from tstockopname_schedule where whs_cd='" + cbwhs.SelectedValue + "' and schedule_dt='" + dateAsString + "'");
        //if (swhs_cd != cbwhs.SelectedValue)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please entri schedule jaret !','Prev stk can not changed ','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "HideProgress();", true);
        //    if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
        //    return;
        //}
        //string slevel_no = bll.vLookUp("select level_no from tmst_warehouse  where whs_cd='" + cbwhs.SelectedValue + "'");
        //if (slevel_no == "1")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Prev stk Only for Sub Depo !','Prev stk can not changed ','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "HideProgress();", true);
        //    if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
        //    return;
        //}
        //if (grd.Rows.Count != 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item already inserted !','Prev stk can not changed ','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "HideProgress();", true);
        //    if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
        //    return;
        //}
    }
    protected void btprintvat_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=invoiceTaxeng&iv=" + txinvoiceno.Text + "','fm_report2.aspx?src=invoiceTaxara&iv=" + txinvoiceno.Text + "');", true);
    }
    protected void btpanda_Click(object sender, EventArgs e)
    {

    }

    protected void dtdelivery_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //DateTime dtSystemDate = DateTime.ParseExact(bll.vLookUp("select [dbo].[fn_getsystemdate]()"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sSystemDate = Request.Cookies["waz_dt"].Value.ToString();
            //string sSystemDate = bll.vLookUp("select [dbo].[fn_getsystemdate]()");
            //string sSystemDate= String.Format("{0:d/M/yyyy}", bll.vLookUp("select [dbo].[fn_getsystemdate]()"));
            //DateTime dtSystemDate = DateTime.Parse(sSystemDate);
            DateTime dtSystemDate = DateTime.ParseExact(sSystemDate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            //string sSystemDate = Request.Cookies["waz_dt"].Value.ToString();
            //DateTime dtSystemDate = DateTime.Parse(sSystemDate);
            //DateTime dtSystemDate = Convert.ToDateTime(sSystemDate);
            DateTime last_date = new DateTime(dtSystemDate.Year, dtSystemDate.Month, DateTime.DaysInMonth(dtSystemDate.Year, dtSystemDate.Month));
            //DateTime cdtdelivery = Convert.ToDateTime(dtdelivery.Text);
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
                //dtdelivery.Text = string.Format("{0:d/M/yyyy}", last_date.ToString()).Substring(0, 10);
                dtdelivery.Text = last_date.Day + "/" + last_date.Month + "/" + last_date.Year;
                return;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    protected void btCustPO_ServerClick(object sender, EventArgs e)
    {
        try
        {
            //if (lbcusttype.Text == "Key Account")
            //{
                //if (lbcustcate.Text != "SP")
                //{
                    if (txpocust.Text.TrimEnd().TrimStart() != "")
                    {
                        if (hdto.Value.ToString() == "")
                        {
                            string sPO = bll.vLookUp("select dbo.fn_checkDuplicatePOSameYear('" + txpocust.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
                            if (sPO != "ok")
                            {
                                lblPO.ForeColor = System.Drawing.Color.Red;
                                lblPO.Text = "Invalid PO Number";
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not duplicate PO number in same year!','Invalid PO Number','warning');", true);
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                                //return;
                            }
                            else
                            {
                                lblPO.ForeColor = System.Drawing.Color.Green;
                                lblPO.Text = "Valid PO Number";
                            }
                        }
                        else
                        {
                            string currPO = bll.vLookUp("select pocust_no from tsalesorder_info where so_cd='" + hdto.Value + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                            if (txpocust.Text.TrimEnd().TrimStart() != currPO)
                            {
                                string sPO = bll.vLookUp("select dbo.fn_checkDuplicatePOSameYear('" + txpocust.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
                                if (sPO != "ok")
                                {
                                    lblPO.ForeColor = System.Drawing.Color.Red;
                                    lblPO.Text = "Invalid PO Number";
                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not duplicate PO number in same year!','Invalid PO Number','warning');", true);
                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                                    //return;
                                }
                                else
                                {
                                    lblPO.ForeColor = System.Drawing.Color.Green;
                                    lblPO.Text = "Valid PO Number";
                                }
                            }
                            else
                            {
                                lblPO.ForeColor = System.Drawing.Color.Green;
                                lblPO.Text = "Valid PO Number";
                            }
                        }

                    }
                    //
                    else if (lblPO.Text == "Enter PO Number" || lblPO.Text == "Invalid PO Number")
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Enter Valid PO number,' Wrong PO " + txpocust.Text + "','warning');", true);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                        //return;

                    }
                //}
            //}

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListProForma(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lInv = new List<string>();
        string sInv = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstPreInv(arr, ref rs);
        while (rs.Read())
        {
            sInv = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["inv_no"].ToString()+" | "+rs["inv_dt"].ToString()+" | "+rs["cust_cd"].ToString()+"_"+rs["cust_nm"].ToString(), rs["inv_no"].ToString());
            lInv.Add(sInv);
        }
        rs.Close();
        return (lInv.ToArray());
    }

    protected void btsearchproinvoice_Click(object sender, EventArgs e)
    {
        try
        {
            btadd.Visible = false;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", hdproforma.Value));
            bll.vInsertPreInvOrder(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
            grd.Columns[14].Visible = false;

            //grd.ToolTip = sDiscount;
            lbprice.Text = "";
            txitemsearch.Text = "";
            txstockcust.Text = "";
            txshipmen.Text = "";
            txqty.Text = "";
            lbstock.Text = "";
            lblItemBlock.Text = "";
            cbuom.SelectedValue = "";
            btdisc.CssClass = "btn btn-primary";
            string sTotVat = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            Label lbtotvat = (Label)grd.FooterRow.FindControl("lbtotvat");
            lbtotvat.Text = sTotVat;
            string sVAT = bll.vLookUp("select sum(vat) from twrk_salesorderdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            lbvat.Text = sVAT;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    protected void grddirect_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            HiddenField hdmec = (HiddenField)grddisc.Rows[e.NewSelectedIndex].FindControl("hdmec");
            string sdiscqty = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_lookupitemcash.aspx?so_cd=" + txorderno.Text.Trim() + "&t=to&u=CTN&wh=" + cbwhs.SelectedValue.ToString() + "&cust=" + hdcust.Value.ToString() + "&bin=" + cbbin.SelectedValue.ToString() + "');", true); //modify daryanto 12-10-2016
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grddirectitem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_takeorderentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btitem_Click(object sender, EventArgs e)
    {
        try
        {
            grddirectitem.CssClass = "mygrid";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddirectitem, "sp_twrk_salesorderfreeitemcash_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_canvasorder2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btreset_Click(object sender, EventArgs e)
    {
        cbuom.SelectedValue = "CTN";
        cbuom_SelectedIndexChanged(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
}
