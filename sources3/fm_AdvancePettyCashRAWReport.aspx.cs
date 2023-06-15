using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_AdvancePettyCashRAWReport : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<cArrayList> arr = new List<cArrayList>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (dtDateFrom.Text != "" && dtDateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date To is not Selected ','Please Select Date To!','warning');", true);
            return;
        }
        if (dtDateFrom.Text == "" && dtDateTo.Text != "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date From is not Selected ','Please Select Date From!','warning');", true);
            return;
        }
        if (dtDateFrom.Text == "" && dtDateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date From and To is not Selected ','Please Select Date From and To!','warning');", true);
            return;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=AdvancePettyCashRAW&Date_From=" + dtDateFrom.Text + "&Date_To=" + dtDateTo.Text + "');", true);
        
    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        if (dtDateFrom.Text != "" && dtDateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date To is not Selected ','Please Select Date To!','warning');", true);
            return;
        }
        if (dtDateFrom.Text == "" && dtDateTo.Text != "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date From is not Selected ','Please Select Date From!','warning');", true);
            return;
        }
        if (dtDateFrom.Text == "" && dtDateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date From and To is not Selected ','Please Select Date From and To!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=CashInCashOutRAW&Date_From=" + dtDateFrom.Text + "&Date_To=" + dtDateTo.Text + "');", true);
    }
}