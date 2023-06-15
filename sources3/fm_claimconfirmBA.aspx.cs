using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimconfirmBA : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirmBA_get");
            //cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
            //cbbranch.Enabled = false;

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)grdCLAIM.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in grdCLAIM.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("approve");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void btconfirm_Click(object sender, EventArgs e)
    {
       string invoiceOrder = "";
       foreach (GridViewRow row in grdCLAIM.Rows)
        {
            //string dtp1 = Request.Cookies["waz_dt"].ToString("d/M/yyyy");
            DateTime dtpayp1 = DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("approve") as CheckBox);
                if (chkRow.Checked)
                {
                    Label lbType = (Label)row.FindControl("lbType");
                    Label lbinvno = (Label)row.FindControl("lbinvoice");
                    Label lbdisc = (Label)row.FindControl("lbdiscount");
                    HiddenField hdfDiscount = (HiddenField)row.FindControl("hdfDiscount");
                    TextBox txremark = (TextBox)row.FindControl("txremark");
                    CheckBox chsign = (CheckBox)row.FindControl("chsign");
                    CheckBox chexclude = (CheckBox)row.FindControl("chexclude");
                    //FileUpload uplo = (FileUpload)row.FindControl("uplo");
                    //FileUpload uplf = (FileUpload)row.FindControl("uplf");
                    List<cArrayList> arr = new List<cArrayList>();
                    //countInv = bll.vLookUp("select * from ");
                    //arr.Add(new cArrayList("@disc_cd", hdfDiscount.Value));
                    arr.Add(new cArrayList("@disc_cd", lbdisc.Text));
                    if (chsign.Checked)
                    {
                        arr.Add(new cArrayList("@isstamp", "1"));
                    }
                    else
                    {
                        arr.Add(new cArrayList("@isstamp", "0"));
                        if (txremark.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Remark Can not empty, If No Stamp.','Please check the Stamp or Remark can not Empty !','warning');", true);
                            return;
                        }
                        else
                        {
                            arr.Add(new cArrayList("@remark", txremark.Text));
                        }
                    }

                    invoiceOrder = bll.vLookUp("SELECT fileinv FROM tclaim_invoice where inv_no='" + lbinvno.Text + "'");

                    if (invoiceOrder == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This invoice, not yet upload!!!','Please upload this invoice first!','warning');", true);
                        return;
                    }

                    arr.Add(new cArrayList("@issign", "1"));
                    arr.Add(new cArrayList("@isexclude", chexclude.Checked == true ? "1" : "0"));
                    arr.Add(new cArrayList("@inv_no", lbinvno.Text));
                    arr.Add(new cArrayList("@received_dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                    bll.vInsertClaimConfirm(arr);
                }
            }
        }
       bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirmBA_get");
    }
    protected void invoice_Click(object sender, EventArgs e)
    {
        hdopinv.Value = "1";
    }
    protected void chapprove_CheckedChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow row in grdCLAIM.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("approve") as CheckBox);
                if (chkRow.Checked)
                {
                    Label lbinvno = (Label)row.FindControl("lbinvoice");
                    //Label lbdisc = (Label)row.FindControl("lbdiscount");
                    HiddenField hdfDiscount = (HiddenField)row.FindControl("hdfDiscount");
                    List<cArrayList> arr = new List<cArrayList>();
                    string x = bll.vLookUp("select count(inv_no) from twrk_claimconfirm where disc_cd='" + lbinvno.Text + "' and inv_no='" + Convert.ToString(hdfDiscount.Value) + "'");
                    if (x == "1")
                    {
                        chkRow.Checked = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Check invoice detail','Please open the invoice and check if there is item needs to scratch !','warning');", true);
                        chkRow.Checked = false;
                        return;


                    }
                }
            }
        }
        

    }
  
    protected void grdCLAIM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@search", txsearch.Text.ToString()));
        bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirmBA_get", arr);
    }
}