using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_contractpayment : System.Web.UI.Page
{

    cbll bll = new cbll();


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {            
            cbstatus_SelectedIndexChanged(sender, e);
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "elapsis", "elapsis();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dataPopUp", "dataPopUp();", true);

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dataPopUp", "dataPopUp();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);


    }
    protected void grdpayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        Label lbagreecode = (Label)e.Row.FindControl("lbagreecode");


        //string status = bll.vLookUp("select approval from tmst_contract where contract_no = '"+lbagreecode.Text+"'");

        string status = cbstatus.SelectedValue.ToString();

        if (status == "N")
        {
            grdpayment.Columns[10].Visible = false;
            grdpayment.Columns[11].Visible = true;
            grdpayment.Columns[12].Visible = false;
            grdpayment.Columns[13].Visible = false;
            grdpayment.Columns[14].Visible = false;
            grdpayment.Columns[15].Visible = false;
            grdpayment.Columns[16].Visible = false;
            grdpayment.Columns[17].Visible = false;
            grdpayment.Columns[18].Visible = false;
            grdpayment.Columns[19].Visible = false;
            grdpayment.Columns[20].Visible = true;
            grdpayment.Columns[21].Visible = true;
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = false;
            Button btnUpload = (Button)e.Row.FindControl("btnUpload");
            btnUpload.Visible = true;
            Button btnPay = (Button)e.Row.FindControl("btnPay");
            btnPay.Visible = false;
            Button btnPostpone = (Button)e.Row.FindControl("btnPostpone");
            btnPostpone.Visible = false;
            Button btnCustomer = (Button)e.Row.FindControl("btnCustRcp");
            btnCustomer.Visible = false;
            Button btnDriver = (Button)e.Row.FindControl("btnDriverRcp");
            btnDriver.Visible = false;
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = false;
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = false;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = false;
            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
            btnCancel.Visible = false;
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Visible = true;
        }
        if (status == "U")
        {
            grdpayment.Columns[10].Visible = true;
            grdpayment.Columns[11].Visible = false;
            grdpayment.Columns[12].Visible = false;
            grdpayment.Columns[13].Visible = false;
            grdpayment.Columns[14].Visible = false;
            grdpayment.Columns[15].Visible = false;
            grdpayment.Columns[16].Visible = false;
            grdpayment.Columns[17].Visible = true;
            grdpayment.Columns[18].Visible = true;
            grdpayment.Columns[19].Visible = false;
            grdpayment.Columns[20].Visible = false;
            grdpayment.Columns[21].Visible = false;
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            Button btnUpload = (Button)e.Row.FindControl("btnUpload");
            btnUpload.Visible = false;
            Button btnPay = (Button)e.Row.FindControl("btnPay");
            btnPay.Visible = false;
            Button btnPostpone = (Button)e.Row.FindControl("btnPostpone");
            btnPostpone.Visible = false;
            Button btnCustomer = (Button)e.Row.FindControl("btnCustRcp");
            btnCustomer.Visible = false;
            Button btnDriver = (Button)e.Row.FindControl("btnDriverRcp");
            btnDriver.Visible = false;
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = false;
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = true;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = true;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = false;
            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
            btnCancel.Visible = true;
        }
        if (status == "A")
        {
            grdpayment.Columns[10].Visible = true;
            grdpayment.Columns[11].Visible = false;
            grdpayment.Columns[12].Visible = true;
            grdpayment.Columns[13].Visible = true;
            grdpayment.Columns[14].Visible = false;
            grdpayment.Columns[15].Visible = false;
            grdpayment.Columns[16].Visible = false;
            grdpayment.Columns[17].Visible = false;
            grdpayment.Columns[18].Visible = false;
            grdpayment.Columns[19].Visible = true;
            grdpayment.Columns[20].Visible = true;
            grdpayment.Columns[21].Visible = false;
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            Button btnUpload = (Button)e.Row.FindControl("btnUpload");
            btnUpload.Visible = false;
            Button btnPay = (Button)e.Row.FindControl("btnPay");
            btnPay.Visible = true;
            Button btnPostpone = (Button)e.Row.FindControl("btnPostpone");
            btnPostpone.Visible = true;
            Button btnCustomer = (Button)e.Row.FindControl("btnCustRcp");
            btnCustomer.Visible = false;
            Button btnDriver = (Button)e.Row.FindControl("btnDriverRcp");
            btnDriver.Visible = false;
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = false;
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = false;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = false;
            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
            btnCancel.Visible = true;
        } 
        if (status == "DR")
        {
            grdpayment.Columns[10].Visible = true;
            grdpayment.Columns[11].Visible = false;
            grdpayment.Columns[12].Visible = false;
            grdpayment.Columns[13].Visible = false;
            grdpayment.Columns[14].Visible = true;
            grdpayment.Columns[15].Visible = false;
            grdpayment.Columns[16].Visible = true;
            grdpayment.Columns[17].Visible = false;
            grdpayment.Columns[18].Visible = false;
            grdpayment.Columns[19].Visible = true;
            grdpayment.Columns[20].Visible = true;
            grdpayment.Columns[21].Visible = false;
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            Button btnUpload = (Button)e.Row.FindControl("btnUpload");
            btnUpload.Visible = false;
            Button btnPay = (Button)e.Row.FindControl("btnPay");
            btnPay.Visible = false;
            Button btnPostpone = (Button)e.Row.FindControl("btnPostpone");
            btnPostpone.Visible = false;
            Button btnCustomer = (Button)e.Row.FindControl("btnCustRcp");
            btnCustomer.Visible = false;
            Button btnDriver = (Button)e.Row.FindControl("btnDriverRcp");
            btnDriver.Visible = true;
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = true;
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = false;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = true;
            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
            btnCancel.Visible = false;
        }if (status == "CR")
        {
            grdpayment.Columns[10].Visible = true;
            grdpayment.Columns[11].Visible = false;
            grdpayment.Columns[12].Visible = false;
            grdpayment.Columns[13].Visible = false;
            grdpayment.Columns[14].Visible = false;
            grdpayment.Columns[15].Visible = true;
            grdpayment.Columns[16].Visible = true;
            grdpayment.Columns[17].Visible = false;
            grdpayment.Columns[18].Visible = false;
            grdpayment.Columns[19].Visible = true;
            grdpayment.Columns[20].Visible = true;
            grdpayment.Columns[21].Visible = false;
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            Button btnUpload = (Button)e.Row.FindControl("btnUpload");
            btnUpload.Visible = false;
            Button btnPay = (Button)e.Row.FindControl("btnPay");
            btnPay.Visible = false;
            Button btnPostpone = (Button)e.Row.FindControl("btnPostpone");
            btnPostpone.Visible = false;
            Button btnCustomer = (Button)e.Row.FindControl("btnCustRcp");
            btnCustomer.Visible = true;
            Button btnDriver = (Button)e.Row.FindControl("btnDriverRcp");
            btnDriver.Visible = false;
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = true;
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = false;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = true;
            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
            btnCancel.Visible = false;
        }
        else if (status == "C")
        {
            grdpayment.Columns[10].Visible = true;
            grdpayment.Columns[11].Visible = false;
            grdpayment.Columns[12].Visible = false;
            grdpayment.Columns[13].Visible = false;
            grdpayment.Columns[14].Visible = false;
            grdpayment.Columns[15].Visible = false;
            grdpayment.Columns[16].Visible = true;
            grdpayment.Columns[17].Visible = false;
            grdpayment.Columns[18].Visible = false;
            grdpayment.Columns[19].Visible = true;
            grdpayment.Columns[20].Visible = true;
            grdpayment.Columns[21].Visible = false;
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            Button btnUpload = (Button)e.Row.FindControl("btnUpload");
            btnUpload.Visible = false;
            Button btnPay = (Button)e.Row.FindControl("btnPay");
            btnPay.Visible = false;
            Button btnPostpone = (Button)e.Row.FindControl("btnPostpone");
            btnPostpone.Visible = false;
            Button btnCustomer = (Button)e.Row.FindControl("btnCustRcp");
            btnCustomer.Visible = false;
            Button btnDriver = (Button)e.Row.FindControl("btnDriverRcp");
            btnDriver.Visible = false;
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = true;
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = false;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = true;
            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
            btnCancel.Visible = false;
        }
        else if (status == "R")
        {
            grdpayment.Columns[10].Visible = true;
            grdpayment.Columns[11].Visible = false;
            grdpayment.Columns[12].Visible = false;
            grdpayment.Columns[13].Visible = false;
            grdpayment.Columns[14].Visible = false;
            grdpayment.Columns[15].Visible = false;
            grdpayment.Columns[16].Visible = false;
            grdpayment.Columns[17].Visible = false;
            grdpayment.Columns[18].Visible = false;
            grdpayment.Columns[19].Visible = false;
            grdpayment.Columns[20].Visible = false;
            grdpayment.Columns[21].Visible = false;
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            Button btnUpload = (Button)e.Row.FindControl("btnUpload");
            btnUpload.Visible = false;
            Button btnPay = (Button)e.Row.FindControl("btnPay");
            btnPay.Visible = false;
            Button btnPostpone = (Button)e.Row.FindControl("btnPostpone");
            btnPostpone.Visible = false;
            Button btnCustomer = (Button)e.Row.FindControl("btnCustRcp");
            btnCustomer.Visible = false;
            Button btnDriver = (Button)e.Row.FindControl("btnDriverRcp");
            btnDriver.Visible = false;
            Button btnPrint = (Button)e.Row.FindControl("btnPrint");
            btnPrint.Visible = false;
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = false;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = false;
            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
            btnCancel.Visible = false;
        }
        
    }

    protected void grdpayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdpayment.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@search", txsearch.Text.ToString()));
        bll.vBindingGridToSp(ref grdpayment, "sp_contract_payment_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }

    protected void grdpayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "uploadD")
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow row = grdpayment.Rows[index];
        //    Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
        //    FileUpload upl = (FileUpload)row.FindControl("upl");
        //    if (upl.HasFile)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('File Uploaded.','Document Upload','warning');", true);
        //        return;
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No File Uploaded.','Document Upload','warning');", true);
        //        return;
        //    }
        //}
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dataPopUp", "dataPopUp();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "elapsis", "elapsis();", true);
        if(e.CommandName == "upload")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            //lbModalTitleUpload.Text = "Upload Document";
            //txContractUpl.Text = lbagreecode.Text;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myUpload').modal();", true);
            //upModalUpload.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('fm_contractpayment_inv.aspx?no=" + lbagreecode.Text + "&act=upload','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
        }
        else if (e.CommandName == "paynow")
        {
            List<cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            // Cek Customer 
            //string cust = bll.vLookUp("select rdcust from tmst_contract where contract_no = '"+lbagreecode.Text+"'");
            //if (cust == "C")
            //{
            //    customer.Attributes.Add("style", "display:none");
            //    grdschedule.Visible = true;
            //    grdscheduleg.Visible = false;
            //    arr.Clear();
            //    arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            //    bll.vBindingGridToSp(ref grdschedule, "sp_contract_payment_schedule", arr);
            //    txamount.Text = bll.vLookUp("select top 1 qty from tcontract_payschedule where contract_no = '" + lbagreecode.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
            //}
            //else
            //{
            //    customer.Attributes.Remove("style");
            //    grdschedule.Visible = false;
            //    grdscheduleg.Visible = true;
            //    arr.Clear();
            //    arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            //    bll.vBindingComboToSp(ref cbcustomer, "sp_tcontract_cusgrcd_customer", "cust_cd", "cust_nm", arr);
            //    bll.vBindingGridToSp(ref grdscheduleg, "sp_contract_payment_schedule_customer", arr);
            //    txamount.Text = bll.vLookUp("select top 1 (tcontract_payschedule.qty-isnull(tcontract_payment.amount,0)) as qty from tcontract_payschedule left join tcontract_payment on tcontract_payschedule.contract_no = tcontract_payment.contract_no and tcontract_payschedule.sequenceno = tcontract_payment.sequenceno where tcontract_payschedule.contract_no = '"+lbagreecode.Text+"' order by tcontract_payschedule.sequenceno asc"); 
            //}
            //// Cek Customer
            //lbModalTitlePaid.Text = "Payment Agreement";
            //btnPaid.Text = "Add Payment";
            //txagreecdPaid.Text = lbagreecode.Text;
            //txseqnoPaid.Text = bll.vLookUp("select top 1 sequenceno from tcontract_payschedule where contract_no = '" + lbagreecode.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
            //dtduePaid.Text = bll.vLookUp("select top 1 convert(varchar(10),(case when paymentadj_dt is null then payment_dt else paymentadj_dt end),103) due_dt from tcontract_payschedule where contract_no = '" + lbagreecode.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
            //lbuomPaid.Text = bll.vLookUp("select top 1 uom from tcontract_payschedule where contract_no = '" + lbagreecode.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
            //dtduePaid.Attributes.Add("readonly", "readonly");
            //txseqnoPaid.Attributes.Add("readonly", "readonly");
            //txagreecdPaid.Attributes.Add("readonly", "readonly");
            //txamount.Attributes.Add("readonly", "readonly");            
            //arr.Clear();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            //cbwhs_SelectedIndexChanged(sender, e);
            //string sDefBin = bll.vLookUp("select top 1 qry_data from tmap_query where qry_cd='bin_branch'");
            //cbbin.SelectedValue = sDefBin;            
            //arr.Clear();
            //arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            //arr.Add(new cArrayList("@bin", cbbin.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@wh", cbwhs.SelectedValue.ToString()));
            //bll.vBindingGridToSp(ref grditem, "sp_contract_payment_freeitem", arr);
            //arr.Clear();
            //arr.Add(new cArrayList("@qry_cd", "driver"));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            //arr.Clear();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ", arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myPaid').modal();", true);
            //upModalPaid.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('fm_contractpayment_inv.aspx?no=" + lbagreecode.Text + "&act=payment','mywindow','location=1,status=1,scrollbars=1,width=800,height=800,top=75,left=300',true);", true);
        }
        else if (e.CommandName == "postpone")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            lblModalTitle.Text = "Postpone Payment Schedule";
            btSave.Text = "Postpone";
            txagreecd.Text = lbagreecode.Text;
            dtdue.Text = bll.vLookUp("select top 1 convert(varchar(10),(case when paymentadj_dt is null then payment_dt else paymentadj_dt end),103) due_dt from tcontract_payschedule where contract_no = '" + lbagreecode.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
            dtdue.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myPostpone').modal();", true);
            upModal.Update();

        }
        else if (e.CommandName == "driver")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            //lbModalTitleDR.Text = "Driver Receipt";
            //txcontractDR.Text = lbagreecode.Text;
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Clear();
            //arr.Add(new cArrayList("@qry_cd", "driver"));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingComboToSp(ref cbdriverDR, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            //arr.Clear();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingComboToSp(ref cbvehicleDR, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ", arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myDriver').modal();", true);
            //upModalDriver.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('fm_contractpayment_inv.aspx?no=" + lbagreecode.Text + "&act=driver','mywindow','location=1,status=1,scrollbars=1,width=800,height=800,top=75,left=300',true);", true);
        }
        else if (e.CommandName == "customer")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            //lbModalTitleCR.Text = "Customer Receipt";
            //txcontractCR.Text = lbagreecode.Text;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myCustomer').modal();", true);
            //upModalCustomer.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('fm_contractpayment_inv.aspx?no=" + lbagreecode.Text + "&act=customer','mywindow','location=1,status=1,scrollbars=1,width=800,height=800,top=75,left=300',true);", true);
        }
        else if (e.CommandName == "reprint")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            //lbModalTitlePaid.Text = "Reprint Invoice";
            //txagreeprint.Text = lbagreecode.Text;
            //txagreeprint.Attributes.Add("readonly", "readonly");
            //List<cArrayList> arr = new List<cArrayList>();
            //string cust = bll.vLookUp("select rdcust from tmst_contract where contract_no = '" + lbagreecode.Text + "'");
            //if (cust == "C")
            //{
            //    grdscheduleprint.Visible = true;
            //    grdschedulegprint.Visible = false;
            //    arr.Clear();
            //    arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            //    bll.vBindingGridToSp(ref grdscheduleprint, "sp_contract_payment_schedule", arr);
            //}
            //else
            //{
            //    grdscheduleprint.Visible = false;
            //    grdschedulegprint.Visible = true;
            //    arr.Clear();
            //    arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            //    bll.vBindingGridToSp(ref grdschedulegprint, "sp_contract_payment_schedule_customer", arr);
            //}
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myPrint').modal();", true);
            //upModalPrint.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('fm_contractpayment_inv.aspx?no=" + lbagreecode.Text + "&act=reprint','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
        }
        else if (e.CommandName == "approve")
        {
            if (bll.nCheckAccess("contractapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this Bussiness Agreement, contact Administrator !!','warning');", true);
                return;
            }
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdateContractApp(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Contract has been approved','" + lbagreecode.Text + "','success');", true);
            cbstatus.SelectedValue = "U";
            cbstatus_SelectedIndexChanged(sender, e);
        }
        else if (e.CommandName == "reject")
        {
            if (bll.nCheckAccess("contractreject", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To Reject this Bussiness Agreement, contact Administrator !!','warning');", true);
                return;
            }
            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = grdpayment.Rows[index];
            //Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Clear();
            //arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            //arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vUpdateContractRej(arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Contract has been Rejected','" + lbagreecode.Text + "','warning');", true);
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            //lbModalTitleReject.Text = "Reject Business Agreement";
            //txagreeReject.Text = lbagreecode.Text;
            //txagreeReject.Attributes.Add("readonly", "readonly");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReject').modal();", true);
            //upModalReject.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('fm_contractpayment_inv.aspx?no=" + lbagreecode.Text + "&act=reject','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
        }
        else if (e.CommandName == "cncel")
        {
            if (bll.nCheckAccess("contractcancel", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To return this Bussiness Agreement, contact Administrator !!','warning');", true);
                return;
            }
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            arr.Add(new cArrayList("@return_by", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdateContractCancel(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Contract has been Canceled','" + lbagreecode.Text + "','warning');", true);
            
        }
        else if (e.CommandName == "return")
        {
            //if (bll.nCheckAccess("contractreturn", Request.Cookies["usr_id"].Value.ToString()) == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To return this Bussiness Agreement, contact Administrator !!','warning');", true);
            //    return;
            //}
            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = grdpayment.Rows[index];
            //Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            //lbModalTitleReturn.Text = "Return Business Agreement";
            //txagreeReturn.Text = lbagreecode.Text;
            //List<cArrayList> arr = new List<cArrayList>();
            //string cust = bll.vLookUp("select rdcust from tmst_contract where contract_no = '" + lbagreecode.Text + "'");
            //if (cust == "C")
            //{
            //    grdscheduleprint.Visible = true;
            //    grdschedulegprint.Visible = false;
            //    arr.Clear();
            //    arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            //    bll.vBindingGridToSp(ref grdschedulereturn, "sp_contract_payment_schedule", arr);
            //}
            //else
            //{
            //    grdscheduleprint.Visible = false;
            //    grdschedulegprint.Visible = true;
            //    arr.Clear();
            //    arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            //    bll.vBindingGridToSp(ref grdschedulegreturn, "sp_contract_payment_schedule_customer", arr);
            //}
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReturn').modal();", true);
            //upModalReturn.Update();


            if (bll.nCheckAccess("contractreturn", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To delete this Bussiness Agreement, contact Administrator !!','warning');", true);
                return;
            }
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            arr.Add(new cArrayList("@return_by", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdateContractRet(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Contract has been Return','" + lbagreecode.Text + "','warning');", true);
            cbstatus.SelectedValue = "N";
            cbstatus_SelectedIndexChanged(sender, e);
        }
        else if (e.CommandName == "del")
        {
            if (bll.nCheckAccess("contractdel", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To delete this Bussiness Agreement, contact Administrator !!','warning');", true);
                return;
            }
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpayment.Rows[index];
            Label lbagreecode = (Label)grdpayment.Rows[index].FindControl("lbagreecode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
            bll.vDelContract(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Contract has been Delete','" + lbagreecode.Text + "','warning');", true);
            cbstatus.SelectedValue = "N";
            cbstatus_SelectedIndexChanged(sender, e);
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (btSave.Text == "Postpone")
        {
            arr.Add(new cArrayList("@contract_no", txagreecd.Text));
            arr.Add(new cArrayList("@postpone_dt", DateTime.ParseExact(dtpostpone.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            bll.vUpdateContractDate(arr);
            arr.Clear();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myPostpone').modal('hide');", true);
        upModal.Update();
        Response.Redirect(Request.Url.AbsoluteUri);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinv&ba=" + txagreecdPaid.Text + "&no="+lbseqno.Text+"');", true);
        }
        else if (e.CommandName == "reprint")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdscheduleprint.Rows[index];
            Label lbseqno = (Label)grdscheduleprint.Rows[index].FindControl("lbseqno");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinv&ba=" + txagreeprint.Text + "&no=" + lbseqno.Text + "');", true);
        }
    }
    protected void btnPaid_Click(object sender, EventArgs e)
    {
        string sStock = "", listItem = "" ;
        bool stStock = false;
        string[] freeitem, freeitemins;
        double totAmount = 0;
        List<cArrayList> arr = new List<cArrayList>();
        if (btnPaid.Text == "Add Payment")
        {
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

            foreach (GridViewRow row in grditem.Rows)
            {
                TextBox txamountpay = (TextBox)row.FindControl("txamountpay");
                Label lbitempay = (Label)row.FindControl("lbitempay");
                freeitem = lbitempay.Text.Split('_');
                
                sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + freeitem[0].ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','0',convert(date,'" + Request.Cookies["waz_dt"].Value.ToString() + "',103))");
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
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "elapsis", "elapsis();", true);

            }

            if (totAmount > Convert.ToDouble(txamount.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Maximum Payment is : "+txamount.Text+ " CTN.','Payment Agreement','warning');", true);
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
                        arr.Add(new cArrayList("@paid_dt", DateTime.ParseExact(dtpay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@amount", txamountpay.Text));
                        arr.Add(new cArrayList("@manual_no", txmanual.Text));
                        arr.Add(new cArrayList("@paid_by", Request.Cookies["usr_id"].Value.ToString()));

                        arr.Add(new cArrayList("@year", DateTime.ParseExact(dtpay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year));
                        arr.Add(new cArrayList("@month", DateTime.ParseExact(dtpay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month));
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
                bll.vBindingGridToSp(ref grdschedule, "sp_contract_payment_schedule", arr);
                txseqnoPaid.Text = bll.vLookUp("select top 1 sequenceno from tcontract_payschedule where contract_no = '" + txagreecdPaid.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
                dtduePaid.Text = bll.vLookUp("select top 1 convert(varchar(10),(case when paymentadj_dt is null then payment_dt else paymentadj_dt end),103) due_dt from tcontract_payschedule where contract_no = '" + txagreecdPaid.Text + "' and paycont_sta_id = 'N' order by sequenceno asc");
                dtpay.Text = "";
                txamount.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myPaid').modal('hide');", true);
                //upModalPaid.Update();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Stock for this Product" + listItem + ", Not Enough.','warning');", true);
                return;
            } 

                       
        }        
    }

    protected void close_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
        upModalPaid.Update();
    }
    protected void btUpload_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
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
                    arr.Add(new cArrayList("@contract_no", txContractUpl.Text));
                    arr.Add(new cArrayList("@fileloc", txContractUpl.Text.ToString() + ext));
                    upl.SaveAs(bll.sGetControlParameter("image_path") + "/contract_doc/" + txContractUpl.Text.ToString() + ext);
                    bll.vUpdateContractDoc(arr);
                    Random rnd = new Random();
                    int token = rnd.Next(1000, 9999);
                    string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + token.ToString();
                    //Send Approval SMS
                    string sSPN = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    string sPropNo = bll.vLookUp("select prop_No from tmst_contract where contract_no='" + txContractUpl.Text + "'");
                    string sCust = bll.vLookUp("select mc.cust_cd+'-'+mc.cust_nm from tcontract_customer tc left join tmst_customer mc on tc.cust_cd = mc.cust_cd and tc.salespointcd=mc.salespointcd where tc.contract_no='" + txContractUpl.Text + "'");
                    List<string> lapproval = bll.lGetApproval("contractapp", 1);
                    string sSMS = "#Business Agreement (" + txContractUpl.Text + ") Prop No : " + sPropNo.ToString() + ", BRN:" + sSPN + ", Customer : " + sCust.ToString() + " ,pls reply with (Y/N)" + stoken;        // cd.vSendSms(sSMS, lapproval[0]);
                    arr.Clear();
                    //arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@token", stoken));
                    arr.Add(new cArrayList("@doc_typ", "contractapp"));
                    arr.Add(new cArrayList("@to", lapproval[0]));
                    arr.Add(new cArrayList("@doc_no", txContractUpl.Text));
                    arr.Add(new cArrayList("@msg", sSMS));
                    bll.vInsertSmsOutbox(arr);
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
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "A")
        {
            if (bll.vLookUp("select dbo.fn_gettransblock()") != "ok")
            {
                Response.Redirect("alert_denied.aspx?m=1");
                return;
            }
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@search", txsearch.Text.ToString()));
        bll.vBindingGridToSp(ref grdpayment, "sp_contract_payment_get", arr);
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
            bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_getbyempcd", "vhc_cd", "vhc_nm", arr);
            //bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ");
            cbvehicle.SelectedValue = sVhc;
        }
        upModal.Update();
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
            bll.vBindingComboToSp(ref cbvehicleDR, "sp_tmst_vehicle_getbyempcd", "vhc_cd", "vhc_nm", arr);
            //bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_typ");
            cbvehicleDR.SelectedValue = sVhc;
        }
        upModalDriver.Update();
    }

    protected void btnDR_Click(object sender, EventArgs e)
    {
        if (dtDR.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Driver Receipt Date Can not empty','Driver Receipt','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", txcontractDR.Text));
        arr.Add(new cArrayList("@dr_dt", DateTime.ParseExact(dtDR.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@driver_cd", cbdriverDR.SelectedValue.ToString()));
        arr.Add(new cArrayList("@vhc_cd", cbvehicleDR.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
        bll.vInsertContractDriverRcp(arr);
        arr.Clear();
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnCR_Click(object sender, EventArgs e)
    {
        if (dtCR.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Receipt Date Can not empty','Customer Receipt','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", txcontractCR.Text));
        arr.Add(new cArrayList("@cr_dt", DateTime.ParseExact(dtCR.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInsertContractCustRcp(arr);
        arr.Clear();
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //if (btSave.Text == "Reject")
        //{
            arr.Add(new cArrayList("@contract_no", txagreeReject.Text));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remarks", txremarkReject.Text));
            bll.vUpdateContractRej(arr);
            arr.Clear();
        //}
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReject').modal('hide');", true);
        upModalReject.Update();
        Response.Redirect(Request.Url.AbsoluteUri);
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Clear();
        //arr.Add(new cArrayList("@contract_no", lbagreecode.Text));
        //arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vUpdateContractRej(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Contract has been Rejected','" + txagreeReject.Text + "','warning');", true);    
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

    protected void grdscheduleg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdscheduleg.Rows[index];
            Label lbseqno = (Label)grdscheduleg.Rows[index].FindControl("lbseqno");
            Label lbcustomer = (Label)grdscheduleg.Rows[index].FindControl("lbcustomer");
            string[] cust_cd = lbcustomer.Text.Split('_');
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinvg&ba=" + txagreecdPaid.Text + "&no=" + lbseqno.Text + "&cust=" + cust_cd[0].ToString() + "');", true);
        }
        else if (e.CommandName == "reprint")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdschedulegprint.Rows[index];
            Label lbseqno = (Label)grdschedulegprint.Rows[index].FindControl("lbseqno");
            Label lbcustomer = (Label)grdschedulegprint.Rows[index].FindControl("lbcustomer");
            string[] cust_cd = lbcustomer.Text.Split('_');
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contractinvg&ba=" + txagreeprint.Text + "&no=" + lbseqno.Text + "&cust=" + cust_cd[0].ToString() + "');", true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        cbstatus_SelectedIndexChanged(sender, e);
    }

    protected void btrefresh_Click(object sender, EventArgs e)
    {
        if (lhStatus.Value == "upload")
        {
            cbstatus.SelectedValue = "N";
            cbstatus_SelectedIndexChanged(sender, e);
        }
        else if (lhStatus.Value == "reject")
        {
            cbstatus.SelectedValue = "U";
            cbstatus_SelectedIndexChanged(sender, e);
        }
        else if (lhStatus.Value == "payment")
        {
            cbstatus.SelectedValue = "A";
            cbstatus_SelectedIndexChanged(sender, e);
        }
        else if (lhStatus.Value == "driver")
        {
            cbstatus.SelectedValue = "DR";
            cbstatus_SelectedIndexChanged(sender, e);
        }
        else if (lhStatus.Value == "customer")
        {
            cbstatus.SelectedValue = "CR";
            cbstatus_SelectedIndexChanged(sender, e);
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        string period = "";

        if (bll.nCheckAccess("contractreturn", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To return this Bussiness Agreement, contact Administrator !!','warning');", true);
            return;
        }

        period = bll.vLookUp("select (case when month(payment_dt) <> month(convert(date,(select dbo.fn_getcontrolparameter('wazaran_dt')),103)) then 'false' else 'true' end) status from tcontract_payment where contract_no='" + txagreeReturn.Text + "'");
        if (period == "false" || period == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To return this Bussiness Agreement, because different Period !!','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", txagreeReturn.Text));
        arr.Add(new cArrayList("@return_by", Request.Cookies["usr_id"].Value.ToString()));
        bll.vUpdateContractRet(arr);
        arr.Clear();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReturn').modal('hide');", true);
        upModalReturn.Update();
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}