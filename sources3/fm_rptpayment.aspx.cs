using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rptpayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dtpayment.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (cbpaydet.Checked)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rpaydt&tg=" + dtpayment.Text + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rpay&tg=" + dtpayment.Text + "');", true);
        }
        
    }
}