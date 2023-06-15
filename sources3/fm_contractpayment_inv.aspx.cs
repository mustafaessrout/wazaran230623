using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_contractpayment_inv : System.Web.UI.Page
{

    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            if (bll.vLookUp("select dbo.fn_gettransblock()") != "ok")
            {
                Response.Redirect("alert_denied.aspx?m=1");
                return;
            }

            btpayment.Text = "";
            lbcontract.Text = Request.QueryString["no"];
            lbbranch.Text = Request.Cookies["spn"].Value.ToString();
            uploadAG.Attributes.Add("style", "display:none");
            rejectAG.Attributes.Add("style", "display:none");
            paymentAG.Attributes.Add("style", "display:none");
            reprintAG.Attributes.Add("style", "display:none");
            driverAG.Attributes.Add("style", "display:none");
            customerAG.Attributes.Add("style", "display:none");
            if (Request.QueryString["act"] == "upload")
            {
                btpayment.Text = "Upload";
                uploadAG.Attributes.Remove("style");
            }
            else if (Request.QueryString["act"] == "reject")
            {
                btpayment.Text = "Reject";
                rejectAG.Attributes.Remove("style");
            }
            else if (Request.QueryString["act"] == "payment")
            {
                btpayment.Text = "Add Payment";
                paymentAG.Attributes.Remove("style");
                detailPayment();
            }
            else if (Request.QueryString["act"] == "reprint")
            {
                btpayment.Text = " ";
                reprintAG.Attributes.Remove("style");
                printPayment();
            }
            else if (Request.QueryString["act"] == "driver")
            {
                btpayment.Text = "Driver Receipt";
                driverAG.Attributes.Remove("style");
                detailReceipt();
            }
            else if (Request.QueryString["act"] == "customer")
            {
                btpayment.Text = "Customer Receipt";
                customerAG.Attributes.Remove("style");
            }
        }
    }

    protected void detailReceipt()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@qry_cd", "driver"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingComboToSp(ref cbdriverDR, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingComboToSp(ref cbvehicleDR, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ", arr);
        arr.Clear();
    }

    protected void printPayment()
    {
        List<cArrayList> arr = new List<cArrayList>();
        string cust = bll.vLookUp("select rdcust from tmst_contract where contract_no = '" + lbcontract.Text + "'");
        if (cust == "C")
        {
            grdscheduleprint.Visible = true;
            grdschedulegprint.Visible = false;
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbcontract.Text));
            bll.vBindingGridToSp(ref grdscheduleprint, "sp_contract_payment_schedule", arr);
        }
        else
        {
            grdscheduleprint.Visible = false;
            grdschedulegprint.Visible = true;
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbcontract.Text));
            bll.vBindingGridToSp(ref grdschedulegprint, "sp_contract_payment_schedule_customer", arr);
        }
    }

    protected void detailPayment()
    {
        List<cArrayList> arr = new List<cArrayList>();
        // Cek Customer 
        string cust = bll.vLookUp("select rdcust from tmst_contract where contract_no = '" + lbcontract.Text + "'");
        if (cust == "C")
        {
            customer.Attributes.Add("style", "display:none");
            grdschedule.Visible = true;
            grdscheduleg.Visible = false;
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbcontract.Text));
            bll.vBindingGridToSp(ref grdschedule, "sp_contract_payment_schedule", arr);
            txamount.Text = bll.vLookUp("select top 1 qty from tcontract_payschedule where contract_no = '" + lbcontract.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
        }
        else
        {
            customer.Attributes.Remove("style");
            grdschedule.Visible = false;
            grdscheduleg.Visible = true;
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbcontract.Text));
            bll.vBindingComboToSp(ref cbcustomer, "sp_tcontract_cusgrcd_customer", "cust_cd", "cust_nm", arr);
            bll.vBindingGridToSp(ref grdscheduleg, "sp_contract_payment_schedule_customer", arr);
            txamount.Text = bll.vLookUp("select top 1 (tcontract_payschedule.qty-isnull(tcontract_payment.amount,0)) as qty from tcontract_payschedule left join tcontract_payment on tcontract_payschedule.contract_no = tcontract_payment.contract_no and tcontract_payschedule.sequenceno = tcontract_payment.sequenceno where tcontract_payschedule.contract_no = '" + lbcontract.Text + "' order by tcontract_payschedule.sequenceno asc");
        }
        // Cek Customer
        txagreecdPaid.Text = lbcontract.Text;
        txseqnoPaid.Text = bll.vLookUp("select top 1 sequenceno from tcontract_payschedule where contract_no = '" + lbcontract.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
        dtduePaid.Text = bll.vLookUp("select top 1 convert(varchar(10),(case when paymentadj_dt is null then payment_dt else paymentadj_dt end),103) due_dt from tcontract_payschedule where contract_no = '" + lbcontract.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
        lbuomPaid.Text = bll.vLookUp("select top 1 uom from tcontract_payschedule where contract_no = '" + lbcontract.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
        dtpay.Text = bll.vLookUp("select top 1 convert(varchar(10),parm_valu,103) due_dt from tcontrol_parameter where parm_nm = 'wazaran_dt' ");
        dtduePaid.Attributes.Add("readonly", "readonly");
        dtpay.Attributes.Add("readonly", "readonly");
        txseqnoPaid.Attributes.Add("readonly", "readonly");
        txagreecdPaid.Attributes.Add("readonly", "readonly");
        txamount.Attributes.Add("readonly", "readonly");
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
        arr.Clear();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
        string sDefBin = bll.vLookUp("select top 1 qry_data from tmap_query where qry_cd='bin_branch'");
        cbbin.SelectedValue = sDefBin;
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", lbcontract.Text));
        arr.Add(new cArrayList("@bin", cbbin.SelectedValue.ToString()));
        arr.Add(new cArrayList("@wh", cbwhs.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_contract_payment_freeitem", arr);
        arr.Clear();
        arr.Add(new cArrayList("@qry_cd", "driver"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ", arr);
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", txagreecdPaid.Text));
        arr.Add(new cArrayList("@bin", cbbin.SelectedValue.ToString()));
        arr.Add(new cArrayList("@wh", cbwhs.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_contract_payment_freeitem", arr);
    }

    protected void grdschedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        Label lbstatus = (Label)e.Row.FindControl("lbstatus");

        if (lbstatus.Text != "Paid")
        {
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = false;
        }
    }

    protected void grdschedule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdschedule.Rows[index];
            Label lbseqno = (Label)grdschedule.Rows[index].FindControl("lbseqno");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinv&ba=" + lbcontract.Text + "&no=" + lbseqno.Text + "');", true);
        }
        else if (e.CommandName == "reprint")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdscheduleprint.Rows[index];
            Label lbseqno = (Label)grdscheduleprint.Rows[index].FindControl("lbseqno");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinv&ba=" + lbcontract.Text + "&no=" + lbseqno.Text + "');", true);
        }
    }


    protected void grdscheduleg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdscheduleg.Rows[index];
            Label lbseqno = (Label)grdscheduleg.Rows[index].FindControl("lbseqno");
            Label lbcustomer = (Label)grdscheduleg.Rows[index].FindControl("lbcustomer");
            string[] cust_cd = lbcustomer.Text.Split('_');
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinvg&ba=" + lbcontract.Text + "&no=" + lbseqno.Text + "&cust=" + cust_cd[0].ToString() + "');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinv&ba=" + lbcontract.Text + "&no=" + lbseqno.Text + "');", true);
        }
        else if (e.CommandName == "reprint")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdschedulegprint.Rows[index];
            Label lbseqno = (Label)grdschedulegprint.Rows[index].FindControl("lbseqno");
            Label lbcustomer = (Label)grdschedulegprint.Rows[index].FindControl("lbcustomer");
            string[] cust_cd = lbcustomer.Text.Split('_');
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinvg&ba=" + lbcontract.Text + "&no=" + lbseqno.Text + "&cust=" + cust_cd[0].ToString() + "');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinv&ba=" + lbcontract.Text + "&no=" + lbseqno.Text + "');", true);
        }
    }

    protected void grdscheduleg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        Label lbstatus = (Label)e.Row.FindControl("lbstatus");

        if (lbstatus.Text != "Paid")
        {
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = false;
        }
    }

    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }
    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", txagreecdPaid.Text));
        arr.Add(new cArrayList("@bin", cbbin.SelectedValue.ToString()));
        arr.Add(new cArrayList("@wh", cbwhs.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_contract_payment_freeitem", arr);
    }

    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", cbdriver.SelectedValue.ToString()));
        string sVhc = bll.vLookUp("select vhc_cd from tmst_vehicle where emp_cd='" + cbdriver.SelectedValue.ToString() + "'");
        if (sVhc == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Driver has not vehicle setup !','Please setup first in vehicle driver master ','warning');", true);
            return;
        }
        else
        {
            //bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_getbyempcd", "vhc_cd", "vhc_nm", arr);
            //bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ");
            cbvehicle.SelectedValue = sVhc;
        }
    }

    protected void btpayment_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (btpayment.Text == "Upload")
        {            
            if (upl.HasFile)
            {
                FileInfo fi = new FileInfo(upl.FileName);
                string ext = fi.Extension;
                byte[] fs = upl.FileBytes;
                if (fs.Length <= 104857600)
                {
                    if ((upl.FileName != "") || (upl.FileName != null))
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@contract_no", lbcontract.Text));
                        arr.Add(new cArrayList("@fileloc", lbcontract.Text.ToString() + ext));
                        upl.SaveAs(bll.sGetControlParameter("image_path") + "/contract_doc/" + lbcontract.Text.ToString() + ext);
                        bll.vUpdateContractDoc(arr);
                        Random rnd = new Random();
                        int token = rnd.Next(1000, 9999);
                        //Send Approval SMS
                        string sSPN = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                        string sPropNo = bll.vLookUp("select prop_No from tmst_contract where contract_no='" + lbcontract.Text + "'");
                        string sCust = bll.vLookUp("select mc.cust_cd+'-'+mc.cust_nm from tcontract_customer tc left join tmst_customer mc on tc.cust_cd = mc.cust_cd and tc.salespointcd=mc.salespointcd where tc.contract_no='" + lbcontract.Text + "'");
                        List<string> lapproval = bll.lGetApproval("contractapp", 1);
                        string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + token.ToString();
                        string sSMS = "#Business Agreement (" + lbcontract.Text + ") Prop No : " + sPropNo.ToString() + ", BRN:" + sSPN + ", Customer : " + sCust.ToString() + " ,pls reply with (Y/N)" + stoken.ToString();        // cd.vSendSms(sSMS, lapproval[0]);
                        arr.Clear();
                        //arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@token", stoken.ToString()));
                        arr.Add(new cArrayList("@doc_typ", "contractapp"));
                        arr.Add(new cArrayList("@to", lapproval[0]));
                        arr.Add(new cArrayList("@doc_no", lbcontract.Text));
                        arr.Add(new cArrayList("@msg", sSMS));
                        bll.vInsertSmsOutbox(arr);
                        ////Insert into Stockcard
                        //arr.Clear();
                        //arr.Add(new cArrayList("@refno", lbcontract.Text));
                        //arr.Add(new cArrayList("@stockcard_typ", "ONDELIVERYBA"));
                        //bll.vBatchStockCard(arr);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "upload", "window.opener.RefreshData('upload');window.close();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Proposal');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 100MB');", true);
                    return;
                }
            }
        }
        else if (btpayment.Text == "Reject")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbcontract.Text));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remarks", txremarkReject.Text));
            bll.vUpdateContractRej(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reject", "window.opener.RefreshData('reject');window.close();", true);
        }
        else if (btpayment.Text == "Add Payment")
        {
            arr.Clear();
            string sStock = "", listItem = "";
            bool stStock = false;
            string[] freeitem, freeitemins;
            double totAmount = 0;
            
            if (txseqnoPaid.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Payment Date Can not empty','Payment Agreement','warning');", true);
                return;
            }

            if (dtpay.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Payment Date Can not empty','Payment Agreement','warning');", true);
                return;
            }

            if (txmanual.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Manual No Invoice Can not empty','Payment Agreement','warning');", true);
                return;
            }

            string cust_typ = bll.vLookUp("select rdcust from tmst_contract where contract_no = '" + txagreecdPaid.Text + "'");
            string cust = "";
            if (cust_typ == "C")
            {
                cust = bll.vLookUp("select top 1 cust_cd from tcontract_customer where contract_no = '" + txagreecdPaid.Text + "'");
            }
            else
            {
                cust = cbcustomer.SelectedValue.ToString();
            }

            // Enh : 22 June 2019 : Customer Transfer Blocked - CIN
            string sCustomerTransferBlock = bll.vLookUp("select dbo.fn_customertransferpending('" + cust + "')");
            if (sCustomerTransferBlock != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused there is pending in customer transfer ','" + sCustomerTransferBlock + "','warning');", true);
                return;
            }

            foreach (GridViewRow row in grditem.Rows)
            {
                TextBox txamountpay = (TextBox)row.FindControl("txamountpay");
                Label lbitempay = (Label)row.FindControl("lbitempay");
                freeitem = lbitempay.Text.Split('_');

                sStock = bll.vLookUp("select isnull(dbo.[sfnGetStockBooking]('" + freeitem[0].ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO',convert(date,(select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'),103)),0)");
                if (Convert.ToDouble(sStock)<0) { sStock = "0"; } //by yanto 17-2-2019 when stock minus change to 0
                //sStock = "101";
                totAmount = Convert.ToDouble(txamountpay.Text);
                if (Convert.ToDouble(sStock) >= Convert.ToDouble(txamountpay.Text))
                {
                    stStock = true;
                }
                else
                {
                    stStock = false;
                    listItem += lbitempay.Text + " ; ";
                    break;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Stock is not enough : " + listItem + " ','Payment Agreement','warning');", true);
                    //return;
                }
            }

            if (totAmount > Convert.ToDouble(txamount.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Maximum Payment is : " + txamount.Text + " CTN.','Payment Agreement','warning');", true);
                return;
            }

            if (stStock == true)
            {
                foreach (GridViewRow row in grditem.Rows)
                {
                    TextBox txamountpay = (TextBox)row.FindControl("txamountpay");
                    Label lbitempay = (Label)row.FindControl("lbitempay");
                    freeitemins = lbitempay.Text.Split('_');
                    //sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + freeitemins[0].ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','0',convert(date,'" + Request.Cookies["waz_dt"].Value.ToString() + "',103))");

                    if (Convert.ToDouble(txamountpay.Text) > 0)
                    {
                        arr.Add(new cArrayList("@contract_no", txagreecdPaid.Text));
                        arr.Add(new cArrayList("@seq", txseqnoPaid.Text));
                        arr.Add(new cArrayList("@paid_dt", DateTime.ParseExact(dtpay.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@amount", txamountpay.Text));
                        arr.Add(new cArrayList("@manual_no", txmanual.Text));
                        arr.Add(new cArrayList("@paid_by", Request.Cookies["usr_id"].Value.ToString()));

                        arr.Add(new cArrayList("@year", DateTime.ParseExact(dtpay.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year));
                        arr.Add(new cArrayList("@month", DateTime.ParseExact(dtpay.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month));
                        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@uom", lbuomPaid.Text));
                        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@vhc_cd", cbvehicle.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@item_cd", freeitemins[0]));
                        arr.Add(new cArrayList("@cust_cd", cust));
                        bll.vInsertContractPayment(arr);
                        arr.Clear();
                    }
                }

                arr.Clear();
                arr.Add(new cArrayList("@contract_no", txagreecdPaid.Text));
                arr.Add(new cArrayList("@paid_dt", DateTime.ParseExact(dtpay.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@manual_no", txmanual.Text));
                arr.Add(new cArrayList("@cust_cd", cust));
                bll.vInsertContractInvoice(arr);

                arr.Clear();
                arr.Add(new cArrayList("@contract_no", txagreecdPaid.Text));
                bll.vBindingGridToSp(ref grdschedule, "sp_contract_payment_schedule", arr);
                txseqnoPaid.Text = bll.vLookUp("select top 1 sequenceno from tcontract_payschedule where contract_no = '" + txagreecdPaid.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
                dtduePaid.Text = bll.vLookUp("select top 1 convert(varchar(10),(case when paymentadj_dt is null then payment_dt else paymentadj_dt end),103) due_dt from tcontract_payschedule where contract_no = '" + txagreecdPaid.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
                dtpay.Text = "";
                txamount.Text = "";
                btpayment.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Stock for this Product" + listItem + ", Not Enough.','warning');", true);
                return;
            }
        }
        else if (btpayment.Text == "Driver Receipt")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbcontract.Text));
            arr.Add(new cArrayList("@dr_dt", DateTime.ParseExact(dtDR.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@driver_cd", cbdriverDR.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vhc_cd", cbvehicleDR.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertContractDriverRcp(arr);
            // Insert into StockCard 
            arr.Clear();
            arr.Add(new cArrayList("@refno", lbcontract.Text));
            arr.Add(new cArrayList("@stockcard_typ", "ONDELIVERYBA"));
            bll.vBatchStockCard(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Driver Receipt", "window.opener.RefreshData('driver');window.close();", true);
        }
        else if (btpayment.Text == "Customer Receipt")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbcontract.Text));
            arr.Add(new cArrayList("@cr_dt", DateTime.ParseExact(dtCR.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            bll.vInsertContractCustRcp(arr);
            // Insert into StockCard 
            arr.Clear();
            arr.Add(new cArrayList("@refno", lbcontract.Text));
            arr.Add(new cArrayList("@stockcard_typ", "SALESBA"));
            bll.vBatchStockCard(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Customer Receipt", "window.opener.RefreshData('customer');window.close();", true);
        }
    }

    protected void cbdriverDR_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", cbdriverDR.SelectedValue.ToString()));
        string sVhc = bll.vLookUp("select vhc_cd from tmst_vehicle where emp_cd='" + cbdriverDR.SelectedValue.ToString() + "'");
        if (sVhc == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Driver has not vehicle setup !','Please setup first in vehicle driver master ','warning');", true);
            return;
        }
        else
        {
            //bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_getbyempcd", "vhc_cd", "vhc_nm", arr);
            //bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ");
            cbvehicleDR.SelectedValue = sVhc;
        }
    }
}