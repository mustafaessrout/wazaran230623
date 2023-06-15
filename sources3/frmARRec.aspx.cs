using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class frmARRec : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@qry_CD","SalesCD"));
            bll.vBindingComboToSp(ref cbSalesCD, "sp_tmst_employee_sal_get", "emp_cd", "emp_desc", arr);

            bll.vBindingFieldValueToCombo(ref cbARCType, "pmtType");
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
            bll.vBindingComboToSp(ref cbBankID, "sp_tblBank_get", "BankID", "banViewName", arr);

            paymentTypeDataUnVisible();
            clearAdd();
            txrecDate.Text = DateTime.Now.ToString("d/M/yyyy");
            //DateTime.ParseExact(DateTime.Now.ToString, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        }
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        if (txARRecCD.Text == "")
        {
            hdrsave(sender, e);
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        //arr.Add(new cArrayList("@ARCType", cbARCType.SelectedValue));
        arr.Add(new cArrayList("@ARCRefID",cbARCRefID.SelectedValue));
        arr.Add(new cArrayList("@ARCAmt", txARCAmt.Text));
        //arr.Add(new cArrayList("@ARCCashAmt", txARCCashAmt.Text));
        //arr.Add(new cArrayList("@ARCTranferAmt", txARCTranferAmt.Text));
        //arr.Add(new cArrayList("@ARCChequeAmt", txARCChequeAmt.Text));
        //arr.Add(new cArrayList("@CustCD", hdcust_cd.Value));
        //arr.Add(new cArrayList("@ARCDocNo", txrscDocNo.Text));
        //arr.Add(new cArrayList("@BankID", cbBankID.Text));
        //arr.Add(new cArrayList("@ARCDate", txARCDate.Text));
        //arr.Add(new cArrayList("@ARCDueDate", txARCDueDate.Text));
        arr.Add(new cArrayList("@ARCDescription", txARCDescription.Text));
        bll.vInsertTblARRecDtl(arr);
        clearAdd();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblARRecDtl_get", arr);
        getInvoice();
        txsearchCust.Focus();
    }
    void clearAdd()
    {
       txARCAmt.Text = "0";
       txrscDocNo.Text = "";
       txrscDocDate.Text = "";
       txrscDueDate.Text = "";
       txARCDescription.Text = "";
    }
    protected void hdrsave(object sender, EventArgs e)
    {
        if (txKey.Text == null || txKey.Text == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sARRecCD = "0";
            DateTime dt = DateTime.ParseExact(txrecDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            arr.Add(new cArrayList("@recDate", dt));
            arr.Add(new cArrayList("@SalesCD", cbSalesCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@ARCType",cbARCType.SelectedValue ));
            arr.Add(new cArrayList("@CustCD",hdcust_cd.Value));
            bll.vInsertTblARRec(arr, ref sARRecCD);
            txARRecCD.Text = sARRecCD;
            txKey.Text = bll.vLookUp("select ARRecID from tblARRec where ARRecCD='" + sARRecCD + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'");
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@ARRecID", txKey.Text));
            bll.vBindingGridToSp(ref grd, "sp_tblARRecDtl_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@ARRecID", txKey.Text));
            bll.vBindingGridToSp(ref grdCH, "sp_tblARRecDtlCH_get", arr);

        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@ARRecID", txKey.Text));
            arr.Add(new cArrayList("@recDate", DateTime.ParseExact(txrecDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@SalesCD", cbSalesCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@ARCType", cbARCType.SelectedValue));
            arr.Add(new cArrayList("@CustCD", hdcust_cd.Value));
            bll.vUpdateTblARRec(arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@ARRecID", txKey.Text));
            bll.vBindingGridToSp(ref grd, "sp_tblARRecDtl_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@ARRecID", txKey.Text));
            bll.vBindingGridToSp(ref grdCH, "sp_tblARRecDtlCH_get", arr);
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCust(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lCust = new List<string>();
        arr.Add(new cArrayList("@SalesPointCD", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@cust_nm", prefixText));
        bll.vSearchMstCust2(arr, ref rs);
        while (rs.Read())
        {
            sCust = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " | " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        } rs.Close();
        return (lCust.ToArray());
    }
    protected void txsearchCust_TextChanged(object sender, EventArgs e)
    {
        getInvoice();
        getAmt();
    }
    void getInvoice()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@CustCD", hdcust_cd.Value));
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.Text));
        bll.vBindingComboToSp(ref cbARCRefID, "sp_InvoiceAging_get", "SOID", "InvDescription", arr);
    }
    void getAmt()
    {
        string scust_cd = "0";
        string sARCRefID = "0";
        if (hdcust_cd.Value == "") 
        {
            scust_cd = "0";
        }
        else
        {
            scust_cd = hdcust_cd.Value;
        }
        if (cbARCRefID.SelectedValue == "")
        {
            scust_cd = "0";
        }
        else
        {
            sARCRefID = cbARCRefID.SelectedValue;
        }

        txARCAmt.Text = bll.vLookUp("select [dbo].[sfnGetInvoiceAgingAmt] (" + cbSalesPointCD.Text + "," + scust_cd + "," + sARCRefID + ")");

    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txKey.Text = Convert.ToString(Session["looARRecID"]);
        cbSalesPointCD.SelectedValue = Convert.ToString(Session["looARRecSalespointCD"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vGettblARRec(arr, ref rs);
        while (rs.Read())
        {
            txARRecCD.Text = rs["ARRecCD"].ToString();
            txrecDate.Text = string.Format("{0:d/M/yyyy}", rs["recDate"]);
            cbSalesCD.SelectedValue = rs["salesCD"].ToString();
            cbSalesPointCD.SelectedValue = rs["SalesPointCD"].ToString();
            hdcust_cd.Value = rs["CustCD"].ToString();
            cbARCType.SelectedValue = rs["ARCType"].ToString();
            txsearchCust.Text = rs["cust_cd"].ToString() + " | " + rs["cust_nm"].ToString();
        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblARRecDtl_get", arr);
        clearAdd();
        cbARCType_SelectedIndexChanged(sender, e);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vBindingGridToSp(ref grdCH, "sp_tblARRecDtlCH_get", arr);


    }
    protected void bttmp2_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txKey.Text = Convert.ToString(Session["looARRecID"]);
        cbSalesPointCD.SelectedValue = Convert.ToString(Session["looARRecSalespointCD"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vGettblARRec(arr, ref rs);
        while (rs.Read())
        {
            txARRecCD.Text = rs["ARRecCD"].ToString();
            txrecDate.Text = Convert.ToDateTime(rs["recDate"]).ToShortDateString();
            cbSalesCD.SelectedValue = rs["salesCD"].ToString();
            cbSalesPointCD.SelectedValue = rs["SalesPointCD"].ToString();
        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblARRecDtl_get", arr);
        clearAdd();
        cbARCType_SelectedIndexChanged(sender, e);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vBindingGridToSp(ref grdCH, "sp_tblARRecDtlCH_get", arr);
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblSeqID = (Label)grd.Rows[e.RowIndex].FindControl("lblSeqID");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SeqID", lblSeqID.Text));
        bll.vDeletetblARRecDet(arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblARRecDtl_get", arr);
        getInvoice();

    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //Label lblARCCD = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblARCCD");
        Label lblARCRefID = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblARCRefID");
        //Label lblCustCD = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblCustCD");
        Label lblcust_nm = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblcust_nm");
        Label lblARCAmt = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblARCAmt");
        //Label lblBankID = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblBankID");
        //Label lblARCDocNo = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblARCDocNo");
        //Label lblARCDate = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblARCDate");
        //Label lblARCDueDate = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblARCDueDate");
        Label lblARCDescription = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblARCDescription");

        //txsearchCust.Text = lblCustCD.Text + " | " + lblcust_nm.Text;
        //hdcust_cd.Value = lblCustCD.Text;
        //txrARCCD.Text = lblARCCD.Text;
        cbARCRefID.SelectedValue= lblARCRefID.Text;
        txARCAmt.Text = lblARCAmt.Text;
        //cbBankID.Text = lblBankID.Text;
        //txrscDocNo.Text = lblARCDocNo.Text;
        //txARCDate.Text = lblARCDate.Text;
        //txARCDueDate.Text = lblARCDueDate.Text;
        txARCDescription.Text = lblARCDescription.Text;

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        hdrsave(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data saved successfully !')", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmARRec.aspx");
    }
    protected void cbARCRefID_TextChanged(object sender, EventArgs e)
    {
        getAmt();
    }
    protected void cbARCType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cbARCType.Text == "2")
        {
            paymentTypeDataVisible();
            txrscDueDate.Visible = false;
            lbrscDueDate.Visible = false;

        }
        else if (cbARCType.Text == "3")
        {
            paymentTypeDataVisible();
            

        }
        else
        {
            paymentTypeDataUnVisible();

        }
    }
    
    void paymentTypeDataUnVisible()
    {

        cbBankID.Visible = false;
        lbBankID.Visible = false;
        txrscDocNo.Visible = false;
        lbrscDocNo.Visible = false;
        txrscDocDate.Visible = false;
        lbrscDocDate.Visible = false;
        txrscDueDate.Visible = false;
        lbrscDueDate.Visible = false;
        lbrscAmount.Visible = false;
        txrscAmount.Visible = false;
        btAddCH.Visible = false;
        txrscDocNo.Text = "";
        txrscDocDate.Text = "";
        txrscDocDate.Text = "";
        txrscAmount.Text = "0";
        grdCH.Visible = false;

        

    }
    void paymentTypeDataVisible()
    {

        cbBankID.Visible = true;
        lbBankID.Visible = true;
        txrscDocNo.Visible = true;
        lbrscDocNo.Visible = true;
        txrscDocDate.Visible = true;
        lbrscDocDate.Visible = true;
        txrscDueDate.Visible = true;
        lbrscDueDate.Visible = true;
        lbrscAmount.Visible = true;
        txrscAmount.Visible = true;
        btAddCH.Visible = true;
        txrscDocNo.Text = "";
        txrscDocDate.Text = "";
        txrscDocDate.Text = "";
        txrscAmount.Text = "0";
        grdCH.Visible = true;



    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        
        List<string> arr = new List<string>();
        arr.Add("{tblARRec.ARRecCD} = '" + txARRecCD.Text + "'");
        Session["lformulaARRec"] = arr;
        Response.Redirect("fm_report.aspx?src=ARRec");
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        bll.vDeleteTblARRec(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Deleted successfully !')", true);
        Response.Redirect("frmARRec.aspx");
    }
    protected void btAddCH_Click(object sender, EventArgs e)
    {
        if (txARRecCD.Text == "")
        {
            hdrsave(sender, e);
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@rscTransDate", DateTime.ParseExact(txrecDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
	    arr.Add(new cArrayList("@BankID",cbBankID.SelectedValue));
	    arr.Add(new cArrayList("@rscDocNo",txrscDocNo.Text));
	    arr.Add(new cArrayList("@rscDocDate",DateTime.ParseExact(txrscDocDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@rscDueDate", DateTime.ParseExact(txrscDueDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
	    arr.Add(new cArrayList("@rscAmount",txrscAmount.Text));
        bll.vInsertTblARRecDtlCH(arr);
        clearAdd();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vBindingGridToSp(ref grdCH, "sp_tblARRecDtlCH_get", arr);
        txrscDocNo.Focus();
   
    }
    protected void grdCH_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblSeqID = (Label)grdCH.Rows[e.RowIndex].FindControl("lblSeqID");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SeqID", lblSeqID.Text));
        bll.vDeletetblARRecDetCH(arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ARRecID", txKey.Text));
        bll.vBindingGridToSp(ref grdCH, "sp_tblARRecDtlCH_get", arr);
    }
    protected void grdCH_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {


        Label lblBankID = (Label)grdCH.Rows[e.NewSelectedIndex].FindControl("lblBankID");
        Label lblrscDocNo = (Label)grdCH.Rows[e.NewSelectedIndex].FindControl("lblrscDocNo");
        Label lblrscDocDate = (Label)grdCH.Rows[e.NewSelectedIndex].FindControl("lblrscDocDate");
        Label lblrscDueDate = (Label)grdCH.Rows[e.NewSelectedIndex].FindControl("lblrscDueDate");
        Label lblrscAmount = (Label)grdCH.Rows[e.NewSelectedIndex].FindControl("lblrscAmount");


        cbBankID.Text = lblBankID.Text;
        txrscDocNo.Text = lblrscDocNo.Text;
        txrscDocDate.Text = lblrscDocDate.Text;
        txrscDueDate.Text = lblrscDueDate.Text;
        txrscAmount.Text = lblrscAmount.Text;
  
    }
}