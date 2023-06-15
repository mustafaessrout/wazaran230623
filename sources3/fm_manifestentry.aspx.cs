using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_manifestentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", "0"));
            bll.vBindingComboToSp(ref cbfrom, "sp_tmst_warehouse_get","whs_cd","whs_nm", arr);
            //bll.vBindingComboToSp(ref cbto, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbtrailer, "sp_tmst_trailer_box_get", "box_cd", "box_nm");
            arr.Clear();
            arr.Add(new cArrayList("@brinv_sta_id", "N"));
            bll.vBindingComboToSp(ref cbinvoice, "sp_tinvoice_branch_get", "invoice_no", "invoice_no",arr);
            bll.vBindingComboToSp(ref cbexpedition, "sp_tmst_company_expedition_get","comp_cd","comp_nm");
            arr.Clear();
            arr.Add(new cArrayList("@job_title_cd", "5"));
            bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_get", "emp_cd", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkBranchInvoice(arr);
            dttrip.Text = System.DateTime.Today.ToShortDateString();
        }
    }
    protected void btsearchinv_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@invoice_no", cbinvoice.SelectedValue.ToString()));
        bll.vInsertWrkBranchInvoice(arr);
        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdinv, "sp_twrk_branchinvoice_get", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<string> lformula = new List<string>();
        lformula.Add("{tmanifest_invoice.trip_no} = '" + txtripno.Text + "'");
        Session["lformula"] = lformula;
        Response.Redirect("fm_report.aspx?src=trip");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sTripNo = "";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trip_dt", dttrip.Text));
        arr.Add(new cArrayList("@drivername", cbdriver.SelectedItem.Text));
        arr.Add(new cArrayList("@comp_cd", cbexpedition.SelectedValue.ToString()));
        arr.Add(new cArrayList("@loc_from", cbfrom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@loc_to", cbto.SelectedValue.ToString()));
        arr.Add(new cArrayList("@trailerbox_no", cbtrailer.SelectedValue.ToString()));
        bll.vInsertManifestInvoice(arr, ref sTripNo);
        txtripno.Text = sTripNo;
        foreach (GridViewRow gr in grdinv.Rows)
        {
            Label lbinvoiceno = (Label)gr.FindControl("lbinvoiceno");
            arr.Clear();
            arr.Add(new cArrayList("@invoice_no", lbinvoiceno.Text));
            arr.Add(new cArrayList("@trip_no", sTripNo));
            bll.vInsertManifestDtl(arr);
        }
        btprint.Enabled = true;
        btsave.Enabled = false;
    }
}