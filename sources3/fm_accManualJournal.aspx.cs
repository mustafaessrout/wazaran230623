using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_accManualJournal : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    decimal totalDebit = 0;
    decimal totalCredit= 0;
    public static int PreviousIndex;

    //temporary for testing, should be uncomment false
    // ||
    // VV
    //Boolean hasApprovalRole = false;
    Boolean hasApprovalRole = true;

    string userid = null;
    //string userid2 = null;
    string idJournal="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string userid = Request.Cookies["usr_id"].Value.ToString();
            //string userid = "2833";

            //userid2 = bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'");
            //if (bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'") != "")
            if (bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'").ToString() == "" |
                bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'").ToString() == null)
            {
                hasApprovalRole = false;
                btapprove.Visible = false;
            }

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            //dttrnstkDate.Text = Request.Cookies["waz_dt"].Value.ToString();
            dttrnstkDate.Text = string.Format("{0:d/M/yyyy}", DateTime.Now);
            bll.vBindingComboToSp(ref cbJournalTranType, "sp_tacc_journaltrantype_get", "journaltrantypecd", "journaltrantype_desc");
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value.ToString();
            bll.vBindingFieldValueToCombo(ref cbUOM, "uom");

            //bindinggrddoc();

            btNewDetailRow.Visible = false;
            newDetailRow.Visible = false;
            btsave.Visible = false;
            btapprove.Visible = false;
            btdelete.Visible = false;
            btreturn.Visible = false;


            idJournal = Request.QueryString["idJournal"];

            if (idJournal == "" | idJournal==null)
            {
                getLatestJournalNo();
            }
            else
            {
                txJournalId.Text = idJournal;
                dttrnstkDate.Text = bll.vLookUp("select FORMAT(tran_date,'dd/MM/yyyy')tran_date from tacc_journal where id_journal='" + idJournal + "';");
                txtrnstkRemark.Text = bll.vLookUp("select description from tacc_journal where id_journal='" + idJournal + "';");
                txJournalStatus.Text = bll.vLookUp("select case when j.status is NULL then 'Unsaved' when j.status='N'then 'Unsaved, Not Balance' when j.status='P'then 'Posted'when j.status='U'then 'Unposted'when j.status='D'then 'Deleted'end journal_status from tacc_journal j where id_journal='" + idJournal + "';");
                cbJournalTranType.SelectedValue = bll.vLookUp("select j.tran_type+' | '+t.tran_type_nm,* from tacc_journal j left join tacc_journal_tran_type t on j.tran_type=t.tran_type_cd where id_journal='" + idJournal + "';");
                cbJournalTranType.SelectedItem.Text = bll.vLookUp("select j.tran_type+' | '+t.tran_type_nm,* from tacc_journal j left join tacc_journal_tran_type t on j.tran_type=t.tran_type_cd where id_journal='" + idJournal + "';");
                cbJournalTranType.Enabled = false;

                if (hasApprovalRole)
                {
                    btapprove.Visible = true;
                    btreturn.Visible = true;

                    //temporary for testing, shoould be uncomment false
                    // ||
                    // VV
                    //btNewDetailRow.Visible = false;

                    dttrnstkDate.Enabled = false;
                    txtrnstkRemark.Enabled = false;

                    //LinkButton deleteCommand = (LinkButton)grd.Rows[0].FindControl("ShowDeleteButton");
                    //deleteCommand.Visible = false;
                    grd.Columns[7].Visible = false;

                }
                else
                {
                    btsave.Visible = true;
                    btdelete.Visible = true;
                    btNewDetailRow.Visible = true;
                }
            }

            //if (btsave_Click.Caption=="S")
            if (txJournalStatus.Text == "Deleted")
            {
                btsave.Text = "Cancel Delete";
            }
            else
            {
                btsave.Text = "Save and Send";
            }

            bindinggrd();

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void cbjournaltrantype_SelectedIndexChanged(object sender, EventArgs e)
    {
        getLatestJournalNo();
        if (cbJournalTranType.SelectedValue == "" && idJournal == "")
        {
            btNewDetailRow.Visible = false;
            btsave.Visible = false;
            btapprove.Visible = false;
        }
        else {
            btNewDetailRow.Visible = true;

            if (bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'").ToString() == "" |
                bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'").ToString() == null)
            {
                hasApprovalRole = false;
                btapprove.Visible = false;
            }

            if (hasApprovalRole)
            {
                btapprove.Visible = true;

                //temporary for testing, shoould be uncomment false
                // ||
                // VV
                //btNewDetailRow.Visible = false;

                dttrnstkDate.Enabled = false;
                txtrnstkRemark.Enabled = false;

                //LinkButton deleteCommand = (LinkButton)grd.Rows[0].FindControl("ShowDeleteButton");
                //deleteCommand.Visible = false;
                grd.Columns[7].Visible = false;

            }
            else
            {
                btsave.Visible = true;
            }
        }

        dttrnstkDate.Text = string.Format("{0:d/M/yyyy}", DateTime.Now);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (grd.Rows.Count == 1)
            {
                Button b = (Button)e.Row.Cells[6].Controls[2];
                b.Visible = false;
            }
        }
    }
    private void getLatestJournalNo()
    {
        if (cbJournalTranType.SelectedValue == "")
        {
            txJournalId.Text = "";
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@firstprefix", cbJournalTranType.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@field", "id_journal"));
            txJournalId.Text = bll.vGetLatestJournalNo(arr).ToString();

            arr.Clear();
            arr.Add(new cArrayList("@firstprefix", cbJournalTranType.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@field", "tran_date"));
            dttrnstkDate.Text = bll.vGetLatestJournalNo(arr).ToString();

            arr.Clear();
            arr.Add(new cArrayList("@firstprefix", cbJournalTranType.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@field", "description"));
            txtrnstkRemark.Text = bll.vGetLatestJournalNo(arr).ToString();

            arr.Clear();
            arr.Add(new cArrayList("@firstprefix", cbJournalTranType.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@field", "status"));
            txJournalStatus.Text = bll.vGetLatestJournalNo(arr).ToString();
        }

        bindinggrd();
    
    }
    private void bindinggrd()
    {
        totalDebit = 0;
        totalCredit = 0;
        string trnstkNo;
        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@JournalId", txJournalId.Text));

        //arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_journalDet_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    //private void bindinggrddoc()    
    //{
        
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
    //    arr.Add(new cArrayList("@JournalId", txJournalId.Text));
    //    bll.vBindingGridToSp(ref grddoc, "sp_tacc_document_get", arr);
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    //}
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        string sstatus;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGettbltrnstock(arr, ref rs);
        while (rs.Read())
        {
            string sdate = string.Format("{0:d/M/yyyy}", rs["trnstkDate"]);
            DateTime dtdate = Convert.ToDateTime(sdate);
            string strDate = dtdate.ToString("d/M/yyyy");
            sstatus = rs["sta_id"].ToString();
            dttrnstkDate.Text = strDate;//string.Format("{0:d/M/yyyy}", rs["trnstkDate"]);
            cbSalesPointCD.SelectedValue = rs["salespointcd"].ToString();
            txtrnstkRemark.Text = rs["trnstkRemark"].ToString();
            //chclaim.Checked = Convert.ToBoolean(rs["claim"].ToString());

            //Panel1.Visible = true;
            //bindinggrddoc();
            //{
            //    Panel1.Visible = false;
            //}

            bindinggrd();
        } rs.Close();
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_accManualJournal.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DateTime dtTran = DateTime.ParseExact(dttrnstkDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dtTranStr = dtTran.Year.ToString() + "-" + dtTran.Month.ToString("00") + "-" + dtTran.Day.ToString("00");

        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@journalid", txJournalId.Text));
        arr.Add(new cArrayList("@trandate", dtTranStr));
        arr.Add(new cArrayList("@description", txtrnstkRemark.Text));
        bll.vSavemanualjournal(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Save journal is successfull','','info');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txJournalId.Text);

        Response.Redirect("fm_accManualJournal_List.aspx");

        //if (grd.Rows.Count == 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Item In Detail','Please Add Item In the Detail','warning');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //    return; 
        //}

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be save','error');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //return;
       
        //if (txtrnstkRemark.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please entry remark','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
           
        //    return;
        //}
        
        //string strnstkNo = "";
        //List<cArrayList> arr = new List<cArrayList>();
        ////arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //arr.Add(new cArrayList("@trnstkDate", DateTime.ParseExact(dttrnstkDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@trnstkRemark", txtrnstkRemark.Text));
        //arr.Add(new cArrayList("@createdtby", Request.Cookies["usr_id"].Value.ToString()));
        ////arr.Add(new cArrayList("@claim", chclaim.Checked));
        //bll.vInserttbltrnstock(arr, ref strnstkNo);
        //// send email
        //string sinvtypename;
        ////if  (cbinvtype.SelectedValue=="12")
        ////{ 
            
        ////List<string> lInfo = bll.ltrnstockApproval("trnstockApproval");
        //List<string> lapproval = bll.lGetApproval(sinvtypename, 1);
        ////Random rnd = new Random();
        ////int nRnd = rnd.Next(1000, 9999);
        //string ssp = Request.Cookies["sp"].Value.ToString();
        //string ssp1 = ssp.Substring(0, 1);
        //string ssp2 = ssp.Substring(0, 1) + ssp.Substring(ssp.Length - 1);
        //string strantoken = "00" + bll.vLookUp("select fld_valu from tfield_value where fld_nm='trantoken' and fld_desc='internaltransfer'");
        //string sMsg = sinvtypename + " " + cbSalesPointCD.SelectedItem.Text + ", no. , detail item in email, do you want to approved : (Y/N)" ;
        //// cd.vSendSms(sMsg, lapproval[0]); 
        ////arr.Clear();
        ////arr.Add(new cArrayList("@token", nRnd.ToString()));
        ////arr.Add(new cArrayList("@doc_typ", "destroy"));
        ////arr.Add(new cArrayList("@to", lapproval[0]));
        ////arr.Add(new cArrayList("@doc_no", txtrnstkNo.Text));
        ////arr.Add(new cArrayList("@msg", sMsg.TrimEnd() + nRnd.ToString()));
        ////bll.vInsertSMSSent(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@msg", sMsg));
        //arr.Add(new cArrayList("@doc_typ", sinvtypename));
        //arr.Add(new cArrayList("@to", lapproval[0]));
        //bll.vInsertSmsOutbox(arr); //by yanto 15-11-2016

        ////string slink_ho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_ho'");
        //string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
        //String sText = "<html><head><body>Dear, New stock " + sinvtypename + " has been created , with no. "+
        //    "<p> Date  : " + dttrnstkDate.Text + " Salespoint : " + cbSalesPointCD.SelectedItem + 
        //    //"<tr><td>Warehouse </td><td>:</td><td>" + cbwhs_cd.SelectedItem.Text + "</td>" +
        //        //"<tr<td>Note </td><td>:</td><td>" + txtrnstkRemark.Text + </td></tr>
        //        "<p> for detail please see attached file </p>" +
        //        "<p>Please Click this  for approved : <a href='" + slink_branch + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&RefNo= &appcode= &sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedItem + "&RefNo= &appcode= &sta=L'>Reject</a></p>";
        ////"<p>Please Click this  for approved : <a href='" + slink_ho + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_ho + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>";
        //        //<table><tr><td>Item Cd</td><td>Item Name</td><td>Size</td><td>Branded</td><td>Qty</td><td>Uom</td><td>Unit Price</td><td>Amount</td></tr>";
        ////string sItem = "";
        ////System.Data.SqlClient.SqlDataReader rs = null;
        ////arr.Clear();
        ////arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
        ////arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        ////arr.Add(new cArrayList("@invtype", cbinvtype.SelectedValue));
        ////bll.vGettbltrnstockDtl(arr, ref rs);
        ////while (rs.Read())
        ////{
        ////    sItem += "<tr><td>" + rs["item_cd"].ToString() + "</td><td>" + rs["item_nm"] + "</td><td>" + rs["size"].ToString() + "</td><td>" + rs["branded_nm"].ToString() + "</td><td>" + rs["qty"].ToString() + "</td><td>" + rs["uom"].ToString() + "</td><td>" + rs["unitprice"].ToString() + "</td><td>" + rs["Amount"].ToString() + "</td></tr>";
        ////}
        ////rs.Close();
        ////sText += sItem + "</table><p>Please Click this  for approved : <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?trnname='trnstock'&salespointcd='" + cbSalesPointCD.SelectedValue + "'&appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?trnname='trnstock'&salespointcd='" + cbSalesPointCD.SelectedValue + "'&appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>";

        ////sText += "</br></br>Note : If you not take action , within 3 days this  will be disabled.";
        ////"<p>Please Click this  for approved : <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>";
        //List<string> lapproval2 = bll.lGetApprovalall(sinvtypename);
        //int n = sText.Length;
        //string sPath = bll.sGetControlParameter("image_path");
        //arr.Clear();
        //arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
        //string sSubject = "New " + sinvtypename + " Has Been Created";
        //string semail = "0";
        //int i = 0;
        //foreach (var email in lapproval2.Skip(1))
        //{
        //    i = i + 1;
        //    if (i % 2 != 0)
        //    {
        //        if (semail == "0")
        //            semail = email;
        //        else
        //            semail = semail + ';' + email;
        //    }
        //}
        //arr.Clear();
        //arr.Add(new cArrayList("@doc_typ", sinvtypename));
        //arr.Add(new cArrayList("@to", semail));
        //arr.Add(new cArrayList("@emailsubject", sSubject));
        //arr.Add(new cArrayList("@msg", sText));
        //bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017

        //arr.Clear();
        //arr.Add(new cArrayList("@trxcd", "stock " + sinvtypename));
        //bll.vInsertEmailSent(arr);
        ////}
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','Tran No. ,'info');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
       
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string userid = Request.Cookies["usr_id"].Value.ToString();
        arr.Add(new cArrayList("@user", userid));
        arr.Add(new cArrayList("@journalid", txJournalId.Text));
        //int jdetid=0;
        bll.vDeletemanualjournal(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txJournalId.Text);

        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        ////return;

        ////List<cArrayList> arr = new List<cArrayList>();
        ////arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        ////bll.vDeletetbltrnstock(arr);
        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        ////Response.Redirect("fm_accManualJournal.aspx");

        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@journalid", txJournalId.Text.ToString()));
        //arr.Add(new cArrayList("@journaldetid", lblseqno.Text.));
        //bll.vDeletemanualjournal(arr);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //Response.Redirect("fm_accManualJournal.aspx");

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void btreturn_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@journalid", txJournalId.Text));
        //int jdetid=0;
        bll.vReturnmanualjournal(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txJournalId.Text);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Return journal is successfull','','info');", true);

        Response.Redirect("fm_accManualJournal_List.aspx");
    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@journalid", txJournalId.Text));
        bll.vApprovemanualjournal(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txJournalId.Text);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Approve journal is successfull','','info');", true);

        Response.Redirect("fm_accManualJournal_List.aspx");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
        Session["lParamtrnstk"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=trnstk');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be save','error');", true);
        //return;

        if (cbJournalTranType.SelectedItem.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please fill Journal Transaction Type field !','warning');", true);

            return;
        }
        if (txdescription.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please fill Description field !','warning');", true);

            return;
        }
        if (txsearchaccount.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please fill Account field !','warning');", true);

            return;
        }
        if (txdebit.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please fill Debit field !','warning');", true);

            return;
        }
        if (txcredit.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please fill Credit field !','warning');", true);

            return;
        }
        List<cArrayList> arr = new List<cArrayList>();


        //char[] delimiterChars = { '|', ':' };
        char[] delimiterChars = { '|' };
        Dictionary<int, string> dictionary = new Dictionary<int, string>();

        //1
        string searchbank = null;


        string[] strsearchbank = txsearchbank.Text.ToString().Split(delimiterChars);

        int i = 1;
        dictionary.Clear();
        foreach (string s in strsearchbank)
        {
            dictionary.Add(i, s);
            i = i + 1;
        }
        if (dictionary.ContainsKey(1) && dictionary.ContainsKey(2))
        {
            searchbank = dictionary[1].ToString().Substring(0, dictionary[1].ToString().Length - 1);
        }
        else if (dictionary.ContainsKey(1) && !dictionary.ContainsKey(2))
        {
            searchbank = "";
        }



        //2
        string searchsupplier = null;

        string[] strsearchsupplier = txsearchsupplier.Text.ToString().Split(delimiterChars);

        i = 1;
        dictionary.Clear();
        foreach (string s in strsearchsupplier)
        {
            dictionary.Add(i, s);
            i = i + 1;
        }
        if (dictionary.ContainsKey(1) && dictionary.ContainsKey(2))
        {
            searchsupplier = dictionary[1].ToString().Substring(0, dictionary[1].ToString().Length - 1);
        }
        else if (dictionary.ContainsKey(1) && !dictionary.ContainsKey(2))
        {
            searchsupplier = "";
        }


        //3
        string searchsalesman = null;

        string[] strsearchsalesman = txsearchsalesman.Text.ToString().Split(delimiterChars);

        i = 1;
        dictionary.Clear();
        foreach (string s in strsearchsalesman)
        {
            dictionary.Add(i, s);
            i = i + 1;
        }
        if (dictionary.ContainsKey(1) && dictionary.ContainsKey(2))
        {
            searchsalesman = dictionary[1].ToString().Substring(0, dictionary[1].ToString().Length - 1);
        }
        else if (dictionary.ContainsKey(1) && !dictionary.ContainsKey(2))
        {
            searchsalesman = "";
        }


        //4
        string searchcustomer = null;

        string[] strsearchcustomer = txsearchcustomer.Text.ToString().Split(delimiterChars);

        i = 1;
        dictionary.Clear();
        foreach (string s in strsearchcustomer)
        {
            dictionary.Add(i, s);
            i = i + 1;
        }
        if (dictionary.ContainsKey(1) && dictionary.ContainsKey(2))
        {
            searchcustomer = dictionary[1].ToString().Substring(0, dictionary[1].ToString().Length - 1);
        }
        else if (dictionary.ContainsKey(1) && !dictionary.ContainsKey(2))
        {
            searchcustomer = "";
        }


        //5
        string searchdepartment = null;

        string[] strsearchdepartment = txsearchdepartment.Text.ToString().Split(delimiterChars);

        i = 1;
        dictionary.Clear();
        foreach (string s in strsearchdepartment)
        {
            dictionary.Add(i, s);
            i = i + 1;
        }
        if (dictionary.ContainsKey(1) && dictionary.ContainsKey(2))
        {
            searchdepartment = dictionary[1].ToString().Substring(0, dictionary[1].ToString().Length - 1);
        }
        else if (dictionary.ContainsKey(1) && !dictionary.ContainsKey(2))
        {
            searchdepartment = "";
        }


        //6
        string searchitem = null;

        string[] strsearchitem = txsearchitem.Text.ToString().Split(delimiterChars);

        i = 1;
        dictionary.Clear();
        foreach (string s in strsearchitem)
        {
            dictionary.Add(i, s);
            i = i + 1;
        }
        if (dictionary.ContainsKey(1) && dictionary.ContainsKey(2))
        {
            searchitem = dictionary[1].ToString().Substring(0, dictionary[1].ToString().Length - 1);
        }
        else if (dictionary.ContainsKey(1) && !dictionary.ContainsKey(2))
        {
            searchitem = "";
        }

        //7
        string searchaccount = null;

        string[] strsearchaccount = txsearchaccount.Text.ToString().Split(delimiterChars);

        i = 1;
        dictionary.Clear();
        foreach (string s in strsearchaccount)
        {
            dictionary.Add(i, s);
            i = i + 1;
        }
        if (dictionary.ContainsKey(1) && dictionary.ContainsKey(2))
        {
            searchaccount = dictionary[1].ToString().Substring(0, dictionary[1].ToString().Length - 1);
        }
        else if (dictionary.ContainsKey(1) && !dictionary.ContainsKey(2))
        {
            searchaccount = "";
        }

        idJournal = txJournalId.Text;

        arr.Add(new cArrayList("@journalid", idJournal));
        arr.Add(new cArrayList("@journaltrantype", cbJournalTranType.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        string dateText = dttrnstkDate.Text.ToString().Trim();
        string dateTextDay = "";
        string dateTextMonth = "";
        string dateTextYear = "";
        //dd
        if (dateText.Substring(1, 1) == "/") {
            dateTextDay = "0".ToString() + dateText.Substring(0, 1);
        }
        else if (dateText.Substring(2, 1) == "/")
        {
            dateTextDay = dateText.Substring(0, 2);
        };
        //MM
        if (dateText.Substring(1, 1) == "/" && dateText.Substring(3, 1) == "/")
        {
            dateTextMonth = "0".ToString() + dateText.Substring(2, 1);
        }
        else if (dateText.Substring(2, 1) == "/" && dateText.Substring(4, 1) == "/")
        {
            dateTextMonth = "0".ToString() + dateText.Substring(3, 1);
        }
        else if (dateText.Substring(1, 1) == "/" && dateText.Substring(4, 1) == "/")
        {
            dateTextMonth = dateText.Substring(2, 2);
        }
        else if (dateText.Substring(2, 1) == "/" && dateText.Substring(5, 1) == "/")
        {
            dateTextMonth = dateText.Substring(3, 2);
        };
        int dateTextLength = dateText.Length;
        //yyyy
        dateTextYear = dateText.Substring (dateTextLength - 4, 4);

        string dateTextConverted = dateTextYear.ToString() + '-' + dateTextMonth.ToString() + '-' + dateTextDay.ToString();
        DateTime dateTextReConverted = DateTime.ParseExact(dateTextConverted +" 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

        //arr.Add(new cArrayList("@trandate", DateTime.ParseExact(dttrnstkDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@trandate", dateTextConverted));
        arr.Add(new cArrayList("@module", "MANUALJOURNAL"));
        arr.Add(new cArrayList("@description", txtrnstkRemark.Text));

        arr.Add(new cArrayList("@bank", searchbank));
        arr.Add(new cArrayList("@supplier", searchsupplier));
        arr.Add(new cArrayList("@salesman", searchsalesman));
        arr.Add(new cArrayList("@customer", searchcustomer));
        arr.Add(new cArrayList("@department", searchdepartment));
        arr.Add(new cArrayList("@item", searchitem));
        decimal qtyNum; if (!decimal.TryParse(Convert.ToString(txqty.Text), out qtyNum)) { qtyNum = 0; } else { qtyNum = decimal.Parse(txqty.Text); }
        arr.Add(new cArrayList("@qty", qtyNum));
        arr.Add(new cArrayList("@uom", cbUOM.Text));
        decimal unitpriceNum; if (!decimal.TryParse(Convert.ToString(txunitprice.Text), out unitpriceNum)) { unitpriceNum = 0; } else { unitpriceNum = decimal.Parse(txunitprice.Text); }
        arr.Add(new cArrayList("@unitprice", unitpriceNum));
        decimal totpriceNum;if (!decimal.TryParse(Convert.ToString(txtotprice.Text), out totpriceNum)) { totpriceNum = 0; } else { totpriceNum = decimal.Parse(txtotprice.Text); }
        arr.Add(new cArrayList("@totprice", totpriceNum));
        arr.Add(new cArrayList("@description2", txdescription.Text));
        arr.Add(new cArrayList("@account", searchaccount));
        decimal debitNum; if (!decimal.TryParse(Convert.ToString(txdebit.Text), out debitNum)) { debitNum = 0; } else { debitNum = decimal.Parse(txdebit.Text); }
        arr.Add(new cArrayList("@debit", debitNum));
        decimal creditNum; if (!decimal.TryParse(Convert.ToString(txcredit.Text), out creditNum)) { creditNum = 0; } else { creditNum = decimal.Parse(txcredit.Text); }
        arr.Add(new cArrayList("@credit", creditNum));

        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));

        //bll.vInserttbltrnstockDtl(arr);
        bll.vInsertmanualjournal(arr);
        bindinggrd();
        txsearchbank.Text = "";
        txsearchsupplier.Text = "";
        txsearchsalesman.Text = "";
        txsearchcustomer.Text = "";
        txsearchdepartment.Text = "";
        txsearchitem.Text = "";
        txqty.Text = "";
        hditem.Value = "";
        cbUOM.Text = "";
        txqty.Text = "";
        txunitprice.Text = "";
        txtotprice.Text = "";
        txdescription.Text = "";
        txsearchaccount.Text = "";
        txdebit.Text = "";
        txcredit.Text = "";
        txsearchbank.Focus();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Add journal detail row is successfull','','info');", true);

        updateText(txJournalId.Text);

    }
    protected void btaddNewDetailRow_Click(object sender, EventArgs e)
    {
        newDetailRow.Visible = true;
    }
    protected void btCancelAddRow_Click(object sender, EventArgs e)
    {
        newDetailRow.Visible = false;
    }
    protected void cbwhs_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue.ToString()));
        //bll.vBindingComboToSp(ref cbbin_cd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txtqty");
        DropDownList cboUOM = (DropDownList)grd.Rows[e.RowIndex].FindControl("cboUOM");
        Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
        Label lbltrnstkNo = (Label)grd.Rows[e.RowIndex].FindControl("lbltrnstkNo");
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@trnstkNo", lbltrnstkNo.Text));
        arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@qty", txtqty.Text));
        arr.Add(new cArrayList("@uom", cboUOM.SelectedValue));
        bll.vUpdatetbltrnstockDtl(arr);
        grd.EditIndex = -1; arr.Clear();
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
        //return;
        ////if (Request.Cookies["waz_dt"].Value.ToString() != dttrnstkDate.Text)
        ////{
        ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
        ////    return;
        ////}
        //Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
        //Label lbltrnstkNo = (Label)grd.Rows[e.RowIndex].FindControl("lbltrnstkNo");
        //Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@trnstkNo", lbltrnstkNo.Text));
        //arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
        //arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        //bll.vDeletetbltrnstockDtl(arr);
        //bindinggrd();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        Label lblseqno = (Label)grd.Rows[e.RowIndex].FindControl("lblseqno");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@journalid", txJournalId.Text));
        arr.Add(new cArrayList("@journaldetid", lblseqno.Text));
        //int jdetid=0;
        //arr.Add(new cArrayList("@journaldetid", int.TryParse(Convert.ToString(lblseqno.Text),out jdetid)));
        bll.vDeletemanualjournaldet(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Info','Row Data Deleted successfully !','success');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txJournalId.Text);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be edit','error');", true);
        //return;

        //Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
        //grd.EditIndex = e.NewEditIndex;
        //bindinggrd();
        //DropDownList cboUOM = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cboUOM");
        //bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
        //cboUOM.SelectedValue = lblUOM.Text;
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be edit','error');", true);
        return;

        Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
        grd.EditIndex = e.NewEditIndex;
        bindinggrd();
        DropDownList cboUOM = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cboUOM");
        bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
        cboUOM.SelectedValue = lblUOM.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.PageIndex = e.NewPageIndex;
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }



    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionBankList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sBank = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lBank = new List<string>();
        arr.Add(new cArrayList("@bank", prefixText));
        bll.vSearchMstBank(arr, ref rs);
        while (rs.Read())
        {
            sBank = AutoCompleteExtender.CreateAutoCompleteItem(rs["acc_no"].ToString() + " | " + rs["bank_nm"].ToString(), rs["acc_no"].ToString());
            lBank.Add(sBank);
        } rs.Close();
        return (lBank.ToArray());
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionSupplierList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sSupplier = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lSupplier = new List<string>();
        arr.Add(new cArrayList("@supplier", prefixText));
        bll.vSearchMstSupplier(arr, ref rs);
        while (rs.Read())
        {
            sSupplier = AutoCompleteExtender.CreateAutoCompleteItem(rs["vendor_cd"].ToString() + " | " + rs["vendor_nm"].ToString(), rs["vendor_cd"].ToString());
            lSupplier.Add(sSupplier);
        } rs.Close();
        return (lSupplier.ToArray());
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionSalesmanList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sSalesman = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lSalesman = new List<string>();
        arr.Add(new cArrayList("@salesman", prefixText));
        bll.vSearchMstSalesman(arr, ref rs);
        while (rs.Read())
        {
            sSalesman = AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + " | " + rs["emp_nm"].ToString() + " (" + rs["dept_cd"].ToString() + ")", rs["emp_cd"].ToString());
            lSalesman.Add(sSalesman);
        } rs.Close();
        return (lSalesman.ToArray());
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionCustomerList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sCustomer = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lCustomer = new List<string>();
        arr.Add(new cArrayList("@customer", prefixText));
        bll.vSearchMstCustomer2(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " | " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCustomer.Add(sCustomer);
        } rs.Close();
        return (lCustomer.ToArray());
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionAccountList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sAccount = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lAccount = new List<string>();
        arr.Add(new cArrayList("@account", prefixText));
        bll.vSearchMstCOA(arr, ref rs);
        while (rs.Read())
        {
            sAccount = AutoCompleteExtender.CreateAutoCompleteItem(rs["coa_cd"].ToString() + " | " + rs["coa_nm"].ToString(), rs["coa_cd"].ToString());
            lAccount.Add(sAccount);
        } rs.Close();
        return (lAccount.ToArray());
    }
     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionItemList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }
     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
     public static string[] GetCompletionDepartmentList(string prefixText, int count, string contextKey)
     {
         cbll bll = new cbll();
         SqlDataReader rs = null;
         string sDepartment = string.Empty;
         List<cArrayList> arr = new List<cArrayList>();
         List<string> lDepartment = new List<string>();
         arr.Add(new cArrayList("@department", prefixText));
         bll.vSearchMstDepartment(arr, ref rs);
         while (rs.Read())
         {
             sDepartment = AutoCompleteExtender.CreateAutoCompleteItem(rs["dept_cd"].ToString() + " | " + rs["dept_nm"].ToString(), rs["dept_cd"].ToString());
             lDepartment.Add(sDepartment);
         } rs.Close();
         return (lDepartment.ToArray());
     }

    protected void txsearchitem_TextChanged(object sender, EventArgs e)
    {
        //txunitprice.Text = bll.vLookUp("select price_sell from tmst_item where item_cd='" + hditem.Value + "'");
        cbUOM_SelectedIndexChanged(sender, e);
    }

    protected void txsearchtotamount_TextChanged(object sender, EventArgs e)
    {
        //txunitprice.Text = bll.vLookUp("select price_sell from tmst_item where item_cd='" + hditem.Value + "'");
        cbUOM_SelectedIndexChanged(sender, e);
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDebit = (Label)e.Row.FindControl("lblDebit");
            Label lblCredit = (Label)e.Row.FindControl("lblCredit");
            decimal debit;
            decimal credit;

            if (!decimal.TryParse(Convert.ToString(lblDebit.Text), out debit))
            {
                debit = 0;
            }
            else
            {
                debit = decimal.Parse(lblDebit.Text);
            }
            if (!decimal.TryParse(Convert.ToString(lblCredit.Text), out credit))
            {
                credit = 0;
            }
            else
            {
                credit = decimal.Parse(lblCredit.Text);
            }
            totalDebit = totalDebit + debit;
            totalCredit = totalCredit + credit;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //sCustType = bll.vLookUp("select top 1 fld_valu from tfield_value where fld_nm='otlbrn'");
            Label lblTotalDebit = (Label)e.Row.FindControl("lblTotalDebit");
            Label lblTotalCredit = (Label)e.Row.FindControl("lblTotalCredit");
            lblTotalDebit.Text = totalDebit.ToString("#,##0.00");
            lblTotalCredit.Text = totalCredit.ToString("#,##0.00");
        }
    }


    //protected void grddoc_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    grddoc.EditIndex = e.NewEditIndex;
    //    //bindinggrddoc();
    //}
    //protected void grddoc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    grddoc.EditIndex = -1;
    //    //bindinggrddoc();
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    //}
    //protected void grddoc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    Label lbdoccode = (Label)grddoc.Rows[e.RowIndex].FindControl("lbdoccode");
    //    FileUpload FileUpload1 = (FileUpload)grddoc.Rows[e.RowIndex].FindControl("FileUpload1");
    //    if (FileUpload1.HasFile)
    //    {
    //        List<cArrayList> arr = new List<cArrayList>();
    //        arr.Add(new cArrayList("@salespointCD", cbSalesPointCD.SelectedValue));
    //        arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
    //        bll.vUpdatetblTrnStock_document(arr);

    //    }
    //    grddoc.EditIndex = -1;
    //    bindinggrddoc();
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    //}
    protected void cbUOM_SelectedIndexChanged(object sender, EventArgs e)
    {
        getprice();
    }
    protected void txsearchitem_TextChanged2(object sender, EventArgs e)
    {
        getprice2();
    }
    private void getprice()
    {
        double dPrice = 0;
        string sCustType = "";
        sCustType = bll.vLookUp("select top 1 fld_valu from tfield_value where fld_nm='otlbrn'");
        dPrice = bll.dGetItemPrice(hditem.Value.ToString(), sCustType, cbUOM.SelectedValue.ToString());
        txunitprice.Text = dPrice.ToString();

        double qty = 0;
        //double.TryParse(txqty.Text, out qty);
        if (!double.TryParse(Convert.ToString(txqty.Text), out qty))
        {
            qty = 0;
        }
        else
        {
            qty = Convert.ToDouble(txqty.Text);
        }


        double totQty = dPrice * qty;
        txtotprice.Text = totQty.ToString();
    }
    private void getprice2()
    {
        double dPrice = 0;
        //double.TryParse(txunitprice.Text, out dPrice);
        if (!double.TryParse(Convert.ToString(txunitprice.Text), out dPrice))
        {
            dPrice = 0;
        }
        else
        {
            dPrice = Convert.ToDouble(txunitprice.Text);
        }

        double qty = 0;
        //double.TryParse(txqty.Text, out qty);
        if (!double.TryParse(Convert.ToString(txqty.Text), out qty))
        {
            qty = 0;
        }
        else
        {
            qty = Convert.ToDouble(txqty.Text);
        }

        double totQty = 0;
        totQty = dPrice * qty;
        txtotprice.Text = totQty.ToString();
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_accManualJournal_List.aspx");
    }
    protected void btdelete_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string userid = Request.Cookies["usr_id"].Value.ToString();
        arr.Add(new cArrayList("@user", userid));
        arr.Add(new cArrayList("@journalid", txJournalId.Text));
        bll.vDeletemanualjournal(arr);
        newDetailRow.Visible = false;

        updateText(txJournalId.Text);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delete journal is successfull','','info');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        Response.Redirect("fm_accManualJournal_List.aspx");
    }
    protected void btdelete_det_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@journalid", txJournalId.Text));
        //bll.vDeletemanualjournaldet(arr);
        //newDetailRow.Visible = false;

        //updateText(txJournalId.Text);

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delete journal detail is successfull','','info');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }

    protected void updateText(string idJournal)
    {
        dttrnstkDate.Text = bll.vLookUp("select FORMAT(tran_date,'dd/MM/yyyy')tran_date from tacc_journal where id_journal='" + idJournal + "';");
        txtrnstkRemark.Text = bll.vLookUp("select description from tacc_journal where id_journal='" + idJournal + "';");
        txJournalStatus.Text = bll.vLookUp("select case when j.status is NULL then 'Unsaved' when j.status='N'then 'Unsaved, Not Balance' when j.status='P'then 'Posted'when j.status='U'then 'Unposted'when j.status='D'then 'Deleted'end journal_status from tacc_journal j where id_journal='" + idJournal + "';");
    }

}

                    