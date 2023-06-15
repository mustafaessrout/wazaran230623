using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_requestproposal : System.Web.UI.Page
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
            cbYear.Items.Insert(0, "<< ALL >>");
            btnSearch_Click(sender, e);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor", (cbvendor.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbvendor.SelectedValue.ToString())));
        arr.Add(new cArrayList("@month", (cbMonth.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbMonth.SelectedValue.ToString())));
        arr.Add(new cArrayList("@year", (cbYear.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbYear.SelectedValue.ToString())));
        arr.Add(new cArrayList("@prop_no", txProposal.Text));
        bll.vBindingGridToSp(ref grdproposal, "sp_request_proposal", arr);
    }

    protected void grdproposal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try { 
            List<cArrayList> arr = new List<cArrayList>();
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
            else if (e.CommandName == "approve") 
            {
                if (bll.nCheckAccess("propapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this Proposal. contact Administrator !!','warning');", true);
                    return;
                }
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdproposal.Rows[index];
                Label lbproposal = (Label)grdproposal.Rows[index].FindControl("lbproposal");
                Label lbrequest = (Label)grdproposal.Rows[index].FindControl("lbrequest");
                arr.Clear();
                arr.Add(new cArrayList("@prop_no", lbproposal.Text.ToString()));
                arr.Add(new cArrayList("@reqdisc_cd", lbrequest.Text.ToString()));
                arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@approval", 1));
                bll.vUpdatetMstProposal(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal has been approved','" + lbproposal.Text + "','success');", true);
            }
            else if (e.CommandName == "reject") 
            {            

                if (bll.nCheckAccess("proprej", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To reject this Proposal. contact Administrator !!','warning');", true);
                    return;
                }
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdproposal.Rows[index];
                Label lbproposal = (Label)grdproposal.Rows[index].FindControl("lbproposal");
                Label lbrequest = (Label)grdproposal.Rows[index].FindControl("lbrequest");
                //lbproposalrej.Value = lbproposal.Text.ToString();
                //lbdiscrej.Value = lbrequest.Text.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "fn_reject('" + lbproposal.Text.ToString() + "','" + lbrequest.Text.ToString() + "');", true);            
            }
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Proposal Reject");
        }
    }

    protected void grdproposal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdproposal.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor", (cbvendor.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbvendor.SelectedValue.ToString())));
        arr.Add(new cArrayList("@month", (cbMonth.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbMonth.SelectedValue.ToString())));
        arr.Add(new cArrayList("@year", (cbYear.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbYear.SelectedValue.ToString())));
        arr.Add(new cArrayList("@prop_no", txProposal.Text));
        bll.vBindingGridToSp(ref grdproposal, "sp_request_proposal", arr);
    }

    protected void btrejectremark_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@prop_no", lbproposalrej.Value.ToString()));
            arr.Add(new cArrayList("@reqdisc_cd", lbdiscrej.Value.ToString()));
            arr.Add(new cArrayList("@noted", txremarkreject.Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@approval", 2));
            bll.vUpdatetMstProposal(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal has been rejected','" + lbproposalrej.Value.ToString() + "','warning');", true);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Proposal Reject");
        }
    }
}