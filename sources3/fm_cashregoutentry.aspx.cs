using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
public partial class fm_cashregoutentry : System.Web.UI.Page
{
    cbll bll = new cbll(); double dTotAmt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkcasregoutdtl(arr);
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingFieldValueToCombo(ref cbtype, "cashout_typ",false);
            
            txcashoutno.Enabled = false;
            cbtype_SelectedIndexChanged(sender, e);
            dtcashout.Text = Request.Cookies["waz_dt"].Value.ToString();
            //cbsalespoint.Enabled = false;
            cbsalespoint.CssClass = "form-control  ro";
            cbsalespoint.Enabled = false;
            arr.Clear();
            arr.Add(new cArrayList("@doc_typ", "cashout"));
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSp(ref cbapp, "sp_tapprovalpattern_get", "emp_cd", "emp_nm", arr);
          //  bll.vBindingComboToSp(ref cbapproval, "sp_tapprovalpattern_get", "emp_cd", "fullname", arr);
            bll.vBindingFieldValueToCombo(ref cbio, "inout");
            cbio.SelectedValue = "O";
            cbio.CssClass = "form-control  ro";
            cbio.Enabled = false;
            //cbio.Enabled = false;
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            cbio_SelectedIndexChanged(sender, e);
            txcashoutno.Text = "NEW";
         //   bll.vBindingFieldValueToCombo(ref cbsource, "source_order");
         //   bll.vBindingFieldValueToCombo(ref cbpaymenttype, "payment_typ");
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
         //   bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_nm", arr);
            txkm.Visible = false;
            lbkm.Visible = false;
            btprint.Visible = false;
            btsave.Visible = true;
            lbbalance.Text = bll.vLookUp("select dbo.fn_getcashierbalance()").ToString();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        
    }
    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_typ", cbtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inout", "O"));
        bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get", "itemco_cd", "item_desc",arr);
        cbitem_SelectedIndexChanged(sender, e);
        //for this
        if (cbtype.SelectedValue.ToString() == "MA")
        { lbkm.Visible = true; txkm.Visible = true; }
        else { lbkm.Visible = false; txkm.Visible = false; }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        { 
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"],rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        txamtPnl.CssClass = "";
        txpicPnl.CssClass = "";
        uplfilePnl.CssClass = "";
        txqtyPnl.CssClass = "";
        txkmPnl.CssClass = "";

        double dAmt = 0;
        if (lbroutine.Text != "Routine")
        {
            if (cbapp.SelectedValue.ToString() == "ALL")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select approval!','Non Routine need approval','warning');", true);
                return;
            }
        }
        if (!double.TryParse(txamt.Text, out dAmt))
        { 
            ScriptManager.RegisterStartupScript(Page,Page.GetType(),"al","sweetAlert('Amount must numeric !','Amount','warning');",true);
            txamtPnl.CssClass = "error";
            return;
        }

        if (hdemp.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('PIC must be selected','Person In Charge','warning');", true);
            txpicPnl.CssClass = "error";
            return;
        }
        if (lbroutine.Text != "Routine" & uplfile.FileName=="")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Doc App must be Fill','Doc App','warning');", true);
            uplfilePnl.CssClass = "error";
            return;
        }

        double dOut = 0; double dKm = 0;
        if (!double.TryParse(txqty.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Quantity must numeric','Check Qty','warning');", true);
            txqtyPnl.CssClass = "error";
            return;
        }

        if (cbtype.SelectedValue.ToString() == "MA")
        {
            if (!double.TryParse(txkm.Text, out dOut))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('KM must numeric','Check KM','warning');", true);
                txkmPnl.CssClass = "error";
                return;
            }
        }
        else
        {
            txkm.Text = "0";
        }

        if (lbroutine.Text != "Routine")
        {
        if (uplfile.FileName == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select file approval','Check File','warning');", true);
            uplfilePnl.CssClass = "error";
            return;
        }
        }

        double dCashier = Convert.ToDouble(lbbalance.Text);
        double dBook = 0;
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.Footer)
            {
                Label lbtotamt = (Label)row.FindControl("lbtotamt");
                dBook = Convert.ToDouble(lbtotamt.Text);
            }
        }
        if ((dCashier - (dBook + dAmt)) < 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Your money in cashier is not enough !','Check cashier','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cashout_typ", cbtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@amt", txamt.Text));
        arr.Add(new cArrayList("@isasset", chkasset.Checked));
        arr.Add(new cArrayList("@km", txkm.Text));
        arr.Add(new cArrayList("@qty", txqty.Text));
        string sRoutine="",sbank="";
        if (cbbank.DataSource == null||cbbank.DataSource == "") { sbank = null; } else { sbank = cbbank.SelectedItem.Text; }
        if (lbroutine.Text == "Routine")
        {
            sRoutine = "R";
        }
        else { sRoutine = "NR"; }
        arr.Add(new cArrayList("@routine", sRoutine));
        arr.Add(new cArrayList("@isclaim", chkclaim.Checked));
        arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        arr.Add(new cArrayList("@app_cd", cbapp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@ref_no", txref.Text));
        arr.Add(new cArrayList("@acc_no", sbank));
        if (sRoutine == "NR")
        {
            string strdate = Convert.ToDateTime(DateTime.ParseExact(dtcashout.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)).ToString("yyyyMM");
            FileInfo fi = new FileInfo(uplfile.FileName);
            string ext = fi.Extension;
            if ((uplfile.FileName != "") || (uplfile.FileName.Equals(null)))
            {
                arr.Add(new cArrayList("appfile", strdate + txref.Text + ext));
                uplfile.SaveAs(bll.sGetControlParameter("image_path") + strdate + txref.Text + ext);
            }
        }
        bll.vInsertWrkCasregoutdtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));

        bll.vBindingGridToSp(ref grd, "sp_twrk_casregoutdtl_get", arr);
       // lbtotal.Text = bll.vLookUp("select sum(amt) from twrk_casregoutdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
        txamt.Text = "";
        lbroutine.Text = "";
        txref.Text = "";
        txpic.Text = "";
        hdemp.Value = "";
        txamt.Text = "";
        txkm.Text = "";
        txqty.Text = "";
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hditem = (HiddenField)grd.Rows[e.RowIndex].FindControl("hditem");
        Label lbcashouttype = (Label)grd.Rows[e.RowIndex].FindControl("lbtype");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@itemco_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@cashout_typ", lbcashouttype.Text));
        bll.vDelWrkcasregoutdtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_casregoutdtl_get", arr);
    }

    void MakeDisable()
    {
        txmanno.Enabled = false;
        txamt.Enabled = false;
        txcashoutno.Enabled = false;
        txpic.Enabled = false;
        txref.Enabled = false;
        txremark.Enabled = false;
        txmanno.CssClass = "form-control  ro";
        txamt.CssClass = "form-control  ro";
        txcashoutno.CssClass = "form-control  ro";
        txpic.CssClass = "form-control  ro";
        txremark.CssClass = "form-control  ro";
        txref.CssClass = "form-control  ro";

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        bool bPrint = false;
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Item Found','Item','warning');", true);
            return;
            
        }

        string scd = bll.vLookUp("select  dbo.sfnGetcountdown('"+ Request.Cookies["sp"].Value.ToString() + "')");
        if (scd == "0" && txcashoutno.Text == "NEW")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Block entry because deadline to daily closing !','Please daily closing !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }

        //bool bCheck = false;
        //foreach (GridViewRow row in grd.Rows)
        //{ 
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chk;
        //    }
        //}
        string sCasregNo = "";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cashout_dt", DateTime.ParseExact( dtcashout.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@tot_amt", dTotAmt));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@manual_no", txmanno.Text));
        try
        {
            bll.vInsertCashRegOut(arr, ref sCasregNo);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : vInsertCashRegOut");
        }
        txcashoutno.Text = sCasregNo;
        btsave.Enabled = false; btprint.Enabled = true;
       
        foreach ( GridViewRow row in grd.Rows)
        { 
            int nrnd;
            HiddenField hdapp = (HiddenField)row.FindControl("hdapp");
            Label lbtotamt = (Label)row.FindControl("lbamt");
            DropDownList cbroutine = (DropDownList)row.FindControl("cbroutine");
            Label lbitemcode = (Label)row.FindControl("lbitemcode");
            //string sItemName = bll.vLookUp("select itemco_nm from tmst_itemcashout where itemco_cd='" + lbitemcode.Text  + "'");
            string sItemName = lbitemcode.Text;
            Random rnd = new Random();
            nrnd = rnd.Next(1000, 9999);
            List<string> lapproval = bll.lGetApproval(hdapp.Value.ToString());
            string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nrnd.ToString();
            string sMsg = "#Cashout req from " + cbsalespoint.SelectedItem.Text +  ", no." + sCasregNo + ", amt  " + lbtotamt.Text + 
                 ", for " + sItemName +  ", do you want to approved : (Y/N)" + stoken.ToString();
            if (cbroutine.SelectedValue.ToString() == "NR")
            {
                bPrint = false;
                arr.Clear();
                arr.Add(new cArrayList("@token", stoken.ToString()));
                arr.Add(new cArrayList("@doc_no", sCasregNo));
                arr.Add(new cArrayList("@doc_typ", "cashout"));
                arr.Add(new cArrayList("@to", lapproval[0]));
                arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
                bll.vInsertSmsOutbox(arr);
                // bll.vInsertSMSSent(arr);
                // Send Email
                string sSub = "Cash out req from " + cbsalespoint.SelectedItem.Text + " No. " + sCasregNo;
                string sEmail = "Dear Approval,<br><br>New Cash register out has been created with following item :<br>" +
                    lbitemcode.Text + " with amount " + lbtotamt.Text + "<br><br>" +
                    "Please go through";
                //send email by yanto 2015-11-14
                Label lbappfile = (Label)row.FindControl("lbappfile");
                Label lbemp_nm = (Label)row.FindControl("lbemp_nm");
                Label lbtype = (Label)row.FindControl("lbtype");
                HiddenField hdemp_cd = (HiddenField)row.FindControl("hdemp_cd");
                if (lbappfile.Text != "")
                {
                    //Random rnd = new Random();
                    int nRnd = rnd.Next(1000, 9999);
                    string sSubject = ""; string sMessage = "";
                    string sfile_attachment = lbappfile.Text, sto = bll.vLookUp("select email from tuser_profile where emp_cd='" + hdapp.Value + "'");
                    //string slink_ho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_ho'");
                    string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
                    string stitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=( select job_title_cd from tmst_employee where emp_cd='" + hdemp_cd.Value + "')");
                    string cashout_typ = lbtype.Text;
                    string scashout_typ = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_typ' and fld_valu='" + cashout_typ + "'");
                    string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + cbsalespoint.SelectedValue);
                    string screator_id = Request.Cookies["usr_id"].Value.ToString();
                    string screator_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + screator_id + "'");
                    string spic_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + hdemp_cd.Value + "'");
                    sSubject = "New Cash Request Branch " + cbsalespoint.SelectedItem.Text + " has been created ";
                    //sMessage = "Request No. " + sCasregNo + " created by " + lbemp_nm.Text + ", with the item " + lbitemcode.Text + 
                    sMessage = "<table><tr><td colspan=3>#Cashout Request Approval</td></tr><tr><td></td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr><tr><td>Request No</td><td>:</td><td>" + sCasregNo + "</td><td>PIC</td><td>:</td><td>" + spic_nm + "</td></tr><tr><td>Created</td><td>:</td><td>" + screator_nm + "</td><td>Position </td><td>:</td><td>" + stitle + "</td></tr><tr><td>Trans Type</td><td>:</td><td>" + scashout_typ + "</td><td>Item</td><td>:</td><td>" + lbitemcode.Text + "</td></tr><tr><td><font color='#FF0000'>Req Amount</font></td><td>:</td><td><font color='#FF0000'>" + lbtotamt.Text + "</font></td></tr><tr><td></td></tr><tr><td>Note</td><td>:</td><td colspan=4>1. See Attached file </td></tr><tr><td></td><td></td><td colspan=4>2. This Approval to make sure all detail as attached is match with entry request.</td></tr><tr><td></td></tr><tr><td></td></tr></table>" +
                        //"<p> Please Click this  for approved : <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpageCasregout.aspx?appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpageCasregout.aspx?appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>" + 
                    "<p> Please Click this  for approved : <a href='" + slink_branch + "/landingpage.aspx?trnname=casregout&salespointcd=" + cbsalespoint.SelectedValue + "&RefNo=" + txcashoutno.Text + "&appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage.aspx?trnname=casregout&salespointcd=" + cbsalespoint.SelectedValue + "&RefNo=" + txcashoutno.Text + "&appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>" +
                    " \n\r\n\r\n\r\n Wazaran Admin";
                    //bll.vSendMail(sto, sSubject, sMessage, sfile_attachment);
                    arr.Clear();
                    arr.Add(new cArrayList("@trxcd", "Casregout"));
                    arr.Add(new cArrayList("@token", nRnd.ToString()));
                    arr.Add(new cArrayList("@doc_no", sCasregNo));
                    bll.vInsertEmailSent(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@token", nRnd.ToString()));
                    arr.Add(new cArrayList("@doc_typ", "Casregout"));
                    arr.Add(new cArrayList("@to", sto));
                    arr.Add(new cArrayList("@doc_no", sCasregNo));
                    arr.Add(new cArrayList("@emailsubject", sSubject));
                    arr.Add(new cArrayList("@msg", sMessage));
                    arr.Add(new cArrayList("@file_attachment", sfile_attachment));
                    bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017
                    //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
                }
                //************************
            }
            else { bPrint = true; }
           
        }
        lbbalance.Text = bll.vLookUp("select dbo.fn_getcashierbalance()").ToString();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Request cashout has saved successfully','CO No. " + sCasregNo + "','success');", true);

        btprint.Visible = bPrint;
        btsave.Visible = false;

        MakeDisable();
       
    }
    protected void cbitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        lbroutine.Text = bll.vLookUp("select (select fld_desc from tfield_value where fld_nm='routine' and fld_valu=tmst_itemcashout.routine) from tmst_itemcashout where itemco_cd='" + cbitem.SelectedValue.ToString() + "'");
        if (lbroutine.Text == "Routine")
        {
            cbapp.Items.Clear();
            cbapp.CssClass = "form-control input-sm ro";
            cbapp.Enabled = false;
            if (cbitem.SelectedValue.ToString() == "cashdep")
            {

                lbbank.Visible = true;
               arr.Clear();
               arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
              //  bll.vBindingComboToSp(ref cbbank, "sp_tmst_bank", "bank_cd", "bank_nm");
               bll.vBindingComboToSp(ref cbbank, "sp_tmst_bankaccount_get", "ids", "bank_desc", arr);
               cbbank.CssClass = "form-control input-sm";
               cbbank.Visible = true;
               cbbankPnl.Visible = true;
            }
            else
            {
                //cbbankaccount.CssClass = "makeitreadonly";
                cbbank.Visible = false;
                cbbankPnl.Visible = false;
                lbbank.Visible = false;
            }
        }

        else {
            //cbbankaccount.CssClass = "makeitreadonly";
            cbbank.Visible = false;
            cbbankPnl.Visible = false;
            lbbank.Visible = false;
            cbapp.CssClass = "form-control input-sm";
            cbapp.Enabled = true;
            arr.Clear();
            arr.Add(new cArrayList("@doc_typ", "cashout"));
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSp(ref cbapp, "sp_tapprovalpattern_getbyselect", "emp_cd", "emp_nm", arr);
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList cbroutine = (DropDownList)e.Row.FindControl("cbroutine");
            Label lbamt = (Label)e.Row.FindControl("lbamt");
            dTotAmt += Convert.ToDouble(lbamt.Text);
            bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
            cbroutine.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "routine"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotamt = (Label)e.Row.FindControl("lbtotamt");
            lbtotamt.Text = dTotAmt.ToString();
          //  hdsubtotal.Value = lbtotamt.Text;
        }
    }
    protected void cbio_SelectedIndexChanged(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        string sQryCd = "";
            if (cbio.SelectedValue.ToString() == "I")
            { sQryCd="cashin"; }else{sQryCd="cashout";}
        //arr.Add(new cArrayList("@qry_cd",sQryCd));
        //arr.Add(new cArrayList("@fld_nm", "cashout_typ"));
        bll.vBindingFieldValueToCombo(ref cbtype, "cashout_typ", false);
        cbtype_SelectedIndexChanged(sender, e);
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void cbsource_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    List<cArrayList> arr = new List<cArrayList>();
    //    if (cbsource.SelectedValue.ToString() == "MAN")
    //    { cbsalesman.Items.Clear(); cbsalesman.Enabled = false; cbsalesman.CssClass = "makeitreadwrite"; }
    //    else {
    //        arr.Add(new cArrayList("@qry_cd", "SalesJob"));
    //        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
    //        bll.vBindingComboToSp(ref cbsalesman,"sp_tmst_employee_getbyqry", "emp_cd", "emp_nm" , arr);
    //        cbsalesman.Enabled = true;
    //        cbsalesman.CssClass = "makeitreadonly";
    //    }
    //}
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=croreq&no=" + txcashoutno.Text +  "');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_cashregoutentry.aspx");
    }
    protected void chkclaim_CheckedChanged(object sender, EventArgs e)
    {
        if (chkclaim.Checked)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookup_isclaim.aspx');", true);
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookup_cashout.aspx');", true);
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkcasregoutdtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@casregout_cd", hdcash.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertCashregoutCoreToWrk(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));

        bll.vBindingGridToSp(ref grd, "sp_twrk_casregoutdtl_get", arr);
        string sCHeck = bll.vLookUp("select cashout_sta_id from tcashregout where casregout_cd='"+hdcash.Value.ToString()+"'");
        txcashoutno.Text = hdcash.Value.ToString();
        lbstatus.Text = bll.sGetFieldValue("cashout_sta_id", sCHeck);
        btsave.CssClass = "divhid";
        if (sCHeck == "A")
        {
            
            btprint.Visible = true;
            btprint.CssClass = "btn btn-primary btn-lg";
        }
        else
        {
            btprint.Visible = false;
            btprint.CssClass = "divhid";
        }
    }
}