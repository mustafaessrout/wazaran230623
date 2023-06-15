using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_DeliveryOAndOtherDeliveryReport : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<cArrayList> arr = new List<cArrayList>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
        }

    }

    protected void btnDOI_Click(object sender, EventArgs e)
    {

        if (dtDateFrom .Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date From','Date From can not be Empty !!','warning');", true);
            return;
        }
        if ( dtDateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date To','Date To can not be Empty !!','warning');", true);
            return;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=DOI&date_from=" + dtDateFrom.Text +"&date_to=" + dtDateTo.Text+ "');", true);

    }
    protected void btnDODI_Click(object sender, EventArgs e)
    {
        if (dtDateFrom.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date From','Date From can not be Empty !!','warning');", true);
            return;
        }
        if (dtDateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date To','Date To can not be Empty !!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=DODI&date_from=" + dtDateFrom.Text + "&date_to=" + dtDateTo.Text + "');", true);
    
    }
    protected void btnOD_Click(object sender, EventArgs e)
    {
        if (dtDateFrom.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date From','Date From can not be Empty !!','warning');", true);
            return;
        }
        if (dtDateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date To','Date To can not be Empty !!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=OD&date_from=" + dtDateFrom.Text + "&date_to=" + dtDateTo.Text + "');", true);

    }
    protected void btnDLCR_Click(object sender, EventArgs e)
    {
        if (dtDateFrom.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date From','Date From can not be Empty !!','warning');", true);
            return;
        }
        if (dtDateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date To','Date To can not be Empty !!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=DLCR&date_from=" + dtDateFrom.Text + "&date_to=" + dtDateTo.Text + "');", true);
    }
}