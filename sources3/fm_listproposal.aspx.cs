using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_listproposal : System.Web.UI.Page
{

    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            bll.vBindingComboToSp(ref cbYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            cbvendor.Items.Insert(0, "<< ALL >>");
            cbMonth.Items.Insert(0, "<< ALL >>");
            //btnSearch_Click(sender, e);
            vCheckAccess();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor", (cbvendor.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbvendor.SelectedValue.ToString())));
        arr.Add(new cArrayList("@month", (cbMonth.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbMonth.SelectedValue.ToString())));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prop_no", txProposal.Text));
        bll.vBindingGridToSp(ref grdproposal, "sp_report_proposal3", arr);
    }

    protected void grdproposal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdproposal.Rows[index];
            Label lbproposal = (Label)grdproposal.Rows[index].FindControl("lbproposal");
            Label lbcust = (Label)grdproposal.Rows[index].FindControl("lbcust");
            Label lbitem = (Label)grdproposal.Rows[index].FindControl("lbitem");
            grdproposal.UseAccessibleHeader = true;
            grdproposal.HeaderRow.TableSection = TableRowSection.TableHeader;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "op", "openreport('fm_report2.aspx?src=prop&prop_no=" + lbproposal.Text + "&cust=" + lbcust.Text + "&product=" + lbitem.Text + "&dbp=no');", true);
        }
    }

    protected void grdproposal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdproposal.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor", (cbvendor.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbvendor.SelectedValue.ToString())));
        arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prop_no", txProposal.Text));
        bll.vBindingGridToSp(ref grdproposal, "sp_report_proposal3", arr);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_proposal.aspx");
    }
    void vCheckAccess()
    {
        int nAccess = bll.nCheckAccess("proposal", Request.Cookies["usr_id"].Value.ToString());
        if (nAccess == 0)
        {
            btnNew.CssClass = "divhid";
        }
    }

}