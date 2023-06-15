using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_CashierPettyCash : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    decimal totDebitAmount = 0; decimal totCreditAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCRDR.SelectedValue = "CR";
            ddlCRDR_SelectedIndexChanged(sender, e);
            dtpost.Text = DateTime.Now.ToString("d/M/yyyy");


            BindGrid();
        }
    }

    void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", Convert.ToString(txtemp.Text.Split('-')[0])));
        bll.vBindingGridToSp(ref grd, "sp_temployee_advanced_get", arr);
        //txtEmpDebit.Text = bll.vLookUp("select sum(debit) from temployee_advanced where doc_cd='" + hdfCashAdvancedID.Value + "'");
        //txtEmpCredit.Text = bll.vLookUp("select sum(credit) from temployee_advanced where doc_cd='" + hdfCashAdvancedID.Value + "'");

        decimal debit = txtEmpDebit.Text == "" ? 0 : Convert.ToDecimal(txtEmpDebit.Text);
        decimal credit = txtEmpCredit.Text == "" ? 0 : Convert.ToDecimal(txtEmpCredit.Text);

        lblBalance.Text = Convert.ToString(Convert.ToDecimal(debit - credit));
    }
    protected void ddlCRDR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCRDR.SelectedValue == "DR")
        {
            //Response.Cookies["itemco_cd"].Value = "CHA00080";
            //GetCompletionList("CHA00080", 10, null);
            string itemco_cd = bll.vLookUp("select itemco_cd + '-' + itemco_nm from tmst_itemcashout where itemco_cd='" + "CHA00080" + "'");
            txItemCashout.Text = itemco_cd;
            hdCashOut.Value = "CHA00080";
            txItemCashout.CssClass = "form-control ro";


        }
        else
        {
            txItemCashout.Text = string.Empty;
            hdCashOut.Value = null;
            //Response.Cookies["itemco_cd"].Value = null;
            txItemCashout.CssClass = "divnormal form-control";
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sItemCash = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_nm", prefixText));
        bll.vSearchItemCashOut(arr, ref rs);
        while (rs.Read())
        {
            sItemCash = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_desc"].ToString(), rs["itemco_cd"].ToString());
            lCust.Add(sItemCash);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployee(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sEmployee = string.Empty;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["empName"].ToString(), rs["emp_cd"].ToString());
            lCust.Add(sEmployee);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void btShowItemCashout_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        string cashout_cd = string.Empty;
        double amount = 0;
        List<cArrayList> arr = new List<cArrayList>();

        if (!double.TryParse(txtAmt.Text, out amount))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount to be Paid must numeric!','Amount to be paid','warning');", true);
            return;
        }
        if (Convert.ToString(txtemp.Text) == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select employee!','Select employee','warning');", true);
            return;
        }
        if (Convert.ToString(hdCashOut.Value) == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please assign CashOut type!','CashOut Type','warning');", true);
            return;
        }
        if (Convert.ToString(txtManualNumber.Text) == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please assign manual number !','manual number missing','warning');", true);
            return;
        }

        decimal debit = txtEmpDebit.Text == "" ? 0 : Convert.ToDecimal(txtEmpDebit.Text);
        decimal amountAssign = txtAmt.Text == "" ? 0 : Convert.ToDecimal(txtAmt.Text);
        decimal balance = lblBalance.Text == "" ? 0 : Convert.ToDecimal(lblBalance.Text);

        if ((balance - amountAssign) < 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not assign more amount in compare balance, make new Cash Out request!','Amount wrong','warning');", true);
            return;
        }

        List<cArrayList> arrAdvance = new List<cArrayList>();
        arrAdvance.Add(new cArrayList("@emp_cd", Convert.ToString(txtemp.Text.Split('-')[0])));
        arrAdvance.Add(new cArrayList("@itemco_cd", Convert.ToString(hdCashOut.Value)));
        if (ddlCRDR.SelectedValue == "DR")
        {
            arrAdvance.Add(new cArrayList("@debit", Convert.ToDouble(txtAmt.Text)));
            arrAdvance.Add(new cArrayList("@credit", 0));
        }
        else
        {
            arrAdvance.Add(new cArrayList("@debit", 0));
            arrAdvance.Add(new cArrayList("@credit", Convert.ToDouble(txtAmt.Text)));
        }
        arrAdvance.Add(new cArrayList("@manual_number", Convert.ToString(txtManualNumber.Text)));
        arrAdvance.Add(new cArrayList("@pc_sta_id", "N"));
        arrAdvance.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
        arrAdvance.Add(new cArrayList("@createdby", Convert.ToString(Request.Cookies["usr_id"].Value)));
        arrAdvance.Add(new cArrayList("@transactiondate", DateTime.ParseExact(dtpost.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));


        arr.Add(new cArrayList("@cashout_dt", DateTime.ParseExact(dtpost.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@emp_cd", txtemp.Text.Split('-')[0]));
        arr.Add(new cArrayList("@dept_cd", txtDept.Text));
        arr.Add(new cArrayList("@remark", null));
        arr.Add(new cArrayList("@sequenceno", null));
        arr.Add(new cArrayList("@amt", Convert.ToDouble(txtAmt.Text)));
            arr.Add(new cArrayList("@vat_amt", 0));
        if (txtInOut.Text == "Cash Out")
        {
            if (txtRoutine.Text == "Not Routine")
            {
                arr.Add(new cArrayList("@cashout_sta_id", "N"));
            }
            else if (txtRoutine.Text == "Routine")
            {
                arr.Add(new cArrayList("@cashout_sta_id", "A"));
            }
        }
        else if (txtInOut.Text == "Cash In")
        {
            arr.Add(new cArrayList("@cashout_sta_id", "A"));
        }
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@approval_cd", cbapproval.SelectedValue.ToString()));
        arr.Add(new cArrayList("@itemco_cd",hdCashOut.Value));
        arr.Add(new cArrayList("@manualno", Convert.ToString(txtManualNumber.Text)));

        //arr.Add(new cArrayList("@doc_cd", Convert.ToString(hdfCashAdvancedID.Value)));
        if ((txtRoutine.Text == "Not Routine") && (txtInOut.Text == "Cash Out"))
        {
            bll.vInsertCashoutRequest(arr, ref cashout_cd);
        }
        else 
        {
            //bll.vInsertCashAdvanced(arrAdvance, ref cashout_cd);
            bll.vInsertCashoutRequest(arr, ref cashout_cd);
        }
        if ((txtRoutine.Text == "Not Routine") && (txtInOut.Text == "Cash Out"))
        {
            int nrnd = 0;
            Random rnd = new Random();
            nrnd = rnd.Next(1000, 9999);
            List<string> lapproval = bll.lGetApproval(cbapproval.SelectedValue.ToString());

            string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nrnd.ToString();

            string sMsg = "#Cashout request from " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + ", no." + cashout_cd + ", amt  " + txtAmt.Text +
                 ", for " + txItemCashout.Text + ", do you want to approved : (Y/N)" + stoken.ToString();
            arr.Clear();
            arr.Add(new cArrayList("@token", stoken.ToString()));
            arr.Add(new cArrayList("@doc_no", cashout_cd));
            arr.Add(new cArrayList("@doc_typ", "cashout"));
            arr.Add(new cArrayList("@to", lapproval[0]));
            arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
            bll.vInsertSmsOutbox(arr);

            // Sending Email 
            string sSubject = ""; string sMessage = "";
            string sfile_attachment = null;
            //string slink_ho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_ho'");
            string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
            string stitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=( select job_title_cd from tmst_employee where emp_cd='" +hdfEmployee.Value + "')");
            string cashout_typ = txtRoutine.Text;
            string scashout_typ = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_typ' and fld_valu='" + cashout_typ + "'");
            string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + Request.Cookies["sp"].Value.ToString());
            string screator_id = Request.Cookies["usr_id"].Value.ToString();
            string screator_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + screator_id + "'");
            string spic_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + hdfEmployee.Value.ToString() + "'");
            sSubject = "#New Cashier Petty Cash Request Branch " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + " has been created ";
            //sMessage = "Request No. " + sCasregNo + " created by " + lbemp_nm.Text + ", with the item " + lbitemcode.Text + 
            sMessage = "<table><tr><td colspan=3>#Cashier Petty Cash Request Approval</td></tr><tr><td></td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr><tr><td>Request No</td><td>:</td><td>" + cashout_cd + "</td><td>PIC</td><td>:</td><td>" + spic_nm + "</td></tr><tr><td>Created</td><td>:</td><td>" + screator_nm + "</td><td>Position </td><td>:</td><td>" + stitle + "</td></tr><tr><td>Trans Type</td><td>:</td><td>" + scashout_typ + "</td><td>Item</td><td>:</td><td>" +hdCashOut.Value + "</td></tr><tr><td><font color='#FF0000'>Req Amount</font></td><td>:</td><td><font color='#FF0000'>" +txtAmt.Text + "</font></td></tr><tr><td></td></tr><tr><td>Note</td><td>:</td><td colspan=4>1. See Attached file </td></tr><tr><td></td><td></td><td colspan=4>2. This Approval to make sure all detail as attached is match with entry request.</td></tr><tr><td></td></tr><tr><td></td></tr></table>" +
            "<p> Please Click this  for approved : <a href='" + slink_branch + "/landingpage2.aspx?src=CashierPettyCash&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids=" + cashout_cd + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage2.aspx?src=cashout&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids=" + cashout_cd + "&sta=R'>Reject</a></p>" +
            " \n\r\n\r\n\r\n Wazaran Admin";
            //bll.vSendMail(lapproval[1], sSubject,sMessage, "CO20409172345.gif");
            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "cashout"));
            arr.Add(new cArrayList("@token", nrnd.ToString()));
            arr.Add(new cArrayList("@doc_no", cashout_cd));
            bll.vInsertEmailSent(arr);
            arr.Clear();
            arr.Add(new cArrayList("@token", nrnd.ToString()));
            arr.Add(new cArrayList("@doc_typ", "cashout"));
            arr.Add(new cArrayList("@to", lapproval[1]));
            arr.Add(new cArrayList("@doc_no", cashout_cd));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sMessage));
            arr.Add(new cArrayList("@file_attachment", sfile_attachment));
            bll.vInsertEmailOutbox(arr);
            //************************ END OF ENTRY
        }

        if (cashout_cd == "-2")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Ref no already available.','Ref no already available. ','warning');", true);
            return;
        }
        else
        {
            BindGrid();
            lbsysno.Text = cashout_cd;
            txtAmt.Text = string.Empty;
            txtManualNumber.Text = string.Empty;
            ddlCRDR.SelectedValue = "CR";

            ddlCRDR_SelectedIndexChanged(sender, e);
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Save Successfully.','"+ cashout_cd + "','success');", true);
        }

    }

    protected void btNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_CashierPettyCash.aspx");
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string userID = Convert.ToString(Request.Cookies["usr_id"].Value);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                totDebitAmount += Convert.ToDecimal(lblDebit.Text);
                totCreditAmount += Convert.ToDecimal(lblCredit.Text);
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                totDebitAmount += Convert.ToDecimal(lblDebit.Text);
                totCreditAmount += Convert.ToDecimal(lblCredit.Text);
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                totDebitAmount += Convert.ToDecimal(lblDebit.Text);
                totCreditAmount += Convert.ToDecimal(lblCredit.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbTotDebit = (Label)e.Row.FindControl("lbTotDebit");
                Label lbTotCredit = (Label)e.Row.FindControl("lbTotCredit");
                lbTotDebit.Text = String.Format("{0:0.00}", totDebitAmount);
                lbTotCredit.Text = String.Format("{0:0.00}", totCreditAmount);
                txtEmpDebit.Text = lbTotDebit.Text;
                txtEmpCredit.Text = lbTotCredit.Text;
            }
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "Petty  Cash", "fm_CashierPettyCash", "grd_RowDataBound", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        var id = hdfCashAdvancedID.Value;
        //txtEmpDebit.Text = bll.vLookUp("select sum(debit) from temployee_advanced where doc_cd='" + hdfCashAdvancedID.Value + "'");
        //txtEmpCredit.Text = bll.vLookUp("select sum(credit) from temployee_advanced where doc_cd='" + hdfCashAdvancedID.Value + "'");
        txtemp.Text = bll.vLookUp("select emp_cd + '-' + emp_nm from tmst_employee where emp_cd in (  select emp_cd from temployee_advanced where doc_cd='" + hdfCashAdvancedID.Value + "')");
        txtCashoutNumber.Text = bll.vLookUp("select doc_cd from temployee_advanced where doc_cd='" + hdfCashAdvancedID.Value + "'");
        lblExpreid.Text = "Cashout " + txtCashoutNumber.Text + " expired after 30 days.";
        //txtCashoutNumber.Text = hdfCashAdvancedID.Value;
        BindGrid();
    }



    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_CashierPettyCashReport.aspx");
    }

    protected void btnRaw_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_AdvancePettyCashRAWReport.aspx");
    }

    protected void btnCashOutType_Click(object sender, EventArgs e)
    {
        string cashOutType = hdCashOut.Value;

        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_nm", hdCashOut.Value.ToString()));
        bll.vGetItemcashout(arr, ref rs);
        while (rs.Read())
        {
            txtInOut.Text = rs["inoutValue"].ToString();
            txtRoutine.Text = rs["routineValue"].ToString();
            txtType.Text = rs["cashout_typValue"].ToString();
            if (txtInOut.Text == "Cash Out")
            {
                arr.Clear();
                arr.Add(new cArrayList("@doc_typ", "cashout"));
                arr.Add(new cArrayList("@level_no", "1"));
                bll.vBindingComboToSp(ref cbapproval, "sp_tapprovalpattern_get", "emp_cd", "emp_nm", arr);
                cbapproval.CssClass = cd.csstext;
            }
            else
            {
                cbapproval.Items.Clear();
                cbapproval.CssClass = cd.csstextro;
            }
        }
        rs.Close();
    }
}