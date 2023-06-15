using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_tabcanvasorder : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    decimal tottaxdiscsum = 0, totamtsum = 0, counttran = 0;
    decimal tottaxdisc = 0, totamt = 0, counttransum = 0;
    decimal totqtysum = 0, totqty = 0, freeqty = 0, totfreesum = 0;

    double totalQtyCtn = 0, totalQtyPcs = 0;
    double totalFreeQtyCtn = 0, totalFreeQtyPcs = 0;
    double dSubTotal = 0;

    double totalQtyCtnTab = 0, totalQtyPcsTab = 0;
    double totalFreeQtyCtnTab = 0, totalFreeQtyPcsTab = 0;
    double dSubTotalTab = 0, dSubTotTaxDiscTab = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToComboWithChoosen(ref cbstatus, "tab_sta_id");
                List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_ttab_canvasorder_getbysalesman", "emp_cd", "emp_desc", arr);

                //cbstatus_SelectedIndexChanged(sender, e);

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //foreach (GridViewRow row in grdtab.Rows)
            //{
            //    CheckBox chk = (row.Cells[0].FindControl("chk") as CheckBox);
            //    if (chk.Checked)
            //    {
            //        Label lbso_cd = (Label)row.FindControl("lbso_cd");
            //        string sCanvasPending = bll.vLookUp("select dbo.fn_tabcanvasordercheck('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')").ToString();
            //        if (sCanvasPending != "ok")
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('SOME DATA NOT YET TRANSFER PLEASE SYNC TAB DATA OR CONTACT WAZARAN','" + lbso_cd.Text + "','warning');", true);
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            //            chk.Checked = false;
            //            return;
            //        }
            //    }
            //}

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtab_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            Label lbso_cd = (Label)grdtab.Rows[e.NewSelectedIndex].FindControl("lbso_cd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@so_cd", lbso_cd.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_canvasorder_get", arr);
            //if (grdtabdtl.Rows.Count > 0)
            //{

            //    lbtotorder.Text = bll.vLookUp("select sum(qty) from ttab_canvasorder where so_cd='" + lbso_cd.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //    lbtotsubtotal.Text = bll.vLookUp("select sum(qty * unitprice) from ttab_canvasorder where so_cd='" + lbso_cd.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //}
            // Get Discount Info
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", lbso_cd.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_ttab_canvasdiscount_get", arr);
            cd.v_disablecontrol(grdtab);
            cd.v_disablecontrol(grddisc);
            cd.v_disablecontrol(grdtabdtl);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btapply_Click(object sender, EventArgs e)
    {
        //try
        //{
        int nCount = 0;
        string _so_cd = String.Empty;
        foreach (GridViewRow row in grdtab.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                if (chk.Checked)
                {
                    Label lbso_cd = (Label)row.FindControl("lbso_cd");
                    Label lbpayment_no = (Label)row.FindControl("lbpayment_no");
                    // Check data already sync from tablet or no  ? 
                    string sMsgsyncedtablet = bll.vLookUp("select dbo.fn_checksyncedtablet('" + lbso_cd.Text + "','" + lbpayment_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sMsgsyncedtablet != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Canvas order number ','" + sMsgsyncedtablet + "', data not yet synced completely from tablet,'warning');", true);
                        return;
                    }

                    string sCanvasPending = bll.vLookUp("select dbo.fn_tabcanvasordercheck('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')").ToString();
                    if (sCanvasPending != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('SOME DATA NOT YET TRANSFER PLEASE SYNC TAB DATA OR CANCEL THIS INVOICE : ','" + lbso_cd.Text + "','warning');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                        return;
                    }
                    // --
                    string sMsgdupcanvas = bll.vLookUp("select dbo.fn_checkdupcanvastab('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sMsgdupcanvas != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Canvas order number ','" + sMsgdupcanvas + "' already transfer to backoffice,'warning');", true);
                        return;
                    }
                    string sMsg = bll.vLookUp("select dbo.fn_checkstockcanvasavailable('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sMsg != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Some items have no stock, pls check','" + sMsg + "','warning');", true);
                        return;
                    }
                    string sCheck = bll.vLookUp("select dbo.fn_checktabcanvashasdiscount('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sCheck != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('" + sCheck + "','Pls check again','warning');", true);
                        return;
                    }
                    string sInvoiceType = bll.vLookUp("select dbo.fn_checktabpaymenttype('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    string[] sPayment = sInvoiceType.Split('_');
                    if (sPayment[0] == "CASH")
                    {
                        string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabpaymentstatus('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sPaymentStatus == "cancel" || sPaymentStatus == "postpone")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Payment with this invoice Postpone / Cancel. : " + sPayment[1] + "','Pls check again','warning');", true);
                            return;
                        }
                    }
                }
            }
        }
        foreach (GridViewRow row in grdtab.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chk");
            Label lbsalespointcd = (Label)row.FindControl("lbsalespointcd");
            Label lbcust_cd = (Label)row.FindControl("lbcust_cd");
            Label lbso_dt = (Label)row.FindControl("lbso_dt");
            Label lbsalesman_cd = (Label)row.FindControl("lbsalesman_cd");
            Label lbmanual_no = (Label)row.FindControl("lbmanual_no");
            Label lbvhc_cd = (Label)row.FindControl("lbvhc_cd");
            Label lbbin_cd = (Label)row.FindControl("lbbin_cd");
            Label lbrdonoff = (Label)row.FindControl("lbrdonoff");
            Label lbtabno = (Label)row.FindControl("lbtabno");
            Label lbso_cd = (Label)row.FindControl("lbso_cd");
            Label lbpayment_no = (Label)row.FindControl("lbpayment_no");
            if (chk.Checked == true)
            {
                //try
                //{
                string sTermCd = bll.vLookUp("select payment_term from tmst_customer where cust_cd='" + lbcust_cd.Text.ToString() + "' and salespointcd='" + lbsalespointcd.Text + "'");
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                arr.Add(new cArrayList("@so_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@so_typ", "CN"));
                arr.Add(new cArrayList("@cust_cd", lbcust_cd.Text));
                arr.Add(new cArrayList("@salesman_cd", lbsalesman_cd.Text));
                arr.Add(new cArrayList("@term_cd", sTermCd));
                arr.Add(new cArrayList("@currency_cd", bll.sGetControlParameter("currency")));
                arr.Add(new cArrayList("@ref_no", lbmanual_no.Text));
                arr.Add(new cArrayList("@remark", "Device Transaction"));
                arr.Add(new cArrayList("@so_sta_id", "D"));
                arr.Add(new cArrayList("@so_source", "TAB"));
                arr.Add(new cArrayList("@vhc_cd", lbvhc_cd.Text));
                arr.Add(new cArrayList("@bin_cd", lbbin_cd.Text));
                arr.Add(new cArrayList("@rdonoff", lbrdonoff.Text));
                arr.Add(new cArrayList("@tabno", lbso_cd.Text));
                bll.vInsertMstCanvasOrder(arr, ref _so_cd);
                //Insert to Working Table
                arr.Clear();
                arr.Add(new cArrayList("@doc_no", _so_cd));
                arr.Add(new cArrayList("@print_typ", "canvas"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vInsertWrkPrintAll(arr);
                arr.Clear();
                arr.Add(new cArrayList("@salesman_cd", lbsalesman_cd.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                arr.Add(new cArrayList("@so_cd", _so_cd));
                arr.Add(new cArrayList("@tabno", lbso_cd.Text));
                bll.vInserttabCanvasOrderDtl(arr);
                while (true)
                {
                    if (Convert.ToInt32(bll.vLookUp("select count(1) from tmst_dosales where so_cd='" + _so_cd + "' and salespointcd='" + Request.Cookies["sp"].Value + "'")) > 0)
                    {
                        //bll.vInsertDOcanvasorderfreeinvoiceitem(arr);
                        break;
                    }
                }
               
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", _so_cd));
                arr.Add(new cArrayList("@tab_no", lbso_cd.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertTabCanvasDiscount(arr);
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", _so_cd));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchGivenDiscount(arr);

                // Add Discount Tax : 2021-11-16
                string discountVat = String.Empty;
                discountVat = bll.vLookUp("select isnull(disc_tax_amt,0) from ttab_sales_info where tab_no='" + lbso_cd.Text + "'");

                if (discountVat != "" || discountVat != "0")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@so_cd", _so_cd));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@discount_vat", bll.sGetControlParameter("discount_vat").ToString()));
                    //bll.vBatchGivenDiscountVatTab(arr);
                }
               
                nCount++;

                //}
                //catch (Exception ex)
                //{
                //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : btapply_Click");
                //}

                // Transfer Auto Payment 
                string stAuto = bll.sGetControlParameterSalespoint("auto_tablet", Request.Cookies["sp"].Value.ToString());
                if (stAuto == "ok")
                {
                    try
                    {
                        string sMsg = string.Empty; //string sPaymentNo = "";
                        arr.Clear();
                        arr.Add(new cArrayList("tab_no", lbpayment_no.Text));
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        string sComment = string.Empty;
                        
                        bll.vBatchTabPaymentReceipt(arr, ref sComment);
                        //Captured untuk run batch discount logistic , IAG, 10-FEB-2023
                        string _invoiceno = bll.vLookUp("select inv_no from tmst_dosales where so_cd='" + _so_cd + "'");
                        arr.Clear();
                        arr.Add(new cArrayList("@inv_no", _invoiceno));
                        bll2.vBatchDiscountLogistic(arr);
                        if (sComment != "ok")
                        {
                            if (sMsg == string.Empty)
                            { sMsg = sComment; }
                            else
                            {
                                sMsg += "," + sComment;
                            }
                        }
                        //sPaymentNo = bll.vLookUp("select payment_no from tmst_payment where tab_no='" + lbpayment_no.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

                    }
                    catch (Exception ex)
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@err_source", Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder"));
                        arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
                        bll.vInsertErrorLog(arr);
                    }
                }
            }
        }
        List<cArrayList> arr1 = new List<cArrayList>();
        arr1.Clear();
        arr1.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBatchStockConfirm(arr1);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "window.opener.senddata('" + sSoNo + "');window.close();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.senddata('" + _so_cd+"');window.close();", true);
        //}
        //catch (Exception ex)
        //{
        //    Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
        //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabcanvasorder");
        //    Response.Redirect("fm_ErrorPage.aspx");
        //}
    }

    private void bindinggrdtab()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingGridToSp(ref grdtab, "sp_ttab_mst_canvasorder_get", arr);
            bll.vBindingGridToSp(ref grdtab, "sp_ttab_canvasorder_get2", arr);
            bll.vBindingGridToSp(ref grdorder, "sp_ttab_canvasorder_order_summary", arr);
            bll.vBindingGridToSp(ref grdfree, "sp_ttab_canvasorder_free_summary", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabfix", "tabFix()", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();

            if (cbstatus.SelectedValue == "TRF")
            {
                btapply.Visible = false;
                btcancel.Visible = false;
                btpostpone.Visible = false;
                arr.Add(new cArrayList("@i", "A"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalesman, "sp_ttab_canvasorder_getbysalesman", "emp_cd", "emp_desc", arr);
            }
            else if (cbstatus.SelectedValue == "DEL")
            {
                btapply.Visible = false;
                btcancel.Visible = false;
                btpostpone.Visible = false;
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalesman, "sp_ttab_canvasorder_getbysalesman", "emp_cd", "emp_desc", arr);
            }
            else
            {
                btapply.Visible = true;
                btcancel.Visible = true;
                btpostpone.Visible = true;
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalesman, "sp_ttab_canvasorder_getbysalesman", "emp_cd", "emp_desc", arr);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabfix", "tabFix()", true);
            bindinggrdtab();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }


    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {

            if (bll.nCheckAccess("tabsalescancel", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To cancel this Canvasorder. contact Administrator !!','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbso_cd = (Label)row.FindControl("lbso_cd");
                        // Check Payment Already Transfer 
                        string sInvoiceType = bll.vLookUp("select dbo.fn_checktabpaymenttype('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        string[] sPayment = sInvoiceType.Split('_');

                        if (sPayment[0] == "CASH")
                        {
                            string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabpaymentstatus('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                            if (sPaymentStatus == "transfer")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Payment with this invoice Already Transfer. : " + sPayment[1] + "','Pls transfer this invoice.','warning');", true);
                                return;
                            }
                        }
                        arr.Clear();
                        arr.Add(new cArrayList("@so_cd", lbso_cd.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vDelTtabCanvasorder(arr);

                    }
                }
            }
            bindinggrdtab();
            grdtabdtl.DataSource = null;
            grdtabdtl.DataBind();
            grddisc.DataSource = null;
            grddisc.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Canvas Tab has been deleted','Deleted Canvas Tablet','success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabfix", "tabFix()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btpostpone_Click(object sender, EventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("tabsalespostpone", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To Postpone this Ca vas Order. contact Administrator !!','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbso_cd = (Label)row.FindControl("lbso_cd");
                        // Check Payment Already Transfer 
                        string sInvoiceType = bll.vLookUp("select dbo.fn_checktabpaymenttype('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        string[] sPayment = sInvoiceType.Split('_');
                        if (sPayment[0] == "CASH")
                        {
                            string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabpaymentstatus('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                            if (sPaymentStatus == "transfer")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Payment with this invoice Already Transfer. : " + sPayment[1] + "','Pls transfer this invoice.','warning');", true);
                                return;
                            }
                        }
                        arr.Clear();
                        arr.Add(new cArrayList("@so_cd", lbso_cd.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateTTabCanvasOrder(arr);

                    }
                }
            }
            bindinggrdtab();
            grdtabdtl.DataSource = null;
            grdtabdtl.DataBind();
            grddisc.DataSource = null;
            grddisc.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Canvas Tab has been Postpone','Change to next day','success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabfix", "tabFix()", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            bindinggrdtab();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabfix", "tabFix()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)grdtab.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in grdtab.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chk");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }

    protected void grdtab_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbtaxdisc = (Label)e.Row.FindControl("lbtaxdisc");
                Label lbtotamt = (Label)e.Row.FindControl("lbtotamt");
                Label lbtotqty = (Label)e.Row.FindControl("lbtotqty");
                Label lbfreeqty = (Label)e.Row.FindControl("lbfreeqty");
                HiddenField hdqty = (HiddenField)e.Row.FindControl("hdqty");
                HiddenField hdqty_ctn = (HiddenField)e.Row.FindControl("hdqty_ctn");
                HiddenField hdqty_pcs = (HiddenField)e.Row.FindControl("hdqty_pcs");
                HiddenField hdfreeqty = (HiddenField)e.Row.FindControl("hdfreeqty");
                HiddenField hdfreeqty_ctn = (HiddenField)e.Row.FindControl("hdfreeqty_ctn");
                HiddenField hdfreeqty_pcs = (HiddenField)e.Row.FindControl("hdfreeqty_pcs");

                totalQtyCtnTab = totalQtyCtnTab + double.Parse(hdqty_ctn.Value);
                totalQtyPcsTab = totalQtyPcsTab + double.Parse(hdqty_pcs.Value);
                totalFreeQtyCtnTab = totalFreeQtyCtnTab + double.Parse(hdfreeqty_ctn.Value);
                totalFreeQtyPcsTab = totalFreeQtyPcsTab + double.Parse(hdfreeqty_pcs.Value);

                dSubTotalTab += Convert.ToDouble(lbtotamt.Text);
                dSubTotTaxDiscTab += Convert.ToDouble(lbtaxdisc.Text);

                //if (lbtotamt != null)
                //{
                //    totamt = decimal.Parse(lbtotamt.Text);
                //    totqty = decimal.Parse(lbtotqty.Text);
                //    freeqty = decimal.Parse(lbfreeqty.Text);
                //}
                //else
                //{
                //    totamt = 0;
                //    totqty = 0;
                //    freeqty = 0;
                //}

                //totamtsum = totamtsum + totamt;
                //totqtysum = totqtysum + totqty;
                //totfreesum = totfreesum + freeqty;
                counttransum = counttransum + 1;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtaxdiscsum = (Label)e.Row.FindControl("lbtaxdiscsum");
                Label lbtotamtsum = (Label)e.Row.FindControl("lbtotamtsum");
                Label lbtotqtysum = (Label)e.Row.FindControl("lbtotqtysum");
                Label lbfreeqtysum = (Label)e.Row.FindControl("lbfreeqtysum");
                Label lbcounttran = (Label)e.Row.FindControl("lbcounttran");

                lbtotqtysum.Text = totalQtyCtnTab.ToString() + " CTN, " + totalQtyPcsTab.ToString() + " PCS";
                lbfreeqtysum.Text = totalFreeQtyCtnTab.ToString() + " CTN, " + totalFreeQtyPcsTab.ToString() + " PCS";
                lbtotamtsum.Text = dSubTotalTab.ToString("#,##0.00");
                lbcounttran.Text = counttransum.ToString("#,##0.00");
                lbtaxdiscsum.Text = dSubTotTaxDiscTab.ToString("#,##0.00");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //totalQtyCtn = 0; totalQtyPcs = 0; totalFreeQtyPcs = 0; totalFreeQtyCtn = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdqty = (HiddenField)e.Row.FindControl("hdqty");
                HiddenField hdqty_ctn = (HiddenField)e.Row.FindControl("hdqty_ctn");
                HiddenField hdqty_pcs = (HiddenField)e.Row.FindControl("hdqty_pcs");
                Label lbsubtotal = (Label)e.Row.FindControl("lbsubtotal");

                totalQtyCtn = totalQtyCtn + double.Parse(hdqty_ctn.Value);
                totalQtyPcs = totalQtyPcs + double.Parse(hdqty_pcs.Value);
                dSubTotal += Convert.ToDouble(lbsubtotal.Text);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotorder = (Label)e.Row.FindControl("lbtotorder");
                Label lbtotsubtotal = (Label)e.Row.FindControl("lbtotsubtotal");


                lbtotorder.Text = totalQtyCtn.ToString() + " CTN, " + totalQtyPcs.ToString() + " PCS";
                lbtotsubtotal.Text = dSubTotal.ToString("#,##0.00");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdfree_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdqtyfree = (HiddenField)e.Row.FindControl("hdqtyfree");
                HiddenField hdqtyfree_ctn = (HiddenField)e.Row.FindControl("hdqtyfree_ctn");
                HiddenField hdqtyfree_pcs = (HiddenField)e.Row.FindControl("hdqtyfree_pcs");

                totalFreeQtyCtn = totalFreeQtyCtn + double.Parse(hdqtyfree_ctn.Value);
                totalFreeQtyPcs = totalFreeQtyPcs + double.Parse(hdqtyfree_pcs.Value);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotfree = (Label)e.Row.FindControl("lbtotfree");

                lbtotfree.Text = totalFreeQtyCtn.ToString() + " CTN, " + totalFreeQtyPcs.ToString() + " PCS";
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdtabdtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //totalQtyCtn = 0; totalQtyPcs = 0; totalFreeQtyPcs = 0; totalFreeQtyCtn = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hdqty = (HiddenField)e.Row.FindControl("hdqty");
                HiddenField hdqty_ctn = (HiddenField)e.Row.FindControl("hdqty_ctn");
                HiddenField hdqty_pcs = (HiddenField)e.Row.FindControl("hdqty_pcs");
                Label lbsubtotal = (Label)e.Row.FindControl("lbsubtotal");

                totalQtyCtn = totalQtyCtn + double.Parse(hdqty_ctn.Value);
                totalQtyPcs = totalQtyPcs + double.Parse(hdqty_pcs.Value);
                dSubTotal += Convert.ToDouble(lbsubtotal.Text);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotsubtotal = (Label)e.Row.FindControl("lbtotsubtotal");
                Label lbtotorder = (Label)e.Row.FindControl("lbtotorder");

                lbtotorder.Text = totalQtyCtn.ToString() + " CTN, " + totalQtyPcs.ToString() + " PCS";
                lbtotsubtotal.Text = dSubTotal.ToString("#,##0.00");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabcanvasorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}