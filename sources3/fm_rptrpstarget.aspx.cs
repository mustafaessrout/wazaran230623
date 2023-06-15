using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rptrpstarget : System.Web.UI.Page
{
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtrps.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=rpstarget&dt="+dtrps.Text+"');", true);
    }
}