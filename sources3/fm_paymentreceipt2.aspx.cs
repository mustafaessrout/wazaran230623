using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentreceipt2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    decimal totAmt, remaingAmt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", 0));
                //bll.vBindingFieldValueToCombo(ref cbpaymnttype, "payment_typ", false);
                arr.Clear();
                arr.Add(new cArrayList("usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelWrkPaymentInvoice(arr);
                arr.Clear();
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                bll.vBindingFieldValueToComboWithChoosen(ref cbsource, "source_order");
                //cbsource_SelectedIndexChanged(sender, e);
                //cbpaymnttype_SelectedIndexChanged(sender, e);
                //dtpayment.CssClass = "form-control ro";
                cd.v_disablecontrol(dtpayment);
                dtpayment.Text = Request.Cookies["waz_dt"].Value.ToString();
                dtpayment_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                txduedate_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                txpaymentdate_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //txcust.CssClass = "form-control";
                //cbgroup.CssClass = "form-control ro";
                //dtdue.CssClass = "form-control ro";
                cd.v_enablecontrol(txcust);
                cd.v_disablecontrol(cbgroup);
                cd.v_disablecontrol(dtdue);
                rdpaid.SelectedValue = "C";
                lbtotalpayment.Text = "0";
                //btprint.CssClass = "btn btn-default ro";
                //btprintall.CssClass = "btn btn-default ro";
                cd.v_hiddencontrol(btprint);
                cd.v_hiddencontrol(btprintall);
                bll.vBindingFieldValueToComboWithChoosen(ref cbmanual, "paymentmode");
                chonepct.Enabled = false;
                //txsearchinv.CssClass = cd.csstextro; // Faruk request 
                cd.v_disablecontrol(txsearchinv);
                bll.vBindingFieldValueToComboWithChoosen(ref ddlPaymentAttribute, "payment_att");
                //cd.v_disablecontrol(bttabno);
                //cd.v_disablecontrol(btreceiptno);

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;HttpCookie uok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@cust_cd", prefixText));
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        // bll.vSearchCustomerBySales(arr, ref rs);
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void btsearchcust_Click(object sender, EventArgs e)
    {
        try
        {
            // Enh : 22 June 2019 : Customer Transfer Blocked - CIN
            string sCustomerTransferBlock = bll.vLookUp("select dbo.fn_customertransferpending('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sCustomerTransferBlock != "ok")
            {
                hdcust.Value = ""; txcust.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused there is pending in customer transfer ','" + sCustomerTransferBlock + "','warning');", true);
                return;
            }

            txsearchinv_AutoCompleteExtender.ContextKey = "C" + hdcust.Value.ToString();
            //txcust.CssClass = "form-control ro";
            cd.v_disablecontrol(txcust);
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vInsertWrkPaymentInvoice(arr);
            arr.Clear();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //   bll.vBindingGridToSp(ref grd, "sp_twrk_paymentinvoice_get", arr);

            cbgroup.SelectedValue = bll.vLookUp("select cusgrcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            cbsalesman.SelectedValue = bll.vLookUp("select salesman_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //cbsalesman.CssClass = "form-control ro";
            cd.v_disablecontrol(cbsalesman);
            //cbsalesman.Enabled = false;
            string sPromised = bll.vLookUp("select dbo.fn_checkexistpaymentpromised('" + hdcust.Value.ToString() + "',default,'" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sPromised != "ok")
            {
                lbpromised.Text = sPromised;
                lbpromised.ToolTip = "Amount:" + bll.vLookUp("select top 1 amt from tpayment_promised where cust_cd='" + hdcust.Value.ToString() + "' and promised_sta_id='N' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            }
            else
            {
                lbpromised.Text = "No Promised";
            }
            //cbsource.CssClass = "form-control ro";
            //txcust.CssClass = "form-control ro";
            //rdpaid.CssClass = "form-control ro";
            cd.v_disablecontrol(cbsource);
            cd.v_disablecontrol(txcust);
            cd.v_disablecontrol(rdpaid);
            txdisc.Text = "0.00";
            txtRound.Text = "0.00";
            arr.Clear();

            var userCode = "C" + hdcust.Value.ToString();
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "ShowProgress();", true);
            hdfinalcust.Value = "C" + hdcust.Value.ToString();
            BindGridInvoice(userCode);
            cd.v_enablecontrol(txsearchinv);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    void BindGridInvoice(string userCode)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "ShowProgress();", true);
            if (cbmanual.SelectedValue == "Automatic" && hdfinalcust.Value != "")
            {
                if (userCode == "" && hdfinalcust.Value == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select  customer group / name','group / name','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "ShowProgress();", true);
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@invcheck", userCode == "" ? hdfinalcust.Value : userCode));
                    arr.Add(new cArrayList("@inv_no", null));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdInvoice, "sp_tdosales_invoice_getByType", arr);         
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                    hdfinalcust.Value = userCode;
                }
            }
            else
            {
                grdInvoice.DataSource = null;
                grdInvoice.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            BindingGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void BindingGrid()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_paymentinvoice_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void chdisc_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grd.Rows)
            {
                if ((row.RowType == DataControlRowType.DataRow) && (row.RowState == DataControlRowState.Normal || row.RowState == DataControlRowState.Alternate))
                {
                    Label lbpaid = (Label)row.FindControl("lbpaid");
                    Label lbremain = (Label)row.FindControl("lbremain");
                    Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
                    Label lbamt = (Label)row.FindControl("lbamt");
                    Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");
                    double dCheckAmt = Convert.ToDouble(lbamt.Text) - (Convert.ToDouble(lbpaid.Text) + Convert.ToDouble(lbremain.Text) + Convert.ToDouble(lbdisconepct.Text));
                    CheckBox chk = (CheckBox)row.FindControl("chdisc");
                    if (chk.Checked)
                    {
                        string sCheck1pct = bll.vLookUp("select dbo.fn_checkdiscountpayment1pct('" + lbinvoiceno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                        if (sCheck1pct == "ok")
                        {
                            chk.Checked = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('You can not switch ON discount','This invoice supposed more than 7 days payment completed','warning');", true);
                            return;
                        }
                        //if ((dCheckAmt != 0) || (dCheckAmt > 0))
                        //{
                        //    chk.Checked = false;
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('You can not switch ON discount','Not completed payment','warning');", true);
                        //    return;
                        //}
                    }
                    // Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                    arr.Add(new cArrayList("@onoff", chk.Checked));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateWrkPaymentInvoiceByOnOff(arr);
                }
                else if ((row.RowType == DataControlRowType.DataRow) && (row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    Label lbpaid = (Label)row.FindControl("lbpaid");
                    Label lbremain = (Label)row.FindControl("lbremain");
                    Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
                    Label txamt = (Label)row.FindControl("txamt");
                    double dCheckAmt = Convert.ToDouble(txamt.Text) - (Convert.ToDouble(lbpaid.Text) + Convert.ToDouble(lbremain.Text) + Convert.ToDouble(lbdisconepct.Text));
                    CheckBox chk = (CheckBox)row.FindControl("chdisc");
                    if (chk.Checked)
                    {
                        if (dCheckAmt != 0)
                        {
                            chk.Checked = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('You can not switch ON discount','Not completed payment','warning');", true);
                            return;
                        }
                    }
                    Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                    arr.Add(new cArrayList("@onoff", chk.Checked));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateWrkPaymentInvoiceByOnOff(arr);
                }
            }
            BindingGrid();
            vGridFooter();
            //SwitchOff();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal))
            {
                Label lbinvoiceno = (Label)e.Row.FindControl("lbinvoiceno");
                Label lbdisconepct = (Label)e.Row.FindControl("lbdisconepct");
                Label lbapply = (Label)e.Row.FindControl("lbamt");
                Label lbdsc = (Label)e.Row.FindControl("lbdisc");
                Label lbRnd = (Label)e.Row.FindControl("lbRound");
                Label lbrmn = (Label)e.Row.FindControl("lbremain");
                Label lbpd = (Label)e.Row.FindControl("lbpaid");
                Label lboutstanding = (Label)e.Row.FindControl("lboutstanding");
                CheckBox chdisc = (CheckBox)e.Row.FindControl("chdisc");
                // we r not giving edit facility for validation purpose.
                chdisc.Enabled = false;
                string sInv = bll.vLookUp("select dbo.fn_checkinvreceived('" + lbinvoiceno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                if (sInv != "ok")
                {
                    e.Row.BackColor = System.Drawing.Color.SlateGray;
                    e.Row.ToolTip = "Sys No Invoice " + lbinvoiceno.Text + " is not yet received !";
                    e.Row.Enabled = false;
                }
                if (Convert.ToDouble(lbdisconepct.Text) > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.GreenYellow;
                    e.Row.ToolTip = "Hit by discount 1 %";
                    // lboutstanding.Text = lbrmn.Text;
                }
                //else
                //{
                //    lboutstanding.Text = Math.Round((Math.Round(Convert.ToDouble(lbrmn.Text), 2) - (Math.Round(Convert.ToDouble(lbapply.Text), 2) + Math.Round(Convert.ToDouble(lbdisconepct.Text), 2) + Math.Round(Convert.ToDouble(lbdsc.Text), 2))), 2).ToString();
                //}


            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit))
            {
                Label lbinvoiceno = (Label)e.Row.FindControl("lbinvoiceno");
                Label lbdisconepct = (Label)e.Row.FindControl("lbdisconepct");
                CheckBox chdisc = (CheckBox)e.Row.FindControl("chdisc");
                string sInv = bll.vLookUp("select dbo.fn_checkinvreceived('" + lbinvoiceno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                if (sInv != "ok")
                {
                    e.Row.BackColor = System.Drawing.Color.SlateGray;
                    e.Row.ToolTip = "Sys No Invoice " + lbinvoiceno.Text + " is not yet received !";
                    e.Row.Enabled = false;
                }
                if (Convert.ToDouble(lbdisconepct.Text) > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.GreenYellow;
                    e.Row.ToolTip = "Hit by discount 1 %";
                }
                //  chdisc.Checked = false;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void rdpaid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grd.DataSource = null;
            grd.DataBind();
            if (rdpaid.SelectedValue.ToString() == "C")
            {
                cbgroup.Items.Clear();
                //txcust.CssClass = "form-control";
                //cbgroup.CssClass = "form-control ro";
                cd.v_disablecontrol(txcust);
                cd.v_disablecontrol(cbgroup);
                txcust.Text = String.Empty;
                hdcust.Value = String.Empty;
                //cbsalesman.CssClass = "form-control ro";
                cd.v_disablecontrol(cbsalesman);
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);

            }
            else
            {

                bll.vBindingFieldValueToCombo(ref cbgroup, "cusgrcd");
                cbgroup_SelectedIndexChanged(sender, e);
                //txcust.CssClass = "form-control ro";
                //cbgroup.CssClass = "form-control";
                cd.v_disablecontrol(txcust);
                cd.v_enablecontrol(cbgroup);
                hdcust.Value = String.Empty;
                txcust.Text = String.Empty;
                cbsalesman.Items.Clear();
                //cbsalesman.CssClass = "form-control ro";
                cd.v_disablecontrol(cbsalesman);
            }
            //rdpaid.CssClass = "form-control ro";
            cd.v_disablecontrol(rdpaid);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btapply_Click(object sender, EventArgs e)
    {
        // //List<cArrayList> arr = new List<cArrayList>();
        // //grd.Enabled = true;
        // double dRemainPaid = 0; double dAmt = 0; double dAmtTotal = 0;
        // if (!double.TryParse(txamount.Text, out dAmt))
        // {
        //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Amout must be numeric','Pls check amount payment','warning');", true);
        //     return;
        // }
        // dRemainPaid = Convert.ToDouble(txamount.Text);

        // if (grd.Rows.Count == 0)
        // {
        //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrtx", "sweetAlert('No invoice available for paid!','Try another customer','warning');", true);
        //     return;
        // }
        // List<cArrayList> arr = new List<cArrayList>();
        // double dAmtPaid = 0;
        // foreach (GridViewRow row in grd.Rows)
        // {
        //     if ((row.RowType == DataControlRowType.DataRow) && row.Enabled == true)
        //     {
        //         //double dDisconepct = 0;
        //         Label lbremain = (Label)row.FindControl("lbremain");
        //         Label lbamt = (Label)row.FindControl("lbamt");
        //         Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
        //         Label lbdisc = (Label)row.FindControl("lbdisc");
        //         Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");
        //         CheckBox chdisc = (CheckBox)row.FindControl("chdisc");
        //         dAmtPaid = 0;

        //         if (dRemainPaid > 0)
        //         {
        //             if (chdisc.Checked)
        //             {
        //                 dAmtPaid = dRemainPaid - Convert.ToDouble(lbremain.Text) - Convert.ToDouble(lbdisconepct.Text);
        //                 dAmtPaid = Math.Round(dAmtPaid, 2);
        //                 if (dAmtPaid >= 0)
        //                 {
        //                     lbamt.Text = (Convert.ToDouble(lbremain.Text) - Convert.ToDouble(lbdisconepct.Text)).ToString(); ;
        //                 }
        //                 else { lbamt.Text = dRemainPaid.ToString(); }

        //             }
        //             else
        //             {
        //                 dAmtPaid = dRemainPaid - Convert.ToDouble(lbremain.Text) - Convert.ToDouble(lbdisconepct.Text);
        //                 dAmtPaid = Math.Round(dAmtPaid, 2);
        //                 if (dAmtPaid >= 0)
        //                 {
        //                     lbamt.Text = lbremain.Text;
        //                 }
        //                 else { lbamt.Text = dRemainPaid.ToString(); }

        //             }
        //             //Make Disc 1 Pct On Or Off

        //         }
        //         else
        //         {
        //             dRemainPaid = 0;
        //             lbamt.Text = dRemainPaid.ToString();
        //         }
        //         arr.Clear();
        //         arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
        //         arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //         arr.Add(new cArrayList("@amt", lbamt.Text));
        //         arr.Add(new cArrayList("@disc_amt", "0"));
        //         bll.vUpdateWrkPaymentInvoice(arr);
        //         //arr.Clear();
        //         //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));

        //         dAmtTotal += Math.Round(Convert.ToDouble(lbamt.Text), 2);
        //         dRemainPaid -= Math.Round(Convert.ToDouble(lbamt.Text), 2);
        //         dRemainPaid = Math.Round(dRemainPaid, 2);
        //     }
        // }

        // BindingGrid();
        // foreach (GridViewRow row in grd.Rows)
        // {
        //     if (row.RowType == DataControlRowType.DataRow)
        //     {
        //         CheckBox chk = (CheckBox)row.FindControl("chdisc");


        //         if (chk.Checked)
        //         {
        //             Label lbremain = (Label)row.FindControl("lbremain");
        //             Label lbdisc1pct = (Label)row.FindControl("lbdisconepct");
        //             Label lbamtt = (Label)row.FindControl("lbamt");
        //             if (Math.Round(Convert.ToDouble(lbamtt.Text), 2) - (Math.Round(Convert.ToDouble(lbdisc1pct.Text), 2) + Math.Round(Convert.ToDouble(lbremain.Text), 2)) > 1)
        //             {
        //                 chk.Checked = false;
        //                 lbdisc1pct.Text = "0";
        //             }

        //         }
        //     }
        // }
        // vGridFooter();
        // btapply.CssClass = "divhid";
        // txamount.CssClass = "form-control ro";
        // txamount.Enabled = false;
        // txcust.CssClass = "form-control ro";
        //// txcust.Enabled = false;
        // cbgroup.CssClass = "form-control ro";
        // cbgroup.Enabled = false;
        // grd.DataBind();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        BindingGrid();
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        BindingGrid();
        vGridFooter();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
        
        string _paymentnumber = String.Empty;
        if (cbpaymnttype.SelectedValue.ToString() == "IT")
        {
            if (dthorec.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('HO Received date must filled in!','HO Voucher Received Date','warning');", true);
                return;
            }
            if (txhovoucher.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('HO Received voucher no must be filled in!','HO Voucher Number','warning');", true);
                return;
            }
            if (!fup.HasFile)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please attach document Collection from HO to system','HO will make clearance','warning');", true);
                return;
            }
        }

        if (cbpaymnttype.SelectedValue.ToString() == "CQ")
        {
            if (dtcheque.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cheque due date must be select !','Cheque Due Date','warning');", true);
                return;
            }

            if (!fup.HasFile)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please attach document Cheque to system','HO will make clearance','warning');", true);
                return;
            }

        }

        if (cbpaymnttype.SelectedValue.ToString() == "BT")
        {
            if (!fup.HasFile)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please attach document Bank Transfer to system','HO will make clearance','warning');", true);
                return;

            }
        }
        if (rdpaid.SelectedValue.ToString() == "C")
        {
            if ((hdcust.Value.ToString() == "") || (hdcust.Value.Equals(null)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('customer must be selected !','customer','warning');", true);
                return;
            }
        }

        if (hdcust.Value.ToString() == "")
        {
            if (rdpaid.SelectedValue.ToString() == "C")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('customer must be selected !','customer','warning');", true);
                return;
            }
        }

        if ((cbpaymnttype.SelectedValue.ToString() == "BT" || (cbpaymnttype.SelectedValue.ToString() == "CQ")))
        {
            if (txdocno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cheque/Transfer No. can not empty !','Bank Transfer/Cheque','warning');", true);
                return;
            }
            else if (ddlPaymentAttribute.SelectedValue == "OT" && cbsource.SelectedValue== "MAN")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select payment status  !','Payment Status','warning');", true);
                return;
            }
        }

        double dTotalAmount = 0;

        if (!double.TryParse(txamount.Text, out dTotalAmount))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Total Amount must be numeric','Check Total Amount','warning');", true);
            return;
        }


        if (grd.Rows.Count > 0)
        {
            Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
            Label lbtotRound = (Label)grd.FooterRow.FindControl("lbtotRound");
            Label lbtotdisc = (Label)grd.FooterRow.FindControl("lbtotdisc");
            Label lbtotOutStand = (Label)grd.FooterRow.FindControl("lbtotOutStand");



            lbtotamt.Text = bll.vLookUp("select sum(amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            lbtotRound.Text = bll.vLookUp("select sum(round_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            lbtotdisc.Text = bll.vLookUp("select sum(disc_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            lbtotOutStand.Text = bll.vLookUp("select sum(outstanding) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
        }
        if (cbpaymnttype.SelectedValue.ToString() == "CH")
        {
            if (grd.Rows.Count == 1)
            {
                Label lbtotOutStand = (Label)grd.FooterRow.FindControl("lbtotOutStand");

                if (Convert.ToDouble(lbtotOutStand.Text) < 1 && Convert.ToDouble(lbtotOutStand.Text) > 0 && Convert.ToDouble(lbsuspense.Text) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please add round value','round value','warning');", true);
                    return;
                }
            }
            else if (grd.Rows.Count > 1)
            {
                Label lbtotOutStand = (Label)grd.FooterRow.FindControl("lbtotOutStand");
                if (Convert.ToDouble(lbtotOutStand.Text) < 1 && Convert.ToDouble(lbtotOutStand.Text) > 0 && Convert.ToDouble(lbsuspense.Text) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please add round value','round value','warning');", true);
                    return;
                }
            }
        }
        List<cArrayList> arrCheck = new List<cArrayList>();
        DataTable dtpaymentinvoice = new DataTable();
        arrCheck.Add(new cArrayList("@invcheck", hdfinalcust.Value));
        arrCheck.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        dtpaymentinvoice = cdl.GetValueFromSP("sp_tdosales_invoice_searchWithInv", arrCheck);

        string ka = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"); ;
        int cnt = 0;

        foreach (DataRow dr in dtpaymentinvoice.Rows)
        {
            string balance = bll.vLookUp("select dbo.fn_gettempbalance('" + dr["inv_no"].ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (Convert.ToDecimal(balance) > 0)
            {
                cnt += 1;
            }
        }

        if (rdpaid.SelectedValue != "G")
        {
            if ((cnt != grd.Rows.Count))
            {
                if (cnt > 1 && Convert.ToDecimal(lbsuspense.Text) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You must use first suspense amount, because remaining invoice available.','use suspense amount','warning');", true);
                    return;
                }
            }
        }


        double dCheck; double dTot = 0; double dDiscount = 0; double dRound = 0; double dCheckRound = 0;
        
        if (!chAdvance.Checked)
        {
            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label txamt = (Label)row.FindControl("lbamt");
                    Label lbdisc = (Label)row.FindControl("lbdisc");
                    Label lbRound = (Label)row.FindControl("lbRound");
                    Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");
                    Label lbpaid = (Label)row.FindControl("lbpaid");
                    Label lbremain = (Label)row.FindControl("lbremain");
                    Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
                    Label lbtotamt = (Label)row.FindControl("lbtotamt");

                    double dPaid = Math.Round(Convert.ToDouble(lbpaid.Text), 2);
                    double dBalance = Math.Round(Convert.ToDouble(lbremain.Text), 2);
                    double dDiscOnePct = Math.Round(Convert.ToDouble(lbdisconepct.Text), 2);
                    double dDisc = Math.Round(Convert.ToDouble(lbdisc.Text), 2);
                    double dRou = Math.Round(Convert.ToDouble(lbRound.Text), 2);
                    double dTotAmt = Math.Round(Convert.ToDouble(lbtotamt.Text), 2);


                    if (!double.TryParse(lbdisc.Text, out dCheck))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Discount must be numeric !!','discount','warning');", true);
                        return;
                    }
                    if (!double.TryParse(lbRound.Text, out dCheckRound))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Round must be numeric !!','Round','warning');", true);
                        return;
                    }
                    dDiscount += dCheck;
                    dRound += dCheckRound;
                    dCheck = 0;
                    dCheckRound = 0;
                    if (!double.TryParse(txamt.Text, out dCheck))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('All amount pay must be numeric !!','payment','warning');", true);
                        return;
                    }
                    string sMsg = bll.vLookUp("select dbo.fn_checkcndn('" + lbinvoiceno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if ((sMsg != "ok") && (dCheck > 0))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sMsg + "','CN DN Outstanding Approval','warning');", true);
                        return;
                    }
                    else { dTot += dCheck; }
                }
            }
            if (dTot == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No available invoice to be paid','Check your cust balance','warning');", true);
                return;
            }
        }    
        

        
        if (txmanualno.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual No can not empty !!','manual no','warning');", true);
            return;
        }
        string sManNo = bll.vLookUp("select dbo.fn_checkmanualno('payment','" + txmanualno.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
        if (sManNo != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sManNo + "','manual no','warning');", true);
            return;
        }

        string scd = bll.vLookUp("select  dbo.sfnGetcountdown('"+ Request.Cookies["sp"].Value.ToString() + "')");
        if (scd == "0" && txreceiptno.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Block entry because deadline to daily closing !','Please daily closing !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "dvshow.setAttribute('class','divhid');", true);
            return;
        }

        if (cbpaymnttype.SelectedValue.ToString() == "IT")
        {
        }
        //Check payment booking 

        double dTotqty = 0; double dTotRound = 0; double dqty = 0; double dqtyRound = 0;
        
        if (!chAdvance.Checked)
        {
            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbamt = (Label)row.FindControl("lbamt");
                    Label lbdisc = (Label)row.FindControl("lbdisc");
                    Label lbRound = (Label)row.FindControl("lbRound");
                    if (double.TryParse(lbamt.Text, out dqty))
                    {
                        dTotqty += dqty;
                    }
                    if (double.TryParse(lbdisc.Text, out dqty))
                    {
                        dTotqty += dqty;
                    }
                    if (double.TryParse(lbRound.Text, out dqtyRound))
                    {
                        dTotRound += dqtyRound;
                    }
                }
            }
        }    
        
        double dRemain = 0;
        List<cArrayList> arr = new List<cArrayList>();
        
            string sPaymentStaID = string.Empty;
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@payment_dt", DateTime.ParseExact(dtpayment.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@payment_typ", cbpaymnttype.SelectedValue.ToString()));
            if (rdpaid.SelectedValue.ToString() == "C")
            {
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            }
            //  arr.Add(new cArrayList("@totamt", txamount.Text));
            if (cbpaymnttype.SelectedValue.ToString() == "CH")
            {
                sPaymentStaID = "C"; //Payment cash tidak melalui cashier received tapi langsung completed potong
            }
            else if ((cbpaymnttype.SelectedValue.ToString() == "BT") || (cbpaymnttype.SelectedValue.ToString() == "CQ") || (cbpaymnttype.SelectedValue.ToString() == "IT"))
            {
                sPaymentStaID = "N";
            }
            else
            {
                sPaymentStaID = "N";
            }
            arr.Add(new cArrayList("@payment_sta_id", sPaymentStaID)); //aslinya cbstatus
            arr.Add(new cArrayList("@btcheq_no", txdocno.Text));
            arr.Add(new cArrayList("@acc_no", cbbankcq.SelectedValue.ToString()));
            arr.Add(new cArrayList("@manual_no", txmanualno.Text));
            arr.Add(new cArrayList("@source_order", cbsource.SelectedValue.ToString()));
            arr.Add(new cArrayList("@remark", txrenark.Text));
            arr.Add(new cArrayList("@voucherho_no", txhovoucher.Text));
            if (dthorec.Text == String.Empty)
            {
                dthorec.Text = Request.Cookies["waz_dt"].Value.ToString();
            }
            arr.Add(new cArrayList("@horec_dt", DateTime.ParseExact(dthorec.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@rdpaidfor", rdpaid.SelectedValue.ToString()));

            if ((cbpaymnttype.SelectedValue.ToString() == "CQ") || (cbpaymnttype.SelectedValue.ToString() == "BT"))
            {
                arr.Add(new cArrayList("@bankaccho", cbbankho.SelectedValue.ToString()));
                arr.Add(new cArrayList("@bank_cd", cbbankcq.SelectedValue.ToString()));
                if (dtcheque.Text == String.Empty)
                {
                    dtcheque.Text = Request.Cookies["waz_dt"].Value.ToString();
                }
                arr.Add(new cArrayList("@chequedue_dt", DateTime.ParseExact(dtdue.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            }
            if (rdpaid.SelectedValue.ToString() == "G")
            {
                arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue.ToString()));
            }

            // Tot Amt : IAG 
            //Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
            //arr.Add(new cArrayList("@totamt", txamount.Text));
            arr.Add(new cArrayList("@totamt", dTotalAmount.ToString()));

            bll.vInsertMstPayment(arr, ref _paymentnumber);

            txreceiptno.Text = _paymentnumber;
            hdpaid.Value = _paymentnumber;

            if ((cbpaymnttype.SelectedValue == "CQ") || (cbpaymnttype.SelectedValue.ToString() == "BT") || (cbpaymnttype.SelectedValue.ToString() == "IT"))
            {
                FileInfo fi = new FileInfo(fup.FileName);
                fup.SaveAs(bll.sGetControlParameter("image_path") + "payment\\" + _paymentnumber + fi.Extension);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@payment_no", _paymentnumber));
                arr.Add(new cArrayList("@docfile", _paymentnumber + fi.Extension));
                if ((cbpaymnttype.SelectedValue.ToString() == "BT" || (cbpaymnttype.SelectedValue.ToString() == "CQ")))
                {
                    arr.Add(new cArrayList("@paymentAttribute", ddlPaymentAttribute.SelectedValue));
                }
                else
                {
                    arr.Add(new cArrayList("@paymentAttribute", null));
                }
                //  arr.Add(new cArrayList("@initamt", txamount.Text));
                bll.vInsertPaymentInfo(arr);
            }
            if (fup.PostedFile.FileName != String.Empty)
            {
                FileInfo fi = new FileInfo(fup.FileName);
                fup.SaveAs(bll.sGetControlParameter("image_path") + "payment\\" + _paymentnumber + fi.Extension);
                //fup.PostedFile.SaveAs(bll.sGetControlParameter("image_path") + _paymentnumber + fi.Extension);
            }

            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@payment_no", _paymentnumber));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                // arr.Add(new cArrayList("@docfile", _paymentnumber + uplpayment.FileName));
                //  arr.Add(new cArrayList("@initamt", txamount.Text));
                bll.vInsertPaymentInfo(arr);
            }
            arr.Clear();
            if (!chAdvance.Checked)
            {
                foreach (GridViewRow row in grd.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");
                        Label lbamt = (Label)row.FindControl("lbamt");
                        Label lbdisc = (Label)row.FindControl("lbdisc");
                        Label lbRound = (Label)row.FindControl("lbRound");
                        Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
                        Label lbremain = (Label)row.FindControl("lbremain");
                        CheckBox chdisc = (CheckBox)row.FindControl("chdisc");
                        Label lbvatonepct = (Label)row.FindControl("lbvatonepct");
                        if (lbamt.Text == "") { lbamt.Text = "0"; }
                        dRemain += Convert.ToDouble(lbamt.Text);
                        arr.Clear();
                        arr.Add(new cArrayList("@payment_no", _paymentnumber));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                        arr.Add(new cArrayList("@amt", lbamt.Text));
                        arr.Add(new cArrayList("@discount_amt", lbdisc.Text));
                        if (cbpaymnttype.SelectedValue == "CH")
                        {
                            arr.Add(new cArrayList("@round_amt", lbRound.Text));
                        }
                        else
                        {
                            arr.Add(new cArrayList("@round_amt", 0));
                        }
                        arr.Add(new cArrayList("@vatonepct", lbvatonepct.Text == "" ? 0 : Convert.ToDecimal(lbvatonepct.Text)));
                        arr.Add(new cArrayList("@creadtedBy", Request.Cookies["usr_id"].Value.ToString()));
                        if (chdisc.Checked)
                        {
                            arr.Add(new cArrayList("@disconepct", lbdisconepct.Text));
                        }
                        else { arr.Add(new cArrayList("@disconepct", "0")); }

                        // For 1 Pct Discount RawData
                        if (rdpaid.SelectedValue.ToString() == "C")
                        {
                            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
                            arr.Add(new cArrayList("@cusgrcd", null));
                        }
                        else if (rdpaid.SelectedValue.ToString() == "G")
                        {
                            arr.Add(new cArrayList("@cust_cd", null));
                            arr.Add(new cArrayList("@salesman_cd", null));
                            arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue.ToString()));
                        }
                        arr.Add(new cArrayList("@rdpaidfor", cbsource.SelectedValue.ToString()));


                        if ((cbpaymnttype.SelectedValue.ToString() != "CQ") && (cbpaymnttype.SelectedValue.ToString() != "BT") && (cbpaymnttype.SelectedValue.ToString() != "IT"))
                        {
                            bll.vInsertPaymentDtl1pctDisc(arr);
                        }
                        else
                        {
                            bll.vInsertPaymentBooking1pctDisc(arr);
                        }
                        //----------------------------- IA 21-Sep-2016-----------
                        arr.Clear();
                        arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                        arr.Add(new cArrayList("@isdisconepct", chdisc.Checked));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateDosalesInvoiceInfoByIsDiscOnePct(arr);
                        //-------------------------------------------------------
                    }
                }
                // --- Payment promised ------------------------------------------- (Pls unremark to activated)
                string sLasPromised = bll.vLookUp("select dbo.fn_getlastpromised('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                double dPromised = Convert.ToDouble(sLasPromised);
                if (dPromised > 0)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                    arr.Add(new cArrayList("@promised_sta_id", "C"));
                    arr.Add(new cArrayList("@old_promised_sta_id", "T"));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdatePaymentPromised(arr);
                }
                BindingGrid();
            }            
            
            // Walking on the moon _________________________
            if ((cbpaymnttype.SelectedValue.ToString() == "CQ") || (cbpaymnttype.SelectedValue.ToString() == "BT") || (cbpaymnttype.SelectedValue.ToString() == "IT"))
            {
                arr.Clear();
                if (cbpaymnttype.SelectedValue.ToString() == "CQ")
                {
                    arr.Add(new cArrayList("@trn_typ", "NEWCQ"));
                }
                else if (cbpaymnttype.SelectedValue.ToString() == "BT")
                {
                    arr.Add(new cArrayList("@trn_typ", "NEWBT"));
                }
                else if (cbpaymnttype.SelectedValue.ToString() == "IT")
                {
                    arr.Add(new cArrayList("@trn_typ", "NEWIT"));
                }
                arr.Add(new cArrayList("@refno", _paymentnumber));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchAccTransactionLog(arr);


                //______________________________________________
            }
            double dSuspense = Convert.ToDouble(lbsuspense.Text);
            if (dSuspense > 0)
            {
                arr.Clear();
                arr.Add(new cArrayList("@payment_no", _paymentnumber));
                arr.Add(new cArrayList("@amt", lbsuspense.Text));
                arr.Add(new cArrayList("@payment_dt", System.DateTime.ParseExact(dtpayment.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@cust_cd", hdcust.Value));
                arr.Add(new cArrayList("@deleted", 0));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertPaymentSuspense(arr);
                arr.Clear();
                arr.Add(new cArrayList("@trn_typ", "SUSPENSE"));
                arr.Add(new cArrayList("@refno", _paymentnumber));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchAccTransactionLog(arr);
            }
           
            cd.v_hiddencontrol(btsave);
            cd.v_disablecontrol(cbpaymnttype);
            cd.v_disablecontrol(txhovoucher);
            cd.v_disablecontrol(txrenark);
            cd.v_disablecontrol(cbbankho);
            cd.v_disablecontrol(cbbankcq);
            cd.v_disablecontrol(dthorec);
            cd.v_disablecontrol(dtcheque);
            cd.v_disablecontrol(txmanualno);
            cd.v_disablecontrol(txcust);
            cd.v_showcontrol(btprintall);
            cd.v_showcontrol(btprint);
            // Only For Inv
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Payment has been saved succesfully!','Payment No. : " + _paymentnumber + "','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally { ScriptManager.RegisterStartupScript(Page,Page.GetType(),Guid.NewGuid().ToString(),"HideProgress();",true); }
        
    }

    void vGridFooter()
    {
        try
        {
            double dAmt = 0; double dDisc = 0; double dRound = 0; double dDiscOnePct = 0; double dOutstanding = 0;
            foreach (GridViewRow row in grd.Rows)
            {
                double dOut = 0;
                if ((row.RowType == DataControlRowType.DataRow) && (row.RowState == DataControlRowState.Alternate || row.RowState == DataControlRowState.Normal))
                {
                    Label lbamt = (Label)row.FindControl("lbamt");
                    Label lbdisc = (Label)row.FindControl("lbdisc");
                    Label lbRound = (Label)row.FindControl("lbRound");
                    Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
                    Label lboutstanding = (Label)row.FindControl("lboutstanding");
                    dAmt += Convert.ToDouble(lbamt.Text);
                    dDisc += Convert.ToDouble(lbdisc.Text);
                    dRound += Convert.ToDouble(lbRound.Text);
                    dDiscOnePct += Convert.ToDouble(lbdisconepct.Text);
                    if (dDiscOnePct > 0)
                    {
                        dOutstanding += Convert.ToDouble(lboutstanding.Text);
                        //dOut = Convert.ToDouble(lboutstanding.Text);
                        //if (dOut < 0)
                        //{
                        //    lboutstanding.Text = "0";
                        //}
                    }

                }
                else if ((row.RowType == DataControlRowType.DataRow) && (row.RowState == DataControlRowState.Edit))
                {
                    TextBox txamt = (TextBox)row.FindControl("txamt");
                    TextBox txdisc = (TextBox)row.FindControl("txdisc");
                    TextBox txtRound = (TextBox)row.FindControl("txtRound");
                    Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
                    Label lboutstanding = (Label)row.FindControl("lboutstanding");
                    dAmt += Convert.ToDouble(txamt.Text);
                    dDisc += Convert.ToDouble(txdisc.Text);
                    dRound += Convert.ToDouble(txtRound.Text);
                    dDiscOnePct += Convert.ToDouble(lbdisconepct.Text);
                    if (dDiscOnePct > 0)
                    {
                        dOutstanding += Convert.ToDouble(lboutstanding.Text);
                        //dOut = Convert.ToDouble(lboutstanding.Text);
                        //if (dOut < 0)
                        //{
                        //    lboutstanding.Text = "0";
                        //}
                    }
                }

            }

            Label lbtotdisc = (Label)grd.FooterRow.FindControl("lbtotdisc");
            Label lbtotRound = (Label)grd.FooterRow.FindControl("lbtotRound");
            Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");

            lbtotRound.Text = dRound.ToString();
            lbtotamt.Text = dAmt.ToString();
            lbtotdisc.Text = dDisc.ToString();

            //if (txamount.Text.Trim() == "") { txamount.Text = "0"; }
            //double dSuspense = (Convert.ToDouble(txamount.Text) - dAmt);
            //dSuspense = Math.Round(dSuspense, 2);
            //if (dOutstanding < 0)
            //{
            //    dSuspense += Math.Abs( dOutstanding);
            //}
            //lbsuspense.Text = dSuspense.ToString();

            //if (Convert.ToDouble(lbsuspense.Text) < 0) 
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt8", "sweetAlert('Total Paid can not bigger than money for paid','Please edit payment','error');", true);
            //}

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            double dDisc = 0; double dRound = 0; double dPay = 0;
            TextBox txdisc = (TextBox)grd.Rows[e.RowIndex].FindControl("txdisc");
            TextBox txtRound = (TextBox)grd.Rows[e.RowIndex].FindControl("txtRound");
            TextBox txamt = (TextBox)grd.Rows[e.RowIndex].FindControl("txamt");
            Label lbremain = (Label)grd.Rows[e.RowIndex].FindControl("lbremain");
            Label lbdisconepct = (Label)grd.Rows[e.RowIndex].FindControl("lbdisconepct");
            Label lbinvoiceno = (Label)grd.Rows[e.RowIndex].FindControl("lbinvoiceno");
            CheckBox chdisc = (CheckBox)grd.Rows[e.RowIndex].FindControl("chdisc");
            if (!double.TryParse(txdisc.Text, out dDisc))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('Discount must numeric','Please try with numeric','warning');", true);
                return;
            }

            if (cbpaymnttype.SelectedValue == "CH")
            {
                if (dDisc > 1.00)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('Discount can not bigger than EGP 1.00','Please try less value','warning');", true);
                    return;
                }
            }
            if (dDisc < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt3", "sweetAlert('Discount can not negative','Please try with postive value','warning');", true);
                return;
            }
            if (cbpaymnttype.SelectedValue == "CH")
            {
                if (!double.TryParse(txtRound.Text, out dRound))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('Round must numeric','Please try with numeric','warning');", true);
                    return;
                }
                if (dRound > 1.00)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('Round can not bigger than EGP 1.00','Please try less value','warning');", true);
                    return;
                }
                if (dRound < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt3", "sweetAlert('Round can not negative','Please try with postive value','warning');", true);
                    return;
                }
            }
            if (!double.TryParse(txamt.Text, out dPay))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt3", "sweetAlert('Amout paid must numeric','Please try with numeric value','warning');", true);
                return;
            }

            if (dPay < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt3", "sweetAlert('Payment can not negative','Please try with postive value','warning');", true);
                return;
            }
            if (chdisc.Checked)
            {
                if (dPay > Math.Round((Convert.ToDouble(lbremain.Text) - Convert.ToDouble(lbdisconepct.Text)), 2))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt4", "sweetAlert('Payment can not bigger than Remain','Payment + Disc 1% must same or less value','warning');", true);
                    return;
                }
            }

            else
            {
                if (dPay > (Convert.ToDouble(lbremain.Text)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt4", "sweetAlert('Payment can not bigger than Remain','Payment must same or less value','warning');", true);
                    return;
                }
            }
            double dDis = 0; double dRou = 0; double dAm = 0; double d1pct = 0; double dBal = 0;
            foreach (GridViewRow row in grd.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chdisc");
                if ((row.RowType == DataControlRowType.DataRow) && ((row.RowState == DataControlRowState.Normal) || (row.RowState == DataControlRowState.Alternate)))
                {
                    Label lbdisc = (Label)row.FindControl("lbdisc");
                    Label lbRound = (Label)row.FindControl("lbRound");
                    Label lbamt = (Label)row.FindControl("lbamt");
                    Label lbdisc1pct = (Label)row.FindControl("lbdisconepct");
                    Label lbbalance = (Label)row.FindControl("lbremain");


                    dDis += Convert.ToDouble(lbdisc.Text);
                    dRou += Convert.ToDouble(lbRound.Text);
                    dAm += Convert.ToDouble(lbamt.Text);
                    dBal += Convert.ToDouble(lbbalance.Text); //d1pct += Convert.ToDouble(lbdisc1pct.Text);
                    if (chk.Checked)
                    {

                        d1pct += Convert.ToDouble(lbdisc1pct.Text);
                    }

                }
                else if ((row.RowType == DataControlRowType.DataRow) && (row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    Label lbdisc1pct = (Label)row.FindControl("lbdisconepct");
                    Label lbbalance = (Label)row.FindControl("lbremain");
                    Label lbdisc = (Label)row.FindControl("lbdisc");
                    Label lbRound = (Label)row.FindControl("lbRound");
                    TextBox txdc = (TextBox)row.FindControl("txdisc");
                    TextBox txtRou = (TextBox)row.FindControl("txtRound");
                    TextBox txam = (TextBox)row.FindControl("txamt");
                    Label lbpaid = (Label)row.FindControl("lbpaid");
                    dDis += Convert.ToDouble(txdisc.Text);
                    dRou += Convert.ToDouble(txtRou.Text);
                    dAm += Convert.ToDouble(txam.Text);
                    dBal += Convert.ToDouble(lbbalance.Text);// d1pct += Convert.ToDouble(lbdisc1pct.Text);
                    if (chk.Checked)
                    {
                        if (Convert.ToDouble(lbdisc1pct.Text) > 0)
                        {
                            double dMoney = Math.Round(Convert.ToDouble(bll.vLookUp("select dbo.fn_checkswitchoff1pct('" + lbinvoiceno.Text + "'," + lbdisconepct.Text + ",'" + Request.Cookies["sp"].Value.ToString() + "')")), 2);
                            double dPaidBy = Math.Round(Convert.ToDouble(txam.Text), 2);
                            if ((dMoney != 0) && (dPaidBy != dMoney) && ((dMoney - dPaidBy) > 1))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('This Invoice should off 1%, because not paid completed!','" + lbinvoiceno.Text + "','warning');", true);
                                return;
                            }
                            else
                            {
                                //  lbdisc.Text = (dMoney - dPaidBy).ToString();
                                d1pct += Convert.ToDouble(lbdisc1pct.Text);
                                //List<cArrayList> arr = new List<cArrayList>();
                                //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                                //arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                                //arr.Add(new cArrayList("@amt", txamt.Text));
                                //arr.Add(new cArrayList("@disc_amt", txdisc.Text));
                            }
                        }

                    }
                }
            }

            //double dAmtPaid = (Math.Round(Convert.ToDouble(txamount.Text), 2) - Math.Round(dAm, 2));
            //if (dAmtPaid < 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt4", "sweetAlert('Total Payment can not less than Total Will Be Paid!','Edit your payment invoice','warning');", true);
            //    return;
            //}
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
            arr.Add(new cArrayList("@amt", txamt.Text));
            arr.Add(new cArrayList("@disc_amt", txdisc.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            if (cbpaymnttype.SelectedValue == "CH")
            {
                arr.Add(new cArrayList("@round_amt", txtRound.Text));
            }
            else
            {
                arr.Add(new cArrayList("@round_amt", 0));
            }
            if (cbpaymnttype.SelectedValue == "CH")
            {
                arr.Add(new cArrayList("@round_amt", txtRound.Text));
            }
            else
            {
                arr.Add(new cArrayList("@round_amt", 0));
            }
            try
            {
                bll.vUpdateWrkPaymentInvoice(arr);
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : update Wrk Payment Invoice");
            }
            grd.EditIndex = -1;
            BindingGrid();
            vGridFooter();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbpaymnttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if ((cbpaymnttype.SelectedValue.ToString() == "CQ") || (cbpaymnttype.SelectedValue.ToString() == "BT"))
            {
                bll.vBindingFieldValueToComboWithChoosen(ref cbbankcq, "bank_cd");
                arr.Add(new cArrayList("@salespointcd", 0));
                //cbbankcq.CssClass = "form-control drop-down";
                //cbbankho.CssClass = "form-control drop-down ro";
                //txdocno.CssClass = "form-control";
                //dtcheque.CssClass = "form-control ";
                //dtdue.CssClass = "form-control ";
                //dthorec.CssClass = "form-control  ro";
                //txhovoucher.CssClass = "form-control  ro";
                cd.v_enablecontrol(cbbankcq);
                cd.v_disablecontrol(cbbankho);
                cd.v_enablecontrol(txdocno);
                cd.v_enablecontrol(dtcheque);
                cd.v_enablecontrol(dtdue);
                cd.v_disablecontrol(dthorec);
                cd.v_disablecontrol(txhovoucher);
                dtpayment.Text = Request.Cookies["waz_dt"].Value.ToString();
                dtcheque.Text = Request.Cookies["waz_dt"].Value.ToString();
                dtdue.Text = Request.Cookies["waz_dt"].Value.ToString();
            }
            else if (cbpaymnttype.SelectedValue.ToString() == "IT")
            {
                //cbbankcq.CssClass = "form-control ro drop-down";
                //cbbankho.CssClass = "form-control ro drop-down";
                //txdocno.CssClass = "form-control ";
                //dtcheque.CssClass = "form-control  ro";
                //dtdue.CssClass = "form-control  ro";
                //dthorec.CssClass = "form-control ";
                cd.v_disablecontrol(cbbankho);
                cd.v_disablecontrol(cbbankho);
                cd.v_enablecontrol(txdocno);
                cd.v_disablecontrol(dtcheque);
                cd.v_disablecontrol(dtdue);
                cd.v_enablecontrol(dthorec);
                //txhovoucher.CssClass = "form-control ";
                cd.v_enablecontrol(txhovoucher);
                dthorec.Text = Request.Cookies["waz_dt"].Value;
                dtcheque.Text = String.Empty;
                dtdue.Text = String.Empty;
            }
            else if (cbpaymnttype.SelectedValue == "CH")
            {
                txdocno.Text = String.Empty;
                cbbankho.Items.Clear(); cbbankcq.Items.Clear();
                //cbbankcq.CssClass = "form-control ro drop-down";
                //cbbankho.CssClass = "form-control ro drop-down";
                //txdocno.CssClass = "form-control  ro";
                //dtdue.CssClass = "form-control  ro";
                //dtcheque.CssClass = "form-control  ro";
                //dthorec.CssClass = "form-control  ro";
                //txhovoucher.CssClass = "form-control  ro";
                cd.v_disablecontrol(cbbankcq);
                cd.v_disablecontrol(cbbankho);
                cd.v_disablecontrol(txdocno);
                cd.v_disablecontrol(dtdue);
                cd.v_disablecontrol(dtcheque);
                cd.v_disablecontrol(dthorec);
                cd.v_disablecontrol(txhovoucher);
                //txmanualno.CssClass = cd.csstext;
                dtpayment.Text = Request.Cookies["waz_dt"].Value.ToString();
                //cbbankcq.CssClass = cd.csstextro;
                cd.v_disablecontrol(cbbankho);
                dtcheque.Text = String.Empty;
                dtdue.Text = String.Empty;
            }
            //else
            //{
            //    //btsave.CssClass = "divhid";
            //    //btprint.CssClass = "divhid";
            //}

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally { ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true); }
    }
    protected void bttabno_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupwindow('fm_lookuppaymenttab.aspx?pt=" + cbpaymnttype.SelectedValue.ToString() + "');", true);
    }
    protected void btreceiptno_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupwindow('lookpayment.aspx?pt=" + cbpaymnttype.SelectedValue.ToString() + "');", true);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

        try
        {
            string sSta = bll.vLookUp("select payment_sta_id from tmst_payment where payment_no='" + txreceiptno.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (((cbpaymnttype.SelectedValue.ToString() == "CQ") || (cbpaymnttype.SelectedValue.ToString() == "BT") || (cbpaymnttype.SelectedValue.ToString() == "IT")) && (sSta == "N"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=pycheque&py=" + txreceiptno.Text + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=py&noy=" + txreceiptno.Text + "');", true);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@payment_no", hdpaid.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            //  bll.vBindingGridToSp(ref grdpaid, "sp_tpayment_dtl_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@payment_no", hdpaid.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vGetPaymentReceipt(arr, ref rs);
            while (rs.Read())
            {
                txreceiptno.Text = hdpaid.Value.ToString();
                //txreceiptno.Text = "XXXXX";
                hdcust.Value = rs["cust_cd"].ToString();
                txcust.Text = rs["cust_cd"].ToString() + ":" + rs["cust_nm"].ToString();
                cbpaymnttype.SelectedValue = rs["payment_typ"].ToString();
                txtabno.Text = rs["tab_no"].ToString();
                txmanualno.Text = rs["manual_no"].ToString();
                //  txamount.Text = rs["totamt"].ToString();
                txrenark.Text = rs["remark"].ToString();
                cbsource.SelectedValue = rs["source_order"].ToString();
                dtpayment.Text = Convert.ToDateTime(rs["payment_dt"]).ToString("d/M/yyyy");
                rdpaid.SelectedValue = rs["rdpaidfor"].ToString();
                if ((cbpaymnttype.SelectedValue.ToString() == "CQ") || (cbpaymnttype.SelectedValue.ToString() == "BT"))
                {

                    dtdue.Text = (rs["chequedue_dt"] == DBNull.Value || rs["chequedue_dt"] == null) ? "" : rs["chequedue_dt"].ToString();
                    txdocno.Text = rs["btcheq_no"].ToString();

                }
                else if (cbpaymnttype.SelectedValue.ToString() == "IT")
                {
                    try
                    {
                        dthorec.Text = Convert.ToDateTime(rs["hored_dt"]).ToString("d/M/yyyy");
                    }
                    catch (Exception ex)
                    {
                        bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Select IT Payment, payment receipt2");
                        dthorec.Text = "";
                    }
                    txhovoucher.Text = rs["voucherho_no"].ToString();
                }
                //cbpaymnttype.CssClass = "form-control  ro";
                //txcust.CssClass = "form-control ro";
                //cbsource.CssClass = "form-control ro";
                //dtpayment.CssClass = "form-control ro";
                //txmanualno.CssClass = "form-control ro";
                //cbgroup.CssClass = "form-control ro";
                //cbsalesman.CssClass = "form-control ro";
                //rdpaid.CssClass = "form-control ro";
                //txrenark.CssClass = "form-control  ro";
                //btsave.CssClass = "divhid";
                //btsearchcust.CssClass = "divhid";
                //bttabno.CssClass = "divhid";
                //btreceiptno.CssClass = "divhid";
                //btprint.CssClass = "btn btn-primary";
                //btprintall.CssClass = "btn btn-info";
                cd.v_disablecontrol(cbpaymnttype);
                cd.v_disablecontrol(txcust);
                cd.v_disablecontrol(cbsource);
                cd.v_disablecontrol(dtpayment);
                cd.v_disablecontrol(txmanualno);
                cd.v_disablecontrol(cbgroup);
                cd.v_disablecontrol(cbsalesman);
                cd.v_hiddencontrol(btsearchcust);
                cd.v_hiddencontrol(bttabno);
                cd.v_hiddencontrol(btreceiptno);
                cd.v_showcontrol(btprint);
                cd.v_showcontrol(btprintall);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            }
            rs.Close();
            // btinv_Click(sender, e);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btinv_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@payment_no", hdpaid.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tpayment_dtl_get", arr);
            //Label lbtotremain = (Label) grdinvpaid.FooterRow.FindControl("lbtotremain");
            //lbtotamtpaid.Text = "Sehe";
            arr.Clear();
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getbycust", arr);
            if (hdcust.Value.ToString() != "")
            {
                string sSalesmanCode = bll.vLookUp("select salesman_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //txcust.CssClass = "form-control ro";
                cd.v_disablecontrol(txcust);
                cbsalesman.SelectedValue = sSalesmanCode;
                if (grd.Rows.Count > 0)
                {
                    Label lbtotremain = (Label)grd.FooterRow.FindControl("lbtotremain");
                    lbtotremain.Text = bll.vLookUp("select sum(balance) from tdosales_invoice where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' and inv_sta_id in ('R','P')");
                    lbtotremain.ForeColor = System.Drawing.Color.White;
                    lbtotremain.Font.Bold = true;
                }
            }
            txreceiptno.Text = hdpaid.Value.ToString();
            if ((Request.QueryString["p"]) == "")
            {
                //txamount.CssClass = "form-control ro";
                //txamount.Enabled = false;
            }
            else
            {
                //txamount.CssClass = "divnormal form-control";
                //txamount.Enabled = true;
            }
            chonepct.Checked = false;
            chonepct.Enabled = false;
            chonepct_CheckedChanged(sender, e);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        txsearchinv_AutoCompleteExtender.ContextKey = "G" + cbgroup.SelectedValue.ToString();
        hdfinalcust.Value = "G" + cbgroup.SelectedValue.ToString();
        //BindGridInvoice("G" + cbgroup.SelectedValue.ToString());
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vInsertWrkPaymentInvoiceByCusgrcd(arr);
        //BindingGrid();
        //try
        //{
        //    string sSalesman = bll.vLookUp("select distinct salesman_cd from tdosales_invoice where cust_cd in (select cust_cd from tmst_customer where cusgrcd='" + cbgroup.SelectedValue.ToString() + "')");
        //    cbsalesman.SelectedValue = sSalesman;
        //    string sPromised = bll.vLookUp("select dbo.fn_checkexistpaymentpromised(default,'" + cbgroup.SelectedValue.ToString() + "')");
        //    if (sPromised == "ok")
        //    { lbpromised.Text = "No Promised"; }
        //    else
        //    {
        //        lbpromised.Text = sPromised;
        //        lbpromised.ToolTip = "Amount:" + bll.vLookUp("select top 1 amt from tpayment_promised where cusgrcd='" + cbgroup.SelectedValue.ToString() + "' and promised_sta_id='N' and rdfor='G'");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Selected Group in Payment Receipt2 (Ignored)");
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is multiple salesman OR No salesman found in this group','" + cbgroup.SelectedValue.ToString() + "','warning');", true);
        //}
    }
    protected void cbsource_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbsource.SelectedValue.ToString() == "MAN")
            {
                cd.v_enablecontrol(dtpayment);
                cd.v_enablecontrol(txmanualno);
                cd.v_disablecontrol(txdocno);
                cd.v_disablecontrol(txtabno);
                cd.v_enablecontrol(txrenark);
                cd.v_disablecontrol(dtcheque);
                cd.v_disablecontrol(dtdue);
                cd.v_disablecontrol(txreceiptno);
                cd.v_showcontrol(btsave);
                cd.v_disablecontrol(rdpaid);
                cd.v_enablecontrol(rdpaid);
                cd.v_enablecontrol(btreceiptno);
                txreceiptnoPnl.CssClass = "input-group";
                txdocno.Text = String.Empty;
                cd.v_enablecontrol(cbpaymnttype);
                //cbbankcq.CssClass = cd.csstext;
                cd.v_enablecontrol(cbbankcq);
                bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbpaymnttype, "payment_typ","payment_typ");

            }
            else
            {
                cd.v_disablecontrol(dtpayment);
                cd.v_disablecontrol(txmanualno);
                cd.v_disablecontrol(txdocno);
                cd.v_disablecontrol(txtabno);
                cd.v_disablecontrol(txrenark);
                cd.v_disablecontrol(dtcheque);
                cd.v_disablecontrol(txdocno);
                cd.v_disablecontrol(dtdue);
                cd.v_disablecontrol(txreceiptno);
                cd.v_hiddencontrol(btsave);
                cd.v_disablecontrol(rdpaid);
                cd.v_enablecontrol(bttabno);
                cd.v_disablecontrol(cbpaymnttype);
                cd.v_disablecontrol(cbbankcq);
                // cbpaymnttype.Text = "";
            }
            cd.v_disablecontrol(cbsource);
            cbpaymnttype_SelectedIndexChanged(sender, e);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_paymentreceipt2.aspx");
    }
    protected void btupload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fup.HasFile)
            {
                string fileName;
                fileName = fup.FileName;
                FileInfo fi = new FileInfo(fup.FileName);

                //fup.SaveAs(Server.MapPath("~/images/" + fileName));
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void fup_DataBinding(object sender, EventArgs e)
    {
        btupload_Click(sender, e);
    }
    protected void btclearpayment_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vClearWrkPaymentInvoice(arr);
            BindingGrid();
            vGridFooter();
            //foreach (GridViewRow row in grd.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DatgaRow)
            //    {
            //        CheckBox chk = (CheckBox)row.FindControl("chdisc");
            //        chk.Checked = false;
            //    }
            //}

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprintall_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_printall.aspx?printtype=payment");
    }

    void SwitchOff()
    {
        try
        {
            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkdisc = (CheckBox)row.FindControl("chdisc");
                    chkdisc.Checked = false;
                    //chdisc_CheckedChanged(null,null);
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        HttpCookie uok, cok;
        uok = HttpContext.Current.Request.Cookies["usr_id"];
        cok = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@invcheck", contextKey));
        arr.Add(new cArrayList("@inv_no", prefixText));
        arr.Add(new cArrayList("@usr_id", uok.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchInvoiceByType(arr, ref rs);
        //DataTable dtTemp = new DataTable();
        //dtTemp = cdl.GetValueFromSP("sp_twrk_paymentinvoice_get", arr);

        while (rs.Read())
        {
            string balance = bll.vLookUp("select dbo.fn_gettempbalance('" + rs["inv_no"].ToString() + "','"+ cok.Value.ToString() + "')");

            if (balance != "")
            {
                if (Convert.ToDecimal(balance) > 0)
                {
                    sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["manual_no"].ToString() + " | " + rs["inv_no"].ToString() + " | Cust-" + rs["cust_cd"].ToString() + " | " + " Inv_dt-" + Convert.ToDateTime(rs["inv_dt"]).ToString("dd-MM-yyyy") + " | Balance - " + balance + " ",
                        rs["inv_no"].ToString());
                    lItem.Add(sItem);
                }
            }
        }
        rs.Close();

        return (lItem.ToArray());
    }
    protected void btsearchinv_Click(object sender, EventArgs e)
    {
        try
        {
            double totalBalance = 0;
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetDOSalesInvoiceByStatus(arr, ref rs);
            lbalert.ForeColor = System.Drawing.Color.Red;
            lbalert.Font.Bold = true; ;
            chonepct.Enabled = false;
            while (rs.Read())
            {
                lbCustCD.Text = rs["cust_cd"].ToString();
                lbmanualno.Text = rs["manual_no"].ToString();
                lbsysno.Text = rs["inv_no"].ToString();
                lbamt.Text = rs["totamt"].ToString();
                //if (rs["cust_type"].ToString() == "SP")
                //{
                //    lbbalance.Text = rs["balance"].ToString();
                //    txdisc.Text = (double.Parse(rs["balance"].ToString()) - double.Parse(rs["amt_preinv"].ToString())).ToString();
                //    txdisc.Enabled = false;
                //}
                //else
                //{
                    lbbalance.Text = rs["balance"].ToString();
                //}

                lbinvoicedate.Text = Convert.ToDateTime(rs["inv_dt"]).ToString("d/M/yyyy");
                //lbreceived.Text = Convert.ToDateTime(bll.vLookUp("select isnull(received_dt,(select convert(date,parm_valu,103) from tcontrol_parameter where parm_nm = 'wazaran_dt')) as received_dt from tdosalesinvoice_received where inv_no='" + rs["inv_no"].ToString() + "'")).ToString("d/M/yyyy"); ;
                lbreceived.Text = rs["received_dt"].ToString();
                lbhaspaid.Text = bll.vLookUp("select isnull(sum(amt),0) from tpayment_dtl d join tmst_payment m on d.payment_no=m.payment_no and d.salespointcd=m.salespointcd where d.inv_no='" + hdinv.Value.ToString() + "' and m.payment_sta_id <> 'L' and d.salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtempbalance.Text = bll.vLookUp("select dbo.fn_gettempbalance('" + hdinv.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                string sDisc1pct = bll.vLookUp("select dbo.fn_checkdiscountpayment1pct('" + hdinv.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                if (sDisc1pct != "ok") // this hit by disc 
                {
                    lbalert.Font.Bold = true;
                    lbalert.ForeColor = System.Drawing.Color.Green;
                    lbalert.Text = "NOTE: " + hdinv.Value.ToString() + " is Eligible for discount 1 %, make it on or off";
                    chonepct.Enabled = true;
                    lbdisc1pct.Text = "0";
                }
                else
                {
                    lbalert.Text = "NOTE: " + hdinv.Value.ToString() + " is not eligible for discount 1 %";
                    lbalert.ForeColor = System.Drawing.Color.Red;
                    lbalert.Font.Bold = true; ;
                    chonepct.Enabled = false;

                }
                lbdisc1pct.Text = "0";
                lbvatdisc1pct.Text = "0";
            }
            rs.Close();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void chonepct_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            double dBalance = 0;
            if (lbbalance.Text != "")
            {
                dBalance = Convert.ToDouble(lbbalance.Text);
            }

            double dOnePct = 0; double dVatOnePct = 0;
            if (chonepct.Checked)
            {
                // Modify   : IAG , 30-Jan-18 
                //Purposed : Change 1 pct from before VAT
                // dOnePct = 0.01 * dBalance; 
                dOnePct = Convert.ToDouble(bll.vLookUp("select dbo.fn_getonepctdiscount('" + hdinv.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')"));
                dVatOnePct = Math.Round(Convert.ToDouble(bll.vLookUp("select dbo.fn_getcontrolparameter('vat')")) * dOnePct, 5);
                // As per suggestion Abdulaahi
                //dBalance -= (dOnePct + dVatOnePct);
                dBalance -= (dOnePct);
                lbbalance.Text = dBalance.ToString();
                lbdisc1pct.Text = dOnePct.ToString();
                lbvatdisc1pct.Text = dVatOnePct.ToString();

            }
            else
            {
                btsearchinv_Click(sender, e);
                //lbvatdisc1pct.Text = "0";
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            double dDiscpct = 0; double dRemain = 0; double dDiscHalala = 0; double dRoundHalala = 0; double dAmountPaid = 0; double dPaidAmount = 0;
            string cust_type = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if ((cbpaymnttype.SelectedValue.ToString() == "BT" || (cbpaymnttype.SelectedValue.ToString() == "CQ")))
            {
                if (txdocno.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cheque/Transfer No. can not empty !','Bank Transfer/Cheque','warning');", true);
                    return;
                }
                else if (ddlPaymentAttribute.SelectedValue == "OT" && cbsource.SelectedValue == "MAN")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select payment status  !','Payment Status','warning');", true);
                    return;
                }
            }

            if (!double.TryParse(txamount.Text, out dAmountPaid))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be Paid must numeric!','Amount to be paid','warning');", true);
                return;
            }

            if (dAmountPaid == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be paid can not zero!','Greater than zero','warning');", true);
                return;
            }

            if (hdinv.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice is not selected!','Please add invoice to be paid','warning');", true);
                return;
            }
            if (!double.TryParse(txdisc.Text, out dDiscHalala))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in discount fraction!','put zero is no discount','warning');", true);
                return;
            }
            else
            {
                if (cbpaymnttype.SelectedValue == "CH")
                {
                    if (cust_type != "SP")
                    {
                        if (dDiscHalala > 1.00)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Discount fraction must below EGP 1.00!','Correctly discount below EGP 1','warning');", true);
                            return;
                        }
                    }

                }
            }
            if (cbpaymnttype.SelectedValue == "CH")
            {
                if (!double.TryParse(txtRound.Text, out dRoundHalala))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in round fraction!','put zero is no round','warning');", true);
                    return;
                }
                else
                {
                    if (dRoundHalala > 1.00)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Round fraction must below EGP 1.00!','Correctly discount below EGP 1','warning');", true);
                        return;
                    }
                }
            }

            if (!double.TryParse(txamtpaid.Text, out dAmountPaid))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in amount paid!','Amount to be paid','warning');", true);
                return;
            }
            dDiscpct = Convert.ToDouble(lbdisc1pct.Text);
            dRemain = Convert.ToDouble(lbtempbalance.Text);


            if (chonepct.Checked == true && chonepct.Enabled == true)
            {
                if (((Convert.ToDouble(lbbalance.Text) + Convert.ToDouble(txtRound.Text)) > (dAmountPaid + Convert.ToDouble(txdisc.Text))))//((Convert.ToDouble(lbbalance.Text) != (dAmountPaid + dDiscHalala)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Must full pay on 1 % discount!','Must full pay','warning');", true);
                    return;
                }
                if (Convert.ToDouble(txamount.Text) < (dAmountPaid))//(Convert.ToDouble(txamount.Text) < (dAmountPaid + dDiscHalala))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount paid can not exceeded than balance!','Amount paid exceeded','warning');", true);
                    return;
                }
                // here we validate value for one %
                if (System.Math.Round((Convert.ToDouble(lbbalance.Text) - Convert.ToDouble(txdisc.Text)), 2) < dAmountPaid)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount paid can not exceeded than balance!','Amount paid exceeded','warning');", true);
                    return;
                }
            }
            else if (System.Math.Round((dRemain - (dAmountPaid + dDiscpct - dDiscHalala - dRoundHalala)), 2) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount paid can not exceeded than balance!','Amount paid exceeded','warning');", true);
                return;
            }
            else if (System.Math.Round(Convert.ToDouble(txamount.Text) - (dAmountPaid + dDiscpct - dRoundHalala), 2) < 0) //if (Convert.ToDouble(txamount.Text) - (dAmountPaid + dDiscpct + dDiscHalala) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount paid can not exceeded than balance!','Amount paid exceeded','warning');", true);
                return;
            }
            // Avoid duplicate round amount 

            string roundAmt = string.Empty;

            if (cbpaymnttype.SelectedValue == "CH")
            {
                roundAmt = bll.vLookUp("select sum(round_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            }

            if (roundAmt != "")
            {
                if (Convert.ToDecimal(roundAmt) > 0 && dRoundHalala > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not make two time round amount!','Round amount','warning');", true);
                    return;
                }
            }

            if (grd.Rows.Count > 0)
            {
                Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
                Label lbtotRound = (Label)grd.FooterRow.FindControl("lbtotRound");
                Label lbtotdisc = (Label)grd.FooterRow.FindControl("lbtotdisc");
                Label lbtotOutStand = (Label)grd.FooterRow.FindControl("lbtotOutStand");



                lbtotamt.Text = bll.vLookUp("select sum(amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotRound.Text = bll.vLookUp("select sum(round_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotdisc.Text = bll.vLookUp("select sum(disc_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotOutStand.Text = bll.vLookUp("select sum(outstanding) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                // validate before save

                double totAmtAssign = Convert.ToDouble(txamount.Text);
                double useAmount = Convert.ToDouble(lbtotamt.Text);
                double roundAmount = Convert.ToDouble(lbtotRound.Text);
                double totDis = Convert.ToDouble(lbtotdisc.Text);

                double remaining = totAmtAssign - dAmountPaid - useAmount - roundAmount + totDis;
                if (System.Math.Round(remaining, 2) < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount paid can not exceeded than balance!','Amount paid exceeded','warning');", true);
                    return;
                }
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@manual_no", lbmanualno.Text));
            arr.Add(new cArrayList("@inv_dt", System.DateTime.ParseExact(lbinvoicedate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@cust_nm", txcust.Text));
            arr.Add(new cArrayList("@totamt", lbamt.Text));
            arr.Add(new cArrayList("@paid", lbhaspaid.Text));
            arr.Add(new cArrayList("@inv_sta_nm", "RECEIVED"));

            if (chonepct.Checked)
            {
                dRemain = Convert.ToDouble(lbtempbalance.Text);
                //dDiscpct = 0.01 * Convert.ToDouble(lbamt.Text);
                dRemain += dDiscpct;
            }
            else { dDiscpct = 0; dRemain = Convert.ToDouble(lbtempbalance.Text); }
            arr.Add(new cArrayList("@remain", dRemain));
            arr.Add(new cArrayList("@disconepct", dDiscpct));
            arr.Add(new cArrayList("@disc_amt", dDiscHalala));
            if (cbpaymnttype.SelectedValue == "CH")
            {
                arr.Add(new cArrayList("@round_amt", dRoundHalala));
            }
            else
            {
                arr.Add(new cArrayList("@round_amt", 0));
            }
            arr.Add(new cArrayList("@amt", dAmountPaid));
            arr.Add(new cArrayList("@onoff", chonepct.Checked));
            arr.Add(new cArrayList("@received_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@tbalance", lbtempbalance.Text));
            if (hdinv.Value.ToString().Substring(0, 2) == "IV")
            {
                arr.Add(new cArrayList("@paymentfor", "IV"));
            }
            else
            { arr.Add(new cArrayList("@paymentfor", "BA")); }
            arr.Add(new cArrayList("@vatonepct", lbvatdisc1pct.Text));
            //arr.Add(new cArrayList("@ptype", lbvatdisc1pct.Text));

            // here we validate if data is eligible for one percent , but user not seleted.

            string sDisc1pct = bll.vLookUp("select dbo.fn_checkdiscountpayment1pct('" + hdinv.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sDisc1pct != "ok" && chonepct.Enabled && chonepct.Checked == false) // this hit by disc 
            {
                //chonepct.Enabled = true;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select Switch Disc option / otherwise you can continue !','Switch Disc not selected','warning');", true);
            }


            bll.vInsertWrkPaymentInvoice2(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_paymentinvoice_get", arr);
            lbbalance.Text = String.Empty;
            lbalert.Text = String.Empty;
            lbamt.Text = String.Empty;
            lbdisc1pct.Text = String.Empty;
            lbhaspaid.Text = String.Empty;
            lbpromised.Text = String.Empty;
            lbsysno.Text = String.Empty;
            lbmanualno.Text = String.Empty;
            lbCustCD.Text = String.Empty;
            txsearchinv.Text = String.Empty;
            hdinv.Value = String.Empty;
            lbinvoicedate.Text = String.Empty;
            lbreceived.Text = String.Empty;
            lbtempbalance.Text = String.Empty;
            txamtpaid.Text = String.Empty;
            lbvatdisc1pct.Text = String.Empty;
            //cbpaymnttype.CssClass = cd.csstextro;
            cd.v_disablecontrol(cbpaymnttype);
            txdisc.Text = "0.00";
            txtRound.Text = "0.00";
            // reset one %
            chonepct.Checked = false;
            chonepct.Enabled = false;
            chonepct_CheckedChanged(sender, e);

            if (grd.Rows.Count > 0)
            {
                Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
                Label lbtotRound = (Label)grd.FooterRow.FindControl("lbtotRound");
                Label lbtotdisc = (Label)grd.FooterRow.FindControl("lbtotdisc");
                Label lbtotOutStand = (Label)grd.FooterRow.FindControl("lbtotOutStand");

                lbtotamt.Text = bll.vLookUp("select sum(amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotRound.Text = bll.vLookUp("select sum(round_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotdisc.Text = bll.vLookUp("select sum(disc_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotOutStand.Text = bll.vLookUp("select sum(outstanding) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

                rdpaid.CssClass = "control-label ro";
                lbtotalpayment.Text = lbtotamt.Text;
                txamount.CssClass = cd.csstextro;

                DataTable dtpaymentinvoice = new DataTable();
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                dtpaymentinvoice = cdl.GetValueFromSP("sp_twrk_paymentinvoice_get", arr);

                decimal sumBalance = Convert.ToDecimal(dtpaymentinvoice.Compute("Sum(tbalance)", string.Empty));
                double sumOutstanding = Convert.ToDouble(dtpaymentinvoice.Compute("Sum(outstanding)", string.Empty));
                double sumDisc_amt = Convert.ToDouble(dtpaymentinvoice.Compute("Sum(disc_amt)", string.Empty));
                double sumRound_amt = Convert.ToDouble(dtpaymentinvoice.Compute("Sum(round_amt)", string.Empty));

                //double dSuspense = Convert.ToDouble(txamount.Text) - Convert.ToDouble(lbtotamt.Text) - Convert.ToDouble(lbtotRound.Text) + Convert.ToDouble(lbtotdisc.Text);
                //double dSuspense = Convert.ToDouble(txamount.Text) - Convert.ToDouble(lbtotamt.Text) - Convert.ToDouble(lbtotRound.Text) ;
                double dSuspense = Convert.ToDouble(txamount.Text) - Convert.ToDouble(lbtotamt.Text) - dRoundHalala; //+ Convert.ToDouble(lbtotdisc.Text);

                if (sumBalance > 0)
                {

                }

                lbsuspense.Text = String.Format("{0:0.00}", dSuspense);
                if (sumBalance > 0 && Convert.ToDecimal(lbsuspense.Text) == 0)
                {
                    //dSuspense = Convert.ToDouble(txamount.Text) + sumOutstanding + sumDisc_amt + sumRound_amt - Convert.ToDouble(sumBalance);
                    dSuspense = Convert.ToDouble(txamount.Text) + sumOutstanding + sumDisc_amt - sumRound_amt - Convert.ToDouble(sumBalance) - dRoundHalala;
                    lbsuspense.Text = String.Format("{0:0.00}", dSuspense);
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally { ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true); }
    }


    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lbinvoiceno = (Label)grd.Rows[e.RowIndex].FindControl("lbinvoiceno");
            Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
            lbtotamt.Text = bll.vLookUp("select sum(amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
            bll.vDelWrkPaymentInvoice(arr);
            arr.Clear();
            grd.DataSource = null;
            grd.DataBind();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_paymentinvoice_get", arr);
            if (grd.Rows.Count > 0)
            {
                //Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
                Label lbtotRound = (Label)grd.FooterRow.FindControl("lbtotRound");
                Label lbtotdisc = (Label)grd.FooterRow.FindControl("lbtotdisc");
                Label lbtotOutStand = (Label)grd.FooterRow.FindControl("lbtotOutStand");

                lbtotamt.Text = bll.vLookUp("select sum(amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotRound.Text = bll.vLookUp("select sum(round_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotdisc.Text = bll.vLookUp("select sum(disc_amt) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotOutStand.Text = bll.vLookUp("select sum(outstanding) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            }
            rdpaid.CssClass = "control-label ro";
            lbtotalpayment.Text = lbtotamt.Text;
            double dSuspense = Convert.ToDouble(txamount.Text) - Convert.ToDouble(lbtotalpayment.Text);
            lbsuspense.Text = String.Format("{0:0.00}", dSuspense);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbmanual_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            double dAmountPaid;
            hdfAutoBalance.Value = "0";
            if (cbmanual.SelectedValue == "Automatic" && !double.TryParse(txamount.Text, out dAmountPaid))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be Paid must numeric!','Amount to be paid','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            else if (cbmanual.SelectedValue == "Automatic" && txamount.Text != "")
            {
                hdfAutoBalance.Value = Convert.ToString(txamount.Text);


                // here we validate Tot Amount Paid not grater than 
                //dTempAmountPaid = if (!double.TryParse(txamtpaid.Text, out dTempAmountPaid))
                double dTemPaid = Convert.ToDouble(bll.vLookUp("select isnull(sum(amt),0) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
                double dDiscpct = Convert.ToDouble(bll.vLookUp("select isnull(sum(disconepct),0) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));

                DataTable dtBalance = new DataTable();
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@invcheck", hdfinalcust.Value));
                arr.Add(new cArrayList("@inv_no", null));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

                dtBalance = cdl.GetValueFromSP("sp_tdosales_invoice_getByType", arr);
                string balance = Convert.ToString(dtBalance.Compute("Sum(balance)", string.Empty));
                double lblTempbalance = Convert.ToDouble(dtBalance.Compute("Sum(empbalance)", string.Empty));

                if ((Convert.ToDouble(txamount.Text) - (Convert.ToDouble(balance) + dTemPaid)) < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Total amount can not less than initial amount','Check total will be paid','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                    return;
                }
                //double dDiscpct = Convert.ToDouble(grdlbdisc1pct.Text);

                if ((lblTempbalance - (Convert.ToDouble(txamount.Text) + dDiscpct)) < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount paid can not exceeded than balance!','Amount paid exceeded','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                    return;
                }
                else
                {
                    //BindGridInvoice(hdfinalcust.Value); 
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                }

            }
            else
            {
                //BindGridInvoice(hdfinalcust.Value); 
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    // we implement later
    protected void grdInvoice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grd.EditIndex = -1;
            //BindGridInvoice(hdfinalcust.Value);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    // we implement later
    protected void grdInvoice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    // we implement later
    protected void grdInvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdInvoice.EditIndex = e.NewEditIndex;
            //BindGridInvoice(hdfinalcust.Value);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    // we implement later
    protected void grdInvoice_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            double dDiscpct = 0; double dRemain = 0; double dDiscHalala = 0; double dAmountPaid = 0; double dPaidAmount = 0;

            Label lblinv_no = (Label)grdInvoice.Rows[e.RowIndex].FindControl("lblinv_no");
            TextBox txtgrddisc = (TextBox)grdInvoice.Rows[e.RowIndex].FindControl("txtgrddisc");
            TextBox txtgrdamtpaid = (TextBox)grdInvoice.Rows[e.RowIndex].FindControl("txtgrdamtpaid");
            Label grdlbdisc1pct = (Label)grdInvoice.Rows[e.RowIndex].FindControl("grdlbdisc1pct");
            Label lblTempbalance = (Label)grdInvoice.Rows[e.RowIndex].FindControl("lblTempbalance");
            Label lblmanual_no = (Label)grdInvoice.Rows[e.RowIndex].FindControl("lblmanual_no");
            Label lbltotamt = (Label)grdInvoice.Rows[e.RowIndex].FindControl("lbltotamt");
            Label grdlblhaspaid = (Label)grdInvoice.Rows[e.RowIndex].FindControl("grdlblhaspaid");
            Label grdlbbalance = (Label)grdInvoice.Rows[e.RowIndex].FindControl("grdlbbalance");
            Label grdlbvatdisc1pct = (Label)grdInvoice.Rows[e.RowIndex].FindControl("grdlbvatdisc1pct");
            CheckBox grdchonepct = (CheckBox)grdInvoice.Rows[e.RowIndex].FindControl("grdchonepct");
            Label lblinv_dt = (Label)grdInvoice.Rows[e.RowIndex].FindControl("lblinv_dt");

            if (!double.TryParse(txamount.Text, out dAmountPaid))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be Paid must numeric!','Amount to be paid','warning');", true);
                return;
            }

            if (dAmountPaid == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be paid can not zero!','Greater than zero','warning');", true);
                return;
            }

            if (lblinv_no.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice is not selected!','Please add invoice to be paid','warning');", true);
                return;
            }
            if (!double.TryParse(txtgrddisc.Text, out dDiscHalala))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in discount fraction!','put zero is no discount','warning');", true);
                return;
            }
            else
            {
                if (dDiscHalala > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Discount fraction must below EGP 1!','Correctly discount below EGP 1','warning');", true);
                    return;
                }
            }
            double dTempAmountPaid = 0;
            if (!double.TryParse(txtgrdamtpaid.Text, out dTempAmountPaid))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in amount paid!','Amount to be paid','warning');", true);
                return;
            }
            double dTemPaid = Convert.ToDouble(bll.vLookUp("select isnull(sum(amt),0) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));
            if ((dAmountPaid - (dTempAmountPaid + dTemPaid)) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Total amount can not less than initial amount','Check total will be paid','warning');", true);
                return;
            }
            dDiscpct = Convert.ToDouble(grdlbdisc1pct.Text);
            dRemain = Convert.ToDouble(lblTempbalance.Text);
            //if ((dRemain - (dAmountPaid + dDiscpct + dDiscHalala)) < 0)
            if ((dRemain - (dAmountPaid + dDiscpct)) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount paid can not exceeded than balance!','Amount paid exceeded','warning');", true);
                return;
            }


            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", lblinv_no.Text.ToString()));
            arr.Add(new cArrayList("@manual_no", lblmanual_no.Text));
            arr.Add(new cArrayList("@inv_dt", System.DateTime.ParseExact(lblinv_dt.Text.Split(' ')[0].Split('/')[1] + "/" + lblinv_dt.Text.Split(' ')[0].Split('/')[0] + "/" + lblinv_dt.Text.Split(' ')[0].Split('/')[2], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@cust_nm", txcust.Text));
            arr.Add(new cArrayList("@totamt", lbltotamt.Text));
            arr.Add(new cArrayList("@paid", grdlblhaspaid.Text));
            arr.Add(new cArrayList("@inv_sta_nm", "RECEIVED"));

            if (grdchonepct.Checked)
            {
                dRemain = Convert.ToDouble(grdlbbalance.Text);
                //dDiscpct = 0.01 * Convert.ToDouble(lbltotamt.Text);
                dRemain += dDiscpct;
            }
            else { dDiscpct = 0; dRemain = Convert.ToDouble(grdlbbalance.Text); }
            arr.Add(new cArrayList("@remain", dRemain));
            arr.Add(new cArrayList("@disconepct", dDiscpct));
            arr.Add(new cArrayList("@disc_amt", dDiscHalala));
            arr.Add(new cArrayList("@amt", dAmountPaid));
            arr.Add(new cArrayList("@onoff", grdchonepct.Checked));
            arr.Add(new cArrayList("@received_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@tbalance", grdlbbalance.Text));
            if (lblinv_no.Text.ToString().Substring(0, 2) == "IV")
            {
                arr.Add(new cArrayList("@paymentfor", "IV"));
            }
            else
            { arr.Add(new cArrayList("@paymentfor", "BA")); }
            arr.Add(new cArrayList("@vatonepct", grdlbvatdisc1pct.Text));
            //arr.Add(new cArrayList("@ptype", grdlbvatdisc1pct.Text));
            bll.vInsertWrkPaymentInvoice2(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));

            //BindGridInvoice(hdfinalcust.Value);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    // we implement later
    protected void grdInvoice_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    // we implement later
    protected void grdInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string userID = Convert.ToString(Request.Cookies["usr_id"].Value);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblinv_no = (Label)e.Row.FindControl("lblinv_no");
                Label lblIsEligible = (Label)e.Row.FindControl("lblIsEligible");
                Label lblbalance = (Label)e.Row.FindControl("lblbalance");
                Label lbltotamt = (Label)e.Row.FindControl("lbltotamt");
                Label lblgrdamtpaid = (Label)e.Row.FindControl("lblgrdamtpaid");
                TextBox txtgrdamtpaid = (TextBox)e.Row.FindControl("txtgrdamtpaid");
                Label grdlblhaspaid = (Label)e.Row.FindControl("grdlblhaspaid");

                if (grdlblhaspaid != null)
                {
                    grdlblhaspaid.Text = bll.vLookUp("select isnull(sum(amt),0) from tpayment_dtl d join tmst_payment m on d.payment_no=m.payment_no and d.salespointcd=m.salespointcd where d.inv_no='" + lblinv_no.Text.ToString() + "' and m.payment_sta_id <> 'L' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                var alreadyAmount = string.Empty;
                double dAmountPaid;
                if (cbmanual.SelectedValue == "Automatic" && !double.TryParse(txamount.Text, out dAmountPaid))
                {

                }
                else if (cbmanual.SelectedValue == "Automatic" && lblgrdamtpaid != null)
                {

                    //alreadyAmount = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no !='" + userID + "'");
                    double dDiscpct = 0; double dRemain = 0; double dDiscHalala = 0; double dPaidAmount = 0;

                    TextBox txtgrddisc = (TextBox)e.Row.FindControl("txtgrddisc");
                    Label grdlbdisc1pct = (Label)e.Row.FindControl("grdlbdisc1pct");
                    Label lblgrddisc = (Label)e.Row.FindControl("lblgrddisc");
                    Label lblTempbalance = (Label)e.Row.FindControl("lblTempbalance");
                    Label lblmanual_no = (Label)e.Row.FindControl("lblmanual_no");
                    Label grdlbbalance = (Label)e.Row.FindControl("grdlbbalance");
                    Label grdlbvatdisc1pct = (Label)e.Row.FindControl("grdlbvatdisc1pct");
                    CheckBox grdchonepct = (CheckBox)e.Row.FindControl("grdchonepct");
                    Label lblinv_dt = (Label)e.Row.FindControl("lblinv_dt");

                    string error = string.Empty;

                    if (!double.TryParse(txamount.Text, out dAmountPaid))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be Paid must numeric!','Amount to be paid','warning');", true);
                        error += "Amount to be Paid must numeric";
                        //return;
                    }

                    if (dAmountPaid == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be paid can not zero!','Greater than zero','warning');", true);
                        error += "Amount to be paid can not zero";
                        //return;
                    }
                    if (lblinv_no.Text.ToString() == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice is not selected!','Please add invoice to be paid','warning');", true);
                        error += "Invoice is not selected";
                        //return;
                    }
                    if (txtgrddisc != null)
                    {
                        if (!double.TryParse(txtgrddisc.Text, out dDiscHalala))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in discount fraction!','put zero is no discount','warning');", true);
                            error += "Please numeric only in discount fraction";
                            //return;
                        }
                    }
                    if (lblgrddisc != null)
                    {
                        if (!double.TryParse(lblgrddisc.Text, out dDiscHalala))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in discount fraction!','put zero is no discount','warning');", true);
                            error += "Please numeric only in discount fraction";
                            //return;
                        }
                    }
                    else
                    {
                        if (dDiscHalala > 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Discount fraction must below EGP 1!','Correctly discount below EGP 1','warning');", true);
                            error += "Discount fraction must below EGP 1";
                            //return;
                        }
                    }
                    double dTempAmountPaid = 0;


                    if (txtgrdamtpaid != null)
                    {
                        if (!double.TryParse(txtgrdamtpaid.Text, out dTempAmountPaid))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in amount paid!','Amount to be paid','warning');", true);
                            error += "Please numeric only in amount paid!";
                            //return;
                        }
                    }
                    else if (lblgrdamtpaid != null)
                    {
                        if (!double.TryParse(lblgrdamtpaid.Text, out dTempAmountPaid))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please numeric only in amount paid!','Amount to be paid','warning');", true);
                            error += "Please numeric only in amount paid!";
                            //return;
                        }
                    }
                    double dTemPaid = Convert.ToDouble(bll.vLookUp("select isnull(sum(amt),0) from twrk_paymentinvoice where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));
                    //if ((dAmountPaid - (dTempAmountPaid + dTemPaid)) < 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Total amount can not less than initial amount','Check total will be paid','warning');", true);
                    //    error += "Total amount can not less than initial amount";
                    //    //return;
                    //}

                    dDiscpct = Convert.ToDouble(grdlbdisc1pct.Text);
                    dRemain = Convert.ToDouble(lblTempbalance.Text);
                    //if ((dRemain - (dAmountPaid + dDiscpct + dDiscHalala)) < 0)
                    //if ((dRemain - (dAmountPaid + dDiscpct)) < 0)
                    //{
                    //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount paid can not exceeded than balance!','Amount paid exceeded','warning');", true);
                    //    error += "Amount paid can not exceeded than balance!";
                    //    //return;
                    //}

                    if (error == "")
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@inv_no", lblinv_no.Text.ToString()));
                        arr.Add(new cArrayList("@manual_no", lblmanual_no.Text));
                        arr.Add(new cArrayList("@inv_dt", System.DateTime.ParseExact(lblinv_dt.Text.Split(' ')[0].Split('/')[1] + "/" + lblinv_dt.Text.Split(' ')[0].Split('/')[0] + "/" + lblinv_dt.Text.Split(' ')[0].Split('/')[2], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@cust_nm", txcust.Text));
                        arr.Add(new cArrayList("@totamt", lbltotamt.Text));
                        arr.Add(new cArrayList("@paid", grdlblhaspaid.Text));
                        arr.Add(new cArrayList("@inv_sta_nm", "RECEIVED"));

                        if (grdchonepct.Checked)
                        {
                            dRemain = Convert.ToDouble(grdlbbalance.Text);
                            //dDiscpct = 0.01 * Convert.ToDouble(lbltotamt.Text);
                            dRemain += dDiscpct;
                        }

                        else { dDiscpct = 0; dRemain = Convert.ToDouble(grdlbbalance.Text); }

                        decimal remaingAmount = Convert.ToDecimal(hdfAutoBalance.Value) - Convert.ToDecimal(grdlbbalance.Text);

                        hdfAutoBalance.Value = Convert.ToString(remaingAmount);


                        arr.Add(new cArrayList("@remain", dRemain));
                        arr.Add(new cArrayList("@disconepct", dDiscpct));
                        arr.Add(new cArrayList("@disc_amt", dDiscHalala));
                        arr.Add(new cArrayList("@amt", dAmountPaid));
                        arr.Add(new cArrayList("@onoff", grdchonepct.Checked));
                        arr.Add(new cArrayList("@received_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@tbalance", grdlbbalance.Text));
                        if (lblinv_no.Text.ToString().Substring(0, 2) == "IV")
                        {
                            arr.Add(new cArrayList("@paymentfor", "IV"));
                        }
                        else
                        { arr.Add(new cArrayList("@paymentfor", "BA")); }
                        arr.Add(new cArrayList("@vatonepct", grdlbvatdisc1pct.Text));
                        //arr.Add(new cArrayList("@ptype", grdlbvatdisc1pct.Text));
                        if (remaingAmount >= 0)
                        {
                            bll.vInsertWrkPaymentInvoice2(arr);
                        }
                    }
                }


                if (lblbalance != null)
                {
                    //totAmt, remaingAmt;
                    remaingAmt += Convert.ToDecimal(lblbalance.Text);
                }
                if (lbltotamt != null)
                {
                    //totAmt, remaingAmt;
                    totAmt += Convert.ToDecimal(lbltotamt.Text);
                }

                if (lblIsEligible.Text == "True")
                {
                    lblIsEligible.CssClass = "isDiscount";
                }
                else
                {
                    lblIsEligible.CssClass = "noDiscount";
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label blbGrndTot = (Label)e.Row.FindControl("blbGrndTot");
                Label lbtotBalance = (Label)e.Row.FindControl("lbtotBalance");
                blbGrndTot.Text = String.Format("{0:0.00}", totAmt);
                lbtotBalance.Text = String.Format("{0:0.00}", remaingAmt);
            }
            //BindGridInvoice(hdfinalcust.Value);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void chonepctAutomatic_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
            //GridViewRow grBA = (GridViewRow)((DataControlFieldCell)((Label)sender).Parent).Parent;
            //find the control in that
            Label lblinv_no = (Label)gr.FindControl("lblinv_no");
            Label grdlbdisc1pct = (Label)gr.FindControl("grdlbdisc1pct");
            Label grdlbvatdisc1pct = (Label)gr.FindControl("grdlbvatdisc1pct");
            Label grdlbbalance = (Label)gr.FindControl("grdlbbalance");
            CheckBox grdchonepct = (CheckBox)gr.FindControl("grdchonepct");
            double dBalance = Convert.ToDouble(grdlbbalance.Text);
            double dOnePct = 0; double dVatOnePct = 0;

            bool selectedvalue = grdchonepct.Checked;
            if (selectedvalue == true && grdlbbalance != null)
            {
                dOnePct = Convert.ToDouble(bll.vLookUp("select dbo.fn_getonepctdiscount('" + lblinv_no.Text.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')"));
                dVatOnePct = Math.Round(Convert.ToDouble(bll.vLookUp("select dbo.fn_getcontrolparameter('vat')")) * dOnePct, 2);
                dBalance -= (dOnePct + dVatOnePct);
                grdlbbalance.Text = String.Format("{0:0.00}", dBalance);// dBalance.ToString();
                grdlbdisc1pct.Text = String.Format("{0:0.00}", dOnePct);// dOnePct.ToString();
                grdlbvatdisc1pct.Text = String.Format("{0:0.00}", dVatOnePct); //dVatOnePct.ToString();
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void txamount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dAmountPaid = 0;

            if (!double.TryParse(txamount.Text, out dAmountPaid))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be Paid must numeric!','Amount to be paid','warning');", true);
                return;
            }

            if (dAmountPaid == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be paid can not zero!','Greater than zero','warning');", true);
                return;
            }
            else
            {
                txsearchinv.CssClass = cd.csstext;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void chAdvance_CheckedChanged(object sender, EventArgs e)
    {
        double dDiscpct = 0; double dRemain = 0; double dDiscHalala = 0; double dRoundHalala = 0; double dAmountPaid = 0; double dPaidAmount = 0;
        string cust_type = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        if ((cbpaymnttype.SelectedValue.ToString() == "BT" || (cbpaymnttype.SelectedValue.ToString() == "CQ")))
        {
            if (txdocno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cheque/Transfer No. can not empty !','Bank Transfer/Cheque','warning');", true);
                return;
            }            
        }

        if (!double.TryParse(txamount.Text, out dAmountPaid))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be Paid must numeric!','Amount to be paid','warning');", true);
            return;
        }

        if (dAmountPaid == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be paid can not zero!','Greater than zero','warning');", true);
            return;
        }

        //rdpaid.CssClass = "control-label ro";
        cd.v_disablecontrol(rdpaid);
        lbtotalpayment.Text = txamount.Text;
        //txamount.CssClass = cd.csstextro;
        cd.v_disablecontrol(txamount);
        lbsuspense.Text = String.Format("{0:0.00000}", txamount.Text);
        chAdvance.Enabled = false;
        tblInvoice.Attributes.Add("style", "display:none");

    }
}