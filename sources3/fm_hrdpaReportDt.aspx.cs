using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_hrdpaReportDt : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbDriver_SelectedIndexChanged(sender, e);
            BindControl();

        }
    }

    private void BindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (cbDriver.SelectedValue == "ActiveDriver")
        {
            arr.Add(new cArrayList("@job_title_cd", '5'));
            arr.Add(new cArrayList("@level_cd", '1'));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            cbemployee.DataSource = null;
            cbemployee.DataBind();
            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitleActive", "emp_cd", "emp_desc", arr);
        }
        else
        {
            arr.Add(new cArrayList("@job_title_cd", '5'));
            arr.Add(new cArrayList("@level_cd", '1'));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            cbemployee.DataSource = null;
            cbemployee.DataBind();
            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_desc", arr);
        }

    }

    protected void btnPrintDriverHis_Click(object sender, EventArgs e)
    {
        if (dtstart.Text == "" || dtend.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Start Date  and End date','Date Error !!','warning');", true);
            return;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=DriverHisDt&emp="
         + cbemployee.SelectedValue.ToString() + "&dtstart=" + dtstart.Text + "&dtend=" + dtend.Text + "');", true);
    }
    protected void btnPrintDriverHisAllDriver_Click(object sender, EventArgs e)
    {
        if (dtstart.Text == "" || dtend.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Start Date  and End date','Date Error !!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=DriverHisAllDriverDt&" +
            "&dtstart=" + dtstart.Text + "&isActive=" + cbDriver.SelectedValue + "&dtend=" + dtend.Text + "');", true);

    }

    protected void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (cbDriver.SelectedValue == "ActiveDriver")
        {
            arr.Add(new cArrayList("@job_title_cd", '5'));
            arr.Add(new cArrayList("@level_cd", '1'));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            cbemployee.DataSource = null;
            cbemployee.DataBind();
            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitleActive", "emp_cd", "emp_desc", arr);
        }
        else
        {
            arr.Add(new cArrayList("@job_title_cd", '5'));
            arr.Add(new cArrayList("@level_cd", '1'));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            cbemployee.DataSource = null;
            cbemployee.DataBind();
            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_desc", arr);

        }
    }
}