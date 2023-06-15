using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_fm_reqcashout : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteWrkExhibitCashOut(arr);
            bll.vBindingFieldValueToCombo(ref cbcashouttype, "cashout_typ");
            bll.vBindingFieldValueToCombo(ref cbinout, "inout");
            bll.vBindingComboToSp(ref cbexhibition, "sp_tmst_exhibition_get", "exhibit_cd", "exhibit_nm");
            if (Request.Cookies["exh_cd"] != null)
            { 
                cbexhibition.SelectedValue = Request.Cookies["exh_cd"].Value.ToString();
                cbexhibition.CssClass = "form-control ro";
            }
            vBindingItem();
        }

    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitcashout_get", arr);
    }
    void vBindingItem()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cashout_typ", cbcashouttype.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitemcashout, "sp_texhibit_mstitemcashout_get", "itemco_cd","itemco_nm", arr);
    }
    protected void cbcashouttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingItem();
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingItem();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        double dAmt = 0;
        if (!double.TryParse(txamt.Text, out dAmt))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Qty must numeric!','Check Qty');", true);
            return;
        }

        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Remark can not empty!','Pls fill remark');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@amt", txamt.Text));
        arr.Add(new cArrayList("@itemco_cd", cbitemcashout.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertWrkExhibitCashOut(arr);
        arr.Clear();
        vBindingGrid();
        txamt.Text = "0";
       
    }
    protected void btsaved_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@exhibit_cd", Request.Cookies["exh_cd"].Value.ToString()));
        arr.Add(new cArrayList("@cashout_sta_id", "N"));
        arr.Add(new cArrayList("@remark", txremark.Text));
        string sNo = string.Empty;
        bll.vInsertExhibitCashout(arr, ref sNo);
        lbcashoutno.Text = sNo;
        txamt.CssClass = "form-control ro";
        cbcashouttype.Items.Clear();
        cbinout.Items.Clear();
        cbitemcashout.Items.Clear();
        cbcashouttype.CssClass = "form-control ro";
        cbinout.CssClass = "form-control ro";
        cbitemcashout.CssClass = "form-control ro";
        grd.CssClass = "mydatagrid ro";
        btsave.CssClass = "btn btn-primary ro";
        btsaved.CssClass = "btn btn-primary ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New cashout saved!','"+sNo+"','success');", true);
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@itemco_cd", lbitemcode.Text));
        bll.vDeleteWrkExhibitCashOut(arr);
        vBindingGrid();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=exhcashout&no="+hdcashout.Value.ToString()+"');", true);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        cbcashouttype.CssClass = "form-control ro"; cbcashouttype.Items.Clear();
        cbinout.CssClass = "form-control ro"; cbinout.Items.Clear();
        btsave.CssClass = "btn btn-primary ro";
        cbitemcashout.CssClass = "form-control ro"; cbitemcashout.Items.Clear();
        txamt.Text = ""; txamt.CssClass = "form-control ro";
        txremark.Text = ""; txremark.CssClass = "form-control ro";
        lbcashoutno.Text = hdcashout.Value.ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_cd", hdcashout.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertWrkExhibitCashoutFromCore(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitcashout_get", arr);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_reqcashout.aspx");
    }
}