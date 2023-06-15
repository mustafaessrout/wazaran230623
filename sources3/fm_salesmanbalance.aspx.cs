using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesmanbalance : System.Web.UI.Page
{
    cbll bll = new cbll();
    double _balance = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            dtstart.Text = Request.Cookies["waz_dt"].Value;
            dtend.Text = Request.Cookies["waz_dt"].Value;
          
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));  
        bll.vBindingGridToSp(ref grd, "rptsalesmanbalance",arr);
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbbalance = (Label)e.Row.FindControl("lbbalance");
            Label lbcashin = (Label)e.Row.FindControl("lbcashin");
            Label lbcashout = (Label)e.Row.FindControl("lbcashout");

            //double _cashin = Convert.ToDouble(lbcashin.Text);
            //double _cashout = Convert.ToDouble(lbcashout.Text);
            //_balance+= (_cashin - _cashout);
            //lbbalance.Text = _balance.ToString("N2");
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=salesmanbalance&emp="+cbsalesman.SelectedValue+"&start="+dtstart.Text+"&end="+dtend.Text+"');", true);
    }

    protected void btprintall_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
           "openreport('fm_report2.aspx?src=salesmanbalanceall&s=" + dtstart.Text + "&e=" + dtend.Text + "');", true);

    }
}