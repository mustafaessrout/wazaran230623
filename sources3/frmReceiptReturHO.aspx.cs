using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class frmReceiptReturHO : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0;
    decimal totalAmount = 0;
    decimal totalQty_ship = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txrecRetHO_no.Text = "NEW"; txrecRetHO_no.ReadOnly = true;
            txrecRetHO_dt.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            bll.vBindingFieldValueToCombo(ref cbrecRetHO_type, "returho_type");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", 0));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            cbwhs_cd_SelectedIndexChanged(sender, e);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
   
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txrecRetHO_no.Text = Convert.ToString(Session["loorecRetHO_no"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@recRetHO_no", txrecRetHO_no.Text));
        bll.vGettblRecReturnHO(arr, ref rs);
        while (rs.Read())
        {
            txrecRetHO_manual_no.Text = rs["recRetHO_manual_no"].ToString();
            txreturho_no.Text = rs["returho_no"].ToString();
            hdreturho_no.Value = rs["returho_no"].ToString();
            hdsalespointcd.Value=rs["salespointcd"].ToString();
            if (hdsalespointcd.Value != "") { cbSalesPointCD.SelectedValue = hdsalespointcd.Value; }
            txrecRetHO_dt.Text = string.Format("{0:d/M/yyyy}", rs["recRetHO_dt"]);
            cbrecRetHO_type.SelectedValue = rs["recRetHO_type"].ToString();
            cbwhs_cd.SelectedValue = rs["whs_cd"].ToString();
            cbbin_cd.SelectedValue = rs["bin_cd"].ToString();
            txrecRetHO_note.Text = rs["recRetHO_note"].ToString();
        } rs.Close();
        bindinggrd();
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void cbSalesPointCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        txreturho_no_AutoCompleteExtender.ContextKey = cbSalesPointCD.SelectedValue.ToString();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListreturho_no(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sreturho_no = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lreturho_no = new List<string>();
        arr.Add(new cArrayList("@returho_no2", prefixText));
        bll.vSearchtmst_returho(arr, ref rs);
        while (rs.Read())
        {
            sreturho_no = AutoCompleteExtender.CreateAutoCompleteItem(rs["returho_no"].ToString() + " | " + rs["salespointcd"].ToString(), rs["returho_no2"].ToString());
            lreturho_no.Add(sreturho_no);
        } rs.Close();
        return (lreturho_no.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListitem(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void txreturho_no_TextChanged(object sender, EventArgs e)
    {
        if (hdreturho_no.Value!=""){
            cbSalesPointCD.SelectedValue = hdsalespointcd.Value;
            cbrecRetHO_type.SelectedValue = bll.vLookUp("select returho_type from tmst_returho where returho_no='" + hdreturho_no.Value + "'");
            bindinggrd();
        }

    }
    protected void cbwhs_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbin_cd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }

    protected void cbreturho_type_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void bindinggrd()
    {
        totalQty = 0;
        totalAmount = 0;
        totalQty_ship = 0;
        string recreturno;
        if (hdreturho_no.Value == "") { recreturno = Request.Cookies["usr_id"].Value.ToString(); } else { recreturno = hdreturho_no.Value; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@returho_no", recreturno));
        bll.vBindingGridToSp(ref grd, "sp_treturho_dtl_get", arr);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmReceiptReturHO.aspx");
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (txrecRetHO_no.Text != "" || txrecRetHO_no.Text != "NEW")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@recRetHO_no", txrecRetHO_no.Text));
            Session["lParamrecRetHO"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=recRetHO');", true);
        }
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbIDS = (Label)grd.Rows[e.RowIndex].FindControl("lbIDS");
        TextBox txqty_ship = (TextBox)grd.Rows[e.RowIndex].FindControl("txqty_ship");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@IDS", lbIDS.Text));
        arr.Add(new cArrayList("@qty_ship", txqty_ship.Text));
        bll.vUpdatetreturho_dtl_rec(arr);
        grd.EditIndex = -1; 
        bindinggrd();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {

        if (Request.Cookies["waz_dt"].Value.ToString() != txrecRetHO_dt.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            grd.EditIndex = -1;
            return;
        }
        grd.EditIndex = e.NewEditIndex;
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
 
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != txrecRetHO_dt.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            return;
        }
        if (txrecRetHO_dt.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Receipt Date must be fill')", true);
            return;
        }
        if (txrecRetHO_no.Text == "NEW" || txrecRetHO_no.Text == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            string srecRetHO_no = "0";
            arr.Add(new cArrayList("@recRetHO_manual_no",txrecRetHO_manual_no.Text));
            arr.Add(new cArrayList("@returho_no",hdreturho_no.Value));
            arr.Add(new cArrayList("@salespointcd",hdsalespointcd.Value));
            arr.Add(new cArrayList("@recRetHO_dt", DateTime.ParseExact(txrecRetHO_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@recRetHO_type",cbrecRetHO_type.SelectedValue));
            arr.Add(new cArrayList("@whs_cd",cbwhs_cd.SelectedValue));
            arr.Add(new cArrayList("@bin_cd",cbbin_cd.SelectedValue));
            arr.Add(new cArrayList("@recRetHO_note",txrecRetHO_note.Text));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInserttblRecReturnHO_ins(arr, ref srecRetHO_no);
            txrecRetHO_no.Text = srecRetHO_no;

        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@recRetHO_no", txrecRetHO_no.Text));
            arr.Add(new cArrayList("@recRetHO_manual_no", txrecRetHO_manual_no.Text));
            arr.Add(new cArrayList("@returho_no", hdreturho_no.Value));
            arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value));
            arr.Add(new cArrayList("@recRetHO_dt", DateTime.ParseExact(txrecRetHO_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@recRetHO_type", cbrecRetHO_type.SelectedValue));
            arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
            arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
            arr.Add(new cArrayList("@recRetHO_note", txrecRetHO_note.Text));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdatetblRecReturnHO(arr);
        }
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','Rec Return No. " + txrecRetHO_no.Text + "','info');", true);
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal qty;
            decimal amount;
            decimal qty_ship=0;
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                TextBox txqty_ship = (TextBox)e.Row.FindControl("txqty_ship");
                if (txqty_ship.Text == null) { qty_ship = 0; }
                else if (txqty_ship.Text == "") { qty_ship = 0; }
                else
                    qty_ship = decimal.Parse(txqty_ship.Text);
            }
            else
            {
                Label lbqty_ship = (Label)e.Row.FindControl("lbqty_ship");
                if (lbqty_ship.Text == null) { qty_ship = 0; }
                else if (lbqty_ship.Text == "") { qty_ship = 0; }
                else
                    qty_ship = decimal.Parse(lbqty_ship.Text);
            }
            Label lblqty = (Label)e.Row.FindControl("lblqty");
            Label lblRetHO_Amount = (Label)e.Row.FindControl("lblRetHO_Amount");

            if (lblqty.Text != null || lblqty.Text != "")
                qty = decimal.Parse(lblqty.Text);
            else
                qty = 0;
            if (lblRetHO_Amount.Text != null || lblRetHO_Amount.Text != "")
                amount = decimal.Parse(lblRetHO_Amount.Text);
            else
                amount = 0;
            

            totalQty = totalQty + qty;
            totalAmount = totalAmount + amount;
            totalQty_ship = totalQty_ship + qty_ship;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
            Label lblTotalqty_ship = (Label)e.Row.FindControl("lblTotalqty_ship");
            lblTotalqty.Text = totalQty.ToString();
            lblTotalAmount.Text = totalAmount.ToString("#,##0.00");
            lblTotalqty_ship.Text = totalQty_ship.ToString();
        }
    }
}