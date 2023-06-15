using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_CashierPettyCashReport : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<cArrayList> arr = new List<cArrayList>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref ddlEmp, "sp_tmst_employee_getbyprofile1", "emp_cd", "emp_desc", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
            ddlEmp.SelectedIndex = 0;
        }

    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        dtDateFrom.Text = "";
        dtDateTo.Text = "";
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlEmp.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected ','Please Select Employee!','warning');", true);
            return;
        }

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

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=PettyCash&Emp_CD=" + ddlEmp.SelectedValue.ToString() +"&Date_From=" + dtDateFrom.Text + "&Date_To=" + dtDateTo.Text + "');", true);
        
    }

    protected void btnPrintCashoutRaw_Click(object sender, EventArgs e)
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

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=PettyCashRAW&Date_From=" + dtDateFrom.Text + "&Date_To=" + dtDateTo.Text + "');", true);
    }

}