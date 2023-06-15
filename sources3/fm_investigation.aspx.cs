using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class fm_investigation : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='N'");
            txivgdate.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value.ToString();
            bll.vBindingComboToSp(ref cbinvtype, "sp_invtype_get", "invtype", "stktypeName");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_get", "emp_cd", "emp_nm", arr);
            cbSalesPointCD_SelectedIndexChanged(sender, e);
            //AutoCompleteExtender1.ContextKey = cbSalesPointCD.SelectedValue;
            bindinggrd();
            bindinggrdlist();
        }
    }
    protected void cbinvtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbSalesPointCD_SelectedIndexChanged(sender, e);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != txivgdate.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            return;
        }
        
        String ivgno;
        if (txivgno.Text == "" || txivgno.Text == "NEW") { ivgno = Request.Cookies["usr_id"].Value.ToString(); } else { ivgno = txivgno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ivgno",ivgno));
        arr.Add(new cArrayList("@salespointcd",cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@invtype",cbinvtype.SelectedValue));
        arr.Add(new cArrayList("@ivgdrefcd", hdivgdrefcd.Value));
        arr.Add(new cArrayList("@ivgddescription", txtivgddescription.Text));
        bll.vInserttinvestigationDtl(arr);
        bindinggrd();
    }
    private void bindinggrd()
    {
        //totalQty = 0;
        //totalAmount = 0;
        string ivgno;
        if (txivgno.Text == "" || txivgno.Text == "NEW") { ivgno = Request.Cookies["usr_id"].Value.ToString(); } else { ivgno = txivgno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ivgno", ivgno));
        arr.Add(new cArrayList("@salespointcd",cbSalesPointCD.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tinvestigationDtl_get", arr);
    }
    private void bindinggrdlist()
    {
        string ivgno;
        if (txivgno.Text == "" || txivgno.Text == "NEW") { ivgno = Request.Cookies["usr_id"].Value.ToString(); } else { ivgno = txivgno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ivgno", ivgno));
        arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
        bll.vBindingGridToSp(ref grdlist, "sp_tinvestigationList_get", arr);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_investigation.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != txivgdate.Text)
        {
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('RefCD has not yet selected !','Please select RefCD To Be Investigation','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (txivgno.Text == "NEW" || txivgno.Text == "")
        {
            string sivgno = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@invtype", cbinvtype.SelectedValue));
            arr.Add(new cArrayList("@ivgdate", DateTime.ParseExact(txivgdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@ivgemp_cd", cbemployee.SelectedValue));
            arr.Add(new cArrayList("@ivgreason", txtivgddescription.Text));
            arr.Add(new cArrayList("@createdtby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInserttinvestigation(arr, ref sivgno);
            txivgno.Text = sivgno;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','No. " + txivgno.Text + "','info');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        }
        else
        {
            String ivgno;
            if (txivgno.Text == "" || txivgno.Text == "NEW") { ivgno = Request.Cookies["usr_id"].Value.ToString(); } else { ivgno = txivgno.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@ivgno", ivgno));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@invtype", cbinvtype.SelectedValue));
            arr.Add(new cArrayList("@ivgdate", DateTime.ParseExact(txivgdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@ivgemp_cd", cbemployee.SelectedValue));
            arr.Add(new cArrayList("@ivgreason", txtivgddescription.Text));
            bll.vUpdatetinvestigation(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Updated successfully ..','No. " + txivgno.Text + "','info');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        }
        bindinggrd();
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (lbstatus.Text == "COMPLETE")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (Request.Cookies["waz_dt"].Value.ToString() != txivgdate.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (txivgno.Text != "" || txivgno.Text != "NEW")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@ivgno", txivgno.Text));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            bll.vDeletetinvestigation(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            Response.Redirect("fm_investigation.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ivgno", txivgno.Text));
        arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@printby", Request.Cookies["usr_id"].Value.ToString()));
        Session["lParamivgno"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=ivgno');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != txivgdate.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            return;
        }
        Label lblivgno = (Label)grd.Rows[e.RowIndex].FindControl("lblivgno");
        Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
        Label lblinvtype = (Label)grd.Rows[e.RowIndex].FindControl("lblinvtype");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ivgno", lblivgno.Text));
        arr.Add(new cArrayList("@salespointcd", lblsalespointCD.Text));
        arr.Add(new cArrayList("@invtype", lblinvtype.Text));
        bll.vDeletetinvestigationDtl(arr);
        bindinggrd();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        string[] data = contextKey.Split(',');
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@trnstkno", prefixText));
        arr.Add(new cArrayList("@salespointcd", data[0]));
        arr.Add(new cArrayList("@invtype", data[1]));
        bll.vSearchtblTrnStock(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["trnstkno"].ToString() + " | " + rs["trnstkdate"].ToString(), rs["trnstkno"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }
    
    protected void cbSalesPointCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] data = new string[2];
        data[0] = cbSalesPointCD.SelectedValue.ToString();
        data[1] = cbinvtype.SelectedValue.ToString();
        string passVal = data[0] + ',' + data[1];
        //AutoCompleteExtender1.ContextKey = cbSalesPointCD.SelectedValue;
        AutoCompleteExtender1.ContextKey = passVal;
        Session["looivgsp"] = cbSalesPointCD.SelectedValue;
        //Session["looinvtype"] = cbinvtype.SelectedValue;
    }

    protected void txtsearchivgdrefcd_TextChanged(object sender, EventArgs e)
    {

    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        string sstatus;
        txivgno.Text = Convert.ToString(Session["looivgno"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ivgno", txivgno.Text));
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(Session["looivgsp"])));
        bll.vGettinvestigation(arr, ref rs);
        while (rs.Read())
        {
            string sdate = string.Format("{0:d/M/yyyy}", rs["ivgdate"]);
            DateTime dtdate = Convert.ToDateTime(sdate);
            string strDate = dtdate.ToString("d/M/yyyy");
            sstatus = rs["sta_id"].ToString();
            txivgdate.Text = strDate;//string.Format("{0:d/M/yyyy}", rs["trnstkDate"]);
            cbemployee.SelectedValue = rs["ivgemp_cd"].ToString();
            cbSalesPointCD.SelectedValue = rs["salespointcd"].ToString();
            cbSalesPointCD_SelectedIndexChanged(sender, e);
            cbinvtype.SelectedValue = rs["invtype"].ToString();
            txivgreason.Text = rs["ivgreason"].ToString();
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + sstatus + "'");
            bindinggrd();
        } rs.Close();
        bindinggrd();
        bindinggrdlist();
    }

    protected void grdlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (lbstatus.Text == "COMPLETE")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be edit','error');", true);
            return;
        }
        if (Request.Cookies["waz_dt"].Value.ToString() != txivgdate.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            grdlist.EditIndex = -1;
            bindinggrdlist();
            return;
        }
        Label lblivgcheck = (Label)grdlist.Rows[e.NewEditIndex].FindControl("lblivgcheck");
        grdlist.EditIndex = e.NewEditIndex;
        bindinggrdlist();
        DropDownList cboivgcheck = (DropDownList)grdlist.Rows[e.NewEditIndex].FindControl("cboivgcheck");
        bll.vBindingFieldValueToCombo(ref cboivgcheck, "check");
        cboivgcheck.SelectedValue = lblivgcheck.Text;
    }
    protected void grdlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdlist.EditIndex = -1;
        bindinggrdlist();
    }
    protected void grdlist_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        
    }
    protected void grdlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbfld_valu = (Label)grdlist.Rows[e.RowIndex].FindControl("lbfld_valu");
        DropDownList cboivgcheck = (DropDownList)grdlist.Rows[e.RowIndex].FindControl("cboivgcheck");
        String ivgno;
        if (txivgno.Text == "" || txivgno.Text == "NEW") { ivgno = Request.Cookies["usr_id"].Value.ToString(); } else { ivgno = txivgno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ivgno", ivgno));
        arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@ivglist", lbfld_valu.Text));
        arr.Add(new cArrayList("@check", cboivgcheck.SelectedValue));
        bll.vUpdatetinvestigationList(arr);
        grdlist.EditIndex = -1; arr.Clear();
        bindinggrdlist();
    }
}