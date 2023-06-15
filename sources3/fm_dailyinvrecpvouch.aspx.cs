using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_dailyinvrecpvouch : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txdt1.Text = Request.Cookies["waz_dt"].Value.ToString();
            txdt2.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }

    protected void btreport_Click(object sender, EventArgs e)
    {                        
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=dailyinvrecp&startdate=" + txdt1.Text + "&enddate=" + txdt2.Text + "');", true);
           // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=dailyinvrecp');", true);        
        
       // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=dailyinvrecp');", true);
    }        
}