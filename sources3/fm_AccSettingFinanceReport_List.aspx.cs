using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_AccSettingFinanceReport_List : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    decimal totalDebit = 0;
    decimal totalCredit= 0;
    public static int PreviousIndex;
    Boolean hasApprovalRole = false;
    Boolean isEmptyGrid = false;
    string userid = null;
    //string userid2 = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string userid = Request.Cookies["usr_id"].Value.ToString();
        //string userid = "2833";

        //userid2 = bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'");
        //if (bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'") != "")
        if (bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'") != "")
        {
            hasApprovalRole = true;
        }

        if (!IsPostBack)
        {
            bindinggrd(hasApprovalRole, isEmptyGrid); 
        }

        if (hasApprovalRole)
        {
            btaddfinreport.Visible = false;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void cbjournaltrantype_SelectedIndexChanged(object sender, EventArgs e)
    {
        getLatestJournalNo();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (grd.Rows.Count == 0)
            {
                Button b = (Button)e.Row.Cells[6].Controls[2];
                b.Visible = false;
            }
        }
    }
    private void getLatestJournalNo()
    {

        bindinggrd(hasApprovalRole, isEmptyGrid);
    
    }
    private void bindinggrd(Boolean hasApprovalRole, Boolean isEmptyGrid)
    {
        totalDebit = 0;
        totalCredit = 0;
        string trnstkNo;
        List<cArrayList> arr = new List<cArrayList>();

            
        string isEmptyGridStr = string.Empty;
        arr.Add(new cArrayList("@isEmptyGridStr", isEmptyGridStr));
        bll.vBindingGridToSp(ref grd, "sp_tacc_fin_report_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        arr.Clear();
        bll.vIsFinreportEmptyGrid(arr, ref isEmptyGridStr);
        if (isEmptyGridStr == "true")
        {
            isEmptyGrid = true;
        }

        if (isEmptyGrid)
        {
            grd.Columns[9].Visible = false;
        }
    }
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
            //cbSalesPointCD.SelectedValue = rs["salespointcd"].ToString();
            //chclaim.Checked = Convert.ToBoolean(rs["claim"].ToString());

            //Panel1.Visible = true;
            //bindinggrddoc();
            //{
            //    Panel1.Visible = false;
            //}

            bindinggrd(hasApprovalRole,isEmptyGrid);
        } rs.Close();
        bindinggrd(hasApprovalRole,isEmptyGrid);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    //protected void btview_Click(object sender, EventArgs e, GridViewUpdateEventArgs g)
    //{
    //    Label lblidjournal = (Label)grd.Rows[g.RowIndex].FindControl("lblidjournal");
    //    Response.Redirect("fm_accManualJournal.aspx?journalId" + lblidjournal);
    //}

    protected void grd_RowSelect(object sender, GridViewUpdateEventArgs g)
    {
        Label lblidjournal = (Label)grd.Rows[g.RowIndex].FindControl("lblidjournal");
        Response.Redirect("fm_accManualJournal.aspx?journalId" + lblidjournal);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_accManualJournal.aspx");
    }
    protected void btapprove_Click(object sender, EventArgs e)
    { 
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
  
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
        Session["lParamtrnstk"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=trnstk');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {

    }
    protected void btaddNewDetailRow_Click(object sender, EventArgs e)
    {
    }
    protected void btCancelAddRow_Click(object sender, EventArgs e)
    {
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
        bindinggrd(hasApprovalRole,isEmptyGrid);
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
        arr.Add(new cArrayList("@trnstkNo", lbltrnstkNo.Text));
        arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@qty", txtqty.Text));
        arr.Add(new cArrayList("@uom", cboUOM.SelectedValue));
        bll.vUpdatetbltrnstockDtl(arr);
        grd.EditIndex = -1; arr.Clear();
        bindinggrd(hasApprovalRole,isEmptyGrid);
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
        //bindinggrd(hasApprovalRole,isEmptyGrid);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        Label lblseqno = (Label)grd.Rows[e.RowIndex].FindControl("lblseqno");
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@journalid", txJournalId.Text.ToString()));
        //arr.Add(new cArrayList("@journaldetid", lblseqno.ToString()));
        //bll.vDeletemanualjournal(arr);
        bindinggrd(hasApprovalRole,isEmptyGrid);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be edit','error');", true);
        //return;

        //Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
        //grd.EditIndex = e.NewEditIndex;
        //bindinggrd(hasApprovalRole,isEmptyGrid);
        //DropDownList cboUOM = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cboUOM");
        //bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
        //cboUOM.SelectedValue = lblUOM.Text;
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be edit','error');", true);
        return;

        Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
        grd.EditIndex = e.NewEditIndex;
        bindinggrd(hasApprovalRole,isEmptyGrid);
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
        bindinggrd(hasApprovalRole,isEmptyGrid);
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
        
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblDebit = (Label)e.Row.FindControl("lblDebit");
        //    Label lblCredit = (Label)e.Row.FindControl("lblCredit");
        //    decimal debit;
        //    decimal credit;

        //    if (!decimal.TryParse(Convert.ToString(lblDebit.Text), out debit))
        //    {
        //        debit = 0;
        //    }
        //    else
        //    {
        //        debit = decimal.Parse(lblDebit.Text);
        //    }
        //    if (!decimal.TryParse(Convert.ToString(lblCredit.Text), out credit))
        //    {
        //        credit = 0;
        //    }
        //    else
        //    {
        //        credit = decimal.Parse(lblCredit.Text);
        //    }
        //    totalDebit = totalDebit + debit;
        //    totalCredit = totalCredit + credit;
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    //sCustType = bll.vLookUp("select top 1 fld_valu from tfield_value where fld_nm='otlbrn'");
        //    Label lblTotalDebit = (Label)e.Row.FindControl("lblTotalDebit");
        //    Label lblTotalCredit = (Label)e.Row.FindControl("lblTotalCredit");
        //    lblTotalDebit.Text = totalDebit.ToString("#,##0.00");
        //    lblTotalCredit.Text = totalCredit.ToString("#,##0.00");
        //}
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
    }
    private void getprice2()
    {
    }
    protected void btaddfinreport_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_AccSettingFinanceReport.aspx");
    }
}

                    