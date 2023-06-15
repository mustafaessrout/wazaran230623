using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_AccSettingFinanceReport : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    public static int PreviousIndex;
    Boolean hasApprovalRole = false;
    string userid = null;
    //string userid2 = null;
    string idfinreport = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        txReportID.ReadOnly = true;
        //txStatus.ReadOnly = true;
        //rdishidden.SelectedValue = null;

        grd.Columns[0].Visible = false;

        if (!IsPostBack)
        {
            
            string userid = Request.Cookies["usr_id"].Value.ToString();
            //string userid = "2833";

            //userid2 = bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'");
            //if (bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'") != "")
            if (bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'") != "")
            {
                hasApprovalRole = true;
            }

            bll.vBindingComboToSp(ref ddAccountSrc, "sp_tacc_fin_report_AccountSrc_get", "coa_cd", "coa_desc");

            btNewDetailRow.Visible = false;
            newDetailRow.Visible = false;

            idfinreport = Request.QueryString["idfinreport"];

            if (idfinreport != "" && idfinreport != null)
            {
                txReportID.Text = idfinreport;
                txReportName.Text = bll.vLookUp("select report_name from tacc_fin_report where id='" + idfinreport + "';");
                ddFrType.SelectedValue = bll.vLookUp("select report_type from tacc_fin_report where id='" + idfinreport + "';");
                ddMtdYtd.SelectedValue = bll.vLookUp("select my from tacc_fin_report where id='" + idfinreport + "';");
                ddStatus.Text = bll.vLookUp("select status journal_status from tacc_fin_report where id='" + idfinreport + "';");
                txRemark.Text = bll.vLookUp("select remarks from tacc_fin_report where id='" + idfinreport + "';");

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@reportId", txReportID.Text));
                bll.vBindingComboToSp(ref ddAmountRef, "sp_tacc_fin_report_AmountRef_get", "seq_no", "finreport_desc", arr);

                if (hasApprovalRole)
                {
                    //btapprove.Visible = true;
                    //btreturn.Visible = true;

                    ////temporary for testing, shoould be uncomment false
                    //// ||
                    //// VV
                    ////btNewDetailRow.Visible = false;

                    //dttrnstkDate.Enabled = false;
                    //txtrnstkRemark.Enabled = false;

                    ////LinkButton deleteCommand = (LinkButton)grd.Rows[0].FindControl("ShowDeleteButton");
                    ////deleteCommand.Visible = false;
                    //grd.Columns[7].Visible = false;

                }
                else
                {
                    btsave.Visible = true;
                    btdelete.Visible = true;
                    btNewDetailRow.Visible = true;
                }
            }

            //rdrealnominal.SelectedValue = "R";
            //rddebitcreditbal.SelectedValue = "B";
            //rdisindirectcf.SelectedValue = "0";
            //rdrowattrtype.SelectedValue = "D";
            //rdcalcval.SelectedValue = "A";
            //rddispval.SelectedValue = "A";


            bindinggrd();

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        btNewDetailRow.Visible = false;
        btsave.Visible = false;
        btNewDetailRow.Visible = true;

        if (hasApprovalRole)
        {
            btNewDetailRow.Visible = false;

            txRemark.Enabled = false;

            //LinkButton deleteCommand = (LinkButton)grd.Rows[0].FindControl("ShowDeleteButton");
            //deleteCommand.Visible = false;
            grd.Columns[7].Visible = false;

        }
        else
        {
            btsave.Visible = true;
        }

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
    private void bindinggrd()
    {
        string trnstkNo;
        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@FinRptId", txReportID.Text));

        //arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tacc_fin_report_det_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    //protected void bttmp_Click(object sender, EventArgs e)
    //{
    //    SqlDataReader rs = null;
    //    string sstatus;
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
    //    bll.vGettbltrnstock(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        string sdate = string.Format("{0:d/M/yyyy}", rs["trnstkDate"]);
    //        DateTime dtdate = Convert.ToDateTime(sdate);
    //        string strDate = dtdate.ToString("d/M/yyyy");
    //        sstatus = rs["sta_id"].ToString();
    //        txRemark.Text = rs["trnstkRemark"].ToString();
    //        //chclaim.Checked = Convert.ToBoolean(rs["claim"].ToString());

    //        //Panel1.Visible = true;
    //        //bindinggrddoc();
    //        //{
    //        //    Panel1.Visible = false;
    //        //}

    //        bindinggrd();
    //    } rs.Close();
    //    bindinggrd();
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    //}
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_AccSettingFinanceReport.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        string idfinreport = null; idfinreport = txReportID.Text.ToString();
        string report_name = null; report_name = txReportName.Text.ToString();
        string report_type = null; report_type = ddFrType.SelectedValue.ToString();
        string mtd_ytd = null; mtd_ytd = ddMtdYtd.SelectedValue.ToString();
        string remarks = null; remarks = txRemark.Text.ToString();



        arr.Add(new cArrayList("@idfinreport", idfinreport));
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@report_name", report_name));
        arr.Add(new cArrayList("@report_type", report_type));
        arr.Add(new cArrayList("@my", mtd_ytd));
        arr.Add(new cArrayList("@remarks", remarks));
        bll.vSaveSettingFinReport(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Save journal is successfull','','info');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(idfinreport);

        Response.Redirect("fm_AccSettingFinanceReport_List.aspx");

    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        if (rdrowattrtype.SelectedValue == "D" | rdrowattrtype.SelectedValue == "S")
        {
            ////if (rdrowattrtype.SelectedValue == "D" && rdisindirectcf.SelectedValue == null)
            //if (rdrowattrtype.SelectedValue == "D")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please choose Indirect Cash Flow option first!','','error');", true);
            //    return;
            //}
            if (rdcalcval.SelectedValue == null | rddispval.SelectedValue == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please choose Calculation Value and Display Value option first!','','error');", true);
                return;
            }
        }
            

        List<cArrayList> arr = new List<cArrayList>();

        string idfinreport = null; idfinreport = txReportID.Text.ToString();
        string seq_no = null; seq_no = txseqno.Text.ToString();
        string checkSeqNo = bll.vLookUp("select seq_no from tacc_fin_report_det where seq_no='" + seq_no + "' and fin_rpt_ID='" + idfinreport + "' and status='A';");
        if (checkSeqNo != null && checkSeqNo != "") 
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Sequence Number is duplicate!','','error');", true);
            return;
        }

        string description = null; description = txdescription.Text.ToString();

        if (rdrowattrtype.SelectedValue == null | rdrowattrtype.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please Select Row Attribute Type First!','','error');", true);
            return;
        }
        //string row_attrib_type = ""; row_attrib_type = rdrowattrtype.SelectedItem.Value;
        string row_attrib_type = ""; row_attrib_type = rdrowattrtype.SelectedValue;
        string real_nominal = null;
        string db_cr_bal = null;
        string is_indirect_cf = "";
        string amt_ref = null;
        string acct_src = null;
        string calc_val = null;
        string disp_val = null;
        int is_hidden = 0;
        if (row_attrib_type == "H")
        {
            real_nominal = "";
            db_cr_bal = "";
            is_indirect_cf = "";
            amt_ref = "";
            acct_src = "";
            calc_val = "";
            disp_val = "";
        }
        else {
            //real_nominal = rdrealnominal.SelectedValue;
            //db_cr_bal = rddebitcreditbal.SelectedValue;

            //is_indirect_cf = Convert.ToInt32(rdisindirectcf.SelectedValue.ToString());
            real_nominal = "N";
            db_cr_bal = "B";
            //is_indirect_cf = rdisindirectcf.SelectedValue.ToString();
            amt_ref = txAmountRef.Text.ToString();
            acct_src = txAccountSrc.Text.ToString();
            calc_val = rdcalcval.SelectedValue;
            disp_val = rddispval.SelectedValue;
        }
        if (rdishidden.SelectedValue != null && rdishidden.SelectedValue != "")
        {
            is_hidden = Convert.ToInt32(rdishidden.SelectedValue.ToString()); ;
        }


        arr.Add(new cArrayList("@idfinreport", idfinreport));
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@seq_no", seq_no));
        arr.Add(new cArrayList("@description", description));
        arr.Add(new cArrayList("@real_nominal", real_nominal));
        arr.Add(new cArrayList("@db_cr_bal", db_cr_bal));
        arr.Add(new cArrayList("@is_indirect_cf", is_indirect_cf));
        arr.Add(new cArrayList("@row_attrib_type", row_attrib_type));
        arr.Add(new cArrayList("@amt_ref", amt_ref));
        arr.Add(new cArrayList("@acct_src", acct_src));
        arr.Add(new cArrayList("@calc_val", calc_val));
        arr.Add(new cArrayList("@disp_val", disp_val));
        arr.Add(new cArrayList("@is_hidden", is_hidden));
        bll.vAddRowSettingFinReport(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Add Finance Report detail row is successfull','','info');", true);


        updateText(idfinreport);

        //Response.Redirect("fm_AccSettingFinanceReport.aspx");

    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string userid = Request.Cookies["usr_id"].Value.ToString();
        arr.Add(new cArrayList("@user", userid));
        arr.Add(new cArrayList("@journalid", txReportName.Text));
        //int jdetid=0;
        bll.vDeletefinreport(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txReportName.Text);

        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        ////return;

        ////List<cArrayList> arr = new List<cArrayList>();
        ////arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        ////bll.vDeletetbltrnstock(arr);
        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        ////Response.Redirect("fm_AccSettingFinanceReport.aspx");

        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@journalid", txReportName.Text.ToString()));
        //arr.Add(new cArrayList("@journaldetid", lblseqno.Text.));
        //bll.vDeletemanualjournal(arr);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //Response.Redirect("fm_AccSettingFinanceReport.aspx");

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void btreturn_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@journalid", txReportName.Text));
        //int jdetid=0;
        bll.vReturnmanualjournal(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txReportName.Text);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Return journal is successfull','','info');", true);

        Response.Redirect("fm_AccSettingFinanceReport_List.aspx");
    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@journalid", txReportName.Text));
        bll.vApprovemanualjournal(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txReportName.Text);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Approve journal is successfull','','info');", true);

        Response.Redirect("fm_AccSettingFinanceReport_List.aspx");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        Session["lParamtrnstk"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=trnstk');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }

    protected void btaddNewDetailRow_Click(object sender, EventArgs e)
    {
        newDetailRow.Visible = true;
        rdishidden.SelectedValue = "0";
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
    protected void rdrowattrtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdrowattrtype.SelectedItem.Value == "H" | rdrowattrtype.SelectedItem.Value == "L" | rdrowattrtype.SelectedItem.Value == "S")
        {
            ddAmountRef.Enabled = true;
            txAmountRef.Enabled = true;
            rdcalcval.Enabled = true;
            rddispval.Enabled = true;

            //rdisindirectcf.Enabled = false;
            //rdisindirectcf.SelectedValue=null;
            ddAccountSrc.Enabled = false;
            ddAccountSrc.SelectedValue = "";
            txAccountSrc.Enabled = false;
            txAccountSrc.Text = "";

            if (rdrowattrtype.SelectedItem.Value == "H" | rdrowattrtype.SelectedItem.Value == "L")
            {
                ddAmountRef.Enabled = false;
                ddAmountRef.SelectedValue = "";
                txAmountRef.Enabled = false;
                txAmountRef.Text = "";
                rdcalcval.Enabled = false;
                rdcalcval.SelectedValue = null;
                rddispval.Enabled = false;
                rddispval.SelectedValue = null;
            }
        }
        else{
            //rdisindirectcf.Enabled = true;
            ddAmountRef.Enabled = true;
            txAmountRef.Enabled = true;
            ddAccountSrc.Enabled = true;
            txAccountSrc.Enabled = true;
            rdcalcval.Enabled = true;
            rddispval.Enabled = true;
        }
    }
    protected void txReportName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txReportName.Text != "")
        {
            btNewDetailRow.Visible = true;
        }
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

        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@finrptid", txReportID.Text));

        Label lbfinreportdetid = (Label)grd.Rows[e.RowIndex].FindControl("lbfinreportdetid");
        arr.Add(new cArrayList("@finrptdetid", lbfinreportdetid.Text));

        bll.vDeletefinreportdet(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Info','Row Data Deleted successfully !','success');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        updateText(txReportID.Text);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {    
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
            sBank = AutoCompleteExtender.CreateAutoCompleteItem(rs["bank_cd"].ToString() + " | " + rs["bank_nm"].ToString(), rs["bank_cd"].ToString());
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

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblDebit = (Label)e.Row.FindControl("lblDebit");
    //        Label lblCredit = (Label)e.Row.FindControl("lblCredit");
    //        decimal debit;
    //        decimal credit;

    //        if (!decimal.TryParse(Convert.ToString(lblDebit.Text), out debit))
    //        {
    //            debit = 0;
    //        }
    //        else
    //        {
    //            debit = decimal.Parse(lblDebit.Text);
    //        }
    //        if (!decimal.TryParse(Convert.ToString(lblCredit.Text), out credit))
    //        {
    //            credit = 0;
    //        }
    //        else
    //        {
    //            credit = decimal.Parse(lblCredit.Text);
    //        }
    //    }
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        //sCustType = bll.vLookUp("select top 1 fld_valu from tfield_value where fld_nm='otlbrn'");
    //        Label lblTotalDebit = (Label)e.Row.FindControl("lblTotalDebit");
    //        Label lblTotalCredit = (Label)e.Row.FindControl("lblTotalCredit");
    //    }
    }

    protected void ddAmountRef_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txAmountRef.Text == "")
        {
            txAmountRef.Text = ddAmountRef.SelectedItem.Value.ToString();
        }
        else
        {
            txAmountRef.Text = txAmountRef.Text + ";" + ddAmountRef.SelectedItem.Value.ToString();
        }
    }
    protected void ddAccountSrc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txAccountSrc.Text == "")
        {
            txAccountSrc.Text = ddAccountSrc.SelectedItem.Value.ToString();
        }
        else {
            txAccountSrc.Text = txAccountSrc.Text + ";" + ddAccountSrc.SelectedItem.Value.ToString();
        }
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_AccSettingFinanceReport_List.aspx");
    }
    protected void btdelete_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string userid = Request.Cookies["usr_id"].Value.ToString();
        arr.Add(new cArrayList("@user", userid));
        arr.Add(new cArrayList("@journalid", txReportName.Text));
        bll.vDeletemanualjournal(arr);
        newDetailRow.Visible = false;

        updateText(txReportName.Text);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delete journal is successfull','','info');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        Response.Redirect("fm_AccSettingFinanceReport_List.aspx");
    }
    protected void btdelete_det_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@journalid", txReportName.Text));
        //bll.vDeletemanualjournaldet(arr);
        //newDetailRow.Visible = false;

        //updateText(txReportName.Text);

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delete journal detail is successfull','','info');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }

    protected void updateText(string idFinReport)
    {
        txReportID.Text = bll.vLookUp("select id from tacc_fin_report where id='" + idFinReport + "';");
        txseqno.Text = "";
        txdescription.Text = "";
        rdrowattrtype.SelectedValue = null;
        //rdisindirectcf.SelectedValue = null;
        ddAmountRef.SelectedValue = null;
        txAmountRef.Text = null;
        ddAccountSrc.SelectedValue = null;
        txAccountSrc.Text = "";
        rdcalcval.SelectedValue = null;
        rddispval.SelectedValue = null;
        rdishidden.SelectedValue = null;

        //rdisindirectcf.Enabled = true;
        ddAmountRef.Enabled = true;
        txAmountRef.Enabled = true;
        ddAccountSrc.Enabled = true;
        txAccountSrc.Enabled = true;
        rdcalcval.Enabled = true;
        rddispval.Enabled = true;
    }

    protected void rdrealnominal_SelectedIndexChanged(object sender, EventArgs e)
    {
        btNewDetailRow.Visible = false;
        btsave.Visible = false;
        btNewDetailRow.Visible = true;

        if (hasApprovalRole)
        {
            btNewDetailRow.Visible = false;

            txRemark.Enabled = false;

            //LinkButton deleteCommand = (LinkButton)grd.Rows[0].FindControl("ShowDeleteButton");
            //deleteCommand.Visible = false;
            grd.Columns[7].Visible = false;

        }
        else
        {
            btsave.Visible = true;
        }

    }

}

                    