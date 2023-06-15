using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_customerTransferApp : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtstart.Text = Request.Cookies["waz_dt"].Value;
            dtend.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=TransCustApp&from_dt="+dtstart.Text+"&to_dt="+dtend.Text+"');", true);
    }

    
}