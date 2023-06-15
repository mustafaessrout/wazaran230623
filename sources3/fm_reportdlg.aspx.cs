using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reportdlg : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd",Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_cd", arr);
        }
    }
    protected void btreport_Click(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue == "cs")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=soa');", true);
        }
        else if (cbreport.SelectedValue.ToString()=="iv")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=riv');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "is")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=is&sa=" + cbsalesman.SelectedValue.ToString() +  "');", true);
        }
    }
}