using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_BAOtherPayment : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void ddlCNDNType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomerForOtherPayment(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btShowInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            if (hdcust.Value == "")
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
            }
            else
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            }
            bll.vBindingGridToSp(ref grd, "sp_contract_invoiceOtherPayment_get", arr);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grd.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
            if (hdcust.Value == "")
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
            }
            else
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            }
            bll.vBindingGridToSp(ref grd, "sp_contract_invoiceOtherPayment_get", arr);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            CheckBox chonepct = (CheckBox)grd.Rows[e.RowIndex].FindControl("chonepct");
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        CheckBox chonepct = (CheckBox)grd.Rows[e.NewSelectedIndex].FindControl("chonepct");
        
        
            grd.DataSource = null;
            grd.DataBind();
        
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grd.EditIndex = -1;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            if (hdcust.Value == "")
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
            }
            else
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            }
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowCancelingEdit", "Exception", ex.Message + ex.InnerException);
        }

    }
    protected void chonepct_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow gr = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
        //GridViewRow grBA = (GridViewRow)((DataControlFieldCell)((Label)sender).Parent).Parent;
        //find the control in that
        Label lblcontract_no = (Label)gr.FindControl("lblcontract_no");
        Label lblOtherPayment = (Label)gr.FindControl("lblOtherPayment");
        Label lblBalance = (Label)gr.FindControl("lblBalance");
        CheckBox chonepct = (CheckBox)gr.FindControl("chonepct");
        bool selectedvalue = chonepct.Checked;
        if (selectedvalue == true && lblBalance!=null)
        {
            decimal percent = (Convert.ToDecimal(lblBalance.Text) * 1) / 100;
            lblOtherPayment.Text = String.Format("{0:0.00}", percent);
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "chonepct")
        {

        }
    }
}