using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class fm_returhoentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    decimal totalQty = 0;
    decimal totalAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbuom, "returho", "uom");
            bll.vBindingFieldValueToCombo(ref cbreturho_type, "returho_type");
            cbreturho_type_SelectedIndexChanged(sender, e);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            arr.Clear();
            arr.Add(new cArrayList("@reasn_typ", "return"));
            bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            cbwhs_cd_SelectedIndexChanged(sender, e);
            txreturno.Text = "NEW";
            dtretur.Text = System.DateTime.Today.ToShortDateString();
            arr.Clear();
            //bll.sFormat2ddmmyyyy(ref dtretur);
            dtretur.Text = Request.Cookies["waz_dt"].Value.ToString();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeltmst_returho_usr_id(arr);
            //btsave.Enabled = true;
            bindinggrd();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        string sEmp = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title", "productspv"));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        cbll bll = new cbll();
        bll.vSearchMstEmployee2(arr, ref rs);
        while (rs.Read())
        {
            sEmp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
            lEmp.Add(sEmp);
        }
        rs.Close();
        return (lEmp.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        var sessionreturho_type = HttpContext.Current.Session["returho_type"];
        var sessioninvoice_no = Convert.ToString(HttpContext.Current.Session["invoice_no"]);
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        string sItem = string.Empty;
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@item_nm", prefixText));
        arr.Add(new cArrayList("@supervisor_cd", contextKey));
        arr.Add(new cArrayList("@returho_type", sessionreturho_type));
        arr.Add(new cArrayList("@invoice_no", sessioninvoice_no));
        bll.vSearchMstItem4(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " - " + rs["item_nm"].ToString() + " - " + rs["size"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void dtexpiry_TextChanged(object sender, EventArgs e)
    {
        cbsubstatus.DataSource = null;
        cbsubstatus.DataBind();
        cbsubstatus.Items.Clear();
        DateTime dtsystem = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtexpire = DateTime.ParseExact(dtexpiry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        TimeSpan ts = dtexpire.Subtract(dtsystem);
        if (ts.Days <= 0)
        {
            lbstatus.Text = "EXPIRED";
            lbstatus.CssClass = "status form-control input-sm danger text-white";

        }
        else if (ts.Days > 90)
        {
            lbstatus.Text = "GOOD";
            bll.vBindingFieldValueToCombo(ref cbsubstatus, "item_sub_status");
            lbstatus.CssClass = "status form-control input-sm success text-white";
        }
        else if (ts.Days > 0 || ts.Days < 90)
        {
            lbstatus.Text = "NEAR EXPIRATION";
            lbstatus.CssClass = "status form-control input-sm warning text-white";
        }

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        cbuomPnl.CssClass = "";
        hditemPnl.CssClass = "";

        if (Request.Cookies["waz_dt"].Value.ToString() != dtretur.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            return;
        }
        if (dtexpiry.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Check Expiry Date','warning');", true);
            return;
        }

        double dQty = 0;
        if (!double.TryParse(txqty.Text, out dQty))
        {
            if (dQty <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty can not less than 0','Check Qty','warning');", true);
                return;
            }
        }

        //if (cbreturho_type.SelectedValue == "REG") // If not regular retur it by passed any checking : 16 IAG Oct 17
        //{

        //double dMaxAmount = Convert.ToDouble(lbamount.Text);
        //double drettotal = 0;
        //if (grd.Rows.Count > 0)
        //{
        //    Label lblRetHOAmt = (Label)grd.FooterRow.FindControl("lblTotalRetHO_Amount");
        //    drettotal = Convert.ToDouble(lblRetHOAmt.Text);
        //}


        //double dAmt = Convert.ToDouble(txqty.Text) * Convert.ToDouble(txRetHO_price.Text);
        //if (dMaxAmount < (drettotal + dAmt))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Return amount can not exceeded approved by Prod Spv!','Return amount exceeded','warning');", true);
        //    return;
        //}
        //}
        List<cArrayList> arr = new List<cArrayList>();
        String returno;
        if (txreturno.Text == "" || txreturno.Text == "NEW") { returno = Request.Cookies["usr_id"].Value.ToString(); } else { returno = txreturno.Text; }
        arr.Add(new cArrayList("@returho_no", returno));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue));
        arr.Add(new cArrayList("@RetHO_price", txRetHO_price.Text));
        arr.Add(new cArrayList("@RetHO_Amount", txRetHO_Amount.Text));
        arr.Add(new cArrayList("@exp_dt", DateTime.ParseExact(dtexpiry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@item_status", lbstatus.Text));
        arr.Add(new cArrayList("@stkavl", txstkavl.Text));
        //arr.Add(new cArrayList("@item_cond", cbsubstatus.SelectedValue.ToString()));
        bll.vInserttreturho_dtl(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vBindingGridToSp(ref grd, "sp_twrk_returho_get", arr);
        bindinggrd();
        lbstatus.Text = "";
        dtexpiry.Text = "";
        txqty.Text = "";
        txRetHO_price.Text = "";
        txRetHO_Amount.Text = "";
        txitemcode.Text = "";
        txinvoice_no.CssClass = "form-control ro";
    }
    private void bindinggrd()
    {
        totalQty = 0;
        totalAmount = 0;
        string returno;
        if (txreturno.Text == "" || txreturno.Text == "NEW") { returno = Request.Cookies["usr_id"].Value.ToString(); } else { returno = txreturno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@returho_no", returno));
        bll.vBindingGridToSp(ref grd, "sp_treturho_dtl_get", arr);
    }
    protected void txitemcode_TextChanged(object sender, EventArgs e)
    {
        txitemcode_AutoCompleteExtender.ContextKey = hdemp.Value.ToString();
        cbuom_SelectedIndexChanged(sender, e);
        disableheader();
    }
    private void disableheader()
    {
        cbwhs_cd.CssClass = "form-control input-sm ro";
        cbwhs_cd.Enabled = false;
        cbbin_cd.CssClass = "form-control input-sm ro";
        cbbin_cd.Enabled = false;
        cbreturho_type.CssClass = "form-control input-sm ro";
        cbreturho_type.Enabled = false;
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txreturho_manual_no.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Please fill manual return !!','warning');", true);
            return;
        }

        string sManualNo = bll.vLookUp("select dbo.fn_checkmanualno('returho','" + txreturho_manual_no.Text + "')");
        if (sManualNo != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This manual no. " + txreturho_manual_no.Text + "  already used','Please use another !!','warning');", true);
            return;
        }
        if (Request.Cookies["waz_dt"].Value.ToString() != dtretur.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            return;
        }
        foreach (GridViewRow row in grd.Rows)
        {
            Label lbqty = (Label)row.FindControl("lbqty");
            Label txtitemcode = (Label)row.FindControl("txtitemcode");
            Label lblUOM = (Label)row.FindControl("lblUOM");
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sstockqty = bll.vLookUp("select dbo.[sfnGetStock]('" + txtitemcode.Text.ToString() + "','" + cbbin_cd.SelectedValue.ToString() + "','" + cbwhs_cd.SelectedValue.ToString() + "','0','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
            string sconv = bll.vLookUp("select dbo.sfnUomQtyConv('" + txtitemcode.Text.ToString() + "','CTN','" + lblUOM.Text.ToString() + "',1)");
            decimal dqty = Convert.ToDecimal(lbqty.Text) / Convert.ToDecimal(sconv);
            decimal dstockqty = Convert.ToDecimal(sstockqty);
            if (dqty > dstockqty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough ! " + txtitemcode.Text + " Qty Stock :" + sstockqty + " Qty Return : " + lbqty.Text + "' ,'Stock Not Enough','warning');", true);
                return;
            }
        }
        if (txreturno.Text == "NEW" || txreturno.Text == "")
        {
            string sReturHoNo = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@productspv_cd", hdemp.Value.ToString()));
            arr.Add(new cArrayList("@returho_dt", DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@returho_type", cbreturho_type.SelectedValue));
            arr.Add(new cArrayList("@invoice_no", hdinvoice_no.Value));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
            arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
            arr.Add(new cArrayList("@returho_manual_no", txreturho_manual_no.Text));
            arr.Add(new cArrayList("@driver_nm", txdriver_nm.Text));
            arr.Add(new cArrayList("@vehicle_no", txvehicle_no.Text));
            arr.Add(new cArrayList("@phone_no", txphone_no.Text));
            arr.Add(new cArrayList("@reason", cbreason.SelectedValue));
            bll.vInsertMstReturHO2(arr, ref sReturHoNo);
            txreturno.Text = sReturHoNo;
            // IAG : 4 Nov 2017 : Purposed : Accounting for stock card
            arr.Clear();
            arr.Add(new cArrayList("@stockcard_typ", "RETURHO"));
            arr.Add(new cArrayList("@refno", sReturHoNo));
            // bll.vBatchStockCard(arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(),Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?no="+sReturHoNo+"&src=returho');", true);
        }
        else
        {
            String returno;
            if (txreturno.Text == "" || txreturno.Text == "NEW") { returno = Request.Cookies["usr_id"].Value.ToString(); } else { returno = txreturno.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@returho_no", returno));
            arr.Add(new cArrayList("@productspv_cd", hdemp.Value.ToString()));
            arr.Add(new cArrayList("@returho_dt", DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@returho_type", cbreturho_type.SelectedValue));
            arr.Add(new cArrayList("@invoice_no", hdinvoice_no.Value));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
            arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
            arr.Add(new cArrayList("@returho_manual_no", txreturho_manual_no.Text));
            arr.Add(new cArrayList("@driver_nm", txdriver_nm.Text));
            arr.Add(new cArrayList("@vehicle_no", txvehicle_no.Text));
            arr.Add(new cArrayList("@phone_no", txphone_no.Text));
            arr.Add(new cArrayList("@reason", cbreason.SelectedValue));
            bll.vUpdatetmst_returho2(arr);
        }
        //arr.Clear();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //arr.Add(new cArrayList("@returho_no", sReturHoNo));
        //bll.vInsertReturHoDtl(arr);
        bindinggrd();
        // EHC : IAG 4 Nov 2017 : Accounting Supported for Stock Card
        //bll.vInsertEmailOutbox(arr);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        //btsave.Enabled = false;
        //btprintt.Enabled = true;
        if (cbreturho_type.SelectedValue == "REG")
        {
            //Send Notification
            lbstatus.Text = "Waiting Approval";
            List<string> lapprovalccspv = bll.lGetApproval(hdemp.Value);
            List<string> lapproval = bll.lGetApproval("returnho", 1);
            List<string> lapprovalcc = bll.lGetApproval("returnho", 2);
            Random rnd2 = new Random();
            int nRnd2 = rnd2.Next(1000, 9999);
            string ssp = Request.Cookies["sp"].Value.ToString();
            string ssp1 = ssp.Substring(0, 1);
            string ssp2 = ssp.Substring(0, 1) + ssp.Substring(ssp.Length - 1);
            string strantoken = "00" + bll.vLookUp("select fld_valu from tfield_value where fld_nm='trantoken' and fld_desc='returnho'");
            string stoken = txreturno.Text.Substring(txreturno.Text.Length - 4);
            if (stoken.Substring(0, 3) == "000") { stoken = strantoken + stoken.Substring(stoken.Length - 1); }
            if (stoken.Substring(0, 2) == "00") { stoken = ssp2 + stoken.Substring(stoken.Length - 2); }
            if (stoken.Substring(0, 1) == "0") { stoken = ssp1 + stoken.Substring(stoken.Length - 3); }
            string stoken2 = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + stoken.ToString();
            string sMsg = "Return to HO tran no " + txreturno.Text + ", detail item in email, do you want to approved : (Y/N)" + stoken2.ToString();
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@token", stoken2.ToString()));
            arr.Add(new cArrayList("@msg", sMsg));
            arr.Add(new cArrayList("@doc_typ", "returnho"));
            arr.Add(new cArrayList("@to", lapproval[0]));
            arr.Add(new cArrayList("@doc_no", txreturno.Text));
            bll.vInsertSmsOutbox(arr); //by yanto 15-11-2016

            //arr.Clear();
            //arr.Add(new cArrayList("@token", nRnd2.ToString()));
            //arr.Add(new cArrayList("@msg", sMsg));
            //arr.Add(new cArrayList("@doc_typ", "returnho"));
            //arr.Add(new cArrayList("@to", lapproval2[0]));
            //arr.Add(new cArrayList("@doc_no", txreturno.Text));
            //bll.vInsertSmsOutbox(arr); //send sms to logistic ho

            string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
            string ssp_nm = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + ssp + "'");
            String sText = "<html><head><body>Dear, New Return to HO has been created , with no. " + txreturno.Text +
                "<p> Date  : " + dtretur.Text + " Salespoint : " + ssp + " - " + ssp_nm +
                 "<p> for detail please see attached file </p>" +
                 "<p>Please Click this  for approved : <a href='" + slink_branch + "/landingpage.aspx?trnname=returnho&salespointcd=" + ssp + "&RefNo=" + txreturno.Text + "&appcode=" + stoken2.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage.aspx?trnname=returnho&salespointcd=" + ssp + "&RefNo=" + txreturno.Text + "&sta=L'>Reject</a></p>";
            int n = sText.Length;
            string sPath = bll.sGetControlParameter("image_path");
            string sPdfName = txreturno.Text + ".pdf";
            string sSubject = "New Return to HO " + txreturno.Text + " Has Been Created";
            arr.Clear();
            arr.Add(new cArrayList("@returho_no", txreturno.Text));
            rep.vShowReportToPDFWithSP("rp_returHO.rpt", arr, sPath + sPdfName);
            arr.Clear();
            arr.Add(new cArrayList("@token", stoken2));
            arr.Add(new cArrayList("@doc_typ", "returnho"));
            arr.Add(new cArrayList("@to", lapproval[1]));
            arr.Add(new cArrayList("@doc_no", txreturno.Text));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sText));
            arr.Add(new cArrayList("@file_attachment", sPdfName));
            bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017

            arr.Clear();
            arr.Add(new cArrayList("@token", stoken2));
            arr.Add(new cArrayList("@doc_typ", "returnho"));
            arr.Add(new cArrayList("@to", lapprovalccspv[1]));
            arr.Add(new cArrayList("@doc_no", txreturno.Text));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sText));
            arr.Add(new cArrayList("@file_attachment", sPdfName));
            bll.vInsertEmailOutbox(arr); //send email to logistic ho

            arr.Clear();
            arr.Add(new cArrayList("@token", stoken2));
            arr.Add(new cArrayList("@doc_typ", "returnho"));
            arr.Add(new cArrayList("@to", lapprovalcc[1]));
            arr.Add(new cArrayList("@doc_no", txreturno.Text));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sText));
            arr.Add(new cArrayList("@file_attachment", sPdfName));
            bll.vInsertEmailOutbox(arr);

            //bll.vSendMail(lapproval[1], sSubject, sText, sPdfName);
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wdw", "window.alert('\tData has been save successfully .. \t\n')", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Save successfully !')", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Save successfully ..','Return No. " + txreturno.Text + "','success');", true);

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtretur.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            grd.EditIndex = -1;
            return;
        }
        Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
        grd.EditIndex = e.NewEditIndex;
        bindinggrd();
        DropDownList cboUOM = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cboUOM");
        bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
        cboUOM.SelectedValue = lblUOM.Text;
        TextBox txtqty = (TextBox)grd.Rows[e.NewEditIndex].FindControl("txtqty");
        TextBox txtstkavl = (TextBox)grd.Rows[e.NewEditIndex].FindControl("txtstkavl");
        Label txtitemcode = (Label)grd.Rows[e.NewEditIndex].FindControl("txtitemcode");

        DateTime dt = DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        String sdate = dt.ToString("yyyy-M-d");
        string sStock = bll.vLookUp("select [dbo].[sfnGetStock] ('" + txtitemcode.Text + "','" + cbbin_cd.SelectedValue + "','" + cbwhs_cd.SelectedValue + "','DEPO','" + sdate + "')");
        string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + txitemcode.Text + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
        double dStock = Convert.ToDouble(txtqty.Text) + (Convert.ToDouble(sStock) * Convert.ToDouble(sQTY));
        txtstkavl.Text = dStock.ToString();

    }

    protected void txqty_TextChanged(object sender, EventArgs e)
    {
        if (cbreturho_type.SelectedValue == "INV")
        {
            var qtydo = bll.vLookUp("select qty from tdo_dtl inner join tmst_do on tdo_dtl.do_no=tmst_do.do_no where tmst_do.invoice_no='" + hdinvoice_no.Value + "' and item_cd='" + hditem.Value.ToString() + "'");
            var pricedo = bll.vLookUp("select unitprice from tdo_dtl inner join tmst_do on tdo_dtl.do_no=tmst_do.do_no where tmst_do.invoice_no='" + hdinvoice_no.Value + "' and item_cd='" + hditem.Value.ToString() + "'");
            if (Convert.ToDouble(txqty.Text) > Convert.ToDouble(qtydo))
            {
                txqty.Text = qtydo;
                txRetHO_price.Text = pricedo;
                Double dobamt = Convert.ToDouble(qtydo) * Convert.ToDouble(pricedo);
                txRetHO_Amount.Text = Convert.ToString(dobamt);
            }
        }
        qtyship();
    }
    protected void qtyship()
    {
        if (txRetHO_price.Text == "") { txRetHO_price.Text = "0"; }
        if (txqty.Text == "") { txqty.Text = "0"; }
        txRetHO_Amount.Text = Convert.ToString((Convert.ToDecimal(txqty.Text) * Convert.ToDecimal(txRetHO_price.Text)));
    }
    protected void txRetHO_price_TextChanged(object sender, EventArgs e)
    {
        qtyship();
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txreturno.Text = Convert.ToString(Session["looreturho_no"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@returho_no", txreturno.Text));
        bll.vGettmst_returho(arr, ref rs);
        while (rs.Read())
        {
            dtretur.Text = string.Format("{0:d/M/yyyy}", rs["returho_dt"]);
            hdemp.Value = rs["productspv_cd"].ToString();
            txemployee.Text = bll.vLookUp("select (emp_cd + ' - ' + emp_nm) as emp_desc from tmst_employee where emp_cd='" + rs["productspv_cd"].ToString() + "'");
            cbreturho_type.SelectedValue = rs["returho_type"].ToString();
            hdinvoice_no.Value = rs["invoice_no"].ToString();
            txinvoice_no.Text = hdinvoice_no.Value;
            cbwhs_cd.SelectedValue = rs["whs_cd"].ToString();
            cbwhs_cd_SelectedIndexChanged(sender, e);
            cbbin_cd.SelectedValue = rs["bin_cd"].ToString();
            txreturho_manual_no.Text = rs["returho_manual_no"].ToString();
            txdriver_nm.Text = rs["driver_nm"].ToString();
            txvehicle_no.Text = rs["vehicle_no"].ToString();
            txphone_no.Text = rs["phone_no"].ToString();
            if (rs["reason"].ToString() != "") cbreason.SelectedValue = rs["reason"].ToString();
        }
        rs.Close();
        bindinggrd();
        cbreturho_type_SelectedIndexChanged(sender, e);
        disableheader();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListinvoice_no(string prefixText, int count, string contextKey)
    {
        string sinvoice_no = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> linvoice_no = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@invoice_no", prefixText));
        cbll bll = new cbll();
        bll.vSearchtmst_do2(arr, ref rs);
        while (rs.Read())
        {
            sinvoice_no = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["invoice_no"].ToString(), rs["invoice_no"].ToString());
            linvoice_no.Add(sinvoice_no);
        }
        rs.Close();
        return (linvoice_no.ToArray());
    }
    protected void cbreturho_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreturho_type.SelectedValue == "REG")
        {
            txemployee.Visible = true;
            txinvoice_no.Visible = false;
            txinvoice_no.Text = null;
            hdinvoice_no.Value = null;
            hditem.Value = null;
            txitemcode.Text = null;
            lbempInv.Text = "Product Supervisor";
            lbRTmark.Visible = true;
        }
        else
        {
            txinvoice_no.Visible = true;
            txemployee.Visible = false;
            txemployee.Text = null;
            hdemp.Value = null;
            hditem.Value = null;
            txitemcode.Text = null;
            lbempInv.Text = "Invoice No.";
            lbRTmark.Visible = false;
        }
        Session["returho_type"] = cbreturho_type.SelectedValue;
    }
    protected void txinvoice_no_TextChanged(object sender, EventArgs e)
    {
        Session["invoice_no"] = hdinvoice_no.Value;
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtretur.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            return;
        }
        Label lbIDS = (Label)grd.Rows[e.RowIndex].FindControl("lbIDS");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@IDS", lbIDS.Text));
        bll.vDeletetreturho_dtl(arr);
        bindinggrd();
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_returhoentry.aspx");
    }


    protected void cbwhs_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbin_cd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }


    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bindinggrd();
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txtqty");
        DropDownList cboUOM = (DropDownList)grd.Rows[e.RowIndex].FindControl("cboUOM");
        TextBox txtRetHO_price = (TextBox)grd.Rows[e.RowIndex].FindControl("txtRetHO_price");
        TextBox txtexp_dt = (TextBox)grd.Rows[e.RowIndex].FindControl("txtexp_dt");
        Label txitem_status = (Label)grd.Rows[e.RowIndex].FindControl("txitem_status");
        Label lbIDS = (Label)grd.Rows[e.RowIndex].FindControl("lbIDS");
        TextBox txtstkavl = (TextBox)grd.Rows[e.RowIndex].FindControl("txtstkavl");
        Label txitemcode = (Label)grd.Rows[e.RowIndex].FindControl("txitemcode");

        //DateTime dt = DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //String sdate = dt.ToString("yyyy-M-d");
        //string sStock = bll.vLookUp("select [dbo].[sfnGetStock] ('" + txitemcode.Text + "','" + cbbin_cd.SelectedValue + "','" + cbwhs_cd.SelectedValue + "','DEPO','" + sdate + "')");
        //string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + txitemcode.Text + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
        //txstkavl.Text = Convert.ToDouble(txtqty.Text)+(Convert.ToDouble(sStock) * Convert.ToDouble(sQTY)).ToString();
        if (txtqty.Text != "")
        {
            if (Convert.ToDouble(txtstkavl.Text) > Convert.ToDouble(txtqty.Text))
            {
                txtqty.Text = txtqty.Text;
            }
            else
            {
                txtqty.Text = txtstkavl.Text;
            }
        }

        string RetHO_Amount;

        RetHO_Amount = Convert.ToString((Convert.ToDecimal(txtqty.Text) * Convert.ToDecimal(txtRetHO_price.Text)));

        DateTime dtsystem = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtexpire = DateTime.ParseExact(txtexp_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        TimeSpan ts = dtexpire.Subtract(dtsystem);
        if (ts.Days <= 0)

        {
            txitem_status.Text = "EXPIRED";
        }
        else if (ts.Days > 90)
        {
            txitem_status.Text = "GOOD";
        }
        else if (ts.Days > 0 || ts.Days < 90)
        {
            txitem_status.Text = "NEAR EXPIRATION";

        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@IDS", lbIDS.Text));
        arr.Add(new cArrayList("@qty", txtqty.Text));
        arr.Add(new cArrayList("@UOM", cboUOM.SelectedValue));
        arr.Add(new cArrayList("@RetHO_price", txtRetHO_price.Text));
        arr.Add(new cArrayList("@RetHO_Amount", RetHO_Amount));
        arr.Add(new cArrayList("@exp_dt", DateTime.ParseExact(txtexp_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@item_status", txitem_status.Text));
        arr.Add(new cArrayList("@stkavl", txtstkavl.Text));
        bll.vUpdatetreturho_dtl(arr);
        grd.EditIndex = -1; arr.Clear();
        bindinggrd();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
       
        if (txreturno.Text != "" || txreturno.Text != "NEW")
        {
            string sMsg = bll.vLookUp("select dbo.fn_checkreturho('" + txreturno.Text + "')");
            if (sMsg != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('"+sMsg+"','"+txreturno.Text+"','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@returho_no", txreturno.Text));
            Session["lParamretHO"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=retHO');", true);
        }
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtretur.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            return;
        }
        if (txreturno.Text != "" || txreturno.Text != "NEW")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@returho_no", txreturno.Text));
            bll.vDeletetmst_returho(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Deleted successfully !')", true);
            Response.Redirect("fm_returhoentry.aspx");
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            Label lbRetHO_Amount = (Label)e.Row.FindControl("lbRetHO_Amount");
            decimal qty;
            decimal amount;
            if (lbRetHO_Amount != null)
                amount = decimal.Parse(lbRetHO_Amount.Text);
            else
                amount = 0;
            if (lbqty != null)
                qty = decimal.Parse(lbqty.Text);
            else
                qty = 0;
            totalQty = totalQty + qty;
            totalAmount = totalAmount + amount;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            Label lblTotalRetHO_Amount = (Label)e.Row.FindControl("lblTotalRetHO_Amount");
            lblTotalqty.Text = totalQty.ToString();
            lblTotalRetHO_Amount.Text = totalAmount.ToString("#,##0.00");
        }
    }
    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbuomPnl.CssClass = "";
        hditemPnl.CssClass = "";
        if (cbuom.SelectedValue == "")
        {
            txstkavl.Text = "";
            cbuomPnl.CssClass = "error";
            return;
        }
        double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + hditem.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
        if (dConv == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Conversion uom not yet setup !','UOM Conversion','warning');", true);
            hditemPnl.CssClass = "error";
            txstkavl.Text = "";
            cbuom.SelectedValue = "";
            return;
        }
        DateTime dt = DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        String sdate = dt.ToString("yyyy-M-d");
        string sStock = bll.vLookUp("select [dbo].[sfnGetStock] ('" + hditem.Value + "','" + cbbin_cd.SelectedValue + "','" + cbwhs_cd.SelectedValue + "','DEPO','" + sdate + "')");
        string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbuom.SelectedValue.ToString() + "',1)");
        txstkavl.Text = (Convert.ToDouble(sStock) * Convert.ToDouble(sQTY)).ToString();
        if (txqty.Text != "")
        {
            if (Convert.ToDouble(txstkavl.Text) > Convert.ToDouble(txqty.Text))
            {
                txqty.Text = txqty.Text;
            }
            else
            {
                txqty.Text = txstkavl.Text;
            }
        }
        getprice();
    }
    private void getprice()
    {
        double dPrice = 0;
        string sCustType = "";
        var qtydo = bll.vLookUp("select qty from tdo_dtl inner join tmst_do on tdo_dtl.do_no=tmst_do.do_no where tmst_do.invoice_no='" + hdinvoice_no.Value + "' and item_cd='" + hditem.Value.ToString() + "'");
        var pricedo = bll.vLookUp("select unitprice from tdo_dtl inner join tmst_do on tdo_dtl.do_no=tmst_do.do_no where tmst_do.invoice_no='" + hdinvoice_no.Value + "' and item_cd='" + hditem.Value.ToString() + "'");
        if (qtydo == "") { qtydo = "0"; }
        if (pricedo == "") { pricedo = "0"; }
        txqty.Text = qtydo;
        if (cbreturho_type.SelectedValue == "INV" & hditem.Value != "")
        {
            txRetHO_price.Text = pricedo;
        }
        else if (cbreturho_type.SelectedValue == "REG" & hditem.Value != "")
        {
            sCustType = bll.vLookUp("select top 1 fld_valu from tfield_value where fld_nm='otlbrn'");
            dPrice = bll.dGetItemPrice(hditem.Value.ToString(), sCustType, cbuom.SelectedValue.ToString());
            txRetHO_price.Text = dPrice.ToString();
        }
        Double dobamt = Convert.ToDouble(qtydo) * Convert.ToDouble(txRetHO_price.Text);
        txRetHO_Amount.Text = Convert.ToString(dobamt);
    }
    protected void btemp_Click(object sender, EventArgs e)
    {
        //lbamount.Text =  bll.vLookUp("select isnull(amt,0) from treturnho_booking where supervisor_cd='"+hdemp.Value.ToString()+"' and reqretho_sta_id='A'");
        //if (lbamount.Text == "") { lbamount.Text = "0"; }
    }

}