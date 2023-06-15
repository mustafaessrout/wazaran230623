using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_po_ho : System.Web.UI.Page
{
    cbll bll = new cbll();


    protected void Page_Load(object sender, EventArgs e)
    {       

        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@fld_nm", "po_sta_id"));
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbstatus, "sp_tfield_value_get", "fld_valu", "fld_desc",arr);
            cbbranch.Items.Insert(0, "<< ALL >>");
            btnSearch_Click(sender, e);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@branch", (cbbranch.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbbranch.SelectedValue.ToString())));
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@po_no", txpo.Text));
        bll.vBindingGridToSp(ref grdpo, "sp_request_po", arr);
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

    protected void grdpo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdpo.Rows[index];
            Label lbsalespoint = (Label)grdpo.Rows[index].FindControl("lbsalespoint");
            Label lbpono = (Label)grdpo.Rows[index].FindControl("lbpono");
            HiddenField hdsalespoint = (HiddenField)grdpo.Rows[index].FindControl("hdsalespoint");

            if (e.CommandName == "print")
            {                
                grdpo.UseAccessibleHeader = true;
                grdpo.HeaderRow.TableSection = TableRowSection.TableHeader;
                List<string> lFormula = new List<string>();
                lFormula.Add("{tmst_po.po_no} = '" + lbpono.Text + "'");
                lFormula.Add("{tmst_po.salespointcd} = '" + hdsalespoint.Value + "'");
                Session["lformula"] = lFormula;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "op", "openreport('fm_report2.aspx?src=po_br&no=" + lbpono.Text + "&salespoint=" + hdsalespoint.Value + "');", true);
            }
            else if (e.CommandName == "process")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_po_ho_dtl.aspx?dc=" + lbpono.Text + "&sp="+ hdsalespoint.Value + "');", true);
            }
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : PO Branch");
        }
    }

    protected void grdpo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdpo.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@branch", (cbbranch.SelectedValue.ToString() == "<< ALL >>" ? "ALL" : cbbranch.SelectedValue.ToString())));
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@po_no", txpo.Text));
        bll.vBindingGridToSp(ref grdpo, "sp_request_po", arr);
    }
}