using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimconfirmco : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@search", txsearch.Text.ToString()));
            bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirmco_get");
            //cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
            //cbbranch.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
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
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void btconfirm_Click(object sender, EventArgs e)
    {
        string statusBudget = "";
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


                    Label lbclaimco = (Label)row.FindControl("lbclaimco");
                    Label lbcashout = (Label)row.FindControl("lbcashout");
                    Label lbproposal = (Label)row.FindControl("lbproposal");
                    TextBox txremark = (TextBox)row.FindControl("txremark");
                    CheckBox chsign = (CheckBox)row.FindControl("chsign");
                    List<cArrayList> arr = new List<cArrayList>();

                    
                    arr.Clear();
                    //countInv = bll.vLookUp("select * from ");
                    arr.Add(new cArrayList("@disc_cd", lbclaimco.Text));
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

                    invoiceOrder = bll.vLookUp("SELECT fileinv FROM tclaim_invoice where inv_no='" + lbcashout.Text + "'");

                    if ( invoiceOrder == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This invoice, not yet upload!!!','Please upload this invoice first!','warning');", true);
                        return;
                    }

                    arr.Add(new cArrayList("@issign", "1"));
                    arr.Add(new cArrayList("@inv_no", lbcashout.Text));
                    arr.Add(new cArrayList("@received_dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                    statusBudget = bll.vLookUp("select tp.rdcost from tmst_proposal tp where tp.prop_no= '" + lbproposal.Text + "' ");
                    bll.vInsertClaimConfirm(arr);
                    arr.Clear();
                    
                }
            }
        }
        bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirmco_get");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void invoice_Click(object sender, EventArgs e)
    {
        hdopinv.Value = "1";
    }
    protected void grdCLAIM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCLAIM.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirmco_get");
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
                    Label lbclaimco = (Label)row.FindControl("lbclaimco");
                    Label lbcashout = (Label)row.FindControl("lbcashout");
                    List<cArrayList> arr = new List<cArrayList>();
                    string x = bll.vLookUp("select count(inv_no) from twrk_claimconfirm where disc_cd='" + lbclaimco.Text + "' and inv_no='" + lbcashout.Text + "'");
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
        bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirmco_get", arr);
    }
}