using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookuppaymenttab : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totamtsum = 0, counttran=0;
    decimal totamt = 0, counttransum=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                List<cArrayList> arr = new List<cArrayList>();
                bll.vBindingFieldValueToCombo(ref cbstatus, "tab_sta_id");
                cbstatus_SelectedIndexChanged(sender, e);
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqrybysalpay", "emp_cd", "emp_desc", arr);
                cbsalesman_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    private void bindinggrdtab(string sSalesmanCode)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salesman_cd", sSalesmanCode));
            arr.Add(new cArrayList("@tab_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_ttab_paymentreceipt_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_DataBound(object sender, EventArgs e)
    {
        try
        {
            for (int i = grd.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = grd.Rows[i];
                GridViewRow previousRow = grd.Rows[i - 1];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindinggrdtab(cbsalesman.SelectedValue.ToString());
            if (cbstatus.SelectedValue == "TRF" || cbstatus.SelectedValue == "DEL")
            {
                btapply.Visible = false;
                btcancel.Visible = false;
                btupdate.Visible = false;
            }
            else
            {
                btapply.Visible = true;
                btcancel.Visible = true;
                btupdate.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btapply_Click(object sender, EventArgs e)
    {
        try
        {

            //foreach (GridViewRow row in grd.Rows)
            //{
            //    Label lbpaymenttype = (Label)row.FindControl("lbpaymenttype");
            //    if (lbpaymenttype.Text == "CH")
            //    {
            //        sMsg = bll.vLookUp("select dbo.fn_checkclosingcashier()");
            //        if (sMsg == "ok")
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Closing has been closed today already!','Can not import payment because cashier closed','warning');", true);
            //            return;
            //        }
            //    }
            //} By othman update can transfer payment with advance date cashier
            string sMsg = string.Empty;
            string sPaymentNo = string.Empty;
            foreach (GridViewRow row in grd.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                if (chk.Checked)
                {
                    Label lbtab_no = (Label)row.FindControl("lbtab_no");
                    Label lbinvtype = (Label)row.FindControl("lbinvtype");
                    string sMsgduppayment = bll.vLookUp("select dbo.fn_checkduppaymenttab('" + lbtab_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sMsgduppayment != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Payment number ','" + sMsgduppayment + "' already transfer to backoffice,'warning');", true);
                        return;
                    }
                    if (lbinvtype.Text == "NC")
                    {
                        sMsg = bll.vLookUp("select dbo.fn_checktabcanvaspayment('" + lbtab_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sMsg != "ok")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Canvas Tablet related with this payment has not transferred','" + sMsg + "','warning');", true);
                            return;
                        }
                    }
                    if (lbinvtype.Text == "NI")
                    {
                        sMsg = bll.vLookUp("select dbo.fn_checktabreturn('" + lbtab_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sMsg != "ok")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Retur tablet related with this payment has not transferred','" + sMsg + "','warning');", true);
                            return;
                        }
                    }

                    if (lbinvtype.Text == "OI")
                    {
                        sMsg = bll.vLookUp("select dbo.fn_checkoverpaymenttab('" + lbtab_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sMsg != "ok")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Over payment from tablet!','" + sMsg + "','warning');", true);
                            return;
                        }
                    }

                    string sInvoiceType = bll.vLookUp("select dbo.fn_checktabinvoicetype('" + lbtab_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sInvoiceType != "ok")
                    {
                        string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabinvoicestatus('" + sInvoiceType + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sPaymentStatus == "cancel" || sPaymentStatus == "postpone")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Invoice with this payment Postpone / Cancel. : " + sInvoiceType + "','Pls check again','warning');", true);
                            return;
                        }
                    }
                }
            }
            foreach (GridViewRow row in grd.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                Label lbtab_no = (Label)row.FindControl("lbtab_no");
                Label lbsalespointcd = (Label)row.FindControl("lbsalespointcd");
                Label lbtab_dt = (Label)row.FindControl("lbtab_dt");
                Label lbpayment_typ = (Label)row.FindControl("lbpayment_typ");
                Label lbcust_cd = (Label)row.FindControl("lbcust_cd");
                Label lbsalesman_cd = (Label)row.FindControl("lbsalesman_cd");
                Label lbtotamt = (Label)row.FindControl("lbtotamt");
                Label lbbtcheq_no = (Label)row.FindControl("lbbtcheq_no");
                Label lbacc_no = (Label)row.FindControl("lbacc_no");

                if (chk.Checked == true)
                {
                    try
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("tab_no", lbtab_no.Text));
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        string sComment = string.Empty;

                        bll.vBatchTabPaymentReceipt(arr, ref sComment);
                        if (sComment != "ok")
                        {
                            if (sMsg == string.Empty)
                            { sMsg = sComment; }
                            else
                            {
                                sMsg += "," + sComment;
                            }
                        }
                        sPaymentNo = bll.vLookUp("select payment_no from tmst_payment where tab_no='" + lbtab_no.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");


                    }
                    catch (Exception ex)
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@err_source", Request.Cookies["sp"].Value.ToString() + " : fm_lookuppaymenttab"));
                        arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
                        bll.vInsertErrorLog(arr);
                    }
                    //finally
                    //{
                    if ((sMsg != "ok") && (sMsg != string.Empty))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('There some Takeorder/Canvasorder not yet imported','" + sMsg + "','warning')", true);
                        return;
                    }

                    //}
                }

            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "window.opener.RefreshData('" + sPaymentNo + "');window.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbtab_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbtab_no");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_no", lbtab_no.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddtl, "sp_ttab_paymentreceipt_dtl_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            grd.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_ttab_paymentreceipt_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            Label lbtabno = (Label)grd.Rows[e.RowIndex].FindControl("lbtab_no");
            TextBox txmanualno = (TextBox)grd.Rows[e.RowIndex].FindControl("txmanualno");
            TextBox txtotamt = (TextBox)grd.Rows[e.RowIndex].FindControl("txtotamt");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_no", lbtabno.Text));
            arr.Add(new cArrayList("@manual_no", txmanualno.Text));
            arr.Add(new cArrayList("@totamt", txtotamt.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateTabPaymentReceipt(arr);
            grd.EditIndex = -1;

            arr.Clear();
            arr.Add(new cArrayList("@tab_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_ttab_paymentreceipt_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();

            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbtabno = (Label)row.FindControl("lbtab_no");
                        Label lbpaymenttype = (Label)row.FindControl("lbpaymenttype");
                        if (lbpaymenttype.Text == "CH")
                        {
                            // Check Invoice Already Transfer 
                            string sInvoiceType = bll.vLookUp("select dbo.fn_checktabinvoicetype('" + lbtabno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                            if (sInvoiceType != "ok")
                            {
                                string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabinvoicestatus('" + sInvoiceType + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                                if (sPaymentStatus == "transfer")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Invoice with this Payment Already Transfer.  : " + sInvoiceType + "','Pls transfer this payment.','warning');", true);
                                    return;
                                }
                            }
                        }
                        arr.Clear();
                        arr.Add(new cArrayList("@tab_no", lbtabno.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vDelTabPaymentReceipt(arr);
                    }
                }
            }
            bindinggrdtab(cbsalesman.SelectedValue.ToString());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Payment Tablet has been cancelled','Cancel payment succeeded','success');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btupdate_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();

            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbtabno = (Label)row.FindControl("lbtab_no");
                        Label lbpaymenttype = (Label)row.FindControl("lbpaymenttype");
                        if (lbpaymenttype.Text == "CQ" || (lbpaymenttype.Text == "BT"))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Payment type Cheque and Bank transfer can not postpone!','Cheque and Bank Transfer ','warning');", true);
                            return;
                        }
                        else
                        {
                            // Check Invoice Already Transfer 
                            string sInvoiceType = bll.vLookUp("select dbo.fn_checktabinvoicetype('" + lbtabno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                            if (sInvoiceType != "ok")
                            {
                                string sPaymentStatus = bll.vLookUp("select dbo.fn_checktabinvoicestatus('" + sInvoiceType + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                                if (sPaymentStatus == "transfer")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Invoice with this Payment Already Transfer.  : " + sInvoiceType + "','Pls transfer this payment.','warning');", true);
                                    return;
                                }
                            }
                        }
                        arr.Clear();
                        arr.Add(new cArrayList("@tab_no", lbtabno.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateTabPaymentReceiptByDate(arr);
                    }
                }
            }
            bindinggrdtab(cbsalesman.SelectedValue.ToString());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Payment Tablet has been change to next day','Advanced date payment succeeded','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {

            bindinggrdtab(cbsalesman.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            grd.EditIndex = -1;
            bindinggrdtab(cbsalesman.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbtotamt = (Label)e.Row.FindControl("lbtotamt");

                if (lbtotamt != null)
                    totamt = decimal.Parse(lbtotamt.Text);
                else
                    totamt = 0;
                totamtsum = totamtsum + totamt;
                counttransum = counttransum + 1;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotamtsum = (Label)e.Row.FindControl("lbtotamtsum");
                Label lbcounttran = (Label)e.Row.FindControl("lbcounttran");

                lbtotamtsum.Text = totamtsum.ToString("#,##0.00");
                lbcounttran.Text = counttransum.ToString("#,##0.00");

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookuppaymenttab");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)grd.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in grd.Rows)
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
}