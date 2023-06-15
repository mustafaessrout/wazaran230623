using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_tabsalesorder : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToCombo(ref cbstatus, "tab_sta_id");
                cbstatus_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
                Response.Redirect("fm_ErrorPage.aspx");
            }
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
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_salesorder_get", arr);
            if (grdtabdtl.Rows.Count > 0)
            {
                Label lbtotqty = (Label)grdtabdtl.FooterRow.FindControl("lbtotqty");
                Label lbtotsubtotal = (Label)grdtabdtl.FooterRow.FindControl("lbtotsubtotal");
                lbtotsubtotal.Text = bll.vLookUp("select sum(qty * unitprice) from ttablet_salesorder where so_cd='" + lbso_cd.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotqty.Text = bll.vLookUp("select sum(qty) from ttablet_salesorder where so_cd='" + lbso_cd.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            }
            bll.vBindingGridToSp(ref grddisc, "sp_ttab_salesdiscount_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btapply_Click(object sender, EventArgs e)
    {
        try
        {
            btapply.Enabled = false;
            btapply.Visible = true;
            int nCount = 0;
            string sSoNo = "";
            foreach (GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbso = (Label)row.FindControl("lbso_cd");
                        string sMsgdupso = bll.vLookUp("select dbo.fn_checkdupsotab('" + lbso.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sMsgdupso != "ok")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Sales order number ','" + sMsgdupso + "' already transfer to backoffice,'warning');", true);
                            return;
                        }
                        string sCheck = bll.vLookUp("select dbo.fn_checktabsaleshasdiscount('" + lbso.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sCheck != "ok")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sCheck + "','Pls check again','warning');", true);
                            return;
                        }

                        string sCashInvoice = bll.vLookUp("select dbo.fn_checkstockavailablefromtablet('" + lbso.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sCashInvoice != "ok")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Ther is product stock not enough for order cash : " + sCashInvoice + "','You only can postpone until stock enough','warning');", true);
                            return;
                        }

                        string sInvoiceType = bll.vLookUp("select dbo.fn_checktabpaymenttype('" + lbso.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        string[] sPayment = sInvoiceType.Split('_');
                        if (sPayment[0] == "CASH")
                        {
                            string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabpaymentstatus('" + lbso.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
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
                Label lbwhs_cd = (Label)row.FindControl("lbwhs_cd");
                Label lbbin_cd = (Label)row.FindControl("lbbin_cd");
                Label lbrdonoff = (Label)row.FindControl("lbrdonoff");
                Label lbtabno = (Label)row.FindControl("lbtabno");
                Label lbso_cd = (Label)row.FindControl("lbso_cd");
                if (chk.Checked == true)
                {
                    try
                    {
                        string sTermCd = bll.vLookUp("select payment_term from tmst_customer where cust_cd='" + lbcust_cd.Text.ToString() + "' and salespointcd='" + lbsalespointcd.Text + "'");
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                        arr.Add(new cArrayList("@so_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@so_typ", "TO"));
                        arr.Add(new cArrayList("@cust_cd", lbcust_cd.Text));
                        arr.Add(new cArrayList("@salesman_cd", lbsalesman_cd.Text));
                        arr.Add(new cArrayList("@term_cd", sTermCd));
                        arr.Add(new cArrayList("@currency_cd", bll.vLookUp("select dbo.fn_getcontrolparameter('currency')")));
                        arr.Add(new cArrayList("@ref_no", lbmanual_no.Text));
                        arr.Add(new cArrayList("@remark", ""));
                        arr.Add(new cArrayList("@so_sta_id", "N"));
                        arr.Add(new cArrayList("@so_source", "TAB"));
                        arr.Add(new cArrayList("@whs_cd", lbwhs_cd.Text));
                        arr.Add(new cArrayList("@bin_cd", lbbin_cd.Text));
                        arr.Add(new cArrayList("@rdonoff", lbrdonoff.Text));
                        arr.Add(new cArrayList("@tabno", lbso_cd.Text));
                        arr.Add(new cArrayList("@manual_no", lbmanual_no.Text));
                        bll.vInsertMstSalesOrder(arr, ref sSoNo);
                        arr.Clear();
                        arr.Add(new cArrayList("@salesman_cd", lbsalesman_cd.Text));
                        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                        arr.Add(new cArrayList("@so_cd", sSoNo));
                        arr.Add(new cArrayList("@tabno", lbso_cd.Text));
                        bll.vInserttabsalesOrderDtl(arr);
                        arr.Clear();
                        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                        arr.Add(new cArrayList("@so_cd", sSoNo));
                        arr.Add(new cArrayList("@tabno", lbso_cd.Text));
                        bll.vTabsalesorderdiscount(arr);
                        arr.Clear();
                        arr.Add(new cArrayList("@so_cd", sSoNo));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@original_dt", DateTime.ParseExact(lbso_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@invoice_dt", DateTime.ParseExact(lbso_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@loading_dt", bll.vLookUp("select delivery_dt from ttab_sales_info where tab_no='" + lbso_cd.Text.ToString() + "'").Substring(0, 10)));
                        arr.Add(new cArrayList("@app_dt", DateTime.ParseExact(lbso_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@app_sta_id", "A")); // Default app_sta_id='W'
                        arr.Add(new cArrayList("source_info", "TAB"));
                        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@paidbycash", 0));
                        arr.Add(new cArrayList("@prevstk", 0));
                        arr.Add(new cArrayList("merchandizer_cd", null));
                        bll.vInsertSalesorderInfo(arr);

                        nCount++;
                    }
                    catch (Exception ex)
                    {
                        //  List<cArrayList> arr = new List<cArrayList>();
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@err_source", "save salesorder"));
                        arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
                        bll.vInsertErrorLog(arr);
                    }
                }

            }
            //if (nCount > 0)
            //{
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Transfer to back office has been saved', 'Canvas Order " + sSoNo + "', 'success');", true);
            //    Response.Write("<script language='javascript'>window.alert('Transfer to back office has been saved')';</script>");
            //    bll.vBindingGridToSp(ref grdtab, "sp_ttab_mst_canvasorder_get");
            //    bindinggrdtab();
            //}

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "window.opener.RefreshData('" + sSoNo + "');window.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    private void bindinggrdtab()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtab, "sp_ttab_mst_salesorder_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdtab.Rows)
            {
                CheckBox chk = (row.Cells[0].FindControl("chk") as CheckBox);
                if (chk.Checked)
                {
                    Label lbso_cd = (Label)row.FindControl("lbso_cd");
                    string sCanvasPending = bll.vLookUp("select dbo.fn_tabsalesordercheck('" + lbso_cd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')").ToString();
                    if (sCanvasPending != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('SOME DATA NOT YET TRANSFER PLEASE SYNC TAB DATA OR CONTACT WAZARAN','" + sCanvasPending + "','warning');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                        chk.Checked = false;
                        return;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            bindinggrdtab();
            if (cbstatus.SelectedValue == "TRF" || cbstatus.SelectedValue == "DEL")
            {
                btapply.Visible = false;
                btcancel.Visible = false;
                btpostpone.Visible = false;
            }
            else
            {
                btapply.Visible = true;
                btcancel.Visible = true;
                btpostpone.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {

            if (bll.nCheckAccess("tabsalescancel", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To cancel this Sales Order. contact Administrator !!','warning');", true);
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
                        Label lbsocd = (Label)row.FindControl("lbso_cd");
                        // Check Payment Already Transfer 
                        string sInvoiceType = bll.vLookUp("select dbo.fn_checktabpaymenttype('" + lbsocd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        string[] sPayment = sInvoiceType.Split('_');
                        if (sPayment[0] == "CASH")
                        {
                            string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabpaymentstatus('" + lbsocd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                            if (sPaymentStatus == "transfer")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Payment with this invoice Already Transfer. : " + sPayment[1] + "','Pls transfer this invoice.','warning');", true);
                                return;
                            }
                        }
                        arr.Clear();
                        arr.Add(new cArrayList("@so_cd", lbsocd.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vDeleteTTabletSalesorder(arr);
                    }
                }
            }
            bindinggrdtab();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Tablet Salesorder has been deleted','deleted successfully','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
     protected void btpostpone_Click(object sender, EventArgs e)
    {
        try
        {

            if (bll.nCheckAccess("tabsalespostpone", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To cancel this Sales Order. contact Administrator !!','warning');", true);
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
                        Label lbso_dt = (Label)row.FindControl("lbso_dt");
                        DateTime dtsystem = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime dtso = System.DateTime.ParseExact(lbso_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Label lbso = (Label)row.FindControl("lbso_cd");
                        if (dtsystem == dtso)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "sweetAlert('You can not postpone this TO because same with sys date','" + lbso.Text + "','warning');", true);
                            return;
                        }
                    }
                }
            }
            foreach (GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbsocd = (Label)row.FindControl("lbso_cd");
                        // Check Payment Already Transfer 
                        string sInvoiceType = bll.vLookUp("select dbo.fn_checktabpaymenttype('" + lbsocd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        string[] sPayment = sInvoiceType.Split('_');
                        if (sPayment[0] == "CASH")
                        {
                            string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabpaymentstatus('" + lbsocd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                            if (sPaymentStatus == "transfer")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Payment with this invoice Already Transfer. : " + sPayment[1] + "','Pls transfer this invoice.','warning');", true);
                                return;
                            }
                        }
                        arr.Clear();
                        arr.Add(new cArrayList("@so_cd", lbsocd.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateTTabletSalesorder(arr);
                    }
                }
            }
            bindinggrdtab();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Tablet Salesorder has been deleted','deleted successfully','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
     protected void grdtab_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
        try
        {
            grdtab.PageIndex = e.NewPageIndex;
            bindinggrdtab();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_tabsalesorder");
            Response.Redirect("fm_ErrorPage.aspx");
        }
     }
}