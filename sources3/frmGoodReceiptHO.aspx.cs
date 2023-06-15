using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class frmGoodReceiptHO : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@receipt_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelGoodReceiptHODtl2(arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", "0"));
            arr.Add(new cArrayList("@whs_typ", "L"));
            bll.vBindingComboToSp(ref cbwhs_vt_cd, "sp_tmst_warehouse_get2", "whs_cd", "whs_nm",arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", "0"));
            arr.Add(new cArrayList("@whs_typ", "P"));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get2", "whs_cd", "whs_nm", arr);
            arr.Clear();
            arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
            bll.vBindingComboToSp(ref cbbin_cd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            bindinggrd();
            txreceipt_dt.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingFieldValueToCombo(ref cbsta_id, "sta_id");
            cbsta_id.SelectedValue = "N";
            //bll.vBindingComboToSp(ref cbwhs_vt_cd, "sp_tmst_item_get4", "ID", "ItemName");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    private void bindinggrd()
    {
    totalQty = 0;
    string receipt_no;
    if (txreceipt_no.Text == "" || txreceipt_no.Text == "NEW") { receipt_no = Request.Cookies["usr_id"].Value.ToString(); } else { receipt_no = txreceipt_no.Text; }
    List<cArrayList> arr = new List<cArrayList>();
    arr.Add(new cArrayList("@receipt_no", receipt_no));
    bll.vBindingGridToSp(ref grd, "sp_tgoodreceiptho_dtl_get", arr);
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListTo_po_branch(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sto_nm = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lto_nm = new List<string>();
        arr.Add(new cArrayList("@to_nm", prefixText));
        arr.Add(new cArrayList("@po_no", null));
        bll.vSearchtmst_po2(arr, ref rs);
        while (rs.Read())
        {
            sto_nm = AutoCompleteExtender.CreateAutoCompleteItem(rs["po_no"].ToString() + " | " + rs["to_nm"].ToString(), rs["po_no"].ToString());
            lto_nm.Add(sto_nm);
        } rs.Close();
        return (lto_nm.ToArray());
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        string receipt_no;
        if (cbsta_id.SelectedValue == "C")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
            return;
        }

        txsearchitemPnl.CssClass = "";
        txqtyPnl.CssClass = "";

        if (hditem.Value == "" || txqty.Text == "" || txqty.Text == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Item Name and quantity must be fill','error');", true);
            if (txsearchitem.Text == ""){
                txsearchitemPnl.CssClass = "error";
            }
            if (txqty.Text == "" || txqty.Text == "0"){
                txqtyPnl.CssClass = "error";  
            }
            return;
        }
        if (txreceipt_no.Text == "" || txreceipt_no.Text == "NEW") { receipt_no = Request.Cookies["usr_id"].Value.ToString(); } else { receipt_no = txreceipt_no.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", receipt_no));
        arr.Add(new cArrayList("@item_cd", hditem.Value));
        arr.Add(new cArrayList("@item_vendor_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue));
        bll.vInserttgoodreceiptho_dtl1(arr);
        clearAdd();
        txsearchitem.Focus();
        bindinggrd();
    }
    void clearAdd()
    {
        txsearchitem.Text = "";
        hditem.Value = "";
       // txitem_vendor_cd.Text = "";
        hditem.Value = "";
        txqty.Text = "0";
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListitem(string prefixText, int count, string contextKey)
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
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmGoodReceiptHO.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (cbsta_id.SelectedValue == "C")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
            return;
        }
        //if  (Request.Cookies["waz_dt"].Value.ToString()!=txreceipt_dt.Text)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
        //    return;
        //}
        if (txreceipt_dt.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Receipt Date must be fill','error');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Receipt Date must be fill')", true);
            return;
        }
        if (txref_no.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Reference no can not empty','Reference Number','warning');", true);
            return;
        }

        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is no item to be receipt','Item must more than zeor','warning');", true);
            return;
        }

        if (txreceipt_no.Text == null || txreceipt_no.Text == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sreceipt_no = "0";
            arr.Add(new cArrayList("@receipt_dt", DateTime.ParseExact(txreceipt_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@whs_vt_cd",cbwhs_vt_cd.SelectedValue));
            arr.Add(new cArrayList("@whs_cd",cbwhs_cd.SelectedValue));
            arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
            arr.Add(new cArrayList("@to_po_branch",hdto_po_branch.Value));
            arr.Add(new cArrayList("@ref_no",txref_no.Text));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInserttmst_goodreceiptho_ins(arr, ref sreceipt_no);
            txreceipt_no.Text = sreceipt_no;

        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@receipt_no",txreceipt_no.Text));
            arr.Add(new cArrayList("@receipt_dt", DateTime.ParseExact(txreceipt_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@whs_vt_cd", cbwhs_vt_cd.SelectedValue));
            arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
            arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
            arr.Add(new cArrayList("@to_po_branch", hdto_po_branch.Value));
            arr.Add(new cArrayList("@ref_no", txref_no.Text));
            bll.vUpdatetmst_goodreceiptho(arr);
        }
        bindinggrd();
        btsave.CssClass = "divhid";
        btprint.CssClass = "btn btn-info btn-print";

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data has been saved', 'Good Receipt No : " + txreceipt_no.Text + "', 'success');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Save successfully !')", true);
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (cbsta_id.SelectedValue == "C")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
            return;
        }
        if (txref_no.Text != "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@receipt_no", txreceipt_no.Text));
            bll.vDeletetmst_goodreceiptho(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Deleted successfully !')", true);
            Response.Redirect("frmGoodReceiptHO.aspx");
        }
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txreceipt_no.Text = Convert.ToString(Session["looreceipt_no"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", txreceipt_no.Text));
        bll.vGettmst_goodreceiptho(arr, ref rs);
        while (rs.Read())
        {
            txreceipt_dt.Text = string.Format("{0:d/M/yyyy}", rs["receipt_dt"]);
            cbwhs_vt_cd.SelectedValue = rs["whs_vt_cd"].ToString();
            cbwhs_cd.SelectedValue = rs["whs_cd"].ToString();
            cbbin_cd.SelectedValue = rs["bin_cd"].ToString();
            txsearchTo_po_branch.Text = rs["to_po_branch"].ToString() + " | " + bll.vLookUp("select  to_nm from tmst_po where po_no='" + rs["to_po_branch"].ToString() + "'");
            hdto_po_branch.Value = rs["to_po_branch"].ToString();
            txref_no.Text = rs["ref_no"].ToString();
            cbsta_id.SelectedValue = rs["sta_id"].ToString();
        } rs.Close();
        bindinggrd();
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbitem_vendor_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitem_vendor_cd");
        Label lbitem_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitem_cd");
        Label lbitem_nm = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitem_nm");
        Label lbuom = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbuom");
        Label lbqty = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbqty");

        //txitem_vendor_cd.Text = lbitem_vendor_cd.Text;
        txsearchitem.Text = lbitem_cd.Text + " | " + lbitem_nm.Text;
        hditem.Value = lbitem_cd.Text;
        cbuom.SelectedValue = lbuom.Text;
        txqty.Text = lbqty.Text;  
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (cbsta_id.SelectedValue == "C")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not delete','error');", true);
            return;
        }
        else if (Request.Cookies["waz_dt"].Value.ToString() != txreceipt_dt.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            return;
        }   
        else
        { 
        Label lbreceipt_no = (Label)grd.Rows[e.RowIndex].FindControl("lbreceipt_no");
        Label lbitem_cd = (Label)grd.Rows[e.RowIndex].FindControl("lbitem_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", lbreceipt_no.Text));
        arr.Add(new cArrayList("@item_cd", lbitem_cd.Text));
        bll.vDeletetgoodreceiptho_dtl(arr);
        bindinggrd();
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", txreceipt_no.Text));
        Session["lParamGRHO"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=GRHO');", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (cbsta_id.SelectedValue == "C")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
            grd.EditIndex = -1;
            return;
        }
        //if (Request.Cookies["waz_dt"].Value.ToString() != txreceipt_dt.Text)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
        //    return;
        //}
        //Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
        //List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = e.NewEditIndex;
        //DropDownList cboUOM = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cboUOM");
        //bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
        //cboUOM.SelectedValue = lblUOM.Text;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbreceipt_no = (Label)grd.Rows[e.RowIndex].FindControl("lbreceipt_no");
        Label lbitem_cd = (Label)grd.Rows[e.RowIndex].FindControl("lbitem_cd");
        TextBox txtqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txtqty");
        TextBox txtitem_vendor_cd = (TextBox)grd.Rows[e.RowIndex].FindControl("txtitem_vendor_cd");
        //DropDownList cboUOM = (DropDownList)grd.Rows[e.RowIndex].FindControl("cboUOM");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", lbreceipt_no.Text));
        arr.Add(new cArrayList("@item_cd", lbitem_cd.Text));
        arr.Add(new cArrayList("@item_vendor_cd", txtitem_vendor_cd.Text));
        arr.Add(new cArrayList("@qty", txtqty.Text));
        //arr.Add(new cArrayList("@uom", cboUOM.SelectedValue.ToString()));
        bll.vUpdatetgoodreceiptho_dtl(arr);
        grd.EditIndex = -1; arr.Clear();
        bindinggrd();
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            decimal qty;
            if (lbqty != null)
                qty = decimal.Parse(lbqty.Text);
            else
                qty = 0;
            totalQty = totalQty + qty;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            lblTotalqty.Text = totalQty.ToString("#,##0.00");
        }
    }

    protected void txsearchTo_po_branch_TextChanged(object sender, EventArgs e)
    {
        //string confirmValue = Request.Form["confirm_value"];
        //if (confirmValue == "Yes" || confirmValue == "Yes,Yes" || confirmValue == "Yes,Yes,Yes") { confirmValue = "Yes"; }
        string receipt_no, to_po_branch = hdto_po_branch.Value;

        // Check For NE Special Customer
        string customer = "", typecustomer = ""; string[] arrCustomer;
        customer = bll.vLookUp("select to_nm from tmst_po where po_no = '" + hdto_po_branch.Value.ToString() + "'");
        arrCustomer = customer.Replace(" ", "").Split('-');
        typecustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd = '" + arrCustomer[0] + "'");

        if (typecustomer == "SP")
        {
            btsave.Visible = false;
            btprint.Visible = false;
            btDelete.Visible = false;
            btadd.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction for Special Customer, Directly go to menu Delivery Order -> Loading Main WH','error');", true);
            return;
        }
        else
        {
            if (txreceipt_no.Text == null || txreceipt_no.Text == "") { receipt_no = ""; } else { receipt_no = txreceipt_no.Text; }
            //if (confirmValue == "Yes" && receipt_no == "" && hdto_po_branch.Value.Length > 0)
            //{
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@to_po_branch", hdto_po_branch.Value));
            bll.vInserttgoodreceiptho_dtl_to_po_branch(arr);
            bindinggrd();
        }
        // Check For NE Special Customer
        
        //}
        //else
        //{
        //    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        //}
    }
    protected void btserachpo_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookup_pobranch.aspx');", true);
    }
    protected void cbsource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbsource.SelectedValue.ToString() == "P")
        { 
            txsearchTo_po_branch.CssClass = "form-control divnormal";
            txsearchTo_po_branch.Enabled = true;
            btserachpo.CssClass = "btn btn-primary btn-search";
            btserachpo.Enabled = true;
        }
        else { 
            txsearchTo_po_branch.CssClass = "ro";
            txsearchTo_po_branch.Enabled = false;
            btserachpo.CssClass = "ro";
            btserachpo.Enabled = false;
        }
    }
}