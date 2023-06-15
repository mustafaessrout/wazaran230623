using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cust_adjust_price : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbll bll = new cbll();
        if(!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@job_title_cd", "14"));
            bll.vBindingComboToSp(ref cbsupervisour, "sp_tmst_employee_get", "emp_cd", "emp_desc", arr);            
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=customertypeprice&spv_cd=" + cbsupervisour.SelectedValue.ToString() + "&p=" + cbreport.SelectedValue.ToString() + "');", true);
    }
}