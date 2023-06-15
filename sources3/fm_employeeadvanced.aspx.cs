using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_employeeadvanced : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtstart.Text =  bll.vLookUp("select dbo.fn_getcontrolparameter('startemployeeadvanced')");
            dtend.Text = Request.Cookies["waz_dt"].Value;
            dtstart.CssClass = cd.csstextro;
            chAll_CheckedChanged(sender, e);
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        if (txsearchemployee.Text =="" || hdemployee.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Employee is not Selected!','Please Select Employee','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=advancedemp&start_dt="+dtstart.Text+"&end_dt="+dtend.Text+"&emp="+hdemployee.Value+"');", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] EmployeeList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }
    protected void btprintAll_Click(object sender, EventArgs e)
    {
      
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=advancedempAll&start_dt=" + dtstart.Text + "&end_dt=" + dtend.Text + "');", true);
    }
    protected void btprintAllSummary_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=advancedempAllSummary&start_dt=" + dtstart.Text + "&end_dt=" + dtend.Text + "');", true);
    }
    protected void chAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chAll.Checked == true)
        {
            txsearchemployee.Text = "";
            txsearchemployee.Enabled = false;
            btprintAll.Enabled = true;
            btprintAllSummary.Enabled = true;
            btprint.Enabled = false;
        }
        else
        {
            txsearchemployee.Enabled = true;
            btprintAll.Enabled = false;
            btprintAllSummary.Enabled = false;
            btprint.Enabled = true;
        }

    }
}